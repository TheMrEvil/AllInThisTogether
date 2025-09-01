using System;

namespace System
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
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
