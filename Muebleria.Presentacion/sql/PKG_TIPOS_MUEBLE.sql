-- ================================================================================================
-- PAQUETE: PKG_TIPOS_MUEBLE
-- DESCRIPCIÓN: CRUD para la tabla MDA_TIPOS_MUEBLE (Tipos de Mueble: INTERIOR/EXTERIOR)
-- AUTOR: Equipo de Desarrollo
-- FECHA: 2024
-- VERSIÓN: 1.0
-- BASE DE DATOS: Oracle 21c
-- ================================================================================================

-- ================================================================================================
-- 1. ADICIONES A LA TABLA MDA_TIPOS_MUEBLE (si es necesario)
-- ================================================================================================
-- Nota: Si la tabla ya existe sin la columna UPDATED_AT, descomenta las siguientes líneas:
--
-- ALTER TABLE MDA_TIPOS_MUEBLE ADD UPDATED_AT TIMESTAMP;
-- ================================================================================================

-- ================================================================================================
-- 2. NOTA SOBRE TIMESTAMP
-- ================================================================================================
-- MDA_TIPOS_MUEBLE no define columna UPDATED_AT en scritpDB.md.
-- Por eso este paquete no crea trigger de auditoría sobre UPDATED_AT.

-- ================================================================================================
-- 3. ESPECIFICACIÓN DEL PAQUETE: PKG_TIPOS_MUEBLE
-- ================================================================================================
-- Funcionalidades:
--   - SP_CREAR_TIPO_MUEBLE: Crear un nuevo tipo de mueble
--   - SP_ACTUALIZAR_TIPO_MUEBLE: Actualizar datos de un tipo de mueble existente
--   - SP_ELIMINAR_TIPO_MUEBLE: Eliminar un tipo de mueble (con validaciones)
--   - SP_OBTENER_TIPO_MUEBLE: Obtener detalles de un tipo de mueble específico
--   - SP_LISTAR_TIPOS_MUEBLE: Listar todos los tipos de mueble
--   - SP_BUSCAR_TIPO_MUEBLE: Buscar tipos de mueble por criterios
--   - FN_EXISTE_TIPO_MUEBLE: Función de validación
-- ================================================================================================

