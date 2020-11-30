using Model.UserDatagrams;
using UnityEngine;

namespace Model
{
    public class ModelManager : MonoBehaviour
    {
        public delegate void Callback(UdpData value);

        private Callback _callbacks;

        public void AddCallBack(Callback callback)
        {
            _callbacks += callback;
        }

        private UdpManager _udpManager;

        private static ModelManager _instance;

        private ModelManager()
        {
        }

        public static ModelManager Instance()
        {
            if (_instance == null)
            {
                var obj = new GameObject("ModelManager");
                _instance = obj.AddComponent<ModelManager>();
            }

            return _instance;
        }

        /// <summary>
        /// UDPで送信します。
        /// </summary>
        /// <param name="value">送信値</param>
        public void SendUdp(string value)
        {
            _udpManager.Send(value);
        }

        private void Awake()
        {
            var obj = new GameObject("UPD Manager");
            _udpManager = obj.AddComponent<UdpManager>();
            obj.transform.parent = transform;
            _udpManager.AddCallback(UdpCallbackHandler);
        }

        private void UdpCallbackHandler(UdpData udpData)
        {
            _callbacks(udpData);
        }
    }
}