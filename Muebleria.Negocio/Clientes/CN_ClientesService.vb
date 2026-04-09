Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

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

    Public Function InsertarCliente(cliente As CE_Cliente) As Boolean
        Try
            If String.IsNullOrWhiteSpace(cliente.Nombre) Then
                Throw New ArgumentException("El nombre del cliente es obligatorio.")
            End If
            If String.IsNullOrWhiteSpace(cliente.Correo) Then
                Throw New ArgumentException("El correo del cliente es obligatorio.")
            End If
            Return _clientesData.Insertar(cliente)
        Catch ex As Exception
            Throw New Exception("Error en la lógica de negocio al insertar cliente: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerClientePorEmail(correo As String) As CE_Cliente
        Try
            If String.IsNullOrWhiteSpace(correo) Then
                Throw New ArgumentException("El correo es obligatorio.")
            End If
            Dim cliente As CE_Cliente = _clientesData.ObtenerPorEmail(correo)
            If cliente Is Nothing Then
                Throw New Exception("No se encontró un cliente con ese correo.")
            End If
            Return cliente
        Catch ex As Exception
            Throw New Exception("Error en la lógica de negocio al obtener cliente: " & ex.Message, ex)
        End Try
    End Function

End Class
