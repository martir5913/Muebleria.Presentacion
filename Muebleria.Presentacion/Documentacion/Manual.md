# Manual de Configuración - Mueblería

## 1. Configuración de la Cadena de Conexión a Oracle

### 1.1 Ubicación del archivo
La cadena de conexión se configura en el archivo **`Web.config`** ubicado en la raíz del proyecto:

```
Muebleria.Presentacion\Web.config
```

### 1.2 Estructura de la cadena de conexión

Localiza la sección `<connectionStrings>`:

```xml
<connectionStrings>
  <add name="OracleDb" 
       connectionString="User Id=system;Password=Oracle123;Data Source=localhost:1523/ORCLCDB;" 
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

### 1.3 Componentes de la cadena de conexión

| Componente | Descripción | Ejemplo |
|-----------|-------------|---------|
| **User Id** | Usuario de Oracle | `system`, `admin`, etc. |
| **Password** | Contraseña del usuario | `Oracle123` |
| **Data Source** | Host, puerto y SID/PDB | `localhost:1523/ORCLCDB` |
| **providerName** | Proveedor de datos | `Oracle.ManagedDataAccess.Client` |

#### Desglose de Data Source:
- **localhost** - IP o nombre del servidor
- **1523** - Puerto de Oracle (por defecto es 1521)
- **ORCLCDB** - SID o nombre de la base de datos (PDB)

### 1.4 Pasos para configurar tu propia cadena de conexión

#### Para ambiente LOCAL:

```xml
<add name="OracleDb" 
     connectionString="User Id=TU_USUARIO;Password=TU_PASSWORD;Data Source=localhost:1521/TU_SID;" 
     providerName="Oracle.ManagedDataAccess.Client" />
```

**Reemplaza:**
- `TU_USUARIO` - Tu usuario de Oracle
- `TU_PASSWORD` - Tu contraseña
- `TU_SID` - El SID o nombre del servicio (ej: ORCL, XE, ORCLCDB)

#### Para ambiente REMOTO:

```xml
<add name="OracleDb" 
     connectionString="User Id=TU_USUARIO;Password=TU_PASSWORD;Data Source=192.168.1.100:1521/TU_SID;" 
     providerName="Oracle.ManagedDataAccess.Client" />
```

**Reemplaza:**
- `192.168.1.100` - IP del servidor remoto
- `1521` - Puerto de Oracle en el servidor remoto
- `TU_USUARIO` - Usuario en esa base de datos
- `TU_PASSWORD` - Contraseña en esa base de datos

### 1.5 Opciones adicionales de la cadena de conexión

Puedes agregar más parámetros según sea necesario:

```xml
<add name="OracleDb" 
     connectionString="User Id=system;Password=Oracle123;Data Source=localhost:1521/ORCL;Pooling=true;Min Pool Size=5;Max Pool Size=50;Connection Lifetime=180;" 
     providerName="Oracle.ManagedDataAccess.Client" />
```

**Parámetros opcionales:**
- `Pooling=true` - Habilitar pool de conexiones (mejora rendimiento)
- `Min Pool Size=5` - Mínimo de conexiones en el pool
- `Max Pool Size=50` - Máximo de conexiones en el pool
- `Connection Lifetime=180` - Tiempo de vida de la conexión en segundos

---

## 2. Verificar la Conexión - Test de Conexión

### 2.1 Cómo usar la funcionalidad de test

La aplicación incluye una funcionalidad para probar la conexión a Oracle.

#### Opción 1: Mediante la interfaz web

Accede a:

```
http://localhost:PUERTO/TestDB
```

Haz clic en el botón **"Probar Conexión"** para verificar que tu cadena de conexión es correcta.

#### Opción 2: Llamada directa al endpoint (API)

Si prefieres probar directamente desde terminal o Postman, puedes hacer una petición GET al endpoint:

```
http://localhost:PUERTO/TestDB/TestConexion
```

**Nota:** El puerto varía según tu configuración local. En el ejemplo usado es **61926**, pero puede ser diferente en tu entorno.

**Ejemplo completo:**
```
http://localhost:61926/TestDB/TestConexion
```

**Respuesta exitosa (JSON):**
```json
{
  "success": true,
  "message": "Conexión exitosa a Oracle"
}
```

**Respuesta con error (JSON):**
```json
{
  "success": false,
  "message": "Error al conectar a Oracle: ORA-12514 TNS:listener does not know of service named ORCLCDB"
}
```

#### Cómo probar desde PowerShell:

```powershell
Invoke-WebRequest -Uri "http://localhost:61926/TestDB/TestConexion" -Method GET | Select-Object -ExpandProperty Content
```

#### Cómo probar desde cURL:

```bash
curl "http://localhost:61926/TestDB/TestConexion"
```

#### Cómo probar desde Postman:

1. Abre Postman
2. Crea una nueva petición GET
3. URL: `http://localhost:PUERTO/TestDB/TestConexion`
4. Haz clic en **Send**
5. Verifica la respuesta en la sección "Body"

