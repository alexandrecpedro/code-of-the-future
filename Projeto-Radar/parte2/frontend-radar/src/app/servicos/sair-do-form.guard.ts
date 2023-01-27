import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Produto } from '../models/produto';
import { FormClienteComponent } from '../pages/form-cliente/form-cliente.component';
import { FormComprarProdutoComponent } from '../pages/form-comprar-produto/form-comprar-produto.component';
import { FormProdutosComponent } from '../pages/form-produtos/form-produtos.component';

@Injectable({
  providedIn: 'root'
})
export class SairDoFormGuard implements CanDeactivate<any> {
  private produto : Produto = {} as Produto;
  canDeactivate(
    component: any,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      console.log(component);

      if(component && component.produto?.nome || component.produto?.descricao || component.produto?.valor || component.produto?.qtd_estoque || component.produto?.custo)
        return confirm("Você possui dados preenchidos, deseja realmente sair?")
      if(component && component.cliente?.nome || component.cliente?.telefone || component.cliente?.email || component.cliente?.cpf || component.cliente?.cep || component.cliente?.logradouro || component.cliente?.numero || component.cliente?.bairro || component.cliente?.cidade || component.cliente?.estado || component.cliente?.complemento)
        return confirm("Você possui dados preenchidos, deseja realmente sair?")
      if(component && component.pedidos?.qtd_estoque)
        return confirm("Você possui dados preenchidos, deseja realmente sair?")
      /*indiceProduto.forEach(index =>{
        if(component && index)
          return confirm("Você possui dados preenchidos, deseja realmente sair?")
        else
          return console.log("Chegou no else")
      })*/
    
      return true;
  }
  
}
