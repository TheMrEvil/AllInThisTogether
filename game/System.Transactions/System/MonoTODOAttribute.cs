using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002064 File Offset: 0x00000264
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000206C File Offset: 0x0000026C
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207B File Offset: 0x0000027B
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400002A RID: 42
		private string comment;
	}
}
