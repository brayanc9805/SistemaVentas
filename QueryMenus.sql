use TiendaGenerica
insert into Menu(nombre,icono,url) values
('DashBoard','dashboard','/pages/dashboard'),
('Usuarios','group','/pages/usuarios'),
('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),
('Historial Ventas','edit_note','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')

select * from Menu

insert into Rol(nombre) values
('Administrador'),
('Empleado'),
('Supervisor')

select * from Rol

insert into MenuRol(idMenu,idRol) values
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)

insert into MenuRol(idMenu,idRol) values
(4,2),
(5,2)

insert into MenuRol(idMenu,idRol) values
(3,3),
(4,3),
(5,3),
(6,3)

select * from MenuRol

insert into NumeroDocumento(ultimo_Numero,fechaRegistro) values
(0,getdate())
