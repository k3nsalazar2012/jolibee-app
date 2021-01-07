
using UnityEngine;

using DG.Tweening;

namespace Jollibee.Menu
{
    public class MenuBody : MonoBehaviour
    {
        [SerializeField] private bool _isCenter = false;

        private RectTransform _rectTransform = null;
        private float _screenWidth = 0f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenWidth = _rectTransform.rect.size.x;

            if (_isCenter)
                _rectTransform.DOAnchorPosX(0f, 0f);
            else
                _rectTransform.DOAnchorPosX(_screenWidth, 0f);
        }

        public void MoveToLeft(float duration)
        {
            _rectTransform.DOAnchorPosX(Position - _screenWidth, duration);
        }

        public void MoveToRight(float duration)
        {
            _rectTransform.DOAnchorPosX(Position + _screenWidth, duration);
        }

        public float Position { get { return _rectTransform.anchoredPosition.x; } }
        public RectTransform Canvas { get { return _rectTransform; } }
    }
}