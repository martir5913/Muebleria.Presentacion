Public Class CategoryIcons

    Private Shared ReadOnly _icons As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
        {"Todos", "&#127968;"},
        {"Sala", "&#128715;"},
        {"Comedor", "&#127869;"},
        {"Habitaciones", "&#128716;"},
        {"Jardin", "&#127807;"},
        {"Jardín", "&#127807;"},
        {"Oficina", "&#128188;"},
        {"Iluminacion", "&#128161;"},
        {"Iluminación", "&#128161;"},
        {"Ofertas", "&#128293;"}
    }

    Public Shared Function GetIcon(cat As String) As String
        Dim icon As String = Nothing
        If Not String.IsNullOrEmpty(cat) AndAlso _icons.TryGetValue(cat, icon) Then
            Return icon
        End If
        Return "&#128230;"
    End Function

End Class
