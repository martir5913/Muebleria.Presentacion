@ModelType IEnumerable(Of CE_Producto)
@Code
    ViewData("Title") = "Catálogo"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim cats As List(Of String) = Nothing
    If ViewBag.Categorias IsNot Nothing Then
        cats = DirectCast(ViewBag.Categorias, List(Of String))
    End If
    Dim catActual As String = If(ViewBag.CatActual IsNot Nothing, ViewBag.CatActual.ToString(), "todos")
End Code

<style>
    .cat-row { display:flex; gap:10px; flex-wrap:wrap; margin-bottom:24px; margin-top:8px; }
    .cat-pill { padding:7px 18px; border-radius:50px; border:1.5px solid var(--border); background:var(--white); color:var(--text-mid); font-size:13px; font-weight:500; cursor:pointer; transition:all .15s; display:inline-flex; align-items:center; gap:6px; text-decoration:none; user-select:none; }
    .cat-pill:hover, .cat-pill.active { background:var(--primary); color:var(--white); border-color:var(--primary); }
    .catalogo-grid { display:grid; grid-template-columns:repeat(5,1fr); gap:18px; }
    @@media (max-width:1280px) { .catalogo-grid { grid-template-columns:repeat(4,1fr); } }
    @@media (max-width:960px)  { .catalogo-grid { grid-template-columns:repeat(3,1fr); } }
    @@media (max-width:600px)  { .catalogo-grid { grid-template-columns:repeat(2,1fr); gap:12px; } }
    .producto-card { background:var(--white); border-radius:var(--radius-lg); border:1.5px solid var(--border); overflow:hidden; transition:box-shadow .2s,transform .2s; display:flex; flex-direction:column; position:relative; }
    .producto-card:hover { box-shadow:var(--shadow-lg); transform:translateY(-4px); }
    .producto-img-wrap { width:100%; height:180px; overflow:hidden; background:var(--cream-dark); position:relative; }
    .producto-img-wrap img { width:100%; height:100%; object-fit:cover; display:block; }
    .badge-cat { position:absolute; top:10px; left:10px; background:var(--primary); color:var(--white); font-size:10px; font-weight:700; letter-spacing:.05em; text-transform:uppercase; padding:4px 9px; border-radius:50px; z-index:2; display:inline-flex; align-items:center; gap:3px; }
    .badge-nuevo { position:absolute; top:10px; left:10px; background:var(--accent); color:var(--white); font-size:10px; font-weight:700; letter-spacing:.05em; text-transform:uppercase; padding:4px 9px; border-radius:50px; z-index:2; }
    .btn-heart { position:absolute; top:8px; right:8px; background:rgba(255,255,255,.88); border:none; border-radius:50%; width:32px; height:32px; display:flex; align-items:center; justify-content:center; cursor:pointer; font-size:15px; color:var(--text-muted); transition:color .15s,background .15s,transform .15s; z-index:2; box-shadow:0 1px 4px rgba(0,0,0,.12); }
    .btn-heart:hover { background:var(--white); transform:scale(1.15); color:#C0392B; }
    .btn-heart.fav { color:#C0392B; background:var(--white); }
    .producto-body { padding:12px 14px; flex:1; display:flex; flex-direction:column; }
    .producto-nombre { font-family:var(--font-display); font-size:14px; color:var(--text); margin-bottom:8px; line-height:1.3; min-height:34px; }
    .producto-precio { font-size:18px; font-weight:700; color:var(--primary); margin-top:auto; margin-bottom:12px; }
    .producto-precio span { font-size:11px; font-weight:400; color:var(--text-muted); }
    .btn-agregar { width:100%; background:var(--primary); color:var(--white); border:none; border-radius:8px; padding:9px; font-size:12px; font-weight:600; cursor:pointer; transition:background .15s; font-family:var(--font-body); }
    .btn-agregar:hover { background:var(--accent); }
    .page-header { display:flex; align-items:center; justify-content:space-between; flex-wrap:wrap; gap:12px; margin-bottom:4px; }
    .page-title { font-family:var(--font-display); font-size:28px; color:var(--primary); }
    .busqueda { padding:9px 16px; border:1.5px solid var(--border); border-radius:8px; font-family:var(--font-body); font-size:13px; background:var(--white); color:var(--text); outline:none; width:240px; }
    .busqueda:focus { border-color:var(--accent); }
    .badge-count { background:var(--cream-dark); color:var(--text-mid); padding:4px 12px; border-radius:20px; font-size:13px; font-weight:500; }
    .empty-state { text-align:center; padding:60px 20px; color:var(--text-muted); grid-column:1/-1; }
    @@media (max-width:600px) { .busqueda { width:160px; } .page-title { font-size:22px; } }
</style>

@If TempData("Exito") IsNot Nothing Then
@<div style="background:#EAF6EE;border-left:4px solid green;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:green;font-size:13px;">@TempData("Exito")</div>
End If
@If TempData("Error") IsNot Nothing Then
@<div style="background:#FDECEA;border-left:4px solid red;padding:10px 16px;border-radius:8px;margin-bottom:16px;color:red;font-size:13px;">@TempData("Error")</div>
End If

<div class="page-header">
    <h1 class="page-title">Cat&#225;logo de Muebles</h1>
    <div style="display:flex;gap:10px;align-items:center;">
        <input type="text" id="busqueda" class="busqueda" placeholder="Buscar mueble..." oninput="filtrar()" />
        <span class="badge-count" id="conteo">@Model.Count() productos</span>
    </div>
</div>

<div class="cat-row">
    <span class="cat-pill" data-cat="todos" onclick="filtrarCat(this)">Todos</span>
@Code
    If cats IsNot Nothing Then
        For Each cat In cats
End Code
    <span class="cat-pill" data-cat="@cat" onclick="filtrarCat(this)">@Html.Raw(CategoryIcons.GetIcon(cat)) @cat</span>
@Code
        Next
    End If
End Code
</div>

<div class="catalogo-grid" id="grid">
@Code
    If Not Model.Any() Then
End Code
    <div class="empty-state"><h3>No hay productos disponibles</h3></div>
@Code
    End If
    For Each p In Model
        Dim fotoSrc As String = If(String.IsNullOrEmpty(p.FotoUrl), "/Resources/placeholder.png", Url.Content(p.FotoUrl))
        Dim catIcon As String = CategoryIcons.GetIcon(If(p.TipoMuebleNombre, ""))
        Dim catNom As String = If(p.TipoMuebleNombre, "")
        Dim prodNom As String = If(p.Nombre, "")
End Code
<div class="producto-card" data-categoria="@catNom" data-nombre="@prodNom.ToLower()">
    <div class="producto-img-wrap">
        <img src="@fotoSrc" alt="@prodNom" onerror="this.onerror=null;this.src='/Resources/placeholder.png'" />
@Code
        If p.EsNuevo Then
End Code
        <span class="badge-nuevo">&#10024; Nuevo</span>
@Code
        Else
End Code
        <span class="badge-cat">@Html.Raw(catIcon) @catNom</span>
@Code
        End If
End Code
        <button class="btn-heart"
            data-id="@p.ProductoId"
            data-nombre="@prodNom"
            data-imagen="@fotoSrc"
            data-precio="@p.Precio"
            data-categoria="@catNom"
            onclick="MLA.toggleWishlist(this)" title="Favorito">&#9829;</button>
    </div>
    <div class="producto-body">
        <div class="producto-nombre">@prodNom</div>
        <div class="producto-precio">Q @p.Precio.ToString("N2") <span>GTQ</span></div>
        <button type="button" class="btn-agregar"
            data-id="@p.ProductoId"
            data-nombre="@prodNom"
            data-imagen="@fotoSrc"
            data-precio="@p.Precio"
            data-categoria="@catNom"
            onclick="MLA.addToCart(this.dataset)">+ Agregar al carrito</button>
    </div>
</div>
@Code
    Next
End Code
</div>

<script>
var catActual = 'todos';

function norm(s) {
    if (!s) return '';
    var nfd = s.normalize('NFD');
    var out = '';
    for (var i = 0; i < nfd.length; i++) {
        var code = nfd.charCodeAt(i);
        if (code < 0x0300 || code > 0x036F) out += nfd[i];
    }
    return out.toLowerCase().trim();
}

(function init() {
    var serverCat = '@catActual';
    document.querySelectorAll('.cat-pill').forEach(function(pill) {
        if (serverCat === 'todos' && pill.dataset.cat === 'todos') {
            pill.classList.add('active');
            catActual = 'todos';
        } else if (norm(pill.dataset.cat) === serverCat) {
            pill.classList.add('active');
            catActual = pill.dataset.cat;
        }
    });
    filtrar();
})();

function filtrarCat(el) {
    document.querySelectorAll('.cat-pill').forEach(function(p) { p.classList.remove('active'); });
    el.classList.add('active');
    catActual = el.dataset.cat;
    filtrar();
}

function filtrar() {
    var q = norm(document.getElementById('busqueda').value);
    var cards = document.querySelectorAll('.producto-card');
    var visible = 0;
    cards.forEach(function(c) {
        var matchCat = catActual === 'todos' || norm(c.dataset.categoria) === norm(catActual);
        var matchNom = !q || norm(c.dataset.nombre).includes(q);
        c.style.display = (matchCat && matchNom) ? '' : 'none';
        if (matchCat && matchNom) visible++;
    });
    document.getElementById('conteo').textContent = visible + ' productos';
}

</script>
