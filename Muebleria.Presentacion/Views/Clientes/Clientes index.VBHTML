@ModelType IEnumerable(Of CE_Cliente)
@Code
    ViewData("Title") = "Clientes"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .page-title {
        font-family: var(--font-display);
        font-size: 26px;
        color: var(--primary);
        margin-bottom: 24px;
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

    .toolbar {
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 14px 16px;
        border-bottom: 1px solid var(--border);
        background: var(--cream);
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

        .busqueda:focus {
            border-color: var(--accent);
        }

    .avatar {
        width: 34px;
        height: 34px;
        background: var(--primary);
        color: var(--white);
        border-radius: 50%;
        display: inline-flex;
        align-items: center;
        justify-content: center;
        font-weight: 700;
        font-size: 14px;
        margin-right: 8px;
        vertical-align: middle;
        flex-shrink: 0;
    }

    .nombre-cell {
        display: flex;
        align-items: center;
    }

    .email-cell {
        color: var(--text-muted);
        font-size: 13px;
    }

    .fecha-cell {
        color: var(--text-muted);
        font-size: 12px;
    }
</style>

@If ViewBag.Error IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@ViewBag.Error</div>
End If

<h1 class="page-title">Clientes registrados</h1>

<div class="tabla-card">
    <div class="toolbar">
        <span style="font-size:13px;color:var(--text-muted);">@Model.Count() clientes activos</span>
        <input type="text" id="busqueda" class="busqueda"
               placeholder="Buscar cliente..." oninput="filtrar()" />
    </div>
    <table>
        <thead>
            <tr>
                <th>#</th>
                <th>Nombre</th>
                <th>Email</th>
                <th>Fecha de registro</th>
            </tr>
        </thead>
        <tbody id="tbody">
            @For Each c In Model
            @<tr data-buscar="@((c.Nombre & " " & c.Correo).ToLower())">
                <td style="color:var(--text-muted);font-size:12px;">@c.Id</td>
                <td>
                    <div class="nombre-cell">
                        <div class="avatar">@c.Nombre.Substring(0,1).ToUpper()</div>
                        <strong>@c.Nombre</strong>
                    </div>
                </td>
                <td class="email-cell">@c.Correo</td>
                <td class="fecha-cell">@c.FechaCreacion.ToString("dd/MM/yyyy HH:mm")</td>
            </tr>
            Next
        </tbody>
    </table>
</div>

<script>function filtrar() {
        var q = document.getElementById('busqueda').value.toLowerCase();
        document.querySelectorAll('#tbody tr').forEach(function(r) {
            r.style.display = r.dataset.buscar.includes(q) ? '' : 'none';
        });
    }</script>
