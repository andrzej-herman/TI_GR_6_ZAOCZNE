using Microsoft.AspNetCore.Mvc;
using Quiz.Api.Services;

namespace Quiz.Api.Controllers
{
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
            // https://localhost:7000/getquestion => Daj pytanie
            // https://localhost:7000/checkanswer => Sprawdü odpowiedü
        }

        // Endpoint do dostarczania pytania z odpowiedziami
        // Przekazujemy kategoriÍ pytania

        [HttpGet]
        [Route("getquestion")]
        public async Task<IActionResult> DajPytanieAsync(int category)
        {
           var question = await _service.GetQuestion(category);
           return question != null ? Ok(question) : BadRequest("Nieprawid≥owa kategoria pytania");   
        }


        // Endpoint do sprawdzania czy odpowiedü jest prawi≥owa
        // Przekazujemy id odpowiedzi

        [HttpGet]
        [Route("checkanswer")]
        public async Task<IActionResult> SprawdzOdpowiedz(Guid answerId, int category)
        {
            var result = await _service.ChackAnswer(answerId, category);
            return Ok(result);
        }

    }
}
