﻿using CONTROLATEC.Data;
using CONTROLATEC.Modelos;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;


namespace CONTROLATEC
{
    public partial class App : Application
    {

        public static SQLiteHelper DBCuentas { get; set; }
        public App()
        {
            InitializeComponent();
            InitializedDatabase();

            MainPage = new NavigationPage(new Home());
        }

        private async void InitializedDatabase()
        {
            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbpath = System.IO.Path.Combine(folderApp, "DBCuentasV1.4.db3");
            DBCuentas = new SQLiteHelper(dbpath);

            var cuentas = await DBCuentas .db.Table<CUENTA>().ToListAsync();
            if(cuentas.Count == 0)
            {
                var egreso = new CUENTA();
                egreso.etiqueta = "EGRESO";
                await DBCuentas.db.InsertAsync(egreso);
                var ingreso = new CUENTA();
                ingreso.etiqueta = "INGRESO";
                await DBCuentas.db.InsertAsync(ingreso);
            }
            cuentas = await DBCuentas.db.Table<CUENTA>().ToListAsync();
            var conceptos = await DBCuentas.db.Table<CONCEPTO>().ToListAsync();
            if (conceptos.Count == 0)
            {
                string query = @"INSERT INTO CONCEPTO (id_cuenta ,tipo, etiqueta, estatus) 
                                VALUES
                                (1, 'Fijo', 'Gasto de vivienda', 'activo'),
                                (1, 'Fijo', 'Gastos de transporte', 'activo'),
                                (1, 'Fijo', 'Seguros', 'activo'),
                                (1, 'Fijo', 'Préstamos', 'activo'),
                                (1, 'Fijo', 'Suscripciones y membresías', 'activo'),
                                (1, 'Fijo', 'Educación', 'activo'),
                                (1, 'Fijo', 'Impuestos', 'activo'),
                                (1, 'Fijo', 'Mantenimiento', 'activo'), 
                                (1, 'Fijo', 'Cuotas', 'activo'),
                                (1, 'Fijo', 'Alimentación', 'activo'),
                                (1, 'Fijo', 'Alquiler de equipos', 'activo'),
                                (1, 'Variable', 'Costos de producción', 'activo'),
                                (1, 'Variable', 'Gastos de marketing', 'activo'),
                                (1, 'Variable', 'Gastos de ventas', 'activo'),
                                (1, 'Variable', 'Costos de envío y distribución', 'activo'),
                                (1, 'Variable', 'Gastos operativos', 'activo'),
                                (1, 'Variable', 'Gastos de viaje', 'activo'),
                                (1, 'Variable', 'Gastos por fechas festivas', 'activo'),
                                (1, 'Variable', 'Costos de mantenimiento', 'activo'),
                                (1, 'Variable', 'Gastos de servicios públicos', 'activo'),
                                (1, 'Inesperado', 'Gastos médicos no anticipados', 'activo'),
                                (1, 'Inesperado', 'Reparaciones importantes en el hogar o el automóvil', 'activo'),
                                (1, 'Inesperado', 'Pérdida de un cliente clave', 'activo'),
                                (1, 'Inesperado', 'Muerte o enfermedad grave de un ser querido', 'activo'),
                                (1, 'Inesperado', 'Caída repentina de un sistema en línea', 'activo'),
                                (1, 'Inesperado', 'Emisiones inesperadas de partículas o energía en un experimento', 'activo'),
                                (2, 'Fijo', 'Salario o Sueldo', 'activo'),
                                (2, 'Fijo', 'Pensión', 'activo'),
                                (2, 'Fijo', 'Renta de Propiedades', 'activo'),
                                (2, 'Fijo', 'Intereses de Inversiones', 'activo'),
                                (2, 'Fijo', 'Regalías', 'activo'),
                                (2, 'Fijo', 'Anualidades', 'activo'),
                                (2, 'Fijo', 'Ingresos por Jubilación', 'activo'),
                                (2, 'Fijo', 'Pagos de Seguro', 'activo'),
                                (2, 'Fijo', 'Compensación por Desempleo', 'activo'),
                                (2, 'Fijo', 'Ingresos de Distribuciones de Sociedades', 'activo'),
                                (2, 'Fijo', 'Pagos de Alimentos o Manutención', 'activo'),
                                (2, 'Fijo', 'Pagos de Rentas Vitalicias', 'activo'),
                                (2, 'Variable', 'Ingresos por ventas', 'activo'),
                                (2, 'Variable', 'Comisiones', 'activo'),
                                (2, 'Variable', 'Bonificaciones', 'activo'),
                                (2, 'Variable', 'Ingresos de inversiones', 'activo'),
                                (2, 'Variable', 'Trabajo independiente o freelance', 'activo'),
                                (2, 'Variable', 'Propinas', 'activo'),
                                (2, 'Variable', 'Ingresos por publicidad y marketing', 'activo'),
                                (2, 'Variable', 'Contratos a corto plazo', 'activo'),
                                (2, 'Esporadico', 'Participación en Estudios o Encuestas', 'activo'),
                                (2, 'Esporadico', 'Ingresos de Alquiler', 'activo'),
                                (2, 'Esporadico', 'Premios y Concursos', 'activo'),
                                (2, 'Esporadico', 'Ingresos de Hobbies', 'activo'),
                                (2, 'Esporadico', 'Reembolsos y Devoluciones', 'activo'),
                                (2, 'Esporadico', 'Trabajos Eventuales', 'activo'); ";
                await DBCuentas.db.ExecuteAsync(query);
                
            }
            conceptos = await DBCuentas.db.Table<CONCEPTO>().ToListAsync();


            var conceptos_detalle = await DBCuentas.db.Table<CONCEPTO_DETALLE>().ToListAsync();
            if (conceptos_detalle.Count == 0)
            {
                string query = @"INSERT INTO CONCEPTO_DETALLE (id_concepto, etiqueta, valor, estatus)
                                VALUES
                                (1, 'Alquiler', 1500, 'Activo'),
                                (1, 'Renta', 3000, 'Activo'), 
                                (1, 'Servicios Público de Agua', 300, 'Activo'),
                                (1, 'Servicios Público de Electricidad', 400, 'Activo'),
                                (1, 'Servicios Público de Gas', 300, 'Activo'),
                                (1, 'Internet y Cable', 300, 'Activo'),
                                (1, 'Mantenimiento y Reparaciones', 400, 'Activo'),
                                (1, 'Muebles y Decoración', 400, 'Activo'),
                                (2, 'Gasolina', 70, 'Activo'),
                                (2, 'Transporte Público', 50, 'Activo'),
                                (2, 'Pago de Préstamo de Automóvil', 20000, 'Activo'),
                                (2, 'Mantenimiento y Reparaciones del Automóvil', 500, 'Activo'),
                                (2, 'Peajes y Estacionamiento', 150, 'Activo'),
                                (2, 'Servicio de Taxi o Uber/Lyft', 30, 'Activo'),
                                (2, 'Pago de Licencia y Registro del Automóvil', 150, 'Activo'),
                                (2, 'Arrendamiento de Automóvil', 250, 'Activo'),
                                (2, 'Bicicleta o Otro Medio de Transporte', 1500, 'Activo'),
                                (3, 'Seguro de Automóvil', 1500, 'Activo'),
                                (3, 'Seguro de Vivienda (Hogar)', 1200, 'Activo'),
                                (3, 'Seguro de Vida', 30, 'Activo'),
                                (3, 'Seguro de Salud', 400, 'Activo'),
                                (3, 'Seguro de Viaje', 200, 'Activo'),
                                (3, 'Seguro de Dental', 200, 'Activo'),
                                (3, 'Seguro de Responsabilidad Civil', 1000, 'Activo'),
                                (3, 'Seguro de Mascotas', 40, 'Activo'),
                                (3, 'Seguro de Ingresos por Incapacidad', 30, 'Activo'),
                                (4, 'Préstamo de Automóvil', 20000, 'Activo'),
                                (4, 'Préstamo Hipotecario', 250000, 'Activo'),
                                (4, 'Préstamo Personal', 10000, 'Activo'),
                                (4, 'Préstamo para Estudiantes', 50000, 'Activo'),
                                (4, 'Préstamo Comercial', 50000, 'Activo'),
                                (5, 'Suscripción a Streaming de Música', 50, 'Activo'),
                                (5, 'Spotify', 150, 'Activo'),
                                (5, 'Membresía de Gimnasio', 100, 'Activo'),
                                (5, 'NetFlix', 150, 'Activo'),
                                (5, 'Disney+', 150, 'Activo'),
                                (5, 'Prime', 150, 'Activo'),
                                (5, 'HBOMAX', 150, 'Activo'),
                                (5, 'YouTube Premium', 150, 'Activo'),
                                (5, 'Membresía de Biblioteca', 10, 'Activo'),
                                (5, 'Suscripción a Revista Digital', 60, 'Activo'),
                                (5, 'Membresía de Cine', 150, 'Activo'),
                                (5, 'Suscripción a Revista Impresa', 5, 'Activo'),
                                (5, 'Membresía de Club Deportivo', 150, 'Activo'),
                                (5, 'Suscripción a Plataforma de Aprendizaje en Línea', 200, 'Activo'),
                                (5, 'Membresía de Club de Libros', 50, 'Activo'),
                                (5, 'Suscripción a Plataforma de Guardado en la nube', 100, 'Activo'),
                                (6, 'Colegiatura', 1500, 'Activo'),
                                (6, 'Cursos', 100, 'Activo'),
                                (6, 'Diplomado en Marketing Digital', 500, 'Activo'),
                                (6, 'Certificación en Diseño Gráfico', 300, 'Activo'),
                                (6, 'Máster en Administración de Empresas (MBA)', 2000, 'Activo'),
                                (6, 'Taller Presencial de Fotografía', 150, 'Activo'),
                                (6, 'Curso de Idiomas en el Extranjero', 1000, 'Activo'),
                                (6, 'Certificación en Finanzas Personales', 250, 'Activo'),
                                (6, 'Taller de Escritura Creativa', 80, 'Activo'),
                                (6, 'Curso de Desarrollo de Videojuegos', 400, 'Activo'),
                                (6, 'Certificación en Gestión de Proyectos', 600, 'Activo'),
                                (7, 'Impuesto sobre la Renta (ISR)', 5000, 'Activo'),
                                (7, 'Impuesto al Valor Agregado (IVA)', 3000, 'Activo'),
                                (7, 'Impuesto sobre Automóviles Nuevos (ISAN)', 1000, 'Activo'),
                                (7, 'Impuesto Empresarial a Tasa Única (IETU)', 8000, 'Activo'),
                                (7, 'Impuesto a los Depósitos en Efectivo (IDE)', 1500, 'Activo'),
                                (7, 'Impuesto Predial', 7000, 'Activo'),
                                (7, 'Impuesto sobre la Adquisición de Inmuebles', 500, 'Activo'),
                                (7, 'Impuesto sobre Espectáculos Públicos', 2000, 'Activo'),
                                (7, 'Impuesto sobre el Patrimonio', 4000, 'Activo'),
                                (7, 'Impuesto a la Herencia', 6000, 'Activo'),
                                (8, 'Mantenimiento Preventivo de HVAC', 150, 'Activo'),
                                (8, 'Mantenimiento Correctivo de Plomería', 200, 'Activo'),
                                (8, 'Mantenimiento Predictivo de Maquinaria', 300, 'Activo'),
                                (8, 'Mantenimiento de Edificios y Instalaciones', 500, 'Activo'),
                                (8, 'Mantenimiento de Jardines y Paisajismo', 100, 'Activo'),
                                (8, 'Mantenimiento de Equipos Informáticos', 120, 'Activo'),
                                (8, 'Mantenimiento de Ascensores', 180, 'Activo'),
                                (8, 'Mantenimiento de Sistemas Eléctricos', 250, 'Activo'),
                                (8, 'Mantenimiento de Redes de Agua Potable', 200, 'Activo'),
                                (8, 'Mantenimiento de Sistemas de Seguridad', 80, 'Activo'),
                                (9, 'Cuota de barrio', 1000, 'Activo'),
                                (9, 'Cuota Estudiantil', 1200, 'Activo'),
                                (9, 'Cuota de Tarjeta de crédito', 250, 'Activo'),
                                (9, 'Cuotas de la Asociación de Propietarios (HOA)', 400, 'Activo'),
                                (9, 'Cuota de membresía', 100, 'Activo'),
                                (9, 'Cuota Comercial', 500, 'Activo'),
                                (10, 'Facturas de Comestibles', 400, 'Activo'),
                                (10, 'Pago  de Restaurante', 150, 'Activo'),
                                (10, 'Servicio de Entrega de Comidas', 200, 'Activo'),
                                (10, 'Compra de Alimentos a Granel Trimestral', 600, 'Activo'),
                                (10, 'Pago Mensual de Servicio de Catering', 500, 'Activo'),
                                (10, 'Compra de Productos Orgánicos Semanales', 100, 'Activo'),
                                (10, 'Presupuesto Trimestral para Cenas en Restaurantes', 600, 'Activo'),
                                (11, 'Alquiler de Silla y Mesa para Evento', 50, 'Activo'),
                                (11, 'Alquiler de Proyector y Pantalla', 80, 'Activo'),
                                (11, 'Alquiler de Equipo de sonido', 100, 'Activo'),
                                (11, 'Alquiler de Herramientas Básicas', 30, 'Activo'),
                                (11, 'Alquiler de Generador Eléctrico Portátil', 70, 'Activo'),
                                (11, 'Alquiler de de Equipos electronicos', 25, 'Activo'),
                                (11, 'Alquiler para eventos y fisetas', 1000, 'Activo'),
                                (12, 'Costo de Materiales', 2000, 'Activo'),
                                (12, 'Costo de Materias Primas', 5000, 'Activo'),
                                (12, 'Costo de Mano de Obra', 3000, 'Activo'),
                                (12, 'Costo de Energía Eléctrica', 1000, 'Activo'),
                                (12, 'Costo de Maquinaria y Equipos', 8000, 'Activo'),
                                (12, 'Costo de Transporte de Productos', 1500, 'Activo'),
                                (12, 'Costo de Almacenamiento de Inventarios', 1200, 'Activo'),
                                (12, 'Costo de Investigación y Desarrollo', 1800, 'Activo'),
                                (12, 'Costo de Envases y Embalajes', 900, 'Activo'),
                                (12, 'Costo de Publicidad y Marketing', 2500, 'Activo'),
                                (12, 'Costo de Manutención de Maquinaria', 600, 'Activo'),
                                (13, 'Campaña de Publicidad en Redes Sociales', 2000, 'Activo'),
                                (13, 'Anuncios en Google Ads', 1500, 'Activo'),
                                (13, 'Marketing de Contenidos', 1200, 'Activo'),
                                (13, 'Email Marketing', 800, 'Activo'),
                                (13, 'SEO y Optimización de Sitio Web', 1000, 'Activo'),
                                (14, 'Suscripción a Plataforma de Automatización de Ventas', 200, 'Activo'),
                                (14, 'Membresía de Asociación Comercial', 100, 'Activo'),
                                (14, 'Software de Seguimiento de Clientes', 150, 'Activo'),
                                (14, 'Herramientas de Prospección de Clientes', 80, 'Activo'),
                                (14, 'Publicidad en Directorios Comerciales', 120, 'Activo'),
                                (14, 'Cursos de Capacitación en Ventas', 300, 'Activo'),
                                (14, 'Suscripción a Software de Email Marketing', 50, 'Activo'),
                                (14, 'Gastos de Viaje para Ferias Comerciales', 500, 'Activo'),
                                (14, 'Publicidad en Medios Especializados', 200, 'Activo'),
                                (14, 'Materiales de Promoción para Eventos de Ventas', 150, 'Activo'),
                                (15, 'Envío de Productos a Clientes', 300, 'Activo'),
                                (15, 'Tarifas de Almacenaje en Centro de Distribución', 200, 'Activo'),
                                (15, 'Gastos de Logística para Exportaciones', 500, 'Activo'),
                                (15, 'Suscripción a Servicio de Mensajería', 100, 'Activo'),
                                (15, 'Costo de Embalaje y Etiquetado', 50, 'Activo'),
                                (15, 'Fletes de Transporte de Carga Pesada', 800, 'Activo'),
                                (15, 'Tarifas de Correo Expreso', 120, 'Activo'),
                                (15, 'Mantenimiento de Flota de Transporte', 300, 'Activo'),
                                (15, 'Seguro de Carga', 150, 'Activo'),
                                (15, 'Costos de Importación y Aduanas', 400, 'Activo'),
                                (16, 'Gastos de Suministros de Oficina', 200, 'Activo'),
                                (16, 'Mantenimiento de Equipos de Oficina', 150, 'Activo'),
                                (16, 'Tarifas de Internet y Telefonía', 300, 'Activo'),
                                (16, 'Gastos de Limpieza y Mantenimiento', 250, 'Activo'),
                                (16, 'Suscripción a Software de Gestión Empresarial', 500, 'Activo'),
                                (17, 'Viaje de Capacitación en el Extranjero', 3000, 'Activo'),
                                (17, 'Viaje de Negocios a Conferencia Internacional', 2500, 'Activo'),
                                (17, 'Viaje para Reunión con Cliente en Otra Ciudad', 1200, 'Activo'),
                                (17, 'Capacitación en Línea sobre Viajes de Negocios', 500, 'Activo'),
                                (17, 'Gastos de Alojamiento y Comida en Viaje de Trabajo', 800, 'Activo'),
                                (18, 'Compra de regalos de Navidad para la familia', 500, 'Activo'),
                                (18, 'Decoraciones y árbol de Navidad' , 200, 'Activo'),
                                (18, 'Cena festiva y postres', 300, 'Activo'),
                                (18, 'Tarjetas de Navidad y papel de regalo', 50, 'Activo'),
                                (18, 'Disfraces y accesorios', 100, 'Activo'),
                                (19, 'Mantenimiento Preventivo de Maquinaria', 2000, 'Activo'),
                                (19, 'Desarrollo de Nuevas Características de Producto', 3000, 'Activo'),
                                (19, 'Actualización de Software de Gestión Empresarial', 1500, 'Activo'),
                                (19, 'Mantenimiento de Plataforma de Comercio Electrónico', 2500, 'Activo'),
                                (20, 'Capacitación de Empleados en Nuevas Tecnologías', 1200, 'Activo'),
                                (20, 'Mantenimiento de Infraestructura de la Oficina', 1000, 'Activo'),
                                (20, 'Desarrollo de Proyectos de Infraestructura', 2000, 'Activo'),
                                (21, 'Consulta Médica Especializada', 150, 'Activo'),
                                (21, 'Exámenes de Laboratorio', 200, 'Activo'),
                                (21, 'Medicamentos Recetados', 80, 'Activo'),
                                (21, 'Sesiones de Terapia Física', 250, 'Activo'),
                                (21, 'Desarrollo de Software Médico Personalizado', 5000, 'Activo'),
                                (21, 'Tratamiento de Fisioterapia', 300, 'Activo'),
                                (21, 'Análisis de Resonancia Magnética', 400, 'Activo'),
                                (21, 'Medicamentos de Recuperación Postoperatoria', 120, 'Activo'),
                                (21, 'Desarrollo de Dispositivo Médico Innovador', 7000, 'Activo'),
                                (21, 'Sesiones de Terapia Psicológica', 180, 'Activo'),
                                (22, 'Reparación del Techo de la Casa', 3000, 'Activo'),
                                (22, 'Cambio de Frenos y Discos en el Automóvil', 800, 'Activo'),
                                (22, 'Reparación del Sistema Eléctrico en Casa', 1500, 'Activo'),
                                (22, 'Arreglo del Sistema de Climatización', 1000, 'Activo'),
                                (22, 'Reparación de Cañerías en la Casa', 1200, 'Activo'),
                                (22, 'Reemplazo de Ventanas en la Casa', 2000, 'Activo'),
                                (22, 'Reparación de Transmisión en el Automóvil', 1500, 'Activo'),
                                (22, 'Renovación del Baño en la Casa', 2500, 'Activo'),
                                (22, 'Cambio de Neumáticos en el Automóvil', 600, 'Activo'),
                                (22, 'Reparación de Goteras en el Techo', 800, 'Activo'),
                                (23, 'Pérdida de Cliente Clave A', 10000, 'Activo'),
                                (24, 'Gastos Funerarios por Fallecimiento de Familiar', 5000, 'Activo'),
                                (24, 'Gastos Médicos por Enfermedad Grave de Familiar', 8000, 'Activo'),
                                (24, 'Viaje de Emergencia por Enfermedad de Familiar en el Extranjero', 3000, 'Activo'),
                                (24, 'Gastos de Asesoría Legal por Trámites de Herencia', 1500, 'Activo'),
                                (24, 'Apoyo Psicológico por Pérdida de Ser Querido', 600, 'Activo'),
                                (25, 'Reparación de Servidores y Hardware', 7000, 'Activo'),
                                (25, 'Contratación de Servicio de Emergencia en Tecnología', 3000, 'Activo'),
                                (25, 'Pérdida de Ingresos por Tiempo de Inactividad', 10000, 'Activo'),
                                (25, 'Restauración de Datos y Respaldo de Información', 5000, 'Activo'),
                                (25, 'Evaluación y Mejora de la Seguridad del Sistema', 2000, 'Activo'),
                                (26, 'Reparación de Equipo de Laboratorio', 2500, 'Activo'),
                                (26, 'Compensación por Daños a la Infraestructura', 5000, 'Activo'),
                                (26, 'Análisis y Monitoreo de Contaminación', 3000, 'Activo'),
                                (26, 'Adquisición de Equipo de Seguridad Adicional', 1000, 'Activo'),
                                (26, 'Capacitación en Seguridad para Personal', 1200, 'Activo'),
                                (27, 'Salario Mensual', 3000, 'Activo'),
                                (27, 'Bonificación Trimestral', 1000, 'Activo'),
                                (27, 'Comisiones por Ventas', 2000, 'Activo'),
                                (27, 'Incentivos por Desempeño Anual', 1500, 'Activo'),
                                (27, 'Participación en Beneficios de la Empresa', 1200, 'Activo'),
                                (27, 'Prima de Antigüedad', 800, 'Activo'),
                                (27, 'Pago por Horas Extra', 300, 'Activo'),
                                (27, 'Subsidio de Transporte', 150, 'Activo'),
                                (27, 'Incentivo por Logro de Metas', 500, 'Activo'),
                                (27, 'Beneficio de Almuerzo Empresarial', 200, 'Activo'),
                                (28, 'Pensión de Jubilación', 1500, 'Activo'),
                                (28, 'Pensión Complementaria de la Empresa', 800, 'Activo'),
                                (28, 'Pensión de Conyugue Sobreviviente', 1000, 'Activo'),
                                (28, 'Pensión por Incapacidad Permanente', 1200, 'Activo'),
                                (28, 'Pensión de Veterano de Guerra', 2000, 'Activo'),
                                (28, 'Pensión de Invalidez', 1800, 'Activo'),
                                (28, 'Pensión de Viudez', 900, 'Activo'),
                                (28, 'Pensión de Orfandad', 700, 'Activo'),
                                (28, 'Pensión por Accidente Laboral', 1600, 'Activo'),
                                (28, 'Pensión de Jubilación del Fondo Privado', 1400, 'Activo'),
                                (29, 'Renta de Apartamento en el Centro de la Ciudad', 1200, 'Activo'),
                                (29, 'Renta de Oficina en Edificio Comercial', 2500, 'Activo'),
                                (29, 'Renta de Equipos Electronicos', 2500, 'Activo'),
                                (29, 'Renta de Local Comercial en Zona Comercial', 1800, 'Activo'),
                                (29, 'Renta de Propiedad para Eventos y Bodas', 3000, 'Activo'),
                                (29, 'Renta de Terreno para Desarrollo Inmobiliario', 3500, 'Activo'),
                                (30, 'Intereses de Cuenta de Ahorro', 50, 'Activo'),
                                (30, 'Rendimientos de Bonos Corporativos', 200, 'Activo'),
                                (30, 'Dividendos de Acciones', 150, 'Activo'),
                                (30, 'Ganancias por Inversión en Fondos de Índices', 120, 'Activo'),
                                (30, 'Intereses de Certificado de Depósito a Plazo', 100, 'Activo'),
                                (30, 'Rendimientos de Fondos Mutuos', 80, 'Activo'),
                                (30, 'Intereses de Cuenta de Mercado Monetario', 70, 'Activo'),
                                (30, 'Ganancias por Inversiones en Bienes Raíces', 300, 'Activo'),
                                (30, 'Dividendos de Fondos de Inversión', 120, 'Activo'),
                                (30, 'Rendimientos de Notas del Tesoro', 90, 'Activo'),
                                (31, 'Regalías por Derechos de Autor de Libro', 1500, 'Activo'),
                                (31, 'Regalías por Uso de Patente de Producto', 2000, 'Activo'),
                                (31, 'Regalías de una persona', 2000, 'Activo'),
                                (31, 'Regalías por Licencia de Software', 1000, 'Activo'),
                                (31, 'Regalías por Uso de Marca Registrada', 1200, 'Activo'),
                                (31, 'Regalías por Derechos de Música', 1800, 'Activo'),
                                (31, 'Regalías por Uso de Diseño Gráfico', 800, 'Activo'),
                                (31, 'Regalías por Derechos de Fotografía', 1000, 'Activo'),
                                (31, 'Regalías por Uso de Tecnología Patentada', 2500, 'Activo'),
                                (31, 'Regalías por Contenido Audiovisual', 1500, 'Activo'),
                                (31, 'Regalías por Uso de Software de Entretenimiento', 2000, 'Activo'),
                                (32, 'Anualidad por Contrato de Seguro de Vida', 800, 'Activo'),
                                (32, 'Anualidad por Plan de Jubilación Privado', 1200, 'Activo'),
                                (32, 'Anualidad por Fondo de Inversión', 1000, 'Activo'),
                                (32, 'Anualidad por Contrato de Rentas Vitalicias', 1500, 'Activo'),
                                (32, 'Anualidad por Acuerdo de Pensión', 900, 'Activo'),
                                (32, 'Anualidad por Contrato de Seguro de Salud', 700, 'Activo'),
                                (32, 'Anualidad por Plan de Ahorro para la Educación', 1100, 'Activo'),
                                (32, 'Anualidad por Inversión en Bienes Raíces', 1300, 'Activo'),
                                (32, 'Anualidad por Acuerdo de Pensión Alimenticia', 950, 'Activo'),
                                (32, 'Anualidad por Contrato de Rentas de Propiedades', 1400, 'Activo'),
                                (33, 'Pensión de Seguridad Social', 1500, 'Activo'),
                                (33, 'Fondos de Pensiones Privados', 2000, 'Activo'),
                                (33, 'Anualidad de Jubilación', 1000, 'Activo'),
                                (33, 'Ingresos de Cuenta de Retiro Individual (IRA)', 1200, 'Activo'),
                                (33, 'Ingresos de Plan de Pensiones Empresariales', 1800, 'Activo'),
                                (34, 'Ingresos por Pago de Seguro de Vida', 500, 'Activo'),
                                (34, 'Ingresos por Pago de Seguro de Automóvil', 800, 'Activo'),
                                (34, 'Ingresos por Pago de Seguro de Salud', 700, 'Activo'),
                                (34, 'Ingresos por Pago de Seguro de Hogar', 1000, 'Activo'),
                                (34, 'Ingresos por Pago de Seguro de Negocio', 1200, 'Activo'),
                                (35, 'Compensación por Desempleo del Gobierno', 800, 'Activo'),
                                (35, 'Seguro de Desempleo de la Empresa', 1000, 'Activo'),
                                (35, 'Fondo de Reserva para Desempleo', 600, 'Activo'),
                                (35, 'Programa de Ayuda Económica a Desempleados', 1200, 'Activo'),
                                (35, 'Beneficio de Desempleo por Sindicato', 900, 'Activo'),
                                (36, 'Distribución de Utilidades de Sociedad Anónima', 5000, 'Activo'),
                                (36, 'Distribución de Beneficios de Sociedad de Responsabilidad Limitada', 4500, 'Activo'),
                                (36, 'Participación en Ganancias de Sociedad en Comandita', 3000, 'Activo'),
                                (36, 'Distribución de Ingresos de Sociedad de Capital Variable', 5500, 'Activo'),
                                (36, 'Dividendos de Sociedad Cooperativa', 4000, 'Activo'),
                                (37, 'Pago de Alimentos por Servicios de Catering', 800, 'Activo'),
                                (37, 'Asignación de Manutención por Hijo', 600, 'Activo'),
                                (37, 'Pago de Alimentos por Servicios de Restaurante', 400, 'Activo'),
                                (37, 'Apoyo Económico a Familiar para Alimentos', 700, 'Activo'),
                                (37, 'Asignación de Manutención por Conyugue', 500, 'Activo'),
                                (38, 'Pago de Renta Vitalicia por Jubilación', 1500, 'Activo'),
                                (38, 'Pago de Renta Vitalicia por Seguro de Vida', 2000, 'Activo'),
                                (38, 'Pago de Renta Vitalicia por Anualidad', 1000, 'Activo'),
                                (38, 'Pago de Renta Vitalicia por Herencia', 1200, 'Activo'),
                                (38, 'Pago de Renta Vitalicia por Acuerdo Legal', 1800, 'Activo'),
                                (39, 'Comisión por Ventas de Producto A', 10, 'Activo'),
                                (39, 'Bonificación por Cumplimiento de Objetivos', 500, 'Activo'),
                                (39, 'Incentivos por Ventas Trimestrales', 800, 'Activo'),
                                (39, 'Participación en Ganancias de Ventas Online', 300, 'Activo'),
                                (39, 'Premio por Ventas del Mes', 200, 'Activo'),
                                (40, 'Comisiones por Ventas de Productos', 500, 'Activo'),
                                (40, 'Comisiones por Referidos de Clientes', 300, 'Activo'),
                                (40, 'Comisiones por Servicios de Consultoría', 700, 'Activo'),
                                (40, 'Comisiones por Ventas de Publicidad', 400, 'Activo'),
                                (40, 'Comisiones por Ventas de Propiedades', 1000, 'Activo'),
                                (41, 'Bonificación por Cumplimiento de Objetivos Trimestrales', 3000, 'Activo'),
                                (41, 'Bonificación por buen Desempeño', 1000, 'Activo'),
                                (41, 'Bonificación por Ventas de Productos Especiales', 2500, 'Activo'),
                                (41, 'Bonificación por Cierre de Contrato con Cliente Clave', 4000, 'Activo'),
                                (41, 'Bonificación por Desarrollo de Nuevo Producto', 3500, 'Activo'),
                                (41, 'Bonificación por Logro de Metas de Equipo', 2800, 'Activo'),
                                (42, 'Rendimientos de Inversiones en Acciones', 2000, 'Activo'),
                                (42, 'Ingresos por Intereses de Cuentas Bancarias', 500, 'Activo'),
                                (42, 'Ganancias por Venta de Bienes Raíces', 3000, 'Activo'),
                                (42, 'Dividendos de Inversiones en Fondos Mutuos', 800, 'Activo'),
                                (42, 'Ingresos por Bonos y Títulos de Deuda', 1200, 'Activo'),
                                (43, 'Diseño de Sitio Web para Cliente A', 800, 'Activo'),
                                (43, 'Redacción de Artículos para Blog B', 500, 'Activo'),
                                (43, 'Consultoría en Marketing para Empresa C', 1200, 'Activo'),
                                (43, 'Diseño Gráfico para Campaña D', 1000, 'Activo'),
                                (43, 'Desarrollo de Aplicación Móvil para Cliente E', 1500, 'Activo'),
                                (44, 'Propinas en Restaurante', 200, 'Activo'),
                                (44, 'Propinas de trabajo', 200, 'Activo'),
                                (44, 'Propinas en Servicio de Entrega a Domicilio', 150, 'Activo'),
                                (44, 'Propinas en Servicio de Limpieza', 100, 'Activo'),
                                (44, 'Propinas en Servicio de Valet Parking', 50, 'Activo'),
                                (44, 'Propinas en Servicio de Hotel', 300, 'Activo'),
                                (45, 'Ingresos por Anuncios en Sitio Web', 3000, 'Activo'),
                                (45, 'Ingresos por Por venta de productos', 3000, 'Activo'),
                                (45, 'Ingresos por Publicidad en Redes Sociales', 2500, 'Activo'),
                                (45, 'Ingresos por Marketing de Afiliados', 1800, 'Activo'),
                                (45, 'Ingresos por Contenido Patrocinado', 2200, 'Activo'),
                                (45, 'Ingresos por Email Marketing', 1200, 'Activo'),
                                (46, 'Contrato de Consultoría para Proyecto Específico', 2500, 'Activo'),
                                (46, 'Contrato de Diseño Gráfico para Campaña Publicitaria', 1800, 'Activo'),
                                (46, 'Contrato de Desarrollo de Software Personalizado', 3500, 'Activo'),
                                (46, 'Contrato de Traducción de Documentos Técnicos', 2000, 'Activo'),
                                (46, 'Contrato de Servicios Legales para Caso Específico', 3000, 'Activo'),
                                (47, 'Participación en Estudio de Mercado sobre Productos de Belleza', 50, 'Activo'),
                                (47, 'Encuesta sobre Hábitos de Consumo de Alimentos Orgánicos', 30, 'Activo'),
                                (47, 'Estudio de Opinión sobre Tecnología Wearable', 40, 'Activo'),
                                (47, 'Encuesta de Satisfacción de Clientes de Servicios Financieros', 60, 'Activo'),
                                (47, 'Participación en Investigación de Nuevos Sabores de Bebidas', 35, 'Activo'),
                                (48, 'Alquiler de Espacio de Oficina', 2500, 'Activo'),
                                (48, 'Alquiler de Propiedad Residencial', 1800, 'Activo'),
                                (48, 'Alquiler de Local Comercial', 3500, 'Activo'),
                                (48, 'Alquiler de Espacio de Almacenamiento', 800, 'Activo'),
                                (48, 'Alquiler de Equipo de Construcción', 1200, 'Activo'),
                                (48, 'Alquiler de Equipo Electronicos', 1200, 'Activo'),
                                (49, 'Premio al Mejor Diseño de Producto', 1000, 'Activo'),
                                (49, 'Premio por concursos', 1000, 'Activo'),
                                (49, 'Concurso de Innovación Tecnológica', 2500, 'Activo'),
                                (49, 'Premio a la Excelencia en Servicio al Cliente', 800, 'Activo'),
                                (49, 'Concurso de Fotografía Empresarial', 1200, 'Activo'),
                                (49, 'Premio a la Mejor Estrategia de Marketing', 1500, 'Activo'),
                                (50, 'Venta de Productos echos a Mano', 500, 'Activo'),
                                (50, 'Clases de Yoga y Meditación', 300, 'Activo'),
                                (50, 'Talleres de Cocina Gourmet', 800, 'Activo'),
                                (50, 'Creación de Joyería Artesanal', 700, 'Activo'),
                                (50, 'Servicios de Fotografía para Eventos', 1000, 'Activo'),
                                (51, 'Devolución de Producto Defectuoso', 200, 'Activo'),
                                (51, 'Reembolso por Cancelación de Servicio', 150, 'Activo'),
                                (51, 'Devolución de Mercancía por Solicitud del Cliente', 300, 'Activo'),
                                (51, 'Reembolso por Error en Facturación', 100, 'Activo'),
                                (51, 'Devolución de Excedente de Inventarios', 50, 'Activo'),
                                (52, 'Diseño Gráfico para Campaña de Publicidad', 300, 'Activo'),
                                (52, 'Instalación de Equipos en Evento Corporativo', 500, 'Activo'),
                                (52, 'Servicios de Fotografía para Evento Social', 400, 'Activo'),
                                (52, 'Asistencia en Montaje de Escenografía', 250, 'Activo'),
                                (52, 'Traducción de Documentos para Conferencia', 200, 'Activo'); ";
                await DBCuentas.db.ExecuteAsync(query);

            }
            conceptos_detalle = await DBCuentas.db.Table<CONCEPTO_DETALLE>().ToListAsync();


        }

  


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
