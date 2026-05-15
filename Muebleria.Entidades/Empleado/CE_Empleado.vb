Namespace Muebleria.Entidades

    Public Class CE_Empleado

        Public Property EmpleadoId As Integer
        Public Property Nombres As String
        Public Property Apellidos As String
        Public Property Dpi As String
        Public Property Telefono As String
        Public Property Email As String
        Public Property Cargo As String
        Public Property Salario As Decimal
        Public Property FechaContratacion As DateTime
        Public Property Estado As String  ' A = Activo, I = Inactivo

        Public Sub New()
            Estado = "A"
        End Sub

        Public ReadOnly Property NombreCompleto As String
            Get
                Return (If(Nombres, "") & " " & If(Apellidos, "")).Trim()
            End Get
        End Property

    End Class

End Namespace
