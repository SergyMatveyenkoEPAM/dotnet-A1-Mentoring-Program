using HelloLibrary;
using System;
using System.Windows.Forms;

namespace HelloWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show($"Hello, {textBox1.Text}!", "Hello", MessageBoxButtons.OK);
            MessageBox.Show(Greeting.Hello(textBox1.Text), "Hello", MessageBoxButtons.OK);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrEmpty(textBox1.Text);
        }
    }
}
