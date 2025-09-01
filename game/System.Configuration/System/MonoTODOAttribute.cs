using System;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020A8 File Offset: 0x000002A8
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B0 File Offset: 0x000002B0
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BF File Offset: 0x000002BF
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400002B RID: 43
		private string comment;
	}
}
