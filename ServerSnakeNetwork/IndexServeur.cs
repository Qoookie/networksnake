using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuchosalN;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;


namespace ServerSnakeNetwork
{
    public partial class IndexServeur : Form
    {
        NetworkSnake server = new NetworkSnake();
        List<Connection> connectionLobby;
        List<serveurGame> listeParty;
        public IndexServeur()
        {
            connectionLobby = new List<Connection>();
            listeParty = new List<serveurGame>();

            InitializeComponent();

            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, 50001));
            ifNewClientConnexion();
        }

        private void IndexServeur_Load(object sender, EventArgs e)
        {
            autoResizeServer();
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("createANewLobby", clientCanCreateGame);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("joinLobby", clientCanJoinGame);
            NetworkComms.AppendGlobalIncomingPacketHandler<bool>("needAllLobby", sendLobbyToClient);
        }

        private void autoResizeServer()
        {
            lblConf.Text = server.ToString();
            pConfiguration.Height = txtConfiguration.Height + lblConf.Height + 25;
            lbxNomLobby.Location = new Point(lbxNomLobby.Location.X, pConfiguration.Location.Y + pConfiguration.Height + 12);
            lbxNomLobby.Height = this.ClientSize.Height - lbxNomLobby.Location.Y - 12;
            lbxConnection.Height = lbxNomLobby.Location.Y + lbxNomLobby.Height;
            this.MaximumSize = this.MinimumSize = this.Size = new Size(this.Width, (this.Height - this.ClientSize.Height) + lbxConnection.Location.Y + lbxConnection.Height + 12);
        }

        private void sendLobbyToClient(PacketHeader packetHeader, Connection connection, bool input)
        {
            string[] allLobby = lbxNomLobby.Items.OfType<string>().ToArray();
            connection.SendObject("AllLobby", JsonConvert.SerializeObject(allLobby));
        }

        private void clientCanJoinGame(PacketHeader packetHeader, Connection connection, string infoConnection)
        {
            bool connect = false;
            string[] connectToLobby = JsonConvert.DeserializeObject<string[]>(infoConnection);
            foreach (serveurGame serveur in listeParty)
            {
                if (serveur.gameName == connectToLobby[0] && serveur.password == connectToLobby[1])
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        serveur.setNewClientConnection(connection);
                        lbxConnection.Items.Remove(connection.ConnectionInfo.RemoteEndPoint.ToString());
                    }));
                    connect = true;
                    connectionLobby.Remove(connection);
                }
            }
            connection.SendObject("canConnect", connect);
            removeConn(connection);

        }

        private void clientCanCreateGame(PacketHeader packetHeader, Connection connection, string game)
        {
            bool nameExist = false;
            serveurGame srvGame;
            string[] gameDeserialize = JsonConvert.DeserializeObject<string[]>(game);
            foreach (serveurGame serveur in listeParty)
            {
                if (serveur.gameName == gameDeserialize[0])
                {
                    nameExist = true;
                }
            }
            if (!nameExist)
            {
                srvGame = new serveurGame();
                srvGame.gameName = srvGame.Text = gameDeserialize[0];
                srvGame.password = gameDeserialize[1];
                srvGame.roundGame = Convert.ToInt32(gameDeserialize[2]);
                srvGame.iVitesse = Convert.ToInt32(gameDeserialize[3]);
                srvGame.ScoreMax = Convert.ToInt32(gameDeserialize[4]);
                srvGame.index = this;

                this.Invoke(new MethodInvoker(delegate
                {
                    srvGame.setNewClientConnection(connection);
                    lbxConnection.Items.RemoveAt(lbxConnection.Items.IndexOf(connection.ConnectionInfo.RemoteEndPoint.ToString()));
                    lbxNomLobby.Items.Add(gameDeserialize[0]);
                    srvGame.Show();
                }));
                connectionLobby.Remove(connection);
                listeParty.Add(srvGame);
                srvGame = null;
            }
            connection.SendObject("youCanCreate", nameExist);
            removeConn(connection);
        }

        private void removeConn(Connection conn)
        { 
            conn.RemoveIncomingPacketHandler("createANewLobby");
            conn.RemoveIncomingPacketHandler("joinLobby");
            conn.RemoveIncomingPacketHandler("Iexist");
            conn.RemoveIncomingPacketHandler("needAllLobby");
        }

        public void lobbyStart(serveurGame srv)
        {
            listeParty.Remove(srv);
            lbxNomLobby.Items.Remove(srv.gameName);
        }

        /// <summary>
        /// Si un client se connecte, 
        /// le server memorisera la connection et l'ajoutera dans la listBox
        /// </summary>
        private void ifNewClientConnexion()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Iexist", (packetHeader, connection, ipadress) =>
            {
                connectionLobby.Add(connection);
                connection.SendObject("IseeU", true);
                lbxConnection.Invoke(new MethodInvoker(delegate
                {
                    lbxConnection.Items.Add(connection.ConnectionInfo.RemoteEndPoint.ToString());
                }));
            });
        }
    }
}
