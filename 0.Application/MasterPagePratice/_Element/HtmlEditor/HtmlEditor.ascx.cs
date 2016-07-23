using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HtmlEditor_HtmlEditor : System.Web.UI.UserControl
{
    public int Height { get { return (int)ViewState["CkEditor.Height"]; } set { ViewState["CkEditor.Height"] = value; } }
    public int Width { get { return (int)ViewState["CkEditor.Width"]; } set { ViewState["CkEditor.Width"] = value; } }
    public string Content { get { return this.CkEditor1.Text; } set { this.CkEditor1.Text = value.Trim(); } }

    protected void Page_Init(object sender, EventArgs e)
    {
        this.Height = 300;
        this.Width = 600;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_InitialScript();
        Render_ToolBar();
    }

    private void Render_InitialScript()
    {
        if (this.Page.Header.FindControl("CkEditor") != null) { return; }

        Literal InitialScript = new Literal();
        InitialScript.ID = "CkEditor";
        InitialScript.Text = "\r\n<script type='text/javascript' src='/_Element/HtmlEditor/ckeditor/ckeditor.js'></script>\r\n";
        this.Page.Header.Controls.Add(InitialScript);
    }

    private void Render_ToolBar()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CkEditor" + this.CkEditor1.ClientID,
            "\r\n<script type='text/javascript'>CKEDITOR.replace('" + this.CkEditor1.ClientID + "', { toolbar:'Templete1', width:'" + Width + "px', height:'" + Height + "px', });</script>"
            , false);
    }
}