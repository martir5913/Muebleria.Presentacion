@Code       ViewData("Title") = "Iniciar Sesion"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script src="https://kit.fontawesome.com/869dc8f5ef.js" crossorigin="anonymous"></script>
    <title>Iniciar Sesion - Muebleria</title>
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@400;600;700&family=DM+Sans:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <style>
        :root {
            --primary:       #4A2C17;
            --primary-light: #7A4A2A;
            --accent:        #C8913A;
            --cream:         #FAF6F0;
            --cream-dark:    #F0E8DC;
            --border:        #E0D0BC;
            --white:         #FFFFFF;
            --font-display:  'Playfair Display', Georgia, serif;
            --font-body:     'DM Sans', sans-serif;
        }

        * { box-sizing: border-box; }

        body {
            font-family: var(--font-body);
            background: var(--cream);
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        h1 {
            font-family: var(--font-display);
            font-weight: 700;
            margin: 0;
            font-size: 22px;
            color: var(--primary);
        }

        p {
            font-size: 14px;
            font-weight: 300;
            line-height: 20px;
            letter-spacing: 0.5px;
            margin: 20px 0 30px;
            color: var(--primary-light);
        }

        span { font-size: 12px; color: var(--primary-light); }

        a {
            color: var(--accent);
            font-size: 14px;
            text-decoration: none;
            margin: 15px 0;
        }

        .error-msg {
            color: #C0392B;
            font-size: 12px;
            margin: 0 0 10px;
        }

        .success-msg {
            color: #27AE60;
            font-size: 12px;
            margin: 0 0 10px;
        }

        .container {
            background: var(--white);
            border-radius: 14px;
            box-shadow: 0 14px 40px rgba(74,44,23,0.18), 0 4px 14px rgba(74,44,23,0.10);
            position: relative;
            overflow: hidden;
            width: 768px;
            max-width: 100%;
            min-height: 480px;
        }

        .sign-in-container form {
            background: var(--white);
            display: flex;
            flex-direction: column;
            padding: 0 50px;
            height: 100%;
            justify-content: center;
            align-items: center;
            text-align: center;
        }

        .sign-up-container form {
            background: var(--white);
            display: flex;
            flex-direction: column;
            padding: 20px 50px;
            height: 100%;
            overflow-y: auto;
            align-items: center;
            text-align: center;
            justify-content: flex-start;
            padding-top: 30px;
            padding-bottom: 30px;
        }

        .social-container { margin: 15px 0; }

        .social-container a {
            border: 1.5px solid var(--border);
            border-radius: 50%;
            display: inline-flex;
            justify-content: center;
            align-items: center;
            margin: 0 5px;
            height: 40px;
            width: 40px;
            color: var(--primary-light);
            transition: border-color .2s, color .2s;
        }

        .social-container a:hover {
            border-color: var(--accent);
            color: var(--accent);
        }

        .form-container input {
            background: var(--cream-dark);
            border: 1.5px solid var(--border);
            padding: 12px 15px;
            margin: 6px 0;
            width: 100%;
            border-radius: 8px;
            font-family: var(--font-body);
            font-size: 13px;
            color: var(--primary);
            transition: border-color .2s;
            outline: none;
        }

        .form-container input:focus {
            border-color: var(--accent);
        }

        button {
            border-radius: 20px;
            border: 1.5px solid var(--primary);
            background: var(--primary);
            color: var(--white);
            font-size: 12px;
            font-weight: 600;
            padding: 12px 45px;
            letter-spacing: 1px;
            text-transform: uppercase;
            transition: transform 80ms ease-in, background .2s;
            cursor: pointer;
            margin-top: 12px;
            font-family: var(--font-body);
        }

        button:hover { background: var(--primary-light); border-color: var(--primary-light); }

        #lila {
            background: var(--accent);
            border-color: var(--accent);
        }

        #lila:hover { background: #b07830; border-color: #b07830; }

        button:active { transform: scale(0.95); }
        button:focus  { outline: none; }

        button.ghost {
            background: transparent;
            border-color: rgba(255,255,255,0.7);
            color: var(--white);
        }

        button.ghost:hover { background: rgba(255,255,255,0.15); }

        .form-container {
            position: absolute;
            top: 0;
            height: 100%;
            transition: all 0.6s ease-in-out;
        }

        .sign-in-container { left: 0; width: 50%; z-index: 2; }
        .sign-up-container { left: 0; width: 50%; opacity: 0; z-index: 1; }

        .overlay-container {
            position: absolute;
            top: 0;
            left: 50%;
            width: 50%;
            height: 100%;
            overflow: hidden;
            transition: transform 0.6s ease-in-out;
            z-index: 100;
        }

        .overlay {
            background: linear-gradient(135deg, var(--primary) 0%, var(--primary-light) 60%, var(--accent) 100%);
            color: var(--white);
            position: relative;
            left: -100%;
            height: 100%;
            width: 200%;
            transform: translateX(0);
            transition: transform 0.6s ease-in-out;
        }

        .overlay-panel {
            position: absolute;
            top: 0;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            padding: 0 40px;
            height: 100%;
            width: 50%;
            text-align: center;
            transform: translateX(0);
            transition: transform 0.6s ease-in-out;
        }

        .overlay-panel h1 { color: var(--white); font-family: var(--font-display); }
        .overlay-panel p  { color: rgba(255,255,255,0.85); }

        .overlay-right { right: 0; }
        .overlay-left  { background: rgba(200,145,58,0.18); }

        .container.right-panel-active .sign-in-container    { transform: translateX(100%); }
        .container.right-panel-active .overlay-container     { transform: translateX(-100%); }
        .container.right-panel-active .sign-up-container {
            transform: translateX(100%);
            opacity: 1;
            z-index: 5;
            animation: show 0.6s;
        }

        @@keyframes show {
            0%, 49.99% { opacity: 0; z-index: 1; }
            50%, 100%  { opacity: 1; z-index: 5; }
        }

        .container.right-panel-active .overlay       { transform: translateX(50%); }
        .container.right-panel-active .overlay-left  { transform: translateX(0); }
        .container.right-panel-active .overlay-right { transform: translateX(20%); }

        .sign-up-container form::-webkit-scrollbar       { width: 4px; }
        .sign-up-container form::-webkit-scrollbar-thumb { background: var(--border); border-radius: 4px; }

        .logo-login {
            display: flex;
            align-items: center;
            gap: 8px;
            margin-bottom: 14px;
        }
        .logo-login-icon {
            width: 36px; height: 36px;
            background: var(--primary);
            border-radius: 8px;
            display: flex; align-items: center; justify-content: center;
        }
        .logo-login-icon svg { width: 20px; height: 20px; fill: var(--accent); }
        .logo-login-text { font-family: var(--font-display); font-size: 16px; font-weight: 700; color: var(--primary); }
    </style>
