Imports Muebleria.Entidades
Imports Muebleria.Entidades.Muebleria.Entidades

Public Class MockProductos

    Private Const IMG As String = "~/Resources/Productos/"

    Public Shared Function ObtenerTodos() As List(Of CE_Producto)
        Return New List(Of CE_Producto) From {
            New CE_Producto With {
                .ProductoId = 1, .Nombre = "Sof" & Chr(225) & " Seccional Moderno", .TipoMuebleNombre = "Sala",
                .Precio = 4500D, .Stock = 5,
                .Descripcion = "Sof" & Chr(225) & " seccional en tela premium, dise" & Chr(241) & "o contempor" & Chr(225) & "neo.",
                .FotoUrl = IMG & "image1.jpeg", .EsMasVendido = True, .Activo = "S", .Descuento = 15
            },
            New CE_Producto With {
                .ProductoId = 2, .Nombre = "Silla Ergon" & Chr(243) & "mica Safari", .TipoMuebleNombre = "Sala",
                .Precio = 1750D, .Stock = 10,
                .Descripcion = "Silla de dise" & Chr(241) & "o ergon" & Chr(243) & "mico con respaldo ajustable.",
                .FotoUrl = IMG & "image2.png", .EsMasVendido = True, .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 3, .Nombre = "Juego Comedor Ergon" & Chr(243) & "mico", .TipoMuebleNombre = "Comedor",
                .Precio = 6500D, .Stock = 3,
                .Descripcion = "Juego de comedor de madera maciza para 6 personas.",
                .FotoUrl = IMG & "image3.jpeg", .EsMasVendido = True, .Activo = "S", .Descuento = 10
            },
            New CE_Producto With {
                .ProductoId = 4, .Nombre = "Mesa de Noche con Gavetas", .TipoMuebleNombre = "Habitaciones",
                .Precio = 850D, .Stock = 8,
                .Descripcion = "Mesa de noche con dos gavetas y acabado nogal.",
                .FotoUrl = IMG & "image4.jpeg", .EsMasVendido = True, .Activo = "S", .Descuento = 25
            },
            New CE_Producto With {
                .ProductoId = 5, .Nombre = "Ba" & Chr(250) & "l de Pie de Cama", .TipoMuebleNombre = "Habitaciones",
                .Precio = 1100D, .Stock = 6,
                .Descripcion = "Ba" & Chr(250) & "l tapizado para pie de cama, con espacio de almacenamiento.",
                .FotoUrl = IMG & "image5.png", .EsMasVendido = True, .Activo = "S", .Descuento = 30
            },
            New CE_Producto With {
                .ProductoId = 6, .Nombre = "Hamaca Tejida con Soporte", .TipoMuebleNombre = "Jard" & Chr(237) & "n",
                .Precio = 1600D, .Stock = 4,
                .Descripcion = "Hamaca artesanal tejida con soporte de aluminio resistente.",
                .FotoUrl = IMG & "image6.jpeg", .EsNuevo = True, .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 7, .Nombre = "Estanter" & Chr(237) & "a y Mueble TV", .TipoMuebleNombre = "Sala",
                .Precio = 1200D, .Stock = 7,
                .Descripcion = "Mueble para TV con estantes laterales, moderno y funcional.",
                .FotoUrl = IMG & "image7.png", .EsNuevo = True, .Activo = "S", .Descuento = 10
            },
            New CE_Producto With {
                .ProductoId = 8, .Nombre = "Repisa Flotante para Libros", .TipoMuebleNombre = "Sala",
                .Precio = 325D, .Stock = 15,
                .Descripcion = "Repisa de madera flotante ideal para libros y decoraci" & Chr(243) & "n.",
                .FotoUrl = IMG & "image8.jpeg", .Activo = "S", .Descuento = 15
            },
            New CE_Producto With {
                .ProductoId = 9, .Nombre = "L" & Chr(225) & "mpara Interior LED", .TipoMuebleNombre = "Iluminaci" & Chr(243) & "n",
                .Precio = 375D, .Stock = 20,
                .Descripcion = "L" & Chr(225) & "mpara de interior LED de bajo consumo y larga duraci" & Chr(243) & "n.",
                .FotoUrl = IMG & "image9.png", .EsNuevo = True, .Activo = "S", .Descuento = 50
            },
            New CE_Producto With {
                .ProductoId = 10, .Nombre = "Aplique de Pared Exterior", .TipoMuebleNombre = "Iluminaci" & Chr(243) & "n",
                .Precio = 425D, .Stock = 12,
                .Descripcion = "Aplique de pared para exterior, resistente a la intemperie.",
                .FotoUrl = IMG & "image10.jpeg", .EsNuevo = True, .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 11, .Nombre = "Foco LED Proyector", .TipoMuebleNombre = "Iluminaci" & Chr(243) & "n",
                .Precio = 280D, .Stock = 30,
                .Descripcion = "Foco proyector LED de alto brillo para ambientes amplios.",
                .FotoUrl = IMG & "image11.png", .Activo = "S", .Descuento = 30
            },
            New CE_Producto With {
                .ProductoId = 12, .Nombre = "L" & Chr(225) & "mpara de Noche Taylor", .TipoMuebleNombre = "Iluminaci" & Chr(243) & "n",
                .Precio = 350D, .Stock = 18,
                .Descripcion = "L" & Chr(225) & "mpara de mesa elegante para mesa de noche, estilo moderno.",
                .FotoUrl = IMG & "image12.jpeg", .Activo = "S", .Descuento = 15
            },
            New CE_Producto With {
                .ProductoId = 13, .Nombre = "L" & Chr(225) & "mpara de Techo de Lujo", .TipoMuebleNombre = "Iluminaci" & Chr(243) & "n",
                .Precio = 4500D, .Stock = 2,
                .Descripcion = "L" & Chr(225) & "mpara de techo de cristal, dise" & Chr(241) & "o exclusivo para ambientes de lujo.",
                .FotoUrl = IMG & "image13.png", .Activo = "S", .Descuento = 25
            },
            New CE_Producto With {
                .ProductoId = 14, .Nombre = "L" & Chr(225) & "mpara de Jard" & Chr(237) & "n", .TipoMuebleNombre = "Jard" & Chr(237) & "n",
                .Precio = 450D, .Stock = 9,
                .Descripcion = "L" & Chr(225) & "mpara solar de jard" & Chr(237) & "n, resistente al agua y UV.",
                .FotoUrl = IMG & "image14.jpeg", .EsNuevo = True, .Activo = "S", .Descuento = 10
            },
            New CE_Producto With {
                .ProductoId = 15, .Nombre = "Plaf" & Chr(243) & "n de Techo Minimalista", .TipoMuebleNombre = "Oficina",
                .Precio = 650D, .Stock = 11,
                .Descripcion = "Plaf" & Chr(243) & "n LED minimalista para oficinas, luz blanca neutra.",
                .FotoUrl = IMG & "image15.png", .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 16, .Nombre = "Sof" & Chr(225) & " de Cuero Cl" & Chr(225) & "sico", .TipoMuebleNombre = "Sala",
                .Precio = 5200D, .Stock = 3,
                .Descripcion = "Sof" & Chr(225) & " de cuero genuino, estilo cl" & Chr(225) & "sico con patas en madera.",
                .FotoUrl = IMG & "image16.jpeg", .Activo = "S", .Descuento = 30
            },
            New CE_Producto With {
                .ProductoId = 17, .Nombre = "Mesa de Centro Redonda", .TipoMuebleNombre = "Sala",
                .Precio = 950D, .Stock = 7,
                .Descripcion = "Mesa de centro redonda en madera y vidrio templado.",
                .FotoUrl = IMG & "image17.png", .Activo = "S", .Descuento = 15
            },
            New CE_Producto With {
                .ProductoId = 18, .Nombre = "Cama King Size de Lujo", .TipoMuebleNombre = "Habitaciones",
                .Precio = 7800D, .Stock = 2,
                .Descripcion = "Cama king size con cabecero tapizado y base de madera s" & Chr(243) & "lida.",
                .FotoUrl = IMG & "image18.jpeg", .EsMasVendido = True, .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 19, .Nombre = "Armario Ropero 4 Puertas", .TipoMuebleNombre = "Habitaciones",
                .Precio = 4200D, .Stock = 4,
                .Descripcion = "Armario ropero de 4 puertas con espejo, dise" & Chr(241) & "o moderno.",
                .FotoUrl = IMG & "image19.png", .Activo = "S", .Descuento = 25
            },
            New CE_Producto With {
                .ProductoId = 20, .Nombre = "Escritorio Ejecutivo Premium", .TipoMuebleNombre = "Oficina",
                .Precio = 2800D, .Stock = 5,
                .Descripcion = "Escritorio ejecutivo en madera y metal, amplio espacio de trabajo.",
                .FotoUrl = IMG & "image20.jpeg", .EsMasVendido = True, .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 21, .Nombre = "Silla de Oficina Ergon" & Chr(243) & "mica Pro", .TipoMuebleNombre = "Oficina",
                .Precio = 1850D, .Stock = 8,
                .Descripcion = "Silla ergon" & Chr(243) & "mica con soporte lumbar y reposabrazos ajustables.",
                .FotoUrl = IMG & "image21.png", .EsNuevo = True, .Activo = "S", .Descuento = 15
            },
            New CE_Producto With {
                .ProductoId = 22, .Nombre = "Set Exterior Rat" & Chr(225) & "n 4 piezas", .TipoMuebleNombre = "Jard" & Chr(237) & "n",
                .Precio = 5400D, .Stock = 3,
                .Descripcion = "Set de jard" & Chr(237) & "n en rat" & Chr(225) & "n sint" & Chr(233) & "tico, incluye sof" & Chr(225) & ", 2 sillas y mesa.",
                .FotoUrl = IMG & "image22.jpeg", .EsMasVendido = True, .Activo = "S", .Descuento = 30
            },
            New CE_Producto With {
                .ProductoId = 23, .Nombre = "Mesa de Comedor Extensible", .TipoMuebleNombre = "Comedor",
                .Precio = 3600D, .Stock = 4,
                .Descripcion = "Mesa de comedor extensible para 6-10 personas, madera de roble.",
                .FotoUrl = IMG & "image23.png", .Activo = "S", .Descuento = 10
            },
            New CE_Producto With {
                .ProductoId = 24, .Nombre = "Librero Modular Alto", .TipoMuebleNombre = "Oficina",
                .Precio = 1350D, .Stock = 6,
                .Descripcion = "Librero modular de 5 niveles, dise" & Chr(241) & "o escandinavo en roble.",
                .FotoUrl = IMG & "image24.jpeg", .EsNuevo = True, .Activo = "S", .Descuento = 20
            },
            New CE_Producto With {
                .ProductoId = 25, .Nombre = "Velador Flotante Minimalista", .TipoMuebleNombre = "Habitaciones",
                .Precio = 620D, .Stock = 10,
                .Descripcion = "Velador de pared flotante con cajones y cargador USB integrado.",
                .FotoUrl = IMG & "image25.png", .EsNuevo = True, .Activo = "S"
            }
        }
    End Function

    Public Shared Function ObtenerMasVendidos() As List(Of CE_Producto)
        Return ObtenerTodos().Where(Function(p) p.EsMasVendido).ToList()
    End Function

    Public Shared Function ObtenerNovedades() As List(Of CE_Producto)
        Return ObtenerTodos().Where(Function(p) p.EsNuevo).ToList()
    End Function

    Public Shared Function ObtenerOfertas() As List(Of CE_Producto)
        Return ObtenerTodos().Where(Function(p) p.Descuento.HasValue AndAlso p.Descuento.Value > 0).ToList()
    End Function

End Class
