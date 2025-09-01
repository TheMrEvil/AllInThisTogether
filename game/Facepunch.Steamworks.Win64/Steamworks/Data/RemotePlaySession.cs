using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks.Data
{
	// Token: 0x02000202 RID: 514
	public struct RemotePlaySession
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0001A9E6 File Offset: 0x00018BE6
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0001A9EE File Offset: 0x00018BEE
		public uint Id
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0001AA14 File Offset: 0x00018C14
		public static implicit operator RemotePlaySession(uint value)
		{
			return new RemotePlaySession
			{
				Id = value
			};
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0001AA33 File Offset: 0x00018C33
		public static implicit operator uint(RemotePlaySession value)
		{
			return value.Id;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public bool IsValid
		{
			get
			{
				return this.Id > 0U;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0001AA47 File Offset: 0x00018C47
		public SteamId SteamId
		{
			get
			{
				return SteamRemotePlay.Internal.GetSessionSteamID(this.Id);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0001AA5E File Offset: 0x00018C5E
		public string ClientName
		{
			get
			{
				return SteamRemotePlay.Internal.GetSessionClientName(this.Id);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0001AA75 File Offset: 0x00018C75
		public SteamDeviceFormFactor FormFactor
		{
			get
			{
				return SteamRemotePlay.Internal.GetSessionClientFormFactor(this.Id);
			}
		}

		// Token: 0x04000C1D RID: 3101
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <Id>k__BackingField;
	}
}
