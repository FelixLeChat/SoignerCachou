using Android.App;
using Android.Content;
using Android.Views;

class ToolTouchListener : Activity, View.IOnTouchListener
{
    public bool OnLongClick(View v)
    {
        View.DragShadowBuilder shadowBuilder = new ToolShadowBuilder(v);

        ClipData clipData = ClipData.NewPlainText(v.Id.ToString(), "Outil");

        v.StartDrag(clipData, shadowBuilder, v, 0);

        return true;
    }

    public bool OnTouch(View v, MotionEvent e)
    {
        if (e.Action == MotionEventActions.Down)
        {
            View.DragShadowBuilder shadowBuilder = new ToolShadowBuilder(v);

            ClipData clipData = ClipData.NewPlainText(v.Id.ToString(), "Outil");

            v.StartDrag(clipData, shadowBuilder, v, 0);

            return true;
        }
        else
        {
            return false;
        }
    }
}