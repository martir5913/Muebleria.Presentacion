Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Ordenes

        Private _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' CREAR ORDEN DESDE CARRITO
        ' =============================================
        Public Function CrearOrden(clienteId As Integer, total As Decimal) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_ORDENES.SP_CREAR_ORDEN", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    cmd.Parameters.Add("p_total", OracleDbType.Decimal).Value = total
                    cmd.Parameters.Add("p_orden_id", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_orden_id").Value)
                End Using
            End Using
        End Function

        ' =============================================
        ' AGREGAR DETALLE A LA ORDEN
        ' =============================================
        Public Function AgregarDetalle(detalle As CE_OrdenDetalle) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_ORDENES.SP_AGREGAR_DETALLE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_orden_id", OracleDbType.Int32).Value = detalle.OrdenId
                    cmd.Parameters.Add("p_producto_id", OracleDbType.Int32).Value = detalle.ProductoId
                    cmd.Parameters.Add("p_cantidad", OracleDbType.Int32).Value = detalle.Cantidad
                    cmd.Parameters.Add("p_subtotal", OracleDbType.Decimal).Value = detalle.Subtotal
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' REGISTRAR PAGO
        ' =============================================
        Public Function RegistrarPago(pago As CE_Pago) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_ORDENES.SP_REGISTRAR_PAGO", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_orden_id", OracleDbType.Int32).Value = pago.OrdenId
                    cmd.Parameters.Add("p_monto", OracleDbType.Decimal).Value = pago.Monto
                    cmd.Parameters.Add("p_forma_pago", OracleDbType.Varchar2).Value = pago.FormaPago
                    cmd.Parameters.Add("p_resultado", OracleDbType.Int32).Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(cmd.Parameters("p_resultado").Value) > 0
                End Using
            End Using
        End Function

        ' =============================================
        ' OBTENER ORDENES POR CLIENTE (HISTORIAL)
        ' =============================================
        Public Function ObtenerPorCliente(clienteId As Integer) As List(Of CE_Orden)
            Dim lista As New List(Of CE_Orden)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_ORDENES.SP_OBTENER_POR_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            lista.Add(New CE_Orden(
                                Convert.ToInt32(reader("ORDEN_ID")),
                                clienteId,
                                Convert.ToDecimal(reader("TOTAL")),
                                Convert.ToDateTime(reader("FECHA"))
                            ))
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' OBTENER DETALLE DE UNA ORDEN
        ' =============================================
        Public Function ObtenerDetalle(ordenId As Integer) As List(Of CE_OrdenDetalle)
            Dim lista As New List(Of CE_OrdenDetalle)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_ORDENES.SP_OBTENER_DETALLE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("p_orden_id", OracleDbType.Int32).Value = ordenId
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim det As New CE_OrdenDetalle()
                            det.DetalleId = Convert.ToInt32(reader("DETALLE_ID"))
                            det.OrdenId = ordenId
                            det.ProductoId = Convert.ToInt32(reader("PRODUCTO_ID"))
                            det.Cantidad = Convert.ToInt32(reader("CANTIDAD"))
                            det.Subtotal = Convert.ToDecimal(reader("SUBTOTAL"))
                            det.NombreProducto = reader("NOMBRE").ToString()
                            lista.Add(det)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

    End Class

End Namespace