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
using System.Xml;

namespace GraphicalCalculatorNEA
{
    public partial class Settings : Form
    {
        private string[] lines = new string[5]; // settings saved locally 
        private bool valid = false; // stores whether settings valid 
        public Settings()
        {
            InitializeComponent();
        }
        //settings read from file and saved to lines[]
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
        //settings written to text file from lines[]
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
        //used to set settings to initial values
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
        //carries out validation to ensure that settings are in the correct range and form
        private void Validation()
        {
            try
            {
                if (Convert.ToDouble(tbxMinX.Text) >= Convert.ToDouble(tbxMaxX.Text) || Convert.ToDouble(tbxMinY.Text) >= Convert.ToDouble(tbxMaxY.Text))
                {
                    lbInvalid.Text = "Invalid: Minimum cannot be >= maximum.";
                    valid = false;
                }
                else if (Convert.ToDouble(tbxMinX.Text) < -100 || Convert.ToDouble(tbxMinY.Text) < -100 || 
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
        //When the form opens the settings from the text file can be read and inserted, and the componenets are anchored to ensure correct resizing.
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
        //saves the settings to the text file if valid before closing the form, if invalid displays error message
        private void btCloseS_Click(object sender, EventArgs e)
        {
            if (valid)
            {
                WriteFile();
                Close();
            }
            else
            {
                lbRejectClose.Text = "Cannot close settings if settings invalid.";
            }
        }
        //handles connection between help and settings form
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
        //handle when the user changes any settings and saves to lines[] if valid
        private void tbxMinX_TextChanged(object sender, EventArgs e)
        {
            Validation();
            if (valid)
            {
                lines[0] = tbxMinX.Text;
            }
        }
        private void tbxMaxX_TextChanged(object sender, EventArgs e)
        {
            Validation();
            if (valid)
            {
                lines[1] = tbxMaxX.Text;
            }
        }
        private void tbxMinY_TextChanged(object sender, EventArgs e)
        {
            Validation();
            if (valid)
            {
                lines[2] = tbxMinY.Text;
            }
        }
        private void tbxMaxY_TextChanged(object sender, EventArgs e)
        {
            Validation();
            if (valid)
            {
                lines[3] = tbxMaxY.Text;
            }
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
        //handles the case where the form is force closed by the user
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (valid == false)
            {
                InitialiseSettings();
            }
        }

    }
}
