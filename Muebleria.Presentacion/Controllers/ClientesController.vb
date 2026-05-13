Imports Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class ClientesController
        Inherits Controller

        Private ReadOnly _clientesService As CN_ClientesService

        Public Sub New()
            _clientesService = New CN_ClientesService()
        End Sub

        ' GET: Clientes
        Function Index() As ActionResult
            If Session("Rol") Is Nothing OrElse Session("Rol").ToString() <> "ADMIN" Then
                Return RedirectToAction("Login", "Cuenta")
            End If
            Try
                Dim clientes = _clientesService.ObtenerTodosLosClientes()
                Return View(clientes)
            Catch ex As Exception
                ViewBag.Error = "Error al cargar clientes: " & ex.Message
                Return View(New List(Of CE_Cliente)())
            End Try
        End Function

    End Class
End Namespace
