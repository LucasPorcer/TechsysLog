import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PedidoFormComponent } from './components/pedido/pedido-form/pedido-form.component';
import { PedidoListComponent } from './components/pedido/pedido-list/pedido-list.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'pedidos', component: PedidoListComponent , canActivate: [AuthGuard]},
  { path: 'cadastrar-pedido', component: PedidoFormComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '/login' }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
