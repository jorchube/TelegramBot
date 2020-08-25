using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface TelegramBotRunnerInterface
    {
        public void Start(TelegramBotInterface bot);
        
        public void Stop();
    }
}
