using WordsToolBot.Dto;

namespace WordsToolBot.UpdateChainOfResponsibility
{
    public sealed class UpdateContext
    {
        public UserDto? User { get; set; }
        public string? StudyLanguageCode { get; set; }
    }
}
