@Code
    ViewData("Title") = "Nueva Sugerencia"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .sug-wrap { max-width:560px; margin:0 auto; }
    .sug-card { background:var(--white); border-radius:var(--radius-lg); box-shadow:var(--shadow); padding:32px; }
    .sug-titulo { font-family:var(--font-display); font-size:24px; color:var(--primary); margin-bottom:8px; }
    .sug-sub { font-size:14px; color:var(--text-muted); margin-bottom:24px; padding-bottom:16px; border-bottom:1px solid var(--border); }
    .form-group { margin-bottom:18px; }
    .form-group label { display:block; font-size:12px; font-weight:600; color:var(--text-mid); text-transform:uppercase; letter-spacing:.06em; margin-bottom:8px; }
    .form-group select, .form-group textarea { width:100%; padding:10px 14px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:14px; color:var(--text); background:var(--cream); outline:none; transition:border-color .2s; }
    .form-group textarea { resize:vertical; min-height:120px; }
    .form-group select:focus, .form-group textarea:focus { border-color:var(--accent); }
    .btn-enviar { background:var(--accent); color:var(--white); border:none; border-radius:10px; padding:12px 32px; font-size:14px; font-weight:600; cursor:pointer; font-family:var(--font-body); }
    .btn-enviar:hover { background:#b07830; }
</style>

@If TempData("Exito") IsNot Nothing Then
@<div style="background:#EAF6EE;border-left:4px solid green;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:green;font-size:13px;">@TempData("Exito")</div>
End If
@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="sug-wrap">
    <div class="sug-card">
        <h1 class="sug-titulo">Nueva Sugerencia</h1>
        <p class="sug-sub">Tu opinión nos ayuda a mejorar. Comparte lo que piensas sobre nuestros productos o servicio.</p>

        @Using Html.BeginForm("Sugerencias", "Clientes", FormMethod.Post)
            @<div class="form-group">
                <label>Tipo</label>
                <select name="tipo">
                    <option value="SUGERENCIA">Sugerencia</option>
                    <option value="QUEJA">Queja</option>
                    <option value="RECLAMO">Reclamo</option>
                </select>
            </div>
            @<div class="form-group">
                <label>Mensaje *</label>
                <textarea name="mensaje" placeholder="Escribe tu mensaje aquí..." required></textarea>
            </div>
            @<button type="submit" class="btn-enviar">Enviar</button>
        End Using
    </div>

    <div style="margin-top:16px;">
        @Html.ActionLink("Volver al perfil", "Perfil", "Clientes", Nothing, New With {.style = "font-size:13px;color:var(--text-muted);"})
    </div>
</div>
