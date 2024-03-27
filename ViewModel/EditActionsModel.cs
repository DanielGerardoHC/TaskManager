using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdministradorDeTareas.Model;
using Task = AdministradorDeTareas.Model.Task;

namespace AdministradorDeTareas.ViewModel
{
    public class EditActionsModel : ViewModelBase
    {
        private List<Task> _tasks;

        public List<Task> Tasks
        {

            // si o si debe cada propiedad debe tener su get y set
            // junto con el metodo OnpropetyChanged
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public EditActionsModel()
        {
            // inicializacion de la lista
            Tasks = new List<Task>
            {
    new Task { title = "Comprar víveres", description = "Necesito ir al supermercado y comprar alimentos para la semana", priority = "high"},
    new Task { title = "Limpiar la casa", description = "La casa está un poco desordenada y necesita una limpieza a fondo", priority = "medium"},
    new Task { title = "Hacer ejercicio", description = "Es importante mantenerse activo, así que haré una rutina de ejercicios en casa", priority = "high"},
    new Task { title = "Estudiar para el examen", description = "Tengo un examen importante la próxima semana, así que debo repasar los temas", priority = "high"},
    new Task { title = "Cortar el césped", description = "El césped del jardín está creciendo mucho y necesita ser cortado", priority = "low"},
    new Task { title = "Reunión de trabajo", description = "Tengo una reunión con el equipo a primera hora para discutir los proyectos pendientes", priority = "medium"},
    new Task { title = "Llamar al médico", description = "Necesito hacer una cita con el médico para una revisión de rutina", priority = "medium"},
    new Task { title = "Enviar correo electrónico", description = "Debo enviar un correo electrónico a mis colegas con las actualizaciones del proyecto", priority = "low"},
    new Task { title = "Pasear al perro", description = "Es hora de sacar al perro a pasear y disfrutar del aire fresco", priority = "low"},
    new Task { title = "Preparar cena", description = "Voy a cocinar una deliciosa cena para disfrutar en familia esta noche", priority = "medium"},
    new Task { title = "Hacer la colada", description = "La ropa sucia se está acumulando, así que es hora de lavarla", priority = "low"},
    new Task { title = "Llamar a mamá", description = "Hace tiempo que no hablo con mamá, así que le daré una llamada para ponerme al día", priority = "low"},
    new Task { title = "Organizar el armario", description = "El armario está desordenado y necesito organizar la ropa", priority = "medium"},
    new Task { title = "Leer un libro", description = "Voy a dedicar un tiempo a leer ese libro que tengo pendiente en la estantería", priority = "low"},
    new Task { title = "Arreglar la lámpara", description = "La lámpara del salón está parpadeando, así que debo arreglarla", priority = "medium"},
    new Task { title = "Planificar vacaciones", description = "Es hora de empezar a planificar las vacaciones de verano para este año", priority = "high"},
    new Task { title = "Renovar el pasaporte", description = "El pasaporte está a punto de caducar, así que debo renovarlo a tiempo", priority = "high"},
    new Task { title = "Ir al banco", description = "Necesito ir al banco para hacer algunas gestiones financieras", priority = "low"},
    new Task { title = "Ordenar el escritorio", description = "Mi escritorio está lleno de papeles, así que necesito organizarlo", priority = "medium"},
    new Task { title = "Aprender un nuevo idioma", description = "Voy a dedicar un tiempo cada día a aprender un nuevo idioma", priority = "high"},
};
        }
    }
}