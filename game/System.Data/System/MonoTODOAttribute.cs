using System;

namespace System
{
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x00003D55 File Offset: 0x00001F55
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00010D46 File Offset: 0x0000EF46
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00010D55 File Offset: 0x0000EF55
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400062D RID: 1581
		private string comment;
	}
}
