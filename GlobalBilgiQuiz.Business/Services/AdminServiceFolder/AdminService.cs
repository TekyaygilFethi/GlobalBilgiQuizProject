using GlobalBilgiQuiz.Business.Repositories;
using GlobalBilgiQuiz.Data.POCO;
using GlobalBilgiQuiz.Data.Services.AdminServiceFolder;
using GlobalBilgiQuiz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Services.AdminServiceFolder
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Question> _questionRepository;
        public AdminService(IRepository<User> userRepository, IRepository<Question> questionRepository)
        {
            _userRepository = userRepository;
            _questionRepository = questionRepository;
        }

        public bool CheckUser(UserDTO user)
        {
            return _userRepository
                .Exists(_ => _.Username == user.Username
                && _.Password == CryptographyHelper.Encode(user.Password, "LnKpsvEmzRBc9fS"));
        }

        public AdminContestObject GetCurrentQuestion(int currentQuestionOrder)
        {
            var question = _questionRepository
                .GetSingle(_ => _.Order == currentQuestionOrder, true, new List<string> { "Answers" });
            
            var answers = question.Answers;

            return new AdminContestObject
            {
                Question = question.Text,
                Answer1 = answers[0].Text,
                Answer2 = answers[1].Text,
                Answer3 = answers[2].Text,
                Answer4 = answers[3].Text
            };

        }
    }
}
