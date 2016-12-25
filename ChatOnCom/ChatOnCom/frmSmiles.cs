using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChatOnCom
{
    public partial class frmSmiles : Form
    {
        string[] SmilesArray = { ":P", ":)]", ":-c", ":-h", ":-t", "8->", "x_x", ":-*", ":-q", "=((", ":-o", ":>", "B-)", ":-S", ":((", ":))", ":(", ":)", "=))", "O:-)", ";)", "(:|", ":O)", ":-$", "[-(", ":ar!", "I-)", ":-w", "8-}", ":D", "@};-" };
        string ImageDir = Environment.CurrentDirectory + @"\Images\Smiles\";

        public frmSmiles()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        private void frmSmiles_Deactivate(object sender, EventArgs e)
        {
            //đóng của s
            this.Close();
        }

        private void frmSmiles_Load(object sender, EventArgs e)
        {
            //load default for Smiles Form
            try
            {
                int countControl = 0;
                foreach (Control c in this.Controls)
                {
                    if (c.GetType() == typeof(Button))
                    {
                        Image smile = Image.FromFile(string.Format("{0}{1}.gif", ImageDir, countControl));
                        ((Button)c).Image = smile;
                        ((Button)c).Tag = SmilesArray[countControl];
                        SmileTootip.SetToolTip(c, SmilesArray[countControl]);
                        countControl++;
                        //smile.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //                throw;
            }
        }

        public delegate void GetSmile(string value);
        public GetSmile getSmile;

        private void btn29_Click(object sender, EventArgs e)
        {
            //event for all button Smiles
            if(getSmile!=null)
                getSmile(((Button)sender).Tag.ToString());
            //this.Close();
        }
    }
}
