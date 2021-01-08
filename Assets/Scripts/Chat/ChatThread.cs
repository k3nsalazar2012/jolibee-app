using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

// third party class
using DG.Tweening;


namespace Jollibee.Chat
{
    public class ChatThread : MonoBehaviour
    {
        [SerializeField] private GameObject _jbDialogPrefab = null;
        [SerializeField] private GameObject _customerDialogPrefab = null;
        [SerializeField] private RectTransform _viewport = null;

        [SerializeField] private List<string> _jbSpiels = new List<string>();
        [SerializeField] private List<string> _customerSpiels = new List<string>();

        private RectTransform _rectTransform = null;
        private SoundWave _soundWave = null;

        private int _jbDialogIndex = 0;
        private int _customerDialogIndex = 0;
        private int _dialogueIndex = 0;

        private bool _isStopped = false;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _soundWave = FindObjectOfType<SoundWave>();
        }

        public IEnumerator StartConversation()
        {
            yield return new WaitForSeconds(0.25f);
            _soundWave.StopBars();
            _isStopped = false;
            _jbDialogIndex = 0;
            _customerDialogIndex = 0;
            _dialogueIndex = 0;

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, 0f);

            if (transform.childCount != 0)
            { 
                for(int i=0; i<transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (_isStopped)
                    yield return null;
                else
                {
                    _soundWave.FluctuateBars();
                    ExchangeConversation();
                    yield return new WaitForSeconds(Random.Range(1.5f, 3f));
                    _soundWave.StopBars();

                    yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                }
            }
        }

        public IEnumerator StopConversation()
        {
            yield return new WaitForSeconds(0.26f);
            _isStopped = true;
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, 0f);
            _soundWave.StopBars();
        }

        private void ExchangeConversation()
        {
            if (_dialogueIndex % 2 == 0)
                StartCoroutine(JBDialogue());
            else
                StartCoroutine(CustomerDialogue());

            _dialogueIndex++;
        }

        private IEnumerator JBDialogue()
        {
            RectTransform jbDialog = Instantiate(_jbDialogPrefab, transform).transform as RectTransform;
            TMP_Text dialogText = jbDialog.GetComponentInChildren<TMP_Text>();

            dialogText.text = _jbSpiels[_jbDialogIndex];

            yield return new WaitForSeconds(0.1f);
            AddMessage(jbDialog);
            yield return new WaitForSeconds(0.1f);
            jbDialog.GetComponent<CanvasGroup>().alpha = 1f;

            _jbDialogIndex++;
        }

        private IEnumerator CustomerDialogue()
        {
            RectTransform customerDialog = Instantiate(_customerDialogPrefab, transform).transform as RectTransform;
            TMP_Text dialogText = customerDialog.GetComponentInChildren<TMP_Text>();

            dialogText.text = _customerSpiels[_customerDialogIndex];            

            yield return new WaitForSeconds(0.1f);
            AddMessage(customerDialog);
            yield return new WaitForSeconds(0.1f);
            customerDialog.GetComponent<CanvasGroup>().alpha = 1f;

            _customerDialogIndex++;
        }

        public void AddMessage(RectTransform message)
        {
            if (_isStopped) return;

            message.DOAnchorPosY(-Height, 0f); 

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, Height + message.rect.height);

            if (Height > Viewport)
            {
                _rectTransform.DOAnchorPosY(Height - Viewport, 0.25f);
            }
        }

        public void UpdateLastMessage(RectTransform message)
        { 
        
        }
        
        public float Width { get { return _rectTransform.rect.width; } }
        public float Height { get { return _rectTransform.rect.height; } }
        
        public float Viewport { get { return Mathf.Abs(_viewport.rect.y); } }
    }
}