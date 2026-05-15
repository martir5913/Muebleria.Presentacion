Public Class CE_Producto
    Public Property ProductoId As Integer
    Public Property Nombre As String
    Public Property Referencia As String
    Public Property Descripcion As String
    Public Property TipoMuebleId As Integer
    Public Property Material As String
    Public Property DimAltoCm As Decimal
    Public Property DimAnchoCm As Decimal
    Public Property DimProfCm As Decimal
    Public Property Color As String
    Public Property PesoGramos As Decimal
    Public Property FotoUrl As String
    Public Property Precio As Decimal
    Public Property Activo As String

    Public Sub New()
        Activo = "S"
    End Sub

    Public Sub New(id As Integer, nombre As String, referencia As String, descripcion As String,
                   tipoMuebleId As Integer, material As String, dimAlto As Decimal,
                   dimAncho As Decimal, dimProf As Decimal, color As String,
                   peso As Decimal, fotoUrl As String, precio As Decimal, activo As String)
        Me.ProductoId = id
        Me.Nombre = nombre
        Me.Referencia = referencia
        Me.Descripcion = descripcion
        Me.TipoMuebleId = tipoMuebleId
        Me.Material = material
        Me.DimAltoCm = dimAlto
        Me.DimAnchoCm = dimAncho
        Me.DimProfCm = dimProf
        Me.Color = color
        Me.PesoGramos = peso
        Me.FotoUrl = fotoUrl
        Me.Precio = precio
        Me.Activo = activo
    End Sub
End Class