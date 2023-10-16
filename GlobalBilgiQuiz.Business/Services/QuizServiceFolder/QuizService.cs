using GlobalBilgiQuiz.Business.Repositories;
using GlobalBilgiQuiz.Business.Services.Base;
using GlobalBilgiQuiz.Business.UnitOfWorkFolder;
using GlobalBilgiQuiz.Data.POCO;
using GlobalBilgiQuiz.Data.Services.QuizServiceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalBilgiQuiz.Business.Services.CacheServiceFolder;
using static System.Net.Mime.MediaTypeNames;

namespace GlobalBilgiQuiz.Business.Services.QuizServiceFolder
{
    public class QuizService : BaseService, IQuizService
    {

        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Metric> _metricRepository;
        private readonly ICacheService _questionCacheService;
        public QuizService(IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IRepository<Metric> metricRepository, ICacheService questionCacheService) : base()
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _metricRepository = metricRepository;
            _questionCacheService = questionCacheService;
        }

        public ContestObject GetQuestionObject(int currentQuestionOrder)
        {

            Question questionObject = null;
            if (!_questionCacheService.Exists("Question"))
            {
                questionObject = _questionRepository
                   .GetSingle(_ => _.Order == currentQuestionOrder, includeExpressions: new List<string> { "Answers" });
                _questionCacheService.Set("Question", questionObject, DateTimeOffset.UtcNow.AddMinutes(3));
            }

            questionObject = _questionCacheService.Get<Question>("Question");


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
