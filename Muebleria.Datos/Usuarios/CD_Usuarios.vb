Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Diagnostics

Namespace Muebleria.Datos

    ''' <summary>
    ''' Clase de acceso a datos para gestión de usuarios.
    ''' Proporciona operaciones de autenticación, creación y validación de usuarios.
    ''' </summary>
    Public Class CD_Usuarios

        ' ==================== CONSTANTES ====================
        Private Const SP_LOGIN As String = "PKG_SEGURIDAD.SP_LOGIN"
        Private Const SP_CREAR_USUARIO_ADMIN As String = "PKG_SEGURIDAD.SP_CREAR_USUARIO_ADMIN"
        Private Const SP_CREAR_USUARIO_CLIENTE As String = "PKG_SEGURIDAD.SP_CREAR_USUARIO_CLIENTE"
        Private Const QUERY_EXISTE_USERNAME As String = "SELECT COUNT(1) FROM MDA_USUARIOS WHERE USERNAME = :p_username"
        Private Const ROL_ADMIN As String = "ADMIN"
        Private Const ROL_CLIENTE As String = "CLIENTE"
        Private Const PARAM_USERNAME As String = "p_username"
        Private Const PARAM_PASSWORD As String = "p_password"
        Private Const PARAM_USUARIO_ID As String = "o_usuario_id"
        Private Const PARAM_ROL As String = "o_rol"
        Private Const PARAM_CLIENTE_ID As String = "o_cliente_id"
        Private Const PARAM_CLIENTE_ID_INPUT As String = "p_cliente_id"

        ' ==================== PROPIEDADES ====================
        Private ReadOnly _conexion As CD_Conexion

        ' ==================== CONSTRUCTOR ====================
        ''' <summary>
        ''' Inicializa una nueva instancia de CD_Usuarios.
        ''' </summary>
        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ''' <summary>
        ''' Inicializa una nueva instancia de CD_Usuarios con inyección de dependencias.
        ''' </summary>
        ''' <param name="conexion">La instancia de CD_Conexion para utilizar.</param>
        Public Sub New(conexion As CD_Conexion)
            If conexion Is Nothing Then
                Throw New ArgumentNullException(NameOf(conexion))
            End If
            _conexion = conexion
        End Sub

        ' ==================== MÉTODOS PÚBLICOS ====================
        ''' <summary>
        ''' Autentica un usuario con username y password.
        ''' </summary>
        ''' <param name="username">El nombre de usuario.</param>
        ''' <param name="password">La contraseña del usuario.</param>
        ''' <returns>Objeto CE_Usuario si la autenticación es exitosa; Nothing en caso contrario.</returns>
        ''' <exception cref="ArgumentNullException">Si username o password son Nothing o vacíos.</exception>
        Public Function Login(username As String, password As String) As CE_Usuario
            ValidarParametroNoVacio(username, NameOf(username))
            ValidarParametroNoVacio(password, NameOf(password))

            Dim usuario As CE_Usuario = Nothing

            Try
                Using con As OracleConnection = _conexion.ObtenerConexion()
                    Using cmd As New OracleCommand(SP_LOGIN, con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.BindByName = True

                        ' Agregar parámetros de entrada
                        cmd.Parameters.Add(PARAM_USERNAME, OracleDbType.Varchar2).Value = username
                        cmd.Parameters.Add(PARAM_PASSWORD, OracleDbType.Varchar2).Value = password

                        ' Agregar parámetros de salida
                        cmd.Parameters.Add(PARAM_USUARIO_ID, OracleDbType.Int32).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(PARAM_ROL, OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output
                        cmd.Parameters.Add(PARAM_CLIENTE_ID, OracleDbType.Int32).Direction = ParameterDirection.Output

                        con.Open()
                        cmd.ExecuteNonQuery()

                        Dim usuarioId As Integer = ObtenerEnteroDesdeParametro(cmd.Parameters(PARAM_USUARIO_ID))
                        If usuarioId > 0 Then
                            Dim rol As String = ObtenerStringDesdeParametro(cmd.Parameters(PARAM_ROL))
                            Dim clienteId As Integer = ObtenerEnteroDesdeParametro(cmd.Parameters(PARAM_CLIENTE_ID))
                            usuario = New CE_Usuario(usuarioId, username, "", rol, clienteId)
                            EscribirLogInfo($"Login exitoso para usuario: {username}")
                        Else
                            EscribirLogWarning($"Intento de login fallido para usuario: {username}")
                        End If
                    End Using
                End Using
            Catch ex As OracleException
                EscribirLogError($"Error de base de datos en Login para usuario {username}: {ex.Message}", ex)
                Throw New ApplicationException($"Error al autenticar usuario {username}. Por favor, intente más tarde.", ex)
            Catch ex As Exception
                EscribirLogError($"Error inesperado en Login: {ex.Message}", ex)
                Throw
            End Try

            Return usuario
        End Function

        ''' <summary>
        ''' Inserta un nuevo usuario en la base de datos.
        ''' </summary>
        ''' <param name="usuario">El objeto CE_Usuario con los datos a insertar.</param>
        ''' <returns>True si la inserción fue exitosa; False en caso contrario.</returns>
        ''' <exception cref="ArgumentNullException">Si usuario es Nothing.</exception>
        Public Function Insertar(usuario As CE_Usuario) As Boolean
            If usuario Is Nothing Then
                Throw New ArgumentNullException(NameOf(usuario))
            End If
            ValidarParametroNoVacio(usuario.Username, NameOf(usuario.Username))
            ValidarParametroNoVacio(usuario.PasswordHash, NameOf(usuario.PasswordHash))

            Try
                Using con As OracleConnection = _conexion.ObtenerConexion()
                    Dim commandText As String = DeterminarStoredProcedureCrearUsuario(usuario.Rol)
                    Using cmd As New OracleCommand(commandText, con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.BindByName = True

                        ' Agregar parámetro cliente_id si es cliente
                        If String.Equals(usuario.Rol, ROL_CLIENTE, StringComparison.OrdinalIgnoreCase) Then
                            cmd.Parameters.Add(PARAM_CLIENTE_ID_INPUT, OracleDbType.Int32).Value = usuario.ClienteId
                        End If

                        cmd.Parameters.Add(PARAM_USERNAME, OracleDbType.Varchar2).Value = usuario.Username
                        cmd.Parameters.Add(PARAM_PASSWORD, OracleDbType.Varchar2).Value = usuario.PasswordHash
                        cmd.Parameters.Add(PARAM_USUARIO_ID, OracleDbType.Int32).Direction = ParameterDirection.Output

                        con.Open()
                        cmd.ExecuteNonQuery()

                        Dim nuevoId As Integer = ObtenerEnteroDesdeParametro(cmd.Parameters(PARAM_USUARIO_ID))
                        Dim exito As Boolean = nuevoId > 0

                        If exito Then
                            EscribirLogInfo($"Usuario creado exitosamente: {usuario.Username} (ID: {nuevoId}, Rol: {usuario.Rol})")
                        Else
                            EscribirLogWarning($"Falló la creación del usuario: {usuario.Username}")
                        End If

                        Return exito
                    End Using
                End Using
            Catch ex As OracleException
                EscribirLogError($"Error de base de datos al insertar usuario {usuario.Username}: {ex.Message}", ex)
                Throw New ApplicationException($"Error al crear usuario {usuario.Username}. Por favor, intente más tarde.", ex)
            Catch ex As Exception
                EscribirLogError($"Error inesperado al insertar usuario: {ex.Message}", ex)
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Verifica si existe un usuario con el username especificado.
        ''' </summary>
        ''' <param name="username">El nombre de usuario a verificar.</param>
        ''' <returns>True si el username existe; False en caso contrario.</returns>
        ''' <exception cref="ArgumentNullException">Si username es Nothing o vacío.</exception>
        Public Function ExisteUsername(username As String) As Boolean
            ValidarParametroNoVacio(username, NameOf(username))

            Try
                Using con As OracleConnection = _conexion.ObtenerConexion()
                    Using cmd As New OracleCommand(QUERY_EXISTE_USERNAME, con)
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.Add(PARAM_USERNAME, OracleDbType.Varchar2).Value = username

                        con.Open()
                        Dim result = cmd.ExecuteScalar()

                        If result Is Nothing OrElse IsDBNull(result) Then
                            Return False
                        End If

                        Dim count As Integer = ObtenerEnteroDesdeParametro(result)
                        Return count > 0
                    End Using
                End Using
            Catch ex As OracleException
                EscribirLogError($"Error de base de datos al verificar username {username}: {ex.Message}", ex)
                Throw New ApplicationException($"Error al verificar disponibilidad del usuario. Por favor, intente más tarde.", ex)
            Catch ex As Exception
                EscribirLogError($"Error inesperado al verificar username: {ex.Message}", ex)
                Throw
            End Try
        End Function

        ' ==================== MÉTODOS PRIVADOS ====================
        ''' <summary>
        ''' Valida que un parámetro string no sea Nothing ni vacío.
        ''' </summary>
        ''' <param name="valor">El valor a validar.</param>
        ''' <param name="nombreParametro">El nombre del parámetro para el mensaje de error.</param>
        ''' <exception cref="ArgumentNullException">Si el valor es Nothing o vacío.</exception>
        Private Sub ValidarParametroNoVacio(valor As String, nombreParametro As String)
            If String.IsNullOrWhiteSpace(valor) Then
                Throw New ArgumentNullException(nombreParametro, $"{nombreParametro} no puede ser nulo o vacío.")
            End If
        End Sub

        ''' <summary>
        ''' Determina cuál stored procedure usar según el rol del usuario.
        ''' </summary>
        ''' <param name="rol">El rol del usuario.</param>
        ''' <returns>El nombre del stored procedure correspondiente.</returns>
        Private Function DeterminarStoredProcedureCrearUsuario(rol As String) As String
            Return If(String.Equals(rol, ROL_ADMIN, StringComparison.OrdinalIgnoreCase),
                      SP_CREAR_USUARIO_ADMIN,
                      SP_CREAR_USUARIO_CLIENTE)
        End Function

        ''' <summary>
        ''' Extrae un valor entero desde un parámetro de Oracle.
        ''' Maneja conversiones desde OracleDecimal y otros tipos numéricos.
        ''' </summary>
        ''' <param name="param">El parámetro de Oracle.</param>
        ''' <returns>El valor entero; 0 si el parámetro es Nothing o NULL.</returns>
        Private Function ObtenerEnteroDesdeParametro(param As OracleParameter) As Integer
            If param Is Nothing OrElse param.Value Is Nothing OrElse IsDBNull(param.Value) Then
                Return 0
            End If

            If TypeOf param.Value Is OracleDecimal Then
                Return CInt(DirectCast(param.Value, OracleDecimal).Value)
            End If

            Try
                Return Convert.ToInt32(param.Value)
            Catch ex As InvalidCastException
                EscribirLogWarning($"No se pudo convertir el valor a entero: {param.Value}")
                Return 0
            End Try
        End Function

        ''' <summary>
        ''' Extrae un valor entero desde un objeto de resultado de ExecuteScalar.
        ''' </summary>
        ''' <param name="valor">El valor del resultado.</param>
        ''' <returns>El valor entero; 0 si es Nothing o NULL.</returns>
        Private Function ObtenerEnteroDesdeParametro(valor As Object) As Integer
            If valor Is Nothing OrElse IsDBNull(valor) Then
                Return 0
            End If

            If TypeOf valor Is OracleDecimal Then
                Return CInt(DirectCast(valor, OracleDecimal).Value)
            End If

            Try
                Return Convert.ToInt32(valor)
            Catch ex As InvalidCastException
                EscribirLogWarning($"No se pudo convertir el valor a entero: {valor}")
                Return 0
            End Try
        End Function

        ''' <summary>
        ''' Extrae un valor string desde un parámetro de Oracle.
        ''' </summary>
        ''' <param name="param">El parámetro de Oracle.</param>
        ''' <returns>El valor string; cadena vacía si es Nothing o NULL.</returns>
        Private Function ObtenerStringDesdeParametro(param As OracleParameter) As String
            If param Is Nothing OrElse param.Value Is Nothing OrElse IsDBNull(param.Value) Then
                Return String.Empty
            End If

            Return param.Value.ToString()
        End Function

        ''' <summary>
        ''' Escribe un mensaje informativo en el Debug Output.
        ''' </summary>
        ''' <param name="mensaje">El mensaje a escribir.</param>
        Private Sub EscribirLogInfo(mensaje As String)
            EscribirLog("[INFO]", mensaje)
        End Sub

        ''' <summary>
        ''' Escribe un mensaje de advertencia en el Debug Output.
        ''' </summary>
        ''' <param name="mensaje">El mensaje a escribir.</param>
        Private Sub EscribirLogWarning(mensaje As String)
            EscribirLog("[WARNING]", mensaje)
        End Sub

        ''' <summary>
        ''' Escribe un mensaje de error en el Debug Output.
        ''' </summary>
        ''' <param name="mensaje">El mensaje a escribir.</param>
        ''' <param name="ex">La excepción asociada (opcional).</param>
        Private Sub EscribirLogError(mensaje As String, Optional ex As Exception = Nothing)
            EscribirLog("[ERROR]", mensaje)
            If ex IsNot Nothing Then
                EscribirLog("[ERROR]", $"Exception: {ex.GetType().Name} - {ex.Message}")
                If ex.InnerException IsNot Nothing Then
                    EscribirLog("[ERROR]", $"Inner Exception: {ex.InnerException.Message}")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Escribe el mensaje formateado en el Debug Output.
        ''' </summary>
        ''' <param name="nivel">El nivel de log.</param>
        ''' <param name="mensaje">El mensaje a escribir.</param>
        Private Sub EscribirLog(nivel As String, mensaje As String)
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
            Dim logLine As String = $"{timestamp} {nivel} [CD_Usuarios] {mensaje}"
            Debug.WriteLine(logLine)
        End Sub

    End Class

End Namespace