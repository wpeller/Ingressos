import { browser, protractor, element, by, $, ExpectedConditions } from 'protractor';
import { LoginPagePo } from '../../pages/login-page-po';
import { writeScreenShot } from '../../../utils/helper';
var fs = require('fs-extra');

describe('Funcionalidade - Login', () => {
	let pagina: LoginPagePo;
	let ec: any;

	beforeEach(() => {
		pagina = new LoginPagePo();
		ec = ExpectedConditions;

		browser.driver.manage().deleteAllCookies();

		// Disable waitForAngularEnabled, otherwise test browser will be closed immediately
		browser.waitForAngularEnabled(false);
	});

	afterEach(() => {
		browser.restart();				
	});

	it('Permite efetuar login com sucesso', async () => {
		browser.driver.manage().window().maximize();

		pagina.carregarPagina();

		await browser.wait(ec.visibilityOf(pagina.campoUsuario));

		pagina.preencherCampo(pagina.campoUsuario, 'jeanluc');
		pagina.preencherCampo(pagina.campoSenha, 'a');
		GerarEvidenciaImagem('[Permite efetuar login com sucesso]_IncluindoCamposDeLogin');
		pagina.acionarBotao(pagina.botaoLogin);

		await browser.wait(ec.visibilityOf(pagina.nomeUsuarioLogado));
		await pagina.nomeUsuarioLogado.getText().then((resultado) => {
			expect(resultado.toLowerCase()).toEqual('jeanluc');
		});		
		GerarEvidenciaImagem('[Permite efetuar login com sucesso]_ValidacaoNomeUsuario02');

		pagina.cliquepapeis.click();				
		expect(element.all(by.xpath('/html/body/app-root/ng-component/div/default-layout/div/header/div/div/div/div/topbar/div/nav/ul/li[2]/div/div/div[2]/div/div/ul/li')).count()).toBeGreaterThanOrEqual(1);	
		GerarEvidenciaImagem('[Permite efetuar login com sucesso]_PapeisCarregados');

	});

	it('Deve gerar erro quando usuario não for preenchido', async () => {
		browser.driver.manage().window().maximize();

		pagina.carregarPagina();

		await browser.wait(ec.visibilityOf(pagina.campoUsuario));

		pagina.preencherCampo(pagina.campoSenha, 'a');

		pagina.acionarBotao(pagina.botaoLogin);

		await browser.wait(ec.visibilityOf(pagina.erroCampoUsuario)).then((resultado) => {
			expect(resultado).toBeTruthy();
		});
		GerarEvidenciaImagem('[Deve gerar erro quando usuario não for preenchido]_result');
	});

	it('Deve gerar erro quando senha não for preenchido', async () => {
		browser.driver.manage().window().maximize();

		pagina.carregarPagina();

		await browser.wait(ec.visibilityOf(pagina.campoUsuario));

		pagina.preencherCampo(pagina.campoUsuario, 'jeanluc');

		pagina.acionarBotao(pagina.botaoLogin);

		await browser.wait(ec.visibilityOf(pagina.erroCampoSenha)).then((resultado) => {
			expect(resultado).toBeTruthy();
		});
		GerarEvidenciaImagem('[Deve gerar erro quando senha não for preenchido]_result');
	});

	it('Deve gerar erro quando nenhum dos campos forem preenchidos', async () => {
		browser.driver.manage().window().maximize();

		pagina.carregarPagina();

		await browser.wait(ec.visibilityOf(pagina.campoUsuario));

		pagina.acionarBotao(pagina.botaoLogin);

		expect(pagina.erroCampoUsuario.isDisplayed()).toBeTruthy();
		expect(pagina.erroCampoSenha.isDisplayed()).toBeTruthy();
		GerarEvidenciaImagem('[Deve gerar erro quando nenhum dos campos forem preenchidos]_result');
	});

	it('Deve gerar erro quando dados do usuário forem inválidos', async () => {
		browser.driver.manage().window().maximize();

		pagina.carregarPagina();

		await browser.wait(ec.visibilityOf(pagina.campoUsuario));

		pagina.preencherCampo(pagina.campoUsuario, 'teste123');
		pagina.preencherCampo(pagina.campoSenha, 'teste123');

		pagina.acionarBotao(pagina.botaoLogin);

		await browser.wait(ec.visibilityOf(pagina.modalErro)).then((resultado) => {
			expect(resultado.isDisplayed()).toBeTruthy();
		});
		GerarEvidenciaImagem('[Deve gerar erro quando dados do usuário forem inválidos]_result');
	});
});
function GerarEvidenciaImagem(nome) {
	browser.getCapabilities().then(function (caps) {
		var browserName = caps.get('browserName');
		browser.takeScreenshot().then(function (png) {
			var stream = fs.createWriteStream('./e2e/reports/dashboardReport/imagens/'+ nome +'.png');
			stream.write(new Buffer(png, 'base64'));
			stream.end();
		});
	});
}

