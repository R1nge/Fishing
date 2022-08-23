namespace Restaurant.Tools
{
    public class Knife : Tool
    {
        protected override void Action() => FishStatus.Chop();
    }
}