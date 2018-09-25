using PrestoPay.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class PrestoPayAnalysis : System.Web.UI.Page
    {

        protected void signOut(object sender, EventArgs e)
        {
            Session["Admin"] = null;
            Session["AdminName"] = null;
            Response.Redirect("~/Login.aspx");


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["Admin"] == null || Session["AdminName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Nav_User.InnerText = Session["AdminName"].ToString();
                
            }
            


            if (!IsPostBack)
            {

                ViewState["Chart"] = "Business_Line";
                populateBizLine();
                populatePicker();
                Panel_Date.Visible = false;
                var da = new ChartDAO();
               Label_biz.Text = da.GetNoOfBusinesses().ToString();
                Label_pers.Text =  da.GetNoOfPersonal().ToString();
                Label_indus.Text = da.GetNoOfIndustry().ToString();
                



            }
            else
            {
                



            }


        }

        protected void Chart_Dynamic_Click(object sender, ImageMapEventArgs e)
        {

            //Response.Write(e.PostBackValue.ToString());

            if (ViewState["Chart"].ToString() == "Business_Line")
            {
                ViewState["Year"] = e.PostBackValue.ToString();
                populateBizBar(e.PostBackValue.ToString());
                ViewState["Chart"] = "Business_Bar";

            }
            else if (ViewState["Chart"].ToString() == "Industry_Pie_Count")
            {
                ViewState["Industry"] = e.PostBackValue.ToString();
                populateIndustryPayment(e.PostBackValue.ToString());
                ViewState["Chart"] = "Industry_Pie_Payment";
            }


        }

        private void populatePersonalSpendingCategory()
        {

            string monthYear = DropDownList_Month.SelectedValue + "/" + DropDownList_Year.SelectedValue;
            var da = new ChartDAO();
            Series series = Chart_Dynamic.Series[0];
            ChartArea ch = Chart_Dynamic.ChartAreas[0];
            DataPointCollection dpc = series.Points;
            dpc.Clear();
            Chart_Dynamic.Titles[0].Text = "Presto Pay Personal Users Spending Categories for the month " + monthYear;
            ch.AxisX.Title = "";
            ch.AxisY.Title = "Amount (SGD)";
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.PostBackValue = "";
            series.LegendText = "#VALX";
            series.XValueMember = "#PERCENT{P1}";
            series.Label = "#PERCENT{P1}";
            Chart_Dynamic.Legends[0].Enabled = true;
            //series.LabelFormat = "SGD $###,###.00";
            series.LabelBackColor = System.Drawing.Color.White;
            //string[] mnths = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            var dt = da.GetCategoryGroupingForMonth(DropDownList_Month.SelectedValue, DropDownList_Year.SelectedValue);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dpc.AddXY(dt.Rows[i]["budgetCategory"].ToString(), Convert.ToInt32(dt.Rows[i]["amt_total"]));
            }

            fillPersonal(null, null);
        }




        private void populateIndustryPayment(string industry)
        {
            ViewState["Industry"] = industry;
            string monthYear = DropDownList_Month.SelectedValue + "/" + DropDownList_Year.SelectedValue;
            var da = new ChartDAO();
            Series series = Chart_Dynamic.Series[0];
            ChartArea ch = Chart_Dynamic.ChartAreas[0];
            DataPointCollection dpc = series.Points;
            dpc.Clear();
            Chart_Dynamic.Titles[0].Text = "Payment methods used by " + industry + " Industry Customers for the month " + monthYear;
            //ch.AxisX.Title = "Month";
            ch.AxisY.Title = "Amount (SGD)";
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.PostBackValue = "";
            series.LegendText = "#VALX";
            series.XValueMember = "#PERCENT{P1}";
            series.Label = "#PERCENT{P1}";
            Chart_Dynamic.Legends[0].Enabled = true;
            series.LabelFormat = "SGD $###,###.00";
            series.LabelBackColor = System.Drawing.Color.White;
            //string[] mnths = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            var dt = da.GetPaymentByIndustryAndDate(DropDownList_Month.SelectedValue, DropDownList_Year.SelectedValue, industry);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dpc.AddXY(dt.Rows[i]["trans_type"].ToString(), Convert.ToInt32(dt.Rows[i]["amt_total"]));
            }

            fillIndustryPayment(null, null);

        }



        private void populateIndustryPie()
        {
            string monthYear = DropDownList_Month.SelectedValue + "/" + DropDownList_Year.SelectedValue;
            var da = new ChartDAO();
            Series series = Chart_Dynamic.Series[0];
            ChartArea ch = Chart_Dynamic.ChartAreas[0];
            DataPointCollection dpc = series.Points;
            dpc.Clear();
            Chart_Dynamic.Titles[0].Text = "Receipts Presto Pay Enterprises by Industries for the month " + monthYear;
            ch.AxisX.Title = "Industry";
            ch.AxisY.Title = "Amount (SGD)";
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.PostBackValue = "#VALX";
            series.Label = "#VALY";
            series.LegendText = "#VALX";
            series.XValueMember = "";
            series.Label = "";
            Chart_Dynamic.Legends[0].Enabled = false;
            series.LabelFormat = "SGD $###,###.00";
            series.LabelBackColor = System.Drawing.Color.White;
            //string[] mnths = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            var dt = da.GetIndustryAmountByMY(DropDownList_Month.SelectedValue, DropDownList_Year.SelectedValue);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dpc.AddXY(dt.Rows[i]["busi_category"].ToString(), Convert.ToInt32(dt.Rows[i]["amt_total"]));
            }

            fillIndustryPie(null, null);

        }


        private void fillIndustryPie(string direction, string field)
        {
            var da = new ChartDAO();
            var gvdt = da.GV_IndustryPie(DropDownList_Month.SelectedValue, DropDownList_Year.SelectedValue);
            GridView_PieCount.Columns.Clear();
            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "ID", DataField = "busi_id", SortExpression = "busi_id" });
            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Enterprise Name", DataField = "busi_companyName", SortExpression = "busi_companyName" });
            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Industry", DataField = "busi_category", SortExpression = "busi_category" });
            var bf = new BoundField() { HeaderText = "Total Transaction Amount  (SGD)", DataField = "amt_total", SortExpression = "amt_total", DataFormatString = "{0:C}" };
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            bf.ItemStyle.CssClass = "GV_right";
            GridView_PieCount.Columns.Add(bf);

            if (direction != null && field != null)
            {
                GridView_PieCount.Attributes["CurrentSortField"] = field;
                GridView_PieCount.Attributes["CurrentSortDirection"] = direction;
                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = field + " " + direction;
                GridView_PieCount.DataSource = dv;
                GridView_PieCount.DataBind();

            }
            else
            {
                GridView_PieCount.Attributes["CurrentSortField"] = "amt_total";
                GridView_PieCount.Attributes["CurrentSortDirection"] = "ASC";
                GridView_PieCount.DataSource = gvdt;
                GridView_PieCount.DataBind();
            }



        }


        private void fillIndustryPayment(string direction, string field)
        {
            var da = new ChartDAO();
            var gvdt = da.GV_IndustryPay(DropDownList_Month.SelectedValue, DropDownList_Year.SelectedValue, ViewState["Industry"].ToString());
            GridView_PieCount.Columns.Clear();

            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Payment Method", DataField = "trans_type", SortExpression = "trans_type" });
            var bf = new BoundField() { HeaderText = "Total Amount (SGD)", DataField = "amt_total", SortExpression = "amt_total", DataFormatString = "{0:C}" };
            bf.ItemStyle.CssClass = "GV_right";
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            GridView_PieCount.Columns.Add(bf);
           


            if (direction != null && field != null)
            {
                GridView_PieCount.Attributes["CurrentSortField"] = field;
                GridView_PieCount.Attributes["CurrentSortDirection"] = direction;
                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = field + " " + direction;
                GridView_PieCount.DataSource = dv;
                GridView_PieCount.DataBind();

            }
            else
            {
                GridView_PieCount.Attributes["CurrentSortField"] = "amt_total";
                GridView_PieCount.Attributes["CurrentSortDirection"] = "ASC";
                GridView_PieCount.DataSource = gvdt;
                GridView_PieCount.DataBind();
            }

        }

        private void fillBusiLine(string direction, string field)
        {
            var da = new ChartDAO();
            var gvdt = da.GV_TransCount();
            GridView_PieCount.Columns.Clear();

            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Year", DataField = "date_year", SortExpression = "date_year" });
            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Total Number of Transactions", DataField = "amt_total", SortExpression = "amt_total" });


            if (direction != null && field != null)
            {
                GridView_PieCount.Attributes["CurrentSortField"] = field;
                GridView_PieCount.Attributes["CurrentSortDirection"] = direction;
                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = field + " " + direction;
                GridView_PieCount.DataSource = dv;
                GridView_PieCount.DataBind();

            }
            else
            {
                GridView_PieCount.Attributes["CurrentSortField"] = "date_year";
                GridView_PieCount.Attributes["CurrentSortDirection"] = "DESC";
                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = "date_year" + " " + "DESC";
                GridView_PieCount.DataSource = gvdt;
                GridView_PieCount.DataBind();
            }
        }

        private void fillPersonal(string direction, string field)
        {
            var da = new ChartDAO();
            var gvdt = da.GV_Personal(DropDownList_Month.SelectedValue, DropDownList_Year.SelectedValue);
            GridView_PieCount.Columns.Clear();
            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Budget Category", DataField = "budgetCategory", SortExpression = "budgetCategory" });
            var bf = new BoundField() { HeaderText = "Total Amount (SGD)", DataField = "amt_total", SortExpression = "amt_total", DataFormatString = "{0:C}" };
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            bf.ItemStyle.CssClass = "GV_right";
            GridView_PieCount.Columns.Add(bf);


            if (direction != null && field != null)
            {
                GridView_PieCount.Attributes["CurrentSortField"] = field;
                GridView_PieCount.Attributes["CurrentSortDirection"] = direction;
                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = field + " " + direction;
                GridView_PieCount.DataSource = dv;
                GridView_PieCount.DataBind();

            }
            else
            {
                GridView_PieCount.Attributes["CurrentSortField"] = "amt_total";
                GridView_PieCount.Attributes["CurrentSortDirection"] = "ASC";
                GridView_PieCount.DataSource = gvdt;
                GridView_PieCount.DataBind();
            }
        }

        private void populateBizBar(string year)
        {

            var da = new ChartDAO();
            Series series = Chart_Dynamic.Series[0];
            ChartArea ch = Chart_Dynamic.ChartAreas[0];
            DataPointCollection dpc = series.Points;
            dpc.Clear();
            Chart_Dynamic.Titles[0].Text = "Presto Pay Enterprises Monthly transactions for the year "+year;
            ch.AxisX.Title = "Month";
            ch.AxisY.Title = "Amount (SGD)";
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            series.Label = "";
            series.PostBackValue = "";
            series.LabelFormat = "SGD $###,###.00";
            series.LabelBackColor = System.Drawing.Color.White;
            Chart_Dynamic.Legends[0].Enabled = false;
            string[] mnths = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            var dt = da.GetPrestoMonthProfitsOfYear(year);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dpc.AddXY(mnths[Convert.ToInt32(dt.Rows[i]["date_month"])], Convert.ToDouble(dt.Rows[i]["amt_total"].ToString()));
            }

            fillBizMonthCount(null, null, ViewState["Year"].ToString());

        }

        private void fillBizMonthCount(string direction, string field, string year)
        {
            var da = new ChartDAO();
            var gvdt = da.GV_TransMonthCount(year);
            DataTable ndt = new DataTable();
            GridView_PieCount.Columns.Clear();
            string[] mnths = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Month", DataField = "date_month", SortExpression = "date_month" });

            GridView_PieCount.Columns.Add(new BoundField() { HeaderText = "Number of Transactions", DataField = "amt_total", SortExpression = "amt_total" });


        
            if (direction != null && field != null)
            {

                DataView ndv = new DataView();
                GridView_PieCount.Attributes["CurrentSortField"] = field;
                GridView_PieCount.Attributes["CurrentSortDirection"] = direction;

                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = field + " " + direction;
                //dv.Table.Columns[1].DataType = typeof(String);
                DataTable dt = new DataTable();
                dt.DefaultView.ApplyDefaultSort = false;
                ndv.ApplyDefaultSort = false;
                dt.Columns.Add("date_month", typeof(string));
                dt.Columns.Add("amt_total", typeof(int));
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["date_month"] = mnths[Convert.ToInt32(dv.Table.Rows[i]["date_month"])];
                    dr["amt_total"] = Convert.ToInt32(dv.Table.Rows[i]["amt_total"]);
                    dt.Rows.Add(dr);
                }
                ndv = dt.DefaultView;



                GridView_PieCount.DataSource = ndv;
                GridView_PieCount.DataBind();

            }
            else
            {
                GridView_PieCount.Attributes["CurrentSortField"] = "date_month";
                GridView_PieCount.Attributes["CurrentSortDirection"] = "DESC";
                DataView ndv = new DataView();
                DataView dv;
                dv = gvdt.DefaultView;
                dv.Sort = "date_month" + " " + "DESC";
                DataTable dt = new DataTable();
                dt.DefaultView.ApplyDefaultSort = false;
                ndv.ApplyDefaultSort = false;
                dt.Columns.Add("date_month", typeof(string));
                dt.Columns.Add("amt_total", typeof(int));
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["date_month"] = mnths[Convert.ToInt32(dv.Table.Rows[i]["date_month"])];
                    dr["amt_total"] = Convert.ToInt32(dv.Table.Rows[i]["amt_total"]);
                    dt.Rows.Add(dr);

                }
                ndv = dt.DefaultView;
               
                GridView_PieCount.DataSource = ndv;
                GridView_PieCount.DataBind();
            }


        }


        private void populateBizLine()
        {

            var da = new ChartDAO();
            Series series = Chart_Dynamic.Series[0];
            ChartArea ch = Chart_Dynamic.ChartAreas[0];
            DataPointCollection dpc = series.Points;
            dpc.Clear();
            Chart_Dynamic.Titles[0].Text = "Total Annual Transactions of Presto Pay Enterprises";
            ch.AxisX.Title = "Year";
            ch.AxisY.Title = "Amount (SGD)";
            series.ChartType = SeriesChartType.Line;
            series.IsValueShownAsLabel = true;
            series.Label = "";
            series.PostBackValue = "#VALX";
            series.LabelFormat = "SGD $###,###.00";
            Chart_Dynamic.Legends[0].Enabled = false;
            series.LabelBackColor = System.Drawing.Color.White;

            var dt = da.GetPrestoAnnualProfits();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dpc.AddXY(dt.Rows[i]["date_year"].ToString(), Convert.ToDouble(dt.Rows[i]["amt_total"].ToString()));
            }

            fillBusiLine(null, null);

        }



        protected void Button_Ent_Click(object sender, EventArgs e)
        {

            ViewState["Chart"] = "Business_Line";
            currentState();
            Panel_Date.Visible = false;

        }

        protected void Button_Indus_Click(object sender, EventArgs e)
        {

            ViewState["Chart"] = "Industry_Pie_Count";
            currentState();
            Panel_Date.Visible = true;
        }

        protected void Button_Pers_Click(object sender, EventArgs e)
        {
            ViewState["Chart"] = "PersonalSpending";
            currentState();
            Panel_Date.Visible = true;
        }

        private void currentState()
        {
            if (ViewState["Chart"].ToString() == "Industry_Pie_Count")
            {
                populateIndustryPie();

            }
            else if (ViewState["Chart"].ToString() == "Business_Line")
            {

                populateBizLine();
            }
            else if (ViewState["Chart"].ToString() == "PersonalSpending")
            {

                populatePersonalSpendingCategory();
            }
            else if (ViewState["Chart"].ToString() == "Business_Bar")
            {

                populateBizBar(ViewState["Year"].ToString());
            }
            else if (ViewState["Chart"].ToString() == "Industry_Pie_Payment")
            {
                populateIndustryPayment(ViewState["Industry"].ToString());
            }




        }



        protected void DropDownList_Year_SelectedIndexChanged(object sender, EventArgs e)
        {


            refreshDays();
            currentState();
        }




        protected void DropDownList_Month_SelectedIndexChanged(object sender, EventArgs e)
        {

            currentState();
        }





        private void populatePicker()
        {
            DateTime now = DateTime.Now;
            int endyear = 2015;
            int currentYear = now.Year;

            while (currentYear >= endyear)
            {
                DropDownList_Year.Items.Add(new ListItem(currentYear.ToString()));
                currentYear--;

            }

            refreshDays();

        }


        private void refreshDays()
        {
            DateTime now = DateTime.Now;
            int currentMonth = now.Month;

            if (Convert.ToInt32(DropDownList_Year.SelectedValue) == now.Year)
            {
                DropDownList_Month.Items.Clear();
                while (currentMonth != 0)
                {
                    if (currentMonth < 10)
                        DropDownList_Month.Items.Add(new ListItem("0" + currentMonth.ToString()));
                    else
                        DropDownList_Month.Items.Add(new ListItem(currentMonth.ToString()));

                    currentMonth--;
                }



            }
            else
            {
                DropDownList_Month.Items.Clear();
                for (int i = 12; i > 0; i--)
                {
                    if (i < 10)
                        DropDownList_Month.Items.Add(new ListItem("0" + i.ToString()));
                    else
                        DropDownList_Month.Items.Add(new ListItem(i.ToString()));
                }
            }

        }

        protected void GridView_PieCount_Sorting(object sender, GridViewSortEventArgs e)
        {

            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = "";
            string strSortDirection = "";

            SortGridview(GridView_PieCount, e, out sortDirection, out sortField);
            strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";
            if (ViewState["Chart"].ToString() == "Industry_Pie_Count")
            {



                fillIndustryPie(strSortDirection, e.SortExpression);


            }
            else if (ViewState["Chart"].ToString() == "Business_Line")
            {

                fillBusiLine(strSortDirection, e.SortExpression);
            }
            else if (ViewState["Chart"].ToString() == "PersonalSpending")
            {

                fillPersonal(strSortDirection, e.SortExpression);
            }
            else if (ViewState["Chart"].ToString() == "Business_Bar")
            {

                fillBizMonthCount(strSortDirection, e.SortExpression, ViewState["Year"].ToString());
            }
            else if (ViewState["Chart"].ToString() == "Industry_Pie_Payment")
            {
                fillIndustryPayment(strSortDirection, e.SortExpression);
            }

            //Response.Write( strSortDirection+ " " + e.SortExpression);
        }


        private void SortGridview(GridView gridView, GridViewSortEventArgs e, out SortDirection sortDirection, out string sortField)
        {
            sortField = e.SortExpression;
            sortDirection = e.SortDirection;
            if (gridView.Attributes["CurrentSortField"] != null && gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else {
                        sortDirection = SortDirection.Ascending;
                    }
            }
            gridView.Attributes["CurrentSortField"] = sortField;
            gridView.Attributes["CurrentSortDirection"] = (sortDirection == SortDirection.Ascending ? "ASC" : "DESC");
        }
    


        protected void GridView_PieCount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
       
            GridView_PieCount.PageIndex = e.NewPageIndex;
          
            if (ViewState["Chart"].ToString() == "Industry_Pie_Count")
            {
                fillIndustryPie(GridView_PieCount.Attributes["CurrentSortDirection"], GridView_PieCount.Attributes["CurrentSortField"]);
            }
            else if (ViewState["Chart"].ToString() == "Business_Line")
            {
                fillBusiLine(GridView_PieCount.Attributes["CurrentSortDirection"], GridView_PieCount.Attributes["CurrentSortField"]);
                
            }
            else if (ViewState["Chart"].ToString() == "PersonalSpending")
            {
                fillPersonal(GridView_PieCount.Attributes["CurrentSortDirection"], GridView_PieCount.Attributes["CurrentSortField"]);
              
            }
            else if (ViewState["Chart"].ToString() == "Business_Bar")
            {
                fillBizMonthCount(GridView_PieCount.Attributes["CurrentSortDirection"], GridView_PieCount.Attributes["CurrentSortField"], ViewState["Year"].ToString());

            }
            else if (ViewState["Chart"].ToString() == "Industry_Pie_Payment")
            {
                fillIndustryPayment(GridView_PieCount.Attributes["CurrentSortDirection"], GridView_PieCount.Attributes["CurrentSortField"]);
            }


        }
    }
}