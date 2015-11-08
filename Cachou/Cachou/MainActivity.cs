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
            handleSeekBarDrag(_seekBar);

            _cachouImageView = FindViewById<ImageView>(Resource.Id.imageViewCachou);

            // Hide nurse
            HideNurse();

            // Add all tools to list
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil1));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil2));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil3));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil4));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil5));
            _tools.Add(FindViewById<ImageView>(Resource.Id.outil6));

            AddEventOnDrag();
        }

        private void handleSeekBarDrag(SeekBar seekBar)
        {
            seekBar.Drag += (s, args) =>
            {
                //Si on relache la barre
                if (args.Event.Action == DragAction.Exited)
                {
                    ChangeCachouMood(seekBar, true);
                }
                else
                {
                    ChangeCachouMood(seekBar);
                }
            };
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

        public static void ChangeCachouMood(SeekBar seekBar, bool sendServer = false)
        {
            int div = seekBar.Progress/25;

            switch (div)
            {
                case 0:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou);
                    if (sendServer)
                    {
                        WebmessageSender.postServer("Justine dit que Cachou est normal.");
                    }
                    break;
                case 1:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_Confu);
                    if (sendServer)
                    {
                        WebmessageSender.postServer("Justine dit que Cachou est confu.");
                    }
                    break;
                case 2:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_happy);
                    if (sendServer)
                    {
                        WebmessageSender.postServer("Justine dit que Cachou est content.");
                    }
                    break;
                case 3:
                case 4:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_triste);
                    if (sendServer)
                    {
                        WebmessageSender.postServer("Justine dit que Cachou est triste.");
                    }
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
            FindViewById<ImageView>(Resource.Id.choose_nurse).Visibility = ViewStates.Invisible;
        }

        public void ShowNurse()
        {
            var imageview = FindViewById<ImageView>(Resource.Id.choose_nurse);
            imageview.BringToFront();
            imageview.Visibility = ViewStates.Visible;
        }
    }
}

