namespace GlobalBilgiQuiz.MVC.Models
{
    public class DenemeModel : SayiModel
    {
        public string Islem { get; set; }
    }

    public class SayiModel
    {
        public int Sayi1 { get; set; }
        public int Sayi2 { get; set; }
    }
}
