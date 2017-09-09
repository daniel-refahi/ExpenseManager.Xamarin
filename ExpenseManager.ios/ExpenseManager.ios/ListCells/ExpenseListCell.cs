using System;
using System.Drawing;
using UIKit;

namespace ExpenseManager.ios.ListCells
{
	public class ExpenseListCell : UITableViewCell
	{
        UILabel _valueLable;
        UILabel _descriptionLable;
        UILabel _expenseDate;
		//UIImageView Avatar;
        ExpensesController _parent;
        bool _showEditBtn;
        int _expenseId;

        public ExpenseListCell(string cellId, UITableViewController parent): base (UITableViewCellStyle.Default, cellId)
        {
			SelectionStyle = UITableViewCellSelectionStyle.Default;
            if (parent is ExpensesController)
            {
                _parent = (ExpensesController)parent;
                _showEditBtn = true;
            }
            else
                _showEditBtn = false;

			// setting background color of each cell 
			//ContentView.BackgroundColor = UIColor.FromRGB(0, 114, 253);
			//Avatar = new UIImageView();
			_valueLable = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 18f),
                TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

			_descriptionLable = new UILabel()
			{
				Font = UIFont.FromName("Cochin-BoldItalic", 12f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};

            _expenseDate = new UILabel()
			{
				Font = UIFont.FromName("Cochin-BoldItalic", 12f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};

			ContentView.Add(_valueLable);
			ContentView.Add(_descriptionLable);
            ContentView.Add(_expenseDate);
            //ContentView.Add(Avatar);
            if (_showEditBtn)
                SetEvents();
		}

		void SetEvents()
		{
			var categoryNameGesture = new UITapGestureRecognizer(() =>
			{
                _parent.EditExpenseClicked(_expenseId, this);
			});
			this.UserInteractionEnabled = true;
			this.AddGestureRecognizer(categoryNameGesture);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			//Avatar.Frame = new RectangleF(20, 5, 30, 40);
			_valueLable.Frame = new RectangleF(40, 15, 163, 30);
			_descriptionLable.Frame = new RectangleF((float)ContentView.Bounds.Width - 250, 17, 150, 30);			
            _expenseDate.Frame = new RectangleF((float)ContentView.Bounds.Width - 80, 17, 150, 30);
		}

		//public void UpdateCell(string name, int salary, UIImage avatar, int employeeId)
		//{
		//	ImageView.Image = avatar;
		//	NameLable.Text = name;
		//	SalarayLable.Text = salary.ToString("C0");
		//	EmployeeId = employeeId;
		//}

        public void UpdateCell(int expenseId, double value, string description, DateTime expenseDate)
		{
            //ImageView.Image = avatar;
            _expenseId = expenseId;
            _valueLable.Text = value.ToString("C0");
            _descriptionLable.Text = description;
            _expenseDate.Text = expenseDate.ToString("ddd - dd MMM");

		}
	}
}
