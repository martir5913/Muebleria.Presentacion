@ModelType IEnumerable(Of CE_OrdenDetalle)
@Code
    ViewData("Title") = "Compra Confirmada"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .confirm-wrap { max-width:600px; margin:0 auto; text-align:center; padding:40px 20px; }
    .confirm-icon { font-size:64px; color:#27AE60; margin-bottom:16px; }
    .confirm-titulo { font-family:var(--font-display); font-size:30px; color:var(--primary); margin-bottom:8px; }
    .confirm-orden { font-size:18px; color:var(--accent); font-weight:700; margin-bottom:24px; }
    .detalle-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); overflow:hidden; margin-bottom:24px; text-align:left; }
    .detalle-card table { width:100%; border-collapse:collapse; }
    .detalle-card thead { background:var(--primary); color:var(--white); }
    .detalle-card thead th { padding:12px 16px; font-size:12px; font-weight:600; text-transform:uppercase; letter-spacing:.06em; }
    .detalle-card tbody td { padding:12px 16px; font-size:14px; border-bottom:1px solid var(--border); }
    .total-row td { font-weight:700; font-size:16px; color:var(--accent); border-bottom:none; border-top:2px solid var(--border); }
    .btn-seguir { display:inline-block; background:var(--primary); color:var(--white); border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; text-decoration:none; transition:background .15s; margin:4px; }
    .btn-seguir:hover { background:var(--primary-light); color:var(--white); }
    .btn-perfil { display:inline-block; background:none; border:1.5px solid var(--primary); color:var(--primary); border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; text-decoration:none; transition:background .15s; margin:4px; }
    .btn-perfil:hover { background:var(--cream-dark); }
</style>

<div class="confirm-wrap">
    <div class="confirm-icon">&#10003;</div>
    <h1 class="confirm-titulo">Compra realizada</h1>
    <p class="confirm-orden">Orden @ViewBag.OrdenId</p>

    @If ViewBag.Error IsNot Nothing Then
    @<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
    End If

    @If Model IsNot Nothing AndAlso Model.Any() Then
    @<div class="detalle-card">
        <table>
            <thead>
                <tr><th>Producto</th><th>Cant.</th><th>Subtotal</th></tr>
            </thead>
            <tbody>
                @For Each d In Model
                @<tr>
                    <td>@d.NombreProducto</td>
                    <td>@d.Cantidad</td>
                    <td>Q @d.Subtotal.ToString("N2")</td>
                </tr>
                Next
                <tr class="total-row">
                    <td colspan="2">TOTAL</td>
                    <td>Q @ViewBag.Total</td>
                </tr>
            </tbody>
        </table>
    </div>
    End If

    @Html.ActionLink("Seguir comprando", "Index", "Catalogo", Nothing, New With {.class = "btn-seguir"})
    @Html.ActionLink("Mi Perfil", "Perfil", "Clientes", Nothing, New With {.class = "btn-perfil"})
</div>
