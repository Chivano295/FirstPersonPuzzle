using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortNetNetManager : NetworkManager
{
    [Serializable]
    public struct ProtocolVersion : NetworkMessage
    {
        public int Mayor;
        public int Minor;
        public int Revision;

    }

    public ProtocolVersion ProtVer;

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        
        base.OnServerConnect(conn);
    }
}
