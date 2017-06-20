using System;
using System.Drawing;
using ExpenseManager.ios.Controllers;
using UIKit;

namespace ExpenseManager.ios.ListCells
{
	public class ExpenseListCell : UITableViewCell
	{
        UILabel valueLable;
        UILabel descriptionLable;
		//UIImageView Avatar;
		UIButton EditBtn;
        ExpensesController Parent;
		int ExpenseId;
        public ExpenseListCell(string cellId, ExpensesController parent): base (UITableViewCellStyle.Default, cellId)
        {
			SelectionStyle = UITableViewCellSelectionStyle.Default;
			Parent = parent;

			// setting background color of each cell 
			//ContentView.BackgroundColor = UIColor.FromRGB(0, 114, 253);
			//Avatar = new UIImageView();
			valueLable = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 18f),
                TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

			descriptionLable = new UILabel()
			{
				Font = UIFont.FromName("Cochin-BoldItalic", 12f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};

			EditBtn = UIButton.FromType(UIButtonType.RoundedRect);
			EditBtn.SetTitle("Edit", UIControlState.Normal);
			//EditBtn.SetTitle("Disabled", UIControlState.Disabled);
			//EditBtn.SetTitleColor(UIColor.LightGray, UIControlState.Disabled);
			EditBtn.TouchUpInside += EditBtnClicked;

			ContentView.Add(valueLable);
			ContentView.Add(descriptionLable);
			//ContentView.Add(Avatar);
			ContentView.Add(EditBtn);
		}

		private void EditBtnClicked(object sender, EventArgs e)
		{
            Parent.EditExpenseClicked(ExpenseId, this);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			//Avatar.Frame = new RectangleF(20, 5, 30, 40);
			valueLable.Frame = new RectangleF(40, 15, 163, 30);
			descriptionLable.Frame = new RectangleF((float)ContentView.Bounds.Width - 250, 17, 150, 30);
			EditBtn.Frame = new RectangleF((float)ContentView.Bounds.Width - 100, 17, 80, 30);
		}



		//public void UpdateCell(string name, int salary, UIImage avatar, int employeeId)
		//{
		//	ImageView.Image = avatar;
		//	NameLable.Text = name;
		//	SalarayLable.Text = salary.ToString("C0");
		//	EmployeeId = employeeId;
		//}

        public void UpdateCell(int expenseId, double value, string description)
		{
            //ImageView.Image = avatar;
            ExpenseId = expenseId;
            valueLable.Text = value.ToString("C0");
            descriptionLable.Text = description;
		}
	}
}
