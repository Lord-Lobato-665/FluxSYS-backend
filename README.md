# FluxSYS - Sistema de Gestión de Inventario

## Problemática

La gestión de inventarios es un aspecto crucial en cualquier empresa que maneje productos. Sin embargo, la falta de un sistema adecuado para llevar a cabo esta tarea puede dar lugar a una serie de problemas que afectan la eficiencia operativa, la satisfacción del cliente y los costos. A continuación, se describen las principales problemáticas derivadas de una gestión de inventarios deficiente:

- Pérdida de productos por mala organización
- Errores humanos en el conteo y registro manual
- Dificultad para rastrear existencias en tiempo real
- Falta de control en entradas y salidas de productos
- Demoras en la reposición de mercancía

## Integrantes

- Sánchez Lobato Gael
- Quintero Escobar Carlos Máximo
- Gutiérrez Canul Gustavo
- Raymundo Mata Isha Mia

- Velázquez De La Cruz Carlos Yahir

## Librerías

- **Entity Framework Core (EF Core)**: Para la gestión de la base de datos y las migraciones.
- **JWT (JSON Web Tokens)**: Para la autenticación y autorización de usuarios.
- **Swagger (Swashbuckle)**: Para la documentación y prueba de la API.
- **DevExpress**: Para componentes de interfaz de usuario y reportes.

## Cómo Correr el Proyecto

Sigue estos pasos para ejecutar el proyecto en tu entorno local:

1. **Clona el repositorio**

   git clone https://github.com/Lord-Lobato-665/FluxSYS-backend.git

2. **Navega al repositorio**

    cd FluxSYS-backend

3. **Restaura las dependencias de .NET en la consola de desarrollardor:**

    dotnet restore

4. **Configura la base de datos:**

   - Asegúrate de tener SQL Server instalado y configurado.
   - Modifica la cadena de conexión en appsettings.json con tus credenciales.

5. **Ejecuta las migraciones para crear la base de datos:**

    dotnet ef database update

6. **Ejecuta la aplicación:**

    dotnet run

7. **Verificacion del despliegue**

    Verifica en tu navegador predeterminado si la aplicacion se ejecuto de forma correcta y de forma automatica

## NOTAS ADICIONALES