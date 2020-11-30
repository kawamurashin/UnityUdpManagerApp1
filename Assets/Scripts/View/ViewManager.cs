using System.Collections.Generic;
using UnityEngine;
namespace View
{
    public class ViewManager : MonoBehaviour
    {
        public delegate void Callback(string value);

        private Callback _callbacks;
        public void AddCallBack(Callback callback)
        {
            _callbacks += callback;
        }
        private List<string> _messages = new List<string>();
        private  string _textToEdit = "";
        private GUIStyle _style;
        private bool _isButtonEnable;
        public void OnGUI()
        {
            _style = GUI.skin.textArea;
            _style.fontSize = 32;
            _textToEdit = GUI.TextArea(new Rect(300, 10, 1000, 400), _textToEdit);
            GUI.enabled = _isButtonEnable;
            var button = GUI.Button(new Rect(10, 10, 180, 50), Main.AppType + " Click");
            if (button)
            {
                _callbacks(Main.AppType + " Click");
            }
        }

        private void Awake()
        {
            _isButtonEnable = false;
        }
        
        public void SetMessage(string value)
        {
            _messages.Add(value);
            _messages = CheckListLength(_messages);
            var message = "";
            var n = _messages.Count;
            for (var i = n - 1; i >= 0; i--)
            {
                message = message + "\n" + _messages[i];
            }
            _textToEdit = message;
        }

        public void Ready()
        {
            _isButtonEnable = true;
        }

        public void StandBy()
        {
            _isButtonEnable = false;
        }
        //private
        private static List<string> CheckListLength(List<string> list)
        {
            if (list.Count <= 6) return list;
            list.RemoveAt(0);
            CheckListLength(list);
            return list;
        }
    }
}