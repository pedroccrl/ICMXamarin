using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ICMXamarin.Model.Api
{
    class ComputerVision
    {
        public static async Task<string> DescreverImagem(byte[] data)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "e27d0a628eee4ab8984a771eb470cc63");
            
            var uri = "https://api.projectoxford.ai/vision/v1.0/analyze?visualFeatures=Description";
            HttpResponseMessage response;
            // Request body
            string resposta = string.Empty;
            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                resposta = await response.Content.ReadAsStringAsync();
            }

            return resposta;
        }

        public static async void DownoadHttp()
        {
            var client = new HttpClient();
            byte[] resposta = await client.GetByteArrayAsync("https://clotildetavares.files.wordpress.com/2009/11/futebol.jpg");
            DescreverImagem(resposta);
        }
    }
}