Namespace Muebleria.Entidades

    Public Class CE_Usuario

        Public Property UsuarioId As Integer
        Public Property Username As String
        Public Property PasswordHash As String
        Public Property Rol As String
        Public Property ClienteId As Integer
        Public Property Activo As String

        Public Sub New()
            Activo = "S"
        End Sub

        Public Sub New(id As Integer, username As String, passwordHash As String,
                       rol As String, clienteId As Integer)
            UsuarioId = id
            Me.Username = username
            Me.PasswordHash = passwordHash
            Me.Rol = rol
            Me.ClienteId = clienteId
            Me.Activo = "S"
        End Sub

        Public Sub New(id As Integer, username As String, passwordHash As String,
                       rol As String, clienteId As Integer, activo As String)
            UsuarioId = id
            Me.Username = username
            Me.PasswordHash = passwordHash
            Me.Rol = rol
            Me.ClienteId = clienteId
            Me.Activo = activo
        End Sub

    End Class

End Namespace
