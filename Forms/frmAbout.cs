namespace RPICommander.Forms
{
    public partial class frmAbout : Form
    {
        public frmAbout(string version)
        {
            InitializeComponent();
            CenterToScreen();
            lblVersion.Text = $"Version: {version}";
        }
    }
}
