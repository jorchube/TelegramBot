using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class UpdateMessage
    {
        public UpdateMessage()
        { 
        
        }

        public UpdateMessage(long id, Message message)
        {
            this.update_id = id;
            this.message = message;
        }

        public long update_id { get; set; }
        public Message message { get; set; }

        public override bool Equals(object obj)
        {
            UpdateMessage other = obj as UpdateMessage;

            if (other == null) {
                return false;
            }

            if (update_id != other.update_id) {
                return false;
            }

            return message.Equals(other.message);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(update_id);
        }

        public class Message
        {
            public Message()
            { 

            }

            public Message(long id, int date, string text, User from, Chat chat)
            {
                this.message_id = id;
                this.date = date;
                this.text = text;
                this.from = from;
                this.chat = chat;
            }

            public long message_id { get; set; }
            public int date { get; set; }
            public string text { get; set; }
            public User from { get; set; }
            public Chat chat { get; set; }

            public override bool Equals(object obj)
            {
                Message other = obj as Message;

                if (other == null) {
                    return false;
                }

                return message_id == other.message_id &&
                    date == other.date &&
                    text == other.text &&
                    from.Equals(other.from) &&
                    chat.Equals(other.chat);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(message_id, date, text, from, chat);
            }
        }

        public class User
        {
            public User()
            {
                username = "";
            }

            public User(long id, string first_name, string last_name, string username = "")
            {
                this.id = id;
                this.first_name = first_name;
                this.last_name = last_name;
                this.username = username;
            }

            public long id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string username { get; set; }

            public override bool Equals(object obj)
            {
                User other = obj as User;

                if (other == null) {
                    return false;
                }

                return id == other.id &&
                    first_name == other.first_name &&
                    last_name == other.last_name &&
                    username == other.username;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(id);
            }
        }

        public class Chat
        {
            public Chat()
            {
            }

            public Chat(long id)
            {
                this.id = id;
            }

            public long id { get; set; }

            public string type { get; set; }

            public override bool Equals(object obj)
            {
                Chat other = obj as Chat;

                if (other == null) {
                    return false;
                }

                return id == other.id;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(id);
            }
        }
    }

}
