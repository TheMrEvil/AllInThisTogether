using System;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Photon.Pun
{
	// Token: 0x02000002 RID: 2
	internal static class CustomTypes
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static void Register()
		{
			PhotonPeer.RegisterType(typeof(Player), 80, new SerializeStreamMethod(CustomTypes.SerializePhotonPlayer), new DeserializeStreamMethod(CustomTypes.DeserializePhotonPlayer));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
		{
			int actorNumber = ((Player)customobject).ActorNumber;
			byte[] obj = CustomTypes.memPlayer;
			short result;
			lock (obj)
			{
				byte[] array = CustomTypes.memPlayer;
				int num = 0;
				Protocol.Serialize(actorNumber, array, ref num);
				outStream.Write(array, 0, 4);
				result = 4;
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E4 File Offset: 0x000002E4
		private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
		{
			if (length != 4)
			{
				return null;
			}
			byte[] obj = CustomTypes.memPlayer;
			int id;
			lock (obj)
			{
				inStream.Read(CustomTypes.memPlayer, 0, (int)length);
				int num = 0;
				Protocol.Deserialize(out id, CustomTypes.memPlayer, ref num);
			}
			if (PhotonNetwork.CurrentRoom != null)
			{
				return PhotonNetwork.CurrentRoom.GetPlayer(id, false);
			}
			return null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002158 File Offset: 0x00000358
		// Note: this type is marked as 'beforefieldinit'.
		static CustomTypes()
		{
		}

		// Token: 0x04000001 RID: 1
		public static readonly byte[] memPlayer = new byte[4];
	}
}
