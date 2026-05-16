@ModelType IEnumerable(Of CE_Producto)
@Code
    ViewData("Title") = "Administrar Productos"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .admin-header {
        display: flex;
        align-items: center;
        justify-content: space-between;
        margin-bottom: 24px;
    }

    .page-title {
        font-family: var(--font-display);
        font-size: 26px;
        color: var(--primary);
    }

    .btn-nuevo {
        background: var(--accent);
        color: var(--white);
        border: none;
        border-radius: 8px;
        padding: 10px 20px;
        font-size: 13px;
        font-weight: 600;
        text-decoration: none;
        display: inline-flex;
        align-items: center;
        gap: 6px;
        transition: background .15s;
        font-family: var(--font-body);
    }

        .btn-nuevo:hover {
            background: #b07830;
            color: var(--white);
        }

    .tabla-card {
        background: var(--white);
        border-radius: var(--radius-lg);
        box-shadow: var(--shadow);
        overflow: hidden;
    }

        .tabla-card table {
            width: 100%;
            border-collapse: collapse;
        }

        .tabla-card thead {
            background: var(--primary);
            color: var(--white);
        }

            .tabla-card thead th {
                padding: 14px 16px;
                font-size: 12px;
                font-weight: 600;
                text-transform: uppercase;
                letter-spacing: .06em;
                text-align: left;
            }

        .tabla-card tbody tr {
            border-bottom: 1px solid var(--border);
            transition: background .1s;
        }

            .tabla-card tbody tr:hover {
                background: var(--cream);
            }

        .tabla-card tbody td {
            padding: 13px 16px;
            font-size: 14px;
            vertical-align: middle;
        }

    .badge-activo {
        display: inline-block;
        padding: 3px 10px;
        border-radius: 20px;
        font-size: 11px;
        font-weight: 600;
    }

    .badge-s {
        background: #EAF6EE;
        color: #27AE60;
    }

    .badge-n {
        background: #FDECEA;
        color: #E74C3C;
    }

    .acciones {
        display: flex;
        gap: 6px;
    }

    .btn-editar {
        background: none;
        border: 1.5px solid var(--accent);
        color: var(--accent);
        border-radius: 6px;
        padding: 5px 12px;
        font-size: 12px;
        cursor: pointer;
        text-decoration: none;
        transition: background .15s;
        font-family: var(--font-body);
    }

        .btn-editar:hover {
            background: var(--accent);
            color: white;
        }

    .btn-eliminar {
        background: none;
        border: 1.5px solid #E74C3C;
        color: #E74C3C;
        border-radius: 6px;
        padding: 5px 12px;
        font-size: 12px;
        cursor: pointer;
        transition: background .15s;
        font-family: var(--font-body);
    }

        .btn-eliminar:hover {
            background: #E74C3C;
            color: white;
        }

    .ref-cell {
        color: var(--text-muted);
        font-size: 12px;
        font-family: monospace;
    }

    .precio-cell {
        font-weight: 600;
        color: var(--primary);
    }

    .busqueda {
        padding: 9px 14px;
        border: 1.5px solid var(--border);
        border-radius: 8px;
        font-family: var(--font-body);
        font-size: 13px;
        width: 260px;
        outline: none;
    }

    .toolbar {
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 14px 16px;
        border-bottom: 1px solid var(--border);
        background: var(--cream);
    }
</style>

@If TempData("Exito") IsNot Nothing Then
@<div style="background:#EAF6EE;border-left:4px solid green;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:green;font-size:13px;">@TempData("Exito")</div>
End If
@If TempData("Error") IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@TempData("Error")</div>
End If
@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<div class="admin-header">
    <h1 class="page-title">Administrar Productos</h1>
    @Html.ActionLink("+ Nuevo Producto", "Crear", "Productos", Nothing, New With {.class = "btn-nuevo"})
</div>

<div class="tabla-card">
    <div class="toolbar">
        <span style="font-size:13px;color:var(--text-muted);">@Model.Count() productos</span>
        <input type="text" id="busqueda" class="busqueda" placeholder="Buscar..." oninput="filtrar()" />
    </div>
    <table>
        <thead>
            <tr>
                <th>#</th>
                <th>Nombre</th>
                <th>Referencia</th>
                <th>Precio (Q)</th>
                <th>Estado</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody id="tbody">
            @For Each p In Model
            @Code
                    Dim buscar As String = (p.Nombre & " " & p.Referencia).ToLower()
                    Dim estadoClass As String = If(p.Activo = "S", "badge-s", "badge-n")
                    Dim estadoTexto As String = If(p.Activo = "S", "Activo", "Inactivo")
            End Code
            @<tr data-buscar="@buscar">
                <td style="color:var(--text-muted);font-size:12px;">@p.ProductoId</td>
                <td><strong>@p.Nombre</strong></td>
                <td class="ref-cell">@p.Referencia</td>
                <td class="precio-cell">Q @p.Precio.ToString("N2")</td>
                <td><span class="badge-activo @estadoClass">@estadoTexto</span></td>
                <td>
                    <div class="acciones">
                        @Html.ActionLink("Editar", "Editar", "Productos",
                            New With {.id = p.ProductoId},
                            New With {.class = "btn-editar"})
                        @Using Html.BeginForm("Eliminar", "Productos", FormMethod.Post)
                        @Html.Hidden("id", p.ProductoId)
                        @<button type="submit" class="btn-eliminar"
                                 onclick="return confirm('Eliminar este producto?')">
                            Eliminar
                        </button>
                            End Using
                    </div>
                </td>
            </tr>
            Next
        </tbody>
    </table>
</div>

<script>function filtrar() {
        var q = document.getElementById('busqueda').value.toLowerCase();
        document.querySelectorAll('#tbody tr').forEach(function(r) {
            r.style.display = r.dataset.buscar && r.dataset.buscar.includes(q) ? '' : 'none';
        });
    }</script>
