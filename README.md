
# Super Aparcamientos API

Sistema para la gestión de un estacionamiento de pago.

- API REST desarrollada con .NET mediante la tecnología de Azure Functions, y desplegada en Azure a través del [IaC (Infrasestructure as Code)](https://learn.microsoft.com/en-us/devops/deliver/what-is-infrastructure-as-code) de Microsoft, [Biceps](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview?tabs=bicep).
- Implementa el patrón de inyección de dependencias, principios SOLID y uso de la arquitectura de n-capas.
- Se hace uso de Entity Framework y del patrón repository para el acceso a datos.
- Automatización del despliegue a través de Github Actions y la herramienta [AZD](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/).
- Uso de Swagger para la documentación de los endpoints.
  
## Solución

La solución esta dividida en los siguientes proyectos:

- **SuperAparcamiento.DataAccess**: Maneja el acceso a los datos implementando el patrón repository y utilizando Entity Framework
- **SuperAparcamiento.Functions**: Contiene las Azure Functions que exponen la API REST.
- **SuperAparcamiento.Logic**: Implementa la lógica de negocio y la generación de Excel para el informe.
- **SuperAparcamiento.Model**: Define los modelos, excepciones y constantes de datos utilizadas en la aplicación

## Swagger

La documentación Swagger de la API se encuentra en la siguiente dirección: https://super-aparcamientos.ahc.software/swagger/ui

## Modelo de datos

El modelado de datos propuesto para la solución ha sido el siguiente:

![bbdd](https://github.com/user-attachments/assets/0eca924f-4272-40a8-998f-c042ed3040eb)


## Autoría

- Ángel Herrador - [@ehf32](https://www.github.com/ehf32)

