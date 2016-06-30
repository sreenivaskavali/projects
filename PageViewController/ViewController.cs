using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace PageViewController
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public List<string> contentImages = null;
		UIStoryboard storyboardToUse = null;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			try
			{
				storyboardToUse = UIStoryboard.FromName("Main", null);
				contentImages = new List<string>() { "scenery1.jpg", "scenery2.jpg", "scenery3.jpg", "scenery4.jpg", "scenery5.jpg" };

				var pageViewController = (PageViewController)storyboardToUse.InstantiateViewController("PageViewControllerSBID");
				pageViewController.DataSource = new PageDataSource(this);

				var firstView = GetItemController(0);
				pageViewController.SetViewControllers(new[] { firstView }, UIPageViewControllerNavigationDirection.Forward, true, null);

				this.AddChildViewController(pageViewController);
				this.View.AddSubview(pageViewController.View);
				pageViewController.DidMoveToParentViewController(this);

				SetupPageControl();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}


		private void SetupPageControl()
		{
			var appearance = UIPageControl.Appearance;
			appearance.PageIndicatorTintColor = UIColor.Gray;
			appearance.CurrentPageIndicatorTintColor = UIColor.White;
			appearance.BackgroundColor = UIColor.DarkGray;
		}


		public PageItemViewController GetItemController(int index)
		{

			if (index < contentImages.Count)
			{
				var pageItemController = (PageItemViewController)storyboardToUse.InstantiateViewController("PageItemViewControllerSBID");

				pageItemController.Index = index;
				pageItemController.ImageName = contentImages[index];
				return pageItemController;
			}

			return null;
		}
	}


	public class PageDataSource : UIPageViewControllerDataSource
	{

		ViewController controller = null;

		public PageDataSource(ViewController controller)
		{
			this.controller = controller;
		}

		override public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentPage = referenceViewController as PageItemViewController;

			if (currentPage.Index > 0 ){
				
				return controller.GetItemController(currentPage.Index - 1);
		    }

			return null;
		}

		override public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var itemController = referenceViewController as PageItemViewController;
			if (itemController.Index + 1 < controller.contentImages.Count)
			{
				return controller.GetItemController(itemController.Index + 1);
			}

			return null;
		}	

		public override nint GetPresentationCount(UIPageViewController pageViewController)
		{
			return controller.contentImages.Count;
		}

		public override nint GetPresentationIndex(UIPageViewController pageViewController)
		{
			return 0;
		}
	}
}

