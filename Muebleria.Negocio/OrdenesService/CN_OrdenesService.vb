Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_OrdenesService
    Private ReadOnly _ordenesData As CD_Ordenes
    Private ReadOnly _carritoData As CD_Carrito

    Public Sub New()
        _ordenesData = New CD_Ordenes()
        _carritoData = New CD_Carrito()
    End Sub

    ' ─── CREAR ORDEN DESDE CARRITO ───────────────────────────────
    Public Function CrearOrden(clienteId As Integer, carritoId As Integer) As Integer
        Try
            If clienteId <= 0 Then
                Throw New ArgumentException("El cliente no es válido.")
            End If
            If carritoId <= 0 Then
                Throw New ArgumentException("El carrito no es válido.")
            End If

            ' Verificar que el carrito tenga items
            Dim items As List(Of CE_CarritoItem) = _carritoData.ObtenerItems(carritoId)
            If items Is Nothing OrElse items.Count = 0 Then
                Throw New Exception("El carrito está vacío. Agregue productos antes de comprar.")
            End If

            ' Calcular total del carrito para pasarlo a la capa de datos
            Dim total As Decimal = items.Sum(Function(i) i.PrecioUnitario * i.Cantidad)

            Return _ordenesData.CrearOrden(clienteId, total)
        Catch ex As Exception
            Throw New Exception("Error en negocio al crear orden: " & ex.Message, ex)
        End Try
    End Function

    ' ─── OBTENER HISTORIAL DE COMPRAS ────────────────────────────
    Public Function ObtenerHistorialCliente(clienteId As Integer) As List(Of CE_Orden)
        Try
            If clienteId <= 0 Then
                Throw New ArgumentException("El ID del cliente no es válido.")
            End If
            Return _ordenesData.ObtenerPorCliente(clienteId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al obtener historial: " & ex.Message, ex)
        End Try
    End Function

    ' ─── OBTENER DETALLE DE ORDEN ────────────────────────────────
    Public Function ObtenerDetalleOrden(ordenId As Integer) As List(Of CE_OrdenDetalle)
        Try
            If ordenId <= 0 Then
                Throw New ArgumentException("El ID de la orden no es válido.")
            End If
            Return _ordenesData.ObtenerDetalle(ordenId)
        Catch ex As Exception
            Throw New Exception("Error en negocio al obtener detalle: " & ex.Message, ex)
        End Try
    End Function

    ' ─── REGISTRAR PAGO ──────────────────────────────────────────
    Public Function RegistrarPago(ordenId As Integer, monto As Decimal,
                                  formaPago As String) As Boolean
        Try
            If ordenId <= 0 Then
                Throw New ArgumentException("La orden no es válida.")
            End If
            If monto <= 0 Then
                Throw New ArgumentException("El monto del pago debe ser mayor a cero.")
            End If
            If String.IsNullOrWhiteSpace(formaPago) Then
                Throw New ArgumentException("La forma de pago es obligatoria.")
            End If

            Dim pago As New CE_Pago With {
                .ordenId = ordenId,
                .monto = monto,
                .formaPago = formaPago,
                .Fecha = DateTime.Now
            }

            Return _ordenesData.RegistrarPago(pago)
        Catch ex As Exception
            Throw New Exception("Error en negocio al registrar pago: " & ex.Message, ex)
        End Try
    End Function

    ' ─── PROCESO COMPLETO DE COMPRA ──────────────────────────────
    ' Crea la orden + registra el pago en un solo llamado desde el formulario
    Public Function EfectuarCompra(clienteId As Integer, carritoId As Integer,
                                   formaPago As String) As Integer
        Try
            ' 1. Crear la orden
            Dim ordenId As Integer = CrearOrden(clienteId, carritoId)
            If ordenId <= 0 Then
                Throw New Exception("No se pudo generar la orden.")
            End If

            ' 2. Obtener el total sumando los detalles
            Dim detalles As List(Of CE_OrdenDetalle) = _ordenesData.ObtenerDetalle(ordenId)
            Dim total As Decimal = detalles.Sum(Function(d) d.Subtotal)

            ' 3. Registrar el pago
            RegistrarPago(ordenId, total, formaPago)

            Return ordenId
        Catch ex As Exception
            Throw New Exception("Error en negocio al efectuar compra: " & ex.Message, ex)
        End Try
    End Function

End Class