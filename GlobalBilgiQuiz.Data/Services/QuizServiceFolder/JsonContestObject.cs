using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Data.Services.QuizServiceFolder
{
    public class JsonContestObject
    {
        [JsonPropertyName("Order")]
        public int Order { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("Metric")]
        public string Metric { get; set; } 

        [JsonPropertyName("Answers")]
        public List<JsonContestObjectAnswer> Answers { get; set; }

        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}
