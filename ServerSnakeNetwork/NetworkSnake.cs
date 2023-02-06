using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.UDP;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace DuchosalN
{
    public class NetworkSnake
    {

        private IPAddress hostIP;
        private IPAddress GateawayIP;
        private IPAddress Mask;
        private IPAddress BroadcastIP;

        public NetworkSnake()
        {
            setHostIP();
            setGateawayIP();
            setMask();
            setBroadCastIP();
        }

        public IPAddress getHostIP()
        {
            return hostIP;
        }
        private void setHostIP()
        {
            hostIP = getHostAdresse();
        }
        public IPAddress getGateawayIP()
        {
            return GateawayIP;
        }
        private void setGateawayIP()
        {
            GateawayIP = getGateawayAdress();
        }
        public IPAddress getMask()
        {
            return Mask;
        }
        private void setMask()
        {
            if (hostIP != null)
                Mask = GetSubnetMask(hostIP);
            else
                Mask = null;
        }
        public IPAddress getBroadCastIP()
        {
            return BroadcastIP;
        }
        private void setBroadCastIP()
        {
            if (hostIP != null || Mask != null)
                BroadcastIP = GetBroadCastAdresse(hostIP, Mask);
            else
                BroadcastIP = null;
        }


        private static IPAddress getHostAdresse()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("157.26.166.16", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        private static IPAddress getGateawayAdress()
        {
            string data = "pong";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            Ping ping = new Ping();
            PingOptions options = new PingOptions(1, true);

            PingReply reply = ping.Send("172.217.168.68", 128, buffer, options);
            return reply.Address;
        }

        private static IPAddress GetSubnetMask(IPAddress address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address))
                        {
                            return unicastIPAddressInformation.IPv4Mask;
                        }
                    }
                }
            }
            throw new ArgumentException(string.Format("Can't find subnetmask for IP address '{0}'", address));
        }

        private static IPAddress GetBroadCastAdresse(IPAddress host, IPAddress mask)
        {
            byte[] broadcastIPBytes = new byte[4];
            byte[] hostBytes = host.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();
            for (int i = 0; i < 4; i++)
            {
                broadcastIPBytes[i] = (byte)(hostBytes[i] | (byte)~maskBytes[i]);
            }
            return new IPAddress(broadcastIPBytes);
        }

        public override string ToString()
        {
            return "Adresse IP: " + getHostIP() + "\nMask: " + getMask() + "\nBroadcast: " + getBroadCastIP() + "\nGateaway: " + getGateawayIP();
        }

    }
}
