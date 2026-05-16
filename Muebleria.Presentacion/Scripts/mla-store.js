// mla-store.js — Client-side cart + wishlist state for Muebles Los Alpes
(function () {
    'use strict';

    // ── Helpers ──────────────────────────────────────────────────────
    function esc(s) {
        return String(s || '')
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    function fmt(n) {
        return 'Q ' + Number(n).toLocaleString('es-GT', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    }

    function ge(id) { return document.getElementById(id); }

    // ── Cart item HTML ────────────────────────────────────────────────
    function buildCartItems(cart) {
        if (!cart.length) {
            return '<div class="cart-empty">Tu carrito est&#225; vac&#237;o &#x1FA91;<br><small>Agrega muebles para comenzar</small></div>';
        }
        return cart.map(function (item) {
            var meta = esc(item.categoria) + (item.material ? ' &middot; ' + esc(item.material) : '');
            return '<div class="cart-item">' +
                '<img class="cart-item-img" src="' + esc(item.imagen) + '" alt="' + esc(item.nombre) + '" onerror="this.src=\'/Resources/placeholder.png\'">' +
                '<div class="cart-item-info">' +
                    '<div class="cart-item-name">' + esc(item.nombre) + '</div>' +
                    '<div class="cart-item-cat">' + meta + '</div>' +
                    '<div class="cart-item-row">' +
                        '<button class="qty-btn" onclick="MLA.updateQty(' + item.id + ',-1)">&minus;</button>' +
                        '<span class="qty-num">' + item.qty + '</span>' +
                        '<button class="qty-btn" onclick="MLA.updateQty(' + item.id + ',1)">+</button>' +
                        '<span class="cart-item-price">' + fmt(item.precio * item.qty) + '</span>' +
                        '<button class="cart-remove" onclick="MLA.removeFromCart(' + item.id + ')" title="Eliminar">&#128465;</button>' +
                    '</div>' +
                '</div>' +
            '</div>';
        }).join('');
    }

    // ── Wishlist item HTML ────────────────────────────────────────────
    function buildWishItems(wishlist) {
        if (!wishlist.length) {
            return '<div class="fav-empty">No tienes favoritos a&#250;n &#x1F90D;<br><small>Guarda muebles para comprarlos despu&#233;s</small></div>';
        }
        return wishlist.map(function (item) {
            return '<div class="fav-item">' +
                '<img class="fav-item-img" src="' + esc(item.imagen) + '" alt="' + esc(item.nombre) + '" onerror="this.src=\'/Resources/placeholder.png\'">' +
                '<div class="fav-item-info">' +
                    '<div class="fav-item-name">' + esc(item.nombre) + '</div>' +
                    '<div class="fav-item-cat">' + esc(item.categoria) + '</div>' +
                    '<div class="fav-item-price">' + fmt(item.precio) + '</div>' +
                '</div>' +
                '<div class="fav-item-actions">' +
                    '<button class="fav-add-btn" onclick="MLA.addFromWishlist(' + item.id + ')">+ Carrito</button>' +
                    '<button class="fav-remove-btn" onclick="MLA.removeFromWishlist(' + item.id + ')" title="Quitar">&#128465;</button>' +
                '</div>' +
            '</div>';
        }).join('');
    }

    // ── MLA global object ─────────────────────────────────────────────
    window.MLA = {
        cart: [],
        wishlist: [],

        // ── Cart ─────────────────────────────────────────────────
        addToCart: function (ds) {
            var id = parseInt(ds.id || 0);
            var precio = parseFloat(ds.precio || 0);
            if (!id) return;

            var found = false;
            for (var i = 0; i < this.cart.length; i++) {
                if (this.cart[i].id === id) {
                    this.cart[i].qty++;
                    found = true;
                    break;
                }
            }
            if (!found) {
                this.cart.push({
                    id: id,
                    nombre: ds.nombre || '',
                    imagen: ds.imagen || '',
                    precio: precio,
                    qty: 1,
                    categoria: ds.categoria || '',
                    material: ds.material || ''
                });
            }
            this.saveToStorage();
            this.updateBadges();
            this._refreshCart();
            this.openCartDrawer();
        },

        removeFromCart: function (id) {
            id = parseInt(id);
            this.cart = this.cart.filter(function (i) { return i.id !== id; });
            this.saveToStorage();
            this.updateBadges();
            this._refreshCart();
        },

        updateQty: function (id, delta) {
            id = parseInt(id);
            for (var i = 0; i < this.cart.length; i++) {
                if (this.cart[i].id === id) {
                    this.cart[i].qty = Math.max(1, this.cart[i].qty + delta);
                    break;
                }
            }
            this.saveToStorage();
            this.updateBadges();
            this._refreshCart();
        },

        getCartTotal: function () {
            return this.cart.reduce(function (s, i) { return s + i.precio * i.qty; }, 0);
        },

        getCartCount: function () {
            return this.cart.reduce(function (s, i) { return s + i.qty; }, 0);
        },

        checkout: function () {
            window.location.href = '/Carrito';
        },

        // ── Wishlist ──────────────────────────────────────────────
        toggleWishlist: function (btn) {
            var id = parseInt(btn.dataset.id || 0);
            if (!id) return;

            if (this.isInWishlist(id)) {
                this.wishlist = this.wishlist.filter(function (i) { return i.id !== id; });
                btn.classList.remove('fav');
            } else {
                this.wishlist.push({
                    id: id,
                    nombre: btn.dataset.nombre || '',
                    imagen: btn.dataset.imagen || '',
                    precio: parseFloat(btn.dataset.precio || 0),
                    categoria: btn.dataset.categoria || '',
                    material: btn.dataset.material || ''
                });
                btn.classList.add('fav');
                btn.style.transform = 'scale(1.4)';
                setTimeout(function () { btn.style.transform = ''; }, 200);
            }
            this.saveToStorage();
            this.updateBadges();
        },

        removeFromWishlist: function (id) {
            id = parseInt(id);
            this.wishlist = this.wishlist.filter(function (i) { return i.id !== id; });
            this.saveToStorage();
            this.updateBadges();
            this._refreshWishlist();
            var btn = document.querySelector('[data-id="' + id + '"][onclick*="toggleWishlist"]');
            if (btn) btn.classList.remove('fav');
        },

        isInWishlist: function (id) {
            id = parseInt(id);
            return this.wishlist.some(function (i) { return i.id === id; });
        },

        getWishlistCount: function () {
            return this.wishlist.length;
        },

        addFromWishlist: function (id) {
            id = parseInt(id);
            for (var i = 0; i < this.wishlist.length; i++) {
                if (this.wishlist[i].id === id) {
                    this.addToCart({
                        id: this.wishlist[i].id,
                        nombre: this.wishlist[i].nombre,
                        imagen: this.wishlist[i].imagen,
                        precio: this.wishlist[i].precio,
                        categoria: this.wishlist[i].categoria,
                        material: this.wishlist[i].material
                    });
                    return;
                }
            }
        },

        // ── Drawers ───────────────────────────────────────────────
        openCartDrawer: function () {
            this._refreshCart();
            var panel = ge('cartPanel'), overlay = ge('cartOverlay');
            if (panel) panel.classList.add('open');
            if (overlay) overlay.classList.add('open');
            document.body.style.overflow = 'hidden';
        },

        closeCartDrawer: function () {
            var panel = ge('cartPanel'), overlay = ge('cartOverlay');
            if (panel) panel.classList.remove('open');
            if (overlay) overlay.classList.remove('open');
            document.body.style.overflow = '';
        },

        openWishlistDrawer: function () {
            this._refreshWishlist();
            var panel = ge('favPanel'), overlay = ge('favOverlay');
            if (panel) panel.classList.add('open');
            if (overlay) overlay.classList.add('open');
            document.body.style.overflow = 'hidden';
        },

        closeWishlistDrawer: function () {
            var panel = ge('favPanel'), overlay = ge('favOverlay');
            if (panel) panel.classList.remove('open');
            if (overlay) overlay.classList.remove('open');
            document.body.style.overflow = '';
        },

        _refreshCart: function () {
            var body = ge('cartItems');
            var totalEl = ge('cartTotal');
            if (body) body.innerHTML = buildCartItems(this.cart);
            if (totalEl) totalEl.textContent = fmt(this.getCartTotal());
        },

        _refreshWishlist: function () {
            var body = ge('favItems');
            if (body) body.innerHTML = buildWishItems(this.wishlist);
        },

        // ── Badges ────────────────────────────────────────────────
        updateBadges: function () {
            var cartCount = this.getCartCount();
            var wishCount = this.getWishlistCount();

            var cc = ge('cartCount');
            if (cc) {
                cc.textContent = cartCount;
                cc.style.display = cartCount > 0 ? 'flex' : 'none';
            }
            var fc = ge('favCount');
            if (fc) {
                fc.textContent = wishCount;
                fc.style.display = wishCount > 0 ? 'flex' : 'none';
            }
        },

        // ── Persistence ───────────────────────────────────────────
        saveToStorage: function () {
            try {
                localStorage.setItem('mla_cart', JSON.stringify(this.cart));
                localStorage.setItem('mla_wishlist', JSON.stringify(this.wishlist));
            } catch (e) { /* quota exceeded or private browsing */ }
        },

        loadFromStorage: function () {
            try {
                var c = localStorage.getItem('mla_cart');
                var w = localStorage.getItem('mla_wishlist');
                this.cart = c ? JSON.parse(c) : [];
                this.wishlist = w ? JSON.parse(w) : [];
                // validate structure
                if (!Array.isArray(this.cart)) this.cart = [];
                if (!Array.isArray(this.wishlist)) this.wishlist = [];
            } catch (e) {
                this.cart = [];
                this.wishlist = [];
            }
        },

        // ── Init (called on DOMContentLoaded) ─────────────────────
        init: function () {
            this.loadFromStorage();
            this.updateBadges();

            // Sync heart button state with wishlist (works for btn-heart and btn-heart-o)
            var self = this;
            document.querySelectorAll('[onclick*="toggleWishlist"][data-id]').forEach(function (btn) {
                if (self.isInWishlist(parseInt(btn.dataset.id))) {
                    btn.classList.add('fav');
                }
            });

            // Close cart drawer when clicking overlay
            var cartOverlay = ge('cartOverlay');
            if (cartOverlay) {
                cartOverlay.addEventListener('click', function () { self.closeCartDrawer(); });
            }
            // Close wishlist drawer when clicking overlay
            var favOverlay = ge('favOverlay');
            if (favOverlay) {
                favOverlay.addEventListener('click', function () { self.closeWishlistDrawer(); });
            }
        }
    };

    document.addEventListener('DOMContentLoaded', function () {
        MLA.init();
    });

})();
