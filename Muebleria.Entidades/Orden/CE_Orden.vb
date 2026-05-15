Namespace Muebleria.Entidades

    Public Class CE_Orden

        Public Property OrdenId As Integer
        Public Property ClienteId As Integer
        Public Property Total As Decimal
        Public Property Fecha As DateTime

        Public Sub New()
            Fecha = DateTime.Now
        End Sub

        Public Sub New(id As Integer, clienteId As Integer, total As Decimal, fecha As DateTime)
            OrdenId = id
            Me.ClienteId = clienteId
            Me.Total = total
            Me.Fecha = fecha
        End Sub

    End Class

    Public Class CE_OrdenDetalle

        Public Property DetalleId As Integer
        Public Property OrdenId As Integer
        Public Property ProductoId As Integer
        Public Property Cantidad As Integer
        Public Property Subtotal As Decimal

        ' Datos extras para mostrar en reportes
        Public Property NombreProducto As String
        Public Property ReferenciaProducto As String

        Public Sub New()
        End Sub

        Public Sub New(detalleId As Integer, ordenId As Integer, productoId As Integer,
                       cantidad As Integer, subtotal As Decimal)
            Me.DetalleId = detalleId
            Me.OrdenId = ordenId
            Me.ProductoId = productoId
            Me.Cantidad = cantidad
            Me.Subtotal = subtotal
        End Sub

    End Class

End Namespace
