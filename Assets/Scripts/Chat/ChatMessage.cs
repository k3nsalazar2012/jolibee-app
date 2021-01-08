using UnityEngine;
using UnityEngine.UI;

namespace Jollibee.Chat
{
    public class ChatMessage : MonoBehaviour
    {
        private ChatThread _chatThread = null;
        private Button _button = null;

        private void Awake()
        {
            _chatThread = FindObjectOfType<ChatThread>();
            _button = GetComponent<Button>();

        }
        private void Start() => _button.onClick.AddListener(() => MessageClick());

        private void MessageClick()
        {
            _chatThread.AddMessage(transform as RectTransform);
            Debug.Log("massage click");
        }
    }
}