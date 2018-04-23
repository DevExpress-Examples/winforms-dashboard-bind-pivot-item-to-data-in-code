Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess

Namespace Dashboard_CreatePivot
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Function CreatePivot(ByVal dataSource As DashboardObjectDataSource) As PivotDashboardItem

            ' Creates a pivot dashboard item and specifies its data source.
            Dim pivot As New PivotDashboardItem()
            pivot.DataSource = dataSource

            ' Specifies dimensions that provide pivot column and row headers.
            pivot.Columns.AddRange(New Dimension("Country"), New Dimension("Sales Person"))
            pivot.Rows.AddRange(New Dimension("CategoryName"), New Dimension("ProductName"))

            ' Specifies measures whose data is used to calculate pivot cell values.
            pivot.Values.AddRange(New Measure("Extended Price"), New Measure("Quantity"))

            ' Specifies the default expanded state of pivot column field values.
            pivot.AutoExpandColumnGroups = True

            Return pivot
        End Function
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            ' Creates a dashboard and sets it as the currently opened dashboad in the dashboard viewer.
            dashboardViewer1.Dashboard = New Dashboard()

            ' Creates a data source and adds it to the dashboard data source collection.
            Dim dataSource As New DashboardObjectDataSource()
            dataSource.DataSource = (New nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData()
            dashboardViewer1.Dashboard.DataSources.Add(dataSource)

            ' Creates a pivot dashboard item with the specified data source 
            ' and adds it to the Items collection, to display it within the dashboard.
            Dim pivot As PivotDashboardItem = CreatePivot(dataSource)
            dashboardViewer1.Dashboard.Items.Add(pivot)

            ' Reloads data in the data sources.
            dashboardViewer1.ReloadData()
        End Sub
    End Class
End Namespace
