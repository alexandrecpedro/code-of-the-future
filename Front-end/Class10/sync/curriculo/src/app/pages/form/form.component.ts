import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientService } from 'src/app/services/client/client.service';
import { ObserverClientServiceService } from 'src/app/services/observer-client-service/observer-client-service.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  /** ATTRIBUTES **/
  title: String ="New Client"
  client: Client = {} as Client;
  value: String = "";
  pluginValue: String = "";

  /** CONSTRUCTOR **/
  constructor(
    private router: Router,
    private routerParams: ActivatedRoute,
    private observerClientService: ObserverClientServiceService,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    let id: Number = this.routerParams.snapshot.params["id"];
    if (id) {
      this.title = "Updating Client"
      this.client = ClientService.searchClientById(id);
      this.value = this.client.value.toString();
    }
  }

  save(): void {
    if (this.client.id > 0) {
      this.client.value = this.convertNumber(this.value);
      ClientService.updateClient(this.client);
      return;
    }
    ClientService.addClient({
      id: 0,
      name: this.client.name,
      cpf: "34567898765",
      phone: 11999599999,
      address: this.client.address,
      date: new Date(),
      value: this.convertNumber(this.value)
    });

    this.observerClientService.updateQuantity();
    this.router.navigateByUrl("/contacts");
  }

  private convertNumber(value: String): Number {
    let matchValue = value.match(/\d|\.|,/g);
    if (matchValue == null) return 0;
    let brazilianValue = matchValue.join("");
    let americanValue = brazilianValue.replace(".", "").replace(",", ".");
    return Number(americanValue);
  }

  onlyNumber() {
    let matchValue = this.value.match(/\d|\.|,/g);
    if (matchValue == null) {
      this.value = "";
      return;
    }

    this.value = matchValue.join("");
  }

  mask(): void {
    let floatValue = Number(this.value);
    this.value = floatValue.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
  }

}
