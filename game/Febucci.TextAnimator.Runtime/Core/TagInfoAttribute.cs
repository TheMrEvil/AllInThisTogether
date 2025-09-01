using System;

namespace Febucci.UI.Core
{
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Class)]
	public class TagInfoAttribute : Attribute
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00006C95 File Offset: 0x00004E95
		public TagInfoAttribute(string tagID)
		{
			this.tagID = tagID;
		}

		// Token: 0x040000FA RID: 250
		public readonly string tagID;
	}
}
