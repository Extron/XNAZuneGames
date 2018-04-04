using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ZNetwork
{
    public class ZNetworkType
    {
        NetworkSession network;
        PacketReader packetReader;
        PacketWriter packetWriter;

        string networkState;
        int gamers;

        public ZNetworkType()
        {
            packetReader = new PacketReader();
            packetWriter = new PacketWriter();
        }

        public void createSession(int maxGamers)
        {
            network = NetworkSession.Create(NetworkSessionType.SystemLink, 1, maxGamers);
        }

        public void joinSession()
        {
            using (AvailableNetworkSessionCollection availableSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 1, null))
            {
                if (availableSessions.Count == 0)
                {
                    networkState = "No network sessions were found.";
                }
                else
                {
                    network = NetworkSession.Join(availableSessions[0]);
                }
            }
        }

        public void startGame()
        {
            if (network != null)
                network.StartGame();
        }

        public void updateNetwork()
        {
            if (network != null)
                network.Update();
        }
    }
}
