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
        ' LOGIN — Oracle genera SALT+SHA256
        ' =============================================
        Public Function Login(username As String, password As String) As CE_Usuario
            Dim usuario As CE_Usuario = Nothing

            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_SEGURIDAD.SP_LOGIN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password

                    Dim pUsuarioId = cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32)
                    pUsuarioId.Direction = ParameterDirection.Output

                    Dim pRol = cmd.Parameters.Add("o_rol", OracleDbType.Varchar2, 20)
                    pRol.Direction = ParameterDirection.Output

                    Dim pClienteId = cmd.Parameters.Add("o_cliente_id", OracleDbType.Int32)
                    pClienteId.Direction = ParameterDirection.Output

                    Try
                        con.Open()
                        cmd.ExecuteNonQuery()

                        Dim usuarioId As Integer = 0
                        Dim rawUid = pUsuarioId.Value.ToString()
                        If rawUid <> "" AndAlso rawUid <> "null" Then
                            usuarioId = Convert.ToInt32(rawUid)
                        End If

                        Dim rol As String = pRol.Value.ToString()

                        Dim clienteId As Integer = 0
                        Dim rawCid = pClienteId.Value.ToString()
                        If rawCid <> "" AndAlso rawCid <> "null" Then
                            clienteId = Convert.ToInt32(rawCid)
                        End If

                        If usuarioId > 0 Then
                            usuario = New CE_Usuario(usuarioId, username, "", rol, clienteId)
                        End If
                    Catch ex As OracleException When ex.Number = 20041
                        ' Credenciales inválidas — Oracle lo señala con RAISE_APPLICATION_ERROR
                        usuario = Nothing
                    End Try
                End Using
            End Using
            Return usuario
        End Function

        ' =============================================
        ' REGISTRAR USUARIO CLIENTE
        ' =============================================
        Public Function InsertarCliente(clienteId As Integer, username As String,
                                        password As String) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_SEGURIDAD.SP_CREAR_USUARIO_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password

                    Dim pOut = cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32)
                    pOut.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(pOut.Value.ToString())
                End Using
            End Using
        End Function

        ' =============================================
        ' REGISTRAR USUARIO ADMIN
        ' =============================================
        Public Function InsertarAdmin(username As String, password As String) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_SEGURIDAD.SP_CREAR_USUARIO_ADMIN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password

                    Dim pOut = cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32)
                    pOut.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(pOut.Value.ToString())
                End Using
            End Using
        End Function

        ' =============================================
        ' VERIFICAR SI USERNAME YA EXISTE
        ' =============================================
        Public Function ExisteUsername(username As String) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand(
                    "SELECT COUNT(1) FROM MDA_USUARIOS WHERE USERNAME = :p_username", con)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    con.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

    End Class

End Namespace
