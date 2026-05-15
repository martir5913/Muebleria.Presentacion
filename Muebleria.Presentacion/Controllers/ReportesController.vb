Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class ReportesController
        Inherits Controller

        Private ReadOnly _reportesService As CN_ReportesService

        Public Sub New()
            _reportesService = New CN_ReportesService()
        End Sub

        Private Function VerificarAdmin() As Boolean
            Return Session("Rol") IsNot Nothing AndAlso Session("Rol").ToString() = "ADMIN"
        End Function

        ' GET: Reportes
        Function Index() As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Dim fechaIni As Date = Date.Today.AddDays(-30)
            Dim fechaFin As Date = Date.Today

            Try
                ViewBag.VentasDiarias = _reportesService.VentasDiarias(fechaIni, fechaFin, Nothing)
                ViewBag.ProductoTop = _reportesService.ProductoMasVendido(fechaIni, fechaFin, Nothing)
                ViewBag.FechaIni = fechaIni.ToString("yyyy-MM-dd")
                ViewBag.FechaFin = fechaFin.ToString("yyyy-MM-dd")
            Catch ex As Exception
                ViewBag.Error = ex.Message
                ViewBag.VentasDiarias = New List(Of CE_VentaDiaria)()
                ViewBag.ProductoTop = Nothing
            End Try

            Return View()
        End Function

        ' POST: Reportes/Filtrar
        <HttpPost>
        Function Filtrar(fechaIni As String, fechaFin As String) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Dim fi As Date = Date.Today.AddDays(-30)
            Dim ff As Date = Date.Today
            Date.TryParse(fechaIni, fi)
            Date.TryParse(fechaFin, ff)

            Try
                ViewBag.VentasDiarias = _reportesService.VentasDiarias(fi, ff, Nothing)
                ViewBag.ProductoTop = _reportesService.ProductoMasVendido(fi, ff, Nothing)
                ViewBag.FechaIni = fi.ToString("yyyy-MM-dd")
                ViewBag.FechaFin = ff.ToString("yyyy-MM-dd")
            Catch ex As Exception
                ViewBag.Error = ex.Message
                ViewBag.VentasDiarias = New List(Of CE_VentaDiaria)()
                ViewBag.ProductoTop = Nothing
            End Try

            Return View("Index")
        End Function

    End Class
End Namespace
