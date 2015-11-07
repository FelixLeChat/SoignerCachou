package md5eeff201f80def86f2b2d6f752f971207;


public class MainActivity_ToolShadowBuilder
	extends android.view.View.DragShadowBuilder
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onProvideShadowMetrics:(Landroid/graphics/Point;Landroid/graphics/Point;)V:GetOnProvideShadowMetrics_Landroid_graphics_Point_Landroid_graphics_Point_Handler\n" +
			"n_onDrawShadow:(Landroid/graphics/Canvas;)V:GetOnDrawShadow_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("Cachou.MainActivity/ToolShadowBuilder, Cachou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_ToolShadowBuilder.class, __md_methods);
	}


	public MainActivity_ToolShadowBuilder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_ToolShadowBuilder.class)
			mono.android.TypeManager.Activate ("Cachou.MainActivity/ToolShadowBuilder, Cachou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public MainActivity_ToolShadowBuilder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MainActivity_ToolShadowBuilder.class)
			mono.android.TypeManager.Activate ("Cachou.MainActivity/ToolShadowBuilder, Cachou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onProvideShadowMetrics (android.graphics.Point p0, android.graphics.Point p1)
	{
		n_onProvideShadowMetrics (p0, p1);
	}

	private native void n_onProvideShadowMetrics (android.graphics.Point p0, android.graphics.Point p1);


	public void onDrawShadow (android.graphics.Canvas p0)
	{
		n_onDrawShadow (p0);
	}

	private native void n_onDrawShadow (android.graphics.Canvas p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
