import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { ContactListComponent } from './pages/contact-list/contact-list.component';
import { FormComponent } from './pages/form/form.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { PortfolioComponent } from './pages/portfolio/portfolio.component';
import { AllowEditGuard } from './services/allow-edit/allow-edit.guard';
import { AuthGuard } from './services/auth/auth.guard';
import { FormLeaveGuard } from './services/form-leave/form-leave.guard';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "portfolio", component: PortfolioComponent, canActivate: [AuthGuard] },
  { path: "about", component: AboutComponent },
  { path: "form", component: FormComponent, canActivate: [AuthGuard], canDeactivate: [FormLeaveGuard] },
  { 
    path: "form/:id",
    canActivateChild: [AllowEditGuard],
    children: [
      { path: "", redirectTo: "update", pathMatch: "full" },
      { path: "update", component: FormComponent, canDeactivate: [FormLeaveGuard] }
    ]
  },
  // { path: "update-form/:id", component: FormComponent, canActivate: [AuthGuard] },
  { path: "contacts", component: ContactListComponent, canActivate: [AuthGuard] },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
