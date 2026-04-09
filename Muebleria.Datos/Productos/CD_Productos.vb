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
        ' OBTENER TODOS LOS PRODUCTOS
        ' =============================================
        Public Function ObtenerTodos() As List(Of CE_Producto)
            Dim lista As New List(Of CE_Producto)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.SP_OBTENER_TODOS", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            lista.Add(New CE_Producto(
                                Convert.ToInt32(reader("PRODUCTO_ID")),
                                reader("NOMBRE").ToString(),
                                reader("REFERENCIA").ToString(),
                                Convert.ToDecimal(reader("PRECIO")),
                                reader("ACTIVO").ToString()
                            ))
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
                Using cmd As New OracleCommand("PKG_PRODUCTOS.SP_OBTENER_POR_ID", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = productoId
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            producto = New CE_Producto(
                                Convert.ToInt32(reader("PRODUCTO_ID")),
                                reader("NOMBRE").ToString(),
                                reader("REFERENCIA").ToString(),
                                Convert.ToDecimal(reader("PRECIO")),
                                reader("ACTIVO").ToString()
                            )
                        End If
                    End Using
                End Using
            End Using
            Return producto
        End Function

        ' =============================================
        ' INSERTAR PRODUCTO
        ' =============================================
        Public Function Insertar(producto As CE_Producto) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.SP_INSERTAR", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = producto.Nombre
                    cmd.Parameters.Add("p_referencia", OracleDbType.Varchar2).Value = producto.Referencia
                    cmd.Parameters.Add("p_precio", OracleDbType.Decimal).Value = producto.Precio
                    cmd.Parameters.Add("p_activo", OracleDbType.Varchar2).Value = producto.Activo
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' ACTUALIZAR PRODUCTO
        ' =============================================
        Public Function Actualizar(producto As CE_Producto) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.SP_ACTUALIZAR", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = producto.ProductoId
                    cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = producto.Nombre
                    cmd.Parameters.Add("p_referencia", OracleDbType.Varchar2).Value = producto.Referencia
                    cmd.Parameters.Add("p_precio", OracleDbType.Decimal).Value = producto.Precio
                    cmd.Parameters.Add("p_activo", OracleDbType.Varchar2).Value = producto.Activo
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' ELIMINAR PRODUCTO
        ' =============================================
        Public Function Eliminar(productoId As Integer) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_PRODUCTOS.SP_ELIMINAR", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = productoId
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

    End Class

End Namespace
