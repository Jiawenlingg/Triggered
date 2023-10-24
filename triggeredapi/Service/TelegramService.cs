// using Telegram.Bot;
// using Telegram.Bot.Exceptions;
// using Telegram.Bot.Polling;
// using Telegram.Bot.Types;
// using triggeredapi.Helpers;

// namespace triggeredapi.Service
// {
//     public class TelegramService: BackgroundService
//     {
//         private IConfiguration _config;
//         private readonly TelegramBotClient _telegramBot;
//         private readonly CancellationTokenSource _cts;
//         private readonly DataContext _dataContext;
//         private readonly IServiceScopeFactory _scopeFactory;
//         private TelegramMessageHandler _messageHandler;
//         public TelegramService(IConfiguration configuration, IServiceScopeFactory scopeFactory )
//         {
//             _config = configuration;
//             _telegramBot = new TelegramBotClient(_config.GetValue<string>("BotConfiguration:BotToken"));
//             _scopeFactory = scopeFactory;
//             _cts = new CancellationTokenSource();
//         }

//         async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//         {
//             // Only process Message updates: https://core.telegram.org/bots/api#message
//             if (update.Message is not { } message)
//                 return;
//             // Only process text messages
//             if (message.Text is not { } messageText)
//                 return;
//             using (var scope = _scopeFactory.CreateScope())
//             {
//                 _messageHandler = scope.ServiceProvider.GetRequiredService<TelegramMessageHandler>();
//                 _messageHandler.LinkTelegramChatId(botClient, update.Message, cancellationToken);
//             }
//         }



//         Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//         {
//             var ErrorMessage = exception switch
//             {
//                 ApiRequestException apiRequestException
//                     => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
//                 _ => exception.ToString()
//             };

//             Console.WriteLine(ErrorMessage);
//             return Task.CompletedTask;
//         }

//         protected async override Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             ReceiverOptions receiverOptions = new ()
//             {
//                 AllowedUpdates = null // receive all update types except ChatMember related updates
//             };

//             _telegramBot.StartReceiving(
//                 updateHandler: HandleUpdateAsync,
//                 pollingErrorHandler: HandlePollingErrorAsync,
//                 receiverOptions: receiverOptions,
//                 cancellationToken: _cts.Token
//             );

//             Console.WriteLine($"Telegram bot started.");
            
//         }
//     }
// }