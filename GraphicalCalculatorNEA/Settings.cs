using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GraphicalCalculatorNEA
{
    public partial class Settings : Form
    {
        public string[] lines = new string[5];
        public bool valid = false;
        private void ReadFile()
        {
            StreamReader reader = new StreamReader("Settings.txt");
            for (int i = 0; i <= 4; i++)
            {
                lines[i] = reader.ReadLine();
            }
            reader.Close();
        }
        private void WriteFile()
        {
            StreamWriter writer = new StreamWriter("Settings.txt");
            for (int i = 0; i <= 4; i++)
            {
                writer.WriteLine(lines[i]);
            }
            writer.Close();
            Graph graph = new();
            graph.FuncCheck();
        }
        private void Validation()
        {
            try
            {
                if (Convert.ToDouble(tbxMinX.Text) >= Convert.ToDouble(tbxMaxX.Text) || Convert.ToDouble(tbxMinY.Text) >= Convert.ToDouble(tbxMaxY.Text))
                {
                    lbInvalid.Text = "Invalid: Minimum cannot be >= maximum.";
                    valid = false;
                }
                else
                {
                    lbInvalid.Text = null;
                    lbRejectClose.Text = null;
                    valid = true;
                }
            }
            catch
            {
                lbInvalid.Text = "Invalid.";
                valid = false;
            }
        }
        public Settings()
        {
            InitializeComponent();
        }

        private void lbSettings_Load(object sender, EventArgs e)
        {
            btCloseS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btHelpS.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lbAngle.Anchor = AnchorStyles.None;
            lbMinX.Anchor = AnchorStyles.None;
            lbMaxX.Anchor = AnchorStyles.None;
            lbMinY.Anchor = AnchorStyles.None;
            lbMaxY.Anchor = AnchorStyles.None;
            tbxMaxX.Anchor = AnchorStyles.None;
            tbxMaxY.Anchor = AnchorStyles.None;
            tbxMinX.Anchor = AnchorStyles.None;
            tbxMinY.Anchor = AnchorStyles.None;
            rbtDegrees.Anchor = AnchorStyles.None;
            rbtRadians.Anchor = AnchorStyles.None;
            lbViewWin.Anchor = AnchorStyles.None;
            lbRejectClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbInvalid.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            ReadFile();
            tbxMinX.Text = lines[0];
            tbxMaxX.Text = lines[1];
            tbxMinY.Text = lines[2];
            tbxMaxY.Text = lines[3];
            if (lines[4] == "Degrees")
            {
                rbtRadians.Checked = false;
                rbtDegrees.Checked = true;
            }
            else
            {
                rbtDegrees.Checked = false;
                rbtRadians.Checked = true;
            }
            Validation();

        }
        private void btCloseS_Click(object sender, EventArgs e)
        {
            if (valid == true)
            {
                Close();
            }
            else
            {
                lbRejectClose.Text = "Cannot close settings if settings invalid.";
            }
        }

        private void btHelpS_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            if (help.Visible == false)
            {
                help.Show();
            }
            else
            {
                help.Hide();
            }
        }

        private void tbxMinX_TextChanged(object sender, EventArgs e)
        {
            Validation();
            ReadFile();
            lines[0] = tbxMinX.Text;
            WriteFile();
        }

        private void tbxMaxX_TextChanged(object sender, EventArgs e)
        {
            Validation();
            ReadFile();
            lines[1] = tbxMaxX.Text;
            WriteFile();
        }

        private void tbxMinY_TextChanged(object sender, EventArgs e)
        {
            Validation();
            ReadFile();
            lines[2] = tbxMinY.Text;
            WriteFile();
        }

        private void tbxMaxY_TextChanged(object sender, EventArgs e)
        {
            Validation();
            ReadFile();
            lines[3] = tbxMaxY.Text;
            WriteFile();
        }

        private void rbtRadians_CheckedChanged(object sender, EventArgs e)
        {
            ReadFile();
            if (rbtRadians.Checked)
            {
                lines[4] = "Radians";
            }
            else
            {
                lines[4] = "Degrees";
            }
            WriteFile();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (valid == false)
            {
                StreamWriter writer = new StreamWriter("Settings.txt");
                writer.WriteLine("-1000");
                writer.WriteLine("1000");
                writer.WriteLine("-1000");
                writer.WriteLine("1000");
                writer.Close();
            }
        }

    }
}
