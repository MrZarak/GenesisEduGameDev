using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace moving
{
    public class InputSourcesHandler
    {
        private readonly Dictionary<RuntimePlatform, List<IInputSource>> _sources = new();

        public void RegisterSource(IInputSource source)
        {
            var runtimePlatforms = source.GetAllowPlatforms();

            foreach (var platform in runtimePlatforms)
            {
                List<IInputSource> list;

                if (!_sources.ContainsKey(platform))
                {
                    list = new List<IInputSource>();
                    _sources[platform] = list;
                }
                else
                {
                    list = _sources[platform];
                }

                list.Add(source);
            }
        }

        public bool IsDown(InputCode code)
        {
            var platform = Application.platform;
            
            if (!_sources.ContainsKey(platform))
                return false;
            
            var sources = _sources[platform];

            return sources.Any(source => source.IsDown(code));
        }

        public bool IsPressed(InputCode code)
        {
           
            var platform = Application.platform;
            
            if (!_sources.ContainsKey(platform))
                return false;
            
            var sources = _sources[platform];

            return sources.Any(source => source.IsPressed(code));
        }

        public bool IsUp(InputCode code)
        {
          
            var platform = Application.platform;
            
            if (!_sources.ContainsKey(platform))
                return false;
            
            var sources = _sources[platform];

            return sources.Any(source => source.IsUp(code));
        }
    }
}