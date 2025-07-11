import { Component,OnInit,AfterViewInit,ViewChild } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { ModalProductoComponent } from '../../Modales/modal-producto/modal-producto.component';
import { Producto } from 'src/app/interfaces/producto';
import { ProductoService } from 'src/app/Services/producto.service';
import { UtilidadService } from 'src/app/Reutilizable/utilidad.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css']
})
export class ProductoComponent implements OnInit, AfterViewInit{

    columnasTabla:string[]=['nombre','categoria','stock','precio','estado','acciones'];
    dataInicio:Producto[]=[];
    datalistaProductos= new MatTableDataSource(this.dataInicio);
    @ViewChild(MatPaginator) paginacionTabla!:MatPaginator;

    constructor(
      private dialog:MatDialog,
      private _productoServicio:ProductoService,
      private _utilidadServicio:UtilidadService
    ){}

    ngAfterViewInit(): void {
      this.datalistaProductos.paginator=this.paginacionTabla;
    }

    ngOnInit(): void {
      this.obtenerProductos();
    }

    obtenerProductos(){
    this._productoServicio.lista().subscribe({
      next:(data)=>{
        if(data.status){
          this.datalistaProductos.data=data.value;
        }else{
          this._utilidadServicio.mostrarAlerta("No se encontraron datos","Advertencia");
        }
      },
      error:(e)=>{

      }
    })
  }

  aplicarFiltroTabla(event:Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.datalistaProductos.filter=filterValue.trim().toLocaleLowerCase();
  }

  nuevoProducto(){
      this.dialog.open(ModalProductoComponent,{
        disableClose:true
      }).afterClosed().subscribe(result=>{
        if(result==="true"){
          this.obtenerProductos();
        }
      });
  }

  editarProducto(producto: Producto){
    this.dialog.open(ModalProductoComponent,{
      disableClose:true,
      data:producto
    }).afterClosed().subscribe(result=>{
      if(result==="true"){
        this.obtenerProductos();
      }
    });
  }

  eliminarProducto(producto: Producto){
    Swal.fire({
      title:'¿Desea eliminar el producto',
      text:producto.nombre,
      icon:"warning",
      confirmButtonColor:'#3085d6',
      confirmButtonText:"Si, eliminar",
      showCancelButton:true,
      cancelButtonColor:'#d33',
      cancelButtonText:'No, volver'
    }).then((result)=>{
      if(result.isConfirmed){
        this._productoServicio.eliminar(producto.idProducto).subscribe({
          next:(data)=>{
            if(data.status){
              this._utilidadServicio.mostrarAlerta("El producto fue eliminado.","Exito");
              this.obtenerProductos();
            }else{
              this._utilidadServicio.mostrarAlerta("No se pudo eliminar el producto.","Error");
            }
          },
          error:(e)=>{}
        })
      }
    })
  }



}
