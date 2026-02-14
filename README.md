o Descripción general de la arquitectura

La arquitectura usada en este proyecto consta de cuatro capas y dos patrones de diseño. La aplicación es un ejemplo básico de un sistema de facturación donde se manejan tres entidades: factura (invoice), detalles de factura (invoice detail) y materiales (materials).
Para la factura y sus detalles se estableció dos casos de uso: crear factura y buscar facturas con base a una serie de filtros, por otro lado, para los materiales se tienen tres casos de uso: crear, actualizar y buscar.
Con todo lo anterior, la arquitectura sigue el modelo estándar de las aplicaciones por capas para proyectos .NET Core separada por Application, Domain, Insfrastructure y Api, el uso de los elementos de la arquitectura se adecua según el alcance para este reto, haciendo que se aplique un mínimo de estos para lograr el objetivo de hacer una aplicación escalable y mantenible.

o  Explicación de las capas.

 -> Application: Contiene las interfaces que se relacionan con el dominio en la capa de infrastructura, las interfaces y las implementaciones de la lógica de negocio, mapeo de dtos, casos de uso para las entidades planteadas en el proyecto, la inyección de dependencias de esta capa y una serie de clases con métodos comunes que pueden ser usados por diferentes implementaciones en este nivel.
 
 -> Domain: Todas las clases, enums, etc. usados para la abstracción de entidades que van a soportar la representación de datos, en este caso no hay reglas complejas por entidad.
 
 -> Infrastructure: Se encuentra la implementación de las interfaces con lógica que interactua con la base de datos, inyección de dependecias de esta capa, el contexto de base de datos junto con la clase que controla los dbset por modelo y los archivos de migración.
 
 -> Api: Expone la lógica de negocio en endpoints para que sean consumidos por un cliente, está la configuración del program para aplicar la inyección de dependencias de las capas de aplicación e infrastructura y configuración de la conexión con la base de datos.

o  Patrones utilizados y justificación.
	-> Dependency Injection: se usa este patrón para desacoplar las diferentes clases que interactuan en el proyecto, especialmente los elementos relacionados con la lógica del nivel de aplicación o infrastructura. De esta manera se puede establecer métodos que luego se implementan en los servicios según la necesidad de la funcionalidad.
	-> Repository pattern: en conjunto a DI proporciona un método para lograr una aplicación escalable y mantenible al establecer un método para interactuar con la base de datos sin exponer las operaciones que se vayan a realizar, consultas EF o cualquier otra forma de intercambio de datos con la BD. De esta manera hay un espacio en específico para delegar la responsabilidad de las operaciones a BD.

o  Decisiones arquitectónicas relevantes.
	-> Separación de la aplicación por medio de arquitectura de capas logrando que cada una tenga una responsabilidad y que la capa de Domain no dependa de ninguna
	-> El db context se encuentra en la capa de Infrastructure y se registra por medio de DI
	-> Se mantiene separado el contexto de dto de las entidades al aplicar la conversión por medio de automapper en la capa de Application
	-> La capa de negocios se establece como un método para agrupar operaciones en lugar de crear una carpeta por caso de uso, de esta manera se simplifica la arquitectura pensando en el alcance del proyecto
	-> Se establece métodos comunes o utilidades que pueden ser usados de manera transversal en la capa de Application
	-> Se modulariza la inyección de dependencias con el motivo de hacer más claro y limpio el program.cs

