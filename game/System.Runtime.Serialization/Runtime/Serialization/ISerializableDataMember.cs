using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000DC RID: 220
	internal class ISerializableDataMember
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00033036 File Offset: 0x00031236
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0003303E File Offset: 0x0003123E
		internal string Name
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00033047 File Offset: 0x00031247
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0003304F File Offset: 0x0003124F
		internal IDataNode Value
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

		// Token: 0x06000C8E RID: 3214 RVA: 0x0000222F File Offset: 0x0000042F
		public ISerializableDataMember()
		{
		}

		// Token: 0x04000531 RID: 1329
		private string name;

		// Token: 0x04000532 RID: 1330
		private IDataNode value;
	}
}
