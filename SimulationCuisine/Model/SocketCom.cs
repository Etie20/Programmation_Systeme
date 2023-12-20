using MCI_Common.Communication;
using MCI_Common.Dishes;
using MCI_Common.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationKitchen.Model
{

    public class SocketCom
    {

        public event EventHandler NewMenuDemand;
        public event EventHandler NewOrderArrive;

        public Socket RoomHandler { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

        public string Datas { get; set; }

        /// <summary>
        /// Create a connection 
        /// </summary>
        public SocketCom(string server, int port)
        {
            this.IpAddress = server;
            this.Port = port;
        }

        public void StartListening()
        {
            new Thread(() =>
            {

                // Establish the local endpoint for the socket.  
                // Dns.GetHostName returns the name of the   
                // host running the application.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPAddress.Parse(this.IpAddress);
                Console.WriteLine(ipAddress.ToString());
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, this.Port);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and   
                // listen for incoming connections.  
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);

                    // Start listening for connections.  
                    while (true)
                    {
                        // Set the event to nonsignaled state.  
                        LogWriter.GetInstance().Write("Kitchen Listenning on " + listener.LocalEndPoint.ToString() + ", waiting for connection ...");

                        // Program is suspended while waiting for an incoming connection.  
                        this.RoomHandler = listener.Accept();

                        this.NewClient();
                        
                    }
                }
                catch (Exception e)
                {
                    LogWriter.GetInstance().Write(e.ToString());
                }
            }).Start();
        }

        private void NewClient()
        {
            new Thread(() =>
            {
                LogWriter.GetInstance().Write("Client " + this.RoomHandler.LocalEndPoint.ToString() + " connected");

                // Data buffer for incoming data.  
                

                while (this.RoomHandler.Connected)
                {
                    byte[] bytes = new Byte[10025];
                    int bytesRec = this.RoomHandler.Receive(bytes);
                    this.Datas += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    if (this.Datas.IndexOf("<EOF>") > -1)
                    {
                        this.ProcessRecieveData(this.Datas);
                        this.Datas = "";

                    }
                    /*LogWriter.GetInstance().Write("Client " + this.RoomHandler.LocalEndPoint.ToString() + " disconnected");
                    this.RoomHandler.Close();*/
                }
            }).Start();
        }

        private void ProcessRecieveData(string data)
        {
            data = data.Substring(0, data.Length - 5);
            if (data == "<MENU>")
            {
                LogWriter.GetInstance().Write("Menu demand");
                this.OnNewMenuDemand(EventArgs.Empty);
            }
            else if (data.IndexOf("<ORDER>") > -1)
            {
                Console.WriteLine("Hello");
                LogWriter.GetInstance().Write("Order to prepare");
                data = data.Substring(0, data.Length - "<ORDER>".Length);
                OrderEventArgs OrderEvent = new OrderEventArgs(data);
                this.OnNewOrderArrive(OrderEvent);
            }
        }
        
        /// <summary>
        /// Send an object in the socket
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Send(string msg)
        {
            msg += "<EOF>";
            LogWriter.GetInstance().Write("Program send info");

            byte[] data = Encoding.ASCII.GetBytes(msg);

            this.RoomHandler.Send(data);
            return true;
        }

        protected virtual void OnNewMenuDemand(EventArgs e)
        {
            NewMenuDemand?.Invoke(this, e);
        }

        protected virtual void OnNewOrderArrive(EventArgs e)
        {
            NewOrderArrive?.Invoke(this, e);
        }
    }

    public class OrderEventArgs : EventArgs
    {
        public Order receiveOrder;

        public OrderEventArgs(string str)
        {
            receiveOrder = (Order) Serialization.DeSerializeAnObject(str, typeof(Order));
        }
    }
}
