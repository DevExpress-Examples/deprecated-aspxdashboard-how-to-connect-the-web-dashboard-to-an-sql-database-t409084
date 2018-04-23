Imports System
Imports System.Web.Hosting
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql

Namespace WebDesignerSqlDataSource
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim dashboardFileStorage As New DashboardFileStorage("~/App_Data/Dashboards")
            ASPxDashboard1.SetDashboardStorage(dashboardFileStorage)

            Dim sqlDataSource As New DashboardSqlDataSource("SQL Data Source", "access97Connection")
            Dim query As SelectQuery = SelectQueryFluentBuilder.
                AddTable("SalesPerson").
                SelectAllColumns().
                Build("Sales Person")
            sqlDataSource.Queries.Add(query)

            Dim dataSourceStorage As New DataSourceInMemoryStorage()
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml())
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage)
        End Sub

        Protected Sub ASPxDashboard1_ConfigureDataConnection(ByVal sender As Object,
                                                             ByVal e As ConfigureDataConnectionWebEventArgs)
            If e.ConnectionName = "access97Connection" Then
                Dim access97Params As New Access97ConnectionParameters()
                access97Params.FileName = HostingEnvironment.MapPath("~/App_Data/nwind.mdb")
                e.ConnectionParameters = access97Params
            End If
        End Sub
    End Class
End Namespace