using ExitGames.Client.Photon;
using OestsCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public OperationCode opCode;
    // Use this for initialization
    public virtual void Start()
    {
        PhotonEngine.Instance.RegisterContoller(opCode, this);
    }
   

    public virtual void OnDestroy()
    {
        PhotonEngine.Instance.UnRegisterContoller(opCode);
    }

    public abstract void OnOperationResponse(OperationResponse response);

}
