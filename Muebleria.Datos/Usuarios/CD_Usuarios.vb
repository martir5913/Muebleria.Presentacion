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
        ' UTILIDAD: SHA256 → hex lowercase
        ' Calcula el hash igual que Oracle: STANDARD_HASH(texto, 'SHA256')
        ' =============================================
        Private Function HashSHA256(texto As String) As String
            If String.IsNullOrEmpty(texto) Then Return ""
            Using sha As SHA256 = SHA256.Create()
                Dim bytes() As Byte = sha.ComputeHash(Encoding.UTF8.GetBytes(texto))
                Dim sb As New StringBuilder(64)
                For Each b As Byte In bytes
                    sb.Append(b.ToString("x2"))   ' hex en minúsculas
                Next
                Return sb.ToString()
            End Using
        End Function

        ' =============================================
        ' LOGIN - VALIDAR CREDENCIALES
        ' VB hashea, Oracle compara contra PASSWORD_HASH
        ' =============================================
        Public Function Login(username As String, password As String) As CE_Usuario
            Dim usuario As CE_Usuario = Nothing

            ' Hashear la contraseña ANTES de mandarla
            Dim passwordHash As String = HashSHA256(password)

            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_SEGURIDAD.SP_LOGIN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True

                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = passwordHash

                    ' OUT: usar Decimal en lugar de Int32 para evitar el cast IConvertible
                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Decimal).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_rol", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_cliente_id", OracleDbType.Decimal).Direction = ParameterDirection.Output

                    Try
                        con.Open()
                        cmd.ExecuteNonQuery()

                        ' --- Lectura segura: pasamos por ToString() para evitar el error de IConvertible ---
                        Dim vUsuarioId As Object = cmd.Parameters("o_usuario_id").Value
                        Dim vRol As Object = cmd.Parameters("o_rol").Value
                        Dim vClienteId As Object = cmd.Parameters("o_cliente_id").Value

                        Dim usuarioId As Integer = ParseOracleInt(vUsuarioId)
                        If usuarioId > 0 Then
                            Dim rol As String = ParseOracleString(vRol)
                            Dim clienteId As Integer = ParseOracleInt(vClienteId)

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
        Public Function InsertarAdmin(username As String, password As String) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim commandText As String = If(usuario.Rol = "ADMIN", "PKG_SEGURIDAD.SP_CREAR_USUARIO_ADMIN", "PKG_SEGURIDAD.SP_CREAR_USUARIO_CLIENTE")
                Using cmd As New OracleCommand(commandText, con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    If usuario.Rol = "CLIENTE" Then
                        cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = usuario.ClienteId
        End Function

        Private Function ParseOracleString(valor As Object) As String
            If valor Is Nothing Then Return ""
            Dim s As String = valor.ToString()
            If s.Equals("null", StringComparison.OrdinalIgnoreCase) Then Return ""
            Return s
        End Function

        ' =============================================
        ' REGISTRAR NUEVO USUARIO
        ' Convención: usuario.PasswordHash debe traer la contraseña EN TEXTO PLANO.
        ' Aquí la hasheamos antes de enviarla al SP.
        ' =============================================
        Public Function Insertar(usuario As CE_Usuario) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim commandText As String = If(usuario.Rol = "ADMIN",
                    "PKG_SEGURIDAD.SP_CREAR_USUARIO_ADMIN",
                    "PKG_SEGURIDAD.SP_CREAR_USUARIO_CLIENTE")

                Using cmd As New OracleCommand(commandText, con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = usuario.Username
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = usuario.PasswordHash
                    cmd.Parameters.Add("o_usuario_id", OracleDbType.Int32).Direction = ParameterDirection.Output
                    End If

                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = usuario.Username
                    ' Ojo: el SP espera p_password_hash (no p_password) y debe traer el SHA256
                    cmd.Parameters.Add("p_password_hash", OracleDbType.Varchar2).Value = HashSHA256(usuario.PasswordHash)
                    cmd.ExecuteNonQuery()
                    Return Not IsDBNull(cmd.Parameters("o_usuario_id").Value) AndAlso Convert.ToInt32(cmd.Parameters("o_usuario_id").Value) > 0
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Not IsDBNull(cmd.Parameters("o_usuario_id").Value) _
                        AndAlso Convert.ToInt32(cmd.Parameters("o_usuario_id").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' =============================================
        Public Function ExisteUsername(username As String) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using con As OracleConnection = _conexion.ObtenerConexion()
                    Using cmd As New OracleCommand("SELECT COUNT(1) FROM MDA_USUARIOS WHERE USERNAME = :p_username", con)
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