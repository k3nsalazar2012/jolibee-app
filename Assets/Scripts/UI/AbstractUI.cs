// c# / unity class
using UnityEngine;

namespace Jollibee.UI
{
    public abstract class AbstractUI : MonoBehaviour
    {
        public CanvasGroup Canvas { get { return _canvas; } }
        private CanvasGroup _canvas = null;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
            Hide();
        }

        private void Start() => Initialize();

        public virtual void Initialize() { _canvas.blocksRaycasts = false; }
        public void Show() =>_canvas.alpha = 1f;
        public void Hide() => _canvas.alpha = 0f;
        public virtual void Loaded() => Debug.Log("loaded");
    }
}