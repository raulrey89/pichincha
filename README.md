# Reto Pichincha - .Net Core

## Resumen

 - Fundamentos
	 - Overview
	 - Git
	 - CLI commands
	 - Program Class
	 - Middleware
	 - Configuration
	 - Environments
	 - Logging
 -  Dependency Injection
	 - Fundamentals
	 - .Net Core DI
 - Data Layer
	 - ORM
	 - Patterns
	 - Entity Framework Core
 - Web Development
 - Security
 - Unit Testing
    - TDD - Test Driven Development
    - Xunit
    - Mock library

# Fundamentals
**.NET Core**  nos permite realizar todo tipo de aplicaciones, como aplicaciones web que podrás desplegar en Windows, Linux, Mac Os.

## Git

	git clone 
Se utiliza principalmente para apuntar a un repositorio existente y clonar o copiar dicho repositorio en un nuevo directorio, en otra ubicación.

	git init:
Esto crea un subdirectorio nuevo llamado .git, el cual contiene todos los archivos necesarios del repositorio – un esqueleto de un repositorio de Git. Todavía no hay nada en tu proyecto que esté bajo seguimiento.

	git fetch:
Descarga los cambios realizados en el repositorio remoto.

	git merge <nombre_rama>:
Impacta en la rama en la que te encuentras parado, los cambios realizados en la rama “nombre_rama”.

	git pull:
Unifica los comandos fetch y merge en un único comando.

	git commit -m "<mensaje>":
Confirma los cambios realizados. El “mensaje” generalmente se usa para asociar al commit una breve descripción de los cambios realizados.

	git push origin <nombre_rama>:
Sube la rama “nombre_rama” al servidor remoto.

	git status:
Muestra el estado actual de la rama, como los cambios que hay sin commitear.

	git add <nombre_archivo>:
Comienza a trackear el archivo “nombre_archivo”.

	git checkout -b <nombre_rama_nueva>:
Crea una rama a partir de la que te encuentres parado con el nombre “nombre_rama_nueva”, y luego salta sobre la rama nueva, por lo que quedas parado en esta última.

	git checkout -t origin/<nombre_rama>:
Si existe una rama remota de nombre “nombre_rama”, al ejecutar este comando se crea una rama local con el nombre “nombre_rama” para hacer un seguimiento de la rama remota con el mismo nombre.

## Download and Install
Puedes descargar .net core desde la [pagina oficial](https://dotnet.microsoft.com/download).
Una vez que instale el SDK, abra una ventana de consola y escriba el siguiente comando para verificar la instalación:

    dotnet --info

Si quieres saber más sobre el proceso de instalación puedes ver esto [link](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro)

## CLI Commands

Cuando instala el SDK, proporciona una interfaz de línea de comandos (CLI), esta interfaz es una herramienta de línea de comandos multiplataforma que se utiliza para desarrollar y realizar diversas actividades de desarrollo al desarrollar aplicaciones .Net Core.

## Program Class
Si está creando su aplicación principal de asp.net con 6.0, no verá la clase Startup.cs en los archivos del proyecto, como solíamos ver en la versión anterior de la aplicación central de asp.net, y solíamos registrar las dependencias. de la aplicación y el middleware.

Entonces, en ASP.NET 6.0, la clase Startup.cs se elimina y la clase Program.cs es el lugar donde se registran las dependencias de la aplicación y el middleware.

## Middleware

La canalización de gestión de solicitudes se compone de una serie de componentes de middleware. Cada componente realiza operaciones asincrónicas en un `HttpContext` y luego invoca el siguiente middleware en la canalización o finaliza la solicitud.

Por convención, un componente de middleware se agrega a la canalización invocando su método de extensión `Usar...` en el método `Startup.Configure`. Por ejemplo, para habilitar la representación de archivos estáticos, llame a `UseStaticFiles`.

## Configuration

ASP.NET Core proporciona un marco de configuración que obtiene la configuración como pares de nombre y valor de un conjunto ordenado de proveedores de configuración. Hay proveedores de configuración integrados para una variedad de orígenes, como archivos _.json_, archivos _.xml_, variables de entorno y argumentos de línea de comandos. También puede escribir proveedores de configuración personalizados.

## Logging

ASP.NET Core admite una API de registro que funciona con una variedad de proveedores de registro integrados y de terceros. Los proveedores disponibles incluyen los siguientes:

-   Console
-   Debug
-   Event Tracing on Windows
-   Windows Event Log
-   TraceSource
-   Azure App Service
-   Azure Application Insights

# Dependency Injection
Es una técnica mediante la cual un objeto (o método estático) proporciona las dependencias de otro objeto. Una dependencia es un objeto que se puede utilizar (un servicio). Una inyección es el paso de una dependencia a un objeto dependiente (un cliente) que la usaría.

Para saber más sobre DI puedes ver estos enlaces:
 - [.Net Core DI](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)

# Data Layer

## ORM
**Object-relational mapping** (**ORM**, **O/RM**, and **O/R mapping tool**) es un modelo de programación que permite mapear las estructuras de una base de datos relacional

## Entity Framework Core

EF Core es una tecnología de acceso a datos para .NET Core y .NET Framework. Es multiplataforma y de código abierto desarrollado por Microsoft con aportes de la comunidad.
