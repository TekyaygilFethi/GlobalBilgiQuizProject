namespace GlobalBilgiQuiz.MVC
{
    public class Deneme
    {
        public void OldOrder(string locationName)
        {
            if(locationName == "McDonalds")
            {
                McDonalds md = new McDonalds();
                md.Order();
            }
            else if (locationName == "Burger King")
            {
                BurgerKing bk = new BurgerKing();
                bk.Order();
            }
            else if (locationName == "KFC")
            {
                KFC kfc = new KFC();
                kfc.Order();
            }
        }

        public void Order(IFastFoods location)
        {
            location.Order();
        }

        public void Order2(McDonalds location)
        {
            location.Order();
        }

        public void Call()
        {
            Order(new McDonalds());
            Order(new BurgerKing());
            Order(new KFC());
        }


    }

    public interface IFastFoods
    {
        public string Order();
    }

    public class McDonalds: IFastFoods
    {
        public string Order()
        {
            return "Ordered from McDonalds";
        }
    }

    public class BurgerKing: IFastFoods
    {
        public string Order()
        {
            return "Ordered from Burger King";
        }
    }

    public class KFC: IFastFoods
    {
        public string Order()
        {
            return "Ordered from KFC";
        }
    }

    public class Popeyes : IFastFoods
    {
        public string Order()
        {
            return "Ordered from Popeyes";
        }
    }
}
