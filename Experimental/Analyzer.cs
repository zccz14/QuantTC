using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QuantTC.Experimental
{
    /// <summary>
    /// Dynamic Analyzer
    /// </summary>
    public class Analyzer
    {
        /// <summary>
        /// Assemblies
        /// </summary>
        public IReadOnlyCollection<Assembly> Assemblies => _assemblies;

        /// <summary>
        /// Types
        /// </summary>
        public IReadOnlyCollection<Type> Types => _types;

        /// <summary>
        /// Model Types
        /// </summary>
        public IReadOnlyCollection<IModel> Models => _models;

        /// <summary>
        /// Load and Analyze a file
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
            var asm = Assembly.LoadFrom(filename);
            _assemblies.Add(asm);
            var types = asm.GetTypes();

            foreach (var type in types)
            {
                _types.Add(type);
                var model = Model.Analyze(type);
                if (model != null)
                {
                    _models.Add(model);
                }
            }
        }

        public void LoadDirectory(string path)
        {
            var validExtensions = new[] {".dll", ".exe"};
            var files = Directory.GetFiles(path).Where(file => validExtensions.Contains(Path.GetExtension(file)));
            foreach (var file in files)
            {
                Load(file);
            }
        }

        private readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();
        private readonly HashSet<Type> _types = new HashSet<Type>();
        private readonly HashSet<IModel> _models = new HashSet<IModel>();
    }
}
