import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AwardsComponent } from './awards/awards.component';
import { EducationComponent } from './education/education.component';
import { ExperienceComponent } from './experience/experience.component';
import { InterestsComponent } from './interests/interests.component';
import { SkillsComponent } from './skills/skills.component';

const routes: Routes = [
  { path: "", component: AboutComponent },
  { path: "awards", component: AwardsComponent },
  { path: "education", component: EducationComponent },
  { path: "experience", component: ExperienceComponent },
  { path: "interests", component: InterestsComponent },
  { path: "skills", component: SkillsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
