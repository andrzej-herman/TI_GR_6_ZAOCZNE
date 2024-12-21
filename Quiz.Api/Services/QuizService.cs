using Microsoft.Data.SqlClient;
using Quiz.Data;

namespace Quiz.Api.Services
{
    public class QuizService : IQuizService
    {
        SqlConnection _connection;
        Random _random;
        private const string connStr = "Server=tcp:projektysan.database.windows.net,1433;Initial Catalog=sanquiz;Persist Security Info=False;User ID=aherman;Password=yxFH#D8w1SabJ1TAH99f;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public QuizService()
        {
            _connection = new SqlConnection(connStr);
            _random = new Random();
        }

        public async Task<QuestionDto?> GetQuestion(int category)
        {
            var questions = new List<QuestionDto>();
            await _connection.OpenAsync();
            var query = $"select QuestionId, QuestionContent from Questions where QuestionCategory = {category}";
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var qId = reader.GetGuid(0); 
                var qContent = reader.GetString(1);
                questions.Add(new QuestionDto
                {
                    Id = qId,
                    Category = category,
                    Content = qContent,
                    Answers = []
                }
                );
            }
            await reader.CloseAsync();

            if (!questions.Any()) return null;
            var index = _random.Next(0, questions.Count);
            var selectedQuestion = questions[index];

            var queryA = $"select AnswerId, AnswerContent from Answers where QuestionId = '{selectedQuestion.Id}'";
            var commandA = new SqlCommand(queryA, _connection);
            var readerA = commandA.ExecuteReader();
            while (readerA.Read())
            {
                var aId = readerA.GetGuid(0);
                var aContent = readerA.GetString(1);
                selectedQuestion.Answers.Add(new AnswerDto { Id = aId, Content = aContent });   
            }

            await _connection.CloseAsync();
            return selectedQuestion;
        }

        public async Task<CheckAnswer> ChackAnswer(Guid answerId, int category)
        {
            List<int> categories = [100, 200, 300, 400, 500, 750, 1000];

            var correct = false;
            await _connection.OpenAsync();
            var query = $"select AnswerIsCorrect from Answers where AnswerId = '{answerId}'";
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                correct = reader.GetBoolean(0);
            }

            var currentIndex = categories.IndexOf(category);
            var nextCategory = currentIndex == 6 ? 0 : categories[currentIndex + 1];
            
            await _connection.CloseAsync();
            return new CheckAnswer { IsCorrect = correct, NextCategory = nextCategory };
        } 
    }
}
