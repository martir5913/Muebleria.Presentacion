Imports System.Security.Cryptography
Imports System.Text
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
        Try
            If String.IsNullOrWhiteSpace(username) Then
                Throw New ArgumentException("El usuario es obligatorio.")
            End If
            If String.IsNullOrWhiteSpace(password) Then
                Throw New ArgumentException("La contraseña es obligatoria.")
            End If

            Dim usuario As CE_Usuario = _usuariosData.Login(username, password)

            If usuario Is Nothing Then
                Throw New Exception("Usuario o contraseña incorrectos.")
            End If

            Return usuario
        Catch ex As Exception
            Throw New Exception("Error en negocio al iniciar sesión: " & ex.Message, ex)
        End Try
    End Function

    ' ─── REGISTRAR ───────────────────────────────────────────────
    Public Function RegistrarUsuario(username As String, password As String,
                                     rol As String, clienteId As Integer) As Boolean
        Try
            If String.IsNullOrWhiteSpace(username) Then
                Throw New ArgumentException("El nombre de usuario es obligatorio.")
            End If
            If String.IsNullOrWhiteSpace(password) OrElse password.Length < 6 Then
                Throw New ArgumentException("La contraseña debe tener al menos 6 caracteres.")
            End If
            If rol <> "ADMIN" AndAlso rol <> "CLIENTE" Then
                Throw New ArgumentException("El rol debe ser ADMIN o CLIENTE.")
            End If

            Dim usuario As New CE_Usuario(0, username, HashSHA256(password), rol, clienteId)

            Return _usuariosData.Insertar(usuario)
        Catch ex As Exception
            Throw New Exception("Error en negocio al registrar usuario: " & ex.Message, ex)
        End Try
    End Function

    ' ─── HASH SHA256 (privado) ───────────────────────────────────
    Private Function HashSHA256(input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(input))
            Dim sb As New StringBuilder()
            For Each b As Byte In bytes
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function

End Class