Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports Muebleria.Entidades

' 💡 ¡ESTA ES LA LÍNEA CLAVE! 
' Revisa cómo se llama tu proyecto de datos o la carpeta donde está tu clase Conexion.
' Si al escribir "Imports Muebleria.Datos" te da error, bórrala y usa el truco de abajo.
Imports Muebleria.Datos

Public Class CD_Productos

    ' Método para listar todos los productos
    Public Function ListarProductos() As List(Of CE_Producto)
        Dim lista As New List(Of CE_Producto)()
        Dim query As String = "SELECT PRODUCTO_ID, NOMBRE, REFERENCIA, DESCRIPCION, TIPO_MUEBLE_ID, MATERIAL, DIM_ALTO_CM, DIM_ANCHO_CM, DIM_PROF_CM, COLOR, PESO_GRAMOS, FOTO_URL, PRECIO, ACTIVO FROM MDA_PRODUCTOS"

        Using con As OracleConnection = (New CD_Conexion()).ObtenerConexion()
            Using cmd As New OracleCommand(query, con)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 30 ' timeout en segundos para evitar bloqueos indefinidos
                Try
                    con.Open()
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim prod As New CE_Producto()
                            prod.ProductoId = Convert.ToInt32(dr("PRODUCTO_ID"))
                            prod.Nombre = dr("NOMBRE").ToString()
                            prod.Referencia = If(dr.IsDBNull(dr.GetOrdinal("REFERENCIA")), "", dr("REFERENCIA").ToString())
                            prod.Descripcion = If(dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")), "", dr("DESCRIPCION").ToString())
                            prod.TipoMuebleId = If(dr.IsDBNull(dr.GetOrdinal("TIPO_MUEBLE_ID")), 0, Convert.ToInt32(dr("TIPO_MUEBLE_ID")))
                            prod.Material = If(dr.IsDBNull(dr.GetOrdinal("MATERIAL")), "", dr("MATERIAL").ToString())
                            prod.DimAltoCm = If(dr.IsDBNull(dr.GetOrdinal("DIM_ALTO_CM")), 0D, Convert.ToDecimal(dr("DIM_ALTO_CM")))
                            prod.DimAnchoCm = If(dr.IsDBNull(dr.GetOrdinal("DIM_ANCHO_CM")), 0D, Convert.ToDecimal(dr("DIM_ANCHO_CM")))
                            prod.DimProfCm = If(dr.IsDBNull(dr.GetOrdinal("DIM_PROF_CM")), 0D, Convert.ToDecimal(dr("DIM_PROF_CM")))
                            prod.Color = If(dr.IsDBNull(dr.GetOrdinal("COLOR")), "", dr("COLOR").ToString())
                            prod.PesoGramos = If(dr.IsDBNull(dr.GetOrdinal("PESO_GRAMOS")), 0D, Convert.ToDecimal(dr("PESO_GRAMOS")))
                            prod.FotoUrl = If(dr.IsDBNull(dr.GetOrdinal("FOTO_URL")), "", dr("FOTO_URL").ToString())
                            prod.Precio = If(dr.IsDBNull(dr.GetOrdinal("PRECIO")), 0D, Convert.ToDecimal(dr("PRECIO")))
                            prod.Activo = If(dr.IsDBNull(dr.GetOrdinal("ACTIVO")), "N", dr("ACTIVO").ToString())

                            lista.Add(prod)
                        End While
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Using
        Return lista
    End Function

    ' Método para buscar producto por su ID
    Public Function ObtenerPorId(ByVal id As Integer) As CE_Producto
        Dim prod As CE_Producto = Nothing
        Dim query As String = "SELECT PRODUCTO_ID, NOMBRE, REFERENCIA, DESCRIPCION, TIPO_MUEBLE_ID, MATERIAL, DIM_ALTO_CM, DIM_ANCHO_CM, DIM_PROF_CM, COLOR, PESO_GRAMOS, FOTO_URL, PRECIO, ACTIVO FROM MDA_PRODUCTOS WHERE PRODUCTO_ID = :id"

        Using con As OracleConnection = (New CD_Conexion()).ObtenerConexion()
            Using cmd As New OracleCommand(query, con)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 30 ' timeout en segundos para evitar bloqueos indefinidos
                cmd.Parameters.Add(New OracleParameter("id", id))
                Try
                    con.Open()
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            prod = New CE_Producto()
                            prod.ProductoId = Convert.ToInt32(dr("PRODUCTO_ID"))
                            prod.Nombre = dr("NOMBRE").ToString()
                            prod.Referencia = If(dr.IsDBNull(dr.GetOrdinal("REFERENCIA")), "", dr("REFERENCIA").ToString())
                            prod.Descripcion = If(dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")), "", dr("DESCRIPCION").ToString())
                            prod.TipoMuebleId = If(dr.IsDBNull(dr.GetOrdinal("TIPO_MUEBLE_ID")), 0, Convert.ToInt32(dr("TIPO_MUEBLE_ID")))
                            prod.Material = If(dr.IsDBNull(dr.GetOrdinal("MATERIAL")), "", dr("MATERIAL").ToString())
                            prod.DimAltoCm = If(dr.IsDBNull(dr.GetOrdinal("DIM_ALTO_CM")), 0D, Convert.ToDecimal(dr("DIM_ALTO_CM")))
                            prod.DimAnchoCm = If(dr.IsDBNull(dr.GetOrdinal("DIM_ANCHO_CM")), 0D, Convert.ToDecimal(dr("DIM_ANCHO_CM")))
                            prod.DimProfCm = If(dr.IsDBNull(dr.GetOrdinal("DIM_PROF_CM")), 0D, Convert.ToDecimal(dr("DIM_PROF_CM")))
                            prod.Color = If(dr.IsDBNull(dr.GetOrdinal("COLOR")), "", dr("COLOR").ToString())
                            prod.PesoGramos = If(dr.IsDBNull(dr.GetOrdinal("PESO_GRAMOS")), 0D, Convert.ToDecimal(dr("PESO_GRAMOS")))
                            prod.FotoUrl = If(dr.IsDBNull(dr.GetOrdinal("FOTO_URL")), "", dr("FOTO_URL").ToString())
                            prod.Precio = If(dr.IsDBNull(dr.GetOrdinal("PRECIO")), 0D, Convert.ToDecimal(dr("PRECIO")))
                            prod.Activo = If(dr.IsDBNull(dr.GetOrdinal("ACTIVO")), "N", dr("ACTIVO").ToString())
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Using
        Return prod
    End Function

    Public Function RegistrarProducto(obj As CE_Producto) As Boolean
        Dim conexion As New CD_Conexion()
        Using con As OracleConnection = conexion.ObtenerConexion()
            Using cmd As New OracleCommand("PKG_PRODUCTOS.INSERTAR_PRODUCTO", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.BindByName = True
                cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = obj.Nombre
                cmd.Parameters.Add("p_referencia", OracleDbType.Varchar2).Value = obj.Referencia
                cmd.Parameters.Add("p_tipo_mueble_id", OracleDbType.Int32).Value = obj.TipoMuebleId
                cmd.Parameters.Add("p_precio_cop", OracleDbType.Decimal).Value = obj.Precio
                cmd.Parameters.Add("p_stock_inicial", OracleDbType.Int32).Value = 0
                Dim outId As New OracleParameter("o_producto_id", OracleDbType.Int32)
                outId.Direction = ParameterDirection.Output
                cmd.Parameters.Add(outId)
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    If outId.Value IsNot Nothing AndAlso Not Convert.IsDBNull(outId.Value) Then
                        obj.ProductoId = Convert.ToInt32(outId.Value.ToString())
                    End If
                    Return True
                Catch ex As OracleException
                    Throw New Exception("Error al insertar producto: " & ex.Message, ex)
                End Try
            End Using
        End Using
    End Function

    Public Function ModificarProducto(obj As CE_Producto) As Boolean
        Dim conexion As New CD_Conexion()
        Using con As OracleConnection = conexion.ObtenerConexion()
            Using cmd As New OracleCommand("PKG_PRODUCTOS.ACTUALIZAR_PRODUCTO", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.BindByName = True
                cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = obj.ProductoId
                ' Use DBNull.Value for optional fields when empty
                cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = If(String.IsNullOrWhiteSpace(obj.Nombre), DBNull.Value, CType(obj.Nombre, Object))
                cmd.Parameters.Add("p_referencia", OracleDbType.Varchar2).Value = If(String.IsNullOrWhiteSpace(obj.Referencia), DBNull.Value, CType(obj.Referencia, Object))
                cmd.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = If(String.IsNullOrWhiteSpace(obj.Descripcion), DBNull.Value, CType(obj.Descripcion, Object))
                cmd.Parameters.Add("p_material", OracleDbType.Varchar2).Value = If(String.IsNullOrWhiteSpace(obj.Material), DBNull.Value, CType(obj.Material, Object))
                cmd.Parameters.Add("p_color", OracleDbType.Varchar2).Value = If(String.IsNullOrWhiteSpace(obj.Color), DBNull.Value, CType(obj.Color, Object))
                cmd.Parameters.Add("p_precio_cop", OracleDbType.Decimal).Value = If(obj.Precio <= 0, DBNull.Value, CType(obj.Precio, Object))
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As OracleException
                    Throw New Exception("Error al actualizar producto: " & ex.Message, ex)
                End Try
            End Using
        End Using
    End Function

    Public Function EliminarProducto(id As Integer) As Boolean
        Dim conexion As New CD_Conexion()
        Using con As OracleConnection = conexion.ObtenerConexion()
            Using cmd As New OracleCommand("PKG_PRODUCTOS.ELIMINAR_PRODUCTO", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.BindByName = True
                cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = id
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As OracleException
                    Throw New Exception("Error al eliminar producto: " & ex.Message, ex)
                End Try
            End Using
        End Using
    End Function

End Class