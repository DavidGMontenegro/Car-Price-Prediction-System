using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinalAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] ProfilePicture { get; set; }

        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;

            // URL de la imagen por defecto
            string defaultPictureUrl = "https://backend-getd.s3.eu-west-2.amazonaws.com/UserDefaultImage.jpg";

            // Descargar la imagen y convertirla en bytes
            this.ProfilePicture = DownloadImage(defaultPictureUrl).GetAwaiter().GetResult();
        }

        // Método para descargar la imagen desde una URL
        private async Task<byte[]> DownloadImage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Realizar la solicitud HTTP
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Leer los bytes de la imagen desde la respuesta
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    // Si la solicitud no tiene éxito, lanzar una excepción o devolver un valor predeterminado
                    throw new Exception($"Failed to download image from {url}");
                }
            }
        }
    }
}
