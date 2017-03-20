﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_Manager_NIT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string getMailListMembers = @"C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\getMailListMembers.ps1";
        string getMailListOwners = @"C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\getMailListOwners.ps1";
        string getMailListDL = @"C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\getMailListDL.ps1";

        string outputowner = @"C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\tmp\outputowner.txt";
        string outputmember = @"C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\tmp\outputmember.txt";
        string outputDistribution = @"C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\tmp\outputDistribution.txt";

        List<string> listOutPutOwner = new List<string>();
        List<string> listOutPutMember = new List<string>();
        List<string> listOutPutDL = new List<string>();

        /// <summary>
        /// Load_Page to load the page with or without dynamic controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["Generated"]) == "true")
            {
                upModal.Visible = false;
                GenerateTableOwner();
                GenerateTableMember();
            }
        }

        /// <summary>
        /// Method Event Handler when click on "Go" Button
        /// Generate Distribution List in the DropDownList Box area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Go_Button_Search_DistributionList(object sender, EventArgs e)
        {
            mailingList.Items.Clear();
            ExecutePowerShellCommand.RunScript(ExecutePowerShellCommand.LoadScript(getMailListDL));

            listOutPutDL = ReadFileOutPut.GetLineFromFile(outputDistribution);
            int Id = 1;
            int countList = listOutPutDL.Count;

            for (int i = Id; i <= countList; i++)
            {
                mailingList.Items.Add(new ListItem(listOutPutDL[i - 1]));
            }
        }

        /// <summary>
        /// Button Event Close Owner Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCloseButton_Owner_Event(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            Type csType = this.GetType();
            cs.RegisterStartupScript(csType, "myAlert", "<script language=JavaScript>window.alert('Welcome toto !');</script>");           
       }

       /// <summary>
       /// Method to open new Visit Card Details on Click in Loup icon
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       protected void OpenOwnerVisitCard_OnClick(object sender, EventArgs e)
       {
           upModal.Visible = true;
           imageowner.ImageUrl = "Images/CAZE-SULFOURT FREDERIC.jpg";
           title.Text = "Owner Description";
           firstname.Text = "First Name : " + "Frédéric";
           lastname.Text = "Last Name : " + "CAZE-SULFOURT";
           fonction.Text = "Fonction : " + "Developer";
           phone.Text = "Phone : " +  "01 42 05 87 99";
           email.Text = "Email : " + "frederic.caze-sulfourt@neurones.net";

           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
           upModal.Update();

           /*ClientScriptManager cs = Page.ClientScript;
           Type csType = this.GetType();
           cs.RegisterStartupScript(csType, "myAlert", "<script language=JavaScript>window.alert('Welcome toto !');</script>");           
       */
        }

        /// <summary>
        /// Generate the table dynamically with the powerShell scripts for the Owners
        /// </summary>
        private void GenerateTableOwner()
        {
            ExecutePowerShellCommand.RunScript(ExecutePowerShellCommand.LoadScript(getMailListOwners));

            listOutPutOwner = ReadFileOutPut.GetLineFromFile(outputowner);
            int countList = listOutPutOwner.Count;

            for (int i = 1; i <= countList; i++)
            {
                TableRow tr = new TableRow();
                TableCell adressCell = new TableCell();
                TableCell imageCell = new TableCell();
                OwnerTable.Rows.Add(tr);
                adressCell.Text = listOutPutOwner[i - 1];
                LinkButton img = new LinkButton();
                img.ID = adressCell.Text;
                img.Click += new EventHandler(OpenOwnerVisitCard_OnClick);
                img.Controls.Add(new Image { ImageUrl = "Images/loupe.png", Width = 20 });

                imageCell.Controls.Add(img);
                tr.Cells.Add(adressCell);
                tr.Cells.Add(imageCell);
            }
        }

        /// <summary>
        /// Generate the table dynamically with the powerShell scripts for the Members
        /// </summary>
        private void GenerateTableMember()
        {
            ExecutePowerShellCommand.RunScript(ExecutePowerShellCommand.LoadScript(getMailListMembers));

            listOutPutMember = ReadFileOutPut.GetLineFromFile(outputmember);
            int countList = listOutPutMember.Count;

            for (int i = 1; i <= countList; i++)
            {
                TableRow tr = new TableRow();
                TableCell adressCell = new TableCell();
                TableCell imageCell = new TableCell();
                MembersTable.Rows.Add(tr);
                adressCell.Text = listOutPutMember[i - 1];
                LinkButton img = new LinkButton();
                img.ID = adressCell.Text;
                img.Click += new EventHandler(OpenOwnerVisitCard_OnClick);
                img.Controls.Add(new Image { ImageUrl = "Images/loupe.png", Width = 20 });

                imageCell.Controls.Add(img);
                tr.Cells.Add(adressCell);
                tr.Cells.Add(imageCell);
            }
        }

        /// <summary>
        /// Method Event Handler when click on "Go!" Button
        /// Generate Owners List in the result DropDownList
        /// Generate Members List in the result DropDownList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Select_Button_DistributionList(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["Generated"]) != "true")
            {
                GenerateTableMember();
                GenerateTableOwner();
                ViewState["Generated"] = "true";
            }
        }  

        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
