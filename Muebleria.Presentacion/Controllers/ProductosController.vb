Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class ProductosController
        Inherits Controller

        Private ReadOnly _productosService As CN_ProductosService

        Public Sub New()
            _productosService = New CN_ProductosService()
        End Sub

        Private Function VerificarAdmin() As Boolean
            Return Session("Rol") IsNot Nothing AndAlso Session("Rol").ToString() = "ADMIN"
        End Function

        ' GET: Productos
        Function Index() As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Try
                Dim productos = _productosService.ObtenerTodosLosProductos()
                Return View(productos)
            Catch ex As Exception
                ViewBag.Error = "Error al cargar productos: " & ex.Message
                Return View(New List(Of CE_Producto)())
            End Try
        End Function

        ' GET: Productos/Crear
        <HttpGet>
        Function Crear() As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Return View(New CE_Producto())
        End Function

        ' POST: Productos/Crear
        <HttpPost>
        Function Crear(producto As CE_Producto) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Try
                _productosService.InsertarProducto(producto)
                TempData("Exito") = "Producto creado exitosamente."
                Return RedirectToAction("Index")
            Catch ex As Exception
                ViewBag.Error = "Error: " & ex.Message
                Return View(producto)
            End Try
        End Function

        ' GET: Productos/Editar/5
        <HttpGet>
        Function Editar(id As Integer) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Try
                Dim producto = _productosService.ObtenerProductoPorId(id)
                If producto Is Nothing Then Return HttpNotFound()
                Return View(producto)
            Catch ex As Exception
                TempData("Error") = "Error: " & ex.Message
                Return RedirectToAction("Index")
            End Try
        End Function

        ' POST: Productos/Editar
        <HttpPost>
        Function Editar(producto As CE_Producto) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Try
                _productosService.ActualizarProducto(producto)
                TempData("Exito") = "Producto actualizado."
                Return RedirectToAction("Index")
            Catch ex As Exception
                ViewBag.Error = "Error: " & ex.Message
                Return View(producto)
            End Try
        End Function

        ' POST: Productos/Eliminar/5
        <HttpPost>
        Function Eliminar(id As Integer) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")

            Try
                _productosService.EliminarProducto(id)
                TempData("Exito") = "Producto eliminado."
            Catch ex As Exception
                TempData("Error") = "Error: " & ex.Message
            End Try

            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace