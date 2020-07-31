using BiliWeb2.Models;
using BiliWeb2.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Backend
{
    /// <summary>
    /// Class that manages the overall data sources
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public class DataSourceBackend
    {
        /// <summary>
        /// Hold one of each of the DataSources as an instance to the datasource
        /// </summary>
        /// 

        // Add YourName Below Here  #1, at the top of the stack, not the bottom
        //public RoleBackend RoleBackend;
        //public UserBackend UserBackend;
        
        private DataSourceBackend()
        {
            // Add YourName Below Here #2, at the top of the stack, not the bottom
            //RoleBackend = new RoleBackend(1);
            //UserBackend = new UserBackend(1);            
        }

        /// <summary>
        /// Call for all data sources to reset
        /// </summary>
        public void Reset()
        {
            // Add YourName Below Here #3, at the top of the stack, not the bottom

            // Independent, do not depend on other data structures for example data
            //RoleBackend.Reset();
            //UserBackend.Reset();
            
            //SetTestingMode(false);
        }

        /// <summary>
        /// Call for all data sources to reset
        /// </summary>
        public void RestoreDefaultData()
        {
            // Add YourName Below Here #3, at the top of the stack, not the bottom
            //RoleBackend.RestoreDefaultData();
            //UserBackend.RestoreDefaultData();

            //// Call for Navigation to reset to rebuild the cache
            //NavigationHelper.ResetCache();

            //SetTestingMode(false);
        }

        /// <summary>
        /// Change between demo, default, and UT data sets
        /// </summary>
        /// <param name="SetEnum"></param>
        public static void SetDataSourceDataSet(DataSourceDataSetEnum SetEnum)
        {

            // Remember the Settigns
            SystemGlobalsModel.SetDataSourceDataSetEnum(SetEnum);

            // Add YourName Below Here #4, at the top of the stack, not the bottom
            //RoleBackend.SetDataSourceDataSet(SetEnum);
            //UserBackend.SetDataSourceDataSet(SetEnum);
            
            // Call for Navigation to reset to rebuild the cache
            //NavigationHelper.ResetCache();
        }

        /// <summary>
        /// Changes the data source, does not call for a reset, that allows for hot swapping but keeping the original data in place
        /// </summary>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            // Set the Global DataSourceEnum Value

            SystemGlobalsModel.SetDataSourceEnum(dataSourceEnum);

            // Add YourName Below Here #5, at the top of the stack, not the bottom
            //RoleBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
            //UserBackend.SetDataSource(SystemGlobalsModel.Instance.DataSourceValue);
            
            // Call for Navigation to reset to rebuild the cache
            //NavigationHelper.ResetCache();
        }
    }
}
