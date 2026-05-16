@ModelType IEnumerable(Of CE_CarritoItem)

@Code
    ViewData("Title") = "Mi Carrito"
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim total As Decimal = If(ViewBag.Total IsNot Nothing, CDec(ViewBag.Total), 0D)
    Dim envio As Decimal = If(total >= 1500D, 0D, 75D)
    Dim totalFinal As Decimal = total + envio
End Code

<style>
    .page-title {
        font-family: var(--font-display);
        font-size: 26px;
        color: var(--primary);
        margin-bottom: 24px;
    }

    .carrito-layout {
        display: grid;
        grid-template-columns: 1fr 340px;
        gap: 24px;
        align-items: start;
    }

    .items-card {
        background: var(--white);
        border-radius: var(--radius-lg);
        border: 1.5px solid var(--border);
        box-shadow: var(--shadow);
        overflow: hidden;
    }

    .items-header {
        padding: 16px 20px;
        border-bottom: 1.5px solid var(--border);
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

        .items-header h3 {
            font-family: var(--font-display);
            font-size: 18px;
            color: var(--primary);
            margin: 0;
        }

    .item-row {
        display: flex;
        align-items: center;
        gap: 16px;
        padding: 16px 20px;
        border-bottom: 1px solid var(--border);
    }

        .item-row:last-child {
            border-bottom: none;
        }

    .item-img {
        width: 64px;
        height: 64px;
        background: var(--cream-dark);
        border-radius: 10px;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 28px;
        flex-shrink: 0;
    }

    .item-info {
        flex: 1;
    }

    .item-name {
        font-weight: 600;
        font-size: 14px;
        color: var(--text);
        margin-bottom: 4px;
    }

    .item-ref {
        font-size: 11px;
        color: var(--text-muted);
    }

    .item-qty {
        font-size: 13px;
        color: var(--text-mid);
        margin-top: 4px;
    }

    .item-subtotal {
        font-family: var(--font-display);
        font-size: 16px;
        font-weight: 700;
        color: var(--primary);
        min-width: 90px;
        text-align: right;
    }

    .btn-eliminar {
        background: none;
        border: 1px solid var(--border);
        padding: 5px 10px;
        border-radius: 6px;
        color: #dc3545;
        cursor: pointer;
        font-size: 12px;
        font-family: var(--font-body);
        transition: all .15s;
    }

        .btn-eliminar:hover {
            background: #F8D7DA;
            border-color: #dc3545;
        }

    .resumen-card {
        background: var(--white);
        border-radius: var(--radius-lg);
        border: 1.5px solid var(--border);
        box-shadow: var(--shadow);
        padding: 24px;
        position: sticky;
        top: 100px;
    }

        .resumen-card h3 {
            font-family: var(--font-display);
            font-size: 20px;
            color: var(--primary);
            margin-bottom: 20px;
        }

    .resumen-row {
        display: flex;
        justify-content: space-between;
        font-size: 13px;
        color: var(--text-mid);
        margin-bottom: 10px;
    }

    .resumen-total {
        display: flex;
        justify-content: space-between;
        font-size: 18px;
        font-weight: 700;
        color: var(--primary);
        padding-top: 12px;
        border-top: 2px solid var(--border);
        margin-top: 4px;
        margin-bottom: 20px;
    }

    .pago-label {
        font-size: 12px;
        font-weight: 600;
        color: var(--text-mid);
        margin-bottom: 6px;
        display: block;
    }

    .pago-select {
        width: 100%;
        padding: 9px 12px;
        border: 1.5px solid var(--border);
        border-radius: var(--radius);
        font-size: 13px;
        font-family: var(--font-body);
        background: var(--cream);
        color: var(--text);
        outline: none;
        margin-bottom: 16px;
    }

        .pago-select:focus {
            border-color: var(--accent);
        }

    .btn-comprar {
        width: 100%;
        padding: 14px;
        background: var(--primary);
        color: var(--white);
        border: none;
        border-radius: var(--radius);
        font-size: 15px;
        font-weight: 700;
        cursor: pointer;
        font-family: var(--font-body);
        transition: background .2s;
    }

        .btn-comprar:hover {
            background: var(--accent);
        }

    .btn-vaciar {
        width: 100%;
        padding: 9px;
        background: none;
        border: 1.5px solid var(--border);
        border-radius: var(--radius);
        font-size: 13px;
        color: var(--text-mid);
        cursor: pointer;
        font-family: var(--font-body);
        margin-top: 8px;
        transition: all .15s;
    }

        .btn-vaciar:hover {
            background: #F8D7DA;
            border-color: #dc3545;
            color: #dc3545;
        }

    .empty-carrito {
        text-align: center;
        padding: 64px 20px;
    }

        .empty-carrito .big-icon {
            font-size: 64px;
            margin-bottom: 16px;
        }

        .empty-carrito h3 {
            font-family: var(--font-display);
            font-size: 22px;
            color: var(--primary);
            margin-bottom: 10px;
        }

        .empty-carrito p {
            color: var(--text-muted);
            font-size: 14px;
            margin-bottom: 24px;
        }

    .btn-ir-catalogo {
        display: inline-block;
        background: var(--primary);
        color: var(--white);
        padding: 12px 28px;
        border-radius: var(--radius);
        font-weight: 600;
        font-size: 14px;
        text-decoration: none;
        transition: background .2s;
    }

        .btn-ir-catalogo:hover {
            background: var(--accent);
        }

    .alert-success {
        background: #EAF6EE;
        border-left: 4px solid green;
        padding: 12px 16px;
        border-radius: 8px;
        margin-bottom: 16px;
        color: green;
        font-size: 13px;
    }

    .alert-danger {
        background: #FDECEA;
        border-left: 4px solid red;
        padding: 12px 16px;
        border-radius: 8px;
        margin-bottom: 16px;
        color: red;
        font-size: 13px;
    }

    @@media(max-width:768px) {
        .carrito-layout {
            grid-template-columns: 1fr;
        }

        .item-row {
            flex-direction: column;
            align-items: flex-start;
        }

        .item-subtotal {
            text-align: left;
        }
    }
</style>

@if TempData("Exito") IsNot Nothing Then
@<div class="alert-success">
    @TempData("Exito")
</div>
End If

@if TempData("Error") IsNot Nothing Then
@<div class="alert-danger">
    @TempData("Error")
</div>
End If

<h1 class="page-title">🛒 Mi Carrito</h1>

@if Not Model.Any() Then

@<div class="empty-carrito">
    <div class="big-icon">🛒</div>

    <h3>Tu carrito está vacío</h3>

    <p>
        Agrega productos desde el catálogo para comenzar tu compra.
    </p>

    <a href="@Url.Action("Index", "Catalogo")"
       class="btn-ir-catalogo">
        Ver Catálogo
    </a>
</div>

Else

@<div class="carrito-layout">

    <div class="items-card">

        <div class="items-header">
            <h3>Productos (@Model.Count() artículos)</h3>
        </div>

        @For Each item In Model

        @<div class="item-row">

            <div class="item-img">🪑</div>

            <div class="item-info">

                <div class="item-name">
                    @item.NombreProducto
                </div>

                <div class="item-ref">
                    Ref:
                    @(If(item.Referencia IsNot Nothing,
                                item.Referencia,
                                "SIN-REF"))
                </div>

                <div class="item-qty">
                    Cantidad:
                    @item.Cantidad ·
                    Q@item.PrecioUnitario.ToString("N2") c/u
                </div>

            </div>

            <div class="item-subtotal">
                Q@item.Subtotal.ToString("N2")
            </div>

            @Using Html.BeginForm("EliminarItem", "Carrito", FormMethod.Post)

            @Html.Hidden("productoId", item.ProductoId)

            @<button type="submit"
                     class="btn-eliminar"
                     onclick="return confirm('¿Eliminar del carrito?')">
                ✕ Quitar
            </button>

                    End Using

        </div>

            Next

    </div>

    <div class="resumen-card">

        <h3>Resumen</h3>

        <div class="resumen-row">
            <span>Subtotal</span>
            <span>Q@total.ToString("N2")</span>
        </div>

        <div class="resumen-row">
            <span>Envío</span>

            <span>
                @(If(envio = 0D,
                        "Gratis",
                        "Q" & envio.ToString("N2")))
            </span>
        </div>

        <div class="resumen-total">
            <span>Total</span>
            <span>Q@totalFinal.ToString("N2")</span>
        </div>

        @Using Html.BeginForm("Comprar", "Carrito", FormMethod.Post)

        @Html.AntiForgeryToken()

        @<label class="pago-label">
            Forma de pago
        </label>

        @<select name="formaPago"
                 class="pago-select"
                 required>

            <option value="">
                -- Selecciona --
            </option>

            <option value="EFECTIVO">
                Efectivo
            </option>

            <option value="TARJETA">
                Tarjeta de crédito/débito
            </option>

            <option value="TRANSFERENCIA">
                Transferencia bancaria
            </option>

        </select>

        @<button type="submit"
                 class="btn-comprar">
            Efectuar Compra →
        </button>

            End Using

        @Using Html.BeginForm("Vaciar", "Carrito", FormMethod.Post)

        @<button type="submit"
                 class="btn-vaciar"
                 onclick="return confirm('¿Vaciar todo el carrito?')">

            Vaciar carrito

        </button>

            End Using

    </div>

</div>

End If