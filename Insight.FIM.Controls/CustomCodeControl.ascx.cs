using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Insight.FIM.Controls
{
    public partial class CustomCodeControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int count = incrementCount(0);

            SetNumOfRows(count);

            txtPropertyCount.Text = count.ToString();
        }

        protected void btnAddParam_Click(object sender, EventArgs e)
        {

            int count = incrementCount(1);

            SetNumOfRows(count);

            txtPropertyCount.Text = count.ToString();

        }

        protected void btnRemoveParam_Click(object sender, EventArgs e)
        {

            int count = incrementCount(-1);

            SetNumOfRows(count);

            txtPropertyCount.Text = count.ToString();

        }

        protected int incrementCount(int amount, int minVal = 0)
        {

            int paramCount = 0;
            int count = 0;

            if (int.TryParse(txtPropertyCount.Text, out count))
            {
                paramCount = count + amount;
            }
            else
            {
                paramCount = 0 + amount;
            }


            if (paramCount < minVal)
            {
                paramCount = minVal;
            }

            return paramCount;

        }

        protected void SetNumOfRows(int numOfRows)
        {

            //tblParams.Rows.Clear()
            int start = 1;

            if ((tblParams != null) && (tblParams.Rows != null))
            {
                while (tblParams.Rows.Count > numOfRows)
                {
                    tblParams.Rows.RemoveAt(tblParams.Rows.Count - 1);
                }

                start = tblParams.Rows.Count + 1;

            }

            for (int index = start; index <= numOfRows; index++)
            {
                //add a new param to the table
                TableRow row = new TableRow();
                TableCell cell = new TableCell();

                Label lbLabel = new Label();
                lbLabel.Text = "Parameters[" + (index - 1).ToString() + "]" + " &nbsp; ";

                TextBox txtBox = new TextBox();
                txtBox.ID = "Parameter" + index.ToString();
                txtBox.CssClass = "ms-lookuptypeintextbox";
                txtBox.Width = Unit.Pixel(300);

                //try to get the value
                try
                {
                    txtBox.Text = txtParamList.Text.Split(new char[] { ',' })[index - 1];
                }
                catch { }

                cell.CssClass = "ms-siteaction";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.VerticalAlign = VerticalAlign.Top;
                cell.Controls.Add(lbLabel);
                cell.Controls.Add(txtBox);

                row.Cells.Add(cell);

                tblParams.Rows.Add(row);

            }

        }
    }
}