import * as _ from 'lodash';
import { AbpMultiTenancyService } from '@abp/multi-tenancy/abp-multi-tenancy.service';
import { AbpSessionService } from '@abp/session/abp-session.service';
import {
    Component,
    ElementRef,
    Injector,
    OnInit,
    ViewChild,
    ViewEncapsulation
} from '@angular/core';
import { AppAuthService } from '@app/shared/common/auth/app-auth.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AppConsts } from '@shared/AppConsts';
import { AppNavigationService } from './nav/app-navigation.service';
import {
    ChangeUserLanguageDto,
    ItemMenuDto,
    LinkedUserDto,
    MenuServicoServiceProxy,
    NavigationDto,
    ObterItemMenuInput,
    ObterItensDeMenuPorUsuarioEPapelInput,
    PapelDto,
    ProfileServiceProxy,
    UserLinkServiceProxy
} from '@shared/service-proxies/service-proxies';
import { ImpersonationService } from '@app/admin/users/impersonation.service';
import { LinkedAccountService } from '@app/shared/layout/linked-account.service';
import { NavegacaoService } from '../services/navegacao.service';
import { PapelService } from '../services/papel.service';
import { Router } from '@angular/router';
import { UsuarioService } from '../services/usuario.service';

@Component({
    templateUrl: './topbar.component.html',
    selector: 'topbar',
    styleUrls: ['./topbar.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class TopBarComponent extends AppComponentBase implements OnInit {

    languages: abp.localization.ILanguageInfo[];
    currentLanguage: abp.localization.ILanguageInfo;
    isImpersonatedLogin = false;
    isMultiTenancyEnabled = false;
    shownLoginName = '';
    tenancyName = '';
    userName = '';
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    defaultLogo = AppConsts.appBaseUrl + '/assets/common/images/app-logo-on-' + this.currentTheme.baseSettings.menu.asideSkin + '.svg';
    recentlyLinkedUsers: LinkedUserDto[];
    unreadChatMessageCount = 0;
    remoteServiceBaseUrl: string = AppConsts.remoteServiceBaseUrl;
    chatConnected = false;

    public papeis: PapelDto[] = [];
    public papeisAux: PapelDto[] = [];
    public papelAtual: PapelDto = new PapelDto();
    public papelTemp: PapelDto[] = [];
    public papeisID: number[] = [];

    public papelPesquisa: PapelDto[] = [];
    public menuPesquisa: NavigationDto[] = [];
    private menuOriginal: ItemMenuDto[] = [];

    public textoDePesquisa = '';
    public textoPesquisaParcial = '';
    public pesquisou = true;
    public pesquisouMenu = true;
    public textoPesquisaMenu = '';
    private menuAux: ItemMenuDto[] = [];

    public dropdownPersistent = 1;

    @ViewChild('searchFilterText') searchFilterTextElement: ElementRef;
    @ViewChild('searchFilterTextParcial') searchFilterTextElementParcial: ElementRef;
    @ViewChild('dropdownPesquisaElement') dropdownPesquisaElement: ElementRef;

    constructor(
        injector: Injector,
        private _abpSessionService: AbpSessionService,
        private _abpMultiTenancyService: AbpMultiTenancyService,
        private _profileServiceProxy: ProfileServiceProxy,
        private _userLinkServiceProxy: UserLinkServiceProxy,
        private _authService: AppAuthService,
        private _impersonationService: ImpersonationService,
        private _linkedAccountService: LinkedAccountService,
        private _papelService: PapelService,
        private _usuarioService: UsuarioService,
        public navegacaoService: NavegacaoService,
        private _menuService: MenuServicoServiceProxy,
        private router: Router
    ) {
        super(injector);
    }

    ngOnInit() {
        this.isMultiTenancyEnabled = this._abpMultiTenancyService.isEnabled;
        this.languages = _.filter(this.localization.languages, l => (l).isDisabled === false);

        this._papelService.obterPapeis().then(x => this.papeis = x).then(y => this.removerAcentoPapeis());
        this._papelService.onTrocarDePapel.subscribe((papel: PapelDto) => {
            this.papelAtual = papel;
            this.obterItensDeMenu(papel);
        });

        this.currentLanguage = this.localization.currentLanguage;
        this.isImpersonatedLogin = this._abpSessionService.impersonatorUserId > 0;

        this.setCurrentLoginInformations();
        this.getProfilePicture();
        this.getRecentlyLinkedUsers();

        this.registerToEvents();
    }

    public definirFocuMenu() {
        setTimeout(() => this.searchFilterTextElement.nativeElement.focus(), 0);
        this.pesquisouMenu = false;
        this.textoPesquisaMenu = '';
    }

    public pesquisarMenu(navigations: ItemMenuDto[], texto: string): ItemMenuDto[] {

        let retorno: ItemMenuDto[] = [];

        for (let nav of navigations) {
            if (nav.titulo.toLowerCase().includes(texto.toLowerCase())) {
                retorno.push(nav);
            }
            if (nav.filhos.length > 0) {
                let retornoFilho = this.pesquisarMenu(nav.filhos, texto);
                if (retornoFilho.length > 0) {
                    for (let f of retornoFilho) {
                        retorno.push(f);
                    }
                }
            }
        }

        return retorno;
    }

    obterItensDeMenu(papelAtual: PapelDto) {

        let filtro: ObterItensDeMenuPorUsuarioEPapelInput = new ObterItensDeMenuPorUsuarioEPapelInput();
        filtro.idPapel = papelAtual.id;
        this.menuOriginal.length = 0;
        this.menuAux.length = 0;
        filtro.codigoExternoUsuario = this._papelService.obterUserName();

        this._menuService.obterItensDeMenuRotas(filtro)
            .subscribe(menu => {
                this.menuOriginal = menu;
                this.menuAux = menu;
            });
    }

    pesquisarMenuParcial() {

        let pesquisa = this.textoPesquisaMenu;
        pesquisa = pesquisa.toLocaleLowerCase();

        this.menuAux = this.pesquisarMenu(this.menuOriginal, pesquisa);
    }

    onChangeMenu(itemMenu: ItemMenuDto) {
        let nav: NavigationDto = new NavigationDto();
        nav.id = itemMenu.id;
        nav.moduloAcesso = itemMenu.moduloAcesso
        nav.rota = itemMenu.rota;
        this.navegacaoService.navigateTo(nav);
        this._papelService.trocarPapel(this.papelAtual);
    }

    fecharPesquisaMenu() {
        this.dropdownPesquisaElement.nativeElement.classList.remove('m-dropdown--open');
        this.dropdownPesquisaElement.nativeElement.classList.remove('m-dropdown__wrapper');
        this.textoPesquisaMenu = '';
        this.pesquisouMenu = true;
    }


    public definirFocus() {
        setTimeout(() => this.searchFilterTextElement.nativeElement.focus(), 0);
        this.pesquisou = false;
        this.textoDePesquisa = '';
        this.papelPesquisa = this.papeis;
    }

    public definirFocusPerquisaPapeis() {
        setTimeout(() => this.searchFilterTextElementParcial.nativeElement.focus(), 0);
        this.pesquisou = false;
        this.textoPesquisaParcial = '';
        this.papeisAux = this.papeis;
    }

    public pesquisar() {
        this.papelPesquisa = this.papeis.filter(x => x.nome.toLowerCase().includes(this.textoDePesquisa.toLowerCase()));
    }

    public pesquisarParcial() {
        if (this.papeisAux.length > 0) {
            this.papeisAux = [];
        }

        let pesquisa = this.removeAcentoString(this.textoPesquisaParcial);
        pesquisa = pesquisa.toLocaleLowerCase();

        this.papeisID = this.papelTemp.filter(x => x.nome.toLocaleLowerCase().includes(pesquisa)).map(y => y.id);

        this.papeisID.forEach(element => {
            let papel = this.papeis.find(x => x.id === element);
            this.papeisAux.push(papel);
        });
    }

    removerAcentoPapeis() {
        this.papeis.forEach(element => {
            let papel: PapelDto = new PapelDto();
            papel.id = element.id;
            papel.mnemonico = element.mnemonico;
            papel.nome = this.removeAcentoString(element.nome);
            this.papelTemp.push(papel);

        });
    }

    removeAcentoString(str) {
        str = str.toLowerCase();
        str = str.replace(/[ÀÁÂÃÄÅ]/, "A");
        str = str.replace(/[àáâãäå]/, "a");
        str = str.replace(/[ÈÉÊË]/, "E");
        str = str.replace(/[èéê]/, "e");
        str = str.replace(/[òóôõ]/, "o");
        str = str.replace(/[ìí]/, "i");
        str = str.replace(/[ú]/, "u");
        str = str.replace(/[Ç]/, "C");
        str = str.replace(/[ç]/, "c");

        // o resto
        let texto = str.normalize('NFD').replace(/[\u0300-\u036f]/g, "")
        //console.log(texto);
        return str.normalize('NFD').replace(/[\u0300-\u036f]/g, "");
    }

    fecharPesquisa() {
        this.dropdownPesquisaElement.nativeElement.classList.remove('m-dropdown--open');
        this.dropdownPesquisaElement.nativeElement.classList.remove('m-dropdown__wrapper');
        this.textoPesquisaParcial = '';
        this.textoDePesquisa = '';
        this.pesquisou = true;
    }

    public navegarPara(item) {
        this.navegacaoService.navigateTo(item);
        this.fecharPesquisa();
        this.textoPesquisaParcial = '';
    }

    registerToEvents() {
        abp.event.on('profilePictureChanged', () => {
            this.getProfilePicture();
        });

        abp.event.on('app.chat.unreadMessageCountChanged', messageCount => {
            this.unreadChatMessageCount = messageCount;
        });

        abp.event.on('app.chat.connected', () => {
            this.chatConnected = true;
        });

        abp.event.on('app.getRecentlyLinkedUsers', () => {
            this.getRecentlyLinkedUsers();
        });

        abp.event.on('app.onMySettingsModalSaved', () => {
            this.onMySettingsModalSaved();
        });
    }

    onChangePapel(papel: PapelDto) {
        if (papel && papel.id) {
            let papelSelecionado = this.papeis.filter(x => x.id == papel.id);
            this.papelAtual = this._papelService.trocarPapel(papelSelecionado[0]);
            this.obterItensDeMenu(papelSelecionado[0]);
            this.router.navigate(['/app/administracao/home/dashboard']);
            this.fecharPesquisa();
        }
    }

    changeLanguage(languageName: string): void {
        const input = new ChangeUserLanguageDto();
        input.languageName = languageName;

        this._profileServiceProxy.changeLanguage(input).subscribe(() => {
            abp.utils.setCookieValue(
                'Abp.Localization.CultureName',
                languageName,
                new Date(new Date().getTime() + 5 * 365 * 86400000), //5 year
                abp.appPath
            );

            window.location.reload();
        });
    }

    setCurrentLoginInformations(): void {
        this.shownLoginName = this._usuarioService.obterNomeUsuario();
        this.tenancyName = this.appSession.tenancyName;
        this.userName = this._usuarioService.obterUserName();
        AppConsts.usuarioLogado = this.userName;
        abp.event.trigger('app.s2-admin.contratoCadastro.init');
    }

    getProfilePicture(): void {
        this._profileServiceProxy.getProfilePicture().subscribe(result => {
            if (result && result.profilePicture) {
                this.profilePicture = 'data:image/jpeg;base64,' + result.profilePicture;
            }
        });
    }

    getRecentlyLinkedUsers(): void {
        this._userLinkServiceProxy.getRecentlyUsedLinkedUsers().subscribe(result => {
            this.recentlyLinkedUsers = result.items;
        });
    }


    showLoginAttempts(): void {
        abp.event.trigger('app.show.loginAttemptsModal');
    }

    showLinkedAccounts(): void {
        abp.event.trigger('app.show.linkedAccountsModal');
    }

    changePassword(): void {
        abp.event.trigger('app.show.changePasswordModal');
    }

    changeProfilePicture(): void {
        abp.event.trigger('app.show.changeProfilePictureModal');
    }

    changeMySettings(): void {
        abp.event.trigger('app.show.mySettingsModal');
    }

    logout(): void {
        this._authService.logout();
    }

    AlterarSenha(): void {
        if (this.appSession.user && !this.appSession.user.siga2UserName) {
            abp.event.trigger('app.show.changePasswordModal');
        } else if (this.appSession.user && this.appSession.user.siga2UserName) {
            let filtro = new ObterItemMenuInput();
            filtro.nome = 'Trocar Senha';
            this._menuService.obterItemMenu(filtro).subscribe(item => {
                this.navegacaoService.navigateTo(item);
            })
        }
    }

    onMySettingsModalSaved(): void {
        this.shownLoginName = this.appSession.getShownLoginName();
    }

    backToMyAccount(): void {
        this._impersonationService.backToImpersonator();
    }

    switchToLinkedUser(linkedUser: LinkedUserDto): void {
        this._linkedAccountService.switchToAccount(linkedUser.id, linkedUser.tenantId);
    }

    get chatEnabled(): boolean {
        return (!this._abpSessionService.tenantId || this.feature.isEnabled('App.ChatFeature'));
    }

    downloadCollectedData(): void {
        this._profileServiceProxy.prepareCollectedData().subscribe(() => {
            this.message.success(this.l('GdprDataPrepareStartedNotification'));
        });
    }
}
