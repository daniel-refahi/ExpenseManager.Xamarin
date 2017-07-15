using System;
using System.Collections.Generic;
using System.Globalization;
using UIKit;
using System.Linq;

namespace ExpenseManager.ios.ListSources
{

    public class SettingEventArgument: EventArgs
    {
        public int SelectedSettins { get; set; }
    }

	public class MonthSelectorSource : UIPickerViewModel
	{
        List<string> _months;
        int _selectedIndex = 0;
        public EventHandler SelectedChanged;
        public bool DisbaleSelectEvent = true;

		public MonthSelectorSource(int selectedIndex)
        {
            _selectedIndex = selectedIndex;
            _months = DateTimeFormatInfo.CurrentInfo.MonthNames.Where(m=> m != "").ToList();
		}

		public string SelectedItem
		{
			get { return _months[_selectedIndex]; }
		}

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView picker, nint component)
		{
			return _months.Count;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component)
		{
			return _months[(int)row];
		}

		public override void Selected(UIPickerView picker, nint row, nint component)
		{
			_selectedIndex = (int)row;
			if (DisbaleSelectEvent)
				return;

			SelectedChanged.Invoke(this, new SettingEventArgument()
			{
			    SelectedSettins = _selectedIndex + 1
			});
		}
	}

	public class YearSelectorSource : UIPickerViewModel
	{
        List<int> _years;
        int _selectedIndex = 0;
        public EventHandler SelectedChanged;
        public bool DisbaleSelectEvent = true;
		public YearSelectorSource(List<int> years, int selectedIndex)
		{
            _years = years;
            _selectedIndex = selectedIndex;
        }

		public int SelectedItem
		{
			get { return _years[_selectedIndex]; }
		}

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView picker, nint component)
		{
			return _years.Count;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component)
		{
            return _years[(int)row].ToString();
		}

		public override void Selected(UIPickerView picker, nint row, nint component)
		{
			_selectedIndex = (int)row;
            if (DisbaleSelectEvent)
                return;
            
            SelectedChanged.Invoke(this, new SettingEventArgument()
            {
                SelectedSettins = _years[_selectedIndex]
			});
		}
	}
}
