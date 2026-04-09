Namespace Muebleria.Entidades

    Public Class CE_Pago

        Public Property PagoId As Integer
        Public Property OrdenId As Integer
        Public Property Monto As Decimal
        Public Property Fecha As DateTime
        Public Property FormaPago As String   ' TARJETA / EFECTIVO / TRANSFERENCIA

        Public Sub New()
            Fecha = DateTime.Now
        End Sub

        Public Sub New(pagoId As Integer, ordenId As Integer, monto As Decimal,
                       fecha As DateTime, formaPago As String)
            Me.PagoId = pagoId
            Me.OrdenId = ordenId
            Me.Monto = monto
            Me.Fecha = fecha
            Me.FormaPago = formaPago
        End Sub

    End Class

End Namespace
