Namespace Muebleria.Entidades

    Public Class CE_VentaDiaria
        Public Property Fecha As DateTime
        Public Property TipoMueble As String
        Public Property Ciudad As String
        Public Property TotalVentas As Decimal
        Public Property Ordenes As Integer
        Public Property Unidades As Integer
    End Class

    Public Class CE_ProductoVendido
        Public Property ProductoId As Integer
        Public Property Referencia As String
        Public Property Nombre As String
        Public Property TipoMueble As String
        Public Property Ciudad As String
        Public Property Unidades As Integer
        Public Property TotalVentas As Decimal
    End Class

    Public Class CE_CompraCliente
        Public Property OrdenId As Integer
        Public Property NumeroOrden As String
        Public Property FechaCompra As DateTime
        Public Property ValorCompra As Decimal
        Public Property FormaPago As String
        Public Property Muebles As String
    End Class

End Namespace