### 2.2 Cómo funciona internamente

#### Flujo de la arquitectura:

```
View (TestDB/Index.vbhtml)
    ↓
Controller (TestDBController)
    ↓
Service (CN_ConexionService)
    ↓
Data Access (CD_Conexion)
    ↓
Oracle Connection
```

#### Capas involucradas:

**1. Presentación (TestDBController.vb)**
```visualbasic
Function TestConexion() As JsonResult
    Try
        Dim estado As Boolean = _service.TestConexion()
        Return Json(New With {
            .success = estado,
            .message = If(estado, "Conexión exitosa a Oracle", "Fallo en conexión")
        }, JsonRequestBehavior.AllowGet)
    Catch ex As Exception
        Return Json(New With {
            .success = False,
            .message = ex.Message
        }, JsonRequestBehavior.AllowGet)
    End Try
End Function
```

**2. Lógica de Negocio (CN_ConexionService.vb)**
```visualbasic
Public Function TestConexion() As Boolean
    Return _conexion.ProbarConexion()
End Function
```

**3. Acceso a Datos (CD_Conexion.vb)**
```visualbasic
Public Function ProbarConexion() As Boolean
    Try
        Using conn As New OracleConnection(_connectionString)
            conn.Open()
            Return True
        End Using
    Catch ex As Exception
        Throw New Exception("Error al conectar a Oracle: " & ex.Message)
    End Try
End Function
```

### 2.3 Mensajes de respuesta

| Respuesta | Ejemplo JSON | Significado |
|-----------|-------------|------------|
| ✅ Exitosa | `{"success": true, "message": "Conexión exitosa a Oracle"}` | La cadena de conexión es correcta |
| ❌ Fallo genérico | `{"success": false, "message": "Fallo en conexión"}` | Error en la conexión sin detalles específicos |
| ❌ Error de credenciales | `{"success": false, "message": "Error al conectar a Oracle: ORA-01017 invalid username/password"}` | Usuario o contraseña incorrectos |
| ❌ Servicio no disponible | `{"success": false, "message": "Error al conectar a Oracle: ORA-12514 TNS:listener does not know of service"}` | El SID/PDB no existe o el servidor no está disponible |
| ❌ Librería no instalada | `{"success": false, "message": "Unable to load Oracle.ManagedDataAccess"}` | Falta instalar el paquete NuGet Oracle.ManagedDataAccess |
| ❌ Timeout | `{"success": false, "message": "Error al conectar a Oracle: Connection Timeout"}` | No se puede conectar al servidor en el tiempo especificado |

---

## 3. Consumir Paquetes Oracle (PKG)

### 3.1 Estructura general

La arquitectura para consumir procedimientos almacenados en Oracle sigue este patrón:

```
Capa de Presentación (Controller)
    ↓
Capa de Negocio (Service)
    ↓
Capa de Datos (Data Access)
    ↓
Paquete Oracle (PKG_*)
```

### 3.2 Ejemplo actual: PKG_CLIENTES

En el proyecto ya existe un ejemplo implementado con `PKG_CLIENTES.OBTENER_CLIENTES`:

**Archivo: Muebleria.Datos\Clientes\CD_Clientes.vb**

