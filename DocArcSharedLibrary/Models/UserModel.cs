using DocArcSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocArcSharedLibrary.Models
{
    public class UserModel : Users
    {
        public UserModel(string UserId = "", string Container = "")
        {
            _UserId = UserId;
            _Container = Container;
        }

        private string _UserId;

        private string _Container;

        public new string UserId
        {
            get
            {
                return _UserId;
            }
        }


        public new string Container {
            get
            {
                return _Container;
            }
        }
    }
}