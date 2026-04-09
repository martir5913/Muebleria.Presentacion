@Code
    ViewData("Title") = "Crear Cuenta"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row justify-content-center mt-4">
    <div class="col-md-6">
        <div class="card shadow">
            <div class="card-body p-4">
                <h2 class="card-title mb-4">Crear Cuenta</h2>

                @If ViewBag.Error IsNot Nothing Then
                    @<div class="alert alert-danger">@ViewBag.Error</div>
                End If

                @Using Html.BeginForm("Registro", "Cuenta", FormMethod.Post)
                    @<div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Nombres *</label>
                            @Html.TextBox("nombres", "", New With {.class = "form-control"})
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Apellidos *</label>
                            @Html.TextBox("apellidos", "", New With {.class = "form-control"})
                        </div>
                    </div>
                    @<div class="mb-3">
                        <label class="form-label">Email *</label>
                        @Html.TextBox("email", "", New With {.class = "form-control", .type = "email"})
                    </div>
                    @<div class="mb-3">
                        <label class="form-label">Dirección *</label>
                        @Html.TextBox("direccion", "", New With {.class = "form-control"})
                    </div>
                    @<div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Usuario *</label>
                            @Html.TextBox("username", "", New With {.class = "form-control"})
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Contraseña * (mín. 6 caracteres)</label>
                            @Html.Password("password", "", New With {.class = "form-control"})
                        </div>
                    </div>
                    @<button type="submit" class="btn btn-primary w-100">Crear Cuenta</button>
                End Using

                <hr />
                <p class="text-center mb-0">
                    ¿Ya tienes cuenta? @Html.ActionLink("Inicia sesión aquí", "Login", "Cuenta")
                </p>
            </div>
        </div>
    </div>
</div>
