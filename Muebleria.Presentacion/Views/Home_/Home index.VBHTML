@Code
    ViewData("Title") = "Inicio"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
  .hero { background: linear-gradient(120deg, var(--primary) 0%, var(--primary-light) 60%, #8B6340 100%); border-radius: var(--radius-lg); padding: 52px 56px; color: var(--white); display: flex; align-items: center; justify-content: space-between; gap: 32px; margin-bottom: 40px; position: relative; overflow: hidden; }
  .hero-tag { font-size: 11px; letter-spacing: .12em; text-transform: uppercase; color: var(--accent-light); font-weight: 600; margin-bottom: 12px; }
  .hero h1 { font-family: var(--font-display); font-size: 40px; line-height: 1.1; margin-bottom: 14px; }
  .hero p { font-size: 15px; color: rgba(255,255,255,.80); max-width: 420px; line-height: 1.6; margin-bottom: 26px; }
  .btn-hero { display: inline-block; background: var(--accent); color: var(--white); padding: 13px 30px; border-radius: var(--radius); font-weight: 600; font-size: 15px; border: none; cursor: pointer; transition: background .2s; font-family: var(--font-body); text-decoration: none; }
  .btn-hero:hover { background: var(--accent-light); color: var(--primary); }
  .hero-visual { font-size: 90px; flex-shrink: 0; }
  .section-head { display: flex; align-items: baseline; justify-content: space-between; margin-bottom: 20px; margin-top: 40px; }
  .section-head h2 { font-family: var(--font-display); font-size: 26px; color: var(--primary); }
  .see-all { font-size: 13px; color: var(--accent); font-weight: 600; text-decoration: none; cursor: pointer; }
  .see-all:hover { text-decoration: underline; }
  .promo-grid { display: grid; grid-template-columns: repeat(4, 1fr); gap: 16px; margin: 32px 0; }
  .promo-card { border-radius: var(--radius-lg); padding: 24px 20px; display: flex; justify-content: space-between; align-items: center; cursor: pointer; transition: transform .2s; text-decoration: none; }
  .promo-card:hover { transform: translateY(-3px); }
  .promo-card h3 { font-family: var(--font-display); font-size: 17px; color: var(--white); margin-bottom: 6px; }
  .promo-card p { font-size: 12px; color: rgba(255,255,255,.8); }
  .promo-card .promo-icon { font-size: 40px; }
  .promo-card.a { background: linear-gradient(135deg, #4A2C17, #7A4A2A); }
  .promo-card.b { background: linear-gradient(135deg, #2E7A40, #3A9A50); }
  .promo-card.c { background: linear-gradient(135deg, #2980B9, #1A6A99); }
  .promo-card.d { background: linear-gradient(135deg, #C8913A, #E8B86D); }
  .stats-row { display: grid; grid-template-columns: repeat(3, 1fr); gap: 20px; margin-bottom: 40px; }
  .stat-box { background: var(--white); border-radius: var(--radius-lg); box-shadow: var(--shadow); padding: 24px; text-align: center; }
  .stat-box .stat-num { font-family: var(--font-display); font-size: 32px; font-weight: 700; color: var(--primary); }
  .stat-box .stat-lbl { font-size: 13px; color: var(--text-muted); margin-top: 6px; }
  @@media(max-width:768px) {
    .hero { flex-direction: column; padding: 28px 20px; }
    .hero h1 { font-size: 26px; }
    .hero-visual { display: none; }
    .promo-grid { grid-template-columns: 1fr 1fr; }
    .stats-row { grid-template-columns: 1fr; }
  }
</style>

@* Hero *@
<div class="hero">
    <div>
        <div class="hero-tag">Nueva coleccion 2026</div>
        <h1>Diseno que<br>transforma espacios</h1>
        <p>Muebles exclusivos para interior y exterior. Alta calidad, elegancia atemporal y materiales premium que perduran.</p>
        @Html.ActionLink("Ver catalogo completo", "Index", "Catalogo",
            Nothing, New With {.class = "btn-hero"})
    </div>
    <div class="hero-visual">[^]</div>
</div>

@* Stats *@
<div class="stats-row">
    <div class="stat-box">
        <div class="stat-num">60+</div>
        <div class="stat-lbl">Disenos exclusivos</div>
    </div>
    <div class="stat-box">
        <div class="stat-num">Q1,500</div>
        <div class="stat-lbl">Envio gratis desde</div>
    </div>
    <div class="stat-box">
        <div class="stat-num">5 anos</div>
        <div class="stat-lbl">Garantia en estructura</div>
    </div>
</div>

@* Secciones destacadas *@
<div class="section-head">
    <h2>Explora por categoria</h2>
    @Html.ActionLink("Ver todos", "Index", "Catalogo", Nothing, New With {.class = "see-all"})
</div>

<div class="promo-grid">
    <a href="@Url.Action("Index","Catalogo")" class="promo-card a">
        <div><h3>Sala</h3><p>Sofas, sillones y mesas de centro</p></div>
        <div class="promo-icon">[S]</div>
    </a>
    <a href="@Url.Action("Index","Catalogo")" class="promo-card b">
        <div><h3>Comedor</h3><p>Comedores de madera, vidrio y metal</p></div>
        <div class="promo-icon">[C]</div>
    </a>
    <a href="@Url.Action("Index","Catalogo")" class="promo-card c">
        <div><h3>Oficina Ejecutiva</h3><p>Escritorios y sillas ergonomicas</p></div>
        <div class="promo-icon">[O]</div>
    </a>
    <a href="@Url.Action("Index","Catalogo")" class="promo-card d">
        <div><h3>Jardin</h3><p>Sets en ratan y aluminio para exterior</p></div>
        <div class="promo-icon">[J]</div>
    </a>
</div>

@* Por que elegirnos *@
<div class="section-head">
    <h2>Por que elegirnos</h2>
</div>
<div style="display:grid;grid-template-columns:repeat(auto-fill,minmax(220px,1fr));gap:20px;margin-bottom:48px;">
    <div style="background:var(--white);border-radius:var(--radius-lg);box-shadow:var(--shadow);padding:24px;">
        <div style="font-size:28px;margin-bottom:12px;">[D]</div>
        <div style="font-weight:600;color:var(--primary);margin-bottom:6px;">Diseno exclusivo</div>
        <div style="font-size:13px;color:var(--text-muted);line-height:1.5;">Cada pieza disenada por especialistas con tendencias actuales.</div>
    </div>
    <div style="background:var(--white);border-radius:var(--radius-lg);box-shadow:var(--shadow);padding:24px;">
        <div style="font-size:28px;margin-bottom:12px;">[M]</div>
        <div style="font-weight:600;color:var(--primary);margin-bottom:6px;">Materiales premium</div>
        <div style="font-size:13px;color:var(--text-muted);line-height:1.5;">Maderas certificadas, telas resistentes y estructuras de acero.</div>
    </div>
    <div style="background:var(--white);border-radius:var(--radius-lg);box-shadow:var(--shadow);padding:24px;">
        <div style="font-size:28px;margin-bottom:12px;">[E]</div>
        <div style="font-weight:600;color:var(--primary);margin-bottom:6px;">Envio a domicilio</div>
        <div style="font-size:13px;color:var(--text-muted);line-height:1.5;">Gratis en compras mayores a Q1,500 en Guatemala.</div>
    </div>
    <div style="background:var(--white);border-radius:var(--radius-lg);box-shadow:var(--shadow);padding:24px;">
        <div style="font-size:28px;margin-bottom:12px;">[G]</div>
        <div style="font-weight:600;color:var(--primary);margin-bottom:6px;">Garantia de 5 anos</div>
        <div style="font-size:13px;color:var(--text-muted);line-height:1.5;">Respaldamos la estructura de todos nuestros muebles.</div>
    </div>
</div>
