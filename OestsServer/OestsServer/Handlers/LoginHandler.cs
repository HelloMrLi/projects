
using System.Collections.Generic;
using LitJson;
using OestsCommon;
using OestsCommon.Model;
using OestsDataBase.Manager;
using OestsServer.Extensions;
using Photon.SocketServer;

namespace OestsServer.Handlers
{
    public class LoginHandler : HandlerBase
    {
        LoginManager manager;

        public LoginHandler()
        {
            manager = new LoginManager();
        }

        public override OperationCode OpCode { get { return OperationCode.Login; } }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer)
        {
            User u = request.Parameters.GetValue<User>(ParameterCode.User);

            /////////////////// 获得 数据库中的 user/////////////////////

            User UserDB = null;
            if (u.Type == UserType.Student)
                UserDB = manager.GetStudentByUserName(u.Name);
            else if (u.Type == UserType.Teacher)
                UserDB = manager.GetTeacherByUserName(u.Name);
            else if (u.Type == UserType.Admin)
                UserDB = manager.GetAdminByUserName(u.Name);
            else
            {
                peer.LoginUser = u;
                //游客 直接返回
                response.ReturnCode = (short)ReturnCode.Success;
            }
            /////////////////// 用户验证/////////////////////

            if (UserDB != null)
            {
                if (u.PWD == UserDB.PWD)
                {
                    peer.LoginUser = u;

                    response.ReturnCode = (short)ReturnCode.Success;
                    Dictionary<byte, object> parameters = new Dictionary<byte, object>();
                    string json = JsonMapper.ToJson(UserDB);
                    parameters.Add((byte)ParameterCode.User, json);
                    response.Parameters = parameters;
                   
                    //TODO:判断是否在线
                }
                else 
                {
                    response.ReturnCode = (short)ReturnCode.Fail;
                    response.DebugMessage = "密码错误！";
                    response.OperationCode = request.OperationCode;
                    Dictionary<byte, object> parameters = new Dictionary<byte, object>();
                    response.Parameters = parameters;
                }
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Fail;
                response.DebugMessage = "用户不存在！";
                response.OperationCode = request.OperationCode;
                Dictionary<byte, object> parameters = new Dictionary<byte, object>();
                response.Parameters = parameters;
            }
        }
    }
}
