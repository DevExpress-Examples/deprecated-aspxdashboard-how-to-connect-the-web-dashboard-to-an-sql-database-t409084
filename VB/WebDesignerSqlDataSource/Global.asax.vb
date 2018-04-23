Imports System
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DashboardCommon
Imports System.Web.Hosting
Imports DevExpress.DataAccess.Sql

Namespace WebDesignerSqlDataSource
    Public Class Global_asax
        Inherits System.Web.HttpApplication

        Private Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
'            #Region "#DashboardFileStorage"
            Dim dashboardFileStorage As New DashboardFileStorage("~/App_Data/Dashboards")
            DashboardService.SetDashboardStorage(dashboardFileStorage)
'            #End Region

'            #Region "#DashboardSqlDataSource"
            Dim sqlDataSource As New DashboardSqlDataSource("SQL Data Source", "access97Connection")
            Dim query As SelectQuery = SelectQueryFluentBuilder.AddTable("SalesPerson").SelectAllColumns().Build("Sales Person")
            sqlDataSource.Queries.Add(query)
'            #End Region

            Dim dataSourceStorage As New DataSourceInMemoryStorage()
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml())
            DashboardService.SetDataSourceStorage(dataSourceStorage)

            AddHandler DashboardService.DataApi.ConfigureDataConnection, AddressOf DataApi_ConfigureDataConnection
        End Sub

        Private Sub DataApi_ConnectionError(ByVal sender As Object, ByVal e As ServiceConnectionErrorEventArgs)

        End Sub

        Private Sub DataApi_ConfigureDataConnection(ByVal sender As Object, ByVal e As ServiceConfigureDataConnectionEventArgs)
            If e.ConnectionName = "access97Connection" Then
                Dim access97Params As New Access97ConnectionParameters()
                access97Params.FileName = HostingEnvironment.MapPath("~/App_Data/nwind.mdb")
                e.ConnectionParameters = access97Params
            End If
        End Sub

        Private Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Code that runs on application shutdown
        End Sub

        Private Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
            ' Code that runs when an unhandled error occurs
        End Sub

        Private Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
            ' Code that runs when a new session is started
        End Sub

        Private Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Code that runs when a session ends. 
            ' Note: The Session_End event is raised only when the sessionstate mode
            ' is set to InProc in the Web.config file. If session mode is set to StateServer 
            ' or SQLServer, the event is not raised.
        End Sub
    End Class
End Namespace