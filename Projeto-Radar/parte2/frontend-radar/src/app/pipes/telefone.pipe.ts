import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'telefone'
})
export class TelefonePipe implements PipeTransform {

  transform(telefone : any): String {
    let telefoneFormatado = telefone.toString();
    return telefoneFormatado.replace(/(\d{2})(\d{5})(\d{4})/, "($1) $2-$3");
  }

}
