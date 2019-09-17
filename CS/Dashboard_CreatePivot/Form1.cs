using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;

namespace Dashboard_CreatePivot {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private PivotDashboardItem CreatePivot(IDashboardDataSource dataSource) {
            PivotDashboardItem pivot = new PivotDashboardItem();
            pivot.DataSource = dataSource;
            pivot.Columns.AddRange(new Dimension("Country"), new Dimension("Sales Person"));
            pivot.Rows.AddRange(new Dimension("CategoryName"), new Dimension("ProductName"));
            pivot.Values.AddRange(new Measure("Extended Price"), new Measure("Quantity"));
            pivot.AutoExpandColumnGroups = false;
            pivot.AutoExpandRowGroups = true;
            return pivot;
        }
        private void Form1_Load(object sender, EventArgs e) {

            Dashboard dashboard = new Dashboard();
            DashboardExcelDataSource dataSource = new DashboardExcelDataSource()
            {
                FileName = "SalesPerson.xlsx",
                SourceOptions = new DevExpress.DataAccess.Excel.ExcelSourceOptions(
                    new DevExpress.DataAccess.Excel.ExcelWorksheetSettings()
                    {
                        WorksheetName = "Data",
                        CellRange = "A1:L100"
                    }
                    )
            };
            dataSource.Fill();
            dashboard.DataSources.Add(dataSource);

            PivotDashboardItem pivot = CreatePivot(dataSource);
            dashboard.Items.Add(pivot);

            dashboardViewer1.Dashboard = dashboard;
            dashboardViewer1.ReloadData();
        }
    }
}
