@ModelType IEnumerable(Of CE_CarritoItem)
@Code
    ViewData("Title") = "Mi Carrito"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .carrito-layout { display:grid; grid-template-columns:1fr 320px; gap:28px; align-items:start; }
    .carrito-tabla { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); overflow:hidden; }
    .carrito-tabla table { width:100%; border-collapse:collapse; }
    .carrito-tabla thead { background:var(--primary); color:var(--white); }
    .carrito-tabla thead th { padding:14px 16px; font-size:12px; font-weight:600; text-transform:uppercase; letter-spacing:.06em; text-align:left; }
    .carrito-tabla tbody tr { border-bottom:1px solid var(--border); }
    .carrito-tabla tbody td { padding:14px 16px; font-size:14px; vertical-align:middle; }
    .prod-nombre { font-weight:500; color:var(--primary); }
    .subtotal-cell { font-weight:600; color:var(--accent); font-size:15px; }
    .btn-eliminar { background:none; border:1.5px solid #E74C3C; color:#E74C3C; border-radius:6px; padding:5px 10px; font-size:12px; cursor:pointer; transition:background .15s; }
    .btn-eliminar:hover { background:#E74C3C; color:white; }
    .resumen-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:24px; position:sticky; top:80px; }
    .resumen-titulo { font-family:var(--font-display); font-size:20px; color:var(--primary); margin-bottom:20px; padding-bottom:12px; border-bottom:2px solid var(--border); }
    .resumen-linea { display:flex; justify-content:space-between; font-size:14px; margin-bottom:10px; color:var(--text-mid); }
    .resumen-total { display:flex; justify-content:space-between; font-size:20px; font-weight:700; color:var(--primary); border-top:2px solid var(--border); padding-top:14px; margin-top:10px; margin-bottom:20px; }
    .select-pago { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:13px; color:var(--text); background:var(--cream); margin-bottom:14px; outline:none; }
    .btn-comprar { width:100%; background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:14px; font-size:15px; font-weight:700; cursor:pointer; transition:background .15s; font-family:var(--font-body); }
    .btn-comprar:hover { background:#b07830; }
    .btn-vaciar { width:100%; background:none; border:1.5px solid var(--border); color:var(--text-muted); border-radius:8px; padding:10px; font-size:13px; cursor:pointer; margin-top:8px; font-family:var(--font-body); }
    .btn-vaciar:hover { border-color:#E74C3C; color:#E74C3C; }
    .empty-carrito { text-align:center; padding:60px 20px; }
    .label-pago { font-size:12px; font-weight:600; color:var(--text-mid); margin-bottom:6px; display:block; text-transform:uppercase; letter-spacing:.06em; }
    @@media(max-width:768px){ .carrito-layout{ grid-template-columns:1fr; } }
</style>

@If TempData("Exito") IsNot Nothing Then
@<div style="background:#EAF6EE;border-left:4px solid green;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:green;font-size:13px;">@TempData("Exito")</div>
End If
@If TempData("Error") IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@TempData("Error")</div>
End If

<h1 style="font-family:var(--font-display);font-size:28px;color:var(--primary);margin-bottom:20px;">Mi Carrito</h1>

@Code
    If Not Model.Any() Then
End Code
<div class="empty-carrito">
    <h3 style="color:var(--primary);font-family:var(--font-display);">Tu carrito está vacío</h3>
    <p style="color:var(--text-muted);margin:10px 0 20px;">Agrega productos desde el catálogo.</p>
    @Html.ActionLink("Ver Catálogo", "Index", "Catalogo", Nothing, New With {.class = "btn-comprar", .style = "display:inline-block;width:auto;padding:12px 30px;text-decoration:none;"})
</div>
@Code
    Else
End Code
<div class="carrito-layout">
    <div class="carrito-tabla">
        <table>
            <thead>
                <tr><th>Producto</th><th>Cantidad</th><th>Precio unit.</th><th>Subtotal</th><th></th></tr>
            </thead>
            <tbody>
@Code
                For Each item In Model
End Code
                <tr>
                    <td><div class="prod-nombre">@item.NombreProducto</div></td>
                    <td>@item.Cantidad</td>
                    <td style="color:var(--text-muted);">Q @item.PrecioUnitario.ToString("N2")</td>
                    <td class="subtotal-cell">Q @item.Subtotal.ToString("N2")</td>
                    <td>
                        <form action="@Url.Action("EliminarItem","Carrito")" method="post" style="display:inline">
                            <input type="hidden" name="productoId" value="@item.ProductoId" />
                            <button type="submit" class="btn-eliminar">Eliminar</button>
                        </form>
                    </td>
                </tr>
@Code
                Next
End Code
            </tbody>
        </table>
    </div>

    <div class="resumen-card">
        <div class="resumen-titulo">Resumen</div>
@Code
        For Each item In Model
End Code
        <div class="resumen-linea">
            <span>@item.NombreProducto x@item.Cantidad</span>
            <span>Q @item.Subtotal.ToString("N2")</span>
        </div>
@Code
        Next
End Code
        <div class="resumen-total">
            <span>TOTAL</span>
            <span>Q @ViewBag.Total.ToString("N2")</span>
        </div>
        <form action="@Url.Action("Comprar","Carrito")" method="post">
            @Html.AntiForgeryToken()
            <label class="label-pago">Forma de pago</label>
            <select name="formaPago" class="select-pago" required>
                <option value="">-- Selecciona --</option>
                <option value="TARJETA">Tarjeta</option>
                <option value="EFECTIVO">Efectivo</option>
                <option value="TRANSFERENCIA">Transferencia</option>
            </select>
            <button type="submit" class="btn-comprar">Confirmar compra</button>
        </form>
        <form action="@Url.Action("Vaciar","Carrito")" method="post">
            <button type="submit" class="btn-vaciar" onclick="return confirm('¿Vaciar todo el carrito?')">Vaciar carrito</button>
        </form>
        <div style="margin-top:14px;text-align:center;">
            @Html.ActionLink("Seguir comprando", "Index", "Catalogo", Nothing, New With {.style = "font-size:13px;color:var(--text-muted);"})
        </div>
    </div>
</div>
@Code
    End If
End Code
