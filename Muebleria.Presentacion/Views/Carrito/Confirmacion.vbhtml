@ModelType IEnumerable(Of Muebleria.Entidades.CE_OrdenDetalle)
@Code
    ViewData("Title") = "Compra Exitosa"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="text-center mt-4">
    <div class="display-1">✅</div>
    <h2 class="mt-3">¡Compra realizada con éxito!</h2>
    <p class="text-muted">Tu número de orden es:</p>
    <h1 class="display-4 text-success fw-bold">@ViewBag.OrdenId</h1>
    <p>Se ha enviado un correo de confirmación a tu email registrado.</p>
</div>

<div class="row justify-content-center mt-4">
    <div class="col-md-7">
        <h5>Detalle de tu pedido:</h5>
        <table class="table table-hover shadow-sm">
            <thead class="table-dark">
                <tr>
                    <th>Producto</th>
                    <th>Cantidad</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr>
                        <td>@item.NombreProducto</td>
                        <td>@item.Cantidad</td>
                        <td>Q @item.Subtotal.ToString("N2")</td>
                    </tr>
                Next
            </tbody>
            <tfoot>
                <tr class="table-success fw-bold">
                    <td colspan="2" class="text-end">TOTAL PAGADO:</td>
                    <td>Q @ViewBag.Total</td>
                </tr>
            </tfoot>
        </table>

        <div class="text-center mt-3">
            @Html.ActionLink("Seguir comprando", "Index", "Catalogo", Nothing, New With {.class = "btn btn-primary btn-lg"})
        </div>
    </div>
</div>