CREATE OR REPLACE PACKAGE PKG_TIPOS_MUEBLE AS

  -- ==============================================================================================
  -- PROCEDIMIENTOS PRINCIPALES
  -- ==============================================================================================

  /**
   * SP_CREAR_TIPO_MUEBLE
   * Crea un nuevo registro de tipo de mueble
   *
   * @param p_codigo       IN VARCHAR2 - Código único (INTERIOR/EXTERIOR o personalizado)
   * @param p_nombre       IN VARCHAR2 - Nombre descriptivo del tipo
   * @param o_tipo_mueble_id OUT NUMBER - ID del tipo de mueble creado
   *
   * @throws ORA-20001 - Código ya existe
   * @throws ORA-20002 - Parámetros obligatorios nulos
   */
  PROCEDURE SP_CREAR_TIPO_MUEBLE(
    p_codigo        IN VARCHAR2,
    p_nombre        IN VARCHAR2,
    o_tipo_mueble_id OUT NUMBER
  );

  /**
   * SP_ACTUALIZAR_TIPO_MUEBLE
   * Actualiza los datos de un tipo de mueble existente
   *
   * @param p_tipo_mueble_id IN NUMBER - ID del tipo de mueble a actualizar
   * @param p_codigo         IN VARCHAR2 - Nuevo código (opcional)
   * @param p_nombre         IN VARCHAR2 - Nuevo nombre (opcional)
   *
   * @throws ORA-20003 - Tipo de mueble no existe
   * @throws ORA-20004 - Código ya existe
   */
  PROCEDURE SP_ACTUALIZAR_TIPO_MUEBLE(
    p_tipo_mueble_id IN NUMBER,
    p_codigo         IN VARCHAR2,
    p_nombre         IN VARCHAR2
  );

  /**
   * SP_ELIMINAR_TIPO_MUEBLE
   * Elimina un tipo de mueble (con validaciones)
   *
   * @param p_tipo_mueble_id IN NUMBER - ID del tipo de mueble a eliminar
   *
   * @throws ORA-20003 - Tipo de mueble no existe
   * @throws ORA-20005 - No se puede eliminar: productos existentes con este tipo
   */
  PROCEDURE SP_ELIMINAR_TIPO_MUEBLE(
    p_tipo_mueble_id IN NUMBER
  );

  /**
   * SP_OBTENER_TIPO_MUEBLE
   * Obtiene los detalles de un tipo de mueble específico
   *
   * @param p_tipo_mueble_id IN NUMBER - ID del tipo de mueble
   * @param o_rc            OUT SYS_REFCURSOR - Cursor con los datos
   */
  PROCEDURE SP_OBTENER_TIPO_MUEBLE(
    p_tipo_mueble_id IN NUMBER,
    o_rc             OUT SYS_REFCURSOR
  );

  /**
   * SP_LISTAR_TIPOS_MUEBLE
   * Lista todos los tipos de mueble registrados en el sistema
   *
   * @param o_rc OUT SYS_REFCURSOR - Cursor con lista de tipos de mueble
   */
  PROCEDURE SP_LISTAR_TIPOS_MUEBLE(
    o_rc OUT SYS_REFCURSOR
  );

  /**
   * SP_BUSCAR_TIPO_MUEBLE
   * Busca tipos de mueble por criterios (código o nombre)
   *
   * @param p_criterio IN VARCHAR2 - Texto a buscar (código o nombre)
   * @param o_rc      OUT SYS_REFCURSOR - Cursor con resultados
   */
  PROCEDURE SP_BUSCAR_TIPO_MUEBLE(
    p_criterio IN VARCHAR2,
    o_rc       OUT SYS_REFCURSOR
  );

  -- ==============================================================================================
  -- FUNCIONES AUXILIARES
  -- ==============================================================================================

  /**
   * FN_EXISTE_TIPO_MUEBLE
   * Verifica si existe un tipo de mueble por ID
   *
   * @param p_tipo_mueble_id IN NUMBER - ID a verificar
   * @return BOOLEAN - TRUE si existe, FALSE en caso contrario
   */
  FUNCTION FN_EXISTE_TIPO_MUEBLE(p_tipo_mueble_id IN NUMBER) RETURN BOOLEAN;

  /**
   * FN_CODIGO_EXISTE
   * Verifica si existe un código en el sistema
   *
   * @param p_codigo         IN VARCHAR2 - Código a verificar
   * @param p_tipo_mueble_id IN NUMBER - ID para exclusión (opcional, para updates)
   * @return BOOLEAN - TRUE si existe, FALSE en caso contrario
   */
  FUNCTION FN_CODIGO_EXISTE(
    p_codigo         IN VARCHAR2,
    p_tipo_mueble_id IN NUMBER DEFAULT NULL
  ) RETURN BOOLEAN;

END PKG_TIPOS_MUEBLE;
/

-- ================================================================================================
-- 4. CUERPO DEL PAQUETE: PKG_TIPOS_MUEBLE
-- ================================================================================================

