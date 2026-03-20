using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using VibeBotApi.Dto;
using VibeBotApi.UpdateChainOfResponsibility;

namespace VibeBotApi.Controllers;

[ApiController]
[Route("api/telegram")]
public class TelegramWebhookController : ControllerBase
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly UpdateProcessor _processor;

    public TelegramWebhookController(
        ITelegramBotClient telegramBotClient,
        UpdateProcessor processor)
    {
        _telegramBotClient = telegramBotClient;
        _processor = processor;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> HandleWebhook(
        [FromBody] Update update,
        [FromHeader(Name = "X-Telegram-Bot-Api-Secret-Token")] string? headerSecret,
        CancellationToken cancellationToken)
    {
        var secret = Environment.GetEnvironmentVariable(EnvironmentVariables.TelegramWebhookSecret);

        if (headerSecret != secret)
        {
            return Unauthorized();
        }

        await _processor.ProcessAsync(update, cancellationToken);
        return Ok();
    }

    [HttpPost("webhook/settings")]
    public async Task<IActionResult> GetWebhookSettings(
        [FromBody] VibeBotApiSecretRequest request,
        CancellationToken cancellationToken)
    {
        var apiSecret = Environment.GetEnvironmentVariable(EnvironmentVariables.VibeBotApiSecret);

        if (request.VibeBotApiSecret != apiSecret)
        {
            return Unauthorized();
        }

        var result = await _telegramBotClient.GetWebhookInfo(cancellationToken);

        return Ok(result);
    }
}
