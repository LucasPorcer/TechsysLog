import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EnderecoModel } from 'src/app/models/entrega/enderecoModel';

@Component({
  selector: 'app-endereco-modal',
  templateUrl: './endereco-modal.component.html',
  styleUrls: ['./endereco-modal.component.css']
})
export class EnderecoModalComponent {
  constructor(
    public dialogRef: MatDialogRef<EnderecoModalComponent>,
    @Inject(MAT_DIALOG_DATA) public endereco: EnderecoModel
  ) {}

  fechar(): void {
    this.dialogRef.close();
  }
}
