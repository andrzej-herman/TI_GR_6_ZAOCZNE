using Quiz.Data;

namespace Quiz.Application.Services
{
    public interface IGameService
    {
        Task<QuestionDto?> GetQuestion(int category);
        Task<CheckAnswer?> CheckAnswer(Guid answerId, int category);
    }
}
