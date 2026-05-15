Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_OrdenesService
    Private ReadOnly _ordenesData As CD_Ordenes

    Public Sub New()
        _ordenesData = New CD_Ordenes()
    End Sub

    ' ─── EFECTUAR COMPRA COMPLETA ─────────────────────────────────
    ' Llama a PKG_COMPRAS.SP_CONFIRMAR_COMPRA_DESDE_CARRITO
    Public Function EfectuarCompra(clienteId As Integer, carritoId As Integer,
                                   formaPago As String) As Integer
        If clienteId <= 0 Then Throw New ArgumentException("Cliente inválido.")
        If carritoId <= 0 Then Throw New ArgumentException("Carrito inválido.")
        If String.IsNullOrWhiteSpace(formaPago) Then Throw New ArgumentException("Forma de pago requerida.")

        Try
            Return _ordenesData.EfectuarCompra(clienteId, carritoId, formaPago)
        Catch ex As Exception
            Throw New Exception("Error al efectuar compra: " & ex.Message, ex)
        End Try
    End Function

    ' ─── OBTENER DETALLE DE ORDEN ────────────────────────────────
    Public Function ObtenerDetalleOrden(ordenId As Integer) As List(Of CE_OrdenDetalle)
        If ordenId <= 0 Then Throw New ArgumentException("ID de orden inválido.")
        Try
            Return _ordenesData.ObtenerDetalle(ordenId)
        Catch ex As Exception
            Throw New Exception("Error al obtener detalle de orden: " & ex.Message, ex)
        End Try
    End Function

    ' ─── HISTORIAL DEL CLIENTE ───────────────────────────────────
    Public Function ObtenerHistorialCliente(clienteId As Integer) As List(Of CE_Orden)
        If clienteId <= 0 Then Throw New ArgumentException("Cliente inválido.")
        Try
            Return _ordenesData.ObtenerPorCliente(clienteId)
        Catch ex As Exception
            Throw New Exception("Error al obtener historial: " & ex.Message, ex)
        End Try
    End Function

End Class
