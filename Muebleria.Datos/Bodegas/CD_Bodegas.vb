Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Namespace Muebleria.Datos

    Public Class CD_Bodegas

        Private ReadOnly _conexion As CD_Conexion

        Public Sub New()
            _conexion = New CD_Conexion()
        End Sub

        ' =============================================
        ' OBTENER TODAS LAS BODEGAS
        ' =============================================
        Public Function ObtenerTodas() As List(Of CE_Bodega)
            Dim lista As New List(Of CE_Bodega)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "SELECT b.BODEGA_ID, b.NOMBRE, b.DIRECCION, b.CIUDAD_ID, b.TELEFONO, b.EMPLEADO_ID, b.ACTIVO, " &
                    "c.NOMBRE AS NOMBRE_CIUDAD, " &
                    "(e.NOMBRES || ' ' || e.APELLIDOS) AS NOMBRE_EMPLEADO " &
                    "FROM MDA_BODEGAS b " &
                    "LEFT JOIN MDA_CIUDADES c ON c.CIUDAD_ID = b.CIUDAD_ID " &
                    "LEFT JOIN MDA_EMPLEADOS e ON e.EMPLEADO_ID = b.EMPLEADO_ID " &
                    "ORDER BY b.BODEGA_ID"
                Using cmd As New OracleCommand(sql, con)
                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            lista.Add(MapearBodega(reader))
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' OBTENER STOCK POR BODEGA
        ' =============================================
        Public Function ObtenerStock(bodegaId As Integer) As List(Of CE_StockBodega)
            Dim lista As New List(Of CE_StockBodega)
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "SELECT s.STOCK_ID, s.PRODUCTO_ID, s.BODEGA_ID, s.CANTIDAD, " &
                    "p.NOMBRE AS NOMBRE_PRODUCTO, p.REFERENCIA, b.NOMBRE AS NOMBRE_BODEGA " &
                    "FROM MDA_STOCK_BODEGA s " &
                    "JOIN MDA_PRODUCTOS p ON p.PRODUCTO_ID = s.PRODUCTO_ID " &
                    "JOIN MDA_BODEGAS b ON b.BODEGA_ID = s.BODEGA_ID " &
                    "WHERE s.BODEGA_ID = :p_bodega_id ORDER BY p.NOMBRE"
                Using cmd As New OracleCommand(sql, con)
                    cmd.Parameters.Add("p_bodega_id", OracleDbType.Int32).Value = bodegaId
                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim s As New CE_StockBodega()
                            s.StockId = Convert.ToInt32(reader("STOCK_ID"))
                            s.ProductoId = Convert.ToInt32(reader("PRODUCTO_ID"))
                            s.BodegaId = bodegaId
                            s.Cantidad = Convert.ToInt32(reader("CANTIDAD"))
                            s.NombreProducto = reader("NOMBRE_PRODUCTO").ToString()
                            s.ReferenciaProducto = reader("REFERENCIA").ToString()
                            s.NombreBodega = reader("NOMBRE_BODEGA").ToString()
                            lista.Add(s)
                        End While
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ' =============================================
        ' OBTENER BODEGA POR ID
        ' =============================================
        Public Function ObtenerPorId(bodegaId As Integer) As CE_Bodega
            Dim b As CE_Bodega = Nothing
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "SELECT b.BODEGA_ID, b.NOMBRE, b.DIRECCION, b.CIUDAD_ID, b.TELEFONO, b.EMPLEADO_ID, b.ACTIVO, " &
                    "c.NOMBRE AS NOMBRE_CIUDAD, " &
                    "(e.NOMBRES || ' ' || e.APELLIDOS) AS NOMBRE_EMPLEADO " &
                    "FROM MDA_BODEGAS b " &
                    "LEFT JOIN MDA_CIUDADES c ON c.CIUDAD_ID = b.CIUDAD_ID " &
                    "LEFT JOIN MDA_EMPLEADOS e ON e.EMPLEADO_ID = b.EMPLEADO_ID " &
                    "WHERE b.BODEGA_ID = :p_id"
                Using cmd As New OracleCommand(sql, con)
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = bodegaId
                    con.Open()
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then b = MapearBodega(reader)
                    End Using
                End Using
            End Using
            Return b
        End Function

        ' =============================================
        ' INSERTAR BODEGA
        ' =============================================
        Public Function Insertar(b As CE_Bodega) As Integer
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "INSERT INTO MDA_BODEGAS(NOMBRE, DIRECCION, CIUDAD_ID, TELEFONO, EMPLEADO_ID) " &
                    "VALUES (:p_nombre, :p_dir, :p_ciudad, :p_tel, :p_emp) " &
                    "RETURNING BODEGA_ID INTO :o_id"
                Using cmd As New OracleCommand(sql, con)
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = b.Nombre
                    cmd.Parameters.Add("p_dir", OracleDbType.Varchar2).Value = If(b.Direccion, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_ciudad", OracleDbType.Int32).Value = If(b.CiudadId = 0, CObj(DBNull.Value), b.CiudadId)
                    cmd.Parameters.Add("p_tel", OracleDbType.Varchar2).Value = If(b.Telefono, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_emp", OracleDbType.Int32).Value = If(b.EmpleadoId = 0, CObj(DBNull.Value), b.EmpleadoId)
                    Dim pOut = cmd.Parameters.Add("o_id", OracleDbType.Int32)
                    pOut.Direction = ParameterDirection.Output
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return Convert.ToInt32(pOut.Value.ToString())
                End Using
            End Using
        End Function

        ' =============================================
        ' ACTUALIZAR BODEGA
        ' =============================================
        Public Function Actualizar(b As CE_Bodega) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "UPDATE MDA_BODEGAS SET NOMBRE=:p_nombre, DIRECCION=:p_dir, " &
                    "TELEFONO=:p_tel, ACTIVO=:p_activo WHERE BODEGA_ID=:p_id"
                Using cmd As New OracleCommand(sql, con)
                    cmd.BindByName = True
                    cmd.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = b.Nombre
                    cmd.Parameters.Add("p_dir", OracleDbType.Varchar2).Value = If(b.Direccion, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_tel", OracleDbType.Varchar2).Value = If(b.Telefono, CObj(DBNull.Value))
                    cmd.Parameters.Add("p_activo", OracleDbType.Varchar2).Value = If(b.Activo, "S")
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = b.BodegaId
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        ' =============================================
        ' ELIMINAR (SOFT DELETE) BODEGA
        ' =============================================
        Public Function Eliminar(bodegaId As Integer) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Using cmd As New OracleCommand(
                    "UPDATE MDA_BODEGAS SET ACTIVO='I' WHERE BODEGA_ID=:p_id", con)
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = bodegaId
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        ' =============================================
        ' ACTUALIZAR STOCK EN BODEGA
        ' =============================================
        Public Function ActualizarStock(productoId As Integer, bodegaId As Integer,
                                        cantidad As Integer) As Boolean
            Using con As OracleConnection = _conexion.ObtenerConexion()
                Dim sql As String =
                    "MERGE INTO MDA_STOCK_BODEGA t " &
                    "USING DUAL ON (t.PRODUCTO_ID = :p_prod AND t.BODEGA_ID = :p_bod) " &
                    "WHEN MATCHED THEN UPDATE SET t.CANTIDAD = :p_cant " &
                    "WHEN NOT MATCHED THEN INSERT (PRODUCTO_ID, BODEGA_ID, CANTIDAD) VALUES (:p_prod2, :p_bod2, :p_cant2)"
                Using cmd As New OracleCommand(sql, con)
                    cmd.Parameters.Add("p_prod", OracleDbType.Int32).Value = productoId
                    cmd.Parameters.Add("p_bod", OracleDbType.Int32).Value = bodegaId
                    cmd.Parameters.Add("p_cant", OracleDbType.Int32).Value = cantidad
                    cmd.Parameters.Add("p_prod2", OracleDbType.Int32).Value = productoId
                    cmd.Parameters.Add("p_bod2", OracleDbType.Int32).Value = bodegaId
                    cmd.Parameters.Add("p_cant2", OracleDbType.Int32).Value = cantidad
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        End Function

        Private Shared Function MapearBodega(reader As OracleDataReader) As CE_Bodega
            Dim b As New CE_Bodega()
            b.BodegaId = Convert.ToInt32(reader("BODEGA_ID"))
            b.Nombre = reader("NOMBRE").ToString()
            If Not IsDBNull(reader("DIRECCION")) Then b.Direccion = reader("DIRECCION").ToString()
            If Not IsDBNull(reader("CIUDAD_ID")) Then b.CiudadId = Convert.ToInt32(reader("CIUDAD_ID"))
            If Not IsDBNull(reader("TELEFONO")) Then b.Telefono = reader("TELEFONO").ToString()
            If Not IsDBNull(reader("EMPLEADO_ID")) Then b.EmpleadoId = Convert.ToInt32(reader("EMPLEADO_ID"))
            Try : b.Activo = reader("ACTIVO").ToString() : Catch : b.Activo = "S" : End Try
            Try : b.NombreCiudad = reader("NOMBRE_CIUDAD").ToString() : Catch : End Try
            Try : b.NombreEmpleado = reader("NOMBRE_EMPLEADO").ToString() : Catch : End Try
            Return b
        End Function

    End Class

End Namespace
