import { Component,OnInit,AfterViewInit,ViewChild } from '@angular/core';

import { FormBuilder,FormGroup,Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

import { ModalDetalleVentaComponent } from '../../Modales/modal-detalle-venta/modal-detalle-venta.component';

import { Venta } from 'src/app/interfaces/venta';
import { VentaService } from 'src/app/Services/venta.service';
import { UtilidadService } from 'src/app/Reutilizable/utilidad.service';

export const MY_DATA_FORMATS = {
  parse:{
    dateInput: 'DD/MM/YYY'
  },
  display:{
    dateInput:'DD/MM/YYYY',
    monthYearLabel:'MMMM YYYY'
  }
}

@Component({
  selector: 'app-historial-venta',
  templateUrl: './historial-venta.component.html',
  styleUrls: ['./historial-venta.component.css'],
  providers:[
    {provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMATS}
  ]
})
export class HistorialVentaComponent implements OnInit, AfterViewInit {

  formularioBusqueda: FormGroup;
  opcionesBusqueda: any[] = [
    {value: "fecha", descripcion: "Por fechas"},
    {value: "numero", descripcion: "Numero venta"}
  ]
  columnasTabla: string[] = ['fechaRegistro','numeroDocumento','tipoPago','total','accion']
  dataInicio: Venta[] = []
  datosListaVenta = new MatTableDataSource(this.dataInicio)
  @ViewChild(MatPaginator)paginacionTabla! : MatPaginator;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private _ventaServicio: VentaService,
    private _utilidadServicio: UtilidadService
  ){

    this.formularioBusqueda = this.fb.group({
      buscarPor: ['fecha'],
      numero: [''],
      fechaInicio: [''],
      fechaFin: [''],
    })

    this.formularioBusqueda.get("buscarPor")?.valueChanges.subscribe(value =>{
      this.formularioBusqueda.patchValue({
        numero: "",
        fechaInicio: "",
        fechaFin: ""
      })
    })

  }

  ngOnInit(): void {
    
  }

  ngAfterViewInit(): void {
    this.datosListaVenta.paginator=this.paginacionTabla;
  }

  aplicarFiltroTabla(event:Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.datosListaVenta.filter=filterValue.trim().toLocaleLowerCase();
  }

  buscarVentas(){
    let _fechaInicio: string = "";
    let _fechaFin: string = "";

    if(this.formularioBusqueda.value.buscarPor === "fecha"){
      _fechaInicio = moment(this.formularioBusqueda.value.fechaInicio).format('DD/MM/YYYY')
      _fechaFin = moment(this.formularioBusqueda.value.fechaFin).format('DD/MM/YYYY')

      if(_fechaInicio==="Invalid date" || _fechaFin==="Invalid date"){
        this._utilidadServicio.mostrarAlerta("Debe ingresar ambas fechas","Advertencia")
        return;
      }

    }

    this._ventaServicio.historial(
      this.formularioBusqueda.value.buscarPor,
      this.formularioBusqueda.value.numero,
      _fechaInicio,
      _fechaFin
    ).subscribe({
      next: (data) =>{
        console.log(data.value)
        if(data.status){
          this.datosListaVenta = data.value;
        }else
          this._utilidadServicio.mostrarAlerta("No se encontraron datos","Advertencia")
      },
      error:(e) =>{}
    })
  }

  verDetalleVenta(_venta: Venta){
    this.dialog.open(ModalDetalleVentaComponent,{
      data: _venta,
      disableClose: true,
      width: '700px'
    })
  }


}
