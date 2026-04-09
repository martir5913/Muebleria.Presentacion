Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Usuarios

        Private _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' LOGIN - VALIDAR CREDENCIALES
        ' =============================================
        Public Function Login(username As String, password As String) As CE_Usuario
            Dim usuario As CE_Usuario = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_USUARIOS.LOGIN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password
                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_rol", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_cliente_id", OracleDbType.Int32).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim usuarioId As Integer = If(IsDBNull(reader("USUARIO_ID")), 0, Convert.ToInt32(reader("USUARIO_ID")))
                            Dim username_val As String = If(IsDBNull(reader("USERNAME")), "", reader("USERNAME").ToString())
                            Dim rol As String = If(IsDBNull(reader("ROL")), "", reader("ROL").ToString())
                            Dim clienteId As Integer = If(IsDBNull(reader("CLIENTE_ID")), 0, Convert.ToInt32(reader("CLIENTE_ID")))

                            usuario = New CE_Usuario(
                                usuarioId,
                                username_val,
                                "",
                                rol,
                                clienteId
                            )
                        End If
                    End Using
                End Using
            End Using
            Return usuario
        End Function

        ' =============================================
        ' REGISTRAR NUEVO USUARIO
        ' =============================================
        Public Function Insertar(usuario As CE_Usuario) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_USUARIOS.SP_INSERTAR", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = usuario.Username
                    cmd.Parameters.Add("p_password_hash", OracleDbType.Varchar2).Value = usuario.PasswordHash
                    cmd.Parameters.Add("p_rol", OracleDbType.Varchar2).Value = usuario.Rol
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = usuario.ClienteId
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' VERIFICAR SI USERNAME YA EXISTE
        ' =============================================
        Public Function ExisteUsername(username As String) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_USUARIOS.FN_EXISTE_USERNAME", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.ReturnValue
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) = 1
                End Using
            End Using
        End Function

    End Class

End Namespace