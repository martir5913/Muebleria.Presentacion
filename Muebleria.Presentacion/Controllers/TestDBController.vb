Imports Muebleria.Negocio

Namespace Controllers
    Public Class TestDBController
        Inherits Controller

        Private ReadOnly _service As CN_ConexionService

        Public Sub New()
            _service = New CN_ConexionService()
        End Sub
        ' GET: TestDB
        Function Index() As ActionResult
            Return View()
        End Function

        <HttpGet>
        Function TestConexion() As JsonResult
            Try
                Dim estado As Boolean = _service.TestConexion()

                Return Json(New With {
                    .success = estado,
                    .message = If(estado, "Conexión exitosa a Oracle", "Fallo en conexión")
                }, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                Return Json(New With {
                    .success = False,
                    .message = ex.Message
                }, JsonRequestBehavior.AllowGet)
            End Try
        End Function
    End Class
End Namespace