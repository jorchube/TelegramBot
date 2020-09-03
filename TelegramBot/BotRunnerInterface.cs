using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface BotRunnerInterface
    {
        public void Start(BotInterface bot);
        
        public void Stop();
    }
}
