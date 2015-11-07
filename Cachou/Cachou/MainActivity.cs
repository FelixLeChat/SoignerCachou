﻿using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;

namespace Cachou
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {ChangeToMainView(); };
        }

        private void ChangeToMainView()
        {
            SetContentView(Resource.Layout.CachouMain);
        }
    }
}

