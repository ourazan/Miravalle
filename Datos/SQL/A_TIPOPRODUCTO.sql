Create table TipoProducto(
IdTipoProducto int primary key identity (1,1)
,CodigoReferencia varchar(100)
,Descripcion varchar(200)
,FechaCreacion smalldatetime  null
,UsuarioCreo int null
,FechaModificacion smalldatetime null
,UsuarioModifico int null
,Activo bit  null
)

GO
Create  procedure CrearTipoProducto
@CodigoReferencia varchar(100)
,@Descripcion varchar(200)
,@UsuarioAutenticado int
 as begin
 
 insert into TipoProducto(
 CodigoReferencia 
,Descripcion 
,FechaCreacion 
,UsuarioCreo 
,FechaModificacion 
,UsuarioModifico
,Activo )
values (
@CodigoReferencia 
,@Descripcion 
,getdate() 
,@UsuarioAutenticado
,getdate() 
,@UsuarioAutenticado
,1)
return SCOPE_IDENTITY()
 end

 GO

Create  procedure EditarTipoProducto
@IdTipoProducto int 
,@CodigoReferencia varchar(100) 
,@Descripcion varchar(200) 
,@UsuarioAutenticado int 
 as begin
 
update  TipoProducto set 
 CodigoReferencia =@CodigoReferencia 
,Descripcion =@Descripcion 
,FechaModificacion =getdate()
,UsuarioModifico=@UsuarioAutenticado
where  
IdTipoProducto =@IdTipoProducto 

return @IdTipoProducto 
end
go
Create  procedure EliminarTipoProducto
@IdTipoProducto int 
,@UsuarioAutenticado int 
 as begin
 update  TipoProducto set Activo=0
 ,FechaModificacion =getdate()
 ,UsuarioModifico=@UsuarioAutenticado 
where
IdTipoProducto =@IdTipoProducto 
return @IdTipoProducto
end
go
Create procedure ConsultarTipoProducto
 @CodigoReferencia varchar(100) = null
,@Descripcion varchar(200)= null
,@IdTipoProducto int = null
as begin
   select
   IdTipoProducto
  ,CodigoReferencia 
  ,Descripcion  from TipoProducto where Activo=1  
  and (CodigoReferencia=@CodigoReferencia or (@CodigoReferencia is null))
  and (Descripcion=@Descripcion or (@Descripcion is null ))
  and (IdTipoProducto=@IdTipoProducto or (@IdTipoProducto is null))
end
