using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace Cachou
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private static ImageView _cachouImageView;
        private List<ImageView> _tools = new List<ImageView>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate { ChangeToMainView(); };
        }

        private void ChangeToMainView()
        {
            SetContentView(Resource.Layout.CachouMain);
            var seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            seekBar.ProgressChanged += ChangeCachouMood;

            _cachouImageView = FindViewById<ImageView>(Resource.Id.imageViewCachou);

            _tools.Add(FindViewById<ImageView>(Resource.Id.outil1));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil2));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil3));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil4));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil5));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil6));

            foreach (var imageView in _tools)
            {
                imageView.Drag += HandleDrag;
                imageView.SetOnTouchListener(new ToolTouchListener());
            }
        }

        private static void HandleDrag(object obj, View.DragEventArgs e)
        {
            switch (e.Event.Action)
            {
                case DragAction.Started:
                    e.Handled = true;
                    break;
                case DragAction.Entered:
                    e.Handled = true;
                    break;
                case DragAction.Exited:
                    e.Handled = true;
                    break;
                case DragAction.Drop:
                    e.Handled = true;
                    break;
                case DragAction.Ended:
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private static void toolDroppedOnCachou()
        {
            _cachouImageView.SetImageResource(Resource.Drawable.Cachou_Confu);
        }

        public static void ChangeCachouMood(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            int div = e.Progress/25;

            switch (div)
            {
                case 0:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou);
                    webMessageSender.postServer("Justine dit que Cachou est normal.");
                    break;
                case 1:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_Confu);
                    webMessageSender.postServer("Justine dit que Cachou est confu.");
                    break;
                case 2:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_happy);
                    webMessageSender.postServer("Justine dit que Cachou est content.");
                    break;
                case 3:
                case 4:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_triste);
                    webMessageSender.postServer("Justine dit que Cachou est triste.");
                    break;
            }

        }

        static class webMessageSender
        {
            public static void postServer(string message)
            {
                DateTime localDate = DateTime.Now;
                string culture = "en-GB";
                WebAPI.WebAPI.SendChildAction(localDate.ToString(culture), message);
            } 
        }
    }
}

