export function estaPreenchido(valor: any) {
   return valor !== null && valor !== undefined && valor !== '';
}

export function listaPossuiConteudo(valor: any[]) {
   return valor !== null && valor !== undefined && valor.length > 0;
}
