Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Productos

        Private _conexion As CD_Conexion
        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' OBTENER TODOS LOS PRODUCTOS — SP_OBTENER_TODOS (incluye inactivos)
        ' =============================================
        Public Function ObtenerTodos() As List(Of CE_Producto)
            Dim lista As New List(Of CE_Producto)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.OBTENER_PRODUCTOS", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            lista.Add(New CE_Producto(
                                Convert.ToInt32(reader("PRODUCTO_ID")),
                                reader("NOMBRE").ToString(),
                                reader("REFERENCIA").ToString(),
                                Convert.ToDecimal(reader("PRECIO_COP")),
                                reader("ACTIVO").ToString()
                            ))
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        Public Function BuscarAdmin(texto As String, tipoMuebleId As Object) As List(Of CE_Producto)
            Return ObtenerTodos()
        End Function

        ' =============================================
        ' BUSCAR CATÁLOGO PÚBLICO (solo activos)
        ' =============================================
        Public Function Buscar(texto As String, tipoMuebleId As Object) As List(Of CE_Producto)
            Dim lista As New List(Of CE_Producto)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.SP_BUSCAR_PRODUCTOS", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_texto", OracleDbType.Varchar2).Value =
                        If(String.IsNullOrEmpty(texto), CObj(DBNull.Value), CObj(texto))
                    cmd.Parameters.Add("p_tipo_mueble_id", OracleDbType.Int32).Value =
                        If(tipoMuebleId Is Nothing, CObj(DBNull.Value), tipoMuebleId)

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            lista.Add(MapearProducto(reader))
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' OBTENER PRODUCTO POR ID
        ' =============================================
        Public Function ObtenerPorId(productoId As Integer) As CE_Producto
            Dim producto As CE_Producto = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.OBTENER_PRODUCTO_POR_ID", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = productoId
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        If reader.Read() Then
                            producto = New CE_Producto(
                                Convert.ToInt32(reader("PRODUCTO_ID")),
                                reader("NOMBRE").ToString(),
                                reader("REFERENCIA").ToString(),
                                Convert.ToDecimal(reader("PRECIO_COP")),
                                reader("ACTIVO").ToString()
                            )
                        End If
                    End Using
                End Using
            End Using
            Return producto
        End Function

        ' =============================================
        ' CREAR PRODUCTO — SP_CREAR_PRODUCTO
        ' =============================================
        Public Function Insertar(p As CE_Producto) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.INSERTAR_PRODUCTO", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = producto.Nombre
                    cmd.Parameters.Add("p_referencia", OracleDbType.Varchar2).Value = producto.Referencia
                    cmd.Parameters.Add("p_tipo_mueble_id", OracleDbType.Int32).Value = 1
                    cmd.Parameters.Add("p_precio_cop", OracleDbType.Decimal).Value = producto.Precio
                    cmd.Parameters.Add("p_stock_inicial", OracleDbType.Int32).Value = 0
                    cmd.Parameters.Add("o_producto_id", OracleDbType.Int32).Direction = ParameterDirection.Output
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        ' =============================================
        ' ACTUALIZAR PRODUCTO — SP_ACTUALIZAR_PRODUCTO
        ' =============================================
        Public Function Actualizar(p As CE_Producto) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.ACTUALIZAR_PRODUCTO", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = producto.ProductoId
                    cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = producto.Nombre
                    cmd.Parameters.Add("p_referencia", OracleDbType.Varchar2).Value = producto.Referencia
                    cmd.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = ""
                    cmd.Parameters.Add("p_material", OracleDbType.Varchar2).Value = ""
                    cmd.Parameters.Add("p_color", OracleDbType.Varchar2).Value = ""
                    cmd.Parameters.Add("p_precio_cop", OracleDbType.Decimal).Value = producto.Precio
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        ' =============================================
        ' ELIMINAR PRODUCTO — SP_ELIMINAR_PRODUCTO
        ' =============================================
        Public Function Eliminar(productoId As Integer) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.ELIMINAR_PRODUCTO", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = productoId
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
            Return mapa
        End Function

        ' =============================================
        ' OBTENER NOMBRES DE CATEGORÍAS
        ' =============================================
        Public Function ObtenerNombresCategorias() As List(Of String)
            Return ObtenerMapaTipos().Values.OrderBy(Function(n) n).ToList()
        End Function

        ' =============================================
        ' HELPER — MAP ROW -> CE_Producto
        ' =============================================
        Private Shared Function MapearProducto(reader As OracleDataReader) As CE_Producto
            Dim p As New CE_Producto()
            p.ProductoId = Convert.ToInt32(reader("PRODUCTO_ID"))
            p.Nombre = reader("NOMBRE").ToString()
            p.Referencia = reader("REFERENCIA").ToString()
            p.FotoUrl = SafeStr(reader, "FOTO_URL")
            p.Material = SafeStr(reader, "MATERIAL")
            p.TipoMuebleCodigo = SafeStr(reader, "TIPO")
            p.TipoMuebleNombre = SafeStr(reader, "NOMBRE_TIPO")
            If String.IsNullOrEmpty(p.TipoMuebleNombre) Then
                p.TipoMuebleNombre = SafeStr(reader, "NOMBRE")
            End If
            ' precio viene de MDA_INVENTARIO como PRECIO_COP
            Dim precioOrd As Integer = -1
            Try : precioOrd = reader.GetOrdinal("PRECIO_COP") : Catch : End Try
            If precioOrd >= 0 AndAlso Not IsDBNull(reader(precioOrd)) Then
                p.Precio = Convert.ToDecimal(reader(precioOrd))
            End If
            Dim stockOrd As Integer = -1
            Try : stockOrd = reader.GetOrdinal("STOCK") : Catch : End Try
            If stockOrd >= 0 AndAlso Not IsDBNull(reader(stockOrd)) Then
                p.Stock = Convert.ToInt32(reader(stockOrd))
            End If
            ' campos extendidos solo en SP_OBTENER_PRODUCTO
            Dim activoOrd As Integer = -1
            Try : activoOrd = reader.GetOrdinal("ACTIVO") : Catch : End Try
            If activoOrd >= 0 AndAlso Not IsDBNull(reader(activoOrd)) Then
                p.Activo = reader(activoOrd).ToString()
            Else
                p.Activo = "S"
            End If
            p.Descripcion = SafeStr(reader, "DESCRIPCION")
            p.Color = SafeStr(reader, "COLOR")
            p.DimAltoCm = SafeDec(reader, "DIM_ALTO_CM")
            p.DimAnchoCm = SafeDec(reader, "DIM_ANCHO_CM")
            p.DimProfCm = SafeDec(reader, "DIM_PROF_CM")
            p.PesoGramos = SafeInt(reader, "PESO_GRAMOS")
            Dim tipoIdOrd As Integer = -1
            Try : tipoIdOrd = reader.GetOrdinal("TIPO_MUEBLE_ID") : Catch : End Try
            If tipoIdOrd >= 0 AndAlso Not IsDBNull(reader(tipoIdOrd)) Then
                p.TipoMuebleId = Convert.ToInt32(reader(tipoIdOrd))
            End If
            Return p
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

        Private Shared Function SafeDec(r As OracleDataReader, col As String) As Decimal
            Dim ord As Integer = -1
            Try : ord = r.GetOrdinal(col) : Catch : Return 0D : End Try
            If IsDBNull(r(ord)) Then Return 0D
            Return Convert.ToDecimal(r(ord))
        End Function

    End Class

End Namespace