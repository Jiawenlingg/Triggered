using Telegram.Bot;
using Telegram.Bot.Types;
using triggeredapi.Helpers;

namespace triggeredapi.Service
{
    public class TelegramMessageHandler
    {
        private readonly DataContext _dataContext;
        public TelegramMessageHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void LinkTelegramChatId(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}.");
            var texts = message.Text.Split(' ');
            var fail = "Failed to register your Telegram into our database :< Please contact the developer.";
            if (texts.Length != 2)
            {
                SendMessage(botClient, chatId, fail, cancellationToken);
                return;
            }
            Guid.TryParse(texts[1], out Guid guid);
            var user = _dataContext.User.Find(guid);
            if(user == null){
                SendMessage(botClient, chatId, fail, cancellationToken);
                return;
            }
            
            user.TelegramId = chatId;
            _dataContext.SaveChanges();

            SendMessage(botClient, chatId, $"Successfully linked! Hi there {user.UserName}~", cancellationToken);
        }

        private async void SendMessage(ITelegramBotClient botClient, long chatId, string message, CancellationToken cancellationToken)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message,
                cancellationToken: cancellationToken);
        }
    }
}