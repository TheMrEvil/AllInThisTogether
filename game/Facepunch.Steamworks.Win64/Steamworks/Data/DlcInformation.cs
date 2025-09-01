using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks.Data
{
	// Token: 0x020001F2 RID: 498
	public struct DlcInformation
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00019A6D File Offset: 0x00017C6D
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x00019A75 File Offset: 0x00017C75
		public AppId AppId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<AppId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<AppId>k__BackingField = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00019A7E File Offset: 0x00017C7E
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x00019A86 File Offset: 0x00017C86
		public string Name
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00019A8F File Offset: 0x00017C8F
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00019A97 File Offset: 0x00017C97
		public bool Available
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Available>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Available>k__BackingField = value;
			}
		}

		// Token: 0x04000BED RID: 3053
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AppId <AppId>k__BackingField;

		// Token: 0x04000BEE RID: 3054
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Name>k__BackingField;

		// Token: 0x04000BEF RID: 3055
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <Available>k__BackingField;
	}
}
