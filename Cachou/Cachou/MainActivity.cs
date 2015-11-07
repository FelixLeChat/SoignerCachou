using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;

namespace Cachou
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private static ImageView _cachouImageView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CachouMain);

            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            seekBar.ProgressChanged += ChangeCachouMood;

            _cachouImageView = FindViewById<ImageView>(Resource.Id.imageViewCachou);

            // Get our button from the layout resource,
            // and attach an event to it
            /*Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {ChangeToMainView(); };*/
        }

        private void ChangeToMainView()
        {
            SetContentView(Resource.Layout.CachouMain);
        }

        public static void ChangeCachouMood(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            int div = e.Progress/25;

            switch (div)
            {
                case 0:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou);
                    break;
                case 1:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_Confu);
                    break;
                case 2:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_happy);
                    break;
                case 3:
                case 4:
                    _cachouImageView.SetImageResource(Resource.Drawable.Cachou_triste);
                    break;
            }

        }
    }
}

