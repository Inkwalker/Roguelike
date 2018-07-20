using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Actions;

namespace Roguelike.UI
{
    public class MessageLog : MonoBehaviour
    {
        [SerializeField] int maxMessages;

        List<IActionResult> logMessages;

        private Text logText;

        private void Awake()
        {
            logText = GetComponentInChildren<Text>();

            logMessages = new List<IActionResult>();

            logText.text = "";
        }

        public void AddMessage(params IActionResult[] messages)
        {
            logMessages.AddRange(messages);

            int excessMessages = logMessages.Count - maxMessages;

            if (excessMessages > 0) 
                logMessages.RemoveRange(0, excessMessages);

            UpdateText();
        }

        private void UpdateText()
        {
            var builder = new System.Text.StringBuilder();

            for (int i = 0; i < logMessages.Count; i++)
            {
                builder.AppendLine(logMessages[i].ToString());
            }

            logText.text = builder.ToString();
        }
    }
}
