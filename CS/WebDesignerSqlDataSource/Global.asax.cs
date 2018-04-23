using System;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DashboardCommon;
using System.Web.Hosting;
using DevExpress.DataAccess.Sql;

namespace WebDesignerSqlDataSource
{
    public class Global_asax : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            #region #DashboardFileStorage
            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
            DashboardService.SetDashboardStorage(dashboardFileStorage);
            #endregion

            #region #DashboardSqlDataSource
            DashboardSqlDataSource sqlDataSource =
                new DashboardSqlDataSource("SQL Data Source", "access97Connection");
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("SalesPerson")
                .SelectAllColumns()
                .Build("Sales Person");
            sqlDataSource.Queries.Add(query);
            #endregion

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());
            DashboardService.SetDataSourceStorage(dataSourceStorage);

            DashboardService.DataApi.ConfigureDataConnection += DataApi_ConfigureDataConnection;
        }

        private void DataApi_ConnectionError(object sender, ServiceConnectionErrorEventArgs e)
        {

        }

        private void DataApi_ConfigureDataConnection(object sender,
            ServiceConfigureDataConnectionEventArgs e)
        {
            if (e.ConnectionName == "access97Connection")
            {
                Access97ConnectionParameters access97Params = new Access97ConnectionParameters();
                access97Params.FileName = HostingEnvironment.MapPath(@"~/App_Data/nwind.mdb");
                e.ConnectionParameters = access97Params;
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}