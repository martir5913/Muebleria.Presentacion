Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_CarritoService
    Private ReadOnly _carritoData As CD_Carrito
    Private ReadOnly _productosData As CD_Productos

    Public Sub New()
        _carritoData = New CD_Carrito()
        _productosData = New CD_Productos()
    End Sub

    Public Function ObtenerCarritoActivo(clienteId As Integer) As CE_Carrito
        Try
            If clienteId <= 0 Then Throw New ArgumentException("El ID del cliente no es válido.")
            Return _carritoData.ObtenerOCrearCarrito(clienteId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al obtener carrito: " & ex.Message, ex)
        End Try
    End Function

    Public Function AgregarProducto(clienteId As Integer, carritoId As Integer, productoId As Integer, cantidad As Integer) As Boolean
        Try
            If clienteId <= 0 Then Throw New ArgumentException("El cliente no es válido.")
            If carritoId <= 0 Then Throw New ArgumentException("El carrito no es válido.")
            If productoId <= 0 Then Throw New ArgumentException("El producto no es válido.")
            If cantidad <= 0 Then Throw New ArgumentException("La cantidad debe ser mayor a cero.")

            Dim producto As CE_Producto = _productosData.ObtenerPorId(productoId)
            If producto Is Nothing OrElse producto.Activo = "N" Then
                Throw New Exception("El producto no está disponible.")
            End If

            Dim item As New CE_CarritoItem With {
                .carritoId = carritoId,
                .productoId = productoId,
                .cantidad = cantidad
            }
            Return _carritoData.AgregarItem(clienteId, item)
        Catch ex As Exception
            Throw New Exception("Error en negocio al agregar producto: " & ex.Message, ex)
        End Try
    End Function

    Public Function EliminarItem(clienteId As Integer, carritoId As Integer, productoId As Integer) As Boolean
        Try
            If clienteId <= 0 Then Throw New ArgumentException("El cliente no es válido.")
            If carritoId <= 0 Then Throw New ArgumentException("El carrito no es válido.")
            If productoId <= 0 Then Throw New ArgumentException("El producto no es válido.")
            Return _carritoData.EliminarItem(clienteId, carritoId, productoId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al eliminar item: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerItems(clienteId As Integer, carritoId As Integer) As List(Of CE_CarritoItem)
        Try
            If clienteId <= 0 Then Throw New ArgumentException("El cliente no es válido.")
            If carritoId <= 0 Then Throw New ArgumentException("El carrito no es válido.")
            Return _carritoData.ObtenerItems(clienteId, carritoId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al obtener items: " & ex.Message, ex)
        End Try
    End Function

    Public Function VaciarCarrito(clienteId As Integer, carritoId As Integer) As Boolean
        Try
            If clienteId <= 0 Then Throw New ArgumentException("El cliente no es válido.")
            If carritoId <= 0 Then Throw New ArgumentException("El carrito no es válido.")
            Return _carritoData.VaciarCarrito(clienteId, carritoId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al vaciar carrito: " & ex.Message, ex)
        End Try
    End Function

    Public Function CalcularTotal(clienteId As Integer, carritoId As Integer) As Decimal
        Try
            Dim items = _carritoData.ObtenerItems(clienteId, carritoId)
            Return items.Sum(Function(i) i.Subtotal)
        Catch ex As Exception
            Throw New Exception("Error en negocio al calcular total: " & ex.Message, ex)
        End Try
    End Function

End Class
