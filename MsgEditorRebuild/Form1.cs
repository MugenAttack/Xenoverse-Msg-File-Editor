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
        bool lockEdit = false;
        bool lockcb = false;
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
            file = msgStream.Load2(FileName);

            slctBox.Items.Clear();
            for (int i = 0; i < file.data.Length; i++)
                slctBox.Items.Add(file.data[i].ID.ToString() + " - " + file.data[i].NameID);

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msgStream.Save(file, FileName);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lockcb)
            {
                lockEdit = true;
                txtNameID.Text = file.data[slctBox.SelectedIndex].NameID;
                txtID.Text = file.data[slctBox.SelectedIndex].ID.ToString();
                LineBox.Items.Clear();
                for (int i = 0; i < file.data[slctBox.SelectedIndex].Lines.Length; i++)
                    LineBox.Items.Add(i);

                LineBox.SelectedIndex = 0;
                txtLine.Text = file.data[slctBox.SelectedIndex].Lines[LineBox.SelectedIndex];
                lockEdit = false;
            }
        }

        private void LineBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lockEdit)
                txtLine.Text = file.data[slctBox.SelectedIndex].Lines[LineBox.SelectedIndex];
        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {
            if (!lockEdit)
                file.data[slctBox.SelectedIndex].Lines[LineBox.SelectedIndex] = txtLine.Text;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msgData[] reduce = new msgData[file.data.Length - 1];
            Array.Copy(file.data, reduce, file.data.Length - 1);
            file.data = reduce;
        }

        private void txtNameID_TextChanged(object sender, EventArgs e)
        {
            if (!lockEdit)
            {
                lockcb = true;
                file.data[slctBox.SelectedIndex].NameID = txtNameID.Text;
                
                
                
                slctBox.Items[slctBox.SelectedIndex] = file.data[slctBox.SelectedIndex].ID.ToString() + "-" + file.data[slctBox.SelectedIndex].NameID;
                
                lockcb = false;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msgData[] expand = new msgData[file.data.Length + 1];
            Array.Copy(file.data, expand, file.data.Length);
            string nameid = file.data[file.data.Length - 1].NameID;
            int endid = int.Parse(nameid.Substring(nameid.Length - 3, 3));
            expand[expand.Length - 1].ID = file.data.Length;
            expand[expand.Length - 1].Lines = new string[] {"New Entry" };
            expand[expand.Length - 1].NameID = nameid.Substring(0, nameid.Length - 3) + (endid + 1).ToString("000");

            file.data = expand;

            slctBox.Items.Clear();
            for (int i = 0; i < file.data.Length; i++)
                slctBox.Items.Add(file.data[i].ID.ToString() + "-" + file.data[i].NameID);
        }
    }
}
