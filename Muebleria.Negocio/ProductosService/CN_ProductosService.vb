Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_ProductosService
    Private ReadOnly _productosData As CD_Productos

    Public Sub New()
        _productosData = New CD_Productos()
    End Sub

    ' Catálogo público (solo activos) — para la tienda
    Public Function ObtenerCatalogo(texto As String, tipoMuebleId As Object) As List(Of CE_Producto)
        Try
            Return _productosData.Buscar(texto, tipoMuebleId)
        Catch ex As Exception
            Throw New Exception("Error al obtener catálogo: " & ex.Message, ex)
        End Try
    End Function

    ' Admin — incluye inactivos
    Public Function ObtenerTodosLosProductos() As List(Of CE_Producto)
        Try
            Return _productosData.ObtenerTodos()
        Catch ex As Exception
            Throw New Exception("Error al obtener productos: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerProductoPorId(productoId As Integer) As CE_Producto
        If productoId <= 0 Then Throw New ArgumentException("ID de producto inválido.")
        Try
            Return _productosData.ObtenerPorId(productoId)
        Catch ex As Exception
            Throw New Exception("Error al obtener producto: " & ex.Message, ex)
        End Try
    End Function

    ' Crea el producto y luego fija precio/stock en MDA_INVENTARIO
    Public Function InsertarProducto(producto As CE_Producto) As Boolean
        If String.IsNullOrWhiteSpace(producto.Nombre) Then
            Throw New ArgumentException("El nombre del producto es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(producto.Referencia) Then
            Throw New ArgumentException("La referencia es obligatoria.")
        End If
        Try
            Dim nuevoId As Integer = _productosData.Insertar(producto)
            If nuevoId > 0 AndAlso (producto.Precio > 0 OrElse producto.Stock > 0) Then
                _productosData.ActualizarInventario(nuevoId, producto.Precio, producto.Stock)
            End If
            Return nuevoId > 0
        Catch ex As Exception
            Throw New Exception("Error al insertar producto: " & ex.Message, ex)
        End Try
    End Function

    ' Actualiza datos del producto y su inventario
    Public Function ActualizarProducto(producto As CE_Producto) As Boolean
        If producto.ProductoId <= 0 Then Throw New ArgumentException("ID de producto inválido.")
        If String.IsNullOrWhiteSpace(producto.Nombre) Then Throw New ArgumentException("El nombre es obligatorio.")
        Try
            _productosData.Actualizar(producto)
            If producto.Precio >= 0 OrElse producto.Stock >= 0 Then
                _productosData.ActualizarInventario(producto.ProductoId, producto.Precio, producto.Stock)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Error al actualizar producto: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerCategorias() As List(Of String)
        Return _productosData.ObtenerNombresCategorias()
    End Function

    Public Function ObtenerMapaTipos() As Dictionary(Of Integer, String)
        Return _productosData.ObtenerMapaTipos()
    End Function

    Public Function EliminarProducto(productoId As Integer) As Boolean
        If productoId <= 0 Then Throw New ArgumentException("ID de producto inválido.")
        Try
            Return _productosData.Eliminar(productoId)
        Catch ex As Exception
            Throw New Exception("Error al eliminar producto: " & ex.Message, ex)
        End Try
    End Function

End Class
