Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Sugerencias

        Private ReadOnly _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' INSERTAR SUGERENCIA — direct INSERT
        ' =============================================
        Public Function Insertar(s As CE_Sugerencia) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "INSERT INTO MDA_SUGERENCIAS_CLIENTE (CLIENTE_ID, TIPO, MENSAJE, ESTADO) " &
                    "VALUES (:p_cliente_id, :p_tipo, :p_mensaje, :p_estado)"
                Using cmd As New OracleCommand(sql, con)
                    cmd.CommandType = CommandType.Text
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = s.ClienteId
                    cmd.Parameters.Add("p_tipo", OracleDbType.Varchar2).Value = If(s.Tipo, "SUGERENCIA")
                    cmd.Parameters.Add("p_mensaje", OracleDbType.Varchar2).Value = s.Mensaje
                    cmd.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = If(s.Estado, "PENDIENTE")
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        ' =============================================
        ' OBTENER SUGERENCIAS POR CLIENTE
        ' =============================================
        Public Function ObtenerPorCliente(clienteId As Integer) As List(Of CE_Sugerencia)
            Dim lista As New List(Of CE_Sugerencia)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "SELECT SUGERENCIA_ID, CLIENTE_ID, TIPO, MENSAJE, ESTADO, FECHA " &
                    "FROM MDA_SUGERENCIAS_CLIENTE WHERE CLIENTE_ID = :p_id ORDER BY FECHA DESC"
                Using cmd As New OracleCommand(sql, con)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = clienteId
                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim sg As New CE_Sugerencia()
                            sg.SugerenciaId = Convert.ToInt32(reader("SUGERENCIA_ID"))
                            sg.ClienteId = clienteId
                            sg.Tipo = reader("TIPO").ToString()
                            sg.Mensaje = If(IsDBNull(reader("MENSAJE")), "", reader("MENSAJE").ToString())
                            sg.Estado = If(IsDBNull(reader("ESTADO")), "PENDIENTE", reader("ESTADO").ToString())
                            If Not IsDBNull(reader("FECHA")) Then
                                sg.FechaCreacion = Convert.ToDateTime(reader("FECHA"))
                            End If
                            lista.Add(sg)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

    End Class

End Namespace
