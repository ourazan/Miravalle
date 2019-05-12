Create table Usuario(
IdUsuario  int primary key identity (1,1)
,Nombre varchar(200)
,Apellido varchar(200)
,Correo varchar(200)
,Clave varchar (1000)
,NombreUsuario varchar(200)
,IdSede int
,FechaCreacion smalldatetime
,UsuarioCreo int null
,FechaModificacion smalldatetime null
,UsuarioModifico int null
,Activo bit  null
)

Create table Sede (
IdSede int primary key identity (1,1)
,NombreSede varchar(100)
,Ciudad varchar(100)
,Direccion varchar (100)
,IdAdministrador int
,FechaCreacion smalldatetime
,UsuarioCreo int null
,FechaModificacion smalldatetime null
,UsuarioModifico int null
,Activo bit  null
)

GO
alter table Usuario add foreign  key (IdSede)  references Sede(IdSede) 
GO
alter table Sede add  foreign key   (IdAdministrador) references Usuario(IdUsuario)


GO

create Procedure CrearUsuario
@Nombre varchar(200)
,@Apellido varchar(200)
,@Correo varchar(200)
,@Clave varchar (1000)
,@NombreUsuario varchar(200)
,@IdSede int
,@UsuarioAutenticado int
as begin
 insert into Usuario
           (Nombre
           ,Apellido
           ,Correo
           ,Clave
           ,NombreUsuario
           ,IdSede
           ,FechaCreacion
           ,UsuarioCreo
           ,FechaModificacion
           ,UsuarioModifico
           ,Activo)
     VALUES(
	  @Nombre
     ,@Apellido
     ,@Correo
     ,@Clave
     ,@NombreUsuario
     ,@IdSede
	 ,GETDATE()
	 ,@UsuarioAutenticado
	 ,GETDATE()
	 ,@UsuarioAutenticado
	 ,1 )

  return SCOPE_IDENTITY() 
end

go

create procedure EditarUsuario
@IdUsuario int 
,@Nombre varchar(200)
,@Apellido varchar(200)
,@Correo varchar(200)
,@Clave varchar (1000)
,@Usuario varchar(200)
,@IdSede int
,@UsuarioAutenticado int
as 
begin
    update Usuario set 
            Nombre=@Nombre
           ,Apellido=@Apellido
           ,Correo=isnull(@Correo,Correo)
           ,Clave=isnull(@Clave,Clave)
           ,IdSede=@IdSede
           ,FechaModificacion=GETDATE()
           ,UsuarioModifico=@UsuarioAutenticado
    where IdUsuario=@IdUsuario

	return  @IdUsuario 
end

go

create procedure EliminarUsuario
 @IdUsuario int 
,@UsuarioAutenticado int
as 
begin
    update Usuario set 
            Activo=0
           ,FechaModificacion=GETDATE()
           ,UsuarioModifico=@UsuarioAutenticado
    where IdUsuario=@IdUsuario

	return @IdUsuario
end

go


Create view v_Usuario
as
select
Usuario.IdUsuario  
,Usuario.Nombre
,Usuario.Apellido 
,Usuario.Correo
,Usuario.Clave 
,Usuario.NombreUsuario
,Usuario.IdSede 
,Usuario.Activo 
,Sede.NombreSede
,Sede.Ciudad 
,Sede.Direccion 
,Sede.IdAdministrador
,Administrador.Nombre   as Administrador_Nombre
,Administrador.Apellido as Administrador_Apellido
,Administrador.Correo   as Administrador_Correo
,Administrador.Clave    as Administrador_Clave
,Administrador.NombreUsuario as Administrador_Usuario
from
Usuario 
inner join Sede on Sede.IdSede=Usuario.IdSede
inner join Usuario Administrador on Administrador.IdUsuario = Sede.IdAdministrador

GO
create procedure ConsultarUsuario
  @filtro varchar(8000)=' 1=1'
as 
begin
  execute ('Select 
 v_Usuario. IdUsuario  
,v_Usuario.Nombre
,v_Usuario.Apellido 
,v_Usuario.Correo
,v_Usuario.Clave 
,v_Usuario.NombreUsuario
,v_Usuario.IdSede 
,v_Usuario.Activo 
,v_Usuario.NombreSede
,v_Usuario.Ciudad 
,v_Usuario.Direccion 
,v_Usuario.IdAdministrador
,v_Usuario.Administrador_Nombre
,v_Usuario.Administrador_Apellido
,v_Usuario.Administrador_Correo
,v_Usuario.Administrador_Clave
,v_Usuario.Administrador_Usuario
from v_Usuario
 where Activo=1 and ' + @filtro
  )



end

go


Create procedure CrearSede
 @NombreSede varchar(100)
,@Ciudad varchar(100)
,@Direccion varchar (100)
,@IdAdministrador int
,@UsuarioAutenticado int
as begin 
insert into Sede
           (NombreSede
           ,Ciudad
           ,Direccion
           ,IdAdministrador
           ,FechaCreacion
           ,UsuarioCreo
           ,FechaModificacion
           ,UsuarioModifico
           ,Activo)
     values
           ( @NombreSede 
			,@Ciudad 
			,@Direccion 
		   ,@IdAdministrador
		   ,GETDATE()
		   ,@UsuarioAutenticado
		   ,GETDATE()
		   ,@UsuarioAutenticado
		   ,1)

  return SCOPE_IDENTITY()

end

go

Create procedure EditarSede
@IdSede int
,@NombreSede varchar(100)
,@Ciudad varchar(100)
,@Direccion varchar (100)
,@IdAdministrador int
,@UsuarioAutenticado int
as begin 
update Sede set 
            NombreSede=@NombreSede 
           ,Ciudad=@Ciudad 
           ,Direccion=@Direccion 
           ,IdAdministrador=@IdAdministrador
           ,FechaModificacion=GETDATE()
           ,UsuarioModifico=@UsuarioAutenticado
           
     where IdSede=@IdSede
 return @IdSede 
end
 go 

 Create view v_Sede
as
select
Sede.IdSede
, Sede.NombreSede
,Sede.Ciudad 
,Sede.Direccion 
,Sede.IdAdministrador
,Sede.Activo
,Usuario.Nombre 
,Usuario.Apellido 
,Usuario.Correo   
,Usuario.Clave    
,Usuario.NombreUsuario
from
Sede 
inner join Usuario  on Usuario.IdUsuario = Sede.IdAdministrador

GO

create procedure EliminarSede
 @IdSede int 
,@UsuarioAutenticado int
as 
begin
    update Sede set 
            Activo=0
           ,FechaModificacion=GETDATE()
           ,UsuarioModifico=@UsuarioAutenticado
    where @IdSede=@IdSede
	return  @IdSede
end

go

create procedure ConsultarSede
  @filtro varchar(8000)=' 1=1'
as 
begin
  execute (' select 
v_Sede.IdSede
, v_Sede.NombreSede
,v_Sede.Ciudad 
,v_Sede.Direccion 
,v_Sede.IdAdministrador
,v_Sede.Activo
,v_Sede.Nombre 
,v_Sede.Apellido 
,v_Sede.Correo   
,v_Sede.Clave    
,v_Sede.NombreUsuario 
from v_Sede
 where Activo=1 and ' + @filtro
  )



end

go
