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
		//UIImageView Avatar;
        UIButton editBtn;

		CategoriesController parent;
		int categoryId;
        public CategoryListCell(string cellId, CategoriesController parent): base (UITableViewCellStyle.Default, cellId)
        {
			SelectionStyle = UITableViewCellSelectionStyle.Default;
            this.parent = parent;

			// setting background color of each cell 
			//ContentView.BackgroundColor = UIColor.FromRGB(0, 114, 253);
			//Avatar = new UIImageView();
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

			editBtn = UIButton.FromType(UIButtonType.RoundedRect);
			editBtn.TitleLabel.Font = UIFont.FromName("Cochin-BoldItalic", 26f);
			editBtn.SetTitle("...", UIControlState.Normal);
			//EditBtn.SetTitle("Disabled", UIControlState.Disabled);
			//EditBtn.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
			editBtn.TouchUpInside += EditBtnClicked;

			ContentView.Add(categoryNameLabel);
            ContentView.Add(totalExpenses);
            ContentView.Add(totalExpensesLabel);
			//ContentView.Add(Avatar);
			ContentView.Add(editBtn);
		}

		void EditBtnClicked(object sender, EventArgs e)
		{
            parent.EditCategoryClicked(categoryId, this);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			//Avatar.Frame = new RectangleF(20, 5, 30, 40);
			categoryNameLabel.Frame = new RectangleF(40, 15, 163, 30);
            totalExpensesLabel.Frame = new RectangleF(150, 17, 150, 30); 
            totalExpenses.Frame = new RectangleF(240, 18, 150, 30);
			editBtn.Frame = new RectangleF((float)ContentView.Bounds.Width - 100, 12, 80, 30);
		}



		//public void UpdateCell(string name, int salary, UIImage avatar, int employeeId)
		//{
		//  ImageView.Image = avatar;
		//  NameLable.Text = name;
		//  SalarayLable.Text = salary.ToString("C0");
		//  EmployeeId = employeeId;
		//}

        public void UpdateCell(int categoryId, string categoryName, double totalExpenses)
		{
			//ImageView.Image = avatar;
            this.categoryId = categoryId;
            categoryNameLabel.Text = categoryName;
            this.totalExpenses.Text = totalExpenses.ToString("C0");
		}
	}
}
