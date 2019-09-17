Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon

Namespace Dashboard_CreatePivot
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Function CreatePivot(ByVal dataSource As IDashboardDataSource) As PivotDashboardItem
			Dim pivot As New PivotDashboardItem()
			pivot.DataSource = dataSource
			pivot.Columns.AddRange(New Dimension("Country"), New Dimension("Sales Person"))
			pivot.Rows.AddRange(New Dimension("CategoryName"), New Dimension("ProductName"))
			pivot.Values.AddRange(New Measure("Extended Price"), New Measure("Quantity"))
			pivot.AutoExpandColumnGroups = False
			pivot.AutoExpandRowGroups = True
			Return pivot
		End Function
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

			Dim dashboard As New Dashboard()
			Dim dataSource As New DashboardExcelDataSource() With {.FileName = "SalesPerson.xlsx", .SourceOptions = New DevExpress.DataAccess.Excel.ExcelSourceOptions(New DevExpress.DataAccess.Excel.ExcelWorksheetSettings() With {.WorksheetName = "Data", .CellRange = "A1:L100"})}
			dataSource.Fill()
			dashboard.DataSources.Add(dataSource)

			Dim pivot As PivotDashboardItem = CreatePivot(dataSource)
			dashboard.Items.Add(pivot)

			dashboardViewer1.Dashboard = dashboard
			dashboardViewer1.ReloadData()
		End Sub
	End Class
End Namespace
