Imports System.Web.Mvc
Imports Muebleria.Entidades
Imports Newtonsoft.Json
Imports Muebleria.Negocio

Public Class ProductosController
    Inherits Controller

    Private ReadOnly _service As New CN_ProductosService()

    ' GET: Productos (Carga la página HTML base)
    Public Function Index() As ActionResult
        If Session("Rol") IsNot Nothing AndAlso Session("Rol").ToString().ToUpper() = "ADMIN" Then
            Return View()
        End If
        Return RedirectToAction("Index", "Home")
    End Function

    ' =============================================
    ' REQUERIDOS POR EL SCRIPT JAVASCRIPT (AJAX)
    ' =============================================

    ' GET: Productos/ObtenerTodos
    <HttpGet>
    Public Function ObtenerTodos() As JsonResult
        Try
            Dim lista As List(Of CE_Producto) = _service.ListarProductos()
            Return Json(lista, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine("Error en ObtenerTodos: " & ex.ToString())
            Response.StatusCode = 500
            Return Json(New With {.Error = ex.Message}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    ' GET: Productos/ObtenerPorId?id=5
    <HttpGet>
    Public Function ObtenerPorId(id As Integer) As JsonResult
        Try
            Dim prod As CE_Producto = _service.ObtenerProductoPorId(id)
            Return Json(prod, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(Nothing, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    ' POST: Productos/Registrar
    <HttpPost>
    Public Function Registrar(prod As CE_Producto) As JsonResult
        Try
            System.Diagnostics.Trace.WriteLine("Registrar recibido: " & JsonConvert.SerializeObject(prod))
            If Not ModelState.IsValid Then
                Dim errors = String.Join(";", ModelState.Values.SelectMany(Function(v) v.Errors).Select(Function(e) e.ErrorMessage))
                System.Diagnostics.Trace.WriteLine("ModelState inválido en Registrar: " & errors)
                Return Json(New With {.Exito = False, .Mensaje = "ModelState inválido: " & errors})
            End If
            Dim resultado As Boolean = _service.RegistrarProducto(prod)
            Return Json(New With {.Exito = resultado, .Mensaje = If(resultado, "Ok", "No se pudo insertar.")})
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine("Error en Registrar: " & ex.ToString())
            Return Json(New With {.Exito = False, .Mensaje = ex.Message})
        End Try
    End Function

    ' POST: Productos/Modificar
    <HttpPost>
    Public Function Modificar(prod As CE_Producto) As JsonResult
        Try
            System.Diagnostics.Trace.WriteLine("Modificar recibido: " & JsonConvert.SerializeObject(prod))
            If Not ModelState.IsValid Then
                Dim errors = String.Join(";", ModelState.Values.SelectMany(Function(v) v.Errors).Select(Function(e) e.ErrorMessage))
                System.Diagnostics.Trace.WriteLine("ModelState inválido en Modificar: " & errors)
                Return Json(New With {.Exito = False, .Mensaje = "ModelState inválido: " & errors})
            End If
            Dim resultado As Boolean = _service.ModificarProducto(prod)
            Return Json(New With {.Exito = resultado, .Mensaje = If(resultado, "Ok", "No se pudo actualizar.")})
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine("Error en Modificar: " & ex.ToString())
            Return Json(New With {.Exito = False, .Mensaje = ex.Message})
        End Try
    End Function

    ' POST: Productos/Eliminar
    <HttpPost>
    Public Function Eliminar(id As Integer) As JsonResult
        Try
            System.Diagnostics.Trace.WriteLine("Eliminar recibido id: " & id.ToString())
            System.Diagnostics.Trace.WriteLine("Request.Form: " & JsonConvert.SerializeObject(Request.Form.AllKeys.ToDictionary(Function(k) k, Function(k) Request.Form(k))))
            Dim resultado As Boolean = _service.EliminarProducto(id)
            Return Json(New With {.Exito = resultado, .Mensaje = If(resultado, "Ok", "No se pudo eliminar.")})
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine("Error en Eliminar: " & ex.ToString())
            Return Json(New With {.Exito = False, .Mensaje = ex.Message})
        End Try
    End Function
End Class