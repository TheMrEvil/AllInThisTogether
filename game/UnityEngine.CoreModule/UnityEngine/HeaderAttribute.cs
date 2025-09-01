using System;

namespace UnityEngine
{
	// Token: 0x020001DA RID: 474
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x060015E0 RID: 5600 RVA: 0x000231DE File Offset: 0x000213DE
		public HeaderAttribute(string header)
		{
			this.header = header;
		}

		// Token: 0x040007B4 RID: 1972
		public readonly string header;
	}
}
