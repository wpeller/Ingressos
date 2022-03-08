import { AdicionarFavoritoInput, NavigationDto, MenuServicoServiceProxy, ObterItemDeMenuPorIdInput } from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';
import {
  Component,
  Input,
  OnInit
  } from '@angular/core';
import { NavegacaoService } from '@app/shared/services/navegacao.service';
import { PapelService } from '@app/shared/services/papel.service';
import { UsuarioService } from '@app/shared/services/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bread-crumb-default',
  templateUrl: './bread-crumb-default.component.html',
  styleUrls: ['./bread-crumb-default.component.css']
})
export class BreadCrumbDefaultComponent implements OnInit {

  @Input() breadCrumb: string;
  ehHome: boolean;
  ambiente: string;
  favorito: boolean;
  @Input() titulo: string;

  navigation: NavigationDto;
  fatiamento: boolean = false;
  homologacao: boolean = false;
  sustentacao: boolean = false;
  preProd: boolean = false;
  desenvolvimento: boolean = false;

  constructor(private navegacaoService: NavegacaoService,
    private menuServico: MenuServicoServiceProxy,
    private papelService: PapelService,
    private router: Router,
    private usuarioService: UsuarioService) { }

    ngOnInit() {
      this.ehHome = false;
      this.favorito = false;
      this.ambiente = AppConsts.ambiente;
      this.ambiente = this.ambiente.toLowerCase();
      this.ambiente = this.ambiente.trim();
  
      this.navigation = this.navegacaoService.navegacaoAtual;
      if (this.navigation && this.navigation.name) {
        this.getBreadBrumb();
        if (this.navigation.rota.indexOf('dashboard') > 0) {
          this.ehHome = true;
        } else {
          this.ehHome = false;
        }
      } else {
        abp.event.on('app.auth.canActivate', navigation => { //see auth-route.guard.ts
          if (navigation && navigation.name) {
            this.navigation = navigation;
            this.favorito = navigation.favorito;
            this.getBreadBrumb();
          }
        });
      }

      this.ajustarBreadCrumb()
    }

    ajustarBreadCrumb(): void {

      this.fatiamento = false;
      this.homologacao = false;
      this.sustentacao = false;
      this.preProd = false;
      this.desenvolvimento = false;
      switch (this.ambiente.toLowerCase()) {
        case 'ambiente: producao':
          this.ambiente = '';
          break;
        case 'ambiente: fatiamento':
          this.sustentacao = true;
          this.ambiente = 'Ambiente: Fatiamento';
          break;
  
        case 'ambiente: sustentacao':
          this.sustentacao = true;
          this.ambiente = 'Ambiente: Sustentação';
          break;
  
        case 'ambiente: homologacao':
          this.homologacao = true;
          this.ambiente = 'Ambiente: Homologação';
          break;
  
        case 'ambiente: preproducao':
          this.preProd = true;
          this.ambiente = 'Ambiente: Pré Produção';
          break;
  
        case 'ambiente: treinamento':
          this.homologacao = true;
          this.ambiente = 'Ambiente: Treinamento';
          break;
  
        case 'ambiente: desenvolvimento':
          this.desenvolvimento = true;
          this.ambiente = 'Ambiente: Desenvolvimento';
          break;
  
        default:
          this.ambiente = '';
          break;
      }
    }
  
    getBreadBrumb(): void {
      let input = new ObterItemDeMenuPorIdInput();
      input.idItemMenu = this.navigation.id;
      this.menuServico.obterItemDeMenuPorId(input).toPromise()
        .then(y => {
          if (y.breadCrumb) {
            this.breadCrumb = y.breadCrumb;
            this.titulo = y.titulo;
          }
        });
    }
  
    verificar(): void {
      if (this.favorito) {
        this.removerFavoritos();
      } else {
        this.adicionarFavoritos();
      }
    }
  
    adicionarFavoritos(): void {
      let input = new AdicionarFavoritoInput();
      let userName = this.usuarioService.obterUserName();
      let papel = this.papelService.obterPapelAtual();
      input.idPapel = papel.id;
      input.idRecurso = this.navigation.id;
      input.codigoExternoUsuario = userName;
      this.menuServico.adicionarFavorito(input)
      .subscribe(result => {
        abp.message.success('Adicionado aos favoritos!');
      });
    }
  
    removerFavoritos(): void {
      abp.message.confirm('Deseja excluir este item da sua lista de favoritos?', (p) => {
        if (p) {
          let userName = this.usuarioService.obterUserName();
          let papel = this.papelService.obterPapelAtual();
          this.menuServico.removerFavorito(
            userName,
            papel.id,
            this.navigation.id)
            .subscribe( () => {
              abp.message.success('Registro excluído com sucesso!');
            });
        }
    });
    }
  
    private getCurrentRouterUrl(): any {
      let currentRoute = this.router.url;
      return currentRoute;
    }
}