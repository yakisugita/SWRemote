using Grapevine;
using System.Diagnostics;
using System.Net.Http;

namespace SWRemote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("a");
            MessageBox.Show("�e�L�X�g", "�f�o�b�O", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var server = RestServerBuilder.UseDefaults().Build();
            //�f�t�H���g����localhost�ӊO�͒e�� Stormworks��127.0.0.1�ŃA�N�Z�X����
            server.Prefixes.Add("http://127.0.0.1:1234/");
            server.Start();
        }
    }

    [RestResource]
    public class MyResource
    {

        [RestRoute("Get", "/api/test")]
        public async Task Test(IHttpContext context)
        {
            Debug.WriteLine("ACCESS");
            //await context.Response.SendResponseAsync("Successfully hit the test route!").ConfigureAwait(false);
            await context.Response.SendResponseAsync("Welcome!").ConfigureAwait(false);
        }
    }
}
