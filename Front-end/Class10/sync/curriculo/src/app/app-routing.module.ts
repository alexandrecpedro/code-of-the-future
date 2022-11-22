import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { ContactListComponent } from './pages/contact-list/contact-list.component';
import { FormComponent } from './pages/form/form.component';
import { HomeComponent } from './pages/home/home.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { PortfolioComponent } from './pages/portfolio/portfolio.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "/portfolio", component: PortfolioComponent },
  { path: "/about", component: AboutComponent },
  { path: "/form", component: FormComponent },
  { path: "/update-form/:id", component: FormComponent },
  { path: "/contacts", component: ContactListComponent },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
