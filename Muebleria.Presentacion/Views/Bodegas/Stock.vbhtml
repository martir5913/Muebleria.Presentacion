@ModelType List(Of CE_StockBodega)
@Code
    ViewData("Title") = "Stock de Bodega"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .page-header { display:flex; align-items:center; justify-content:space-between; margin-bottom:24px; }
    .page-title { font-family:var(--font-display); font-size:24px; color:var(--primary); }
    .tabla-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); overflow:hidden; }
    .tabla-card table { width:100%; border-collapse:collapse; }
    .tabla-card th { background:var(--primary); color:#F5DEB3; font-size:11px; text-transform:uppercase; letter-spacing:.06em; padding:12px 16px; text-align:left; }
    .tabla-card td { padding:12px 16px; border-bottom:1px solid var(--border); font-size:14px; }
    .tabla-card tr:last-child td { border-bottom:none; }
    .tabla-card tr:hover td { background:var(--cream-dark); }
    .empty { text-align:center; padding:48px; color:var(--text-muted); }
    .badge-cant { background:var(--accent); color:#fff; border-radius:20px; padding:2px 12px; font-size:12px; font-weight:600; }
</style>

<div class="page-header">
    <h1 class="page-title">Stock — Bodega #@ViewBag.BodegaId</h1>
    @Html.ActionLink("← Volver", "Index", "Bodegas", Nothing, New With {.style = "color:var(--accent);text-decoration:none;font-size:14px;"})
</div>

<div class="tabla-card">
    @If Model IsNot Nothing AndAlso Model.Count > 0 Then
    @<table>
        <thead>
            <tr>
                <th>Producto</th>
                <th>Referencia</th>
                <th>Bodega</th>
                <th>Cantidad</th>
            </tr>
        </thead>
        <tbody>
            @For Each s In Model
            @<tr>
                <td><strong>@s.NombreProducto</strong></td>
                <td>@s.ReferenciaProducto</td>
                <td>@s.NombreBodega</td>
                <td><span class="badge-cant">@s.Cantidad</span></td>
            </tr>
            Next
        </tbody>
    </table>
    Else
    @<div class="empty">Esta bodega no tiene stock registrado.</div>
    End If
</div>
