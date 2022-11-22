import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatarTelefone'
})
export class FormatarTelefonePipe implements PipeTransform {

  transform(telefone: Number): String {
    let telefoneFormatado = telefone.toString();
    return telefoneFormatado.replace(/(\d{2})(\d{5})(\d{4})/, "($1) $2-$3");
  }

}
