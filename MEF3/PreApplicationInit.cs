using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Web.Compilation;
using System.Reflection;

[assembly: PreApplicationStartMethod(typeof(MEF3.PreApplicationInit),"Initialize")]

namespace MEF3
{
    public class PreApplicationInit
    {
        private static readonly DirectoryInfo PluginFolder, ShadowCopyFolder;

        static PreApplicationInit()
        {
            PluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Modules/Plugin1"));
            ShadowCopyFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Modules/Plugin1/temp"));
        }

        public static void Initialize()
        {
            Directory.CreateDirectory(ShadowCopyFolder.FullName);
            foreach (var f in ShadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories))
                f.Delete();

            foreach (var plug in PluginFolder.GetFiles("*.dll",SearchOption.AllDirectories))
            {
                //var di = Directory.CreateDirectory(Path.Combine(ShadowCopyFolder.FullName, plug.Directory.Name));
                File.Copy(plug.FullName, Path.Combine(ShadowCopyFolder.FullName, plug.Name), true);
            }

            var dFiles = ShadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories);

            foreach (var a in dFiles) 
                BuildManager.AddReferencedAssembly(Assembly.Load(AssemblyName.GetAssemblyName(a.FullName)));
        }

    }
}