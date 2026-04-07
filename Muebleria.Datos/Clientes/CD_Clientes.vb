Imports System.Configuration
Imports Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Public Class CD_Clientes
    Private ReadOnly _connectionString As String

    Public Sub New()
        _connectionString = ConfigurationManager.ConnectionStrings("OracleDb").ConnectionString
    End Sub

    Public Function ObtenerClientes() As List(Of CE_Cliente)
        Dim clientes As New List(Of CE_Cliente)()

        Try
            Using conn As New OracleConnection(_connectionString)
                Using cmd As New OracleCommand("PKG_CLIENTES.OBTENER_CLIENTES", conn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    ' Parámetro OUT para el cursor
                    Dim pCursor As New OracleParameter("p_cursor", OracleDbType.RefCursor)
                    pCursor.Direction = System.Data.ParameterDirection.Output
                    cmd.Parameters.Add(pCursor)

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    ' Procesar el cursor
                    Dim cursor As OracleRefCursor = CType(pCursor.Value, OracleRefCursor)
                    If cursor IsNot Nothing Then
                        Dim reader As OracleDataReader = cursor.GetDataReader()
                        If reader IsNot Nothing Then
                            While reader.Read()
                                Dim cliente As New CE_Cliente With {
                                    .Id = Convert.ToInt32(reader("ID")),
                                    .Nombre = reader("NOMBRE").ToString(),
                                    .Correo = reader("CORREO").ToString(),
                                    .FechaCreacion = Convert.ToDateTime(reader("FECHA_CREACION"))
                                }
                                clientes.Add(cliente)
                            End While
                            reader.Close()
                            reader.Dispose()
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener clientes: " & ex.Message, ex)
        End Try

        Return clientes
    End Function
End Class
