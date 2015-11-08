using System;
using Android.Media;
using Android.Views;
using Android.Widget;

namespace Cachou.Tutorial
{
    class TotorialManager
    {
        private MediaPlayer _player = new MediaPlayer();
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
                    _mainActivity.ShowNurse();
                    _step++;
                    break;
            }
        }
    }
}