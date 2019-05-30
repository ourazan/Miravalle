Create table Usuario(
IdUsuario  int primary key identity (1,1)
,Nombre varchar(200)
,Apellido varchar(200)
,Correo varchar(200)
,Clave varchar (1000)
,NombreUsuario varchar(200)
,IdSede int
,Perfil int
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
,@Perfil int
,@UsuarioAutenticado int
as begin
 insert into Usuario
           (Nombre
           ,Apellido
           ,Correo
           ,Clave
           ,NombreUsuario
           ,IdSede
		   ,Perfil
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
	 ,@Perfil
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
,@IdSede int
,@Perfil int
,@UsuarioAutenticado int
as 
begin
    update Usuario set 
            Nombre=@Nombre
           ,Apellido=@Apellido
           ,Correo=isnull(@Correo,Correo)
           ,Clave=isnull(@Clave,Clave)
           ,IdSede=@IdSede
		   ,Perfil=@Perfil
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
,isnull(Usuario.IdSede,0)  as IdSede
,Usuario.Activo 
,Usuario.Perfil
,isnull(Sede.NombreSede,'') as NombreSede
,isnull(Sede.Ciudad,'')  as Ciudad
,isnull(Sede.Direccion,'')  as Direccion
,isnull(Sede.IdAdministrador,0) as IdAdministrador
,isnull(Administrador.Nombre ,'')  as Administrador_Nombre
,isnull(Administrador.Apellido,'') as Administrador_Apellido
,isnull(Administrador.Correo,'')   as Administrador_Correo
,isnull(Administrador.Clave ,'')   as Administrador_Clave
,isnull(Administrador.NombreUsuario ,'')as Administrador_Usuario
,isnull(Administrador.Perfil,0) as Administrador_Perfil
from
Usuario 
left join Sede on Sede.IdSede=Usuario.IdSede
left join Usuario Administrador on Administrador.IdUsuario = Sede.IdAdministrador

GO
create procedure ConsultarUsuario
 @IdUsuario int = null
,@Nombre varchar(200)=null
,@Apellido varchar(200)=null
,@Correo varchar(200)=null
,@IdSede int=null
,@Perfil int=null
as 
begin
 Select 
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
,v_Usuario.Perfil
,v_Usuario.IdAdministrador
,v_Usuario.Administrador_Nombre
,v_Usuario.Administrador_Apellido
,v_Usuario.Administrador_Correo
,v_Usuario.Administrador_Clave
,v_Usuario.Administrador_Usuario
,v_Usuario.Administrador_Perfil
from v_Usuario
 where Activo=1 
 and (Nombre=@Nombre or(@Nombre is null))
 and (Apellido=@Apellido or(@Apellido is null))
 and (Correo=@Correo or(@Correo is null))
 and (IdSede=@IdSede or(@IdSede is null))  
 and (Perfil=@Perfil or(@Perfil is null))
 and (IdUsuario=@IdUsuario or (@IdUsuario is null))
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
,isnull(Sede.IdAdministrador,0 ) as IdAdministrador
,Sede.Activo
,isnull(Usuario.Nombre,'') as Nombre 
,isnull(Usuario.Apellido,'')  as Apellido
,isnull(Usuario.Correo ,'')   as Correo
,isnull(Usuario.Clave ,'')  as  Clave
,isnull(Usuario.NombreUsuario,'') as NombreUsuario
,isnull(Usuario.Perfil,0) as Perfil
from
Sede 
left join Usuario  on Usuario.IdUsuario = Sede.IdAdministrador

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
 @IdSede int= null
,@NombreSede varchar(100)= null
,@Ciudad varchar(100)= null
,@Direccion varchar (100)= null
,@IdAdministrador int= null

as 
begin
 select 
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
,v_Sede.Perfil 
from v_Sede
 where Activo=1 
 and (IdSede=@IdSede or(@IdSede is null))
 and (NombreSede=@NombreSede or(@NombreSede is null))
 and (Ciudad=@Ciudad or(@Ciudad is null))
 and (Direccion=@Direccion or(@Direccion is null))
 and (IdAdministrador=@IdAdministrador or(@IdAdministrador is null))


end

go

create procedure [dbo].[ValidarUsuario]
 @NombreUsuario varchar(1000)=null,
 @Clave varchar(200)=null
as 
begin
 Select 
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
,v_Usuario.Perfil
,v_Usuario.IdAdministrador
,v_Usuario.Administrador_Nombre
,v_Usuario.Administrador_Apellido
,v_Usuario.Administrador_Correo
,v_Usuario.Administrador_Clave
,v_Usuario.Administrador_Usuario
,v_Usuario.Administrador_Perfil
from v_Usuario
 where 
		NombreUsuario = @NombreUsuario
 and	Clave=@Clave
end

