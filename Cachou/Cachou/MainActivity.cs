﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.IO;
using Cachou.Tutorial;
using Java.IO;
using Java.Net;
using File = System.IO.File;
using System;

namespace Cachou
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private static ImageView _cachouImageView;
        private readonly List<ImageView> _tools = new List<ImageView>();
        private SeekBar _seekBar;
        public static bool _tutorial = false;
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
            else
            {
                ShowScroll();
                SetNurse(Resource.Drawable.girafe);
            }
                

            //sendAudioFile();
        }

        private void ChangeToMainView()
        {
            // Starting tutorial
            SetContentView(Resource.Layout.CachouMain);
            _seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);

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

        private void handleSeekBarDrag(SeekBar seekBar)
        {
            seekBar.ProgressChanged += (s, args) =>
            {
                ChangeCachouMood(seekBar);
            };

            /*seekBar.StopTrackingTouch += (sender, args) =>
            {
                ChangeCachouMood(seekBar, true);
            };*/
        }

        private void HandleDrag(object obj, View.DragEventArgs e)
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
                    /*string image = e.Event.ClipData.GetItemAt(0).Text;

                    if (image == Resource.Id.imageViewCachou.ToString())
                    {
                        SetCachouImg(Resource.Drawable.fox);
                    }
                    if (image == "Outil")
                    {
                        SetCachouImg(Resource.Drawable.bear);
                    }*/

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

            int div = (seekBar.Progress%50)/12;

            switch (div)
            {
                case 0:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou);
                    /*if (sendServer)
                    {
                        WebmessageSender.postServer("Cachou est normal.");
                    }*/
                    break;
                case 1:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_Confu);
                    if (sendServer)
                    {
                        //WebmessageSender.postServer("Cachou est confu.");
                    }
                    break;
                case 2:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_triste);
                    if (sendServer)
                    {
                       //WebmessageSender.postServer("Cachou est content.");
                    }
                    break;
                default:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_happy);
                    if (sendServer)
                    {
                        //WebmessageSender.postServer("Cachou est triste.");
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
            _seekBar.Visibility = ViewStates.Gone;
        }

        public void ShowScroll()
        {
            _seekBar.Visibility = ViewStates.Visible;
            handleSeekBarDrag(_seekBar);
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
            FindViewById<LinearLayout>(Resource.Id.choose_nurse).Visibility = ViewStates.Gone;
        }

        public void ShowNurse()
        {
            var layout = FindViewById<LinearLayout>(Resource.Id.choose_nurse);
            layout.BringToFront();
            layout.Visibility = ViewStates.Visible;
        }

        public void ShowColors()
        {
            var layout = FindViewById<LinearLayout>(Resource.Id.coloring_nurse);
            layout.BringToFront();
            layout.Visibility = ViewStates.Visible;
        }

        public void HideColors()
        {
            FindViewById<LinearLayout>(Resource.Id.coloring_nurse).Visibility = ViewStates.Gone;
        }

        public void sendAudioFile()
        {
            //Resource.Raw.amy
            /*var stream = Assets.Open("amy.wav");
            WebAPI.WebAPI.SendAudioFile(convertAudioStreamtoByteArray(stream));*/
        }

        private byte[] convertAudioStreamtoByteArray(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
            return null;
        }

        public void SetNurse(int nurseId)
        {
            FindViewById<ImageView>(Resource.Id.nurse_layout).SetImageResource(nurseId);
        }
    }
}

