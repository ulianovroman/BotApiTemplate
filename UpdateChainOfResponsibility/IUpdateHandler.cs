using Telegram.Bot.Types;

namespace WordsToolBot.UpdateChainOfResponsibility
{
    public interface IUpdateHandler
    {
        Task HandleAsync(Update update, UpdateContext context, Func<Task> next, CancellationToken ct);
    }
}
