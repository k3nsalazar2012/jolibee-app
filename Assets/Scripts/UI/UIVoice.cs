// c# / unity class
using UnityEngine;
using TMPro;

// third party class
using DG.Tweening;

namespace Jollibee.UI
{
    public class UIVoice : AbstractUI, IUILayout
    {
        [SerializeField] private TMP_Text _aiText = null;
        [SerializeField] private Color _color = Color.white;

        private float _transitionDuration = 0.75f;

        public override void Initialize()
        {
            base.Initialize();
            _aiText.rectTransform.DOAnchorPosY((transform as RectTransform).rect.size.y / 2 + 50f, 0f);
        }
        public override void Loaded()
        {
            _aiText.DOColor(_color, _transitionDuration);
            _aiText.rectTransform.DOAnchorPosY(0f, _transitionDuration);
        }
    }
}