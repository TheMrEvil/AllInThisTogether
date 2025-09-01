using System;

namespace ES3Types
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Class)]
	public class ES3PropertiesAttribute : Attribute
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00009753 File Offset: 0x00007953
		public ES3PropertiesAttribute(params string[] members)
		{
			this.members = members;
		}

		// Token: 0x0400007F RID: 127
		public readonly string[] members;
	}
}
