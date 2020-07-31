using BiliWeb2.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models
{
    public class SystemGlobalsModel
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SystemGlobalsModel instance;
        private static readonly object syncRoot = new Object();

        private SystemGlobalsModel() { }

        public static SystemGlobalsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SystemGlobalsModel();
                            Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        // The Enum to use for the current data source
        // Default to Unknown, allowing for it to be set
        private static DataSourceEnum _DataSourceValue = DataSourceEnum.Unknown;

        // Default to Mock
        private static DataSourceDataSetEnum _DataSourceDataSetValue;

        // The DataSource to use
        public DataSourceEnum DataSourceValue => _DataSourceValue;

        // The Data Source Data set to use
        public DataSourceDataSetEnum DataSourceDataSet => _DataSourceDataSetValue;

        // Set default timeout for writing to azure at 60 seconds
        public TimeSpan ServerTimeout = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Initilize the site with the default context
        /// </summary>
        static public void Initialize()
        {
            return;
        }

        /// <summary>
        /// Call this from the controllers to warm up the database
        /// It first updates the System Path
        /// Then it calls to touch the datasource which warms up the database
        /// </summary>
        /// <param name="path"></param>
        public static void SetHostPath(string path)
        {
            // If it is not set, then set it. 
            if (_DataSourceValue == DataSourceEnum.Unknown)
            {
                SystemURLModel.Instance.HostingPath = path;

                var myDataSoruceEnum = SetDefaultDataSourceForURL();

                if (myDataSoruceEnum != _DataSourceValue)
                {
                    SetDataSourceEnum(myDataSoruceEnum);
                }

                // Cause the data to load
                //Backend.DataSourceBackend.Instance.WarmUp();
            }
        }

        public static DataSourceEnum SetDefaultDataSourceForURL()
        {
            var myReturn = DataSourceEnum.Mock;

            if (SystemURLModel.Instance.IsStringInSystemPath("biliweb"))
            {
                return DataSourceEnum.ServerLive;
            }

            if (SystemURLModel.Instance.IsStringInSystemPath("ski"))
            {
                return DataSourceEnum.ServerLive;
            }

            if (SystemURLModel.Instance.IsStringInSystemPath("azurewebsites.net"))
            {
                return DataSourceEnum.ServerTest;
            }

            if (SystemURLModel.Instance.IsStringInSystemPath("localhost"))
            {
                // TODO: Mike, this is where to switch between local and mock for testing

                // Mock is the default so it runs local

                //return DataSourceEnum.ServerLive;
                return DataSourceEnum.Mock;
            }

            if (SystemURLModel.Instance.IsStringInSystemPath("127.0.0"))
            {
                return DataSourceEnum.Mock;
            }

            // Not found, so return Mock
            return myReturn;
        }

        /// <summary>
        /// Sets the Data Source Enum
        /// </summary>
        /// <param name="SetDataSourceValue"></param>
        public static void SetDataSourceEnum(DataSourceEnum SetDataSourceValue)
        {
            _DataSourceValue = SetDataSourceValue;
        }

        /// <summary>
        /// Sets the Data Source Set Enum (Unit Test, demo etc.)
        /// </summary>
        /// <param name="SetDataSourceValue"></param>
        public static void SetDataSourceDataSetEnum(DataSourceDataSetEnum SetDataSourceDataSetValue)
        {
            _DataSourceDataSetValue = SetDataSourceDataSetValue;
        }
    }
}
