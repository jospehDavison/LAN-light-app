using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace LAN_light_app
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class LightClient
    {
        Thread tcpThread;
        bool _conn = false;

        public IPAddress serverIP;
        public int port { get { return 1201; } }

        public TcpClient client;
        public NetworkStream netStream;
        public BinaryReader br;
        public BinaryWriter bw;

        public string color;

        private Form1 form1;

        public LightClient(Form1 f)
        {
            form1 = f;
        }

        public void connect()
        {
            if (!_conn)
            {
                _conn = true;

                tcpThread = new Thread(new ThreadStart(setupConn));
                tcpThread.Start();
            }
        }

        public void setupConn()
        {
            try
            {
                client = new TcpClient(serverIP.AddressFamily);
                client.Connect(serverIP, port);
                netStream = client.GetStream();
                br = new BinaryReader(netStream, Encoding.UTF8);
                bw = new BinaryWriter(netStream, Encoding.UTF8);

                byte reply = br.ReadByte();
                if (reply == OK)
                {
                    bw.Write(OK);
                    bw.Flush();

                    reciever();
                }
            }
            catch
            {
                MessageBox.Show("could not connect close and try again");
                closeConn();
            }
        }

        void reciever()
        {
            try
            {
                while (client.Connected)
                {
                    color = br.ReadString();
                    //get changeBackground function and pass color
                    form1.changeColor(color);
                }
            }
            catch
            {
            }
        }

        public void logout()
        {
            bw.Write(LOGOUT);
            bw.Flush();
            closeConn();
        }

        void closeConn()
        {
            br.Close();
            bw.Close();
            netStream.Close();
            client.Close();
            _conn = false;
        }

        private readonly byte OK = 3; //ok packet
        private readonly byte LOGOUT = 4;
    }
}
