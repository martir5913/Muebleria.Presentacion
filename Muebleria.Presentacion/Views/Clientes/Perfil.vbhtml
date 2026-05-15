@ModelType CE_Cliente
@Code
    ViewData("Title") = "Mi Perfil"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .perfil-wrap { max-width:600px; margin:0 auto; }
    .perfil-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:32px; }
    .perfil-titulo { font-family:var(--font-display); font-size:24px; color:var(--primary); margin-bottom:24px; padding-bottom:12px; border-bottom:2px solid var(--border); }
    .form-row { display:grid; grid-template-columns:1fr 1fr; gap:14px; }
    .form-group { margin-bottom:16px; }
    .form-group label { display:block; font-size:12px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.06em; margin-bottom:6px; }
    .form-group input { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:14px; color:var(--text); background:var(--cream); outline:none; transition:border-color .2s; }
    .form-group input:focus { border-color:var(--accent); }
    .form-group input[readonly] { background:#f5f0e8; color:var(--text-muted); cursor:not-allowed; }
    .btn-guardar { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; cursor:pointer; font-family:var(--font-body); }
    .btn-guardar:hover { background:#b07830; }
    .acciones-bottom { display:flex; gap:12px; margin-top:24px; }
    .perfil-note { font-size:12px; color:var(--text-muted); margin-bottom:20px; }
</style>

@If TempData("Exito") IsNot Nothing Then
@<div style="background:#EAF6EE;border-left:4px solid green;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:green;font-size:13px;">@TempData("Exito")</div>
End If
@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="perfil-wrap">
    <div class="perfil-card">
        <h1 class="perfil-titulo">Mi Perfil</h1>
        <p class="perfil-note">Puedes actualizar tu email, teléfonos y dirección. Nombre y documento no son editables.</p>

        @Using Html.BeginForm("EditarPerfil", "Clientes", FormMethod.Post)
            @<div class="form-row">
                <div class="form-group">
                    <label>Nombres</label>
                    <input type="text" value="@Model.Nombres" readonly />
                </div>
                <div class="form-group">
                    <label>Apellidos</label>
                    <input type="text" value="@Model.Apellidos" readonly />
                </div>
            </div>
            @<div class="form-group">
                <label>Email *</label>
                <input type="email" name="email" value="@Model.Correo" required />
            </div>
            @<div class="form-row">
                <div class="form-group">
                    <label>Tel. Residencia *</label>
                    <input type="text" name="telResidencia" value="@Model.TelResidencia" required />
                </div>
                <div class="form-group">
                    <label>Tel. Celular</label>
                    <input type="text" name="telCelular" value="@Model.TelCelular" />
                </div>
            </div>
            @<div class="form-group">
                <label>Dirección *</label>
                <input type="text" name="dirección" value="@Model.Direccion" required />
            </div>
            @<div class="acciones-bottom">
                <button type="submit" class="btn-guardar">Guardar cambios</button>
            </div>
        End Using
    </div>

    <div style="margin-top:20px;text-align:right;">
        @Html.ActionLink("Enviar sugerencia", "Sugerencias", "Clientes", Nothing, New With {.style = "font-size:13px;color:var(--accent);"})
    </div>
</div>
