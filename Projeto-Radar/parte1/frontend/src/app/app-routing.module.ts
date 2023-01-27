import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UpdateFormComponent } from './components/update-form/update-form.component';
import { AuthGuard } from './guards/auth/auth.guard';
import { EditPermissionGuard } from './guards/edit-permission/edit-permission.guard';
import { FormLeaveGuard } from './guards/form-leave/form-leave.guard';
import { CashFlowComponent } from './pages/cash-flow/cash-flow.component';
import { ClientsComponent } from './pages/clients/clients.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { ProductsComponent } from './pages/products/products.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'clients', component: ClientsComponent, canActivate: [AuthGuard] },
  {
    path: 'clients/:id',
    canActivateChild: [EditPermissionGuard],
    children: [
      { path: '', redirectTo: 'update', pathMatch: 'full' },
      { path: 'update', component: UpdateFormComponent, canDeactivate: [FormLeaveGuard] }
    ]
  },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard] },
  {
    path: 'orders/:id',
    canActivateChild: [EditPermissionGuard],
    children: [
      { path: '', redirectTo: 'order', pathMatch: 'full' },
      { path: 'order', component: OrdersComponent }
    ]
  },
  { path: 'products', component: ProductsComponent, canActivate: [AuthGuard] },
  {
    path: 'products/:id',
    canActivateChild: [EditPermissionGuard],
    children: [
      { path: '', redirectTo: 'update', pathMatch: 'full' },
      { path: 'update', component: ProductsComponent, canDeactivate: [FormLeaveGuard] }
    ]
  },
  { path: 'cash-flow', component: CashFlowComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
