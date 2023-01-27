import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cep'
})
export class CepPipe implements PipeTransform {

  transform(cep : any): String {
    let cepFormatado = cep.toString();
    return cepFormatado.replace(/(\d{5})(\d{3})/, "$1-$2");
  }

}
