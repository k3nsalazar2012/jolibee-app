// c# / unity class
using System.Collections;
using UnityEngine;

// project class
using Jollibee.Nav;

namespace Jollibee.UI
{
    public class UIManager : MonoBehaviour
    {
        private UISplash _uiSplash = null;
        private UIVoice _uiVoice = null;

        private float _splashDuration = 2f;

        private void Awake()
        {
            _uiSplash = FindObjectOfType<UISplash>();
            _uiVoice = FindObjectOfType<UIVoice>();
        }

        private IEnumerator Start()
        {
            Navigator.Instance.CurrentUI = _uiSplash;
            _uiSplash.Show();

            yield return new WaitForSeconds(_splashDuration);
            Navigator.Instance.Navigate(_uiVoice);
        }
    }
}