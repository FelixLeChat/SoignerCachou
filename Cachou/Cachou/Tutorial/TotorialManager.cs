using System;
using Android.Media;
using Android.Views;
using Android.Widget;

namespace Cachou.Tutorial
{
    class TotorialManager
    {
        private MediaPlayer _player = new MediaPlayer();
        private int _selectedNurse;
        private MainActivity _mainActivity;
        private int _step = 0;

        public void StartTutorial(MainActivity activity)
        {
            _mainActivity = activity;

            // hide Tools for tuto
            _mainActivity.HideTools();
            _mainActivity.HideScroll();

            //TODO : put Bed Cachou
            _mainActivity.SetCachouImg(Resource.Drawable.Cachou);

            // start audio 1
            PlayAudio(_mainActivity, Resource.Raw.audio1);

            // Add event on nurse click
            _mainActivity.FindViewById<ImageView>(Resource.Id.bear1).Click += OnCompletion;
            _mainActivity.FindViewById<ImageView>(Resource.Id.bear2).Click += OnCompletion;
            _mainActivity.FindViewById<ImageView>(Resource.Id.bear3).Click += OnCompletion;

            _mainActivity.FindViewById<ImageView>(Resource.Id.fox1).Click += OnCompletion;
            _mainActivity.FindViewById<ImageView>(Resource.Id.fox2).Click += OnCompletion;

            _mainActivity.FindViewById<ImageView>(Resource.Id.girafe1).Click += OnCompletion;
            _mainActivity.FindViewById<ImageView>(Resource.Id.girafe2).Click += OnCompletion;

            _mainActivity.FindViewById<ImageView>(Resource.Id.panda1).Click += OnCompletion;
            _mainActivity.FindViewById<ImageView>(Resource.Id.panda2).Click += OnCompletion;
        }

        public void PlayAudio(MainActivity act,int ress)
        {
            _player = MediaPlayer.Create(act, ress);
            _player.Completion += OnCompletion;
            _player.Start();
        }

        public void OnCompletion(object obj,EventArgs e)
        {
            switch (_step)
            {
                case 0:
                    // only show tools
                    _mainActivity.ShowTools();
                    _step++;
                    break;
                case 1:
                    if (e is View.DragEventArgs)
                    {
                        if (obj is ImageView)
                        {
                            var id = ((ImageView) obj).Id;
                            if (id == Resource.Id.outil4)
                            {
                                PlayAudio(_mainActivity, Resource.Raw.audio2);
                                _step++;
                            }
                        }
                    }
                    break;
                case 2:
                    PlayAudio(_mainActivity, Resource.Raw.audio3);
                    _mainActivity.HideTools();
                    _mainActivity.ShowNurse();
                    _step++;
                    break;

                case 3:
                    if (obj is ImageView)
                    {
                        PlayAudio(_mainActivity, Resource.Raw.audio4);
                        _mainActivity.HideNurse();
                        _mainActivity.ShowColors();
                        var idToPut = ((ImageView) obj).Id;
                        var res = Resource.Drawable.bear;

                        switch (idToPut)
                        {
                            case Resource.Id.girafe1:
                            case Resource.Id.girafe2:
                                res = Resource.Drawable.girafe;
                                break;

                            case Resource.Id.bear1:
                            case Resource.Id.bear2:
                            case Resource.Id.bear3:
                                res = Resource.Drawable.bear;
                                break;

                            case Resource.Id.panda1:
                            case Resource.Id.panda2:
                                res = Resource.Drawable.panda;
                                break;

                            case Resource.Id.fox1:
                            case Resource.Id.fox2:
                                res = Resource.Drawable.fox;
                                break;
                        }
                        // set nurse tutorial
                        _selectedNurse = res;
                        _mainActivity.FindViewById<ImageView>(Resource.Id.to_color).SetImageResource(res);

                        _mainActivity.FindViewById<ImageView>(Resource.Id.coloring).Click += OnCompletion;
                        _step++;
                    }
                    break;

                case 4:
                    if (obj is ImageView)
                    {
                        _mainActivity.HideColors();
                        PlayAudio(_mainActivity, Resource.Raw.audio5);

                        // Trigger animations
                        //_mainActivity.Find ViewById<ImageView>(Resource.Id.imageViewCachou).SetImageResource(Resource.Drawable);
                        // trigger real game
                        _step++;
                    }
                    break;
                case 5:
                    if (obj is MediaPlayer)
                    {
                        PlayAudio(_mainActivity, Resource.Raw.audio6);

                        _mainActivity.ShowScroll();
                        _mainActivity.ShowTools();
                        _mainActivity.SetNurse(_selectedNurse);
                        _step++;
                    }
                    break;
            }
        }
    }
}