using System;
using System.Drawing;
using UIKit;

namespace ExpenseManager.ios.ListCells
{
	public class CategoryListCell : UITableViewCell
	{
        UILabel categoryNameLabel;
        UILabel totalExpenses;
        UILabel totalExpensesLabel;

		CategoriesController parent;
		int categoryId;
        public CategoryListCell(string cellId, CategoriesController parent): base (UITableViewCellStyle.Default, cellId)
        {
			SelectionStyle = UITableViewCellSelectionStyle.Default;
            this.parent = parent;

			categoryNameLabel = new UILabel()
			{
				Font = UIFont.FromName("Cochin-BoldItalic", 18f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

            totalExpenses = new UILabel()
			{
				Font = UIFont.FromName("Cochin-BoldItalic", 12f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};

			totalExpensesLabel = new UILabel()
			{
				Font = UIFont.FromName("Cochin-BoldItalic", 12f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear,
                Text = "Total Expenses:"
			};

            SetEvents();

			ContentView.Add(categoryNameLabel);
            ContentView.Add(totalExpenses);
            ContentView.Add(totalExpensesLabel);
		}

        void SetEvents()
        {
			var categoryNameGesture = new UITapGestureRecognizer(() =>
			{
				parent.EditCategoryClicked(categoryId, this);
			});
            this.UserInteractionEnabled = true;
            this.AddGestureRecognizer(categoryNameGesture);
        }

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			categoryNameLabel.Frame = new RectangleF(40, 15, 163, 30);
            totalExpensesLabel.Frame = new RectangleF((float)ContentView.Bounds.Width - 150, 16, 150, 30); 
            totalExpenses.Frame = new RectangleF((float)ContentView.Bounds.Width - 65, 17, 150, 30);
		}

        public void UpdateCell(int categoryId, string categoryName, double totalExpenses)
		{
            this.categoryId = categoryId;
            categoryNameLabel.Text = categoryName;
            this.totalExpenses.Text = totalExpenses.ToString("C0");
		}
	}
}
