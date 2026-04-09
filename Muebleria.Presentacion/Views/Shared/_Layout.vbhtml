<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData("Title") - Muebles Los Alpes</title>
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@400;600;700&family=DM+Sans:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <style>
        :root {
            --primary: #4A2C17;
            --primary-light: #7A4A2A;
            --accent: #C8913A;
            --accent-light: #E8B86D;
            --cream: #FAF6F0;
            --cream-dark: #F0E8DC;
            --text: #1C1008;
            --text-mid: #5A3E28;
            --text-muted: #9A7A5A;
            --white: #FFFFFF;
            --danger: #C0392B;
            --success: #27AE60;
            --border: #E0D0BC;
            --shadow: 0 2px 16px rgba(74,44,23,0.10);
            --shadow-lg: 0 8px 40px rgba(74,44,23,0.18);
            --radius: 10px;
            --radius-lg: 18px;
            --font-display: 'Playfair Display', Georgia, serif;
            --font-body: 'DM Sans', sans-serif;
        }

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: var(--font-body);
            background: var(--cream);
            color: var(--text);
            min-height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .topbar {
            background: var(--primary);
            color: #F5DEB3;
            font-size: 12.5px;
            text-align: center;
            padding: 7px;
            letter-spacing: .04em;
        }

        header {
            background: var(--white);
            border-bottom: 2px solid var(--border);
            position: sticky;
            top: 0;
            z-index: 200;
            box-shadow: var(--shadow);
        }

        .header-inner {
            display: flex;
            align-items: center;
            gap: 14px;
            max-width: 1280px;
            margin: auto;
            padding: 12px 24px;
        }

        .logo {
            display: flex;
            align-items: center;
            gap: 10px;
            text-decoration: none;
            flex-shrink: 0;
        }

        .logo-icon {
            width: 44px;
            height: 44px;
            background: var(--primary);
            border-radius: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
        }

            .logo-icon svg {
                width: 26px;
                height: 26px;
                fill: var(--accent);
            }

        .logo-text {
            font-family: var(--font-display);
            color: var(--primary);
        }

            .logo-text span {
                display: block;
                font-size: 10px;
                font-weight: 400;
                color: var(--text-muted);
                letter-spacing: .1em;
                text-transform: uppercase;
            }

            .logo-text strong {
                font-size: 20px;
                font-weight: 700;
                line-height: 1;
            }

        .header-actions {
            display: flex;
            align-items: center;
            gap: 4px;
            margin-left: auto;
            flex-shrink: 0;
            flex-wrap: wrap;
        }

        .hbtn {
            display: flex;
            align-items: center;
            justify-content: center;
            background: none;
            border: none;
            cursor: pointer;
            padding: 8px 10px;
            color: var(--text-mid);
            border-radius: var(--radius);
            transition: background .15s, color .15s;
            text-decoration: none;
            font-family: var(--font-body);
            font-size: 13px;
            font-weight: 500;
            gap: 5px;
        }

            .hbtn:hover {
                background: var(--cream-dark);
                color: var(--primary);
            }

            .hbtn svg {
                width: 18px;
                height: 18px;
                flex-shrink: 0;
            }

        .hbtn-primary {
            background: var(--primary);
            color: var(--white) !important;
            border-radius: 8px;
            padding: 8px 16px;
        }

            .hbtn-primary:hover {
                background: var(--primary-light) !important;
            }

        .user-greeting {
            font-size: 12px;
            color: var(--text-muted);
            padding: 0 6px;
        }

        nav {
            background: var(--primary);
        }

        .nav-inner {
            display: flex;
            max-width: 1280px;
            margin: auto;
            padding: 0 24px;
            overflow-x: auto;
        }

        .nav-item {
            color: #F5DEB3;
            font-size: 13px;
            font-weight: 500;
            padding: 13px 16px;
            border-bottom: 3px solid transparent;
            white-space: nowrap;
            transition: background .15s, border-color .15s, color .15s;
            text-decoration: none;
            display: block;
        }

            .nav-item:hover, .nav-item.active {
                background: rgba(255,255,255,.07);
                border-bottom-color: var(--accent);
                color: var(--accent-light);
            }

        .main-content {
            flex: 1;
            max-width: 1280px;
            width: 100%;
            margin: auto;
            padding: 32px 24px;
        }

        #toast {
            position: fixed;
            bottom: 24px;
            left: 50%;
            transform: translateX(-50%) translateY(80px);
            background: var(--primary);
            color: var(--white);
            padding: 10px 24px;
            border-radius: 50px;
            font-size: 13px;
            font-weight: 500;
            z-index: 999;
            transition: transform .3s;
            box-shadow: var(--shadow-lg);
            pointer-events: none;
            white-space: nowrap;
        }

            #toast.show {
                transform: translateX(-50%) translateY(0);
            }

        footer {
            background: var(--primary);
            color: #C8A882;
            margin-top: auto;
        }

        .footer-inner {
            max-width: 1280px;
            margin: auto;
            padding: 44px 24px 22px;
        }

        .footer-grid {
            display: grid;
            grid-template-columns: 2fr 1fr 1fr 1fr;
            gap: 36px;
            margin-bottom: 36px;
        }

        .footer-brand h3 {
            font-family: var(--font-display);
            font-size: 22px;
            color: var(--white);
            margin-bottom: 10px;
        }

        .footer-brand p {
            font-size: 13px;
            line-height: 1.7;
            opacity: .75;
            margin: 0;
        }

        .footer-col h4 {
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: .1em;
            color: var(--accent-light);
            font-weight: 700;
            margin-bottom: 12px;
        }

        .footer-col ul {
            list-style: none;
            display: flex;
            flex-direction: column;
            gap: 7px;
            padding: 0;
        }

        .footer-col li {
            font-size: 13px;
            opacity: .75;
        }

        .footer-bottom {
            border-top: 1px solid rgba(255,255,255,.12);
            padding-top: 18px;
            text-align: center;
            font-size: 12px;
            opacity: .5;
        }

        @@media (max-width: 768px) {
            .footer-grid {
                grid-template-columns: 1fr 1fr;
            }

            .header-inner {
                flex-wrap: wrap;
            }
        }
    </style>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>

    <div class="topbar">🪑 Envio gratis en compras mayores a Q1,500 · Mas de 60 disenos exclusivos · Guatemala y Centroamerica</div>

    <header>
        <div class="header-inner">
            <a class="logo" href="@Url.Action("Index", "Home")">
                <div class="logo-icon">
                    <svg viewBox="0 0 24 24"><path d="M3 13h2v8H3zm4-4h2v12H7zm4-4h2v16h-2zm4 2h2v14h-2zm4-4h2v18h-2z" /></svg>
                </div>
                <div class="logo-text">
                    <span>Muebles</span>
                    <strong>Los Alpes</strong>
                </div>
            </a>

            <div class="header-actions">
                @If Session("Usuario") IsNot Nothing Then
                    @<span class="user-greeting">Hola, <strong>@Session("Username")</strong></span>
                    @<a class="hbtn" href="@Url.Action("Index", "Carrito")">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="9" cy="21" r="1" /><circle cx="20" cy="21" r="1" /><path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6" /></svg>
                        Carrito
                    </a>
                    @If Session("Rol") = "ADMIN" Then
                        @<a class="hbtn" href="@Url.Action("Index", "Productos")">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="3" width="7" height="7" /><rect x="14" y="3" width="7" height="7" /><rect x="14" y="14" width="7" height="7" /><rect x="3" y="14" width="7" height="7" /></svg>
                            Administrar
                        </a>
                    End If
                    @<a class="hbtn" href="@Url.Action("Logout", "Cuenta")" style="color:var(--danger);">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4" /><polyline points="16 17 21 12 16 7" /><line x1="21" y1="12" x2="9" y2="12" /></svg>
                        Salir
                    </a>
                Else
                    @<a class="hbtn" href="@Url.Action("Login", "Cuenta")">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M15 3h4a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2h-4" /><polyline points="10 17 15 12 10 7" /><line x1="15" y1="12" x2="3" y2="12" /></svg>
                        Iniciar Sesion
                    </a>
                    @<a class="hbtn hbtn-primary" href="@Url.Action("Registro", "Cuenta")">Registrarse</a>
                End If
            </div>
        </div>
    </header>

    <nav>
        <div class="nav-inner">
            <a class="nav-item @(If(ViewContext.RouteData.Values("controller").ToString() = "Home", "active", ""))"
               href="@Url.Action("Index", "Home")">🏠 Inicio</a>
            @If Session("Usuario") IsNot Nothing Then
                @<a class="nav-item @(If(ViewContext.RouteData.Values("controller").ToString() = "Catalogo", "active", ""))"
                    href="@Url.Action("Index", "Catalogo")">🛋️ Catalogo</a>
                @<a class="nav-item @(If(ViewContext.RouteData.Values("controller").ToString() = "Carrito", "active", ""))"
                    href="@Url.Action("Index", "Carrito")">🛒 Mi Carrito</a>
            End If
            @If Session("Rol") = "ADMIN" Then
                @<a class="nav-item @(If(ViewContext.RouteData.Values("controller").ToString() = "Productos", "active", ""))"
                    href="@Url.Action("Index", "Productos")">📦 Productos</a>
                @<a class="nav-item @(If(ViewContext.RouteData.Values("controller").ToString() = "Clientes", "active", ""))"
                    href="@Url.Action("Index", "Clientes")">👥 Clientes</a>
            End If
        </div>
    </nav>

    <div class="main-content">
        @RenderBody()
    </div>

    <div id="toast"></div>

    <footer>
        <div class="footer-inner">
            <div class="footer-grid">
                <div class="footer-brand">
                    <h3>🪑 Muebles Los Alpes</h3>
                    <p>Disenos exclusivos para interior y exterior. Alta calidad, elegancia atemporal y materiales premium que perduran generacion tras generacion.</p>
                </div>
                <div class="footer-col">
                    <h4>Categorias</h4>
                    <ul>
                        <li>Sala</li>
                        <li>Comedor</li>
                        <li>Habitaciones</li>
                        <li>Jardin & Exterior</li>
                        <li>Oficina</li>
                    </ul>
                </div>
                <div class="footer-col">
                    <h4>Empresa</h4>
                    <ul>
                        <li>Nosotros</li>
                        <li>Sucursales</li>
                        <li>Trabaja con nosotros</li>
                        <li>Sostenibilidad</li>
                    </ul>
                </div>
                <div class="footer-col">
                    <h4>Ayuda</h4>
                    <ul>
                        <li>Preguntas frecuentes</li>
                        <li>Contacto</li>
                        <li>Politica de devolucion</li>
                        <li>Garantia</li>
                    </ul>
                </div>
            </div>
            <div class="footer-bottom">© 2026 Muebles Los Alpes · Guatemala · Todos los derechos reservados</div>
        </div>
    </footer>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)

    <script>function showToast(msg) {
            const t = document.getElementById('toast');
            t.textContent = msg;
            t.classList.add('show');
            setTimeout(() => t.classList.remove('show'), 2600);
        }</script>
</body>
</html>
