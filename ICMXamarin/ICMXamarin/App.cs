<<<<<<< HEAD
﻿using ICMXamarin.Model;
using ICMXamarin.Model.Api;
=======
﻿using ICMXamarin.Model.Api;
using ICMXamarin.Model.Camera.View;
>>>>>>> 0102da930061a4d2b0d7b564527f2110beffa057
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
            ComputerVision.DownoadHttp();
            //SaveAndLoad.Carregar("ms-appx//Assets/futebol.jpg").Wait();
=======
            MainPage = new CameraPage();
            ComputerVision.MakeRequest();
>>>>>>> 0102da930061a4d2b0d7b564527f2110beffa057
		}

        public static ISaveAndLoad SaveAndLoad { get; set; }

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
