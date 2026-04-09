Imports System.Configuration
Imports Oracle.ManagedDataAccess.Client

Public Class CD_Conexion
    Private ReadOnly _connectionString As String

    Public Sub New()
        _connectionString = ConfigurationManager.ConnectionStrings("OracleDb").ConnectionString
    End Sub

    Public Function ObtenerConexion() As OracleConnection
        Return New OracleConnection(_connectionString)
    End Function

    Public Function ProbarConexion() As Boolean
        Try
            Using conn As New OracleConnection(_connectionString)
                conn.Open()
                Return True
            End Using
        Catch ex As Exception
            Throw New Exception("Error al conectar a Oracle: " & ex.Message)
        End Try
    End Function
End Class
