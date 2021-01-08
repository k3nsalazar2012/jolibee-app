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

        private void Awake() => _rectTransform = GetComponent<RectTransform>();
        private int _jbDialogIndex = 0;
        private int _customerDialogIndex = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine( JBDialogue());
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(CustomerDialogue());
            }
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