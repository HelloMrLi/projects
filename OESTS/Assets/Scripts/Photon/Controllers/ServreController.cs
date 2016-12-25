using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using OestsCommon;

public class ServreController : ControllerBase
{
    public override void Start()
    {
        base.Start();
        PhotonEngine.Instance.OnConnectedToServer += GetServerList;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public void GetServerList()
    {
        PhotonEngine.Instance.SendReguest(OperationCode.LoadServer, new Dictionary<byte, object>());
    }

    public override void OnOperationResponse(OperationResponse response)
    {
    }
}
