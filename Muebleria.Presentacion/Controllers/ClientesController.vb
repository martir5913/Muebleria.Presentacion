Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class ClientesController
        Inherits Controller

        Private ReadOnly _clientesService As CN_ClientesService

        Public Sub New()
            _clientesService = New CN_ClientesService()
        End Sub

        Private Function VerificarSesion() As Boolean
            Return Session("Usuario") IsNot Nothing
        End Function

        ' GET: Clientes (solo ADMIN)
        Function Index() As ActionResult
            If Session("Rol") Is Nothing OrElse Session("Rol").ToString() <> "ADMIN" Then
                Return RedirectToAction("Login", "Cuenta")
            End If
            Try
                Dim clientes = _clientesService.ObtenerTodosLosClientes()
                Return View(clientes)
            Catch ex As Exception
                ViewBag.Error = "Error al cargar clientes: " & ex.Message
                Return View(New List(Of Entidades.Muebleria.Entidades.CE_Cliente)())
            End Try
        End Function

        ' GET: Clientes/Perfil
        <HttpGet>
        Function Perfil() As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim clienteId As Integer = Convert.ToInt32(Session("ClienteId"))
                Dim cliente As CE_Cliente = _clientesService.ObtenerClientePorId(clienteId)
                Return View(cliente)
            Catch ex As Exception
                ViewBag.Error = "Error al cargar perfil: " & ex.Message
                Return View(New CE_Cliente())
            End Try
        End Function

        ' POST: Clientes/EditarPerfil
        <HttpPost>
        Function EditarPerfil(dirección As String, telResidencia As String,
                              telCelular As String, email As String) As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim clienteId As Integer = Convert.ToInt32(Session("ClienteId"))
                Dim cliente As New CE_Cliente With {
                    .ClienteId = clienteId,
                    .Direccion = dirección,
                    .TelResidencia = telResidencia,
                    .TelCelular = telCelular,
                    .Correo = email,
                    .CiudadId = 1
                }
                _clientesService.ActualizarCliente(cliente)
                TempData("Exito") = "Perfil actualizado correctamente."
                Return RedirectToAction("Perfil")
            Catch ex As Exception
                ViewBag.Error = "Error al actualizar perfil: " & ex.Message
                Return RedirectToAction("Perfil")
            End Try
        End Function

        ' GET: Clientes/Sugerencias
        <HttpGet>
        Function Sugerencias() As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")
            Return View()
        End Function

        ' POST: Clientes/Sugerencias
        <HttpPost>
        Function Sugerencias(mensaje As String, tipo As String) As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim clienteId As Integer = Convert.ToInt32(Session("ClienteId"))
                Dim sugerencia As New CE_Sugerencia With {
                    .ClienteId = clienteId,
                    .Mensaje = mensaje,
                    .Tipo = If(tipo, "SUGERENCIA"),
                    .Estado = "PENDIENTE"
                }
                _clientesService.GuardarSugerencia(sugerencia)
                TempData("Exito") = "Sugerencia enviada. ¡Gracias por tu opinión!"
                Return RedirectToAction("Sugerencias")
            Catch ex As Exception
                ViewBag.Error = "Error al enviar sugerencia: " & ex.Message
                Return View()
            End Try
        End Function

    End Class
End Namespace
