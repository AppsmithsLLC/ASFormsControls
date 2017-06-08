package md54d709e4996ae11bf690d247baa333abc;


public class CheckBoxViewRenderer_ClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("ASFormsControls.Droid.Renderers.CheckBoxViewRenderer+ClickListener, ASFormsControls.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CheckBoxViewRenderer_ClickListener.class, __md_methods);
	}


	public CheckBoxViewRenderer_ClickListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CheckBoxViewRenderer_ClickListener.class)
			mono.android.TypeManager.Activate ("ASFormsControls.Droid.Renderers.CheckBoxViewRenderer+ClickListener, ASFormsControls.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

	private java.util.ArrayList refList;
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
