Create Table Configuraciones
(IdConfiguracion int primary key identity (1,1)
,Configuracion varchar(200)
,Valor varchar(200)
,FechaCreacion smalldatetime
,UsuarioCreo int null
,FechaModificacion smalldatetime null
,UsuarioModifico int null
,Activo bit  null
)
go 

create procedure CrearConfiguracion
@Configuracion varchar(200)
,@Valor varchar(200)
,@UsuarioAutenticado int
as begin

insert into Configuraciones
( 
 Configuracion 
,Valor 
,FechaCreacion 
,UsuarioCreo 
,FechaModificacion 
,UsuarioModifico 
,Activo 
)
values(
@Configuracion 
,@Valor 
,GETDATE()
,@UsuarioAutenticado
,GETDATE()
,@UsuarioAutenticado
,1)

select SCOPE_IDENTITY() as ID
end 
go
create procedure EditarConfiguracion
@IdConfiguracion int 
,@Configuracion varchar(200)
,@Valor varchar(200)
,@UsuarioAutenticado int
as begin

update Configuraciones
set
 Configuracion =@Configuracion 
,Valor =@Valor 
,FechaModificacion =@UsuarioAutenticado
,UsuarioModifico  =1
where IdConfiguracion=@IdConfiguracion

select @IdConfiguracion as ID
end 
go

create procedure ConsultarConfiguraciones
@filtro varchar(1000)='1=1'
as begin
exec('
select 
 IdConfiguracion
,Configuracion 
,Valor
 from Configuraciones where Activo=1  and ' + @filtro)
end