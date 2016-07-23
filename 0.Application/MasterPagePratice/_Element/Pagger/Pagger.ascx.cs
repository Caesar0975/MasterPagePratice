using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class _Element_Pagger_Pagger : System.Web.UI.UserControl
{

    public int PageIndex { get { return Convert.ToInt32(ViewState["PageIndex"]); } set { ViewState["PageIndex"] = value; } }
    public int PageSize { get { return Convert.ToInt32(ViewState["PageSize"]); } set { ViewState["PageSize"] = value; } }
    public int DataAmount { get { return Convert.ToInt32(ViewState["TotalAmount"]); } set { ViewState["TotalAmount"] = value; } }

    //最大頁碼
    public int PageMaxNumber
    {
        get { if (ViewState["PageMaxNumber"] == null) { ViewState["PageMaxNumber"] = 19; } return Convert.ToInt32(ViewState["PageMaxNumber"]); }
        set { ViewState["PageMaxNumber"] = value; }
    }

    public int DataStartIndex { get { return ((this.PageIndex - 1) * this.PageSize) + 1; } }
    public int DataEndIndex { get { return this.PageIndex * this.PageSize; } }

    public Queue PaggerQueue;

    public int PageCount
    {
        get
        {
            if (this.DataAmount < PageSize) { return 1; }

            int Counter = this.DataAmount / this.PageSize;
            if ((this.DataAmount % this.PageSize) > 0) { Counter += 1; }
            return Counter;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        { this.PageIndex = 1; }
    }

    //第一頁
    protected void btnFirstPage_Click(object sender, EventArgs e)
    { this.PageIndex = 1; }

    //最末頁
    protected void btnLastPage_Click(object sender, EventArgs e)
    { this.PageIndex = this.PageCount; }

    //上一頁
    protected void btnPreviousPage_Click(object sender, EventArgs e)
    {
        this.PageIndex -= 1;
        if (this.PageIndex < 1) { this.PageIndex = 1; }
    }

    //依照PageIndex跳頁
    protected void PageIndex_Click(object sender, EventArgs e)
    {
        LinkButton lkn = (LinkButton)sender;
        PageIndex = int.Parse(lkn.CommandArgument);
    }

    //下一頁
    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        this.PageIndex += 1;
        if (this.PageIndex > this.PageCount) { this.PageIndex = this.PageCount; }
    }

    protected void RepeaterPagger_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int Index = (int)e.Item.DataItem;

            LinkButton lknIndex = (LinkButton)e.Item.FindControl("lknIndex");
            Label lblSeparate = (Label)e.Item.FindControl("lblSeparate");

            lknIndex.Text = Index.ToString();
            lknIndex.CommandArgument = Index.ToString();

            if (Index == this.PageIndex)
            { lknIndex.CssClass += " Selected "; }

            //最後一個不顯示"/"
            if (e.Item.ItemIndex + 1 == PaggerQueue.Count) { lblSeparate.Visible = false; }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //設定PaggerQueue的內容
        OptionPaggerQueue();

        this.RepeaterPagger.DataSource = PaggerQueue;
        this.RepeaterPagger.DataBind();

        //設定Buttion的作用
        OptionButton();
    }


    private void OptionPaggerQueue()
    {
        PaggerQueue = new Queue();

        //將到目前的PageIndex皆存到Queue
        for (int i = 1; i <= PageIndex; i++)
        { PaggerQueue.Enqueue(i); }

        int Counter;

        Counter = PageIndex + PageMaxNumber / 2;
        if (Counter >= PageCount) { Counter = PageCount; }

        //處理新增的Queue並更新Queue裡最後一筆的值
        for (int i = PageIndex + 1; i <= Counter; i++)
        { PaggerQueue.Enqueue(i); }

        //若超過最大頁數則移除前端的項目
        while (PaggerQueue.Count > PageMaxNumber)
        { PaggerQueue.Dequeue(); }
    }

    private void OptionButton()
    {
        this.btnFirstPage.Visible = true;
        this.btnPreviousPage.Visible = true;
        this.btnNextPage.Visible = true;
        this.btnLastPage.Visible = true;

        if (PageIndex == 1)
        {
            this.btnFirstPage.Visible = false;
            this.btnPreviousPage.Visible = false;
        }

        if (PageIndex == PageCount)
        {
            this.btnNextPage.Visible = false;
            this.btnLastPage.Visible = false;
        }

        //若資料為空，則將整個Panel隱藏
        if (DataAmount == 0 || DataAmount < PageSize) { this.Element_Pagger.Visible = false; }
        else { this.Element_Pagger.Visible = true; }
    }

}