</head>
<body>
    <div class="container" id="container">

        <!-- Panel Registro -->
        <div class="form-container sign-up-container">
            <form method="post" action="@Url.Action("Registro", "Cuenta")">
                <div class="logo-login">
                    <div class="logo-login-icon">
                        <svg viewBox="0 0 24 24"><path d="M3 13h2v8H3zm4-4h2v12H7zm4-4h2v16h-2zm4 2h2v14h-2zm4-4h2v18h-2z"/></svg>
                    </div>
                    <span class="logo-login-text">Los Alpes</span>
                </div>
                <h1>Crea tu Cuenta</h1>
                <div class="social-container">
                    <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
                    <a href="#" class="social"><i class="fab fa-google" id="red"></i></a>
                    <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
                </div>
                <span>o usa tu email como registro</span>
                <input type="text" name="nombres" placeholder="Nombres" required />
                <input type="text" name="apellidos" placeholder="Apellidos" required />
                <input type="email" name="email" placeholder="Email" required />
                <input type="text" name="direccion" placeholder="Direccion" required />
                <input type="text" name="username" placeholder="Usuario" required />
                <input type="password" name="password" placeholder="Contrasena" required />
                <button type="submit" id="lila">Registrar</button>
            </form>
        </div>

        <!-- Panel Login -->
        <div class="form-container sign-in-container">
            <form method="post" action="@Url.Action("Login", "Cuenta")">
                <div class="logo-login">
                    <div class="logo-login-icon">
                        <svg viewBox="0 0 24 24"><path d="M3 13h2v8H3zm4-4h2v12H7zm4-4h2v16h-2zm4 2h2v14h-2zm4-4h2v18h-2z"/></svg>
                    </div>
                    <span class="logo-login-text">Los Alpes</span>
                </div>
                <h1>Iniciar Sesion</h1>
                <div class="social-container">
                    <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
                    <a href="#" class="social"><i class="fab fa-google" id="red"></i></a>
                    <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
                </div>
                <span>o usa tu usuario</span>
                @If ViewBag.Error IsNot Nothing Then
                    @<p class="error-msg">@ViewBag.Error</p>
                End If
                @If TempData("Exito") IsNot Nothing Then
                    @<p class="success-msg">@TempData("Exito")</p>
                End If
                <input type="text" name="username" placeholder="Usuario" />
                <input type="password" name="password" placeholder="Contrasena" />
                <a href="#">Olvidaste tu contrasena?</a>
                <button type="submit">Iniciar Sesion</button>
            </form>
        </div>

        <!-- Overlay animado -->
        <div class="overlay-container">
            <div class="overlay">
                <div class="overlay-panel overlay-left">
                    <h1>Bienvenido!</h1>
                    <p>Inicia sesion con tu cuenta</p>
                    <button class="ghost" id="signIn">Inicia Sesion</button>
                </div>
                <div class="overlay-panel overlay-right">
                    <h1>Hola!</h1>
                    <p>Crea tu cuenta para comenzar</p>
                    <button class="ghost" id="signUp">Registrar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        const signUpButton = document.getElementById('signUp');
        const signInButton = document.getElementById('signIn');
        const container = document.getElementById('container');

        signUpButton.addEventListener('click', () =>
            container.classList.add('right-panel-active')
        );

        signInButton.addEventListener('click', () =>
            container.classList.remove('right-panel-active')
        );
    </script>
</body>
</html>
