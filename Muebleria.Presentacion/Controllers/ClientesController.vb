Imports Muebleria.Negocio
Imports Muebleria.Entidades

Namespace Controllers
    Public Class ClientesController
        Inherits Controller

        Private ReadOnly _clientesService As CN_ClientesService

        Public Sub New()
            _clientesService = New CN_ClientesService()
        End Sub

        ' GET: Clientes
        Function Index() As ActionResult
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
