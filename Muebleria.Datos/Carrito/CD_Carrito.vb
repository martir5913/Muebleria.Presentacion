Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Carrito

        Private _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' CREAR O RECUPERAR CARRITO ACTIVO DEL CLIENTE
        ' =============================================
        Public Function ObtenerOCrearCarrito(clienteId As Integer) As CE_Carrito
            Dim carrito As CE_Carrito = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CARRITO.SP_OBTENER_O_CREAR", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            carrito = New CE_Carrito(
                                Convert.ToInt32(reader("CARRITO_ID")),
                                clienteId,
                                reader("ESTADO").ToString()
                            )
                        End If
                    End Using
                End Using
            End Using
            Return carrito
        End Function

        ' =============================================
        ' AGREGAR ITEM AL CARRITO
        ' =============================================
        Public Function AgregarItem(item As CE_CarritoItem) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CARRITO.SP_AGREGAR_ITEM", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_carrito_id", OracleDbType.Int32).Value = item.CarritoId
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = item.ProductoId
                    cmd.Parameters.Add("p_cantidad", OracleDbType.Int32).Value = item.Cantidad
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' OBTENER ITEMS DEL CARRITO
        ' =============================================
        Public Function ObtenerItems(carritoId As Integer) As List(Of CE_CarritoItem)
            Dim lista As New List(Of CE_CarritoItem)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CARRITO.SP_OBTENER_ITEMS", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_carrito_id", OracleDbType.Int32).Value = carritoId
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New CE_CarritoItem()
                            item.ItemId = Convert.ToInt32(reader("ITEM_ID"))
                            item.CarritoId = carritoId
                            item.ProductoId = Convert.ToInt32(reader("PRODUCTO_ID"))
                            item.Cantidad = Convert.ToInt32(reader("CANTIDAD"))
                            item.NombreProducto = reader("NOMBRE").ToString()
                            item.PrecioUnitario = Convert.ToDecimal(reader("PRECIO"))
                            lista.Add(item)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' ELIMINAR ITEM DEL CARRITO
        ' =============================================
        Public Function EliminarItem(itemId As Integer) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CARRITO.SP_ELIMINAR_ITEM", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_item_id", OracleDbType.Int32).Value = itemId
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' VACIAR CARRITO COMPLETO
        ' =============================================
        Public Function VaciarCarrito(carritoId As Integer) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_CARRITO.SP_VACIAR", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_carrito_id", OracleDbType.Int32).Value = carritoId
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

    End Class

End Namespace