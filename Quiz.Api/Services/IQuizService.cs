using Microsoft.IdentityModel.Logging;
using Quiz.Data;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        Task<QuestionDto?> GetQuestion(int category);

        Task<CheckAnswer> ChackAnswer(Guid answerId, int category);
    }
}
