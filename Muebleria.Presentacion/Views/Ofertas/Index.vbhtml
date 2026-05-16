@ModelType IEnumerable(Of CE_Producto)
@Code
    ViewData("Title") = "Ofertas"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim cats As List(Of String) = Nothing
    If ViewBag.Categorias IsNot Nothing Then
        cats = DirectCast(ViewBag.Categorias, List(Of String))
    End If
    Dim catActual As String = If(ViewBag.CatActual IsNot Nothing, ViewBag.CatActual.ToString(), "todos")
End Code

<style>
    .ofertas-banner { background:linear-gradient(120deg,#C0392B 0%,#E74C3C 60%,#C0392B 100%); border-radius:var(--radius-lg); padding:36px 48px; color:var(--white); display:flex; align-items:center; justify-content:space-between; gap:24px; margin-bottom:28px; }
    .ofertas-banner-text .ob-tag { font-size:11px; letter-spacing:.12em; text-transform:uppercase; color:rgba(255,255,255,.75); font-weight:600; margin-bottom:8px; }
    .ofertas-banner-text h2 { font-family:var(--font-display); font-size:32px; line-height:1.1; margin-bottom:8px; }
    .ofertas-banner-text p { font-size:14px; color:rgba(255,255,255,.85); max-width:400px; line-height:1.5; }
    .ofertas-banner-icon { font-size:72px; flex-shrink:0; line-height:1; filter:drop-shadow(0 6px 18px rgba(0,0,0,.25)); }
    .cat-row { display:flex; gap:10px; flex-wrap:wrap; margin-bottom:24px; margin-top:8px; }
    .cat-pill { padding:7px 18px; border-radius:50px; border:1.5px solid var(--border); background:var(--white); color:var(--text-mid); font-size:13px; font-weight:500; cursor:pointer; transition:all .15s; display:inline-flex; align-items:center; gap:6px; text-decoration:none; user-select:none; }
    .cat-pill:hover, .cat-pill.active { background:var(--primary); color:var(--white); border-color:var(--primary); }
    .oferta-grid { display:grid; grid-template-columns:repeat(5,1fr); gap:18px; }
    @@media (max-width:1280px) { .oferta-grid { grid-template-columns:repeat(4,1fr); } }
    @@media (max-width:960px)  { .oferta-grid { grid-template-columns:repeat(3,1fr); } }
    @@media (max-width:600px)  { .oferta-grid { grid-template-columns:repeat(2,1fr); gap:12px; } }
    .oferta-card { background:var(--white); border-radius:var(--radius-lg); border:1.5px solid var(--border); overflow:hidden; transition:box-shadow .2s,transform .2s; display:flex; flex-direction:column; position:relative; }
    .oferta-card:hover { box-shadow:var(--shadow-lg); transform:translateY(-4px); }
    .oferta-img-wrap { width:100%; height:180px; overflow:hidden; background:var(--cream-dark); position:relative; }
    .oferta-img-wrap img { width:100%; height:100%; object-fit:cover; display:block; }
    .badge-desc { position:absolute; top:10px; left:10px; background:#C0392B; color:#fff; font-size:11px; font-weight:700; padding:4px 10px; border-radius:50px; z-index:2; }
    .btn-heart-o { position:absolute; top:8px; right:8px; background:rgba(255,255,255,.88); border:none; border-radius:50%; width:32px; height:32px; display:flex; align-items:center; justify-content:center; cursor:pointer; font-size:15px; color:var(--text-muted); transition:color .15s,background .15s,transform .15s; z-index:2; box-shadow:0 1px 4px rgba(0,0,0,.12); }
    .btn-heart-o:hover { background:var(--white); color:#C0392B; transform:scale(1.15); }
    .btn-heart-o.fav { color:#C0392B; background:var(--white); }
    .oferta-body { padding:12px 14px; flex:1; display:flex; flex-direction:column; }
    .oferta-cat-tag { font-size:10px; text-transform:uppercase; letter-spacing:.07em; color:var(--accent); font-weight:600; margin-bottom:4px; display:flex; align-items:center; gap:3px; }
    .oferta-nombre { font-family:var(--font-display); font-size:14px; color:var(--text); margin-bottom:6px; line-height:1.3; min-height:34px; }
    .precio-original { font-size:12px; color:var(--text-muted); text-decoration:line-through; }
    .precio-oferta { font-size:19px; font-weight:700; color:#C0392B; margin-bottom:12px; margin-top:2px; }
    .btn-agregar-o { width:100%; background:var(--primary); color:var(--white); border:none; border-radius:8px; padding:9px; font-size:12px; font-weight:600; cursor:pointer; transition:background .15s; font-family:var(--font-body); }
    .btn-agregar-o:hover { background:var(--accent); }
    .page-header { display:flex; align-items:center; justify-content:space-between; flex-wrap:wrap; gap:12px; margin-bottom:4px; }
    .page-title { font-family:var(--font-display); font-size:28px; color:var(--primary); }
    .badge-count { background:var(--cream-dark); color:var(--text-mid); padding:4px 12px; border-radius:20px; font-size:13px; font-weight:500; }
    .empty-state { text-align:center; padding:60px 20px; color:var(--text-muted); grid-column:1/-1; }
    @@media (max-width:600px) { .ofertas-banner { flex-direction:column; padding:22px; } .ofertas-banner-text h2 { font-size:22px; } .ofertas-banner-icon { font-size:44px; } }
</style>

<div class="page-header">
    <h1 class="page-title">&#128293; Ofertas y Descuentos</h1>
    <a href="@Url.Action("Index","Catalogo")" style="font-size:13px;color:var(--accent);font-weight:600;text-decoration:none;">
        Ver cat&#225;logo completo &#8594;
    </a>
</div>

<div class="ofertas-banner">
    <div class="ofertas-banner-text">
        <div class="ob-tag">Ofertas especiales</div>
        <h2>Hasta 50% de descuento</h2>
        <p>En muebles seleccionados de todas las categor&#237;as. Oferta por tiempo limitado.</p>
    </div>
    <div class="ofertas-banner-icon">&#127991;</div>
</div>

<div class="cat-row" id="pillRow">
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

<div style="display:flex;align-items:center;justify-content:flex-end;margin-bottom:16px;">
    <span class="badge-count" id="conteo">@Model.Count() ofertas</span>
</div>

<div class="oferta-grid" id="grid">
@Code
    If Not Model.Any() Then
End Code
    <div class="empty-state"><h3>No hay ofertas disponibles en este momento</h3></div>
@Code
    End If
    For Each p In Model
        Dim fotoSrc As String = If(String.IsNullOrEmpty(p.FotoUrl), "/Resources/placeholder.png", Url.Content(p.FotoUrl))
        Dim catNom As String = If(p.TipoMuebleNombre, "")
        Dim catIcon As String = CategoryIcons.GetIcon(catNom)
        Dim prodNom As String = If(p.Nombre, "")
        Dim desc As Integer = If(p.Descuento.HasValue, p.Descuento.Value, 0)
        Dim precioDesc As Decimal = Math.Round(p.Precio * (1 - desc / 100D), 2)
End Code
<div class="oferta-card" data-categoria="@catNom" data-nombre="@prodNom.ToLower()">
    <div class="oferta-img-wrap">
        <img src="@fotoSrc" alt="@prodNom" onerror="this.onerror=null;this.src='/Resources/placeholder.png'" />
        <span class="badge-desc">-@desc%</span>
        <button class="btn-heart-o"
            data-id="@p.ProductoId"
            data-nombre="@prodNom"
            data-imagen="@fotoSrc"
            data-precio="@p.Precio"
            data-categoria="@catNom"
            onclick="MLA.toggleWishlist(this)" title="Favorito">&#9829;</button>
    </div>
    <div class="oferta-body">
        <div class="oferta-cat-tag">@Html.Raw(catIcon) @catNom</div>
        <div class="oferta-nombre">@prodNom</div>
        <div class="precio-original">Q @p.Precio.ToString("N2")</div>
        <div class="precio-oferta">Q @precioDesc.ToString("N2")</div>
        <button type="button" class="btn-agregar-o"
            data-id="@p.ProductoId"
            data-nombre="@prodNom"
            data-imagen="@fotoSrc"
            data-precio="@precioDesc"
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
    var cards = document.querySelectorAll('.oferta-card');
    var visible = 0;
    cards.forEach(function(c) {
        var matchCat = catActual === 'todos' || norm(c.dataset.categoria) === norm(catActual);
        c.style.display = matchCat ? '' : 'none';
        if (matchCat) visible++;
    });
    document.getElementById('conteo').textContent = visible + ' ofertas';
}
</script>
