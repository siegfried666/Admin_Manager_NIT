﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_Manager_NIT
{
    public partial class VisitCard : System.Web.UI.Page
    {
        string memberPhoto = string.Empty;
        string ownerPhoto = string.Empty;
        int index = 0;
        private string nameSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (ViewState["AddedControl"] != null)
                {
                    if (details.Text.Length > 0)
                        OpenVisitCard_OnClick(sender, e);                
                }
            }
        }

        /// <summary>
        /// Set Visit Card for Owners visible or invisible to the DL_View.aspx page
        /// in specific case with bool factor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetVisitCard_Visible(bool _IsVisible)
        {
            details.Visible = _IsVisible;
            photo_area.Visible = _IsVisible;
        }

        /// <summary>
        /// Load current photo webControl into current photo-area controls
        /// with proportional width
        /// </summary>
        /// <param name="currentImage"></param>
        /// <param name="photo_area"></param>
        protected void PhotosLoading(string currentImage, System.Web.UI.HtmlControls.HtmlControl photo_area)
        {
            photo_area.Controls.Clear();
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
            img.ImageUrl = currentImage; // setting the path to the image
            img.Width = 125;
            img.ID = currentImage + Convert.ToString(index++);
            photo_area.Controls.Add(img);
        }

        /// <summary>
        /// Method to open new Owner Visit Card Details on Click in Loup icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OpenVisitCard_OnClick(object sender, EventArgs e)
        {
            nameSelected = (string)(Session["toEmail"]);
            ownerPhoto = "~/Images/CAZE-SULFOURT FREDERIC.jpg";
            PhotosLoading(ownerPhoto, photo_area);

            details.Text = string.Empty;//Empty textbox for refreshing the information
            details.Text += "Frédéric" + "\n";
            details.Text += "CAZE-SULFOURT" + "\n";
            details.Text += "Developer" + "\n";
            details.Text += "01 42 05 87 99";
            details.ReadOnly = true;

            //LinkButton definition with current owner email adress
            LinkButton.Text = "frederic.caze-sulfourt@neurones.net";
            LinkButton.ForeColor = System.Drawing.Color.White;
            LinkButton.Font.Name = "Trocchi";
            LinkButton.Font.Size = 15;

            SetVisitCard_Visible(true);
        }
    }
}