import { PlanoComExcessoDeParcelamentoServiceProxy } from "./../../../../shared/service-proxies/service-proxies";
import {
    FiltroAlunoInput,
    FiltroPlanoComExcessoDeParcelamentoDto,
    FiltroTurmaInput,
    FiltroUnidadeInput,
} from "../../../../shared/service-proxies/service-proxies";
import {
    Component,
    Injector,
    Input,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    ValidatorFn,
    Validators,
} from "@angular/forms";
import { LazyLoadEvent, SelectItem } from "primeng/api";
import { ActivatedRoute } from "@angular/router";
import { PapelService } from "@app/shared/services/papel.service";
import {
    CursoServicoServiceProxy,
    AlunoServicoServiceProxy,
    TurmaServicoServiceProxy,
    UnidadeServicoServiceProxy,
    PapelDto,
    OutputPapelPorTipoDto,
} from "@shared/service-proxies/service-proxies";
import { FileDownloadService } from "@shared/utils/file-download.service";
import { BsLocaleService } from "ngx-bootstrap";
import {
    ConsultaAlunoComponent,
    FiltroAlunoDto,
    Grid,
    ModalConsultaTurmaComponent,
    ModalConsultaUnidadeComponent,
} from "ngx-siga2-modais";
import { Table } from "primeng/table";
import { FgvInputSelectComponent } from "@app/shared/component/fgv-input-select/fgv-input-select.component";
import { Paginator } from "primeng/primeng";
import { GenericItem } from "@app/shared/component/GenericItem";
import { finalize } from "rxjs/operators";
import { AppComponentConsultaBaseSiga2 } from "@shared/common/app-component-consulta-base-siga2";
import * as moment from "moment";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Component({
    selector: "app-planos-com-excesso-de-parcelamento",
    templateUrl: "./planos-com-excesso-de-parcelamento.component.html",
    styleUrls: ["./planos-com-excesso-de-parcelamento.component.css"],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class PlanosComExcessoDeParcelamentoComponent
    extends AppComponentConsultaBaseSiga2
    implements OnInit
{
    constructor(
        _injector: Injector,
        private _servicoCurso: CursoServicoServiceProxy,
        private _servicoAluno: AlunoServicoServiceProxy,
        private _servicoTurma: TurmaServicoServiceProxy,
        private _servicoUnidade: UnidadeServicoServiceProxy,
        private _papelServico: PapelService,
        private _planoComExcessoDeParcelamentoServico: PlanoComExcessoDeParcelamentoServiceProxy,
        private activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private localeService: BsLocaleService,
        protected formBuilder: FormBuilder
    ) {
        super(_injector);
    }

    initMessages() {}

    @ViewChild("modalAluno") modalAluno: ConsultaAlunoComponent;
    @ViewChild("modalTurma") modalTurma: ModalConsultaTurmaComponent;
    @ViewChild("modalUnidade") modalUnidade: ModalConsultaUnidadeComponent;
    @ViewChild("dataTable") dataTable: Table;
    @ViewChild("paginator") paginator: Paginator;
    @ViewChild("exportacao") exportacao: FgvInputSelectComponent;
    @ViewChild("programas") programas: FgvInputSelectComponent;

    dataFim: Date;
    dataInicio: Date;
    @Input() public style: string;

    ptBr = {
        firstDayOfWeek: 0,
        dayNames: [
            "Domingo",
            "Segunda",
            "Terça",
            "Quarta",
            "Quinta",
            "Sexta",
            "Sabado",
        ],
        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        dayNamesMin: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        monthNames: [
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro",
        ],
        monthNamesShort: [
            "Jan",
            "Fev",
            "Mar",
            "Abr",
            "Маi",
            "Jun",
            "Jul",
            "Ago",
            "Set",
            "Out",
            "Nov",
            "Dez",
        ],
        today: "Hoje",
        clear: "Clear",
    };

    ngOnInit() {
        super.ngOnInit();
        abp.ui.setBusy();
        this.load();
        this.geraFormulario();
        this.localeService.use("pt-br");
    }

    geraFormulario() {
        this.formConsulta = new FormGroup(
            {
                ddlPrograma: new FormControl(""),
                ddlExportacao: new FormControl(""),
                dataInicio: new FormControl(""),
                dataFim: new FormControl(""),
            },
            { validators: this.dateLessThan("dataInicio", "dataFim") }
        );
        moment.locale("pt-br");
    }

    dateLessThan(from: string, to: string) {
        return (group: FormGroup): { [key: string]: any } => {
            let data1 = group.controls[from].value;
            let data2 = group.controls[to].value;

            if (data1 && data2) {
                const utc1 = Date.UTC(
                    data1.getFullYear(),
                    data1.getMonth(),
                    data1.getDate()
                );
                const utc2 = Date.UTC(
                    data2.getFullYear(),
                    data2.getMonth(),
                    data2.getDate()
                );

                if (utc1 && utc2) {
                    const MS_PER_DAY = 1000 * 60 * 60 * 24;
                    const diffDays = Math.floor((utc2 - utc1) / MS_PER_DAY);

                    if (diffDays < 0) {
                        this.message.warn(
                            "Data de Início deve ser maior ou igual que a Data Fim"
                        );
                        return { valid: false };
                    } else if (diffDays > 1096) {
                        this.message.warn(
                            "O intervalo máximo permitido é de 3 anos"
                        );
                        return { valid: false };
                    }
                }
            }
            return {};
        };
    }

    gridColunasModalUnidade: Grid[] = [];
    gridColunasModalTurma: Grid[] = [];
    gridColunasModalAluno: Grid[] = [];
    opcoesDeProgramas: SelectItem[];
    opcoesDeExportacao: SelectItem[];
    papelAtual: PapelDto = new PapelDto();
    permissoesPapel: OutputPapelPorTipoDto = new OutputPapelPorTipoDto();
    popupVisible = false;
    showFilterRow: boolean;
    telaCarregada: boolean = false;

    filtroParcelamentoMaximo: FiltroPlanoComExcessoDeParcelamentoDto =
        new FiltroPlanoComExcessoDeParcelamentoDto();

    private load() {
        abp.ui.setBusy();

        this.papelAtual = this._papelServico.obterPapelAtual()
      //  console.log('Papel atual na pagina ', this.papelAtual);
        if (this.papelAtual && this.papelAtual.id) {
            this.ObterPermissoesPapel();
        } else {
            this.showFilterRow = true;
            this._papelServico.onTrocarDePapel.subscribe((p) => {
                this.papelAtual = p;
                this.ObterPermissoesPapel();
            });
        }
    }

    private ObterPermissoesPapel() {
        console.log(this.papelAtual);

        this._servicoUnidade
            .buscarPapelPorTipo(this.papelAtual.mnemonico)
            .pipe(finalize(() => abp.ui.clearBusy()))
            .subscribe((g) => {
                this.permissoesPapel = g;
                if (
                    g.codigoUnidadePapel !== undefined &&
                    g.codigoUnidadePapel !== ""
                ) {
                    if (
                        this.filtroParcelamentoMaximo.listaDeUnidades ===
                        undefined
                    ) {
                        this.filtroParcelamentoMaximo.listaDeUnidades = [];
                    }

                    this.filtroParcelamentoMaximo.listaDeUnidades.push(
                        g.codigoUnidadePapel
                    );

                    this.modalUnidade.unidadeSelecionada = {
                        codigo: g.codigoUnidadePapel,
                        nome: g.nomeUnidadePapel,
                    };

                    this.modalUnidade.ngOnInit();
                } else {
                    this.modalUnidade.desabilitarBoataoLimpar = false;
                    this.modalUnidade.desabilitarBotaoConsulta = false;
                }

                this.obterProgramas();
                this.getFormatosExportacao();
                this.telaCarregada = true;
            });
    }

    alunoSelecionadoHandler(input) {
        this.filtroParcelamentoMaximo.codigoAluno = input.codigo;
    }

    unidadeSelecionadaHandler(input) {
        this.filtroParcelamentoMaximo.listaDeUnidades = [];
        this.filtroParcelamentoMaximo.listaDeUnidades.push(input.codigo);
        this.modalUnidade.unidadeSelecionada = {
            codigo: input.codigo,
            nome: input.nomeAbreviado,
        };
    }

    turmaSelecionadaHandler(input) {
        this.filtroParcelamentoMaximo.codigoTurma = input.codigoTurma;
        this.modalTurma.codigoTurmaSelecionada = input.codigoTurma;
    }

    buscarAluno(filtro: FiltroAlunoDto) {
        let filtroAluno: FiltroAlunoInput = new FiltroAlunoInput();

        if (
            this.permissoesPapel.codigoUnidadePapel !== undefined &&
            this.permissoesPapel.codigoUnidadePapel !== ""
        ) {
            filtroAluno.codigoUnidadeEnsino =
                this.permissoesPapel.codigoUnidadePapel;
        } else if (
            this.modalUnidade.unidadeSelecionada.codigo !== undefined &&
            this.modalUnidade.unidadeSelecionada.codigo !== ""
        ) {
            filtroAluno.codigoUnidadeEnsino =
                this.modalUnidade.unidadeSelecionada.codigo;
        }

        filtroAluno.papelEhAcademicoFgvOnline =
            this.permissoesPapel.papelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline;
        filtroAluno.papelEhCaps =
            this.permissoesPapel.papelEhControladoriaOuCaps;

        filtroAluno.papelEhCoordenacaoFgvOnline =
            this.permissoesPapel.papelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline;

        filtroAluno.papelEhFgvOnline =
            filtroAluno.papelEhAcademicoFgvOnline ||
            filtroAluno.papelEhCoordenacaoFgvOnline;

        filtroAluno.papelEhSuperintendenciaNucleo =
            this.permissoesPapel.papelEhSuperintendenciaNucleo;

        filtroAluno.papelEhSuperintendenciaRede =
            this.permissoesPapel.papelEhSuperintendenciaRede;

        filtroAluno.codigoAluno = filtro.matricula;
        filtroAluno.codigoUnidadeEnsino =
            this.modalUnidade.unidadeSelecionada.codigo;
        filtroAluno.cpfPassaporte = filtro.cpfPassaporte;
        filtroAluno.nomeAluno = filtro.nome;
        filtroAluno.skip = filtro.skip;
        filtroAluno.order = filtro.order;
        filtroAluno.registrosPorPagina = filtro.regPerPage;

        if (!filtroAluno.order || filtro.order.length === 0) {
            filtroAluno.order = "NomeCompleto ASC";
        }

        abp.ui.setBusy();
        this._servicoAluno
            .obterAlunosPor(filtroAluno)
            .pipe(finalize(() => abp.ui.clearBusy()))
            .subscribe((p) => {
                this.DefiniGridAlunos();
                this.modalAluno.colunas = this.gridColunasModalAluno;
                this.modalAluno.data = {
                    alunos: p.itensAluno,
                    totalRegistros: p.totalDeRegistrosNaConsulta,
                };
            });
    }

    buscarTurma(filtro) {
        let filtroTurma = new FiltroTurmaInput();

        filtroTurma.listaDeUnidades = [];
        if (
            this.permissoesPapel.codigoUnidadePapel !== undefined &&
            this.permissoesPapel.codigoUnidadePapel !== ""
        ) {
            filtroTurma.listaDeUnidades.push(
                this.permissoesPapel.codigoUnidadePapel
            );
        } else if (
            this.modalUnidade.unidadeSelecionada.codigo !== undefined &&
            this.modalUnidade.unidadeSelecionada.codigo !== ""
        ) {
            filtroTurma.listaDeUnidades.push(
                this.modalUnidade.unidadeSelecionada.codigo
            );
        }

        filtroTurma.mnemonico = this.papelAtual.mnemonico;

        if (filtro.codigoTurma !== undefined && filtro.codigoTurma !== "") {
            if (filtro.codigoTurma.length < 5) {
                abp.message.info(
                    "Obrigatório informar 5 caracteres para a busca."
                );
                return;
            }
        }

        filtroTurma.ehGerencialOnline =
            this.permissoesPapel.papelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline;

        filtroTurma.ehGerencialRede =
            this.permissoesPapel.papelEhSuperintendenciaRede;

        filtroTurma.ehGerencialRedeMGM =
            this.permissoesPapel.papelEhSuperintendenteRedeMGM;

        filtroTurma.codigoTurma = filtro.codigoTurma;
        filtroTurma.order = filtro.order;
        filtroTurma.skip = filtro.skip;
        filtroTurma.registrosPorPagina = filtro.registroPorPagina;

        if (!filtroTurma.order || filtroTurma.order.length === 0) {
            filtroTurma.order = "CodigoTurma ASC";
        }
        abp.ui.setBusy();
        this._servicoTurma
            .obterTurmaPor(filtroTurma)
            .pipe(finalize(() => abp.ui.clearBusy()))
            .subscribe((p) => {
                this.DefinirGridTurmas();

                this.modalTurma.colunas = this.gridColunasModalTurma;
                this.modalTurma.gridData = {
                    data: p.itensTurma,
                    totalRegistros: p.totalDeRegistrosNaConsulta,
                };
            });
    }

    buscarUnidade(filtro) {
        let filtroUnidade = new FiltroUnidadeInput();

        this.validaPapelSelecionado();

        filtroUnidade.codigoUnidadePapel = filtro.codigoUnidade;

        filtroUnidade.nomeUnidade = filtro.nome;

        filtroUnidade.papelEhAcademicoOuFinanceiro =
            this.permissoesPapel.papelEhAcademicoOuFinanceiro;
        filtroUnidade.papelEhAuditoria = this.permissoesPapel.papelEhAuditoria;
        filtroUnidade.papelEhControladoriaOuCaps =
            this.permissoesPapel.papelEhControladoriaOuCaps;
        filtroUnidade.papelEhSuperintendenciaNucleo =
            this.permissoesPapel.papelEhSuperintendenciaNucleo;
        filtroUnidade.papelEhSuperintendenciaPEC =
            this.permissoesPapel.papelEhSuperintendenciaPEC;
        filtroUnidade.papelEhSuperintendenciaRede =
            this.permissoesPapel.papelEhSuperintendenciaRede;
        filtroUnidade.papelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline =
            this.permissoesPapel.papelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline;

        filtroUnidade.papelEhAuditoria = filtro.papelEhAuditoria;
        filtroUnidade.registrosPorPagina = filtro.regPerPage;
        filtroUnidade.skip = filtro.skip;
        filtroUnidade.order = filtro.order;
        abp.ui.setBusy();
        this._servicoUnidade
            .obterUnidadesPor(filtroUnidade)
            .pipe(finalize(() => abp.ui.clearBusy()))
            .subscribe((p) => {
                this.DefinirGridUnidades();
                this.modalUnidade.colunas = this.gridColunasModalUnidade;
                this.modalUnidade.gridData = {
                    data: p.listaDeUnidadesDeEnsino,
                    totalRegistros: p.totalDeRegistrosDaConsulta,
                };
            });
    }

    obterProgramas() {
        this.opcoesDeProgramas = [];
        Promise.all([this.programas]).then((obj) => {
            this._servicoCurso
                .obterProgramaCurso()
                .pipe(finalize(() => abp.ui.clearBusy()))
                .subscribe((p) => {
                    // console.log(p);
                    this.opcoesDeProgramas = p.map(function (item) {
                        return {
                            label: item.descricaoProgramaCurso,
                            value: item.valorProgramaCurso,
                        };
                    });
                });
        });
    }

    getFormatosExportacao(): void {
        this.opcoesDeExportacao = [];
        this.opcoesDeExportacao.push(new GenericItem("PDF", "1"));
        this.opcoesDeExportacao.push(new GenericItem("EXCEL", "2"));
    }

    getInput() {
        var codigoUnidadeSelecionada: string = "";

        this.filtroParcelamentoMaximo.mnemonico = this.papelAtual.mnemonico;

        if (
            this.filtroParcelamentoMaximo.listaDeUnidades === undefined ||
            this.filtroParcelamentoMaximo.listaDeUnidades.length <= 0
        ) {
            codigoUnidadeSelecionada =
                this.permissoesPapel.codigoUnidadePapel === undefined ||
                this.permissoesPapel.codigoUnidadePapel == ""
                    ? ""
                    : this.permissoesPapel.codigoUnidadePapel;

            if (
                codigoUnidadeSelecionada !== undefined &&
                codigoUnidadeSelecionada !== ""
            ) {
                this.filtroParcelamentoMaximo.listaDeUnidades = [];
                this.filtroParcelamentoMaximo.listaDeUnidades.push(
                    codigoUnidadeSelecionada
                );
            }
        }

        this.filtroParcelamentoMaximo.programa =
            this.formConsulta.get("ddlPrograma") === undefined ||
            this.formConsulta.get("ddlPrograma").value === ""
                ? ""
                : this.formConsulta.get("ddlPrograma").value;
    }

    DefiniGridAlunos() {
        this.gridColunasModalAluno = [];
        this.gridColunasModalAluno.push({
            field: "codigo",
            order: "codigo",
            titulo: "Código Aluno",
        });
        this.gridColunasModalAluno.push({
            field: "nome",
            order: "nome",
            titulo: "Nome",
        });
        this.gridColunasModalAluno.push({
            field: "cpf",
            order: "cpf",
            titulo: "Cpf/Passaporte",
        });
        this.gridColunasModalAluno.push({
            field: "codigoTurmaPreferencial",
            order: "codigoTurmaPreferencial",
            titulo: "Turma",
        });
    }

    DefinirGridTurmas() {
        this.gridColunasModalTurma = [];
        this.gridColunasModalTurma.push({
            field: "codigoTurma",
            order: "CodigoTurma",
            titulo: "Cód. Turma",
        });

        this.gridColunasModalTurma.push({
            field: "codigoCurriculo",
            order: "CodigoCurriculo",
            titulo: "Cód. Currículo",
        });

        this.gridColunasModalTurma.push({
            field: "dataInicio",
            order: "DataInicio",
            titulo: "Data Início",
        });

        this.gridColunasModalTurma.push({
            field: "dataFim",
            order: "DataFim",
            titulo: "Data Fim",
        });

        this.gridColunasModalTurma.push({
            field: "codigoUnidadeFisica",
            order: "CodigoUnidadeFisica",
            titulo: "Unidade Física",
        });
    }

    DefinirGridUnidades() {
        this.gridColunasModalUnidade = [];
        this.gridColunasModalUnidade.push({
            field: "codigo",
            order: "CodigoUnidade",
            titulo: "Código Unidade",
        });
        this.gridColunasModalUnidade.push({
            field: "nomeAbreviado",
            order: "NomeAbreviado",
            titulo: "Nome",
        });
    }

    onCellPrepared(e) {
        if (e.rowType === "data") {
            if (e.data.status === "INADIMPLENTE" && e.data.valorPendente > 0) {
                if (e.column.dataField === "valor") {
                    e.cellElement.style.fontWeight = "bold";
                    e.cellElement.style.color = "#8B0000";
                }
            }
        }

        if (e.rowType === "group") {
            let nodeColors = ["#BEDFE6", "#C9ECD7"];
            e.cellElement.style.backgroundColor = nodeColors[e.row.groupIndex];
            e.cellElement.style.color = "#000";
            if (e.cellElement.firstChild && e.cellElement.firstChild.style) {
                e.cellElement.firstChild.style.color = "#000";
            }
        }
        if (e.rowType === "groupFooter") {
            e.cellElement.style.fontStyle = "italic";
        }
    }

    onLimparAlunoSelecionado() {
        this.filtroParcelamentoMaximo.codigoAluno = "";
    }

    onLimparTurmaSelecionada() {
        this.filtroParcelamentoMaximo.codigoTurma = "";
    }

    onLimparUnidadeSelecionada() {
        this.filtroParcelamentoMaximo.listaDeUnidades = [];
    }

    getEntityChanges(event?: LazyLoadEvent) {
        this.buscar(event);
    }

    validaPapelSelecionado() {
        if (
            !this.permissoesPapel.papelEhAcademicoOuFinanceiro &&
            !this.permissoesPapel.papelEhAuditoria &&
            !this.permissoesPapel.papelEhControladoriaOuCaps &&
            !this.permissoesPapel
                .papelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline &&
            !this.permissoesPapel.papelEhSuperintendenciaNucleo &&
            !this.permissoesPapel.papelEhSuperintendenciaPEC &&
            !this.permissoesPapel.papelEhSuperintendenciaRede &&
            !this.permissoesPapel.papelEhSuperintendenteRedeMGM
        ) {
            // console.log("precisou recarregar");

            this._papelServico.onTrocarDePapel.subscribe((p) => {
                this.papelAtual = p;

                this._servicoUnidade
                    .buscarPapelPorTipo(this.papelAtual.mnemonico)
                    .subscribe((g) => {
                        this.permissoesPapel = g;
                        if (
                            g.codigoUnidadePapel !== undefined &&
                            g.codigoUnidadePapel !== ""
                        ) {
                            if (
                                this.filtroParcelamentoMaximo
                                    .listaDeUnidades === undefined
                            ) {
                                this.filtroParcelamentoMaximo.listaDeUnidades =
                                    [];
                            }

                            this.filtroParcelamentoMaximo.listaDeUnidades.push(
                                g.codigoUnidadePapel
                            );

                            this.modalUnidade.unidadeSelecionada = {
                                codigo: g.codigoUnidadePapel,
                                nome: g.nomeUnidadePapel,
                            };

                            this.modalUnidade.ngOnInit();
                        }
                        this.telaCarregada = true;
                    });
            });
        }
    }

    onChangeExportarHandler(event) {
        // console.log(event.target.value);

        if (event.target.value.toString() === "1") {
            console.log("entrou pdf");
            this.exportarParaPDF();
        } else if (event.target.value.toString() === "2") {
            console.log("entrou excel");
            this.exportarParaExcel();
        } else {
            console.log("entrou em lugar nenhum");
        }
    }

    exportarParaPDF() {
        abp.ui.setBusy();
        const self = this;
        this._planoComExcessoDeParcelamentoServico
            .exportToFilePDF(this.filtroParcelamentoMaximo)
            .subscribe((file) => {
                self._fileDownloadService.downloadTempFile(file);
                abp.ui.clearBusy();
            });
    }

    exportarParaExcel() {
        abp.ui.setBusy();
        const self = this;
        this._planoComExcessoDeParcelamentoServico
            .exportToFileExcel(99999, this.filtroParcelamentoMaximo)
            .subscribe((file) => {
                self._fileDownloadService.downloadTempFile(file);
                abp.ui.clearBusy();
            });
    }

    buscar(event?: LazyLoadEvent): void {
        this.existeBusca = true;
        this.getListaMaximoParcelamento(event);
    }
    getListaMaximoParcelamento(event: LazyLoadEvent) {
        this.getInput();

        this.filtroParcelamentoMaximo.totalDeRegistrosPorPagina =
            this.paginator !== undefined
                ? this.primengTableHelper.getMaxResultCount(
                      this.paginator,
                      event
                  )
                : 10;

        this.filtroParcelamentoMaximo.skip =
            this.paginator !== undefined
                ? this.primengTableHelper.getSkipCount(this.paginator, event)
                : 0;

        this.filtroParcelamentoMaximo.sorting =
            this.dataTable !== undefined
                ? this.primengTableHelper.getSorting(this.dataTable)
                : "Matricula ASC";

        if (
            this.filtroParcelamentoMaximo.totalDeRegistrosPorPagina ===
                undefined ||
            this.filtroParcelamentoMaximo.totalDeRegistrosPorPagina === 0
        ) {
            this.filtroParcelamentoMaximo.totalDeRegistrosPorPagina = 10;
        }

        if (
            this.filtroParcelamentoMaximo.sorting === undefined ||
            this.filtroParcelamentoMaximo.sorting === ""
        ) {
            this.filtroParcelamentoMaximo.sorting = "Matricula ASC";
        }

        this.filtroParcelamentoMaximo.dataInicioPeriodo =
            this.formConsulta.controls.dataInicio.value;

        this.filtroParcelamentoMaximo.dataFimPeriodo =
            this.formConsulta.controls.dataFim.value;

        abp.ui.setBusy();

        this._planoComExcessoDeParcelamentoServico
            .obterAlunosComExcessoDeParcelamento(this.filtroParcelamentoMaximo)
            .pipe(
                finalize(() => {
                    abp.ui.clearBusy();
                    this.primengTableHelper.hideLoadingIndicator();
                })
            )
            .subscribe(
                (result) => {
                    // console.log(result.itens);
                    this.primengTableHelper.records = result.itens;
                    this.primengTableHelper.totalRecordsCount =
                        result.totalDeRegistrosNaConsulta;
                    this.primengTableHelper.hideLoadingIndicator();

                    abp.ui.clearBusy();
                    this.existeBusca = false;

                    if (result.totalDeRegistrosNaConsulta === 0) {
                        this.message.warn("Nenhum registro encontrado.");
                    }
                },
                (error) => {
                    abp.ui.clearBusy();
                    this.primengTableHelper.hideLoadingIndicator();
                }
            );
    }

    limpar(): void {
        super.limpar();
        this.onLimparUnidadeSelecionada();
    }
}
