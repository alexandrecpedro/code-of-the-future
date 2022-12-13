import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientService } from 'src/app/services/client/client.service';
import { ObserverClientService } from 'src/app/services/observer-client/observer-client.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  /** ATTRIBUTES **/
  private clientService: ClientService = {} as ClientService;
  title: String = "New Client";
  client: Client | undefined = {} as Client;
  value: any = "";
  pluginValue: String = "";

  /** CONSTRUCTOR **/
  constructor(
    private http: HttpClient,
    private observerClientService: ObserverClientService,
    private router: Router,
    private routerParams: ActivatedRoute,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.clientService = new ClientService(this.http);
    let id: Number = this.routerParams.snapshot.params["id"];
    if (id) {
      this.editClient(id);
    }
  }

  private async editClient(id: Number): Promise<void> {
    this.title = "Updating Client";
    this.client = await this.clientService.findById(id);
    this.value = this.client?.value.toString();
  }

  save(): void {
    if (this.value && this.client && this.client.id > 0) {
      this.client.value = this.convertNumber(this.value);
      this.clientService.update(this.client);
    } else {
      this.clientService.create({
        id: 0,
        name: this.client?.name,
        cpf: "34567898765",
        phone: 11999599999,
        address: this.client?.address,
        date: new Date(),
        value: this.convertNumber(this.value)
      });
    }

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
    this.value = floatValue.toLocaleString("pt-br", { style: "currency", currency: "BRL" });
  }
}
