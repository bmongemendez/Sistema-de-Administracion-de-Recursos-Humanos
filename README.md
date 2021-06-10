# Sistema-de-Administracion-de-Recursos-Humanos

Este software está diseñado para la administración y el manejo de los empleados de la empresa, incorporando un sofisticado y eficaz sistema de control de empleados y planilla en el cual se cumplan las funciones de administrar salarios mediante un seguimiento de las horas de manera individual por empleado, controlar las vacaciones correspondientes, permitir una revisión y comentarios por empleado desde un supervisor a cargo, entre otras más funciones.

## Desarrolladores

* [Brayan Monge Méndez](http://github.com/bmongemendez)
* [Byron Leal Mora](http://github.com)

## Requerimientos Mínimos

* ASP.Net Core = 3.1
* ADO.NET Entity Framework Core = 3.1.1
* MySQL MySQL Community Server >= 8.0.22
* MySQL Workbench 8.0.22

## Como descargar e iniciar el proyecto en un entorno de desarrollo/pruebas

1. Descargar e instalar Visual Studio 2019 >= 16.7.8
2. Descargar e instalar MySql.
3. Descargar e instalar MySql Workbench.
4. Abrir MySql y crear una base de datos llamada `root` con password `fidelitas` en el puerto que trae MySql por defecto.
5. Ejecutar el script de creación de la base de datos disponible [aquí](https://github.com/bmongemendez/Sistema-de-Administracion-de-Recursos-Humanos/blob/main/script-sarh-application.sql).
6. Descargar e instalar Git desde su [página oficial](https://git-scm.com/).
7. Abrir una terminal y clonar el repositorio ejecutando el siguiente comando:
   ```sh
   git clone https://github.com/bmongemendez/Sistema-de-Administracion-de-Recursos-Humanos 
   ```
8. Abrir Visual Studio 2019 y abrir el proyecto localizado en la carpeta desde donde se hizo el "git clone".
9. Realizar un rebuild del proyecto e iniciarlo.
