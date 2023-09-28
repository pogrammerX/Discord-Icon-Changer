using DiscordIconChanger.Core;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace DiscordIconChanger.GUI
{
    public partial class Form1 : Form
    {
        static int r = 255, g = 0, b = 0;

        void doRGB()
        {
            if (r > 0 && b == 0)
            {
                r--;
                g++;
            }
            if (g > 0 && r == 0)
            {
                g--;
                b++;
            }
            if (b > 0 && g == 0)
            {
                r++;
                b--;
            }
        }

        public Form1()
        {
            Icon = SystemIcons.Application;
            InitializeComponent();
            DoubleBuffered = true;
            System.Windows.Forms.Timer timer = new();
            timer.Tick += (o, e) => { Invoke(() => Invalidate()); };
            timer.Interval = 1;
            timer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            var brush = new SolidBrush(Color.FromArgb(100, r, g, b));
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
            brush.Dispose();

            doRGB();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button1.Cursor = Cursors.No;
            button1.Enabled = false;
            button3.Cursor = Cursors.No;
            button3.Enabled = false;
            try
            {
                DiscordUtil.LocateDiscord();
                label1.Text = "Discord Found!";
                label1.ForeColor = Color.Lime;
                button1.Cursor = Cursors.Default;
                button1.Enabled = true;
                button3.Cursor = Cursors.Default;
                button3.Enabled = true;
            }
            catch (Exception ex)
            {
                label1.Text = "Discord not found!";
                label1.ForeColor = Color.Red;
            }

            if (label1.ForeColor == Color.Lime)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes(Environment.CurrentDirectory + "\\normal.ico", Properties.Resources.normal);
            DIC.Perform(DiscordUtil.LocateDiscord(), Environment.CurrentDirectory + "\\normal.ico");
            File.Delete(Environment.CurrentDirectory + "\\normal.ico");
            MessageBox.Show("Restored!", "Discord Icon Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "Icon Files | *.ico";
            ofd.FileName = "";
            ofd.Title = "Select Icon";
            DialogResult a = ofd.ShowDialog();
            if (a != DialogResult.Cancel)
            {
                if (!File.Exists(ofd.FileName))
                {
                    MessageBox.Show(ofd.FileName + " was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DIC.Perform(DiscordUtil.LocateDiscord(), ofd.FileName);
                MessageBox.Show("Icon Changed!", "Discord Icon Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}