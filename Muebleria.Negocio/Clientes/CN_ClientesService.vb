Imports Muebleria.Datos
Imports Muebleria.Datos.Muebleria.Datos
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class CN_ClientesService
    Private ReadOnly _clientesData As CD_Clientes
    Private ReadOnly _sugerenciasData As CD_Sugerencias

    Public Sub New()
        _clientesData = New CD_Clientes()
        _sugerenciasData = New CD_Sugerencias()
    End Sub

    Public Function ObtenerTodosLosClientes() As List(Of CE_Cliente)
        Try
            Return _clientesData.ObtenerClientes()
        Catch ex As Exception
            Throw New Exception("Error al obtener clientes: " & ex.Message, ex)
        End Try
    End Function

    Public Function ObtenerClientePorId(clienteId As Integer) As CE_Cliente
        If clienteId <= 0 Then Throw New ArgumentException("ID de cliente inválido.")
        Try
            Dim c As CE_Cliente = _clientesData.ObtenerPorId(clienteId)
            If c Is Nothing Then Throw New Exception("No se encontró el cliente.")
            Return c
        Catch ex As Exception
            Throw New Exception("Error al obtener cliente: " & ex.Message, ex)
        End Try
    End Function

    Public Function RegistrarCliente(cliente As CE_Cliente) As Integer
        If String.IsNullOrWhiteSpace(cliente.Nombres) Then
            Throw New ArgumentException("El nombre del cliente es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(cliente.Correo) Then
            Throw New ArgumentException("El email del cliente es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(cliente.NumDocumento) Then
            Throw New ArgumentException("El número de documento es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(cliente.TelResidencia) Then
            Throw New ArgumentException("El teléfono de residencia es obligatorio.")
        End If
        Try
            Return _clientesData.Insertar(cliente)
        Catch ex As Exception
            Throw New Exception("Error al registrar cliente: " & ex.Message, ex)
        End Try
    End Function

    Public Function ActualizarCliente(cliente As CE_Cliente) As Boolean
        If cliente.ClienteId <= 0 Then Throw New ArgumentException("ID de cliente inválido.")
        Try
            Return _clientesData.Actualizar(cliente)
        Catch ex As Exception
            Throw New Exception("Error al actualizar cliente: " & ex.Message, ex)
        End Try
    End Function

    Public Function GuardarSugerencia(sugerencia As CE_Sugerencia) As Boolean
        If sugerencia.ClienteId <= 0 Then Throw New ArgumentException("ID de cliente inválido.")
        If String.IsNullOrWhiteSpace(sugerencia.Mensaje) Then
            Throw New ArgumentException("El mensaje de la sugerencia es obligatorio.")
        End If
        Try
            Return _sugerenciasData.Insertar(sugerencia)
        Catch ex As Exception
            Throw New Exception("Error al guardar sugerencia: " & ex.Message, ex)
        End Try
    End Function

End Class
