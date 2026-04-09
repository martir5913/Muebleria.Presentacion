Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_ProductosService
    Private ReadOnly _productosData As CD_Productos

    Public Sub New()
        _productosData = New CD_Productos()
    End Sub

    Public Function ObtenerTodosLosProductos() As List(Of CE_Producto)
        Try
            Return _productosData.ObtenerTodos()
        Catch ex As Exception
            Throw New Exception("Error en negocio al obtener productos: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerProductoPorId(productoId As Integer) As CE_Producto
        Try
            If productoId <= 0 Then Throw New ArgumentException("El ID del producto no es válido.")
            Return _productosData.ObtenerPorId(productoId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al obtener producto: " & ex.Message, ex)
        End Try
    End Function

    Public Function InsertarProducto(producto As CE_Producto) As Boolean
        Try
            If String.IsNullOrWhiteSpace(producto.Nombre) Then Throw New ArgumentException("El nombre es obligatorio.")
            If String.IsNullOrWhiteSpace(producto.Referencia) Then Throw New ArgumentException("La referencia es obligatoria.")
            If producto.Precio <= 0 Then Throw New ArgumentException("El precio debe ser mayor a cero.")
            Return _productosData.Insertar(producto)
        Catch ex As Exception
            Throw New Exception("Error en negocio al insertar producto: " & ex.Message, ex)
        End Try
    End Function

    Public Function ActualizarProducto(producto As CE_Producto) As Boolean
        Try
            If producto.ProductoId <= 0 Then Throw New ArgumentException("El ID no es válido.")
            If String.IsNullOrWhiteSpace(producto.Nombre) Then Throw New ArgumentException("El nombre es obligatorio.")
            If producto.Precio <= 0 Then Throw New ArgumentException("El precio debe ser mayor a cero.")
            Return _productosData.Actualizar(producto)
        Catch ex As Exception
            Throw New Exception("Error en negocio al actualizar producto: " & ex.Message, ex)
        End Try
    End Function

    Public Function EliminarProducto(productoId As Integer) As Boolean
        Try
            If productoId <= 0 Then Throw New ArgumentException("El ID no es válido.")
            Return _productosData.Eliminar(productoId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al eliminar producto: " & ex.Message, ex)
        End Try
    End Function

End Class