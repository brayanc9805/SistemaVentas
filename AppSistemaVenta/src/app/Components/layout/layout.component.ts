import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { Menu } from 'src/app/interfaces/menu';

import { MenuService } from 'src/app/Services/menu.service';
import { UtilidadService } from 'src/app/Reutilizable/utilidad.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  listaMenus: Menu[] = [];
  correoUsuario: String = "";
  rolUsuario: string = "";

  constructor(
    private router: Router,
    private _menuServicio: MenuService,
    private _utilidadServicio: UtilidadService
  ){

  }

  ngOnInit(): void {
    const usuario = this._utilidadServicio.obtenerSesionUsuario();
    if(usuario!= null){
      this.correoUsuario = usuario.correo;
      this.rolUsuario = usuario.rolDescripcion;

      this._menuServicio.lista(usuario.idUsuario).subscribe({
        next: (data)=>{
          if(data.status){
            this.listaMenus = data.value
            console.log(this.listaMenus)
          }
        },
        error: (e) =>{}
      })
    }else{
      this.router.navigate(['login'])
    }
  }
  cerrarSesion(){
    this._utilidadServicio.eliminarSesionUsuario();
    this.router.navigate(['login'])
  }
}
