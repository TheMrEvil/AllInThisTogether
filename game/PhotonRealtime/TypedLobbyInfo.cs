using System;

namespace Photon.Realtime
{
	// Token: 0x02000031 RID: 49
	public class TypedLobbyInfo : TypedLobby
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00008504 File Offset: 0x00006704
		public override string ToString()
		{
			return string.Format("TypedLobbyInfo '{0}'[{1}] rooms: {2} players: {3}", new object[]
			{
				this.Name,
				this.Type,
				this.RoomCount,
				this.PlayerCount
			});
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008554 File Offset: 0x00006754
		public TypedLobbyInfo()
		{
		}

		// Token: 0x0400019F RID: 415
		public int PlayerCount;

		// Token: 0x040001A0 RID: 416
		public int RoomCount;
	}
}
