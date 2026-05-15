Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_BodegasService

    Private ReadOnly _bodegasData As CD_Bodegas

    Public Sub New()
        _bodegasData = New CD_Bodegas()
    End Sub

    Public Function ObtenerBodegas() As List(Of CE_Bodega)
        Try
            Return _bodegasData.ObtenerTodas()
        Catch ex As Exception
            Throw New Exception("Error al obtener bodegas: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerStock(bodegaId As Integer) As List(Of CE_StockBodega)
        Try
            Return _bodegasData.ObtenerStock(bodegaId)
        Catch ex As Exception
            Throw New Exception("Error al obtener stock: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerBodegaPorId(bodegaId As Integer) As CE_Bodega
        Try
            Return _bodegasData.ObtenerPorId(bodegaId)
        Catch ex As Exception
            Throw New Exception("Error al obtener bodega: " & ex.Message, ex)
        End Try
    End Function

    Public Function CrearBodega(b As CE_Bodega) As Integer
        If String.IsNullOrWhiteSpace(b.Nombre) Then
            Throw New ArgumentException("El nombre de la bodega es obligatorio.")
        End If
        Try
            Return _bodegasData.Insertar(b)
        Catch ex As Exception
            Throw New Exception("Error al crear bodega: " & ex.Message, ex)
        End Try
    End Function

    Public Function ActualizarBodega(b As CE_Bodega) As Boolean
        If String.IsNullOrWhiteSpace(b.Nombre) Then
            Throw New ArgumentException("El nombre de la bodega es obligatorio.")
        End If
        Try
            Return _bodegasData.Actualizar(b)
        Catch ex As Exception
            Throw New Exception("Error al actualizar bodega: " & ex.Message, ex)
        End Try
    End Function

    Public Function EliminarBodega(bodegaId As Integer) As Boolean
        Try
            Return _bodegasData.Eliminar(bodegaId)
        Catch ex As Exception
            Throw New Exception("Error al desactivar bodega: " & ex.Message, ex)
        End Try
    End Function

    Public Function ActualizarStock(productoId As Integer, bodegaId As Integer,
                                    cantidad As Integer) As Boolean
        Try
            Return _bodegasData.ActualizarStock(productoId, bodegaId, cantidad)
        Catch ex As Exception
            Throw New Exception("Error al actualizar stock: " & ex.Message, ex)
        End Try
    End Function

End Class
