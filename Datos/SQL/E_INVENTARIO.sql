create table Inventario
(IdInventario int primary key identity(1,1)
,FechaRegistro smalldatetime
,Cantidad int
,IdLote int
,IdSede int
,FechaCreacion smalldatetime
,UsuarioCreo int null
,FechaModificacion smalldatetime null
,UsuarioModifico int null
,Activo bit  null
,foreign key (IdLote)references Lote(IdLote)
,foreign key (IdSede)references Sede(IdSede)
)
GO
create view v_Inventario
as
select
 Inventario.IdInventario 
,Inventario.FechaRegistro 
,Inventario.Cantidad 
,Inventario.IdLote 
,Inventario.IdSede 
,Inventario.Activo
,v_lote.CodigoLote 
,v_lote.FechaRegistro as Lote_FechaRegistro
,v_lote.FechaVencimiento 
,v_lote.IdProducto 
,v_lote.NombreProducto
,v_lote.IdTipoProducto
,v_lote.CodigoReferencia 
,v_lote.Descripcion
,v_Sede.NombreSede
,v_Sede.Ciudad 
,v_Sede.Direccion 
,v_Sede.IdAdministrador
,v_Sede.Nombre 
,v_Sede.Apellido 
,v_Sede.Correo   
,v_Sede.Clave    
,v_Sede.NombreUsuario  
from Inventario
inner join dbo.v_Lote on v_Lote.IdLote=Inventario.IdLote
inner join dbo.v_sede on v_sede.IdSede=Inventario.IdSede

GO

create procedure EditarInventario
@IdInventario int 
,@FechaRegistro smalldatetime
,@Cantidad int
,@IdLote int
,@IdSede int
,@UsuarioAutenticado int
as begin
update Inventario
set
 FechaRegistro=@FechaRegistro
,Cantidad =@Cantidad 
,IdLote =@IdLote
,IdSede =@IdSede
,FechaModificacion=GETDATE()
,UsuarioModifico=@UsuarioAutenticado
where
IdInventario=@IdInventario
 
return @IdInventario 
end

go
create procedure CrearInventario
 @FechaRegistro smalldatetime
,@Cantidad int
,@IdLote int
,@IdSede int
,@UsuarioAutenticado int
as begin
INSERT INTO Inventario
(FechaRegistro
,Cantidad
,IdLote
,IdSede
,FechaCreacion
,UsuarioCreo
,FechaModificacion
,UsuarioModifico
,Activo)
values(
@FechaRegistro
,@Cantidad
,@IdLote
,@IdSede
,GETDATE()
,@UsuarioAutenticado
,GETDATE()
,@UsuarioAutenticado
,1)
return SCOPE_IDENTITY()
end
go
create procedure ELiminarInventario
@IdInventario int 
,@UsuarioAutenticado int
as begin
update Inventario
set
 Activo=0
,FechaModificacion=GETDATE()
,UsuarioModifico=@UsuarioAutenticado
where
IdInventario=@IdInventario

return @IdInventario
end
go
create procedure ConsultarInventario
  @filtro varchar(8000)=' 1=1'
as 
begin
 execute (' select 
 v_Inventario.IdInventario 
,v_Inventario.FechaRegistro 
,v_Inventario.Cantidad 
,v_Inventario.IdLote 
,v_Inventario.IdSede 
,v_Inventario.Activo
,v_Inventario.CodigoLote 
,v_Inventario.Lote_FechaRegistro 
,v_Inventario.FechaVencimiento 
,v_Inventario.IdProducto 
,v_Inventario.NombreProducto
,v_Inventario.IdTipoProducto
,v_Inventario.CodigoReferencia 
,v_Inventario.Descripcion
,v_Inventario.NombreSede
,v_Inventario.Ciudad 
,v_Inventario.Direccion 
,v_Inventario.IdAdministrador
,v_Inventario.Nombre 
,v_Inventario.Apellido 
,v_Inventario.Correo   
,v_Inventario.Clave    
,v_Inventario.NombreUsuario
from v_Inventario
where Activo=1 and ' + @filtro
)

  end