CREATE OR REPLACE PACKAGE BODY PKG_TIPOS_MUEBLE AS

  -- ==============================================================================================
  -- CÓDIGOS DE ERROR (consistentes con PKG_ERRORS)
  -- ==============================================================================================
  c_err_codigo_duplicado    CONSTANT NUMBER := -20010;
  c_err_tipo_no_existe      CONSTANT NUMBER := -20011;
  c_err_tipo_con_productos  CONSTANT NUMBER := -20012;
  c_err_param_obligatorio   CONSTANT NUMBER := -20020;

  -- ==============================================================================================
  -- IMPLEMENTACIÓN: FUNCIONES AUXILIARES
  -- ==============================================================================================

  FUNCTION FN_EXISTE_TIPO_MUEBLE(p_tipo_mueble_id IN NUMBER) RETURN BOOLEAN IS
    v_count NUMBER;
  BEGIN
    IF p_tipo_mueble_id IS NULL THEN
      RETURN FALSE;
    END IF;

    SELECT COUNT(1) INTO v_count
      FROM MDA_TIPOS_MUEBLE
     WHERE TIPO_MUEBLE_ID = p_tipo_mueble_id;

    RETURN v_count > 0;
  END FN_EXISTE_TIPO_MUEBLE;

  FUNCTION FN_CODIGO_EXISTE(
    p_codigo         IN VARCHAR2,
    p_tipo_mueble_id IN NUMBER DEFAULT NULL
  ) RETURN BOOLEAN IS
    v_count NUMBER;
  BEGIN
    IF p_codigo IS NULL THEN
      RETURN FALSE;
    END IF;

    SELECT COUNT(1) INTO v_count
      FROM MDA_TIPOS_MUEBLE
     WHERE UPPER(CODIGO) = UPPER(p_codigo)
       AND (p_tipo_mueble_id IS NULL OR TIPO_MUEBLE_ID <> p_tipo_mueble_id);

    RETURN v_count > 0;
  END FN_CODIGO_EXISTE;

  -- ==============================================================================================
  -- IMPLEMENTACIÓN: PROCEDIMIENTOS
  -- ==============================================================================================

  PROCEDURE SP_CREAR_TIPO_MUEBLE(
    p_codigo        IN VARCHAR2,
    p_nombre        IN VARCHAR2,
    o_tipo_mueble_id OUT NUMBER
  ) IS
  BEGIN
    -- Validación: parámetros obligatorios
    IF p_codigo IS NULL OR p_nombre IS NULL THEN
      RAISE_APPLICATION_ERROR(
        c_err_param_obligatorio,
        'Código y Nombre son campos obligatorios.'
      );
    END IF;

    -- Validación: trimming de espacios
    IF TRIM(p_codigo) IS NULL OR TRIM(p_nombre) IS NULL THEN
      RAISE_APPLICATION_ERROR(
        c_err_param_obligatorio,
        'Código y Nombre no pueden ser solo espacios en blanco.'
      );
    END IF;

    -- Validación: código no duplicado
    IF FN_CODIGO_EXISTE(p_codigo) THEN
      RAISE_APPLICATION_ERROR(
        c_err_codigo_duplicado,
        'El código "'||p_codigo||'" ya existe en el sistema.'
      );
    END IF;

    -- Insertar nuevo tipo de mueble
    INSERT INTO MDA_TIPOS_MUEBLE (CODIGO, NOMBRE, CREATED_AT, UPDATED_AT)
    VALUES (UPPER(TRIM(p_codigo)), TRIM(p_nombre), SYSTIMESTAMP, SYSTIMESTAMP)
    RETURNING TIPO_MUEBLE_ID INTO o_tipo_mueble_id;

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      RAISE_APPLICATION_ERROR(
        c_err_codigo_duplicado,
        'El código ya existe. Por favor, intente con otro.'
      );
  END SP_CREAR_TIPO_MUEBLE;

  PROCEDURE SP_ACTUALIZAR_TIPO_MUEBLE(
    p_tipo_mueble_id IN NUMBER,
    p_codigo         IN VARCHAR2,
    p_nombre         IN VARCHAR2
  ) IS
    v_existe BOOLEAN;
  BEGIN
    -- Validación: tipo de mueble debe existir
    v_existe := FN_EXISTE_TIPO_MUEBLE(p_tipo_mueble_id);
    IF NOT v_existe THEN
      RAISE_APPLICATION_ERROR(
        c_err_tipo_no_existe,
        'Tipo de mueble con ID '||p_tipo_mueble_id||' no existe.'
      );
    END IF;

    -- Validación: si se proporciona código, verificar que no sea duplicado
    IF p_codigo IS NOT NULL AND TRIM(p_codigo) IS NOT NULL THEN
      IF FN_CODIGO_EXISTE(TRIM(p_codigo), p_tipo_mueble_id) THEN
        RAISE_APPLICATION_ERROR(
          c_err_codigo_duplicado,
          'El código "'||p_codigo||'" ya existe en el sistema.'
        );
      END IF;
    END IF;

    -- Actualizar los campos proporcionados
    UPDATE MDA_TIPOS_MUEBLE
       SET CODIGO = NVL(UPPER(TRIM(p_codigo)), CODIGO),
           NOMBRE = NVL(TRIM(p_nombre), NOMBRE),
           UPDATED_AT = SYSTIMESTAMP
     WHERE TIPO_MUEBLE_ID = p_tipo_mueble_id;

    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(
        c_err_tipo_no_existe,
        'No se pudo actualizar el tipo de mueble. Intente de nuevo.'
      );
    END IF;

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      RAISE_APPLICATION_ERROR(
        c_err_codigo_duplicado,
        'El código ya existe. Por favor, intente con otro.'
      );
  END SP_ACTUALIZAR_TIPO_MUEBLE;

  PROCEDURE SP_ELIMINAR_TIPO_MUEBLE(
    p_tipo_mueble_id IN NUMBER
  ) IS
    v_existe BOOLEAN;
    v_prod_count NUMBER;
  BEGIN
    -- Validación: tipo de mueble debe existir
    v_existe := FN_EXISTE_TIPO_MUEBLE(p_tipo_mueble_id);
    IF NOT v_existe THEN
      RAISE_APPLICATION_ERROR(
        c_err_tipo_no_existe,
        'Tipo de mueble con ID '||p_tipo_mueble_id||' no existe.'
      );
    END IF;

    -- Validación: no debe existir productos asociados
    -- Esta es una regla de negocio importante para mantener integridad
    SELECT COUNT(1) INTO v_prod_count
      FROM MDA_PRODUCTOS
     WHERE TIPO_MUEBLE_ID = p_tipo_mueble_id;

    IF v_prod_count > 0 THEN
      RAISE_APPLICATION_ERROR(
        c_err_tipo_con_productos,
        'No se puede eliminar: existen '||v_prod_count||' producto(s) '||
        'asociado(s) a este tipo de mueble. '||
        'Elimine o reasigne los productos primero.'
      );
    END IF;

    -- Eliminar el tipo de mueble
    DELETE FROM MDA_TIPOS_MUEBLE
     WHERE TIPO_MUEBLE_ID = p_tipo_mueble_id;

    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(
        c_err_tipo_no_existe,
        'No se pudo eliminar el tipo de mueble. Intente de nuevo.'
      );
    END IF;

  END SP_ELIMINAR_TIPO_MUEBLE;

  PROCEDURE SP_OBTENER_TIPO_MUEBLE(
    p_tipo_mueble_id IN NUMBER,
    o_rc             OUT SYS_REFCURSOR
  ) IS
  BEGIN
    OPEN o_rc FOR
      SELECT TIPO_MUEBLE_ID,
             CODIGO,
             NOMBRE,
             CREATED_AT,
             UPDATED_AT,
             (SELECT COUNT(1) FROM MDA_PRODUCTOS WHERE TIPO_MUEBLE_ID = t.TIPO_MUEBLE_ID) AS PRODUCTOS_ASOCIADOS
        FROM MDA_TIPOS_MUEBLE t
       WHERE TIPO_MUEBLE_ID = p_tipo_mueble_id;
  END SP_OBTENER_TIPO_MUEBLE;

  PROCEDURE SP_LISTAR_TIPOS_MUEBLE(
    o_rc OUT SYS_REFCURSOR
  ) IS
  BEGIN
    OPEN o_rc FOR
      SELECT TIPO_MUEBLE_ID,
             CODIGO,
             NOMBRE,
             CREATED_AT,
             UPDATED_AT,
             (SELECT COUNT(1) FROM MDA_PRODUCTOS WHERE TIPO_MUEBLE_ID = t.TIPO_MUEBLE_ID) AS PRODUCTOS_ASOCIADOS
        FROM MDA_TIPOS_MUEBLE t
       ORDER BY CODIGO ASC;
  END SP_LISTAR_TIPOS_MUEBLE;

  PROCEDURE SP_BUSCAR_TIPO_MUEBLE(
    p_criterio IN VARCHAR2,
    o_rc       OUT SYS_REFCURSOR
  ) IS
  BEGIN
    OPEN o_rc FOR
      SELECT TIPO_MUEBLE_ID,
             CODIGO,
             NOMBRE,
             CREATED_AT,
             UPDATED_AT,
             (SELECT COUNT(1) FROM MDA_PRODUCTOS WHERE TIPO_MUEBLE_ID = t.TIPO_MUEBLE_ID) AS PRODUCTOS_ASOCIADOS
        FROM MDA_TIPOS_MUEBLE t
       WHERE p_criterio IS NULL
          OR UPPER(CODIGO) LIKE '%'||UPPER(p_criterio)||'%'
          OR UPPER(NOMBRE) LIKE '%'||UPPER(p_criterio)||'%'
       ORDER BY CODIGO ASC;
  END SP_BUSCAR_TIPO_MUEBLE;

