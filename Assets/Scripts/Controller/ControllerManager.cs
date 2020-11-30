using System;
using Model;
using Model.UserDatagrams;
using UnityEngine;
using View;

namespace Controller
{
    public class ControllerManager : MonoBehaviour
    {
        private ModelManager _modelManager;
        private ViewManager _viewManager;
        private bool _isReady = false;
        private void Start()
        {
            _modelManager = ModelManager.Instance();
            _modelManager.gameObject.transform.parent = transform;

            _modelManager.AddCallBack(ModelCallback);
            var obj = new GameObject("View Manager");
            obj.transform.position = new Vector3(-960f,540f);
            _viewManager = obj.AddComponent<ViewManager>();
            obj.transform.parent = transform;
            _viewManager.AddCallBack(ViewCallback);

            SetViewMessage(Main.AppType + " ready");
            ModelSend("ready");
        }

        private void ViewCallback(string value)
        {
            SetViewMessage(value);
            ModelSend(value);
        }

        private void ModelSend(string value)
        {
            _modelManager.SendUdp(value);
        }

        private void ModelCallback(UdpData udpData)
        {
            Debug.Log("udp receive:" + udpData.Text);
            SetViewMessage(udpData.Text);
            if (udpData.Text == "ready")
            {
                if (_isReady == false)
                {
                    _isReady = true;
                    _viewManager.Ready();
                    ModelSend("ready");
                }
            }
            else if (udpData.Text == "quit")
            {
                _isReady = false;
                _viewManager.StandBy();
            }
        }
        //
        private void SetViewMessage(string text)
        {
            var dateTime = DateTime.Now;
            var h = dateTime.Hour;
            var m = (dateTime.Minute + 100).ToString().Substring(1);
            var s = (dateTime.Second + 100).ToString().Substring(1);
            var ms = (dateTime.Millisecond + 1000).ToString().Substring(1);
            var str = "[" + h + ":" + m + ":" + s + ":" + ms + "]" + text;
            _viewManager.SetMessage(str);
        }
        
        private void OnApplicationQuit()
        {
            ModelSend("quit");
            _viewManager.SetMessage("quit");
        }
    }
}