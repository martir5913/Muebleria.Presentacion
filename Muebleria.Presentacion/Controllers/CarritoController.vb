Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class CarritoController
        Inherits Controller

        Private ReadOnly _carritoService As CN_CarritoService
        Private ReadOnly _ordenesService As CN_OrdenesService

        Public Sub New()
            _carritoService = New CN_CarritoService()
            _ordenesService = New CN_OrdenesService()
        End Sub

        Private Function VerificarSesion() As Boolean
            Return Session("Usuario") IsNot Nothing
        End Function

        ' GET: Carrito
        Function Index() As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")

            Try
                Dim clienteId As Integer = Convert.ToInt32(Session("ClienteId"))
                Dim carrito As CE_Carrito = _carritoService.ObtenerCarritoActivo(clienteId)

                If carrito IsNot Nothing Then
                    Session("CarritoId") = carrito.CarritoId
                    Dim items = _carritoService.ObtenerItems(carrito.CarritoId)
                    ViewBag.Total = items.Sum(Function(i) i.Subtotal)
                    Return View(items)
                End If

                Return View(New List(Of CE_CarritoItem)())

            Catch ex As Exception
                ViewBag.Error = "Error al cargar carrito: " & ex.Message
                Return View(New List(Of CE_CarritoItem)())
            End Try
        End Function

        ' POST: Carrito/EliminarItem
        <HttpPost>
        Function EliminarItem(itemId As Integer) As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")

            Try
                _carritoService.EliminarItem(itemId)
                TempData("Exito") = "Producto eliminado del carrito."
            Catch ex As Exception
                TempData("Error") = "Error: " & ex.Message
            End Try

            Return RedirectToAction("Index")
        End Function

        ' POST: Carrito/Comprar
        <HttpPost>
        Function Comprar(formaPago As String) As ActionResult
            If Not VerificarSesion() Then Return RedirectToAction("Login", "Cuenta")

            Try
                Dim clienteId As Integer = Convert.ToInt32(Session("ClienteId"))
                Dim carritoId As Integer = Convert.ToInt32(Session("CarritoId"))

                Dim ordenId As Integer = _ordenesService.EfectuarCompra(clienteId, carritoId, formaPago)

                Session("UltimaOrdenId") = ordenId
                Session.Remove("CarritoId")

                Return RedirectToAction("Confirmacion")

            Catch ex As Exception
                TempData("Error") = "Error al procesar compra: " & ex.Message
                Return RedirectToAction("Index")
            End Try
        End Function

        ' GET: Carrito/Confirmacion
        Function Confirmacion() As ActionResult
            If Session("UltimaOrdenId") Is Nothing Then
                Return RedirectToAction("Index", "Catalogo")
            End If

            Try
                Dim ordenId As Integer = Convert.ToInt32(Session("UltimaOrdenId"))
                Dim detalle = New CN_OrdenesService().ObtenerDetalleOrden(ordenId)

                ViewBag.OrdenId = "#" & ordenId.ToString("D6")
                ViewBag.Total = detalle.Sum(Function(d) d.Subtotal).ToString("N2")

                Session.Remove("UltimaOrdenId")
                Return View(detalle)

            Catch ex As Exception
                ViewBag.Error = ex.Message
                Return View(New List(Of CE_OrdenDetalle)())
            End Try
        End Function

    End Class
End Namespace