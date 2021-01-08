using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Jollibee.Chat
{
    public class SoundWave : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _bars = null;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _fullColor = Color.white;

        private void Start()
        {
            InitializeBars();
        }

        private void InitializeBars()
        {
            int pairs = _bars.Count / 2;
            int x = 10;

            for (int i = 0; i < pairs; i++)
            {
                int index = i * 2;
                
                index++;
                _bars[index].DOAnchorPosX(x, 0f);
                _bars[index].DOSizeDelta(new Vector2(8f, 2f), 0f);
                
                index++;
                _bars[index].DOAnchorPosX(-x, 0f);
                _bars[index].DOSizeDelta(new Vector2(8f, 2f), 0f);
                
                x += 10;
            }
        }

        public void FluctuateBars()
        {
            foreach (RectTransform b in _bars)
            {
                Fluctuate(b, b.GetComponent<Image>());
            }
        }

        private void Fluctuate(RectTransform bar, Image image)
        {
            float height = Random.Range(2f, 60f);
            float randomDuration = Random.Range(0.25f, 0.5f);
            float delay = Random.Range(0f, 0.2f);
            bar.DOSizeDelta(new Vector2(8f, height), 0f).SetDelay(delay);
            image.DOColor(_defaultColor, 0f).SetDelay(delay);

            bar.DOSizeDelta(new Vector2(8f, 2f), randomDuration).SetDelay(delay).OnComplete(()=> Fluctuate(bar, image));
            image.DOColor(_fullColor, randomDuration).SetDelay(delay);
        }

        public void StopBars()
        {
            DOTween.Clear();
            
            foreach (RectTransform b in _bars)
            {
                b.DOSizeDelta(new Vector2(8f, 2f), 0.25f);
                b.GetComponent<Image>().DOColor(_defaultColor, 0.25f);
            }
        }
    }
}