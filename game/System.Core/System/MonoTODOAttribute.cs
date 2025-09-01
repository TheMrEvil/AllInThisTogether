using System;

namespace System
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000023F5 File Offset: 0x000005F5
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000023FD File Offset: 0x000005FD
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000240C File Offset: 0x0000060C
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x040002B9 RID: 697
		private string comment;
	}
}
