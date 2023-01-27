import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ClientsComponent } from 'src/app/pages/clients/clients.component';
import { OrdersComponent } from 'src/app/pages/orders/orders.component';
import { ProductsComponent } from 'src/app/pages/products/products.component';

@Injectable({
  providedIn: 'root'
})
export class FormLeaveGuard implements CanDeactivate<ClientsComponent | OrdersComponent | ProductsComponent> {
  canDeactivate(
    component: ClientsComponent | OrdersComponent | ProductsComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (component)
        return confirm("Você deseja realmente sair?");

      return true;
  }
  
}