END PKG_TIPOS_MUEBLE;
/

-- ================================================================================================
-- 5. ÍNDICES PARA OPTIMIZACIÓN
-- ================================================================================================

CREATE INDEX IX_TIPOS_MUEBLE_CODIGO ON MDA_TIPOS_MUEBLE (UPPER(CODIGO));
CREATE INDEX IX_TIPOS_MUEBLE_NOMBRE ON MDA_TIPOS_MUEBLE (UPPER(NOMBRE));
CREATE INDEX IX_TIPOS_MUEBLE_CREATED ON MDA_TIPOS_MUEBLE (CREATED_AT);

-- ================================================================================================
-- 6. DATOS DE PRUEBA (OPCIONAL - descomenta para cargar datos iniciales)
-- ================================================================================================
--
-- INSERT INTO MDA_TIPOS_MUEBLE (CODIGO, NOMBRE, CREATED_AT, UPDATED_AT)
-- SELECT 'INTERIOR', 'Mueble de interior', SYSTIMESTAMP, SYSTIMESTAMP FROM DUAL
-- WHERE NOT EXISTS (SELECT 1 FROM MDA_TIPOS_MUEBLE WHERE CODIGO = 'INTERIOR');
--
-- INSERT INTO MDA_TIPOS_MUEBLE (CODIGO, NOMBRE, CREATED_AT, UPDATED_AT)
-- SELECT 'EXTERIOR', 'Mueble de exterior', SYSTIMESTAMP, SYSTIMESTAMP FROM DUAL
-- WHERE NOT EXISTS (SELECT 1 FROM MDA_TIPOS_MUEBLE WHERE CODIGO = 'EXTERIOR');
--
-- COMMIT;
-- ================================================================================================

