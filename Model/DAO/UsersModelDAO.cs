
using AdministradorDeTareas.Interfaces;
using AdministradorDeTareas.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministradorDeTareas.Model.DAO
{
    public class UsersModelDAO : ITaskManagerServiceDAO<UsersModel> 
    {
        public static readonly HttpClient client = new HttpClient();
        public bool Delete(int id, string token)
        {
            string apiUrl = $"https://localhost:44384/api/Users/{id}";
            try
            {
                HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string Message = "User deleted successfully";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return true;
                }
                else
                {
                    string Message = $"User not deleted. Error Code: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Message = $"User not deleted. Error Code: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                customMessageBox.ShowDialog();
                return false;
            }
        }
        public List<UsersModel> GetAll(string token)
        {
            throw new NotImplementedException();
        }
        public UsersModel GetSpecificObject(int id, string token)
        {
            try
            {
                UsersModel user;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl = (string)Application.Current.FindResource("GetUser"); 
                HttpResponseMessage response =  client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json =  response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UsersModel>(json);
                    return user;
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
        public List<UsersModel> GetWhere(string userName, string token)
        {
            throw new NotImplementedException();
        }
        public bool Post(UsersModel user, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                // Serializamos al usuario en json
                string jsonUser = JsonConvert.SerializeObject(user);
                // Configure the HTTP POST request with the JSON content
                string apiURL = (string)Application.Current.FindResource("PostUser");
                HttpResponseMessage response = client.PostAsync(apiURL, new StringContent(jsonUser, Encoding.UTF8, "application/json")).Result;
                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    string description = "User registered successfully";
                    CustomMessageBox messageBox = new CustomMessageBox(description);
                    messageBox.ShowDialog();
                    return true;
                }
                else
                {
                    string message = $"Error: Operation could not be completed. Code: {response.StatusCode}";
                    CustomMessageBox customMessageBox = new CustomMessageBox(message);
                    customMessageBox.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string message = $"Error: Operation could not be completed. Code: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(message);
                customMessageBox.ShowDialog();
                return false;
            }
        }
        public bool Put(UsersModel user, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string jsonPrioritie = JsonConvert.SerializeObject(user);
                // Metodo Put que evalua las credenciales del usuario y devuelve tru si son correctas
                string urlApi = (string)Application.Current.FindResource("PutUser");
                // configurar la solicitud HTTP put con el contenido JSON
                HttpResponseMessage response = client.PutAsync(urlApi, new StringContent(jsonPrioritie, Encoding.UTF8, "application/json")).Result;
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    string description = "User Modified Successflly";
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
                string Message = $"Error: Operation could not be completed. Cod: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                customMessageBox.ShowDialog();
                return false;
            }
        }
        public string Put(UsersModel user)
        {
            try
            {              
                string jsonPrioritie = JsonConvert.SerializeObject(user);

                string urlApi = "https://localhost:7040/TM/Users/auth";
                // configurar la solicitud HTTP put con el contenido JSON
                HttpResponseMessage response = client.PostAsync(urlApi, new StringContent(jsonPrioritie, Encoding.UTF8, "application/json")).Result;
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // Deserializa la respuesta JSON para obtener el token
                    var tokenResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    string token = tokenResponse.token;
                    return token;
                }
                else
                {
                    string Message = $"The Password or Username are incorrect: {response.StatusCode}";
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
    }
}
