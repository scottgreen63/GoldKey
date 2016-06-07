using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net;
using System.Net.Mail;
using System;
using System.Web.UI;
using GoldKeyLib;

namespace GoldKeyWeb
{
    public partial class PreReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        GoldKeyLib.Entities.Customer newcustomer = new GoldKeyLib.Entities.Customer();


    protected void btnSubmit_Click(object sender, EventArgs e)

    {

        MailMessage msg;
        SqlCommand cmd = new SqlCommand();
        string ActivationUrl = string.Empty;
        string emailId = string.Empty;

        try

        {
                newcustomer.CustomerID = "0";
                newcustomer.FirstName = txtFirstName.Text;
                newcustomer.LastName = txtLastName.Text;
                newcustomer.Address1 = "test1";
                newcustomer.Address2 = "test2";
                newcustomer.City = "PDX";
                newcustomer.State = "OR";
                newcustomer.ZipCode = "97201";
                newcustomer.ContactEmail = txtEmailId.Text;

                newcustomer.AddCustomer(0);

                           
                //Sending activation link in the email

            msg = new MailMessage();

            SmtpClient smtp = new SmtpClient();
                emailId = "scottgreen@portofportland.com";
            //sender email address

            msg.From = new MailAddress("GoldKey@portofportland.com");

            //Receiver email address
            msg.To.Add(emailId);
            msg.Subject = "Confirmation email for pending GoldKey customer ";

            //For testing replace the local host path with your lost host path and while making online replace with your website domain name

            ActivationUrl = Server.HtmlEncode("http://localhost:49184/ActivateCustomer.aspx?CustomerID=" + newcustomer.CustomerID);

                msg.Body = " New  GoldKey customer " + txtFirstName.Text.Trim() + " " +txtLastName.Text.Trim()+  " pending approval !\n" + 

                 " Please <a href='" + ActivationUrl + "'>click here to approve this customer</a> . \nThanks!";

            msg.IsBodyHtml = true;

            //smtp.Credentials = new NetworkCredential(    );
            smtp.Host = "outlook.pop.portptld.com";
            smtp.Send(msg);

            clear_controls();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Confirmation Link to activate your account has been sent to your email address');", true);

        }

        catch (Exception ex)

        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);

            return;

        }

        finally

        {

            ActivationUrl = string.Empty;

            emailId = string.Empty;

            

        }

    }




   


    private void clear_controls()
    {
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtEmailId.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtFirstName.Focus();
    }

    }
}