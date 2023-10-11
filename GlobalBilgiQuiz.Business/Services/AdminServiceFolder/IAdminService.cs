using GlobalBilgiQuiz.Data.Services.AdminServiceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Services.AdminServiceFolder
{
    public interface IAdminService
    {
        public bool CheckUser(UserDTO user);

        AdminContestObject GetCurrentQuestion(int currentQuestionOrder);
    }
}
