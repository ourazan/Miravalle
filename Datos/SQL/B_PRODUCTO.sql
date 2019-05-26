
create table [Producto](
	 IdProducto int primary key identity(1,1) NOT NULL
	,NombreProducto varchar(300) NULL
	,IdTipoProducto int
	,FechaCreacion smalldatetime
	,UsuarioCreo int null
	,FechaModificacion smalldatetime null
	,UsuarioModifico int null
	,Activo bit  null
	,foreign key (IdTipoProducto) references TipoProducto(IdTipoProducto)
 )

 GO
Create  procedure CrearProducto
@NombreProducto varchar(300) 
,@IdTipoProducto int
,@UsuarioAutenticado int
 as begin
 
 insert into Producto(
 NombreProducto 
,IdTipoProducto 
,FechaCreacion 
,UsuarioCreo 
,FechaModificacion 
,UsuarioModifico
,Activo )
values (
 @NombreProducto 
,@IdTipoProducto  
,getdate() 
,@UsuarioAutenticado
,getdate() 
,@UsuarioAutenticado
,1)
return SCOPE_IDENTITY()
 end

 GO
 Create  procedure EditarProducto
 @NombreProducto varchar(300) 
,@IdTipoProducto int
,@UsuarioAutenticado int
,@IdProducto int
 as begin
 update Producto set 
 NombreProducto =@NombreProducto
,IdTipoProducto=@IdTipoProducto
,FechaModificacion =getdate()
,UsuarioModifico=@UsuarioAutenticado
where  
IdProducto =@IdProducto 
return @IdProducto 
end
go

Create  procedure EliminarProducto
@IdProducto int 
,@UsuarioAutenticado int 
 as begin
 update  Producto set Activo=0
 ,FechaModificacion =getdate()
 ,UsuarioModifico=@UsuarioAutenticado 

where
IdProducto =@IdProducto 

return @IdProducto
end
go

Create view v_producto as
select
        Producto.[IdProducto]
      , Producto.[NombreProducto]
      , Producto.[IdTipoProducto]
	  , Producto .Activo
      , TipoProducto.[CodigoReferencia]
      , TipoProducto.[Descripcion]
     
 from Producto
inner join TipoProducto on TipoProducto.IdTipoProducto=Producto.IdTipoProducto

Go 



Create procedure ConsultarProducto
 @NombreProducto varchar(300) = null
,@IdTipoProducto int=null
,@IdProducto int= null
as begin
 Select
        IdProducto
	   ,NombreProducto
	   ,IdTipoProducto
	   ,Activo
	   ,CodigoReferencia
	   ,Descripcion
	   from v_producto where Activo=1  
	   and ((@NombreProducto is not null and NombreProducto like @NombreProducto +'%') or (@NombreProducto is null))
	   and (IdTipoProducto=@IdTipoProducto or (@IdTipoProducto is null))
	   and (IdProducto=@IdProducto or(@IdProducto is null))
end

