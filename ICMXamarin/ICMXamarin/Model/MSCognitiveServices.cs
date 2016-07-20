using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICMXamarin.Model
{
    public static class MSCognitiveServices
    {
        /// <summary>
        /// Retorna a descrição formatada da imagem
        /// </summary>
        /// <param name="dadosImg"></param>
        /// <returns></returns>
        public static async Task<string> GetDescricaoImagemComputerVisionApi(byte[] dadosImg)
        {
            string json = await Api.ComputerVision.DescreverImagem(dadosImg);
            RespostaComputerVision resposta = JsonConvert.DeserializeObject<RespostaComputerVision>(json);
            return FormataResposta(resposta.description.captions[0].text, resposta.description.captions[0].confidence);
        }

        static string FormataResposta(string descricao, float confianca)
        {
            confianca *= 100;
            int conf = (int)confianca;
            if (conf < 0) return $"Confiança muito baixa. Tire outra foto.";
            else return $"Com confiança de {conf} porcento, eu vejo {descricao}";
        }
    }


    public class RespostaComputerVision
    {
        public Description description { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Description
    {
        public string[] tags { get; set; }
        public Caption[] captions { get; set; }
    }

    public class Caption
    {
        public string text { get; set; }
        public float confidence { get; set; }
    }

    public class Metadata
    {
        public int width { get; set; }
        public int height { get; set; }
        public string format { get; set; }
    }

}
