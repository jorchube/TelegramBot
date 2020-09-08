using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface LoggerInterface
    {
        public void Log(string message_format, params object[] args);
    }
}
