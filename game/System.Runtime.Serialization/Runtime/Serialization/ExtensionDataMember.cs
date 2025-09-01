using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000D5 RID: 213
	internal class ExtensionDataMember
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00032BDF File Offset: 0x00030DDF
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00032BE7 File Offset: 0x00030DE7
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00032BF0 File Offset: 0x00030DF0
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x00032BF8 File Offset: 0x00030DF8
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00032C01 File Offset: 0x00030E01
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x00032C09 File Offset: 0x00030E09
		public IDataNode Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00032C12 File Offset: 0x00030E12
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x00032C1A File Offset: 0x00030E1A
		public int MemberIndex
		{
			get
			{
				return this.memberIndex;
			}
			set
			{
				this.memberIndex = value;
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0000222F File Offset: 0x0000042F
		public ExtensionDataMember()
		{
		}

		// Token: 0x0400051A RID: 1306
		private string name;

		// Token: 0x0400051B RID: 1307
		private string ns;

		// Token: 0x0400051C RID: 1308
		private IDataNode value;

		// Token: 0x0400051D RID: 1309
		private int memberIndex;
	}
}
