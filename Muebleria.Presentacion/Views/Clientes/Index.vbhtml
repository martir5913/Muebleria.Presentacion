@Imports Muebleria.Entidades
@ModelType List(Of CE_Cliente)

@Code
    ViewData("Title") = "Gestión de Clientes"
End Code

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData("Title")</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        .hover-shadow:hover {
            transform: translateY(-5px);
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.2) !important;
        }

        .bg-gradient {
            background: linear-gradient(135deg, #0d6efd 0%, #0860ca 100%);
        }

        .card {
            border-radius: 0.75rem;
        }

        .card-header {
            border-radius: 0.75rem 0.75rem 0 0;
        }

        .container-wrapper {
            min-height: calc(100vh - 200px);
            background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
            padding-top: 2rem;
            padding-bottom: 2rem;
        }

        h1.display-4 {
            color: #2c3e50;
            font-weight: 700;
        }

        .btn-primary-custom {
            background: linear-gradient(135deg, #0d6efd 0%, #0860ca 100%);
            border: none;
            transition: all 0.3s ease;
        }

        .btn-primary-custom:hover {
            transform: translateY(-2px);
            box-shadow: 0 0.5rem 1rem rgba(13, 110, 253, 0.3);
        }
    </style>
</head>
<body>
    <div class="container-wrapper">
        <div class="container-fluid mt-5 mb-5">
            <div class="row">
                <div class="col-md-12">
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <h1 class="display-4">
                            <i class="fas fa-users"></i> Gestión de Clientes
                        </h1>
                        <a href="#" class="btn btn-primary btn-lg btn-primary-custom">
                            <i class="fas fa-plus"></i> Nuevo Cliente
                        </a>
                    </div>

                    @If Not String.IsNullOrEmpty(ViewBag.Error) Then
                        @<div class="alert alert-danger alert-dismissible fade show" role="alert">
                            <strong><i class="fas fa-exclamation-circle"></i> Error:</strong> @ViewBag.Error
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>
                    End If

                    @If Model Is Nothing OrElse Model.Count = 0 Then
                        @<div class="alert alert-info text-center py-5">
                            <i class="fas fa-inbox fa-3x mb-3"></i>
                            <p class="fs-5">No hay clientes registrados en el sistema.</p>
                        </div>
                    Else
                        @<div class="row g-4">
                            @For Each cliente In Model
                                @<div class="col-md-6 col-lg-4">
                                    <div class="card h-100 shadow-sm border-0 hover-shadow" style="transition: transform 0.3s, box-shadow 0.3s;">
                                        <div class="card-header bg-gradient text-white">
                                            <h5 class="card-title mb-0">
                                                <i class="fas fa-user-circle"></i> @cliente.Nombre
                                            </h5>
                                        </div>
                                        <div class="card-body">
                                            <div class="mb-3">
                                                <label class="form-label text-muted small mb-1">Correo Electrónico</label>
                                                <p class="mb-0">
                                                    <i class="fas fa-envelope text-secondary"></i>
                                                    <a href="mailto:@cliente.Correo">@cliente.Correo</a>
                                                </p>
                                            </div>
                                            <div class="mb-3">
                                                <label class="form-label text-muted small mb-1">Fecha de Creación</label>
                                                <p class="mb-0">
                                                    <i class="fas fa-calendar-alt text-secondary"></i>
                                                    @cliente.FechaCreacion.ToString("dd/MM/yyyy HH:mm")
                                                </p>
                                            </div>
                                            <div class="mb-0">
                                                <label class="form-label text-muted small mb-1">ID Cliente</label>
                                                <p class="mb-0">
                                                    <span class="badge bg-secondary">@cliente.Id</span>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="card-footer bg-light border-top">
                                            <div class="btn-group w-100" role="group">
                                                <a href="#" class="btn btn-sm btn-outline-primary">
                                                    <i class="fas fa-edit"></i> Editar
                                                </a>
                                                <a href="#" class="btn btn-sm btn-outline-danger">
                                                    <i class="fas fa-trash"></i> Eliminar
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            Next
                        </div>

                        @<div class="mt-5 text-center text-muted">
                            <small>
                                <i class="fas fa-info-circle"></i> Total de clientes: <strong>@Model.Count</strong>
                            </small>
                        </div>
                    End If
                </div>
            </div>
        </div>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>
