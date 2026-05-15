@Code
    ViewData("Title") = "Administrar Productos"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<link href="~/Content/productos-admin.css" rel="stylesheet" />

<div class="container-fluid mt-4">
    <div class="d-flex justify-content-between align-items-center mb-4 pb-2 border-bottom border-dark">
        <h1 class="h2 text-dark m-0">Administrar Productos</h1>
        <button id="btnNuevoProducto" class="btn btn-primary font-weight-bold px-4">
            <i class="glyphicon glyphicon-plus"></i> + Nuevo Mueble
        </button>
    </div>

    <div class="panel panel-default shadow-sm">
        <div class="panel-body p-0">
            <div class="table-responsive">
                <table class="table table-striped table-hover m-0">
                    <thead style="background-color: #4A3525; color: white;">
                        <tr>
                            <th class="py-3 px-4">ID</th>
                            <th>Imagen</th>
                            <th>Producto / SKU</th>
                            <th>Material</th>
                            <th>Dimensiones (Al x An x Pr)</th>
                            <th>Precio</th>
                            <th class="text-center">Acciones</th>
                        </tr>
                    </thead>
                    <tbody id="tablaProductosCuerpo">
                        <tr>
                            <td colspan="7" class="text-center py-4">
                                <span class="text-muted">Cargando catálogo de muebles...</span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

<div id="modalProducto" class="modal-muebles" style="display: none;">
    <div class="modal-contenido">
        <div class="modal-header">
            <h3 id="modalTitulo" class="m-0" style="color: #4A3525;">Registrar Nuevo Mueble</h3>
            <span class="cerrar-modal" style="font-size: 28px; font-weight: bold;">&times;</span>
        </div>

        <form id="formProducto" class="mt-3">
            <input type="hidden" id="txtProductoId" value="" />

            <div class="formulario-grilla">
                <div class="form-grupo col-full">
                    <label>Nombre del Mueble</label>
                    <input type="text" id="txtNombre" class="form-control" required placeholder="Ej. Sofá Cama de Cuero" />
                </div>

                <div class="form-grupo">
                    <label>Referencia / SKU</label>
                    <input type="text" id="txtReferencia" class="form-control" placeholder="Ej. REF-E30M3" />
                </div>

                <div class="form-grupo">
                    <label>Categoría / Tipo Mueble</label>
                    <select id="ddlTipoMueble" class="form-control">
                        <option value="1">Salas / Sofás</option>
                        <option value="2">Comedores</option>
                        <option value="3">Dormitorios</option>
                        <option value="4">Oficinas / Escritorios</option>
                    </select>
                </div>

                <div class="form-grupo col-full">
                    <label>Descripción del Producto</label>
                    <textarea id="txtDescripcion" class="form-control" rows="2" placeholder="Detalles, acabados y texturas..."></textarea>
                </div>

                <div class="form-grupo">
                    <label>Material Principal</label>
                    <input type="text" id="txtMaterial" class="form-control" required placeholder="Ej. Madera de Caoba, Cuero" />
                </div>

                <div class="form-grupo">
                    <label>Color</label>
                    <input type="text" id="txtColor" class="form-control" placeholder="Ej. Café Nogal" />
                </div>

                <div class="form-grupo">
                    <label>Alto (cm)</label>
                    <input type="number" step="0.01" id="txtAlto" class="form-control" required placeholder="0.00" />
                </div>

                <div class="form-grupo">
                    <label>Ancho (cm)</label>
                    <input type="number" step="0.01" id="txtAncho" class="form-control" required placeholder="0.00" />
                </div>

                <div class="form-grupo">
                    <label>Profundidad (cm)</label>
                    <input type="number" step="0.01" id="txtProfundidad" class="form-control" required placeholder="0.00" />
                </div>

                <div class="form-grupo">
                    <label>Peso (Gramos)</label>
                    <input type="number" step="0.01" id="txtPeso" class="form-control" placeholder="0.00" />
                </div>

                <div class="form-grupo col-full">
                    <label>URL de la Imagen</label>
                    <input type="text" id="txtFotoUrl" class="form-control" placeholder="https://tusitio.com/imagenes/mueble.jpg" />
                </div>

                <div class="form-grupo">
                    <label>Precio de Venta (Q)</label>
                    <input type="number" step="0.01" id="txtPrecio" class="form-control" required placeholder="0.00" />
                </div>

                <div class="form-grupo" style="display: flex; flex-direction: row; align-items: center; gap: 10px; margin-top: 30px;">
                    <input type="checkbox" id="chkActivo" checked style="transform: scale(1.3); cursor: pointer;" />
                    <label for="chkActivo" class="m-0" style="cursor: pointer; user-select: none;">Disponible en catálogo</label>
                </div>
            </div>

            <div class="modal-pie text-right border-top pt-3 mt-4">
                <button type="submit" class="btn btn-success font-weight-bold px-4" style="background-color: #8C6239; border-color: #4A3525;">
                    <i class="glyphicon glyphicon-floppy-disk"></i> Guardar Cambios
                </button>
            </div>
        </form>
    </div>
</div>

@Section scripts
    <script src="~/Scripts/productos-admin.js"></script>
End Section
