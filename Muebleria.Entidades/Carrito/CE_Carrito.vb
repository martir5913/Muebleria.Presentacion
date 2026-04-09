Namespace Muebleria.Entidades

    Public Class CE_Carrito

        Public Property CarritoId As Integer
        Public Property ClienteId As Integer
        Public Property Estado As String

        Public Sub New()
            Estado = "ACTIVO"
        End Sub

        Public Sub New(id As Integer, clienteId As Integer, estado As String)
            CarritoId = id
            Me.ClienteId = clienteId
            Me.Estado = estado
        End Sub

    End Class

    Public Class CE_CarritoItem

        Public Property ItemId As Integer
        Public Property CarritoId As Integer
        Public Property ProductoId As Integer
        Public Property Cantidad As Integer

        ' Datos extras para mostrar en pantalla
        Public Property NombreProducto As String
        Public Property PrecioUnitario As Decimal
        Public ReadOnly Property Subtotal As Decimal
            Get
                Return Cantidad * PrecioUnitario
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub New(itemId As Integer, carritoId As Integer, productoId As Integer, cantidad As Integer)
            Me.ItemId = itemId
            Me.CarritoId = carritoId
            Me.ProductoId = productoId
            Me.Cantidad = cantidad
        End Sub

    End Class

End Namespace