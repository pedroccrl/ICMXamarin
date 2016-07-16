using ICMXamarin.Model.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xamarin.Forms;

namespace ICMXamarin.Views
{
    public class CameraPreview : View, ICameraOption
    {
        public static readonly BindableProperty CameraProperty = BindableProperty.Create(
            propertyName: "Camera",
            returnType: typeof(CameraOptions),
            declaringType: typeof(CameraPreview),
            defaultValue: CameraOptions.Rear);

        public CameraOptions Camera
        {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public async void ImagemCapturada(byte[] bytes)
        {
            // comprime
            var comprimido = Compress(bytes);

            // envia api
            string descricao = await ComputerVision.DescreverImagem(bytes);

            // traduz

            // fala
        }

        byte[] Compress(byte[] b)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream z = new GZipStream(ms, CompressionMode.Compress, true))
                    z.Write(b, 0, b.Length);
                return ms.ToArray();
            }
        }

        byte[] Decompress(byte[] b)
        {
            using (var ms = new MemoryStream())
            {
                using (var bs = new MemoryStream(b))
                using (var z = new GZipStream(bs, CompressionMode.Decompress))
                    z.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    public enum CameraOptions
    {
        Rear,
        Front
    }

    public interface ICameraOption
    {
        /// <summary>
        /// Método é chamado quando uma imagem é capturada por algum dispositivo
        /// </summary>
        /// <param name="bytes">A imagem em array de byte</param>
        void ImagemCapturada(byte[] bytes);
    }
}
