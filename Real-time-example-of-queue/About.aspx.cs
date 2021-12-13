using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Real_time_example_of_queue
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TokenQueue"] == null)
            {
                var queueTokesen = new Queue<int>();
                Session["TokenQueue"] = queueTokesen;
            }

        }

        protected void btnPrintToken_Click(object sender, EventArgs e)
        {
            Queue<int> queueTokens = (Queue<int>)Session["TokenQueue"];
            lblCurrentStatus.Text = $"There are {queueTokens.Count} customers before you in the queue";

            if (Session["LastTokenNumberIssued"] == null)
            {
                Session["LastTokenNumberIssued"] = 0;
            }
            int nextTokenNumberTobeIssued = (int)Session["LastTokenNumberIssued"] + 1;
            Session["LastTokenNumberIssued"] = nextTokenNumberTobeIssued;
            queueTokens.Enqueue(nextTokenNumberTobeIssued);
            AddTokensToListbox(queueTokens);
        }

        private void AddTokensToListbox(Queue<int> queueTokens)
        {
            listTokens.Items.Clear();
            foreach (var token in queueTokens)
            {
                listTokens.Items.Add(token.ToString());
            }
        }

        protected void btnCounter1_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(txtCounter1, 1);
        }

        private void ServeNextCustomer(TextBox textBox, int counterNumber)
        {
            Queue<int> queueTokens = (Queue<int>)Session["TokenQueue"];
            if (queueTokens.Count == 0)
            {
                textBox.Text = "No customers in the Queue";
            }
            else
            {
                int tokenNumberToBeServed = queueTokens.Dequeue();
                textBox.Text = tokenNumberToBeServed.ToString();
                txtNextToken.Text = $"Token Number: {tokenNumberToBeServed} please go to the counter {counterNumber}";
                AddTokensToListbox(queueTokens);
            }            
        }

        protected void btnCounter2_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(txtCounter2, 2);
        }

        protected void btnCounter3_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(txtCounter3, 3);
        }
    }
}