using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface TelegramBotInterface
    {
        public void Start();
        public void Stop();
        public List<TelegramBotUpdate> GetUpdates();
        public void HandleUpdate(TelegramBotUpdate update);
    }
}
