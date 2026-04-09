@ModelType IEnumerable(Of Muebleria.Entidades.CE_CarritoItem)
@Code
    ViewData("Title") = "Mi Carrito"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2 class="mb-4">🛒 Mi Carrito</h2>

@If TempData("Exito") IsNot Nothing Then
    @<div class="alert alert-success">@TempData("Exito")</div>
End If
@If TempData("Error") IsNot Nothing Then
    @<div class="alert alert-danger">@TempData("Error")</div>
End If

@If Model.Any() Then
    @<table class="table table-hover shadow-sm">
        <thead class="table-dark">
            <tr>
                <th>Producto</th>
                <th>Precio Unit.</th>
                <th>Cantidad</th>
                <th>Subtotal</th>
                <th>Eliminar</th>
            </tr>
        </thead>
        <tbody>
            @For Each item In Model
                @<tr>
                    <td>@item.NombreProducto</td>
                    <td>Q @item.PrecioUnitario.ToString("N2")</td>
                    <td>@item.Cantidad</td>
                    <td>Q @item.Subtotal.ToString("N2")</td>
                    <td>
                        @Using Html.BeginForm("EliminarItem", "Carrito", FormMethod.Post)
                            @Html.Hidden("itemId", item.ItemId)
                            @<button type="submit" class="btn btn-danger btn-sm"
                                     onclick="return confirm('¿Eliminar este producto?')">
                                ✕
                            </button>
                        End Using
                    </td>
                </tr>
            Next
        </tbody>
        <tfoot>
            <tr class="table-success fw-bold">
                <td colspan="3" class="text-end">TOTAL:</td>
                <td colspan="2">Q @ViewBag.Total.ToString("N2")</td>
            </tr>
        </tfoot>
    </table>

    <div class="card shadow-sm p-4 mt-3" style="max-width:500px">
    <h5> Forma de Pago</h5>
@Using Html.BeginForm("Comprar", "Carrito", FormMethod.Post)
            @<div class="mb-3">
                <select name="formaPago" class="form-select">
                    <option value="TARJETA">Tarjeta de Crédito/Débito</option>
                    <option value="EFECTIVO">Efectivo</option>
                    <option value="TRANSFERENCIA">Transferencia Bancaria</option>
                </select>
            </div>
            @<button type="submit" class="btn btn-success w-100"
                     onclick="return confirm('¿Confirmas tu compra?')">
                💳 Efectuar Compra
            </button>
            End Using
    </div>
Else
@<div class="alert alert-info">
    Tu carrito está vacío.
    @Html.ActionLink("Ver productos", "Index", "Catalogo", Nothing, New With {.class = "alert-link"})
</div>
End If

<div class="mt-3">
    @Html.ActionLink("← Seguir comprando", "Index", "Catalogo", Nothing, New With {.class = "btn btn-outline-secondary"})
</div>
