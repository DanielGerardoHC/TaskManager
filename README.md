# Task Manager

**Descripcion**

Task Manager es una app de escritorio desarrollada con el framwork .NET8,  WPF (Windows Presentation Fundation) y C# utilizando los patrones DAO, MVVM, y Cliente Servidor la cual se encarga de proporcionar una experiencia atractiva
a los usuarios a la hora de llevar un registro de tareas pendientes, incorporando un DashBoard en el cual se puede visualizar las estadisticas de las tareas con prioridad alta, tareas
pendiente y tambien cuales han sido las ultimas tareas añadidas por el usuario, ademas de proporcionar la funcionalidad de que los usuarios puedan modificar eliminar y hacer una busqueda
en su lista de tareas y modificar sus credenciales de acceso o informacion personal todo esto consumiendo el servicio de una API de tipo RESTfull que se encarga de la manipulacion a la base de 
datos. El codigo y documentacion de la API la encontrara aqui:  https://github.com/DanielGerardoHC/TaskManagerServiceApi

**Funcionalidades**

Crear tareas: Permite a los usuarios añadir nuevas tareas especificando detalles como el título, la descripcion, la fecha de vencimiento, la prioridad, el estado y el usuario asignado 

Leer tareas: Proporciona una vista general de todas las tareas existentes, con detalles completos de cada una devolviendo unicamente las tareas segun el token generado cuando un usuario se autentifica

Estadisticas: Proporciona un DashBoard para ver una estadistica del progreso y detalle de las tareas

Actualizar tareas: Permite modificar los detalles de las tareas existentes Eliminar tareas: Facilita la eliminación de tareas que ya no son necesarias 

Registrar: Permite relizar el registro de nuevos usuarios 

Actualizar Usuario: Proporciona la funcionalidad de que los usuario puedan modificar sus credenciales de inicio de sesion o informacion personal

**Tecnologías Utilizadas**

.NET8, C#, WPF, ademas del uso de la biblioteca **Material Design Theme** para un diseño atractivo de la aplicacion

# Estructura del Proyecto

Images: Contiene las imagenes utilizadas en la interfaz de usuario

Interfaces: Contiene la interfaz principal para el patron de diseño DAO la cual contiene los metodos abstractos para la consultas HTTPS, Get Put Push etc.

MaterialDesignTheme: Contiene el diccionario de recursos para la integracion de la biblioteca MateriaDesignTheme para ser utilizados por las vistas

Model: Contiene las clases Modelo para hacer uso del patron DAO y la clase modelo para la actualizacion de contraseñas

Model\DAO: Contiene las clases que implementan la interfaz DAO para realizar las consultas HTTPS a la API

ViewModel: Contiene las clases que manejan la logica de manipulacion y presentacion de datos para ser presentados por las vistas

View: Contiene los archivos XAML de la (UI) para la presentacion eh interaccion de los datos

AppResource: Contiene diccionarios de recursos de estilos de componentes especificos para ser utilizados por las vistas ademas del diccionario de recursos
Que contiene las Url de los endpoints de la API

**Requisitos**

.NET 8.0, Visual Studio 2022 o Rider
