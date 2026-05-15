Namespace Muebleria.Entidades

    Public Class CE_Bodega
        Public Property BodegaId As Integer
        Public Property Nombre As String
        Public Property Direccion As String
        Public Property CiudadId As Integer
        Public Property Telefono As String
        Public Property EmpleadoId As Integer
        Public Property Activo As String
        Public Property NombreCiudad As String
        Public Property NombreEmpleado As String
    End Class

    Public Class CE_StockBodega
        Public Property StockId As Integer
        Public Property ProductoId As Integer
        Public Property BodegaId As Integer
        Public Property Cantidad As Integer
        Public Property NombreProducto As String
        Public Property ReferenciaProducto As String
        Public Property NombreBodega As String
    End Class

End Namespace
