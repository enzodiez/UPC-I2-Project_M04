using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorInterface
{
    public partial class SecurityCiclesInterface : Form
    {
        Double cycleDuration;
        Double securityDistance;
        public SecurityCiclesInterface()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(textBox1.Text) < 0 || Convert.ToDouble(textBox2.Text) < 0)
                {
                    MessageBox.Show("Please enter valid positive numeric values for security distance and cycle duration.");
                }
                else
                {
                    securityDistance = Convert.ToDouble(textBox1.Text);
                    cycleDuration = Convert.ToDouble(textBox2.Text);
                    Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for security distance and cycle duration.");
            }
        }
        public Double DameSecurityDistance()
        {
            return securityDistance;
        }
        public Double DameCycleDuration()
        {
                return cycleDuration;
        }
    }
}
