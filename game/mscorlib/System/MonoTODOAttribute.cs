using System;

namespace System
{
	// Token: 0x020001DA RID: 474
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x00002050 File Offset: 0x00000250
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00051761 File Offset: 0x0004F961
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00051770 File Offset: 0x0004F970
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400146F RID: 5231
		private string comment;
	}
}
