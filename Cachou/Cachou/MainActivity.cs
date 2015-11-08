﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Threading;
using Android.Media;
using Cachou.Tutorial;
using System;
using System.Globalization;

namespace Cachou
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private static ImageView _cachouImageView;
        private List<ImageView> _tools = new List<ImageView>();
        private SeekBar _seekBar;
        public static bool _tutorial = true;
        private static TotorialManager _tutorialManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate { ChangeToMainView(); };

            // debug 
            ChangeToMainView();

            if (_tutorial)
            {
                _tutorialManager = new TotorialManager();
                _tutorialManager.StartTutorial(this);
            }
        }

        private void ChangeToMainView()
        {
            // Starting tutorial
            SetContentView(Resource.Layout.CachouMain);
            _seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            _seekBar.ProgressChanged += ChangeCachouMood;

            _cachouImageView = FindViewById<ImageView>(Resource.Id.imageViewCachou);

            // Hide nurse
            HideNurse();

            // hide colors
            HideColors();

            // Add all tools to list
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil1));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil2));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil3));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil4));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil5));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil6));

            AddEventOnDrag();
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
                    if (_tutorial)
                        _tutorialManager.OnCompletion(obj, e);
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
                    WebmessageSender.postServer("Justine dit que Cachou est normal.");
                    break;
                case 1:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_Confu);
                    WebmessageSender.postServer("Justine dit que Cachou est confu.");
                    break;
                case 2:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_happy);
                    WebmessageSender.postServer("Justine dit que Cachou est content.");
                    break;
                case 3:
                case 4:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_triste);
                    WebmessageSender.postServer("Justine dit que Cachou est triste.");
                    break;
            }

        }

        public void HideTools()
        {
            foreach (var imageView in _tools)
            {
                imageView.Visibility = ViewStates.Invisible;
            }
        }

        public void ShowTools()
        {
            foreach (var imageView in _tools)
            {
                imageView.Visibility = ViewStates.Visible;
            }
        }

        public void HideScroll()
        {
            _seekBar.Visibility = ViewStates.Invisible;
        }

        public void ShowScroll()
        {
            _seekBar.Visibility = ViewStates.Visible;
        }

        public void AddEventOnDrag()
        {
            foreach (var imageView in _tools)
            {
                imageView.Drag += HandleDrag;
                imageView.SetOnTouchListener(new ToolTouchListener());
            }
        }

        public void SetCachouImg(int res)
        {
            _cachouImageView.SetImageResource(res);
        }

        public void HideNurse()
        {
            FindViewById<LinearLayout>(Resource.Id.choose_nurse).Visibility = ViewStates.Invisible;
        }

        public void ShowNurse()
        {
            var layout = FindViewById<LinearLayout>(Resource.Id.choose_nurse);
            layout.BringToFront();
            layout.Visibility = ViewStates.Visible;
        }

        public void ShowColors()
        {
            
        }

        public void HideColors()
        {
            
        }
    }
}

