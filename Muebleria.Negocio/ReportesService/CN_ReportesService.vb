Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_ReportesService

    Private ReadOnly _reportesData As CD_Reportes

    Public Sub New()
        _reportesData = New CD_Reportes()
    End Sub

    Public Function VentasDiarias(fechaIni As Date, fechaFin As Date,
                                   ciudadId As Object) As List(Of CE_VentaDiaria)
        Try
            Return _reportesData.VentasDiarias(fechaIni, fechaFin, ciudadId)
        Catch ex As Exception
            Throw New Exception("Error en reporte de ventas: " & ex.Message, ex)
        End Try
    End Function

    Public Function ProductoMasVendido(fechaIni As Date, fechaFin As Date,
                                       ciudadId As Object) As CE_ProductoVendido
        Try
            Return _reportesData.ProductoMasVendido(fechaIni, fechaFin, ciudadId)
        Catch ex As Exception
            Throw New Exception("Error en reporte de producto más vendido: " & ex.Message, ex)
        End Try
    End Function

    Public Function ComprasPorCliente(clienteId As Integer,
                                      fechaIni As Date, fechaFin As Date) As List(Of CE_CompraCliente)
        Try
            Return _reportesData.ComprasPorCliente(clienteId, fechaIni, fechaFin)
        Catch ex As Exception
            Throw New Exception("Error en reporte de compras: " & ex.Message, ex)
        End Try
    End Function

End Class
