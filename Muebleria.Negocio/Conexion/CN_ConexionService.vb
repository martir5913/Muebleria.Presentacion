Imports Muebleria.Datos
Public Class CN_ConexionService
    Private ReadOnly _conexion As CD_Conexion

    Public Sub New()
        _conexion = New CD_Conexion()
    End Sub

    Public Function TestConexion() As Boolean
        Return _conexion.ProbarConexion()
    End Function

End Class
