using ICMXamarin.Views;
using ICMXamarin.WinPhone;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Phone.UI.Input;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.WinRT;

[assembly: Xamarin.Forms.Dependency(typeof(CameraPreview))]
[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace ICMXamarin.WinPhone
{

    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, Windows.UI.Xaml.Controls.CaptureElement>
    {
        MediaCapture mediaCapture;
        CaptureElement PreviewControl;
        CameraOptions cameraOptions;
        Application app;
        bool isPreviewing = false;

        CameraPreview CamPreview;

        protected override void OnElementChanged(ElementChangedEventArgs<ICMXamarin.Views.CameraPreview> e)
        {
            base.OnElementChanged(e);
            
            if (Control == null)
            {
                app = Application.Current;
                app.Suspending += OnAppSuspending;
                app.Resuming += OnAppResuming;
                HardwareButtons.BackPressed += OnBackButtonPressed;

                cameraOptions = e.NewElement.Camera;
                PreviewControl = new CaptureElement();
                PreviewControl.Stretch = Stretch.UniformToFill;

                InitializeAsync();
                SetNativeControl(PreviewControl);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
                Tapped -= OnCameraPreviewTapped;
            }
            if (e.NewElement != null)
            {
                // Subscribe
                Tapped += OnCameraPreviewTapped;
                CamPreview = e.NewElement;
            }
        }

        async void OnCameraPreviewTapped(object sender, TappedRoutedEventArgs e)
        {
            var stream = new InMemoryRandomAccessStream();
            if (isPreviewing)
            {
                await mediaCapture.StopPreviewAsync();
                await mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);

                // transforma em bytes
                var reader = new DataReader(stream.GetInputStreamAt(0));
                var bytes = new byte[stream.Size];
                await reader.LoadAsync((uint)stream.Size);
                reader.ReadBytes(bytes);

                Xamarin.Forms.DependencyService.Get<ICameraOption>().ImagemCapturada(bytes);

                isPreviewing = false;
            }
            else
            {
                await mediaCapture.StartPreviewAsync();
                isPreviewing = true;
            }
        }


        async void InitializeAsync()
        {
            DeviceInformation camera = null;

            try
            {
                var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
                if (cameraOptions == CameraOptions.Rear)
                {
                    camera = devices.FirstOrDefault(c => c.EnclosureLocation != null && c.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back);
                }
                else
                {
                    camera = devices.FirstOrDefault(c => c.EnclosureLocation != null && c.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
                }

                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings
                {
                    VideoDeviceId = camera.Id,
                    AudioDeviceId = string.Empty,
                    StreamingCaptureMode = StreamingCaptureMode.Video,
                    PhotoCaptureSource = PhotoCaptureSource.Photo,
                });

                PreviewControl.Source = mediaCapture;
                await mediaCapture.StartPreviewAsync();
                isPreviewing = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"      ERROR: ", ex.Message);
            }
        }

        #region CICLO
        async void OnBackButtonPressed(object sender, BackPressedEventArgs e)
        {
            await CleanUpCaptureResourcesAsync();
        }
        async void OnAppSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await CleanUpCaptureResourcesAsync();
            deferral.Complete();
        }
        void OnAppResuming(object sender, object e)
        {
            InitializeAsync();
        }
        void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            HardwareButtons.BackPressed -= OnBackButtonPressed;
        }
        async Task CleanUpCaptureResourcesAsync()
        {
            if (isPreviewing && mediaCapture != null)
            {
                try
                {
                    await mediaCapture.StopPreviewAsync();
                    isPreviewing = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"      ERROR: ", ex.Message);
                }
            }

            if (mediaCapture != null)
            {
                if (PreviewControl != null)
                {
                    PreviewControl.Source = null;
                }

                try
                {
                    mediaCapture.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"      ERROR: ", ex.Message);
                }
            }
        }
        #endregion

    }

}
