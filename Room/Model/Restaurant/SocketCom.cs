using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using MCI_Common.Communication;
using MCI_Common.Recipes;
using MCI_Common.Dishes;

namespace Room.Model.Restaurant
{
    public class SocketCom
    {
        
        public Socket Cnx { get; set; }

        public string IpAdress { get; set; }

        public int Port { get; set; }


        public event EventHandler MenuReception;

        public event EventHandler OrderReadyReception;

        /// <summary>
        /// Create a connection 
        /// </summary>
        public SocketCom(string server, int port)
        {
            this.Port = port;
            this.IpAdress = server;
        }

        public void SocketRequest(string msg)
        {

            new Thread(() =>
            {
                try
                {
                    IPAddress ipAddress = IPAddress.Parse(IpAdress);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    // Create a TCP/IP  socket.  
                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

                    // Connect the socket to the remote endpoint. Catch any errors.  
                    try
                    {
                        sender.Connect(remoteEP);

                        if (sender.Connected)
                            Console.WriteLine("Socket connected to {0}",
                            sender.RemoteEndPoint.ToString());
                        else
                            Console.WriteLine("Socket failed to connect");

                        msg += "<EOF>";
                        // Encode the data string into a byte array.  
                        byte[] inBuffer = Encoding.ASCII.GetBytes(msg);

                        // Send the data through the socket.  
                        int bytesSent = sender.Send(inBuffer);

                        string XmlStr = "";

                        do
                        {
                            byte[] outBuffer = new byte[10025];
                            int bytesRec = sender.Receive(outBuffer);
                            XmlStr += Encoding.ASCII.GetString(outBuffer, 0, bytesRec);
                            Console.WriteLine("received: {0}", XmlStr);
                        } while (XmlStr.IndexOf("<EOF>") == -1);

                        Receive(XmlStr);

                        // Release the socket.  
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                } 
            }).Start();
        }

        /// <summary>
        /// Send a string in the socket
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public void Send(string msg)
        {
            SocketRequest(msg);
        }

        public void Receive(string datas)
        {
            Console.WriteLine("Reception");
            datas = datas.Substring(0, datas.Length - 5);
            Console.WriteLine(datas);

            if (datas.IndexOf("<MENU>") > -1)
            {
                datas = datas.Substring(0, datas.Length - "<MENU>".Length);
                ObjectEventArgs args = new ObjectEventArgs(datas, typeof(List<Recipe>));
                OnMenuReception(args);   
            }
            else if (datas.IndexOf("<ORDER_READY>") > -1)
            {
                ObjectEventArgs args = new ObjectEventArgs(datas, typeof(MCI_Common.Dishes.Order));
                OnOrderReadyReception(args);
            }
        }

        protected virtual void OnMenuReception(EventArgs e)
        {
            MenuReception?.Invoke(this, e);
        }

        protected virtual void OnOrderReadyReception(EventArgs e)
        {
            OrderReadyReception?.Invoke(this, e);
        }
    }

    public class ObjectEventArgs : EventArgs
    {
        public object receiveObject;

        public ObjectEventArgs(string str, Type type)
        {
            receiveObject = Serialization.DeSerializeAnObject(str,type);
        }
    }
}
