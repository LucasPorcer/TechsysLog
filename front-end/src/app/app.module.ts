import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// Angular Material Modules
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card'; 
import { MatMenuModule } from '@angular/material/menu'; 
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PedidoFormComponent } from './components/pedido/pedido-form/pedido-form.component';
import { PedidoListComponent } from './components/pedido/pedido-list/pedido-list.component';
import { UsuarioFormComponent } from './components/usuario/usuario-form.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatPaginatorModule } from '@angular/material/paginator';
import { LoginComponent } from './components/login/login.component';
import { SignalRService } from './services/SignalR/signalr.service';
import { EnderecoModalComponent } from './components/endereco/modals/endereco-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import { EntregaModalComponent } from './components/entrega/modals/entrega-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    PedidoListComponent,
    PedidoFormComponent,
    UsuarioFormComponent,
    LoginComponent,
    EnderecoModalComponent,
    EntregaModalComponent
  ],
  providers: [SignalRService],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatCardModule,
    MatMenuModule,
    MatSnackBarModule,
    MatDialogModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
