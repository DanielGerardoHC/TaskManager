﻿
using AdministradorDeTareas.Interfaces;
using AdministradorDeTareas.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdministradorDeTareas.Model.DAO
{
    public class TaskModelDAO : ITaskManagerServiceDAO<TaskModel>
    {
        public static readonly HttpClient client = new HttpClient();
        public List<TaskModel> GetAll(int userID)
        {
            try
            {
                string apiUrl = (string)Application.Current.FindResource("GetTasksUrl")+userID;
                List<TaskModel> tasksList;
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    tasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);
                    return tasksList;
                }
                else
                {
                    string errorMessage = $"Error: Operation could not be completed. Code: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                    customMessageBox.ShowDialog();
                    return null;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: Operation could not be completed. Message: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
                return null;
            }
        }
        public List<TaskModel> GetWhere(string title, int userID)
        {
            try
            {
                string apiUrl = (string)Application.Current.FindResource("GetTasksWhere?")+title+ "&userId="+userID;
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
                    string Message = $"Error: Operation could not be completed. Cod: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error: Operation could not be completed. Cod: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                customMessageBox.ShowDialog();
                return null;
            }
        }
        public bool Delete(int id)
        {
            string apiUrl = (string)Application.Current.FindResource("DeleteTask")+id;
            try
            {
                HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string Message = "Task deleted successfully";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return true;
                }
                else
                {
                    string Message = $"Task not deleted. Error Code: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Task not deleted. Error Code: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                customMessageBox.ShowDialog();
                return false;
            }
        }
        public bool Post(TaskModel task)
        {
            try
            {
                string JsonTask = JsonConvert.SerializeObject(task);
                // hacemos referencia al recurso donde se encuentran las Url de la Api
                string urlApi = (string)Application.Current.FindResource("PostTask");
                // configurar la solicitud HTTP POST con el contenido JSON
                HttpResponseMessage response = client.PostAsync(urlApi, new StringContent(JsonTask, Encoding.UTF8, "application/json")).Result;
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    string description = "Task Added Successflly";
                    CustomMessageBox messageBox = new CustomMessageBox(description);
                    messageBox.ShowDialog();
                    return true;
                }
                else
                {
                    string Message = $"Error: Operation could not be completed. Cod: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error: Operation could not be completed. CodServer: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                customMessageBox.ShowDialog();
                return false;
            }
        }
        public TaskModel GetEspecificObject(int id, int userID)
        {
            try
            {
                TaskModel task;
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
                    CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                    customMessageBox.ShowDialog();
                    return null;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: Operation could not be completed. Message: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
                return null;
            }
        }
        public bool Put(int id , TaskModel task)
        {
            try
            {
                string urlApi = (string)Application.Current.FindResource("PutTask")+id;
                string jsonPrioritie = JsonConvert.SerializeObject(task);
                // configurar la solicitud HTTP Put con el contenido JSON
                HttpResponseMessage response = client.PutAsync(urlApi, new StringContent(jsonPrioritie, Encoding.UTF8, "application/json")).Result;
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    string description = "Task Edited Successflly";
                    CustomMessageBox messageBox = new CustomMessageBox(description);
                    messageBox.ShowDialog();
                    return true;
                }
                else
                {
                    string Message = $"Error: Operation could not be completed. Server Cod: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"Error: Operation could not be completed. Cod: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                customMessageBox.ShowDialog();
                return false;
            }
        }
    }
}
