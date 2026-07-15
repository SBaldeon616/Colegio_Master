USE master_colegio;

/* 1. ESTADO_CLIENTE */
INSERT IGNORE INTO estado_cliente (id, codigo, descripcion) VALUES
    (1, 'ACTIVO',     'Cliente activo y operativo'),
    (2, 'INACTIVO',   'Cliente desactivado temporalmente'),
    (3, 'SUSPENDIDO', 'Cliente suspendido por incumplimiento');

/* 2. ESTADO_SUSCRIPCION */
INSERT IGNORE INTO estado_suscripcion (id, codigo, descripcion) VALUES
    (1, 'ACTIVA',    'Suscripcion vigente'),
    (2, 'VENCIDA',   'Suscripcion con fecha fin superada'),
    (3, 'CANCELADA', 'Suscripcion cancelada antes de su vencimiento');

/* 3. PLAN */
INSERT IGNORE INTO `plan` (codigo, nombre, precio_mensual, precio_anual, max_estudiante, max_usuario, estado, usuario_creacion) VALUES
    ('BASICO',      'Plan Basico',      49.90,   499.90,  100,  5,    1, 'SISTEMA'),
    ('ESTANDAR',    'Plan Estandar',   99.90,   999.90,  500,  15,   1, 'SISTEMA'),
    ('PROFESIONAL', 'Plan Profesional', 199.90, 1999.90, NULL, NULL, 1, 'SISTEMA');

/* 4. USUARIO_PLATAFORMA */
INSERT IGNORE INTO usuario_plataforma
    (nombres, apellidos, correo, clave_cifrada, estado, usuario_creacion)
VALUES
    ('Franklin', 'Huaman', 'fhuaman', 'mGErFoUo22DlRlk0qEqG8g==', 1, 'SISTEMA');