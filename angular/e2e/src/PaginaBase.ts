import { ElementFinder, ExpectedConditions, $, browser, element, by } from 'protractor';

export abstract class PaginaBase {
	campoUsuario: ElementFinder;
	campoSenha: ElementFinder;
	botaoLogin: ElementFinder;

	nomeUsuarioLogado: ElementFinder;

	erroCampoUsuario: ElementFinder;
	erroCampoSenha: ElementFinder;
	modalErro: ElementFinder;
	listaPapeis: ElementFinder;
	ec: any;
	cliquepapeis: ElementFinder;

	constructor() {
		this.campoUsuario = $('#user');
		this.campoSenha = $('#password');

		this.botaoLogin = $('.btn-success');

		this.nomeUsuarioLogado = $('.m-topbar__username');
		this.erroCampoUsuario = $('#user-error');
		this.erroCampoSenha = $('#password-error');

		this.modalErro = $('.sweet-alert');
		this.listaPapeis = element(by.xpath('/html/body/app-root/ng-component/div/default-layout/div/header/div/div/div/div/topbar/div/nav/ul/li[2]/div/div/div[2]/div/div/ul'));
		this.cliquepapeis = element(by.xpath('/html/body/app-root/ng-component/div/default-layout/div/header/div/div/div/div/topbar/div/nav/ul/li[2]/a'));
		this.ec = ExpectedConditions;
	}

	async logarUsuarioComSucesso() {
		this.carregarPagina();

		await browser.wait(this.ec.visibilityOf(this.campoUsuario));

		this.preencherCampo(this.campoUsuario, 'jeanluc');
		this.preencherCampo(this.campoSenha, 'a');

		this.acionarBotao(this.botaoLogin);		
		await browser.wait(this.ec.visibilityOf(this.nomeUsuarioLogado));		
		browser.click(this.cliquepapeis);
		await browser.wait(this.ec.visibilityOf(this.listaPapeis));
	}

	carregarPagina(url: string = '/') {
		browser.get(url);
	}

	preencherCampo(campo: ElementFinder, valor: string) {
		campo.sendKeys(valor);
	}

	acionarBotao(botao: ElementFinder) {
		botao.click();
	}
}
