using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

class ToolShadowBuilder : View.DragShadowBuilder
{
    private readonly Drawable _shadow;

    public ToolShadowBuilder(View v) : base(v)
    {
        // Nous permettons l'utilisation d'une cache pour dessiner
        // L'ombre de notre icône
        v.DrawingCacheEnabled = true;
        Bitmap bm = v.DrawingCache;
        _shadow = new BitmapDrawable(bm);

        // L'ombre devient un genre de gris
        _shadow.SetColorFilter(Color.ParseColor("#4EB1FB"), PorterDuff.Mode.Multiply);
    }

    public override void OnProvideShadowMetrics(Point size, Point touch)
    {
        // Nous prenons les dimensions de notre image
        int width = View.Width;
        int height = View.Height;
        // Nous créons les dimensions de l'ombre
        _shadow.SetBounds(0, 0, width, height);
        size.Set(width, height);

        touch.Set(width / 2, height / 2);
    }

    public override void OnDrawShadow(Canvas canvas)
    {
        // Nous dessinons l'ombre de l'image
        base.OnDrawShadow(canvas);
        _shadow.Draw(canvas);
    }
}