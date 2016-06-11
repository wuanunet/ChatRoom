using Microsoft.AspNet.SignalR.Client;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Chat.Tray
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayApplicationContext());
        }
    }

    public class TrayApplicationContext : ApplicationContext
    {
        public static string ServerBaseUrl = "http://localhost:26836/";

        private IContainer _components;
        private NotifyIcon _notifyIcon;

        private ContextMenuStrip _contextMenu;
        private ToolStripMenuItem _callWeb;
        private ToolStripMenuItem _exit;

        private HubConnection connection;
        private IHubProxy chatHub;

        public TrayApplicationContext()
        {
            _components = new Container();

            _notifyIcon = new NotifyIcon(_components);
            //_notifyIcon.Icon = Properties.Resources.Talk;
            _notifyIcon.Icon = new Icon(@"E:\DevProjects\Dev2013\Chat.Web\Chat.Tray\bin\Debug\Talk.ico");
            _notifyIcon.Text = "Chat";
            _notifyIcon.BalloonTipTitle = "隨意聊天室";
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            _notifyIcon.BalloonTipClicked += OpenCallHelper_Click;
            _notifyIcon.Visible = true;

            _contextMenu = new ContextMenuStrip(_components);

            _notifyIcon.ContextMenuStrip = _contextMenu;

            _callWeb = new ToolStripMenuItem("進入網站");
            _callWeb.Click += OpenCallHelper_Click;
            _contextMenu.Items.Add(_callWeb);

            _contextMenu.Items.Add(new ToolStripSeparator());

            _exit = new ToolStripMenuItem("離開");
            _exit.Click += Exit_Click;
            _contextMenu.Items.Add(_exit);

            connection = new HubConnection("http://localhost:26836/");
            chatHub = connection.CreateHubProxy("ChatHub");

            chatHub.On<string, string>("gotMessage", (who, message) =>
            {
                _notifyIcon.BalloonTipText = who + "：" + message;
                _notifyIcon.ShowBalloonTip(5000);
            });

            connection.Start();
        }

        private void OpenCallHelper_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://localhost:26836/");
            Process.Start(sInfo);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}