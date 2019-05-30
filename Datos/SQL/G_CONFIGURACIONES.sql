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

INSERT INTO [dbo].[Configuraciones]
           ([Configuracion]
           ,[Valor]
           ,[FechaCreacion]
           ,[UsuarioCreo]
           ,[FechaModificacion]
           ,[UsuarioModifico]
           ,[Activo])
     VALUES
           ('Productoavencer'
           ,'30'
           ,getdate()
           ,0
           ,getdate()
           ,0
           ,1)


INSERT INTO [dbo].[Configuraciones]
           ([Configuracion]
           ,[Valor]
           ,[FechaCreacion]
           ,[UsuarioCreo]
           ,[FechaModificacion]
           ,[UsuarioModifico]
           ,[Activo])
     VALUES
           ('ProductoMinimo'
           ,'10'
           ,getdate()
           ,0
           ,getdate()
           ,0
           ,1)


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
as begin
select 
 IdConfiguracion
,Configuracion 
,Valor
 from Configuraciones where Activo=1  
end



INSERT INTO [dbo].[Configuraciones]
           ([Configuracion]
           ,[Valor]
           ,[FechaCreacion]
           ,[UsuarioCreo]
           ,[FechaModificacion]
           ,[UsuarioModifico]
           ,[Activo])
     VALUES
           ('Productoavencer'
           ,'30'
           ,getdate()
           ,0
           ,getdate()
           ,0
           ,1)


go
 create  procedure [dbo].[ObtenerProductosaVencer] 

as
begin


declare @diasaVencer int 
select @diasaVencer= cast(isnull(Valor,30) as int) from Configuraciones where Configuracion='ProductoMinimo'
select v_Inventario.IdInventario 
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
,v_Inventario.Perfil
from v_Inventario where activo=1 and
DATEDIFF (dd,getdate(), Convert(Date,FechaVencimiento)) <=  @diasaVencer


end


go


 create  procedure [dbo].[ObtenerProductosEscasos] 

as
begin


declare @minimo int 
select @minimo=300000  from Configuraciones where Configuracion='ProductoMinimo'--isnull(cast(Valor as int ),300000)
select v_Inventario.IdInventario 
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
,v_Inventario.Perfil
from v_Inventario where  activo=1 and
 Cantidad <=  @minimo


end