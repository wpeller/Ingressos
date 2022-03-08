import { Component, OnInit, Input, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { PapelDto, CategoriaDocumentoSigaDto, CategoriaDocumentoSigaFilhoDto, DashboardServicoServiceProxy, DocumentoSigaDto } from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { PapelService } from '@app/shared/services/papel.service';
import { finalize } from 'rxjs/operators';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'app-categoria-documentos-home',
  templateUrl: './categoria-documentos-home.component.html',
  styleUrls: ['./categoria-documentos-home.component.css']
})
export class CategoriaDocumentosHomeComponent extends AppComponentBase implements OnInit {

  public papelAtual: PapelDto;
  carregandoFavoritos: Boolean;
  @Input('cols') colunas: any;
  carregandoCategoriaDocumentos: Boolean;
  categorias: CategoriaDocumentoSigaDto[] = [];
  documentos: DocumentoSigaDto[] = [];
  categoriasTree: TreeNode[] = [];
  categoriaSelecionada: any = 0;

  constructor(
    injector: Injector,
    private _router: Router,
    private _servicoHome: DashboardServicoServiceProxy,
    private _sessionService: AppSessionService,
    private _papelService: PapelService
  ) {
    super(injector);
  }

  ngOnInit() {
    this._papelService.onTrocarDePapel.subscribe(papel => {
      this.papelAtual = papel;
      this.carregandoCategoriaDocumentos = true;
      this.categoriasTree = [];
      this.obterCategoriasDeDocumentos();
    });
  }

  private obterCategoriasDeDocumentos() {
    this._servicoHome.obterCategoriasDeDocumentos()
      .pipe(finalize(() => this.carregandoCategoriaDocumentos = false))
      .subscribe(p => {
        let cats = p.filter(p => p.nivel === 0);
        this.getTodasCategorias(cats);
      });
  }

  getTodasCategorias(categorias: CategoriaDocumentoSigaDto[]): void {
    this.categorias = categorias;
    this.categorias = this.categorias.sort((a, b): number => {
      if (a.nome > b.nome) {
        return 1;
      }
      if (a.nome < b.nome) {
        return -1;
      }
      return 0;
    });
    this.categorias.forEach(categoria => {
      let node: TreeNode = { label: categoria.nome, expandedIcon: 'fa fa-folder-open', collapsedIcon: 'fa fa-folder' };
      if (categoria.filhos && categoria.filhos.length > 0) {
        node.children = this.obterFilhos(categoria.filhos);
      }
      node.styleClass = 'rootTreeStyle';
      node.data = categoria.id;

      this.categoriasTree.push(node);
    });

  }

  private obterFilhos(catChildrens: CategoriaDocumentoSigaFilhoDto[]): any {
    let childrens: TreeNode[] = [];
    catChildrens.forEach(categoria => {
      let node: TreeNode = { label: categoria.nome, 
        expandedIcon: 'fa fa-folder-open', 
        collapsedIcon: 'fa fa-folder' };
      node.data = categoria.id;
      node.children = [];
      this._servicoHome.obterListaDeDocumentosPorCategoria(node.data)
        .subscribe(p => {
          let files = p.sort((s, a) => s.ordemExibicao);
          files.forEach(doc => {
            let child: TreeNode = { label: doc.nome, collapsedIcon: 'fa fa-file' };
            child.data = doc.nome;
            //child.key = doc.arquivo;
            node.children.push(child);
          });
        })
      childrens.push(node);
    });
    return childrens;
  }

  obterClass(): String {
    return 'col-md-' + this.colunas + ' col-12 float-left';
  }

  loadNode($event) {

  }

  nodeSelect($event) {
    let node: TreeNode = $event.node;
    if (node.children && node.children.length > 0) {
      node.expanded = !node.expanded;
    } else {
      node.styleClass = 'colorDefault';
      
      abp.ui.setBusy();

      let doc: DocumentoSigaDto = new DocumentoSigaDto();
      //doc.id = node.data;
      doc.nome = node.label;
      //doc.arquivo = node.key;

      this._servicoHome.downloadDocumento(doc)
        .pipe(finalize(() => abp.ui.clearBusy()))
        .subscribe(p => {
          let bytes = this.base64ToArrayBuffer(p);
          let urlCreator = window.URL;
          let nomeArquivo = doc.arquivo + '.pdf';
          let blob = new Blob([bytes], { type: 'application/pdf' });
          let url = urlCreator.createObjectURL(blob);
          let link = document.createElement('a');
          link.setAttribute('href', url);
          link.setAttribute('download', nomeArquivo);
    
          let evento = document.createEvent('MouseEvents');
          evento.initMouseEvent('click', true, true, window, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
          link.dispatchEvent(evento);         
        });
    }
  }

  base64ToArrayBuffer(base64): Uint8Array {
    let binaryString = window.atob(base64);
    let binaryLen = binaryString.length;
    let bytes = new Uint8Array(binaryLen);
    for (let i = 0; i < binaryLen; i++) {
        let ascii = binaryString.charCodeAt(i);
        bytes[i] = ascii;
    }
    return bytes;
  }

  collapseAll() {
    this.categoriasTree.forEach(node => {
      this.expandRecursive(node, false);
    });
  }

  private expandRecursive(node: TreeNode, isExpand: boolean) {
    node.expanded = isExpand;
    if (node.children) {
      node.children.forEach(childNode => {
        this.expandRecursive(childNode, isExpand);
      });
    }
  }

}
