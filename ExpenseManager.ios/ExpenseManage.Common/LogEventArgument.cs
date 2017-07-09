using System;
namespace ExpenseManage.Common
{
	public class LogEventArgument
	{
		public string CallerName { get; set; }
		public string LogContent { get; set; }
		public LogType LogType { get; set; } = LogType.Info;
        public DateTime LogTime { get; set; } = DateTime.Now;
	}
}
