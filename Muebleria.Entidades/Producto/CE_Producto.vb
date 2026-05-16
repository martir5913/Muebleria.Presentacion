Namespace Muebleria.Entidades
    Public Class CE_Producto
        Public Property ProductoId As Integer
        Public Property Referencia As String
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property TipoMuebleId As Integer    ' FK a MDA_TIPOS_MUEBLE
        Public Property TipoMuebleCodigo As String  ' INTERIOR / EXTERIOR
        Public Property TipoMuebleNombre As String  ' Sala, Comedor, Habitaciones, etc.
        Public Property Material As String
        Public Property DimAltoCm As Decimal
        Public Property DimAnchoCm As Decimal
        Public Property DimProfCm As Decimal
        Public Property Color As String
        Public Property PesoGramos As Integer
        Public Property FotoUrl As String
        Public Property Activo As String   ' 'S' / 'N'
        ' Inventario (de MDA_INVENTARIO) lo manejamos aparte, pero por simplicidad lo incluimos aquí
        Public Property Precio As Decimal
        Public Property Stock As Integer
        Public Property CreatedAt As DateTime
        Public Property UpdatedAt As DateTime
        Public Property EsNuevo As Boolean
        Public Property EsMasVendido As Boolean
        Public Property Descuento As Nullable(Of Integer)

        Public Sub New()
            Activo = "S"
        End Sub
    End Class
End Namespace