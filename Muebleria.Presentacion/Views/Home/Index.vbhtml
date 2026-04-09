@Code
    ViewData("Title") = "Inicio"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .hero {
        background: linear-gradient(120deg, var(--primary) 0%, var(--primary-light) 60%, #8B6340 100%);
        border-radius: var(--radius-lg);
        padding: 52px 56px;
        color: var(--white);
        display: flex;
        align-items: center;
        justify-content: space-between;
        gap: 32px;
        margin-bottom: 40px;
        position: relative;
        overflow: hidden;
    }

    .hero-text {
        position: relative;
    }

    .hero-tag {
        font-size: 11px;
        letter-spacing: .12em;
        text-transform: uppercase;
        color: var(--accent-light);
        font-weight: 600;
        margin-bottom: 12px;
    }

    .hero h1 {
        font-family: var(--font-display);
        font-size: 40px;
        line-height: 1.1;
        margin-bottom: 14px;
    }

    .hero p {
        font-size: 15px;
        color: rgba(255,255,255,.80);
        max-width: 420px;
        line-height: 1.6;
        margin-bottom: 26px;
        margin-top: 0;
    }

    .btn-hero {
        display: inline-block;
        background: var(--accent);
        color: var(--white);
        padding: 13px 30px;
        border-radius: var(--radius);
        font-weight: 600;
        font-size: 15px;
        border: none;
        cursor: pointer;
        transition: background .2s, transform .15s;
        font-family: var(--font-body);
        text-decoration: none;
    }

        .btn-hero:hover {
            background: var(--accent-light);
            transform: translateY(-2px);
            color: var(--white);
        }

    .hero-visual {
        font-size: 100px;
        flex-shrink: 0;
        line-height: 1;
    }

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

    .promo-grid {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 16px;
        margin-bottom: 40px;
    }

    .promo-card {
        border-radius: var(--radius-lg);
        padding: 26px 30px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        gap: 16px;
        color: var(--white);
        cursor: pointer;
        transition: transform .2s;
        text-decoration: none;
    }

        .promo-card:hover {
            transform: scale(1.02);
        }

        .promo-card.a {
            background: linear-gradient(135deg, #4A2C17, #7A4A2A);
        }

        .promo-card.b {
            background: linear-gradient(135deg, #C8913A, #A0702A);
        }

        .promo-card.c {
            background: linear-gradient(135deg, #1E5A8A, #2980B9);
        }

        .promo-card.d {
            background: linear-gradient(135deg, #1E4A2A, #2E7A40);
        }

        .promo-card h3 {
            font-family: var(--font-display);
            font-size: 20px;
            margin-bottom: 5px;
        }

        .promo-card p {
            font-size: 12px;
            opacity: .85;
            margin: 0;
        }

    .promo-icon {
        font-size: 48px;
    }

    .features-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 20px;
        margin-bottom: 40px;
    }

    .feature-card {
        background: var(--white);
        border-radius: var(--radius-lg);
        border: 1.5px solid var(--border);
        padding: 28px 24px;
        text-align: center;
        box-shadow: var(--shadow);
    }

    .feature-icon {
        font-size: 36px;
        margin-bottom: 12px;
    }

    .feature-card h3 {
        font-family: var(--font-display);
        font-size: 16px;
        color: var(--primary);
        margin-bottom: 6px;
    }

    .feature-card p {
        font-size: 13px;
        color: var(--text-muted);
        line-height: 1.5;
        margin: 0;
    }

    @@media (max-width: 768px) {
        .hero {
            padding: 28px 20px;
            flex-direction: column;
        }

            .hero h1 {
                font-size: 26px;
            }

        .promo-grid {
            grid-template-columns: 1fr;
        }

        .features-grid {
            grid-template-columns: 1fr;
        }
    }
</style>

<!-- HERO -->
<div class="hero">
    <div class="hero-text">
        <div class="hero-tag">✦ Nueva coleccion 2026</div>
        <h1>Diseno que<br />transforma espacios</h1>
        <p>Muebles exclusivos para interior y exterior. Alta calidad, elegancia atemporal y materiales premium que perduran.</p>
        @If Session("Usuario") IsNot Nothing Then
            @<a class="btn-hero" href="@Url.Action("Index", "Catalogo")">Ver catalogo completo →</a>
        Else
            @<a class="btn-hero" href="@Url.Action("Login", "Cuenta")">Inicia sesion para comprar →</a>
        End If
    </div>
    <div class="hero-visual">🪑</div>
</div>

<!-- CATEGORIAS PROMO -->
<div class="section-head">
    <h2>Nuestras Colecciones</h2>
</div>
<div class="promo-grid">
    <a class="promo-card a" href="@Url.Action("Index", "Catalogo")">
        <div><h3>Coleccion Sala</h3><p>Sofas, sillones y mesas de centro para cada estilo</p></div>
        <div class="promo-icon">🛋️</div>
    </a>
    <a class="promo-card b" href="@Url.Action("Index", "Catalogo")">
        <div><h3>Coleccion Comedor</h3><p>Comedores de madera, vidrio y metal</p></div>
        <div class="promo-icon">🍽️</div>
    </a>
    <a class="promo-card c" href="@Url.Action("Index", "Catalogo")">
        <div><h3>Oficina Ejecutiva</h3><p>Escritorios, sillas ergonomicas y mas</p></div>
        <div class="promo-icon">💼</div>
    </a>
    <a class="promo-card d" href="@Url.Action("Index", "Catalogo")">
        <div><h3>Jardin & Exterior</h3><p>Sets en ratan y aluminio para exteriores</p></div>
        <div class="promo-icon">🌿</div>
    </a>
</div>

<!-- CARACTERISTICAS -->
<div class="section-head">
    <h2>Por que elegirnos</h2>
</div>
<div class="features-grid">
    <div class="feature-card">
        <div class="feature-icon">🚚</div>
        <h3>Envio Gratis</h3>
        <p>En compras mayores a Q1,500 en Guatemala y Centroamerica</p>
    </div>
    <div class="feature-card">
        <div class="feature-icon">🛡️</div>
        <h3>Garantia de Calidad</h3>
        <p>Todos nuestros muebles tienen garantia de 2 anos contra defectos de fabrica</p>
    </div>
    <div class="feature-card">
        <div class="feature-icon">🎨</div>
        <h3>Disenos Exclusivos</h3>
        <p>Mas de 60 disenos unicos creados por disenadores guatemaltecos</p>
    </div>
</div>
