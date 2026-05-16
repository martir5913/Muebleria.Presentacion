Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class CuentaController
        Inherits Controller

        Private ReadOnly _usuariosService As CN_UsuariosService

        Public Sub New()
            _usuariosService = New CN_UsuariosService()
        End Sub

        ' GET: Cuenta/Login
        <HttpGet>
        Function Login() As ActionResult
            If Session("Usuario") IsNot Nothing Then
                Return RedirectToAction("Index", "Catalogo")
            End If
            Return View()
        End Function

        ' POST: Cuenta/Login
        <HttpPost>
        Function Login(username As String, password As String) As ActionResult
            Try
                Dim usuario As CE_Usuario = _usuariosService.Login(username, password)

                If usuario Is Nothing Then
                    ViewBag.Error = "Usuario o contraseña incorrectos."
                    Return View()
                End If

                Session("Usuario") = usuario
                Session("UsuarioId") = usuario.UsuarioId
                Session("Rol") = usuario.Rol
                Session("ClienteId") = usuario.ClienteId
                Session("Username") = usuario.Username
                Session("UsuarioNombre") = usuario.Username

                Return RedirectToAction("Index", "Catalogo")

            Catch ex As Exception
                ViewBag.Error = "Error al iniciar sesión: " & ex.Message
                Return View()
            End Try
        End Function

        ' GET: Cuenta/Registro
        <HttpGet>
        Function Registro() As ActionResult
            Return View()
        End Function

        ' POST: Cuenta/Registro
        <HttpPost>
        Function Registro(nombres As String, apellidos As String, email As String,
                          direccion As String, numDocumento As String, telResidencia As String,
                          username As String, password As String) As ActionResult
            Try
                Dim clientesService As New CN_ClientesService()

                Dim cliente As New CE_Cliente With {
                    .Nombres = nombres,
                    .Apellidos = apellidos,
                    .Correo = email,
                    .Direccion = If(direccion, ""),
                    .NumDocumento = numDocumento,
                    .TelResidencia = telResidencia,
                    .TipoPersona = "N",
                    .TipoDocId = 1,
                    .CiudadId = 1
                }
                Dim nuevoClienteId As Integer = clientesService.RegistrarCliente(cliente)

                _usuariosService.RegistrarUsuario(username, password, "CLIENTE", nuevoClienteId)

                TempData("Exito") = "Cuenta creada. Ya puedes iniciar sesión."
                Return RedirectToAction("Login")

            Catch ex As Exception
                ViewBag.Error = ex.Message
                Return View()
            End Try
        End Function

        ' GET: Cuenta/Logout
        Function Logout() As ActionResult
            Session.Clear()
            Session.Abandon()
            Return RedirectToAction("Login")
        End Function

    End Class
End Namespace
