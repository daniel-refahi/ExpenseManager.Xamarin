using Foundation;
using System;
using UIKit;
using ExpenseManager.ios.Utilities;
using ExpenseManager.ios.ListSources;
using ExpenseManager.Repository.Repository;
using System.Collections.Generic;

namespace ExpenseManager.ios
{
    public partial class SettingController : UIViewController
    {
        Repository.Setting _appSettings;
        public SettingController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

            ParentViewController.Title = StaticValues.SettingsNavigationTitle;
            var repository = new RepositoryCore(CoreUtilities.GetLogService());
            _appSettings = repository.GetAppSettings();

            handleMonthSelector();
            handleYearSelector();
		}

        void MonthSelectorSource_SelectedChanged(object sender, EventArgs e)
        {
            _appSettings.CurrentMonth = ((SettingEventArgument)e).SelectedSettins;
            _appSettings.Upsert();
        }

        void handleMonthSelector()
        {
            var selectedIndex = _appSettings.CurrentMonth - 1;
			var monthSelectorSource = new MonthSelectorSource(selectedIndex);
			monthSelectorSource.SelectedChanged += MonthSelectorSource_SelectedChanged;
			Settings_MonthPicker.Model = monthSelectorSource;
			Settings_MonthPicker.Select(selectedIndex, 0, true);
			monthSelectorSource.DisbaleSelectEvent = false;
        }

        void handleYearSelector()
        {
			var years = new List<int>();
			int index = 0;
			var selectedIndex = 0;
			for (int year = 2015; year < 2025; year++)
			{
				if (year == _appSettings.CurrentYear)
					selectedIndex = index;
				years.Add(year);
				index++;
			}

			var yearSelectorSource = new YearSelectorSource(years, selectedIndex);
			yearSelectorSource.SelectedChanged += YearSelectorSource_SelectedChanged;
			Settings_YearPicker.Model = yearSelectorSource;
            Settings_YearPicker.Select(selectedIndex, 0, true);
            yearSelectorSource.DisbaleSelectEvent = false;
        }

        void YearSelectorSource_SelectedChanged(object sender, EventArgs e)
        {
            _appSettings.CurrentYear = ((SettingEventArgument)e).SelectedSettins;
            _appSettings.Upsert();
        }
    }
}