using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks
{
	// Token: 0x020000BC RID: 188
	internal abstract class SteamInterface
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0001257E File Offset: 0x0001077E
		public virtual IntPtr GetUserInterfacePointer()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00012585 File Offset: 0x00010785
		public virtual IntPtr GetServerInterfacePointer()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001258C File Offset: 0x0001078C
		public virtual IntPtr GetGlobalInterfacePointer()
		{
			return IntPtr.Zero;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00012593 File Offset: 0x00010793
		public bool IsValid
		{
			get
			{
				return this.Self != IntPtr.Zero;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000125A5 File Offset: 0x000107A5
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000125AD File Offset: 0x000107AD
		public bool IsServer
		{
			[CompilerGenerated]
			get
			{
				return this.<IsServer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsServer>k__BackingField = value;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000125B8 File Offset: 0x000107B8
		internal void SetupInterface(bool gameServer)
		{
			bool flag = this.Self != IntPtr.Zero;
			if (!flag)
			{
				this.IsServer = gameServer;
				this.SelfGlobal = this.GetGlobalInterfacePointer();
				this.Self = this.SelfGlobal;
				bool flag2 = this.Self != IntPtr.Zero;
				if (!flag2)
				{
					if (gameServer)
					{
						this.SelfServer = this.GetServerInterfacePointer();
						this.Self = this.SelfServer;
					}
					else
					{
						this.SelfClient = this.GetUserInterfacePointer();
						this.Self = this.SelfClient;
					}
				}
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001264D File Offset: 0x0001084D
		internal void ShutdownInterface()
		{
			this.Self = IntPtr.Zero;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001265B File Offset: 0x0001085B
		protected SteamInterface()
		{
		}

		// Token: 0x0400077A RID: 1914
		public IntPtr Self;

		// Token: 0x0400077B RID: 1915
		public IntPtr SelfGlobal;

		// Token: 0x0400077C RID: 1916
		public IntPtr SelfServer;

		// Token: 0x0400077D RID: 1917
		public IntPtr SelfClient;

		// Token: 0x0400077E RID: 1918
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <IsServer>k__BackingField;
	}
}
