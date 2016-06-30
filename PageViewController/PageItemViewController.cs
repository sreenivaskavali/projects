using System;

using UIKit;

namespace PageViewController
{
	public partial class PageItemViewController : UIViewController
	{
		public int Index = 0;

		public string ImageName;

		public PageItemViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if(!string.IsNullOrEmpty(ImageName))
			  this.imgView.Image = UIImage.FromBundle(ImageName);

			// pich on image
			scrollView.MaximumZoomScale = 3f;
			scrollView.MinimumZoomScale = 1f;
			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => { return imgView; };
			scrollView.ShowsVerticalScrollIndicator = false;
			scrollView.ShowsVerticalScrollIndicator = false;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


