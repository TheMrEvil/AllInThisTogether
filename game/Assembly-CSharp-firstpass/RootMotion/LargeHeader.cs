using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000BD RID: 189
	public class LargeHeader : PropertyAttribute
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x00039614 File Offset: 0x00037814
		public LargeHeader(string name)
		{
			this.name = name;
			this.color = "white";
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00039639 File Offset: 0x00037839
		public LargeHeader(string name, string color)
		{
			this.name = name;
			this.color = color;
		}

		// Token: 0x040006C6 RID: 1734
		public string name;

		// Token: 0x040006C7 RID: 1735
		public string color = "white";
	}
}
