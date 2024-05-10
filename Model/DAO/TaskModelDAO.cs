
using AdministradorDeTareas.Interfaces;
using AdministradorDeTareas.View;
using AdministradorDeTareas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdministradorDeTareas.Model.DAO
{
    public class TaskModelDAO : ITaskManagerServiceDAO<TaskModel>
    {
        public static readonly HttpClient client = new HttpClient();
        public List<TaskModel> GetAll(string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetTasks");
                List<TaskModel> tasksList = new List<TaskModel>();

                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    tasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);
                    return tasksList;
                }
                else
                {
                    string errorMessage = $"Error: Operation could not be completed. Cod: {response.StatusCode}";
                    CustomMessageBox.MostrarCustomMessageBox(errorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: Operation could not be completed. Message: {ex.Message}";
                CustomMessageBox.MostrarCustomMessageBox(errorMessage);
                return null;
            }
        }
        public List<TaskModel> GetWhere(string title, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetTasksWhere")+title;
                List<TaskModel> tasksList = new List<TaskModel>();
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    tasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);
                    return tasksList;
                }
                else
                {
                    string Message = $"Error: Operation could not be completed 'GetTasks'. Cod: {response.StatusCode}";
                    CustomMessageBox.MostrarCustomMessageBox(Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error: Operation could not be completed 'GetTasks'. Cod: {ex.Message}";
                CustomMessageBox.MostrarCustomMessageBox(Message);
                return null;
            }
        }
        public bool Delete(int id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string apiUrl = (string)Application.Current.FindResource("ViewModelDeleteTask")+id;
            try
            {
                HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string Message = "Task deleted successfully";
                    CustomMessageBox.MostrarCustomMessageBox(Message);
                    return true;
                }
                else
                {
                    string Message = $"Task not deleted. Error Code: {response.StatusCode}";
                    CustomMessageBox.MostrarCustomMessageBox(Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Task not deleted. Error Code: {ex.Message}";
                CustomMessageBox.MostrarCustomMessageBox(Message);
                return false;
            }
        }
        public bool Post(TaskModel task, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string JsonTask = JsonConvert.SerializeObject(task);
                // hacemos referencia al recurso donde se encuentran las Url de la Api
                string urlApi = (string)Application.Current.FindResource("PostTask");
                // configurar la solicitud HTTP POST con el contenido JSON
                HttpResponseMessage response = client.PostAsync(urlApi, new StringContent(JsonTask, Encoding.UTF8, "application/json")).Result;
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    string description = "Task Added Successflly";
                    CustomMessageBox.MostrarCustomMessageBox(description);
                    return true;
                }
                else
                {
                    string Message = $"Error: Operation could not be completed. (AddTask) Cod: {response.StatusCode}";
                    CustomMessageBox.MostrarCustomMessageBox(Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error: Operation could not be completed. 'AddTasks' CodServer: {ex.Message}";
                CustomMessageBox.MostrarCustomMessageBox(Message);
                return false;
            }
        }
        public TaskModel GetSpecificObject(int id, string token)
        {
            try
            {
                TaskModel task;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetEspecificTask")+id;
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    task = JsonConvert.DeserializeObject<TaskModel>(json);
                    return task;
                }
                else
                {
                    string errorMessage = $"Error: Operation could not be completed. Code: {response.StatusCode}";
                    CustomMessageBox.MostrarCustomMessageBox(errorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: Operation could not be completed. Message: {ex.Message}";
                CustomMessageBox.MostrarCustomMessageBox(errorMessage);
                return null;
            }
        }
        public bool Put(TaskModel task, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string urlApi = (string)Application.Current.FindResource("PutTask");
                string jsonPrioritie = JsonConvert.SerializeObject(task);
                // configurar la solicitud HTTP Put con el contenido JSON
                HttpResponseMessage response = client.PutAsync(urlApi, new StringContent(jsonPrioritie, Encoding.UTF8, "application/json")).Result;
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    string description = "Task Edited Successflly";
                    CustomMessageBox.MostrarCustomMessageBox(description);
                    return true;
                }
                else
                {
                    string Message = $"Error: Operation could not be completed. Server Cod: {response.StatusCode}";
                    CustomMessageBox.MostrarCustomMessageBox(Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error: Operation could not be completed. Cod: {ex.Message}";
                CustomMessageBox.MostrarCustomMessageBox(Message);
                return false;
            }
        }
    }
}
