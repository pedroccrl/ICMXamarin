using ICMXamarin.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using ICMXamarin.Views;
using Windows.UI.Xaml.Controls;
using Windows.Media.SpeechSynthesis;

[assembly: ExportRenderer(typeof(ICMXamarin.Views.CameraPage), typeof(CameraPageRenderer))]
namespace ICMXamarin.UWP
{
    public class CameraPageRenderer : ViewRenderer<Views.CameraPage, Windows.UI.Xaml.Controls.Grid>
    {
        Frame frame;
        Fala fala;
        MediaElement media;
        Grid grid;
        SpeechSynthesizer speech;
        protected override void OnElementChanged(ElementChangedEventArgs<CameraPage> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                grid = new Grid();
                fala = new Fala();
                media = new MediaElement();
                frame = new Frame();
                speech = new SpeechSynthesizer();

                frame.Navigate(typeof(CameraManualControls.MainPage), fala);

                grid.Children.Add(frame);
                grid.Children.Add(media);

                fala.ProntoParaFalar += async (s, ev) =>
                {
                    var stream = await speech.SynthesizeTextToStreamAsync(ev.Mensagem);
                    media.SetSource(stream, stream.ContentType);
                    media.Play();
                };

                SetNativeControl(grid);
            }
            
        }
    }
}
