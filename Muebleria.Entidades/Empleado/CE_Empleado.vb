Namespace Muebleria.Entidades

    Public Class CE_Empleado

        Public Property EmpleadoId As Integer
        Public Property Nombre As String
        Public Property Cargo As String
        Public Property Salario As Decimal
        Public Property Activo As String

        Public Sub New()
            Activo = "S"
        End Sub

        Public Sub New(id As Integer, nombre As String, cargo As String,
                       salario As Decimal, activo As String)
            EmpleadoId = id
            Me.Nombre = nombre
            Me.Cargo = cargo
            Me.Salario = salario
            Me.Activo = activo
        End Sub

    End Class

End Namespace