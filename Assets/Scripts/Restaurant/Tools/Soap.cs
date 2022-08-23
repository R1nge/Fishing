using Restaurant.UI;

namespace Restaurant.Tools
{
    public class Soap : Tool
    {
        private int _progress;
        private WasherUI _ui;

        protected override void Awake()
        {
            base.Awake();
            _ui = FindObjectOfType<WasherUI>();
        }

        protected override void Action()
        {
            if (FishStatus.IsWashed) return;
            _progress += 10;
            _ui.UpdateProgressUI(_progress);

            if (_progress >= 100)
            {
                FishStatus.Wash();
                _progress = 0;
            }
        }
    }
}