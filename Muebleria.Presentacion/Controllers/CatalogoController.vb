Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class CatalogoController
        Inherits Controller

        Private ReadOnly _productosService As CN_ProductosService
        Private ReadOnly _carritoService As CN_CarritoService

        Public Sub New()
            _productosService = New CN_ProductosService()
            _carritoService = New CN_CarritoService()
        End Sub

        Private Function VerificarSesion() As Boolean
            Return Session("Usuario") IsNot Nothing
        End Function

        ' GET: Catalogo
        Function Index(Optional cat As String = "") As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")

            Dim productos As List(Of CE_Producto) = MockProductos.ObtenerTodos()

            Dim cats = productos _
                .Select(Function(p) p.TipoMuebleNombre) _
                .Where(Function(n) Not String.IsNullOrEmpty(n)) _
                .Distinct() _
                .OrderBy(Function(n) n) _
                .ToList()

            ViewBag.Categorias = cats
            ViewBag.CatActual = If(String.IsNullOrWhiteSpace(cat), "todos", cat.Trim().ToLower())

            Return View(productos)
        End Function

        ' POST: Catalogo/AgregarAlCarrito
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function AgregarAlCarrito(productoId As Integer, cantidad As Integer) As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")

            Try
                Dim clienteId As Integer = Convert.ToInt32(Session("ClienteId"))
                Dim carrito As CE_Carrito = _carritoService.ObtenerCarritoActivo(clienteId)
                _carritoService.AgregarProducto(clienteId, carrito.CarritoId, productoId, cantidad)

                TempData("Exito") = "Producto agregado al carrito."
                Return RedirectToAction("Index")

            Catch ex As Exception
                TempData("Error") = "Error: " & ex.Message
                Return RedirectToAction("Index")
            End Try
        End Function

    End Class
End Namespace
