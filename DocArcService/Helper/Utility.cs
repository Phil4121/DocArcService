using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DocArcService.Helper
{
    public static class Utility
    {
        public static bool AppSettingExists(string AppSetting)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(AppSetting))
                return false;

            return true;
        }
    }
}