using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegramBotApp;

namespace TelegramBotTests
{
    public class BotRunnerTest
    {
        [SetUp]
        public void Setup()
        {
        
        }

        [Test]
        public void ItStartsAndStopsABot()
        {
            BotRunner runner = new BotRunner();
            BotStub bot = new BotStub();

            runner.Start(bot);

            Thread.Sleep(millisecondsTimeout: 100);
            runner.Stop();

            Assert.Greater(bot.get_updates_numcalls, 0);
            Assert.Greater(bot.handle_update_numcalls, 0);
        }

        class BotStub : BotInterface
        {
            public int get_updates_numcalls = 0;
            public int handle_update_numcalls = 0;

            public List<UpdateMessage> GetUpdates()
            {
                get_updates_numcalls++;

                Thread.Sleep(millisecondsTimeout: 10);

                return new List<UpdateMessage> { new UpdateMessage() };
            }

            public void HandleUpdate(UpdateMessage update)
            {
                handle_update_numcalls++;
            }

            public void Start()
            {
                
            }

            public void Stop()
            {
                
            }
        }
    }
}
