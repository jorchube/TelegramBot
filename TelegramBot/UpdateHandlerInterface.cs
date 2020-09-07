using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface UpdateHandlerInterface
    {
        public delegate void HandleUpdateCallback(OutgoingMessage response_message);

        public void HandleUpdate(UpdateMessage message, HandleUpdateCallback response_callback);
    }
}
