import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EntregaModel } from 'src/app/models/entrega/entregaModel';

@Component({
  selector: 'app-entrega-modal',
  templateUrl: './entrega-modal.component.html',
  styleUrls: ['./entrega-modal.component.css']
})
export class EntregaModalComponent {
  constructor(
    public dialogRef: MatDialogRef<EntregaModalComponent>,
    @Inject(MAT_DIALOG_DATA) public entrega: EntregaModel
  ) {}

  fechar(): void {
    this.dialogRef.close();
  }
}
