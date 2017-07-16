using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ExpenseManager.ios.Utilities
{
	public class ImageInfo
	{
		#region Computed Properties
		public string ImageFilename { get; set; }
		public string Title { get; set; }
		public bool CanSelect { get; set; }
		#endregion

		#region Constructors
		public ImageInfo(string filename, string title, bool canSelect)
		{
			// Initialize
			this.ImageFilename = filename;
			this.Title = title;
			this.CanSelect = canSelect;
		}
		#endregion
	}

	public static class ImageUtilities
	{
		public static UIImage ResizeImage(UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContext(new SizeF(width, height));
			sourceImage.Draw(new RectangleF(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}
	}

	public class ImageViewDelegate : UICollectionViewDelegateFlowLayout
	{
		#region Application Access
		public static AppDelegate App
		{
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}
		#endregion

		#region Constructors
		public ImageViewDelegate()
		{
		}
		#endregion

		#region Override Methods
		public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
		{
			return new CGSize(50, 50);
		}

		//public override bool CanFocusItem(UICollectionView collectionView, NSIndexPath indexPath)
		//{
		//	if (indexPath == null)
		//	{
		//		return false;
		//	}
		//	else
		//	{
		//		var controller = collectionView as ImageCollectionView;
		//		return controller.Source.Images[indexPath.Row].CanSelect;
		//	}
		//}

		//public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		//{
		//	var controller = collectionView as ImageCollectionView;
		//	if (!controller.Source.Images[indexPath.Row].CanSelect)
		//		return;
		//	App.SelectedCell = controller.Source.Images[indexPath.Row];

		//	// Close Collection
		//	controller.ParentController.NavigationController.PopViewController(true);
		//}
		#endregion
	}
}
