using System;

namespace Photon.Realtime
{
	// Token: 0x02000030 RID: 48
	public class TypedLobby
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000084AE File Offset: 0x000066AE
		public bool IsDefault
		{
			get
			{
				return string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000084BB File Offset: 0x000066BB
		internal TypedLobby()
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000084C3 File Offset: 0x000066C3
		public TypedLobby(string name, LobbyType type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000084D9 File Offset: 0x000066D9
		public override string ToString()
		{
			return string.Format("lobby '{0}'[{1}]", this.Name, this.Type);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000084F6 File Offset: 0x000066F6
		// Note: this type is marked as 'beforefieldinit'.
		static TypedLobby()
		{
		}

		// Token: 0x0400019C RID: 412
		public string Name;

		// Token: 0x0400019D RID: 413
		public LobbyType Type;

		// Token: 0x0400019E RID: 414
		public static readonly TypedLobby Default = new TypedLobby();
	}
}
