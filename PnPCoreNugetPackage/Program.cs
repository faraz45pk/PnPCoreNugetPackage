using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PnPCoreNugetPackage
{
    class Program
    {                      
        static void Main(string[] args)
        {
            string pwd = 
                System.Environment.
                GetEnvironmentVariable("MSOPWD", 
                EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(pwd))
            {
                System.Console.
                    WriteLine("MSOPWD user environment variable empty, cannot continue. Press any key to exit");
                System.Console.ReadKey();
                return;
            }
            else
            {
                using (var ctx =
                    new ClientContext("https://mydev2016.sharepoint.com/"))
                {
                    var passWord = new SecureString();
                    foreach (char c in pwd.ToCharArray())
                    {
                        passWord.AppendChar(c);
                    }
                    ctx.Credentials = 
                        new SharePointOnlineCredentials("faraz@mydev2016.onmicrosoft.com", passWord);
                    ctx.Web.CreateContentType("PnPCoreDoc", "0x010100D989C773362145A2A72CE6EE7F36B592", "PnP CT Group");
                    var list = ctx.Web.CreateList(ListTemplateType.DocumentLibrary,
                                "PnP Documents", false, enableContentTypes: true);
                    list.AddContentTypeToListByName("PnPCoreDoc", true);
                    Console.ReadKey();
                }
            }
        }
    }
}
