using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NenrDZ5
{
    public partial class DatasetCreator : Form
    {
        private Dataset _dataset;

        public DatasetCreator()
        {
            InitializeComponent();

            _dataset = new Dataset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.ShowDialog();
            _dataset.Save(sfd.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var line = canvas.GetPoints();
            Label label = Encoder.LabelFromText(comboBox1.SelectedItem.ToString());
            _dataset.Add(line, label);
        }
    }
}
