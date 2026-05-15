Imports System.Data
Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types ' Esencial para manejar OracleDecimal

Namespace Muebleria.Datos

    Public Class CD_Usuarios

        Private _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' LOGIN - VALIDAR CREDENCIALES (CORREGIDO PARA NULLS)
        ' =============================================
        Public Function Login(username As String, password As String) As CE_Usuario
            Dim usuario As CE_Usuario = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_SEGURIDAD.SP_LOGIN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password

                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Decimal).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_rol", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_cliente_id", OracleDbType.Decimal).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim usuarioId As Integer = 0

                    ' Validación segura para o_usuario_id
                    If Not IsDBNull(cmd.Parameters("o_usuario_id").Value) Then
                        Dim oraUserDecimal As OracleDecimal = CType(cmd.Parameters("o_usuario_id").Value, OracleDecimal)
                        ' Validamos que el objeto OracleDecimal interno no marque que es nulo
                        If Not oraUserDecimal.IsNull Then
                            usuarioId = oraUserDecimal.ToInt32()
                        End If
                    End If

                    If usuarioId > 0 Then
                        Dim rol As String = If(IsDBNull(cmd.Parameters("o_rol").Value), "", cmd.Parameters("o_rol").Value.ToString())
                        Dim clienteId As Integer = 0

                        ' === AQUÍ ESTABA EL PROBLEMA: Validación ultra segura para o_cliente_id ===
                        If Not IsDBNull(cmd.Parameters("o_cliente_id").Value) Then
                            Dim oraClientDecimal As OracleDecimal = CType(cmd.Parameters("o_cliente_id").Value, OracleDecimal)
                            ' Si el OracleDecimal NO es nulo, extraemos su entero. Si es nulo (como en los ADMIN), se queda en 0
                            If Not oraClientDecimal.IsNull Then
                                clienteId = oraClientDecimal.ToInt32()
                            End If
                        End If

                        usuario = New CE_Usuario(
                            usuarioId,
                            username,
                            "",
                            rol,
                            clienteId
                        )
                    End If
                End Using
            End Using
            Return usuario
        End Function

        ' =============================================
        ' REGISTRAR NUEVO USUARIO
        ' =============================================
        Public Function Insertar(usuario As CE_Usuario) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim commandText As String = If(usuario.Rol = "ADMIN", "PKG_SEGURIDAD.SP_CREAR_USUARIO_ADMIN", "PKG_SEGURIDAD.SP_CREAR_USUARIO_CLIENTE")
                Using cmd As New OracleCommand(commandText, con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True

                    If usuario.Rol = "CLIENTE" Then
                        cmd.Parameters.Add("p_cliente_id", OracleDbType.Decimal).Value = usuario.ClienteId
                    End If

                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = usuario.Username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = usuario.PasswordHash
                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Decimal).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    If Not IsDBNull(cmd.Parameters("o_usuario_id").Value) Then
                        Dim oraInsertDecimal As OracleDecimal = CType(cmd.Parameters("o_usuario_id").Value, OracleDecimal)
                        Return oraInsertDecimal.ToInt32() > 0
                    End If

                    Return False
                End Using
            End Using
        End Function

        ' =============================================
        ' VERIFICAR SI USERNAME YA EXISTE
        ' =============================================
        Public Function ExisteUsername(username As String) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                ' CORREGIDO: Se agregó la palabra clave "Using" que faltaba aquí
                Using cmd As New OracleCommand("SELECT COUNT(1) FROM MDA_USUARIOS WHERE USERNAME = :p_username", con)
                    cmd.CommandType = CommandType.Text
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username

                    con.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

    End Class

End Namespace