@ModelType CE_Bodega
@Code
    ViewData("Title") = "Editar Bodega"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim estadoItems As New List(Of SelectListItem) From {
        New SelectListItem With {.Value = "S", .Text = "Activa", .Selected = (If(Model.Activo, "S") = "S")},
        New SelectListItem With {.Value = "I", .Text = "Inactiva", .Selected = (Model.Activo = "I")}
    }
End Code

<style>
    .form-wrap { max-width:520px; margin:0 auto; }
    .form-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:32px; }
    .form-titulo { font-family:var(--font-display); font-size:22px; color:var(--primary); margin-bottom:24px; padding-bottom:10px; border-bottom:2px solid var(--border); }
    .form-group { margin-bottom:16px; }
    .form-group label { display:block; font-size:12px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.05em; margin-bottom:5px; }
    .form-group input, .form-group select { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:14px; background:var(--cream); outline:none; }
    .form-group input:focus, .form-group select:focus { border-color:var(--accent); }
    .form-acciones { display:flex; gap:12px; margin-top:8px; }
    .btn-guardar { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; cursor:pointer; }
    .btn-cancelar { background:none; border:1.5px solid var(--border); color:var(--text-mid); border-radius:10px; padding:12px 28px; font-size:14px; font-weight:600; text-decoration:none; display:inline-flex; align-items:center; }
</style>

@If ViewBag.Error IsNot Nothing Then
    @<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="form-wrap">
    <div class="form-card">
        <h1 class="form-titulo">Editar Bodega #@Model.BodegaId</h1>
        @Using Html.BeginForm("Editar", "Bodegas", FormMethod.Post)
            @Html.Hidden("id", Model.BodegaId)
            @<div class="form-group">
                <label>Nombre *</label>
                @Html.TextBox("nombre", Model.Nombre, New With {.required = "required"})
            </div>
            @<div class="form-group">
                <label>Dirección</label>
                @Html.TextBox("direccion", Model.Direccion)
            </div>
            @<div class="form-group">
                <label>Teléfono</label>
                @Html.TextBox("telefono", Model.Telefono)
            </div>
            @<div class="form-group">
                <label>Estado</label>
                @Html.DropDownList("activo", estadoItems)
            </div>
            @<div class="form-acciones">
                <button type="submit" class="btn-guardar">Guardar Cambios</button>
                @Html.ActionLink("Cancelar", "Index", "Bodegas", Nothing, New With {.class = "btn-cancelar"})
            </div>
        End Using
    </div>
</div>
