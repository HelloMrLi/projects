using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhotonSocket : MonoBehaviour, IPhotonPeerListener
{

    #region 单例

    private static PhotonSocket _Instance;

    public static PhotonSocket Instance
    {
        get { return _Instance; }
    }

    #endregion

    private string address; //最好在Awake或Start中赋值，Unity 小问题，容易造成值不更改，还有最好写成私有
    private string Server; //同上
    private PhotonPeer peer;


    [SerializeField]
    private InputField inputFieldIp;

    [SerializeField]
    private Text result;
    public ClientState state;


    void Awake()
    {
        _Instance = this;
        address = "localhost:5055";
        Server = "MyServer";
        state = ClientState.DisConnect;
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect(address, Server);
       // //peer.Connect("788" + ":5055", Server);
        //peer.Connect("1v62708w05.imwork.net:21524", Server);

    }

    public void OnClickConect()
    {
        Dictionary<byte, object> dict = new Dictionary<byte, object>();
        dict.Add(1,"123");
        dict.Add(2,"1234");
        //peer.Connect(inputFieldIp.text + ":5055", Server);
        SendMessage((byte)OpCodeEnum.Login, dict);
    }

    public void SendMessage(byte Code, Dictionary<byte, object> param)
    {
        peer.OpCustom(Code, param, true);
    }

    void Update()
    {
        peer.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnEvent(EventData eventData)
    {
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("login back  " + operationResponse.OperationCode);
        switch (operationResponse.OperationCode)
        {
            case (byte)OpCodeEnum.LoginSuccess:
                Debug.Log("login Success");
                state = ClientState.LoginSuccess;
                break;
            case (byte)OpCodeEnum.LoginFailed:
                Debug.Log("login Failed");
                state = ClientState.LoginFailed;
                break;
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect");
                result.text = "Connect";
                break;
            case StatusCode.Disconnect:
                Debug.Log("DisConnect");
                result.text = "DisConnect";
                break;
        }
    }

    public enum ClientState : byte
    {
        DisConnect,
        Connect,
        LoginSuccess,
        LoginFailed
    }

    enum OpCodeEnum : byte
    {
        //Login
        Login = 1,
        LoginSuccess = 2,
        LoginFailed = 3,
    }
}