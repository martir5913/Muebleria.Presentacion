@ModelType Muebleria.Entidades.CE_Producto
@Code
    Dim esEditar = Model.ProductoId > 0
    ViewData("Title") = If(esEditar, "Editar Producto", "Nuevo Producto")
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row justify-content-center">
    <div class="col-md-6">
        <div class="card shadow">
            <div class="card-body p-4">
                <h3 class="mb-4">@ViewData("Title")</h3>

                @If ViewBag.Error IsNot Nothing Then
                    @<div class="alert alert-danger">@ViewBag.Error</div>
                End If

                @Using Html.BeginForm(If(esEditar, "Editar", "Crear"), "Productos", FormMethod.Post)
                    If esEditar Then
                        @Html.Hidden("ProductoId", Model.ProductoId)
                    End If

                    @<div class="mb-3">
                        <label class="form-label">Nombre *</label>
                        @Html.TextBox("Nombre", Model.Nombre, New With {.class = "form-control", .placeholder = "Nombre del producto"})
                    </div>
                    @<div class="mb-3">
                        <label class="form-label">Referencia *</label>
                        @Html.TextBox("Referencia", Model.Referencia, New With {.class = "form-control", .placeholder = "Ej: MESA001", .readonly = esEditar})
                    </div>
                    @<div class="mb-3">
                        <label class="form-label">Precio (Q) *</label>
                        @Html.TextBox("Precio", Model.Precio, New With {.class = "form-control", .type = "number", .step = "0.01", .min = "0"})
                    </div>

                    @<div class="d-flex gap-2 mt-3">
                        <button type="submit" class="btn btn-primary">
                            @If esEditar Then@("💾 Actualizar") Else @("➕ Crear Producto")
                                                        </button>
                                @Html.ActionLink("Cancelar", "Index", "Productos", Nothing, New With {.class = "btn btn-secondary"})
                                                    </div>
                                                End Using
                                            </div>
                                        </div>
                                    </div>
</div>
