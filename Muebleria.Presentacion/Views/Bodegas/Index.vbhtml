@ModelType List(Of CE_Bodega)
@Code
    ViewData("Title") = "Bodegas"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .page-header { display:flex; align-items:center; justify-content:space-between; margin-bottom:24px; }
    .page-title { font-family:var(--font-display); font-size:26px; color:var(--primary); }
    .btn-nuevo { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:10px 24px; font-size:14px; font-weight:600; text-decoration:none; display:inline-flex; align-items:center; gap:6px; }
    .btn-nuevo:hover { background:#b07830; color:#fff; }
    .tabla-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); overflow:hidden; }
    .tabla-card table { width:100%; border-collapse:collapse; }
    .tabla-card th { background:var(--primary); color:#F5DEB3; font-size:11px; text-transform:uppercase; letter-spacing:.06em; padding:12px 16px; text-align:left; }
    .tabla-card td { padding:12px 16px; border-bottom:1px solid var(--border); font-size:14px; }
    .tabla-card tr:last-child td { border-bottom:none; }
    .tabla-card tr:hover td { background:var(--cream-dark); }
    .btn-accion { background:none; border:1.5px solid var(--border); border-radius:6px; padding:5px 12px; font-size:12px; font-weight:600; text-decoration:none; color:var(--text-mid); cursor:pointer; font-family:var(--font-body); }
    .btn-accion:hover { border-color:var(--accent); color:var(--accent); }
    .btn-elim { border-color:#e74c3c; color:#e74c3c; }
    .btn-elim:hover { background:#e74c3c; color:#fff; }
    .empty { text-align:center; padding:48px; color:var(--text-muted); }
</style>

@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If
@If TempData("Exito") IsNot Nothing Then
@<div style="background:#EAF7EE;border-left:4px solid #27AE60;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:#1a6e38;font-size:13px;">@TempData("Exito")</div>
End If

<div class="page-header">
    <h1 class="page-title">Bodegas y Stock</h1>
    @Html.ActionLink("+ Nueva Bodega", "Crear", "Bodegas", Nothing, New With {.class = "btn-nuevo"})
</div>

<div class="tabla-card">
@Code
    If Model IsNot Nothing AndAlso Model.Count > 0 Then
End Code
    <table>
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Dirección</th>
                <th>Ciudad</th>
                <th>Teléfono</th>
                <th>Responsable</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
@Code
            For Each b In Model
End Code
            <tr>
                <td>@b.BodegaId</td>
                <td><strong>@b.Nombre</strong></td>
                <td>@(If(b.Direccion, "-"))</td>
                <td>@(If(b.NombreCiudad, "-"))</td>
                <td>@(If(b.Telefono, "-"))</td>
                <td>@(If(b.NombreEmpleado, "-"))</td>
                <td>
                    <a href="@Url.Action("Stock","Bodegas",New With {.id=b.BodegaId})" class="btn-accion">Ver Stock</a>
                    <a href="@Url.Action("Editar","Bodegas",New With {.id=b.BodegaId})" class="btn-accion">Editar</a>
                    <form action="@Url.Action("Eliminar","Bodegas")" method="post" style="display:inline">
                        <input type="hidden" name="id" value="@b.BodegaId" />
                        <button type="submit" class="btn-accion btn-elim" onclick="return confirm('¿Desactivar esta bodega?')">Desactivar</button>
                    </form>
                </td>
            </tr>
@Code
            Next
        Else
End Code
    <div class="empty">No hay bodegas registradas.</div>
@Code
    End If
End Code
</div>
