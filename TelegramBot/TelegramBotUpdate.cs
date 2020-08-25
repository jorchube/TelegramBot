using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class TelegramBotUpdate
    {
        public TelegramBotUpdate(long id, Message message)
        {
            this.id = id;
            this.message = message;
        }

        public long id { get; }
        public Message message { get; }

        public class Message
        {
            public Message(long id, int date, string text, User from, Chat chat)
            {
                this.id = id;
                this.date = date;
                this.text = text;
                this.from = from;
                this.chat = chat;
            }

            public long id { get; }
            public int date { get; }
            public string text { get; }
            public User from { get; }
            public Chat chat { get; }
        }

        public class User
        {
            public User(long id, string first_name, string last_name, string username)
            {
                this.id = id;
                this.first_name = first_name;
                this.last_name = last_name;
                this.username = username;
            }

            public long id { get; }
            public string first_name { get; }
            public string last_name { get; }
            public string username { get; }
        }

        public class Chat
        {
            public Chat(long id)
            {
                this.id = id;
            }

            public long id { get; }
        }
    }

}
