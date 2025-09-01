using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002E7 RID: 743
	public class MissingTypeSpecReference
	{
		// Token: 0x0600236C RID: 9068 RVA: 0x000AD0A6 File Offset: 0x000AB2A6
		public MissingTypeSpecReference(TypeSpec type, MemberSpec caller)
		{
			this.Type = type;
			this.Caller = caller;
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x000AD0BC File Offset: 0x000AB2BC
		// (set) Token: 0x0600236E RID: 9070 RVA: 0x000AD0C4 File Offset: 0x000AB2C4
		public TypeSpec Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000AD0CD File Offset: 0x000AB2CD
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x000AD0D5 File Offset: 0x000AB2D5
		public MemberSpec Caller
		{
			[CompilerGenerated]
			get
			{
				return this.<Caller>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Caller>k__BackingField = value;
			}
		}

		// Token: 0x04000D7D RID: 3453
		[CompilerGenerated]
		private TypeSpec <Type>k__BackingField;

		// Token: 0x04000D7E RID: 3454
		[CompilerGenerated]
		private MemberSpec <Caller>k__BackingField;
	}
}
