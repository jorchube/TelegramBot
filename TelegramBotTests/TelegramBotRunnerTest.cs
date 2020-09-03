using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegramBotApp;

namespace TelegramBotTests
{
    public class TelegramBotRunnerTest
    {
        [SetUp]
        public void Setup()
        {
        
        }

        [Test]
        public void ItStartsAndStopsABot()
        {
            TelegramBotRunner runner = new TelegramBotRunner();
            BotStub bot = new BotStub();

            runner.Start(bot);

            Thread.Sleep(millisecondsTimeout: 100);
            runner.Stop();

            Assert.Greater(bot.get_updates_numcalls, 0);
            Assert.Greater(bot.handle_update_numcalls, 0);
        }

        class BotStub : TelegramBotInterface
        {
            public int get_updates_numcalls = 0;
            public int handle_update_numcalls = 0;

            public List<TelegramBotUpdate> GetUpdates()
            {
                get_updates_numcalls++;

                Thread.Sleep(millisecondsTimeout: 10);

                return new List<TelegramBotUpdate> { new TelegramBotUpdate() };
            }

            public void HandleUpdate(TelegramBotUpdate update)
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
