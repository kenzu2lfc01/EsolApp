import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { RegisterComponent } from './auth/register/register.component';
import { AfterRegisterComponent } from './auth/register/after-register/after-register.component';
import { DialogComponent } from './todo/image/dialog/dialog.component';
import { SharedTodoComponent } from './todo/shared-todo/shared-todo.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', loadChildren: () => import('./home/home.module').then(m => m.HomePageModule) },
  { path: 'todo', loadChildren: './todo/todo.module#TodoPageModule', canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent },
  { path: 'afterregister', component: AfterRegisterComponent },
  { path: 'sharedtodo', component: SharedTodoComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'tododetail/:id', component: DialogComponent },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
