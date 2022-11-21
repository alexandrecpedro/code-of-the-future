import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneFormat'
})
export class PhoneFormatPipe implements PipeTransform {

  transform(phone: Number): String {
    let formattedPhone = phone.toString();
    return formattedPhone.replace(/(\d{2})(\d{5})(\d{4})/, "($1) $2-$3");
  }

}
