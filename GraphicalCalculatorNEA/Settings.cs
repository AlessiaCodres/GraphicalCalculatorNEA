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
        private string[] lines = new string[5];
        private bool valid = false;
        public Settings()
        {
            InitializeComponent();
        }
        private string[] ReadFile()
        {
            StreamReader reader = new StreamReader("Settings.txt");
            for (int i = 0; i <= 4; i++)
            {
                lines[i] = reader.ReadLine();
            }
            reader.Close();
            return lines;
        }
        private string[] WriteFile()
        {
            StreamWriter writer = new StreamWriter("Settings.txt");
            for (int i = 0; i <= 4; i++)
            {
                writer.WriteLine(lines[i]);
            }
            writer.Close();
            Graph graph = new();
            graph.FuncCheck();
            return lines;
        }
        public void InitialiseSettings()
        {
            StreamWriter writer = new StreamWriter("Settings.txt");
            writer.WriteLine("-100");
            writer.WriteLine("100");
            writer.WriteLine("-100");
            writer.WriteLine("100");
            writer.WriteLine("Radians");
            writer.Close();
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
                if (Convert.ToDouble(tbxMinX.Text) < -100 || Convert.ToDouble(tbxMinY.Text) < -100 || 
                    Convert.ToDouble(tbxMaxX.Text) > 100 || Convert.ToDouble(tbxMaxY.Text) > 100)
                {
                    lbInvalid.Text = "Invalid: Must be in the range -100 <= Settings <= 100.";
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

            try
            {
                ReadFile();
            }
            catch
            {
                WriteFile();
            }
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
                WriteFile();
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
            lines[0] = tbxMinX.Text;
        }

        private void tbxMaxX_TextChanged(object sender, EventArgs e)
        {
            Validation();
            lines[1] = tbxMaxX.Text;
        }

        private void tbxMinY_TextChanged(object sender, EventArgs e)
        {
            Validation();
            lines[2] = tbxMinY.Text;
        }

        private void tbxMaxY_TextChanged(object sender, EventArgs e)
        {
            Validation();
            lines[3] = tbxMaxY.Text;
        }

        private void rbtRadians_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtRadians.Checked)
            {
                lines[4] = "Radians";
            }
            else
            {
                lines[4] = "Degrees";
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (valid == false)
            {
                InitialiseSettings();
            }
        }

    }
}
