using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Data.Services.QuizServiceFolder
{
    public class JsonContestObjectAnswer
    {
        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("Question")]
        public object Question { get; set; } // Question, null olabilir, bu nedenle object olarak belirlendi.

        [JsonPropertyName("QuestionId")]
        public int QuestionId { get; set; }

        [JsonPropertyName("IsTrue")]
        public bool IsTrue { get; set; }

        [JsonPropertyName("ChosenCount")]
        public int ChosenCount { get; set; }

        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}
