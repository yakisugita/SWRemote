using Grapevine;
using System.Diagnostics;
using System.Net.Http;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            //MessageBox.Show("�e�L�X�g", "�f�o�b�O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ValueStore.count = 0;

            var server = RestServerBuilder.UseDefaults().Build();
            //�f�t�H���g����localhost�ӊO�͒e�� Stormworks��127.0.0.1�ŃA�N�Z�X����
            server.Prefixes.Add("http://127.0.0.1:1234/");
            server.ContentFolders.Add(new ContentFolder(Path.Combine(Directory.GetCurrentDirectory(), "website")));
            server.UseContentFolders();
            server.Start();
        }
    }

    [RestResource]
    public class MyResource
    {
        [RestRoute("Get", "/api/input")]
        public async Task Receive(IHttpContext context)
        {
            Debug.WriteLine("ACCESS");

            for (int i = 0; i < context.Request.QueryString.AllKeys.Length; i++)
            {
                switch (context.Request.QueryString.AllKeys[i])
                {
                    case "numAD":
                        int.TryParse(context.Request.QueryString.GetValue<string>(context.Request.QueryString.AllKeys[i]), out ValueStore.NumAD);
                        break;
                    default:
                        break;
                }
            }
            Debug.WriteLine(ValueStore.NumAD.ToString());
            //await context.Response.SendResponseAsync("Successfully hit the test route!").ConfigureAwait(false);
            await context.Response.SendResponseAsync("Welcome!"+ValueStore.count.ToString()).ConfigureAwait(false);
        }

        // Stormworks�̃}�C�R���ɏ��𑗂�
        // �X�}�z�ؒf�� SWREMOTE|DISCONNECTED
        // [U]p/[D]own/[N]eutral/[V]oid(�{�^�����얳��) �{�^������̂Ƃ����l��+000
        // SWREMOTE|READY|WS[3������][U/D/N]|AD[3������][U/D/N]|Up/Down[3������][U/D/N]|Left/Right[3������][U/D/N]|1[T/F]2[T/F]3[T/F]4[T/F]5[T/F]6[T/F]7[T/F]8[T/F]
        // �� SWREMOTE|READY|WS+000U|AD-050V|UD+000N|LR+000D|TFFFFFFF
        [RestRoute("Get", "/api/sw")]
        public async Task Send(IHttpContext context)
        {
            Debug.WriteLine("ACCESS");

            for (int i = 0; i < context.Request.QueryString.AllKeys.Length; i++)
            {
                switch (context.Request.QueryString.AllKeys[i])
                {
                    case "numAD":
                        int.TryParse(context.Request.QueryString.GetValue<string>(context.Request.QueryString.AllKeys[i]), out ValueStore.NumAD);
                        break;
                    default:
                        break;
                }
            }
            Debug.WriteLine(ValueStore.NumAD.ToString());
            //await context.Response.SendResponseAsync("Successfully hit the test route!").ConfigureAwait(false);
            await context.Response.SendResponseAsync("Welcome!" + ValueStore.count.ToString()).ConfigureAwait(false);
        }
    }

    public static class ValueStore
    {
        public static int count;
        public static int NumAD = 0;
    }
}
