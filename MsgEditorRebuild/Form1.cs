using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Msgfile;
namespace MsgEditorRebuild
{
    public partial class Form1 : Form
    {
        msg file;
        string FileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Filter = "Xenoverse msg (*.msg)|*.msg";
            browseFile.Title = "Browse for msg File";
            if (browseFile.ShowDialog() == DialogResult.Cancel)
                return;


            FileName = browseFile.FileName;
            file = msgStream.Load(FileName);

            slctBox.Items.Clear();
            for (int i = 0; i < file.data.Length; i++)
                slctBox.Items.Add(i);

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msgStream.Save(file, FileName);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNameID.Text = file.data[slctBox.SelectedIndex].NameID;
            txtID.Text = file.data[slctBox.SelectedIndex].ID.ToString();
            LineBox.Items.Clear();
            for (int i = 0; i < file.data[slctBox.SelectedIndex].Lines.Length; i++)
                LineBox.Items.Add(i);

            LineBox.SelectedIndex = 0;
            txtLine.Text = file.data[slctBox.SelectedIndex].Lines[LineBox.SelectedIndex];
        }

        private void LineBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLine.Text = file.data[slctBox.SelectedIndex].Lines[LineBox.SelectedIndex];
        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {
            file.data[slctBox.SelectedIndex].Lines[LineBox.SelectedIndex] = txtLine.Text;
        }
    }
}
