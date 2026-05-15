Namespace Muebleria.Entidades

    Public Class CE_Cliente
        Public Property ClienteId As Integer
        Public Property TipoPersona As String  ' N = natural, J = jurídica
        Public Property TipoDocId As Integer
        Public Property NumDocumento As String
        Public Property Nombres As String
        Public Property Apellidos As String
        Public Property RazonSocial As String
        Public Property TelResidencia As String
        Public Property TelCelular As String
        Public Property Correo As String       ' columna EMAIL en BD
        Public Property Direccion As String
        Public Property CiudadId As Integer
        Public Property Profesion As String
        Public Property Activo As String
        Public Property FechaCreacion As DateTime

        Public Sub New()
            TipoPersona = "N"
            TipoDocId = 1
            CiudadId = 1
            Activo = "S"
            FechaCreacion = DateTime.Now
        End Sub

        Public ReadOnly Property NombreCompleto As String
            Get
                If TipoPersona = "J" Then
                    Return If(RazonSocial, "")
                End If
                Return (If(Nombres, "") & " " & If(Apellidos, "")).Trim()
            End Get
        End Property
    End Class

End Namespace
