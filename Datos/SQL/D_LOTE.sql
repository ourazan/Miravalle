create table Lote (
IdLote int primary key identity(1,1)
,CodigoLote varchar(100)
,FechaRegistro smalldatetime
,FechaVencimiento smalldatetime
,IdProducto int
,FechaCreacion smalldatetime
,UsuarioCreo int null
,FechaModificacion smalldatetime null
,UsuarioModifico int null
,Activo bit  null
,foreign key (IdProducto) references Producto(IdProducto) 
)

GO 

create view v_lote
as 

select 
Lote.IdLote 
,Lote.CodigoLote 
,Lote.FechaRegistro 
,Lote.FechaVencimiento 
,Lote.IdProducto 
,Lote.Activo
,Producto.NombreProducto
,Producto.IdTipoProducto
,TipoProducto.CodigoReferencia 
,TipoProducto.Descripcion
from Lote 
inner join Producto on Producto.IdProducto= Lote.IdProducto
inner join TipoProducto on TipoProducto.IdTipoProducto=Producto.IdTipoProducto

Go 

create procedure EditarLote
@IdLote int
,@CodigoLote varchar(100)
,@FechaRegistro smalldatetime
,@FechaVencimiento smalldatetime
,@IdProducto int
,@UsuarioAutenticado int 
as
begin
update Lote set
CodigoLote =@CodigoLote 
,FechaRegistro=@FechaRegistro 
,FechaVencimiento=@FechaVencimiento 
,IdProducto =@IdProducto
,FechaModificacion=getdate() 
,UsuarioModifico=@UsuarioAutenticado 
where
IdLote=@IdLote

return @IdLote 
end
GO
create procedure CrearLote
@CodigoLote varchar(100)
,@FechaRegistro smalldatetime
,@FechaVencimiento smalldatetime
,@IdProducto int
,@UsuarioAutenticado int 
as
begin
INSERT INTO Lote
           (CodigoLote
           ,FechaRegistro
           ,FechaVencimiento
           ,IdProducto
           ,FechaCreacion
           ,UsuarioCreo
           ,FechaModificacion
           ,UsuarioModifico
           ,Activo)
     VALUES
           (@CodigoLote 
           ,@FechaRegistro 
           ,@FechaVencimiento
           ,@IdProducto
           ,getdate()
           ,@UsuarioAutenticado
           ,getdate()
           ,@UsuarioAutenticado
           ,1)
return SCOPE_IDENTITY() 
end

GO

Create procedure ConsultarLote
@filtro varchar(8000)= ''
as begin
exec (' Select
		v_lote.IdLote 
		,v_lote.CodigoLote 
		,v_lote.FechaRegistro 
		,v_lote.FechaVencimiento 
		,v_lote.IdProducto 
		,v_lote.Activo
		,v_lote.NombreProducto
		,v_lote.IdTipoProducto
		,v_lote.CodigoReferencia 
		,v_lote.Descripcion
	   from v_lote where Activo=1  and '+ @filtro)
end
go
create procedure EliminarLote
@IdLote int
,@UsuarioAutenticado int 
as
begin
update Lote set
Activo=0
,FechaModificacion=getdate() 
,UsuarioModifico=@UsuarioAutenticado 
where
IdLote=@IdLote

return @IdLote
end
GO
