@ModelType CE_Producto
@Code
    ViewData("Title") = "Editar Producto"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .form-wrap { max-width:640px; margin:0 auto; }
    .form-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:32px; }
    .form-titulo { font-family:var(--font-display); font-size:24px; color:var(--primary); margin-bottom:24px; padding-bottom:12px; border-bottom:2px solid var(--border); }
    .form-row { display:grid; grid-template-columns:1fr 1fr; gap:16px; }
    .form-group { margin-bottom:18px; }
    .form-group label { display:block; font-size:12px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.06em; margin-bottom:6px; }
    .form-group input, .form-group select, .form-group textarea { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:14px; color:var(--text); background:var(--cream); outline:none; transition:border-color .2s; }
    .form-group input:focus, .form-group select:focus, .form-group textarea:focus { border-color:var(--accent); }
    .form-acciones { display:flex; gap:12px; margin-top:8px; }
    .btn-guardar { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; cursor:pointer; transition:background .15s; font-family:var(--font-body); }
    .btn-guardar:hover { background:#b07830; }
    .btn-cancelar { background:none; border:1.5px solid var(--border); color:var(--text-mid); border-radius:10px; padding:12px 28px; font-size:14px; font-weight:600; text-decoration:none; display:inline-flex; align-items:center; transition:border-color .15s; }
    .btn-cancelar:hover { border-color:var(--primary); color:var(--primary); }
</style>

@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="form-wrap">
    <div class="form-card">
        <h1 class="form-titulo">Editar Producto</h1>

        @Using Html.BeginForm("Editar", "Productos", FormMethod.Post)
            @Html.Hidden("ProductoId", Model.ProductoId)
            @<div class="form-row">
                <div class="form-group">
                    <label>Nombre *</label>
                    @Html.TextBox("Nombre", Model.Nombre, New With {.class = "form-control", .required = "required"})
                </div>
                <div class="form-group">
                    <label>Referencia *</label>
                    @Html.TextBox("Referencia", Model.Referencia, New With {.class = "form-control", .required = "required"})
                </div>
            </div>
            @<div class="form-group">
                <label>Descripción</label>
                @Html.TextArea("Descripcion", Model.Descripcion, 3, 0, New With {.class = "form-control"})
            </div>
            @<div class="form-row">
                <div class="form-group">
                    <label>Precio (Q) *</label>
                    @Html.TextBox("Precio", Model.Precio, New With {.class = "form-control", .type = "number", .step = "0.01", .min = "0", .required = "required"})
                </div>
                <div class="form-group">
                    <label>Stock</label>
                    @Html.TextBox("Stock", Model.Stock, New With {.class = "form-control", .type = "number", .min = "0"})
                </div>
            </div>
            @<div class="form-row">
                <div class="form-group">
                    <label>Estado</label>
                    @Html.DropDownList("Activo", New List(Of SelectListItem) From {
                        New SelectListItem With {.Text = "Activo", .Value = "S", .Selected = (Model.Activo = "S")},
                        New SelectListItem With {.Text = "Inactivo", .Value = "N", .Selected = (Model.Activo = "N")}
                    }, New With {.class = "form-control"})
                </div>
                <div class="form-group">
                    <label>URL Foto</label>
                    @Html.TextBox("FotoUrl", Model.FotoUrl, New With {.class = "form-control"})
                </div>
            </div>
            @<div class="form-acciones">
                <button type="submit" class="btn-guardar">Guardar cambios</button>
                @Html.ActionLink("Cancelar", "Index", "Productos", Nothing, New With {.class = "btn-cancelar"})
            </div>
        End Using
    </div>
</div>
