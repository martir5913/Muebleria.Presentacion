@Code       ViewData("Title") = "Catalogo"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .section-head {
        display: flex;
        align-items: baseline;
        justify-content: space-between;
        margin-bottom: 20px;
    }

        .section-head h2 {
            font-family: var(--font-display);
            font-size: 26px;
            color: var(--primary);
        }

    .cat-row {
        display: flex;
        gap: 10px;
        flex-wrap: wrap;
        margin-bottom: 24px;
    }

    .cat-pill {
        padding: 7px 18px;
        border-radius: 50px;
        border: 1.5px solid var(--border);
        background: var(--white);
        color: var(--text-mid);
        font-size: 13px;
        font-weight: 500;
        cursor: pointer;
        transition: all .15s;
        text-decoration: none;
        display: inline-block;
    }

        .cat-pill.active, .cat-pill:hover {
            background: var(--primary);
            color: var(--white);
            border-color: var(--primary);
        }

    .product-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
        gap: 20px;
    }

    .product-card {
        background: var(--white);
        border-radius: var(--radius-lg);
        border: 1.5px solid var(--border);
        overflow: hidden;
        transition: box-shadow .2s, transform .2s;
    }

        .product-card:hover {
            box-shadow: var(--shadow-lg);
            transform: translateY(-4px);
        }

    .product-img-fb {
        width: 100%;
        height: 200px;
        background: var(--cream-dark);
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 56px;
        overflow: hidden;
    }

    .product-img-fb img {
        width: 100%;
        height: 100%;
        object-fit: cover;
        transition: transform .3s ease;
    }

    .product-card:hover .product-img-fb img {
        transform: scale(1.06);
    }

    .product-info {
        padding: 14px 16px;
    }

    .product-tag {
        font-size: 10px;
        text-transform: uppercase;
        letter-spacing: .08em;
        color: var(--accent);
        font-weight: 600;
        margin-bottom: 4px;
    }

    .product-name {
        font-family: var(--font-display);
        font-size: 14px;
        color: var(--text);
        margin-bottom: 8px;
        line-height: 1.3;
        min-height: 36px;
    }

    .product-price {
        font-size: 18px;
        font-weight: 700;
        color: var(--primary);
        margin-bottom: 12px;
    }

        .product-price span {
            font-size: 11px;
            font-weight: 400;
            color: var(--text-muted);
        }

    .btn-add {
        width: 100%;
        padding: 9px;
        background: var(--primary);
        color: var(--white);
        border: none;
        border-radius: var(--radius);
        cursor: pointer;
        font-size: 13px;
        font-weight: 600;
        transition: background .2s;
        font-family: var(--font-body);
    }

        .btn-add:hover {
            background: var(--accent);
        }

    .alert-success {
        background: #D4EDDA;
        color: #155724;
        padding: 12px 18px;
        border-radius: var(--radius);
        margin-bottom: 20px;
        font-size: 13px;
        border: 1px solid #C3E6CB;
    }

    .alert-danger {
        background: #F8D7DA;
        color: #721C24;
        padding: 12px 18px;
        border-radius: var(--radius);
        margin-bottom: 20px;
        font-size: 13px;
        border: 1px solid #F5C6CB;
    }

    .empty-state {
        text-align: center;
        padding: 80px 20px;
        color: var(--text-muted);
    }

        .empty-state .empty-icon {
            font-size: 64px;
            margin-bottom: 16px;
        }

        .empty-state h3 {
            font-family: var(--font-display);
            font-size: 22px;
            color: var(--primary);
            margin-bottom: 8px;
        }
</style>

@If TempData("Exito") IsNot Nothing Then
    @<div class="alert-success">✅ @TempData("Exito")</div>
End If
@If TempData("Error") IsNot Nothing Then
    @<div class="alert-danger">❌ @TempData("Error")</div>
End If
@If ViewBag.Error IsNot Nothing Then
    @<div class="alert-danger">❌ @ViewBag.Error</div>
End If

<div class="section-head">
    <h2>🛋️ Catalogo de Muebles</h2>
</div>

<div class="cat-row">
    <span class="cat-pill active">Todos</span>
    <span class="cat-pill">🛋️ Sala</span>
    <span class="cat-pill">🍽️ Comedor</span>
    <span class="cat-pill">🛏️ Habitaciones</span>
    <span class="cat-pill">🌿 Jardin</span>
    <span class="cat-pill">💼 Oficina</span>
</div>

@Code
    Dim imageNums = New Integer() {1,3,4,6,8,10,12,14,16,18,20,22,24,26,28,30,31,32,33,35,36,37,39,41,43,45,47,48,49,51,52,53,55,57,59,61,62,63,64,66,67,68,70,72,73,75,77,79,81,82,83,84,86,87,89,90,92,93,95,96,97,98,99,101}
End Code

@If Model IsNot Nothing AndAlso Model.Count > 0 Then
    @<div class="product-grid">
        @For Each producto In Model
            @Code
                Dim idx = (producto.ProductoId - 1) Mod imageNums.Length
                Dim num = imageNums(idx)
                Dim imgSrc = Url.Content("~/Content/Images/Productos/image" + num.ToString() + ".jpeg")
            End Code
            @<div class="product-card">
                <div class="product-img-fb">
                    <img src="@imgSrc" alt="@producto.Nombre" loading="lazy" />
                </div>
                <div class="product-info">
                    <div class="product-tag">@(If(producto.Referencia IsNot Nothing AndAlso producto.Referencia.Trim() <> "", producto.Referencia, "Mueble"))</div>
                    <div class="product-name">@producto.Nombre</div>
                    <div class="product-price">Q @String.Format("{0:N2}", producto.Precio) <span>GTQ</span></div>
                    @Using Html.BeginForm("AgregarAlCarrito", "Catalogo", FormMethod.Post)
                        @Html.Hidden("productoId", producto.ProductoId)
                        @Html.Hidden("cantidad", 1)
                       @<button type="submit" class="btn-add">+ Agregar al carrito</button>
                    End Using
                </div>
            </div>
        Next
    </div>
    @<div class="empty-state">
        <div class="empty-icon">🪑</div>
        <h3>No hay productos disponibles</h3>
        <p>Pronto tendras nuevos disenos disponibles.</p>
    </div>
End If
