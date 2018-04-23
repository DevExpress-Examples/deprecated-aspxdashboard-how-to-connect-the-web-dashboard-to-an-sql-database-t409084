using System;
using System.Web.Hosting;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;

namespace WebDesignerSqlDataSource
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
            ASPxDashboard1.SetDashboardStorage(dashboardFileStorage);

            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "access97Connection");
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("SalesPerson")
                .SelectAllColumns()
                .Build("Sales Person");
            sqlDataSource.Queries.Add(query);

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage);
        }

        protected void ASPxDashboard1_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.ConnectionName == "access97Connection") {
                Access97ConnectionParameters access97Params = new Access97ConnectionParameters();
                access97Params.FileName = HostingEnvironment.MapPath(@"~/App_Data/nwind.mdb");
                e.ConnectionParameters = access97Params;
            }
        }
    }
}