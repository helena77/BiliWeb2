using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models
{
    /// <summary>
    /// System wide Global variables
    /// </summary>
    public class SystemURLModel
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SystemURLModel instance;
        private static readonly object syncRoot = new Object();

        private SystemURLModel() { }

        public static SystemURLModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SystemURLModel();
                        }
                    }
                }

                return instance;
            }
        }

        public List<string> SystemURLs { get; set; } = new List<string>();
        public string HostingPath { get; set; }

        public bool IsStringInSystemPath(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (HostingPath.ToLower().Contains(value))
            {
                return true;
            }

            return false;
        }
    }
}