```visualbasic
Public Function ObtenerClientes() As List(Of CE_Cliente)
    Dim clientes As New List(Of CE_Cliente)()

    Try
        Using conn As New OracleConnection(_connectionString)
            Using cmd As New OracleCommand("PKG_CLIENTES.OBTENER_CLIENTES", conn)
                cmd.CommandType = System.Data.CommandType.StoredProcedure

                ' Parámetro OUT para el cursor
                Dim pCursor As New OracleParameter("p_cursor", OracleDbType.RefCursor)
                pCursor.Direction = System.Data.ParameterDirection.Output
                cmd.Parameters.Add(pCursor)

                conn.Open()
                cmd.ExecuteNonQuery()

                ' Procesar el cursor
                Dim cursor As OracleRefCursor = CType(pCursor.Value, OracleRefCursor)
                If cursor IsNot Nothing Then
                    Dim reader As OracleDataReader = cursor.GetDataReader()
                    If reader IsNot Nothing Then
                        While reader.Read()
                            Dim cliente As New CE_Cliente With {
                                .Id = Convert.ToInt32(reader("ID")),
                                .Nombre = reader("NOMBRE").ToString(),
                                .Correo = reader("CORREO").ToString(),
                                .FechaCreacion = Convert.ToDateTime(reader("FECHA_CREACION"))
                            }
                            clientes.Add(cliente)
                        End While
                        reader.Close()
                        reader.Dispose()
                    End If
                End If
            End Using
        End Using
    Catch ex As Exception
        Throw New Exception("Error al obtener clientes: " & ex.Message, ex)
    End Try

    Return clientes
End Function
```

### 3.3 Pasos para consumir un nuevo paquete Oracle

#### Paso 1: Crear la clase de entidad

**Archivo: Muebleria.Entidades\[TuEntidad]\CE_[NombreEntidad].vb**

```visualbasic
Public Class CE_Productos
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Precio As Decimal
    Public Property Descripcion As String
    Public Property FechaCreacion As DateTime
End Class
```

#### Paso 2: Crear la clase de acceso a datos

**Archivo: Muebleria.Datos\[TuEntidad]\CD_[NombreEntidad].vb**

```visualbasic
Imports System.Configuration
Imports Muebleria.Entidades
Imports Oracle.ManagedDataAccess.Client

Public Class CD_Productos
    Private ReadOnly _connectionString As String

    Public Sub New()
        _connectionString = ConfigurationManager.ConnectionStrings("OracleDb").ConnectionString
    End Sub

    Public Function ObtenerProductos() As List(Of CE_Productos)
        Dim productos As New List(Of CE_Productos)()

        Try
            Using conn As New OracleConnection(_connectionString)
                Using cmd As New OracleCommand("PKG_PRODUCTOS.OBTENER_PRODUCTOS", conn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    ' Parámetro OUT para el cursor
                    Dim pCursor As New OracleParameter("p_cursor", OracleDbType.RefCursor)
                    pCursor.Direction = System.Data.ParameterDirection.Output
                    cmd.Parameters.Add(pCursor)

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    ' Procesar el cursor
                    Dim cursor As OracleRefCursor = CType(pCursor.Value, OracleRefCursor)
                    If cursor IsNot Nothing Then
                        Dim reader As OracleDataReader = cursor.GetDataReader()
                        If reader IsNot Nothing Then
                            While reader.Read()
                                Dim producto As New CE_Productos With {
                                    .Id = Convert.ToInt32(reader("ID")),
                                    .Nombre = reader("NOMBRE").ToString(),
                                    .Precio = Convert.ToDecimal(reader("PRECIO")),
                                    .Descripcion = reader("DESCRIPCION").ToString(),
                                    .FechaCreacion = Convert.ToDateTime(reader("FECHA_CREACION"))
                                }
                                productos.Add(producto)
                            End While
                            reader.Close()
                            reader.Dispose()
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener productos: " & ex.Message, ex)
        End Try

        Return productos
    End Function
End Class
```

#### Paso 3: Crear el servicio de negocio

**Archivo: Muebleria.Negocio\[TuEntidad]\CN_ProductosService.vb**

```visualbasic
Imports Muebleria.Datos
Imports Muebleria.Entidades

Public Class CN_ProductosService
    Private ReadOnly _productosData As CD_Productos

    Public Sub New()
        _productosData = New CD_Productos()
    End Sub

    Public Function ObtenerProductos() As List(Of CE_Productos)
        Try
            Return _productosData.ObtenerProductos()
        Catch ex As Exception
            Throw New Exception("Error en la lógica de negocio al obtener productos: " & ex.Message, ex)
        End Try
    End Function
End Class
```

#### Paso 4: Crear el controlador

**Archivo: Muebleria.Presentacion\Controllers\ProductosController.vb**

