using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace MEF3
{
    public class BootStrapper
    {
        private static CompositionContainer _compostionContainer;
        private static bool IsLoaded = false;

        public static void Compose(List<string> pluginFolder)
        {
            if (IsLoaded) return;

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));
            foreach (var plugin in pluginFolder) {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", plugin));
                catalog.Catalogs.Add(directoryCatalog);
            }

            _compostionContainer = new CompositionContainer(catalog);
            _compostionContainer.ComposeParts();
            IsLoaded = true;
        }

        public static T GetInstance<T>(string contractname = null)
        {
            var type = default(T);
            if (_compostionContainer == null) return type;
            if (!string.IsNullOrWhiteSpace(contractname))
                type = _compostionContainer.GetExportedValue<T>(contractname);
            else
                type = _compostionContainer.GetExportedValue<T>();

            return type;
        }
    }
}