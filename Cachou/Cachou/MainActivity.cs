using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;

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
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate { ChangeToMainView(); };
        }

        private void ChangeToMainView()
        {
            SetContentView(Resource.Layout.CachouMain);
            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            seekBar.ProgressChanged += ChangeCachouMood;

            _cachouImageView = FindViewById<ImageView>(Resource.Id.imageViewCachou);
        }

        private class ToolShadowBuilder : View.DragShadowBuilder
        {
            private Drawable shadow;

            public ToolShadowBuilder(View v) : base(v)
            {
                // Nous permettons l'utilisation d'une cache pour dessiner
                // L'ombre de notre icône
                v.DrawingCacheEnabled = true;
                Bitmap bm = v.DrawingCache;
                shadow = new BitmapDrawable(bm);

                // L'ombre devient un genre de gris
                shadow.SetColorFilter(Color.ParseColor("#4EB1FB"), PorterDuff.Mode.Multiply);
            }

            public override void OnProvideShadowMetrics(Point size, Point touch)
            {
                // Nous prenons les dimensions de notre image
                int width = View.Width;
                int height = View.Height;
                // Nous créons les dimensions de l'ombre
                shadow.SetBounds(0, 0, width, height);
                size.Set(width, height);

                touch.Set(width / 2, height / 2);
            }

            public override void OnDrawShadow(Canvas canvas)
            {
                // Nous dessinons l'ombre de l'image
                base.OnDrawShadow(canvas);
                shadow.Draw(canvas);
            }
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

