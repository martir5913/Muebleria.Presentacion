Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private ReadOnly _productosService As CN_ProductosService

    Public Sub New()
        _productosService = New CN_ProductosService()
    End Sub

    Function Index() As ActionResult
        ViewBag.Destacados = MockProductos.ObtenerMasVendidos()
        ViewBag.Novedades = MockProductos.ObtenerNovedades()
        Return View()
    End Function

End Class
