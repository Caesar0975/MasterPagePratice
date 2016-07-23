using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SortSelector : System.Web.UI.UserControl
{

    //每取到的項目值
    public string SelectedValue
    {
        get { return this.ddlSort.SelectedValue; }
        set { this.ddlSort.SelectedValue = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //處理年下拉選單產生            
        for (int i = 0; i <= 200; i++)
        {
            ListItem SortListItem = new ListItem();
            SortListItem.Text = i.ToString();
            SortListItem.Value = i.ToString();

            this.ddlSort.Items.Add(SortListItem);
        }
    }
}