import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneFormat'
})
export class PhoneFormatPipe implements PipeTransform {

  transform(phone: string): string {
    // let formattedPhone = phone.toString();
    return phone.replace(/(\d{2})(\d{5})(\d{4})/, "($1) $2-$3");
  }

}
