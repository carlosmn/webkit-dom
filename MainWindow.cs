using System;
using Gtk;
using WebKit;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		var webView = new WebKit.WebView();
		var scrolled = new ScrolledWindow();
		var v = new VPaned();

		scrolled.Add(webView);
		v.Pack1(scrolled, true, true);
		scrolled.SetSizeRequest(-1, 50);

		var button = new Button("foo");
		v.Pack2(button, true, true);

		this.Add(v);
		this.ShowAll();

		webView.LoadString("<p>foo</p>", "text/html", "utf-8", null);

		// This won't show up until we've returned from the constructor
		// so let's do something easy that can happen after the window shows
		button.Clicked += (object sender, EventArgs e) => {
			var document = webView.DomDocument;
			var first = document.FirstChild;
			var body = document.GetElementsByTagName("body").Item(0);
			var para = document.CreateElement("p");
			para.AppendChild(document.CreateTextNode("this is some text"));
			body.AppendChild(para);
		};
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
