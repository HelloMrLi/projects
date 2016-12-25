using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using OestsCommon;
using LitJson;

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

        Dictionary<byte,object> parameters = response.Parameters;
        object objJson;
        parameters.TryGetValue((byte)ParameterCode.ServerList, out objJson);
        List<string> list = JsonMapper.ToObject<List<string>>(objJson.ToString());
        foreach (var item in list)
        {
            Debug.Log("服务器地址： "+item);

        }

    }
}
