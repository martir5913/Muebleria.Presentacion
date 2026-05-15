Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Muebleria.Negocio

Namespace Controllers
    Public Class BodegasController
        Inherits Controller

        Private ReadOnly _bodegasService As CN_BodegasService

        Public Sub New()
            _bodegasService = New CN_BodegasService()
        End Sub

        Private Function VerificarAdmin() As Boolean
            Return Session("Rol") IsNot Nothing AndAlso Session("Rol").ToString() = "ADMIN"
        End Function

        ' GET: Bodegas
        Function Index() As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim bodegas = _bodegasService.ObtenerBodegas()
                Return View(bodegas)
            Catch ex As Exception
                ViewBag.Error = ex.Message
                Return View(New List(Of CE_Bodega)())
            End Try
        End Function

        ' GET: Bodegas/Stock/5
        Function Stock(id As Integer) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim items = _bodegasService.ObtenerStock(id)
                ViewBag.BodegaId = id
                Return View(items)
            Catch ex As Exception
                ViewBag.Error = ex.Message
                Return View(New List(Of CE_StockBodega)())
            End Try
        End Function

        ' GET: Bodegas/Crear
        <HttpGet>
        Function Crear() As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Return View(New CE_Bodega())
        End Function

        ' POST: Bodegas/Crear
        <HttpPost>
        Function Crear(nombre As String, direccion As String, telefono As String) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim b As New CE_Bodega With {
                    .Nombre = nombre,
                    .Direccion = direccion,
                    .Telefono = telefono,
                    .CiudadId = 1
                }
                _bodegasService.CrearBodega(b)
                TempData("Exito") = "Bodega creada exitosamente."
                Return RedirectToAction("Index")
            Catch ex As Exception
                ViewBag.Error = ex.Message
                Return View(New CE_Bodega())
            End Try
        End Function

        ' GET: Bodegas/Editar/5
        <HttpGet>
        Function Editar(id As Integer) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim b = _bodegasService.ObtenerBodegaPorId(id)
                If b Is Nothing Then Return HttpNotFound()
                Return View(b)
            Catch ex As Exception
                TempData("Error") = ex.Message
                Return RedirectToAction("Index")
            End Try
        End Function

        ' POST: Bodegas/Editar
        <HttpPost>
        Function Editar(id As Integer, nombre As String, direccion As String,
                        telefono As String, activo As String) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Try
                Dim b As New CE_Bodega With {
                    .BodegaId = id,
                    .Nombre = nombre,
                    .Direccion = direccion,
                    .Telefono = telefono,
                    .Activo = If(activo, "S")
                }
                _bodegasService.ActualizarBodega(b)
                TempData("Exito") = "Bodega actualizada exitosamente."
                Return RedirectToAction("Index")
            Catch ex As Exception
                ViewBag.Error = ex.Message
                Return View(New CE_Bodega With {
                    .BodegaId = id, .Nombre = nombre,
                    .Direccion = direccion, .Telefono = telefono
                })
            End Try
        End Function

        ' POST: Bodegas/Eliminar
        <HttpPost>
        Function Eliminar(id As Integer) As ActionResult
            If Not VerificarAdmin() Then Return RedirectToAction("Login", "Cuenta")
            Try
                _bodegasService.EliminarBodega(id)
                TempData("Exito") = "Bodega desactivada exitosamente."
            Catch ex As Exception
                TempData("Error") = ex.Message
            End Try
            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace
