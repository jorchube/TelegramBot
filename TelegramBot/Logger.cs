using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class Logger : LoggerInterface
    {
        string app_name;
        Action<string> log;
        Func<DateTime> datetime;

        public Logger(string app_name, Action<string> logger_callback, Func<DateTime> datetime_callback)
        {
            this.app_name = app_name;
            this.log = logger_callback;
            this.datetime = datetime_callback;
        }

        public void Log(string message_format, params object[] args)
        {
            DateTime datetime = this.datetime();

            string datetime_string = $"[{datetime:yyyy-MM-dd HH:mm:ss.fff}]";
            string message = string.Format(message_format, args);

            this.log($"{datetime_string} {app_name}: {message}");
        }
    }
}
