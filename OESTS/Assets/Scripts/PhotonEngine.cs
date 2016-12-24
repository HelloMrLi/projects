using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{

    #region 单例

    private static PhotonEngine _Instance;

    public static PhotonEngine Instance
    {
        get { return _Instance; }
    }

    #endregion

    private string address; //最好在Awake或Start中赋值，Unity 小问题，容易造成值不更改，还有最好写成私有
    private string Server; //同上
    private PhotonPeer peer;
    public ClientState state;


    void Awake()
    {
        _Instance = this;
        address = "localhost:4530";
        Server = "OestsServer";
        state = ClientState.DisConnect;
        peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
        peer.Connect(address, Server);
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
                break;
            case StatusCode.Disconnect:
                Debug.Log("DisConnect");
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