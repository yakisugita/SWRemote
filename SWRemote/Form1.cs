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
            //MessageBox.Show("テキスト", "デバッグ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ValueStore.count = 0;

            var server = RestServerBuilder.UseDefaults().Build();
            //デフォルトだとlocalhost意外は弾く Stormworksは127.0.0.1でアクセスする
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

        // Stormworksのマイコンに情報を送る
        // スマホ切断時 SWREMOTE|DISCONNECTED
        // [U]p/[D]own/[N]eutral/[V]oid(ボタン操作無効) ボタン操作のとき数値は+000
        // SWREMOTE|READY|WS[3桁整数][U/D/N]|AD[3桁整数][U/D/N]|Up/Down[3桁整数][U/D/N]|Left/Right[3桁整数][U/D/N]|1[T/F]2[T/F]3[T/F]4[T/F]5[T/F]6[T/F]7[T/F]8[T/F]
        // 例 SWREMOTE|READY|WS+000U|AD-050V|UD+000N|LR+000D|TFFFFFFF
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
