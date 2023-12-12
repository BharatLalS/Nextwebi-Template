using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

public partial class Admin_dashboard : System.Web.UI.Page
{
    SqlConnection conSQ = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ConnectionString);
    public string strTotalArct = "", StrTotalOrders="", Strusername="", StrCustomerCnt="", StrRequestcall="", strOrders="", StrPresentdate="";
    public string strTotalSales = "", strOpenTickets = "", StrTdyOrd = "", strTotalOrders = "", strLast10Sales = "", strUserName = "", strProfileImage = "",StrWish="";

    protected void Page_Load(object sender, EventArgs e)
    {//check if admin login is valid
        if (Request.Cookies["sq_aaid"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        getTime();
        BindUserName();
        StrCustomerCnt = DashBoard.GetCustomerCount(conSQ).ToString();
        strTotalArct = DashBoard.GetArchitectCount(conSQ).ToString();
        DateTime date = TimeStamps.UTCTime();
        StrPresentdate = date.ToString("dd-MMM-yyyy");
    }


    public void getTime()
    {
        try
        {
            var mydate = TimeStamps.UTCTime();
            var time = mydate.ToString("HH"); // 23:58

            if (Convert.ToInt32(time) < 11)
            {
                StrWish = "Good Morning";
            }
            else if (Convert.ToInt32(time) > 12 &&Convert.ToInt32(time)<15)
            {
                StrWish = "Good AfterNoon";

            }else if (Convert.ToInt32(time) > 16 && Convert.ToInt32(time) < 23)
            {
                StrWish = "Good Evening";

            }
            else if(Convert.ToInt32(time) >=24)
            {
                StrWish = "Good Night";

            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "getTime", ex.Message);
        }
    }

    public void BindUserName()
    {
        try
        {
            Strusername = CreateUser.GetLoggedUserName(conSQ, Request.Cookies["sq_aaid"].Value);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindUserName", ex.Message);
        }
    }
}