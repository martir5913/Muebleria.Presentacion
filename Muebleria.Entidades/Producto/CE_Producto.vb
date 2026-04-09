Namespace Muebleria.Entidades

    Public Class CE_Producto

        Public Property ProductoId As Integer
        Public Property Nombre As String
        Public Property Referencia As String
        Public Property Precio As Decimal
        Public Property Activo As String

        Public Sub New()
            Activo = "S"
        End Sub

        Public Sub New(id As Integer, nombre As String, referencia As String, precio As Decimal, activo As String)
            ProductoId = id
            Me.Nombre = nombre
            Me.Referencia = referencia
            Me.Precio = precio
            Me.Activo = activo
        End Sub

    End Class

End Namespace