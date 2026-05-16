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
        ' CONFIRMAR COMPRA DESDE CARRITO
        ' Obtiene ciudad/dirección del cliente y busca forma_pago_id por CODIGO
        ' =============================================
        Public Function EfectuarCompra(clienteId As Integer, carritoId As Integer,
                                       formaPagoCodigo As String) As Integer
            Dim formaPagoId As Integer = 1
            Dim ciudadEntregaId As Integer = 1
            Dim direccionEntrega As String = "Pendiente de confirmar"

            Using con As OracleConnection = _conexion.ObtenerConexion()
                ' Obtener forma de pago
                Using cmd As New OracleCommand(
                    "SELECT FORMA_PAGO_ID FROM MDA_FORMAS_PAGO WHERE UPPER(CODIGO) = UPPER(:p_codigo) AND ROWNUM=1", con)
                    cmd.Parameters.Add("p_codigo", OracleDbType.Varchar2).Value = formaPagoCodigo
                    con.Open()
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                        formaPagoId = Convert.ToInt32(res)
                    End If
                End Using
            End Using

            Using con As OracleConnection = _conexion.ObtenerConexion()
                ' Obtener ciudad y dirección del cliente
                Using cmd As New OracleCommand(
                    "SELECT CIUDAD_ID, DIRECCION FROM MDA_CLIENTES WHERE CLIENTE_ID = :p_id", con)
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = clienteId
                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("CIUDAD_ID")) Then
                                ciudadEntregaId = Convert.ToInt32(reader("CIUDAD_ID"))
                            End If
                            If Not IsDBNull(reader("DIRECCION")) Then
                                direccionEntrega = reader("DIRECCION").ToString()
                            End If
                        End If
                    End Using
                End Using
            End Using

            ' Confirmar compra
            Dim ordenId As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_COMPRAS.SP_CONFIRMAR_COMPRA_DESDE_CARRITO", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    cmd.Parameters.Add("p_carrito_id", OracleDbType.Int32).Value = carritoId
                    cmd.Parameters.Add("p_ciudad_entrega_id", OracleDbType.Int32).Value = ciudadEntregaId
                    cmd.Parameters.Add("p_direccion_entrega", OracleDbType.Varchar2).Value = direccionEntrega
                    cmd.Parameters.Add("p_forma_pago_id", OracleDbType.Int32).Value = formaPagoId
                    cmd.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = "Compra en línea"

                    Dim pOrdenId = cmd.Parameters.Add("o_orden_id", OracleDbType.Int32)
                    pOrdenId.Direction = ParameterDirection.Output
                    Dim pNumOrden = cmd.Parameters.Add("o_numero_orden", OracleDbType.Varchar2, 30)
                    pNumOrden.Direction = ParameterDirection.Output
                    Dim pTotal = cmd.Parameters.Add("o_total", OracleDbType.Decimal)
                    pTotal.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()
                    ordenId = Convert.ToInt32(pOrdenId.Value.ToString())
                End Using
            End Using
            Return ordenId
        End Function

        ' =============================================
        ' OBTENER DETALLE DE ORDEN
        ' =============================================
        Public Function ObtenerDetalle(ordenId As Integer) As List(Of CE_OrdenDetalle)
            Dim lista As New List(Of CE_OrdenDetalle)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_COMPRAS.SP_OBTENER_ORDEN_DETALLE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_orden_id", OracleDbType.Int32).Value = ordenId

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            Dim det As New CE_OrdenDetalle()
                            det.OrdenId = ordenId
                            det.ProductoId = Convert.ToInt32(reader("PRODUCTO_ID"))
                            det.NombreProducto = If(IsDBNull(reader("NOMBRE_SNAPSHOT")), "", reader("NOMBRE_SNAPSHOT").ToString())
                            det.Cantidad = Convert.ToInt32(reader("CANTIDAD"))
                            det.Subtotal = Convert.ToDecimal(reader("SUBTOTAL"))
                            lista.Add(det)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' OBTENER ÓRDENES POR CLIENTE
        ' =============================================
        Public Function ObtenerPorCliente(clienteId As Integer) As List(Of CE_Orden)
            Dim lista As New List(Of CE_Orden)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_COMPRAS.SP_LISTAR_ORDENES_POR_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            Dim o As New CE_Orden()
                            o.OrdenId = Convert.ToInt32(reader("ORDEN_ID"))
                            o.ClienteId = clienteId
                            o.Total = Convert.ToDecimal(reader("TOTAL"))
                            o.Fecha = Convert.ToDateTime(reader("FECHA_ORDEN"))
                            lista.Add(o)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

    End Class

End Namespace
