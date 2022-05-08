using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket server;
        bool ok = false;
        string input, stringData;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;
        private void SendMsgBTN_Click(object sender, EventArgs e)
        {

            string data;
            string input;
            if (!ok)
            {
                IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"), 9050);
                server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);
                }
                catch (SocketException ex)
                {
                    InfoTxt.Text += "Unable to connect to server.\n";
                    InfoTxt.Text += ex.ToString() + "\n";
                    return;
                }
                ns = new NetworkStream(server);
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);
                data = sr.ReadLine();
                InfoTxt.Text += data + "\n";
                IPTxt.Text = "127.0.0.1";
                MsgTxt.ReadOnly = false;
                MsgTxt.Text = "Connecting to server: 127.0.0.1";
                SendMsgBTN.Text = "Send Message";
                ok = true;
            }
            input = MsgTxt.Text;
            sw.WriteLine(input);
            sw.Flush();
            data = sr.ReadLine();
            InfoTxt.Text += data + "\n";
            // Encode the data string into a byte array+
            if (MsgTxt.Text == "exit")
            {
                SendMsgBTN.Text = "Connect";
                MsgTxt.ReadOnly = true;
                InfoTxt.Text += "Disconnecting from server...\n";
                sr.Close();
                sw.Close();
                ns.Close();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                ok = false;
            }
            MsgTxt.Clear();
        }
    }
}