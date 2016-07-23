using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DateSelector : System.Web.UI.UserControl
{
    public int Year
    {
        get { return int.Parse(this.ddlYear.SelectedValue); }
        set { this.ddlYear.SelectedValue = value.ToString(); }
    }

    public int Month
    {
        get { return int.Parse(this.ddlMonth.SelectedValue); }
        set { this.ddlMonth.SelectedValue = value.ToString(); }
    }

    public int Day
    {
        get { return int.Parse(this.ddlDay.SelectedValue); }
        set { this.ddlDay.SelectedValue = value.ToString(); }
    }

    public DateTime SelectedDateTime
    {
        get
        {
            DateTime SelectedDateTime;
            if (DateTime.TryParse(this.ddlYear.SelectedValue + "/" + this.ddlMonth.SelectedValue + "/" + this.ddlDay.SelectedValue, out SelectedDateTime) == false)
            { throw new Exception("日期選擇不完全"); }

            return SelectedDateTime;
        }
        set
        {
            this.ddlYear.SelectedValue = value.Year .ToString();
            this.ddlMonth.SelectedValue = value.Month.ToString();
            this.ddlDay.SelectedValue = value.Day.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            //處理年下拉選單產生            
            for (int i = 1940; i <= 2030; i++)
            {
                ListItem YearListItem = new ListItem();
                YearListItem.Text = i.ToString();
                YearListItem.Value = i.ToString();
                this.ddlYear.Items.Add(YearListItem);
            }


            //處理月下拉選單產生
            for (int i = 1; i <= 12; i++)
            {
                ListItem ddlMonthListItem = new ListItem();
                ddlMonthListItem.Text = i.ToString();
                ddlMonthListItem.Value = i.ToString();
                this.ddlMonth.Items.Add(ddlMonthListItem);
            }

            //處理日下拉選單產生
            for (int i = 1; i <= 31; i++)
            {
                ListItem ddlDayListItem = new ListItem();
                ddlDayListItem.Text = i.ToString();
                ddlDayListItem.Value = i.ToString();
                this.ddlDay.Items.Add(ddlDayListItem);
            }

        }

    }
}