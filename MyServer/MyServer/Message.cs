namespace MyServer.Message
{
    enum OpCodeEnum : byte
    {
        Login = 1,
        LoginSuccess = 2,
        LoginFailed = 3,

        Create = 250,
        Join = 255,
        Leave = 254,
        RaiseEvent = 253,
        SetProperties = 252,
        GetProperties = 251
    }

    enum OpKeyEnum : byte
    {
        RoomId = 251,
        UserName = 252,
        PassWord = 253
    }
}