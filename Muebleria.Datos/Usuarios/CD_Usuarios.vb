Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client
Imports System.Security.Cryptography
Imports System.Text

Namespace Muebleria.Datos

    Public Class CD_Usuarios

        Private _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' FUNCION PRIVADA: Calcular SHA256
        ' Convierte password a hash hex lowercase
        ' =============================================
        Private Function SHA256Hash(texto As String) As String
            Using sha As SHA256 = SHA256.Create()
                Dim bytes As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(texto))
                Dim sb As New StringBuilder()
                For Each b As Byte In bytes
                    sb.Append(b.ToString("x2"))
                Next
                Return sb.ToString() ' hex lowercase
            End Using
        End Function

        ' =============================================
        ' LOGIN - VALIDAR CREDENCIALES
        ' =============================================
        Public Function Login(username As String, password As String) As CE_Usuario
            Dim passwordHash As String = SHA256Hash(password)

            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_SEGURIDAD.SP_LOGIN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2, 80).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2, 64).Value = passwordHash
                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_rol", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_cliente_id", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim usuarioId As Integer = Convert.ToInt32(cmd.Parameters("o_usuario_id").Value)
                    Dim rol As String = cmd.Parameters("o_rol").Value.ToString()

                    Dim clienteId As Integer = 0
                    Dim clienteIdRaw As Object = cmd.Parameters("o_cliente_id").Value
                    If clienteIdRaw IsNot Nothing AndAlso Not Convert.IsDBNull(clienteIdRaw) AndAlso clienteIdRaw.ToString() <> "" Then
                        Integer.TryParse(clienteIdRaw.ToString(), clienteId)
                    End If

                    Return New CE_Usuario(usuarioId, username, "", rol, clienteId)
                End Using
            End Using
        End Function

        ' =============================================
        ' REGISTRAR NUEVO USUARIO
        ' =============================================
        Public Function Insertar(username As String, password As String, rol As String, clienteId As Integer) As Boolean
            Dim passwordHash As String = SHA256Hash(password)

            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim procedureName As String = If(rol = "ADMIN",
                    "PKG_SEGURIDAD.SP_CREAR_USUARIO_ADMIN",
                    "PKG_SEGURIDAD.SP_CREAR_USUARIO_CLIENTE")

                Using cmd As New OracleCommand(procedureName, con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True

                    If rol <> "ADMIN" Then
                        cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    End If
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password_hash", OracleDbType.Varchar2).Value = passwordHash
                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("o_usuario_id").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' VERIFICAR SI USERNAME YA EXISTE
        ' =============================================
        Public Function ExisteUsername(username As String) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("SELECT COUNT(*) FROM MDA_USUARIOS WHERE USERNAME = :p_username", con)
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    con.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

    End Class

End Namespace