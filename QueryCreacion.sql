create database TiendaGenerica

use TiendaGenerica

Create table Rol(
idRol int primary key identity(1,1),
nombre varchar(50),
fechaRegistro datetime default getdate()
)

Create table Menu(
idMenu int primary key identity(1,1),
nombre varchar(50),
icono varchar(50),
url varchar(50)
)

Create table MenuRol(
idMenuRol int primary key identity(1,1),
idMenu int references Menu(idMenu),
idRol int references Rol(idRol)
)

Create table Usuario(
idUsuario int primary key identity(1,1),
nombreCompleto varchar(100),
correo varchar(60),
idRol int references Rol(idRol),
clave varchar(200),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

Create table Categoria(
idCategoria int primary key identity(1,1),
nombre varchar(50),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

Create table Producto(
idProducto int primary key identity(1,1),
nombre varchar(50),
idCategoria int references Categoria(idCategoria),
stock int,
precio decimal(10,2),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

Create table NumeroDocumento(
idNumeroDocumento int primary key identity(1,1),
ultimo_Numero int Not null,
fechaRegistro datetime default getdate()
)


Create table Venta(
idVenta int primary key identity(1,1),
numeroDocumento varchar(40),
tipoPago varchar(50),
total decimal(10,2),
fechaRegistro datetime default getdate()
)

Create table DetalleVenta(
idDetalleVenta int primary key identity(1,1),
idVenta int references Venta(idVenta),
idProducto int references Producto(idProducto),
cantidad int,
precio decimal(10,2),
total decimal(10,2)
)


ALTER AUTHORIZATION ON DATABASE::TiendaGenerica TO sa;