Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_UsuariosService
    Private ReadOnly _usuariosData As CD_Usuarios

    Public Sub New()
        _usuariosData = New CD_Usuarios()
    End Sub

    ' ─── LOGIN ───────────────────────────────────────────────────
    Public Function Login(username As String, password As String) As CE_Usuario
        If String.IsNullOrWhiteSpace(username) Then
            Throw New ArgumentException("El usuario es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(password) Then
            Throw New ArgumentException("La contraseña es obligatoria.")
        End If
        Try
            Return _usuariosData.Login(username, password)
        Catch ex As Exception
            Throw New Exception("Error al iniciar sesión: " & ex.Message, ex)
        End Try
    End Function

    ' ─── REGISTRAR USUARIO CLIENTE ───────────────────────────────
    Public Function RegistrarUsuario(username As String, password As String,
                                     rol As String, clienteId As Integer) As Boolean
        If String.IsNullOrWhiteSpace(username) Then
            Throw New ArgumentException("El nombre de usuario es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(password) OrElse password.Length < 6 Then
            Throw New ArgumentException("La contraseña debe tener al menos 6 caracteres.")
        End If
        Try
            If rol = "ADMIN" Then
                _usuariosData.InsertarAdmin(username, password)
            Else
                _usuariosData.InsertarCliente(clienteId, username, password)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Error al registrar usuario: " & ex.Message, ex)
        End Try
    End Function

End Class