```visualbasic
Imports Muebleria.Negocio
Imports Muebleria.Entidades

Namespace Controllers
    Public Class ProductosController
        Inherits Controller

        Private ReadOnly _productosService As CN_ProductosService

        Public Sub New()
            _productosService = New CN_ProductosService()
        End Sub

        ' GET: Productos
        Function Index() As ActionResult
            Try
                Dim productos = _productosService.ObtenerProductos()
                Return View(productos)
            Catch ex As Exception
                ViewBag.Error = "Error al cargar productos: " & ex.Message
                Return View(New List(Of CE_Productos)())
            End Try
        End Function
    End Class
End Namespace
```

### 3.4 Tipos de parámetros en Oracle

#### Parámetros de ENTRADA (Input)

```visualbasic
Dim pNombre As New OracleParameter("p_nombre", OracleDbType.Varchar2)
pNombre.Direction = System.Data.ParameterDirection.Input
pNombre.Value = "Juan"
cmd.Parameters.Add(pNombre)
```

#### Parámetros de SALIDA (Output)

```visualbasic
Dim pCursor As New OracleParameter("p_cursor", OracleDbType.RefCursor)
pCursor.Direction = System.Data.ParameterDirection.Output
cmd.Parameters.Add(pCursor)
```

#### Parámetros de ENTRADA/SALIDA (InputOutput)

```visualbasic
Dim pId As New OracleParameter("p_id", OracleDbType.Int32)
pId.Direction = System.Data.ParameterDirection.InputOutput
pId.Value = 1
cmd.Parameters.Add(pId)
```

### 3.5 Tipos de datos Oracle → .NET

| Oracle | .NET | OracleDbType |
|--------|-----|--------------|
| VARCHAR2 | String | Varchar2 |
| NUMBER | Decimal / Int32 | Decimal / Int32 |
| DATE | DateTime | Date |
| TIMESTAMP | DateTime | TimeStamp |
| CLOB | String | Clob |
| REF CURSOR | DataReader | RefCursor |
| BLOB | Byte[] | Blob |

### 3.6 Manejo de RefCursor (Cursor como parámetro OUT)

**Importante:** Los RefCursor de Oracle requieren llamar a `.GetDataReader()`:

```visualbasic
' ❌ INCORRECTO - Causará error de casting
Dim reader As OracleDataReader = CType(pCursor.Value, OracleDataReader)

' ✅ CORRECTO - Forma adecuada
Dim cursor As OracleRefCursor = CType(pCursor.Value, OracleRefCursor)
Dim reader As OracleDataReader = cursor.GetDataReader()
```

### 3.7 Mejor práctica: Usar Using para recursos

```visualbasic
Using conn As New OracleConnection(_connectionString)
    Using cmd As New OracleCommand(...)
        ' ... tu código
    End Using
End Using
```

Esto garantiza que los recursos se liberen correctamente incluso si ocurre una excepción.

---

## 4. Checklist de Configuración

- [ ] Editar `Web.config` con la cadena de conexión correcta
- [ ] Verificar que el servidor Oracle esté disponible
- [ ] Probar conexión usando la opción Test DB
- [ ] Instalar Oracle.ManagedDataAccess NuGet si es necesario
- [ ] Crear las entidades en `Muebleria.Entidades`
- [ ] Crear las clases de datos en `Muebleria.Datos`
- [ ] Crear los servicios en `Muebleria.Negocio`
- [ ] Crear los controladores en `Muebleria.Presentacion`
- [ ] Crear las vistas correspondientes
- [ ] Probar la funcionalidad completa

---

## 5. Solución de problemas

### Error: "Unable to load Oracle.ManagedDataAccess"
**Solución:** Instala el paquete NuGet `Oracle.ManagedDataAccess`

### Error: "ORA-12514 TNS:listener does not know of service"
**Solución:** Verifica que el SID/PDB sea correcto en la cadena de conexión

### Error: "ORA-01017 invalid username/password"
**Solución:** Verifica el usuario y contraseña en la cadena de conexión

### Error: "Connection Timeout"
**Solución:** Verifica que el servidor Oracle esté accesible en la IP y puerto especificados

---

## 6. Contacto y Soporte

Para dudas o problemas adicionales, puedes contactar al desarrollador o consultar la documentación oficial:

**Contacto del Desarrollador:**
- 📧 Email: [martir.dev@gmail.com](mailto:martir.dev@gmail.com)

**Documentación Oficial:**
- [Oracle Data Provider for .NET](https://www.oracle.com/database/technologies/net-data-access.html)
- [ASP.NET MVC Documentation](https://docs.microsoft.com/aspnet/mvc)
- [Oracle Database Documentation](https://docs.oracle.com/en/database/)
