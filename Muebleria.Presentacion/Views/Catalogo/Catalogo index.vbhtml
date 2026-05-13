@ModelType IEnumerable(Of Muebleria.Entidades.CE_Producto)
@Code
    ViewData("Title") = "Catálogo"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .catalogo-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
        gap: 24px;
        margin-top: 24px;
    }
    .producto-card {
        background: var(--white);
        border-radius: var(--radius-lg);
        box-shadow: var(--shadow);
        overflow: hidden;
        transition: box-shadow .2s, transform .2s;
        display: flex;
        flex-direction: column;
    }
    .producto-card:hover {
        box-shadow: var(--shadow-lg);
        transform: translateY(-3px);
    }
    .producto-img {
        width: 100%;
        height: 200px;
        background: var(--cream-dark);
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 64px;
    }
    .producto-body {
        padding: 16px;
        flex: 1;
        display: flex;
        flex-direction: column;
    }
    .producto-ref {
        font-size: 11px;
        color: var(--text-muted);
        text-transform: uppercase;
        letter-spacing: .08em;
        margin-bottom: 4px;
    }
    .producto-nombre {
        font-family: var(--font-display);
        font-size: 17px;
        font-weight: 600;
        color: var(--primary);
        margin-bottom: 8px;
        line-height: 1.3;
    }
    .producto-precio {
        font-size: 20px;
        font-weight: 700;
        color: var(--accent);
        margin-top: auto;
        margin-bottom: 14px;
    }
    .btn-agregar {
        width: 100%;
        background: var(--primary);
        color: var(--white);
        border: none;
        border-radius: 8px;
        padding: 10px;
        font-size: 13px;
        font-weight: 600;
        cursor: pointer;
        transition: background 0.15s;
        font-family: var(--font-body);
    }
    .btn-agregar:hover {
        background: var(--primary-light);
    }
    .page-header {
        display: flex;
        align-items: center;
        justify-content: space-between;
        margin-bottom: 8px;
    }
    .page-title {
        font-family: var(--font-display);
        font-size: 28px;
        color: var(--primary);
    }
    .badge-count {
        background: var(--cream-dark);
        color: var(--text-mid);
        padding: 4px 12px;
        border-radius: 20px;
        font-size: 13px;
    }
    .busqueda {
        width: 100%;
        max-width: 380px;
        padding: 10px 16px;
        border: 1.5px solid var(--border);
        border-radius: 8px;
        font-family: var(--font-body);
        font-size: 13px;
        background: var(--white);
        color: var(--text);
        outline: none;
        transition: border-color .2s;
    }
    .busqueda:focus {
        border-color: var(--accent);
    }
    .empty-state {
        text-align: center;
        padding: 60px 20px;
        color: var(--text-muted);
    }
</style>

@If TempData("Exito") IsNot Nothing Then
    @<div style="background:#EAF6EE;border-left:4px solid var(--success);padding:10px 16px;border-radius:8px;margin-bottom:16px;color:var(--success);font-size:13px;">
        ✅ @TempData("Exito")
    </div>
End If
@If TempData("Error") IsNot Nothing Then
    @<div style="background:#FDECEA;border-left:4px solid var(--danger);padding:10px 16px;border-radius:8px;margin-bottom:16px;color:var(--danger);font-size:13px;">
        ⚠️ @TempData("Error")
    </div>
End If

<div class="page-header">
    <h1 class="page-title">🛋️ Catálogo</h1>
    <span class="badge-count">@Model.Count() productos</span>
</div>

<input type="text" id="busqueda" class="busqueda"
       placeholder="🔍 Buscar producto..." oninput="filtrar()" />

@If Not Model.Any() Then
    @<div class="empty-state">
        <div style="font-size:48px;margin-bottom:12px;">📦</div>
        <h3>No hay productos disponibles</h3>
        <p>El catálogo está vacío por el momento.</p>
    </div>
Else
    @<div class="catalogo-grid" id="grid">
        @For Each p In Model
            @<div class="producto-card" data-nombre="@p.Nombre.ToLower()">
                <div class="producto-img">🪑</div>
                <div class="producto-body">
                    <div class="producto-ref">Ref: @p.Referencia</div>
                    <div class="producto-nombre">@p.Nombre</div>
                    <div class="producto-precio">Q @p.Precio.ToString("N2")</div>
                    @Using Html.BeginForm("AgregarAlCarrito", "Catalogo", FormMethod.Post)
                        @Html.AntiForgeryToken()
                        @Html.Hidden("productoId", p.ProductoId)
                        @Html.Hidden("cantidad", 1)
                        @<button type="submit" class="btn-agregar">
                            🛒 Agregar al carrito
                        </button>
                    End Using
                </div>
            </div>
        Next
    </div>
End If

<script>
    function filtrar() {
        const q = document.getElementById('busqueda').value.toLowerCase();
        document.querySelectorAll('.producto-card').forEach(c => {
            c.style.display = c.dataset.nombre.includes(q) ? '' : 'none';
        });
    }
</script>
