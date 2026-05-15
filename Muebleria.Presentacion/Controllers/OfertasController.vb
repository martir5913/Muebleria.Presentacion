Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Namespace Controllers
    Public Class OfertasController
        Inherits Controller

        Private Function VerificarSesion() As Boolean
            Return Session("Usuario") IsNot Nothing
        End Function

        ' GET: Ofertas
        Function Index(Optional cat As String = "") As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")

            Dim todos As List(Of CE_Producto) = MockProductos.ObtenerTodos()
            Dim ofertaList As List(Of CE_Producto) = todos.Where(
                Function(p) p.Descuento.HasValue AndAlso p.Descuento.Value > 0).ToList()

            Dim cats As List(Of String) = ofertaList.Select(Function(p) p.TipoMuebleNombre) _
                .Where(Function(n) Not String.IsNullOrEmpty(n)) _
                .Distinct().OrderBy(Function(n) n).ToList()

            ViewBag.Categorias = cats
            ViewBag.CatActual = If(String.IsNullOrWhiteSpace(cat), "todos", cat.Trim().ToLower())

            Return View(ofertaList)
        End Function

    End Class
End Namespace
