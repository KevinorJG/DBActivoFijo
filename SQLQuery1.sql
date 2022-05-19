create database DepreciacionDB

use DepreciacionDB

create table ActivoFijo(
	id int primary key identity (1,1),
	nombre varchar (50) not null,
	descripcion varchar (200) null,
	valorActivo decimal (9,2) not null,
	valorResidual decimal (9,2) not null,
	vidaUtil int not null,
	codigo varchar(50) not null,
	estado varchar(50) not null,
	EsActivo bit
)
select * from ActivoFijo

create table Empleado(
	[id] int primary key identity(1,1),
	[nombre] varchar(50) not null,
	[apellido] varchar(50) not null,
	[direccion] varchar(200) not null,
	[telefono] varchar(16) not null,
	[email] varchar(100) not null,
	[dni] varchar(20) not null,
	[status] varchar(20) not null
)
go
create table ActivoEmpleado(
	[id] int primary key identity(1,1),
	activoID int not null,
	empleadoID int not null,
	[date] date not null,
	[IsActive] bit not null
)


--Relacion las tablas
go
alter table ActivoEmpleado
add constraint fk_Activo foreign key (activoID)
references ActivoFijo(id)
go
alter table ActivoEmpleado
add constraint fk_Empleado foreign key (empleadoID)
references Empleado(id)

select * from dbo.ActivoFijo
select * from dbo.Empleado
select * from dbo.ActivoEmpleado

delete from dbo.ActivoFijo
