using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using OestsCommon;
using LitJson;
using OestsCommon.Model;

public class MajorController : ControllerBase
{
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.LoadMajorList;
        }
    }

    public override void Start()
    {
        base.Start();
        PhotonEngine.Instance.OnConnectedToServer += LoadMajorList;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        PhotonEngine.Instance.OnConnectedToServer -= LoadMajorList;

    }
    public void LoadMajorList()
    {
        PhotonEngine.Instance.SendReguest(opCode, new Dictionary<byte, object>());
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        Dictionary<byte,object> parameters = response.Parameters;
        object objJson;
        parameters.TryGetValue((byte)ParameterCode.MajorList, out objJson);

        List<Major> list = JsonMapper.ToObject<List<Major>>(objJson.ToString());
        foreach (var item in list)
        {
            Debug.Log("专业名称： " + item.Name);
        }
    }
}
