using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace triggeredapi.Service
{
    public class TelegramService: BackgroundService
    {
        private IConfiguration _config;
        private readonly TelegramBotClient _telegramBot;
        private readonly CancellationTokenSource _cts;
        public TelegramService(IConfiguration configuration)
        {
            _config = configuration;
            _telegramBot = new TelegramBotClient(_config.GetValue<string>("BotConfiguration:BotToken"));
            _cts = new CancellationTokenSource();
        }

        public string GetUniqueLink()
        {
            return "";
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            // Echo received message text
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said:\n" + messageText,
                cancellationToken: cancellationToken);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ReceiverOptions receiverOptions = new ()
            {
                AllowedUpdates = null // receive all update types except ChatMember related updates
            };

            _telegramBot.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: _cts.Token
            );

            Console.WriteLine($"Telegram bot started.");
            
        }
    }
}