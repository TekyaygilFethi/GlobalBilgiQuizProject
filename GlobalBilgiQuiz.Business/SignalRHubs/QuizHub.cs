using GlobalBilgiQuiz.Data.Services.QuizServiceFolder;
using GlobalBilgiQuiz.Data.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.SignalRHubs
{
    public class QuizHub : Hub
    {

        public Task Broadcast(TrueFalseCount information)
        {
            return Clients.All.SendAsync("Broadcast", information);
        }

        public Task BroadcastByQuestion(QuestionBaseTrueFalseCount information)
        {
            return Clients.All.SendAsync("BroadcastByQuestion", information);
        }

        public Task ResetSingleBarChart()
        {
            return Clients.All.SendAsync("ResetSingleBarChart");
        }

        public Task NextQuestion()
        {
            return Clients.All.SendAsync("NextQuestion");
        }

        public Task StartContest()
        {
            return Clients.All.SendAsync("StartContest");
        }

        public Task EndCountdown()
        {
            return Clients.All.SendAsync("EndCountdown");
        }

        public Task UpdateCounter()
        {
            return Clients.All.SendAsync("UpdateCounter");

        }

    }
}
