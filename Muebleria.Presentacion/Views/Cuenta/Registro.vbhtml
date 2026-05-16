@Code
    ViewData("Title") = "Crear Cuenta"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .reg-wrap { max-width:560px; margin:40px auto; }
    .reg-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:36px; }
    .reg-titulo { font-family:var(--font-display); font-size:24px; color:var(--primary); margin-bottom:24px; padding-bottom:12px; border-bottom:2px solid var(--border); }
    .form-row { display:grid; grid-template-columns:1fr 1fr; gap:14px; }
    .form-group { margin-bottom:16px; }
    .form-group label { display:block; font-size:12px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.05em; margin-bottom:5px; }
    .form-group input, .form-group select { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:14px; color:var(--text); background:var(--cream); outline:none; transition:border-color .2s; }
    .form-group input:focus, .form-group select:focus { border-color:var(--accent); }
    .btn-reg { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:13px 0; width:100%; font-size:15px; font-weight:600; cursor:pointer; margin-top:6px; font-family:var(--font-body); }
    .btn-reg:hover { background:#b07830; }
    .reg-footer { text-align:center; margin-top:18px; font-size:14px; color:var(--text-muted); }
    .reg-footer a { color:var(--accent); text-decoration:none; }
</style>

@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="reg-wrap">
    <div class="reg-card">
        <h1 class="reg-titulo">Crear Cuenta</h1>

        @Using Html.BeginForm("Registro", "Cuenta", FormMethod.Post)
            @<div class="form-row">
                <div class="form-group">
                    <label>Nombres *</label>
                    @Html.TextBox("nombres", "", New With {.required = "required"})
                </div>
                <div class="form-group">
                    <label>Apellidos *</label>
                    @Html.TextBox("apellidos", "", New With {.required = "required"})
                </div>
            </div>
            @<div class="form-row">
                <div class="form-group">
                    <label>No. Documento *</label>
                    @Html.TextBox("numDocumento", "", New With {.required = "required"})
                </div>
                <div class="form-group">
                    <label>Teléfono *</label>
                    @Html.TextBox("telResidencia", "", New With {.required = "required"})
                </div>
            </div>
            @<div class="form-group">
                <label>Email *</label>
                @Html.TextBox("email", "", New With {.type = "email", .required = "required"})
            </div>
            @<div class="form-group">
                <label>Dirección *</label>
                @Html.TextBox("direccion", "", New With {.required = "required"})
            </div>
            @<div class="form-row">
                <div class="form-group">
                    <label>Usuario *</label>
                    @Html.TextBox("username", "", New With {.required = "required"})
                </div>
                <div class="form-group">
                    <label>Contraseña * (mín. 6)</label>
                    @Html.Password("password", "", New With {.required = "required"})
                </div>
            </div>
            @<button type="submit" class="btn-reg">Crear Cuenta</button>
        End Using

        <div class="reg-footer">
            ¿Ya tienes cuenta? <a href="@Url.Action("Login","Cuenta")">Inicia sesión aquí</a>
        </div>
    </div>
</div>
