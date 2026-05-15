@Code
    ViewData("Title") = "Reportes"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .rep-header { display:flex; align-items:center; justify-content:space-between; margin-bottom:24px; }
    .rep-titulo { font-family:var(--font-display); font-size:26px; color:var(--primary); }
    .filtro-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:20px 24px; margin-bottom:24px; display:flex; align-items:flex-end; gap:16px; flex-wrap:wrap; }
    .filtro-group label { display:block; font-size:11px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.05em; margin-bottom:4px; }
    .filtro-group input { padding:9px 12px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:13px; background:var(--cream); outline:none; }
    .filtro-group input:focus { border-color:var(--accent); }
    .btn-filtrar { background:var(--accent); color:#fff; border:none; border-radius:10px; padding:10px 22px; font-size:13px; font-weight:600; cursor:pointer; }
    .stats-row { display:grid; grid-template-columns:repeat(auto-fit,minmax(220px,1fr)); gap:16px; margin-bottom:24px; }
    .stat-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:22px 24px; }
    .stat-label { font-size:11px; font-weight:600; color:var(--text-muted); text-transform:uppercase; letter-spacing:.06em; margin-bottom:8px; }
    .stat-value { font-family:var(--font-display); font-size:28px; color:var(--primary); font-weight:700; }
    .stat-sub { font-size:12px; color:var(--text-muted); margin-top:4px; }
    .tabla-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); overflow:hidden; }
    .tabla-card h2 { font-family:var(--font-display); font-size:18px; color:var(--primary); padding:18px 20px 0; }
    .tabla-card table { width:100%; border-collapse:collapse; margin-top:12px; }
    .tabla-card th { background:var(--primary); color:#F5DEB3; font-size:11px; text-transform:uppercase; letter-spacing:.06em; padding:10px 16px; text-align:left; }
    .tabla-card td { padding:10px 16px; border-bottom:1px solid var(--border); font-size:13px; }
    .tabla-card tr:last-child td { border-bottom:none; }
    .tabla-card tr:hover td { background:var(--cream-dark); }
    .empty { text-align:center; padding:36px; color:var(--text-muted); font-size:14px; }
</style>

@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="rep-header">
    <h1 class="rep-titulo">Reportes de Ventas</h1>
</div>

@* Filtro de fechas *@
@Using Html.BeginForm("Filtrar", "Reportes", FormMethod.Post)
@<div class="filtro-card">
    <div class="filtro-group">
        <label>Fecha Inicio</label>
        <input type="date" name="fechaIni" value="@ViewBag.FechaIni" />
    </div>
    <div class="filtro-group">
        <label>Fecha Fin</label>
        <input type="date" name="fechaFin" value="@ViewBag.FechaFin" />
    </div>
    <button type="submit" class="btn-filtrar">Filtrar</button>
</div>
End Using

@* Estadísticas resumen *@
@Code
    Dim ventas As List(Of CE_VentaDiaria) = TryCast(ViewBag.VentasDiarias, List(Of CE_VentaDiaria))
    Dim productoTop As CE_ProductoVendido = TryCast(ViewBag.ProductoTop, CE_ProductoVendido)
    Dim totalVentas As Decimal = 0
    Dim totalOrdenes As Integer = 0
    Dim totalUnidades As Integer = 0
    If ventas IsNot Nothing AndAlso ventas.Count > 0 Then
        totalVentas = ventas.Sum(Function(v) v.TotalVentas)
        totalOrdenes = ventas.Sum(Function(v) v.Ordenes)
        totalUnidades = ventas.Sum(Function(v) v.Unidades)
    End If
End Code

<div class="stats-row">
    <div class="stat-card">
        <div class="stat-label">Total Ventas</div>
        <div class="stat-value">Q @totalVentas.ToString("N2")</div>
        <div class="stat-sub">En el período seleccionado</div>
    </div>
    <div class="stat-card">
        <div class="stat-label">Órdenes</div>
        <div class="stat-value">@totalOrdenes</div>
        <div class="stat-sub">Confirmadas</div>
    </div>
    <div class="stat-card">
        <div class="stat-label">Unidades vendidas</div>
        <div class="stat-value">@totalUnidades</div>
        <div class="stat-sub">Muebles despachados</div>
    </div>
    @If productoTop IsNot Nothing Then
    @<div class="stat-card">
        <div class="stat-label">Producto más vendido</div>
        <div class="stat-value" style="font-size:18px;">@productoTop.Nombre</div>
        <div class="stat-sub">@productoTop.Unidades unidades · @productoTop.TipoMueble</div>
    </div>
    End If
</div>

@* Tabla detalle de ventas diarias *@
<div class="tabla-card">
    <h2>Ventas por Día</h2>
    @If ventas IsNot Nothing AndAlso ventas.Count > 0 Then
    @<table>
        <thead>
            <tr>
                <th>Fecha</th>
                <th>Tipo Mueble</th>
                <th>Ciudad</th>
                <th>Órdenes</th>
                <th>Unidades</th>
                <th>Total Ventas</th>
            </tr>
        </thead>
        <tbody>
            @For Each v In ventas
            @<tr>
                <td>@v.Fecha.ToString("yyyy-MM-dd")</td>
                <td>@v.TipoMueble</td>
                <td>@v.Ciudad</td>
                <td>@v.Ordenes</td>
                <td>@v.Unidades</td>
                <td><strong>Q @v.TotalVentas.ToString("N2")</strong></td>
            </tr>
            Next
        </tbody>
    </table>
    Else
    @<div class="empty">No hay ventas en el período seleccionado.</div>
    End If
</div>
