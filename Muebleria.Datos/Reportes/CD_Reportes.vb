Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Reportes

        Private ReadOnly _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' VENTAS DIARIAS — PKG_REPORTES.SP_VENTAS_DIARIAS
        ' =============================================
        Public Function VentasDiarias(fechaIni As Date, fechaFin As Date,
                                      ciudadId As Object) As List(Of CE_VentaDiaria)
            Dim lista As New List(Of CE_VentaDiaria)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_REPORTES.SP_VENTAS_DIARIAS", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_fecha_ini", OracleDbType.Date).Value = fechaIni
                    cmd.Parameters.Add("p_fecha_fin", OracleDbType.Date).Value = fechaFin
                    cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value =
                        If(ciudadId Is Nothing, CObj(DBNull.Value), ciudadId)

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            Dim v As New CE_VentaDiaria()
                            v.Fecha = Convert.ToDateTime(reader("FECHA"))
                            v.TipoMueble = reader("TIPO_MUEBLE").ToString()
                            v.Ciudad = If(IsDBNull(reader("CIUDAD")), "TODAS", reader("CIUDAD").ToString())
                            v.TotalVentas = Convert.ToDecimal(reader("TOTAL_VENTAS"))
                            v.Ordenes = Convert.ToInt32(reader("ORDENES"))
                            v.Unidades = Convert.ToInt32(reader("UNIDADES"))
                            lista.Add(v)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' PRODUCTO MAS VENDIDO
        ' =============================================
        Public Function ProductoMasVendido(fechaIni As Date, fechaFin As Date,
                                           ciudadId As Object) As CE_ProductoVendido
            Dim resultado As CE_ProductoVendido = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_REPORTES.SP_PRODUCTO_MAS_VENDIDO", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_fecha_ini", OracleDbType.Date).Value = fechaIni
                    cmd.Parameters.Add("p_fecha_fin", OracleDbType.Date).Value = fechaFin
                    cmd.Parameters.Add("p_ciudad_id", OracleDbType.Int32).Value =
                        If(ciudadId Is Nothing, CObj(DBNull.Value), ciudadId)

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        If reader.Read() Then
                            resultado = New CE_ProductoVendido()
                            resultado.ProductoId = Convert.ToInt32(reader("PRODUCTO_ID"))
                            resultado.Referencia = reader("REFERENCIA").ToString()
                            resultado.Nombre = reader("NOMBRE").ToString()
                            resultado.TipoMueble = reader("TIPO_MUEBLE").ToString()
                            resultado.Ciudad = If(IsDBNull(reader("CIUDAD")), "TODAS", reader("CIUDAD").ToString())
                            resultado.Unidades = Convert.ToInt32(reader("UNIDADES"))
                            resultado.TotalVentas = Convert.ToDecimal(reader("TOTAL_VENTAS"))
                        End If
                    End Using
                End Using
            End Using
            Return resultado
        End Function

        ' =============================================
        ' COMPRAS POR CLIENTE
        ' =============================================
        Public Function ComprasPorCliente(clienteId As Integer,
                                          fechaIni As Date, fechaFin As Date) As List(Of CE_CompraCliente)
            Dim lista As New List(Of CE_CompraCliente)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand("PKG_REPORTES.SP_COMPRAS_POR_CLIENTE", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = clienteId
                    cmd.Parameters.Add("p_fecha_ini", OracleDbType.Date).Value = fechaIni
                    cmd.Parameters.Add("p_fecha_fin", OracleDbType.Date).Value = fechaFin

                    Dim pRc = cmd.Parameters.Add("o_rc", OracleDbType.RefCursor)
                    pRc.Direction = ParameterDirection.Output

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Dim cursor = CType(pRc.Value, Oracle.ManagedDataAccess.Types.OracleRefCursor)
                    Using reader As OracleDataReader = cursor.GetDataReader()
                        While reader.Read()
                            Dim c As New CE_CompraCliente()
                            c.OrdenId = Convert.ToInt32(reader("ORDEN_ID"))
                            c.NumeroOrden = reader("NUMERO_ORDEN").ToString()
                            c.FechaCompra = Convert.ToDateTime(reader("FECHA_COMPRA"))
                            c.ValorCompra = Convert.ToDecimal(reader("VALOR_COMPRA"))
                            c.FormaPago = If(IsDBNull(reader("FORMA_PAGO")), "", reader("FORMA_PAGO").ToString())
                            lista.Add(c)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

    End Class

End Namespace
