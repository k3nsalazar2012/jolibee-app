// c# / unity class
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Jollibee.Chat;

// third party class
using DG.Tweening;
using System;

namespace Jollibee.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Color _activeColor = Color.white;
        [SerializeField] private Color _defaultColor = Color.white;

        [SerializeField] private Button _currentMenuButton = null;
        [SerializeField] private MenuBody[] _menuScreens = null;

        private MenuBody _currentScreen = null;
        private MenuBody _previousScreen = null;
        private float _transitionDuration = 0.25f;

        private ChatThread _chatThread = null;

        private void Awake()
        {
            _chatThread = FindObjectOfType<ChatThread>();
        }

        private void Start()
        {
            _currentScreen = _menuScreens[0];
            ChangeColor(_currentMenuButton, _activeColor);
        }

        public void MenuButtonClick(Button button)
        {
            if (_currentMenuButton == button) return;

            Debug.Log($"current: {_currentMenuButton.gameObject.name}, clicked: {button.gameObject.name}");

            ChangeColor(_currentMenuButton, _defaultColor);
            ChangeColor(button, _activeColor);

            int index = button.transform.GetSiblingIndex();
        
            _previousScreen = _currentScreen;
            _currentScreen = _menuScreens[index];
            SwitchScreen();
        }

        private void ChangeColor(Button targetButton, Color newColor)
        {
            targetButton.GetComponent<Image>().DOColor(newColor, _transitionDuration);

            if(newColor == _activeColor)
                targetButton.GetComponentInChildren<TMP_Text>().DOColor(newColor, _transitionDuration).OnComplete(()=> OnColorComplete(targetButton));
            else
                targetButton.GetComponentInChildren<TMP_Text>().DOColor(newColor, _transitionDuration);
        }

        private void SwitchScreen()
        {
            if (_currentScreen == _menuScreens[4])
                StartCoroutine(_chatThread.StartConversation());
            else
                StartCoroutine( _chatThread.StopConversation());

            if (_previousScreen.Position < _currentScreen.Position)
            {
                _previousScreen.MoveToLeft(_transitionDuration);
                _currentScreen.MoveToLeft(_transitionDuration);
            }
            else
            {
                _previousScreen.MoveToRight(_transitionDuration);
                _currentScreen.MoveToRight(_transitionDuration);
            }
        }

        private void OnColorComplete(Button button) => _currentMenuButton = button;
    }
}
