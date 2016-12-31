using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HQ_CONTROLS
{
    public partial class ucHeader : UserControl
    {
        public ucHeader()
        {
            InitializeComponent();
        }

        public Image pictureHeader
        {
            set
            {
                picIcon.Image = value;
            }
            get
            {
                return picIcon.Image;
            }
        }

        public Color TextHeaderColor
        {
            set
            {
                lblHeaderText.ForeColor = value;
            }
            get
            {
                return lblHeaderText.ForeColor;
            }
        }
        public string TextHeader
        {
            set
            {
                lblHeaderText.Text = value;
            }
            get
            {
                return lblHeaderText.Text;
            }
        }
    }
}
