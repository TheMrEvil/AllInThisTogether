using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000BA RID: 186
	public class InspectorComment : PropertyAttribute
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00038CD5 File Offset: 0x00036ED5
		public InspectorComment(string name)
		{
			this.name = name;
			this.color = "white";
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00038CFA File Offset: 0x00036EFA
		public InspectorComment(string name, string color)
		{
			this.name = name;
			this.color = color;
		}

		// Token: 0x040006A8 RID: 1704
		public string name;

		// Token: 0x040006A9 RID: 1705
		public string color = "white";
	}
}
