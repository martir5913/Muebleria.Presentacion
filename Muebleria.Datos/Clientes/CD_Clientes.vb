Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Clientes
        Private ReadOnly _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' BUSCAR CLIENTES (admin - incluye inactivos)
        ' =============================================
        Public Function ObtenerClientes() As List(Of CE_Cliente)
            Dim clientes As New List(Of CE_Cliente)()
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CLIENTES.OBTENER_CLIENTES", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            clientes.Add(MapearClienteLista(reader))
                        End While
                    End Using
                End Using
            End Using
            Return clientes
        End Function

        ' =============================================
        ' OBTENER CLIENTE POR ID (detalle completo)
        ' =============================================
        Public Function ObtenerPorId(clienteId As Integer) As CE_Cliente
            Dim cliente As CE_Cliente = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CLIENTES.SP_OBTENER_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        If reader.Read() Then
                            cliente = MapearClienteDetalle(reader)
                        End If
                    End Using
                End Using
            End Using
            Return cliente
        End Function

        ' =============================================
        ' CREAR CLIENTE — PKG_CLIENTES.SP_CREAR_CLIENTE
        ' =============================================
        Public Function Insertar(c As CE_Cliente) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CLIENTES.SP_CREAR_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_tipo_persona", OracleDbType.Char).Value = If(c.TipoPersona, "N")
                    cmd.Parameters.Add("p_tipo_doc_id", OracleDbType.Int32).Value = If(c.TipoDocId = 0, 1, c.TipoDocId)
                    cmd.Parameters.Add("p_num_documento", OracleDbType.Varchar2).Value = c.NumDocumento
                    cmd.Parameters.Add("p_nit", OracleDbType.Varchar2).Value = If(c.TipoPersona = "J", CObj(c.NumDocumento), CObj(DBNull.Value))
                    cmd.Parameters.Add("p_nombres", OracleDbType.Varchar2).Value = If(c.Nombres, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_apellidos", OracleDbType.Varchar2).Value = If(c.Apellidos, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_razon_social", OracleDbType.Varchar2).Value = If(c.RazonSocial, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_tel_res", OracleDbType.Varchar2).Value = c.TelResidencia
                    cmd.Parameters.Add("p_tel_cel", OracleDbType.Varchar2).Value = If(c.TelCelular, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = c.Direccion
                    cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value = If(c.CiudadId = 0, 1, c.CiudadId)
                    cmd.Parameters.Add("p_profesion", OracleDbType.Varchar2).Value = If(c.Profesion, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = c.Correo
                    cmd.Parameters.Add("p_idioma_id", OracleDbType.Int32).Value = DBNull.Value


                    Dim pOut = cmd.Parameters.Add("o_cliente_id", OracleDbType.Int32)
                    pOut.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(pOut.Value.ToString())
                End Using
            End Using
        End Function

        ' =============================================
        ' ACTUALIZAR CLIENTE — PKG_CLIENTES.SP_ACTUALIZAR_CLIENTE
        ' =============================================
        Public Function Actualizar(c As CE_Cliente) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CLIENTES.SP_ACTUALIZAR_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = c.ClienteId
                    cmd.Parameters.Add("p_tel_res", OracleDbType.Varchar2).Value = If(c.TelResidencia, "")
                    cmd.Parameters.Add("p_tel_cel", OracleDbType.Varchar2).Value = If(c.TelCelular, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = If(c.Direccion, "")
                    cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value = If(c.CiudadId = 0, 1, c.CiudadId)
                    cmd.Parameters.Add("p_profesion", OracleDbType.Varchar2).Value = If(c.Profesion, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = If(c.Correo, "")
                    cmd.Parameters.Add("p_idioma_id", OracleDbType.Int32).Value = DBNull.Value
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        ' =============================================
        ' HELPER — mapear fila de SP_BUSCAR_CLIENTES
        ' =============================================
        Private Shared Function MapearClienteLista(reader As OracleDataReader) As CE_Cliente
            Dim c As New CE_Cliente()
            c.ClienteId = Convert.ToInt32(reader("CLIENTE_ID"))
            c.NumDocumento = SafeStr(reader, "NUM_DOCUMENTO")
            Dim nombre As String = SafeStr(reader, "NOMBRE")
            c.Nombres = nombre
            c.Correo = SafeStr(reader, "EMAIL")
            c.TipoPersona = SafeStr(reader, "TIPO_PERSONA")
            c.Activo = SafeStr(reader, "ACTIVO")
            Dim fechaOrd As Integer = -1
            Try : fechaOrd = reader.GetOrdinal("FECHA_REGISTRO") : Catch : End Try
            If fechaOrd >= 0 AndAlso Not IsDBNull(reader(fechaOrd)) Then
                c.FechaCreacion = Convert.ToDateTime(reader(fechaOrd))
            End If
            Return c
        End Function

        ' =============================================
        ' HELPER — mapear fila de SP_OBTENER_CLIENTE
        ' =============================================
        Private Shared Function MapearClienteDetalle(reader As OracleDataReader) As CE_Cliente
            Dim c As New CE_Cliente()
            c.ClienteId = Convert.ToInt32(reader("CLIENTE_ID"))
            c.TipoPersona = SafeStr(reader, "TIPO_PERSONA")
            c.NumDocumento = SafeStr(reader, "NUM_DOCUMENTO")
            c.Nombres = SafeStr(reader, "NOMBRES")
            c.Apellidos = SafeStr(reader, "APELLIDOS")
            c.RazonSocial = SafeStr(reader, "RAZON_SOCIAL")
            c.TelResidencia = SafeStr(reader, "TEL_RESIDENCIA")
            c.TelCelular = SafeStr(reader, "TEL_CELULAR")
            c.Correo = SafeStr(reader, "EMAIL")
            c.Direccion = SafeStr(reader, "DIRECCION")
            c.CiudadId = SafeInt(reader, "CIUDAD_ID")
            c.Profesion = SafeStr(reader, "PROFESION")
            c.Activo = SafeStr(reader, "ACTIVO")
            If Not IsDBNull(reader("FECHA_REGISTRO")) Then
                c.FechaCreacion = Convert.ToDateTime(reader("FECHA_REGISTRO"))
            End If
            Return c
        End Function

        Private Shared Function SafeStr(r As OracleDataReader, col As String) As String
            Dim ord As Integer = -1
            Try : ord = r.GetOrdinal(col) : Catch : Return "" : End Try
            If IsDBNull(r(ord)) Then Return ""
            Return r(ord).ToString()
        End Function

        Private Shared Function SafeInt(r As OracleDataReader, col As String) As Integer
            Dim ord As Integer = -1
            Try : ord = r.GetOrdinal(col) : Catch : Return 0 : End Try
            If IsDBNull(r(ord)) Then Return 0
            Return Convert.ToInt32(r(ord))
        End Function

    End Class

End Namespace
