@ModelType CE_Bodega
@Code
    ViewData("Title") = "Nueva Bodega"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .form-wrap { max-width:520px; margin:0 auto; }
    .form-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:32px; }
    .form-titulo { font-family:var(--font-display); font-size:22px; color:var(--primary); margin-bottom:24px; padding-bottom:10px; border-bottom:2px solid var(--border); }
    .form-group { margin-bottom:16px; }
    .form-group label { display:block; font-size:12px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.05em; margin-bottom:5px; }
    .form-group input { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:14px; background:var(--cream); outline:none; }
    .form-group input:focus { border-color:var(--accent); }
    .form-acciones { display:flex; gap:12px; margin-top:8px; }
    .btn-guardar { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; cursor:pointer; }
    .btn-cancelar { background:none; border:1.5px solid var(--border); color:var(--text-mid); border-radius:10px; padding:12px 28px; font-size:14px; font-weight:600; text-decoration:none; display:inline-flex; align-items:center; }
</style>

@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="form-wrap">
    <div class="form-card">
        <h1 class="form-titulo">Nueva Bodega</h1>
        @Using Html.BeginForm("Crear", "Bodegas", FormMethod.Post)
            @<div class="form-group">
                <label>Nombre *</label>
                @Html.TextBox("nombre", "", New With {.required = "required"})
            </div>
            @<div class="form-group">
                <label>Dirección</label>
                @Html.TextBox("direccion", "")
            </div>
            @<div class="form-group">
                <label>Teléfono</label>
                @Html.TextBox("telefono", "")
            </div>
            @<div class="form-acciones">
                <button type="submit" class="btn-guardar">Crear Bodega</button>
                @Html.ActionLink("Cancelar", "Index", "Bodegas", Nothing, New With {.class = "btn-cancelar"})
            </div>
        End Using
    </div>
</div>
