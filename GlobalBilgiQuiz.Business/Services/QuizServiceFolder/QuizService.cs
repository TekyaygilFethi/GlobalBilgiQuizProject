using GlobalBilgiQuiz.Business.Repositories;
using GlobalBilgiQuiz.Business.Services.Base;
using GlobalBilgiQuiz.Business.Services.RedisServiceFolder;
using GlobalBilgiQuiz.Data.POCO;
using GlobalBilgiQuiz.Data.Services.QuizServiceFolder;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GlobalBilgiQuiz.Business.Services.QuizServiceFolder
{
    public class QuizService : BaseService, IQuizService
    {

        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Metric> _metricRepository;
        private readonly IRedisService _redisService;
        public QuizService(IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IRepository<Metric> metricRepository,
            IRedisService redisService) : base()
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _metricRepository = metricRepository;
            _redisService = redisService;
        }

        public ContestObject GetQuestionObject(int currentQuestionOrder)
        {
            
            var redisKey = currentQuestionOrder.ToString();
            var result = _redisService.GetDb().KeyExists(redisKey);

            if (!result)
            {
                var questionObject = _questionRepository
                    .GetSingle(_ => _.Order == currentQuestionOrder, includeExpressions: new List<string> { "Answers" });

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Converters = { new JsonStringEnumConverter() },
                    IgnoreNullValues = true
                };

                var jsonString = System.Text.Json.JsonSerializer.Serialize(questionObject, options);

                _redisService.GetDb().StringSet(redisKey, jsonString);

                return new ContestObject
                {
                    QuestionId = questionObject.Id,
                    QuestionMetricId = questionObject.Id,
                    QuestionOrder = currentQuestionOrder,
                    QuestionText = questionObject.Text,
                    ShowcaseAnswers = questionObject.Answers.Select(_ => new ContestObjectAnswer
                    {
                        AnswerId = _.Id,
                        AnswerText = _.Text
                    })
                };
            }

            var serializedResult = _redisService.GetDb().StringGet(redisKey);
            var deserializedResult =  JsonConvert.DeserializeObject<JsonContestObject>(serializedResult);

            return new ContestObject
            {
                QuestionId = deserializedResult.Id,
                QuestionMetricId = deserializedResult.Id,
                QuestionOrder = currentQuestionOrder,
                QuestionText = deserializedResult.Text,
                ShowcaseAnswers = deserializedResult.Answers.Select(_ => new ContestObjectAnswer
                {
                    AnswerId = _.Id,
                    AnswerText = _.Text
                })
            };
        }

        public void AnswerQuestion(AnswerQuestionModel model)
        {
            var questionObject = _questionRepository
                .GetSingle(_ => _.Id == model.QuestionId, includeExpressions: new List<string> { "Answers", "Metric" });

            var chosenAnswer = questionObject.Answers.SingleOrDefault(_ => _.Id == model.ChosenAnswerId);
            chosenAnswer.ChosenCount += 1;

            if (chosenAnswer.IsTrue)
                questionObject.Metric.TrueCount += 1;
            else
                questionObject.Metric.FalseCount += 1;
        }


        public TrueFalseCount GetTotalStatistics()
        {
            int totalTrue = 0;
            int totalFalse = 0;

            _metricRepository.GetAllQueryable()
                .Select(_ => new { _.TrueCount, _.FalseCount })
                .ToList()
                .ForEach(_ => CalculateTotalSum(ref totalTrue, ref totalFalse, _));

            return new TrueFalseCount
            {
                TotalTrue = totalTrue,
                TotalFalse = totalFalse
            };
        }

        public IEnumerable<int> GetChosenCounts(int currentQuestionOrder)
        {
            return _questionRepository
                .GetSingle(_ => _.Order == currentQuestionOrder, includeExpressions: new List<string> { "Answers" }).Answers
                .OrderBy(_ => _.Id)
                .Select(_ => _.ChosenCount);
        }

        private void CalculateTotalSum(ref int totalTrue, ref int totalFalse, dynamic metric)
        {
            totalTrue += metric.TrueCount;
            totalFalse += metric.FalseCount;
        }

    }
}
