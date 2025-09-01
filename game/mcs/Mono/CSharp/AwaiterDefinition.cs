using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002D5 RID: 725
	public class AwaiterDefinition
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000AA512 File Offset: 0x000A8712
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x000AA51A File Offset: 0x000A871A
		public PropertySpec IsCompleted
		{
			[CompilerGenerated]
			get
			{
				return this.<IsCompleted>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsCompleted>k__BackingField = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000AA523 File Offset: 0x000A8723
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x000AA52B File Offset: 0x000A872B
		public MethodSpec GetResult
		{
			[CompilerGenerated]
			get
			{
				return this.<GetResult>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GetResult>k__BackingField = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000AA534 File Offset: 0x000A8734
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x000AA53C File Offset: 0x000A873C
		public bool INotifyCompletion
		{
			[CompilerGenerated]
			get
			{
				return this.<INotifyCompletion>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<INotifyCompletion>k__BackingField = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000AA545 File Offset: 0x000A8745
		public bool IsValidPattern
		{
			get
			{
				return this.IsCompleted != null && this.GetResult != null && this.IsCompleted.HasGet;
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00002CCC File Offset: 0x00000ECC
		public AwaiterDefinition()
		{
		}

		// Token: 0x04000D51 RID: 3409
		[CompilerGenerated]
		private PropertySpec <IsCompleted>k__BackingField;

		// Token: 0x04000D52 RID: 3410
		[CompilerGenerated]
		private MethodSpec <GetResult>k__BackingField;

		// Token: 0x04000D53 RID: 3411
		[CompilerGenerated]
		private bool <INotifyCompletion>k__BackingField;
	}
}
