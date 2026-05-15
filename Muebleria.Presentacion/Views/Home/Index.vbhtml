@Code
    ViewData("Title") = "Inicio"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim destacados As List(Of CE_Producto) = Nothing
    Dim novedades As List(Of CE_Producto) = Nothing
    If ViewBag.Destacados IsNot Nothing Then
        destacados = DirectCast(ViewBag.Destacados, List(Of CE_Producto))
    End If
    If ViewBag.Novedades IsNot Nothing Then
        novedades = DirectCast(ViewBag.Novedades, List(Of CE_Producto))
    End If
End Code

<style>
    .hero { background:linear-gradient(120deg,var(--primary) 0%,#7A4A2A 60%,#8B6340 100%); border-radius:var(--radius-lg); padding:52px 56px; color:var(--white); display:flex; align-items:center; justify-content:space-between; gap:32px; margin-bottom:40px; overflow:hidden; }
    .hero-tag { font-size:11px; letter-spacing:.12em; text-transform:uppercase; color:#E8B86D; font-weight:600; margin-bottom:12px; }
    .hero h1 { font-family:var(--font-display); font-size:40px; line-height:1.1; margin-bottom:14px; }
    .hero p { font-size:15px; color:rgba(255,255,255,.82); max-width:420px; line-height:1.6; margin-bottom:26px; }
    .btn-hero { display:inline-block; background:var(--accent); color:var(--white); padding:13px 30px; border-radius:var(--radius); font-weight:600; font-size:15px; text-decoration:none; }
    .btn-hero:hover { background:#E8B86D; color:var(--white); text-decoration:none; }
    .hero-visual { font-size:96px; flex-shrink:0; line-height:1; filter:drop-shadow(0 8px 24px rgba(0,0,0,.3)); }
    .section-head { display:flex; align-items:baseline; justify-content:space-between; margin-bottom:20px; }
    .section-head h2 { font-family:var(--font-display); font-size:26px; color:var(--primary); }
    .see-all { font-size:13px; color:var(--accent); text-decoration:none; font-weight:500; }
    .see-all:hover { text-decoration:underline; }
    .product-grid { display:grid; grid-template-columns:repeat(auto-fill,minmax(210px,1fr)); gap:20px; margin-bottom:44px; }
    .product-card { background:var(--white); border-radius:var(--radius-lg); border:1.5px solid var(--border); overflow:hidden; transition:box-shadow .2s,transform .2s; }
    .product-card:hover { box-shadow:var(--shadow-lg); transform:translateY(-4px); }
    .product-img-wrap { width:100%; height:175px; overflow:hidden; background:var(--cream-dark); }
    .product-img-wrap img { width:100%; height:100%; object-fit:cover; display:block; }
    .product-info { padding:14px 16px; }
    .product-tag { font-size:10px; text-transform:uppercase; letter-spacing:.08em; color:var(--accent); font-weight:600; margin-bottom:4px; }
    .product-name { font-family:var(--font-display); font-size:14px; color:var(--text); margin-bottom:8px; line-height:1.3; min-height:36px; }
    .product-price { font-size:18px; font-weight:700; color:var(--primary); margin-bottom:12px; }
    .product-price span { font-size:11px; font-weight:400; color:var(--text-muted); }
    .btn-add { width:100%; padding:9px; background:var(--primary); color:var(--white); border:none; border-radius:var(--radius); cursor:pointer; font-size:13px; font-weight:600; transition:background .2s; font-family:var(--font-body); }
    .btn-add:hover { background:var(--accent); }
    .promo-grid { display:grid; grid-template-columns:1fr 1fr; gap:16px; margin-bottom:40px; }
    .promo-card { border-radius:var(--radius-lg); padding:26px 30px; display:flex; align-items:center; justify-content:space-between; gap:16px; color:var(--white); text-decoration:none; transition:transform .2s; }
    .promo-card:hover { transform:scale(1.02); color:var(--white); text-decoration:none; }
    .promo-card.a { background:linear-gradient(135deg,#4A2C17,#7A4A2A); }
    .promo-card.b { background:linear-gradient(135deg,#C8913A,#A0702A); }
    .promo-card.c { background:linear-gradient(135deg,#1E5A8A,#2980B9); }
    .promo-card.d { background:linear-gradient(135deg,#1E4A2A,#2E7A40); }
    .promo-card h3 { font-family:var(--font-display); font-size:20px; margin-bottom:5px; }
    .promo-card p { font-size:12px; opacity:.85; margin:0; }
    .promo-icon { font-size:48px; flex-shrink:0; }
    @@media(max-width:768px) {
        .hero { flex-direction:column; padding:28px 20px; }
        .hero h1 { font-size:26px; }
        .hero-visual { font-size:60px; }
        .promo-grid { grid-template-columns:1fr; }
    }
</style>

<div class="hero">
    <div>
        <div class="hero-tag">Nueva colecci&#243;n 2026</div>
        <h1>Dise&#241;o que<br>transforma espacios</h1>
        <p>Muebles exclusivos para interior y exterior. Alta calidad, elegancia atemporal y materiales premium que perduran.</p>
        @Html.ActionLink("Ver cat&#225;logo completo", "Index", "Catalogo", Nothing, New With {.class = "btn-hero"})
    </div>
    <div class="hero-visual">&#x1FA91;</div>
</div>

@Code
    If destacados IsNot Nothing AndAlso destacados.Count > 0 Then
End Code
<div class="section-head">
    <h2>&#11088; M&#225;s vendidos</h2>
    <a class="see-all" href="@Url.Action("Index","Catalogo")">Ver todos</a>
</div>
<div class="product-grid">
@Code
    For Each p In destacados
        Dim fotoSrc As String = If(String.IsNullOrEmpty(p.FotoUrl), "/Resources/placeholder.png", Url.Content(p.FotoUrl))
        Dim catNom As String = If(p.TipoMuebleNombre, "")
        Dim prodNom As String = If(p.Nombre, "")
End Code
    <div class="product-card">
        <div class="product-img-wrap">
            <img src="@fotoSrc" alt="@p.Nombre" onerror="this.onerror=null;this.src='/Resources/placeholder.png'" />
        </div>
        <div class="product-info">
            <div class="product-tag">@Html.Raw(CategoryIcons.GetIcon(catNom)) @catNom</div>
            <div class="product-name">@p.Nombre</div>
            <div class="product-price">Q @p.Precio.ToString("N2") <span>GTQ</span></div>
            <button type="button" class="btn-add"
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
@Code
    End If
End Code

@Code
    If novedades IsNot Nothing AndAlso novedades.Count > 0 Then
End Code
<div class="section-head">
    <h2>&#10024; Novedades</h2>
    <a class="see-all" href="@Url.Action("Index","Catalogo")">Ver todas</a>
</div>
<div class="product-grid">
@Code
    For Each p In novedades
        Dim fotoSrc As String = If(String.IsNullOrEmpty(p.FotoUrl), "/Resources/placeholder.png", Url.Content(p.FotoUrl))
        Dim catNom As String = If(p.TipoMuebleNombre, "")
        Dim prodNom As String = If(p.Nombre, "")
End Code
    <div class="product-card">
        <div class="product-img-wrap">
            <img src="@fotoSrc" alt="@p.Nombre" onerror="this.onerror=null;this.src='/Resources/placeholder.png'" />
        </div>
        <div class="product-info">
            <div class="product-tag">&#10024; Nuevo &middot; @catNom</div>
            <div class="product-name">@p.Nombre</div>
            <div class="product-price">Q @p.Precio.ToString("N2") <span>GTQ</span></div>
            <button type="button" class="btn-add"
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
@Code
    End If
End Code

<div class="promo-grid">
    <a class="promo-card a" href="@Url.Action("Index","Catalogo",New With {.cat="sala"})">
        <div><h3>Colecci&#243;n Sala</h3><p>Sof&#225;s, sillones y mesas de centro para cada estilo</p></div>
        <div class="promo-icon">&#x1F6CB;&#xFE0F;</div>
    </a>
    <a class="promo-card b" href="@Url.Action("Index","Catalogo",New With {.cat="comedor"})">
        <div><h3>Colecci&#243;n Comedor</h3><p>Comedores de madera, vidrio y metal</p></div>
        <div class="promo-icon">&#x1F37D;&#xFE0F;</div>
    </a>
    <a class="promo-card c" href="@Url.Action("Index","Catalogo",New With {.cat="oficina"})">
        <div><h3>Oficina Ejecutiva</h3><p>Escritorios, sillas ergon&#243;micas y m&#225;s</p></div>
        <div class="promo-icon">&#x1F4BC;</div>
    </a>
    <a class="promo-card d" href="@Url.Action("Index","Catalogo",New With {.cat="jardin"})">
        <div><h3>Jard&#237;n y Exterior</h3><p>Sets en rat&#225;n y aluminio para exteriores</p></div>
        <div class="promo-icon">&#x1F33F;</div>
    </a>
</div>
