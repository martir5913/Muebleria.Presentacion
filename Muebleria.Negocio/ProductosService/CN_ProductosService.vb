Imports Muebleria.Entidades
Imports Muebleria.Datos

Public Class CN_ProductosService

    Private ReadOnly _datos As New CD_Productos()

    ' 1. Listar
    Public Function ListarProductos() As List(Of CE_Producto)
        Return _datos.ListarProductos()
    End Function

    ' 2. Obtener Producto Por ID (Revisa el error de la línea 37 en la presentación)
    ' Tu controlador ProductosController busca "ObtenerProductoPorId"
    Public Function ObtenerProductoPorId(ByVal id As Integer) As CE_Producto
        Return _datos.ObtenerPorId(id)
    End Function

    ' 3. Registrar
    Public Function RegistrarProducto(ByVal obj As CE_Producto) As Boolean
        ' Si en tu CD_Productos lo llamaste "Insertar", cambia aqui a: _datos.Insertar(obj)
        Return _datos.RegistrarProducto(obj)
    End Function

    ' 4. Modificar
    Public Function ModificarProducto(ByVal obj As CE_Producto) As Boolean
        ' Si en tu CD_Productos lo llamaste "Actualizar", cambia aqui a: _datos.Actualizar(obj)
        Return _datos.ModificarProducto(obj)
    End Function

    ' 5. Eliminar
    Public Function EliminarProducto(ByVal id As Integer) As Boolean
        ' Si en tu CD_Productos lo llamaste "Eliminar" a secas, cambia aqui a: _datos.Eliminar(id)
        Return _datos.EliminarProducto(id)
    End Function

End Class