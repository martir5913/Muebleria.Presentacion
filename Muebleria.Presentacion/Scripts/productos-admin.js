$(document).ready(function () {
    // Almacenar la URL base del sitio de forma dinámica para evitar problemas de rutas locales
    const baseUrl = window.location.origin;

    // Cargar los productos inmediatamente al iniciar la página
    listarProductos();

    // Eventos de la Interfaz de Usuario (UI)
    $("#btnNuevoProducto").on("click", function () {
        abrirModal(false);
    });

    $(".cerrar-modal").on("click", function () {
        cerrarModal();
    });

    $("#formProducto").on("submit", function (e) {
        e.preventDefault();
        guardarProducto();
    });

    // 1. OBTENER Y LISTAR PRODUCTOS (READ)
    function listarProductos() {
        $.ajax({
            url: baseUrl + '/Productos/ObtenerTodos',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                const $tablaCuerpo = $("#tablaProductosCuerpo");
                $tablaCuerpo.empty();

                if (!data || data.length === 0) {
                    $tablaCuerpo.append('<tr><td colspan="7" class="text-center py-4 text-muted">No hay productos registrados en el catálogo de Oracle.</td></tr>');
                    return;
                }

                data.forEach(function (prod) {
                // Validar si trae imagen, si no colocar una por defecto (data URI para evitar 404)
                    const placeholderPng = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGNgYAAAAAMAAWgmWQ0AAAAASUVORK5CYII=';
                    const foto = prod.FotoUrl ? prod.FotoUrl : placeholderPng;

                    const fila = `
                        <tr>
                            <td class="align-middle">${prod.ProductoId}</td>
                            <td class="align-middle"><img src="${foto}" class="img-thumbnail" style="width: 50px; height: 50px; object-fit: cover;" /></td>
                            <td class="align-middle"><strong>${prod.Nombre}</strong><br><small class="text-muted">${prod.Referencia || 'S/R'}</small></td>
                            <td class="align-middle">${prod.Material || 'N/A'}</td>
                            <td class="align-middle">${prod.DimAltoCm} x ${prod.DimAnchoCm} x ${prod.DimProfCm} cm</td>
                            <td class="align-middle">Q ${parseFloat(prod.Precio).toFixed(2)}</td>
                            <td class="text-center align-middle">
                                <button class="btn btn-sm btn-warning btn-editar" data-id="${prod.ProductoId}" style="margin-right: 5px;">
                                    <i class="glyphicon glyphicon-pencil"></i> Editar
                                </button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${prod.ProductoId}">
                                    <i class="glyphicon glyphicon-trash"></i> Eliminar
                                </button>
                            </td>
                        </tr>
                    `;

                    const $filaHtml = $(fila);

                    // Asignar los eventos de forma explícita a los botones de la fila
                    $filaHtml.find(".btn-editar").on("click", function () {
                        cargarProductoParaEditar($(this).data("id"));
                    });

                    $filaHtml.find(".btn-eliminar").on("click", function () {
                        eliminarProducto($(this).data("id"));
                    });

                    $tablaCuerpo.append($filaHtml);
                });
            },
            error: function (xhr, status, error) {
                console.error("Error crítico al obtener productos:", error);
                $("#tablaProductosCuerpo").html('<tr><td colspan="7" class="text-center text-danger py-4">Error de comunicación con el servidor. Revisa la consola.</td></tr>');
            }

        }); // end $.ajax
    } // end listarProductos

    // Evitar que errores de reproducción de medios sin manejar llenen la consola (p.ej. AbortError)
    window.addEventListener('unhandledrejection', function (event) {
        try {
            if (event && event.reason && event.reason.name === 'AbortError') {
                event.preventDefault();
            }
        } catch (e) {
            // ignore
        }
    });

    // 2. GUARDAR / ACTUALIZAR PRODUCTO (CREATE / UPDATE)
    function guardarProducto() {
        const idActual = $("#txtProductoId").value ? parseInt($("#txtProductoId").value) : 0;

        const productoData = {
            ProductoId: $("#txtProductoId").val() ? parseInt($("#txtProductoId").val()) : 0,
            Nombre: $("#txtNombre").val(),
            Referencia: $("#txtReferencia").val(),
            Descripcion: $("#txtDescripcion").val(),
            TipoMuebleId: parseInt($("#ddlTipoMueble").val()),
            Material: $("#txtMaterial").val(),
            DimAltoCm: parseFloat($("#txtAlto").val()) || 0,
            DimAnchoCm: parseFloat($("#txtAncho").val()) || 0,
            DimProfCm: parseFloat($("#txtProfundidad").val()) || 0,
            Color: $("#txtColor").val(),
            PesoGramos: parseFloat($("#txtPeso").val()) || 0,
            FotoUrl: $("#txtFotoUrl").val(),
            Precio: parseFloat($("#txtPrecio").val()) || 0,
            Activo: $("#chkActivo").is(":checked") ? "S" : "N"
        };

        const urlDestino = productoData.ProductoId === 0 ? '/Productos/Registrar' : '/Productos/Modificar';

        $.ajax({
            url: baseUrl + urlDestino,
            type: 'POST',
            data: productoData, // enviar como form-urlencoded para que el model binder de MVC lo reciba
            dataType: 'json',
            success: function (respuesta) {
                if (respuesta.Exito) {
                    alert(productoData.ProductoId === 0 ? "¡Mueble añadido con éxito!" : "¡Mueble actualizado con éxito!");
                    cerrarModal();
                    listarProductos();
                } else {
                    alert("Error en el proceso: " + (respuesta.Mensaje || JSON.stringify(respuesta)));
                }
            },
            error: function (xhr, status, err) {
                console.error('Error al guardar:', status, err, xhr.responseText);
                alert("Error fatal al intentar guardar en el servidor. Revisa la consola para más detalles.");
            }
        });
    }

    // 3. EDITAR: EXTRAER REGISTRO POR ID
    function cargarProductoParaEditar(id) {
        $.ajax({
            url: baseUrl + '/Productos/ObtenerPorId',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (prod) {
                if (prod) {
                    abrirModal(true);
                    $("#txtProductoId").val(prod.ProductoId);
                    $("#txtNombre").val(prod.Nombre);
                    $("#txtReferencia").val(prod.Referencia);
                    $("#txtDescripcion").val(prod.Descripcion);
                    $("#ddlTipoMueble").val(prod.TipoMuebleId);
                    $("#txtMaterial").val(prod.Material);
                    $("#txtAlto").val(prod.DimAltoCm);
                    $("#txtAncho").val(prod.DimAnchoCm);
                    $("#txtProfundidad").val(prod.DimProfCm);
                    $("#txtColor").val(prod.Color);
                    $("#txtPeso").val(prod.PesoGramos);
                    $("#txtFotoUrl").val(prod.FotoUrl);
                    $("#txtPrecio").val(prod.Precio);
                    $("#chkActivo").prop("checked", prod.Activo === "S");
                }
            },
            error: function () {
                alert("Error al extraer los datos del mueble seleccionado.");
            }
        });
    }

    // 4. BORRAR / RETIRAR PRODUCTO (DELETE)
    function eliminarProducto(id) {
        if (confirm("¿Estás seguro de que deseas dar de baja este mueble del catálogo?")) {
            $.ajax({
                url: baseUrl + '/Productos/Eliminar',
                type: 'POST',
                data: { id: id }, // enviar como formulario para que el binder reciba el int
                dataType: 'json',
                success: function (respuesta) {
                    if (respuesta.Exito) {
                        alert("El mueble se ha retirado del catálogo activo.");
                        listarProductos();
                    } else {
                        alert("Error al intentar eliminar: " + (respuesta.Mensaje || JSON.stringify(respuesta)));
                    }
                },
                error: function (xhr, status, err) {
                    console.error('Error al eliminar:', status, err, xhr.responseText);
                    alert("Error al comunicarse con el servidor al eliminar. Ver consola.");
                }
            });
        }
    }

    // Funciones Auxiliares de Modales (Asegura la visualización)
    function abrirModal(esEditar) {
        $("#modalTitulo").text(esEditar ? "Modificar Mueble" : "Registrar Nuevo Mueble");
        if (!esEditar) {
            $("#formProducto")[0].reset();
            $("#txtProductoId").val("");
        }
        // Forzar despliegue visual mediante estilos CSS directos en lugar de confiar en dependencias externas
        $("#modalProducto").css("display", "flex");
    }

    function cerrarModal() {
        $("#modalProducto").css("display", "none");
        $("#formProducto")[0].reset();
        $("#txtProductoId").val("");
    }
});