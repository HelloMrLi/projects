

namespace OestsCommon.Model
{
    public class Admin : User
    {       
      
       public Admin(string name, string pwd)
            : base(name, pwd)
        {
            Type = UserType.Admin;  

        }

        public Admin()
        {
            Type = UserType.Admin;  
        }
    }
}
