<<<<<<< HEAD
<<<<<<< HEAD
﻿using ICMXamarin.Model;
using ICMXamarin.Model.Api;
=======
﻿using ICMXamarin.Model.Api;
using ICMXamarin.Model.Camera.View;
>>>>>>> 0102da930061a4d2b0d7b564527f2110beffa057
=======
﻿using ICMXamarin.Model.Api;
>>>>>>> parent of 7fbb69e... Acesso a API
using ICMXamarin.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ICMXamarin
{
	public class App : Application
	{
		public App ()
		{
            // The root page of your application
<<<<<<< HEAD
            MainPage = new FotoPage();
<<<<<<< HEAD
            ComputerVision.DownoadHttp();
            //SaveAndLoad.Carregar("ms-appx//Assets/futebol.jpg").Wait();
=======
            MainPage = new CameraPage();
            ComputerVision.MakeRequest();
>>>>>>> 0102da930061a4d2b0d7b564527f2110beffa057
=======
            ComputerVision.MakeRequest();
>>>>>>> parent of 7fbb69e... Acesso a API
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
