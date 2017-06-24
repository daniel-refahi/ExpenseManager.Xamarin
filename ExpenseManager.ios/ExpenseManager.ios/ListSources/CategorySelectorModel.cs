using System;
using System.Collections.Generic;
using UIKit;

namespace ExpenseManager.ios.ListSources
{
    public class CategorySelectorModel: UIPickerViewModel
    {
        public CategorySelectorModel()
        {
        }

        List<string> categories;
		protected int selectedIndex = 0;

		public CategorySelectorModel(List<string> items)
		{
			categories = items;
		}

		public string SelectedItem
		{
			get { return categories[selectedIndex]; }
		}

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView picker, nint component)
		{
			return categories.Count;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component)
		{
            return categories[ (int)row];
		}

		public override void Selected(UIPickerView picker, nint row, nint component)
		{
			selectedIndex = (int)row;
		}
    }
}
