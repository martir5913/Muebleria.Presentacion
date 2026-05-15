Namespace Muebleria.Entidades

    Public Class CE_Sugerencia

        Public Property SugerenciaId As Integer
        Public Property ClienteId As Integer
        Public Property Tipo As String          ' SUGERENCIA / QUEJA / RECLAMO
        Public Property Mensaje As String
        Public Property Estado As String        ' PENDIENTE / ATENDIDA
        Public Property FechaCreacion As DateTime
        Public Property NombreCliente As String

        Public Sub New()
            Tipo = "SUGERENCIA"
            Estado = "PENDIENTE"
            FechaCreacion = DateTime.Now
        End Sub

    End Class

End Namespace
