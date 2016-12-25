using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine.UI;
using OestsCommon;

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

    private Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();


    public delegate void OnConnectedToServerEvent();
    public event OnConnectedToServerEvent OnConnectedToServer;

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

    public void RegisterContoller(OperationCode opCode,ControllerBase controller)
    {
        controllers.Add((byte)opCode, controller);
    }
    public void UnRegisterContoller(OperationCode opCode)
    {
        controllers.Remove((byte)opCode);
    }

    public void SendReguest(OperationCode opCode,Dictionary<byte,object> parameters)
    {
        peer.OpCustom((byte)opCode, parameters, true);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(level + ":" + message);
    }

    public void OnEvent(EventData eventData)
    {
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        ControllerBase controller;
        controllers.TryGetValue(operationResponse.OperationCode, out controller);
        if(controller != null)
        {
            controller.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("Receive a unknown response");
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect");
                OnConnectedToServer();
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
}