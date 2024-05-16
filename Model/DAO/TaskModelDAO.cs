
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
        public async Task<List<TaskModel>> GetAll(string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetTasks");
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<TaskModel> tasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);
                    return tasksList;
                }
                else
                {
                    CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. Cod: {response.StatusCode}");
                    return null;
                }
            }
            
            catch (TaskCanceledException)
            {
                CustomMessageBox.MostrarCustomMessageBox("The request has time out");
                return null;
            }
            
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. Message: {ex.Message}");
                return null;
            }
        }
        public async Task<List<TaskModel>> GetWhere(string title, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetTasksWhere")+title;
                List<TaskModel> tasksList = new List<TaskModel>();
                HttpResponseMessage response = await client.GetAsync(apiUrl);

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
        public async Task<bool> Delete(int id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                string apiUrl = (string)Application.Current.FindResource("DeleteTask")+id;
                
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);
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
        public async Task<bool> Post(TaskModel task, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string JsonTask = JsonConvert.SerializeObject(task);
                string urlApi = (string)Application.Current.FindResource("PostTask");
                HttpResponseMessage response = await client.PostAsync(urlApi, new StringContent(JsonTask, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    CustomMessageBox.MostrarCustomMessageBox("Task Added Successflly");
                    return true;
                }
                else
                {
                    CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. (AddTask) Cod: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. 'AddTasks' CodServer: {ex.Message}");
                return false;
            }
        }
        public async Task<TaskModel> GetSpecificObject(int id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetEspecificTask")+id;
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    TaskModel task = JsonConvert.DeserializeObject<TaskModel>(json);
                    return task;
                }
                else
                {
                    CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. Message: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> Put(TaskModel task, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string urlApi = (string)Application.Current.FindResource("PutTask");
                string jsonPrioritie = JsonConvert.SerializeObject(task);
                HttpResponseMessage response = await client.PutAsync(urlApi, new StringContent(jsonPrioritie, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    CustomMessageBox.MostrarCustomMessageBox("Task Edited Successflly");
                    return true;
                }
                else
                {
                    CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. Server Cod: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox($"Error: Operation could not be completed. Cod: {ex.Message}");
                return false;
            }
        }
    }
}
