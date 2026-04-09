Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Namespace Muebleria.Datos

Public Class CD_Clientes
    Private ReadOnly _conexion As CD_Conexion

    Public Sub New()
        _conexion = New CD_Conexion()
    End Sub

    Public Function ObtenerClientes() As List(Of CE_Cliente)
        Dim clientes As New List(Of CE_Cliente)()

        Try
            Using conn As OracleConnection = _conexion.ObtenerConexion()
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

    ' =============================================
    ' INSERTAR CLIENTE
    ' =============================================
    Public Function Insertar(cliente As CE_Cliente) As Boolean
        Using con As OracleConnection = _conexion.ObtenerConexion()
            Using cmd As New OracleCommand("PKG_CLIENTES.SP_INSERTAR", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = cliente.Nombre
                cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = cliente.Correo
                cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                con.Open()
                cmd.ExecuteNonQuery()
                Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
            End Using
        End Using
    End Function

    ' =============================================
    ' OBTENER CLIENTE POR CORREO
    ' =============================================
    Public Function ObtenerPorEmail(correo As String) As CE_Cliente
        Dim cliente As CE_Cliente = Nothing
        Using con As OracleConnection = _conexion.ObtenerConexion()
            Using cmd As New OracleCommand("PKG_CLIENTES.SP_OBTENER_POR_EMAIL", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("p_correo", OracleDbType.Varchar2).Value = correo
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                con.Open()
                Using reader As OracleDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        cliente = New CE_Cliente With {
                            .Id = Convert.ToInt32(reader("ID")),
                            .Nombre = reader("NOMBRE").ToString(),
                            .Correo = reader("CORREO").ToString(),
                            .FechaCreacion = Convert.ToDateTime(reader("FECHA_CREACION"))
                        }
                    End If
                End Using
            End Using
        End Using
        Return cliente
    End Function

End Class

End Namespace