-- ================================================================================================
-- 7. EJEMPLOS DE USO
-- ================================================================================================
--
-- -- Crear un nuevo tipo de mueble
-- DECLARE
--   v_id NUMBER;
-- BEGIN
--   PKG_TIPOS_MUEBLE.SP_CREAR_TIPO_MUEBLE('JUVENIL', 'Muebles para habitación juvenil', v_id);
--   DBMS_OUTPUT.PUT_LINE('Tipo de mueble creado con ID: '||v_id);
--   COMMIT;
-- EXCEPTION
--   WHEN OTHERS THEN
--     DBMS_OUTPUT.PUT_LINE('Error: '||SQLERRM);
--     ROLLBACK;
-- END;
-- /
--
-- -- Listar todos los tipos de mueble
-- DECLARE
--   v_rc SYS_REFCURSOR;
--   v_id NUMBER;
--   v_codigo VARCHAR2(20);
--   v_nombre VARCHAR2(50);
-- BEGIN
--   PKG_TIPOS_MUEBLE.SP_LISTAR_TIPOS_MUEBLE(v_rc);
--   LOOP
--     FETCH v_rc INTO v_id, v_codigo, v_nombre;
--     EXIT WHEN v_rc%NOTFOUND;
--     DBMS_OUTPUT.PUT_LINE(v_id||' - '||v_codigo||' - '||v_nombre);
--   END LOOP;
--   CLOSE v_rc;
-- END;
-- /
--
-- -- Actualizar un tipo de mueble
-- BEGIN
--   PKG_TIPOS_MUEBLE.SP_ACTUALIZAR_TIPO_MUEBLE(1, 'INT', 'Muebles para interiores');
--   DBMS_OUTPUT.PUT_LINE('Tipo de mueble actualizado exitosamente');
--   COMMIT;
-- EXCEPTION
--   WHEN OTHERS THEN
--     DBMS_OUTPUT.PUT_LINE('Error: '||SQLERRM);
--     ROLLBACK;
-- END;
-- /
--
-- -- Buscar tipos de mueble
-- DECLARE
--   v_rc SYS_REFCURSOR;
--   v_id NUMBER;
--   v_codigo VARCHAR2(20);
--   v_nombre VARCHAR2(50);
-- BEGIN
--   PKG_TIPOS_MUEBLE.SP_BUSCAR_TIPO_MUEBLE('INTERIOR', v_rc);
--   LOOP
--     FETCH v_rc INTO v_id, v_codigo, v_nombre;
--     EXIT WHEN v_rc%NOTFOUND;
--     DBMS_OUTPUT.PUT_LINE(v_id||' - '||v_codigo||' - '||v_nombre);
--   END LOOP;
--   CLOSE v_rc;
-- END;
-- /
--
-- -- Eliminar un tipo de mueble
-- BEGIN
--   PKG_TIPOS_MUEBLE.SP_ELIMINAR_TIPO_MUEBLE(1);
--   DBMS_OUTPUT.PUT_LINE('Tipo de mueble eliminado exitosamente');
--   COMMIT;
-- EXCEPTION
--   WHEN OTHERS THEN
--     DBMS_OUTPUT.PUT_LINE('Error: '||SQLERRM);
--     ROLLBACK;
-- END;
-- /
--
-- ================================================================================================
-- FIN DEL SCRIPT - PKG_TIPOS_MUEBLE
-- ================================================================================================
