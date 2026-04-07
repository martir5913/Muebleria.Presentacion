Imports Muebleria.Datos
Imports Muebleria.Entidades

Public Class CN_ClientesService
    Private ReadOnly _clientesData As CD_Clientes

    Public Sub New()
        _clientesData = New CD_Clientes()
    End Sub

    Public Function ObtenerTodosLosClientes() As List(Of CE_Cliente)
        Try
            Return _clientesData.ObtenerClientes()
        Catch ex As Exception
            Throw New Exception("Error en la lógica de negocio al obtener clientes: " & ex.Message, ex)
        End Try
    End Function
End Class
