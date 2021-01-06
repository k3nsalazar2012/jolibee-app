// c# / unity class
using UnityEngine;

// project class
using Jollibee.UI;

// third party class
using DG.Tweening;

namespace Jollibee.Nav
{
    public class Navigator : MonoBehaviour
    {
        public AbstractUI CurrentUI { set; get; }
        public AbstractUI PreviousUI { set; get; }

        private float _transitionDuration = 0.25f;

        #region Singleton Implementation
        private static Navigator _instance;
        public static Navigator Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
        }
        #endregion

        public void Navigate(AbstractUI target)
        {
            PreviousUI = CurrentUI;
            CurrentUI = target;

            PreviousUI.Canvas.DOFade(0f, _transitionDuration);
            CurrentUI.Canvas.DOFade(1f, _transitionDuration).OnComplete(() => OnScreenLoaded());
        }

        private void OnScreenLoaded()
        {
            CurrentUI.Canvas.blocksRaycasts = true;
            CurrentUI.Loaded();
        }
    }
}