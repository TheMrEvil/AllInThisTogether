using System;

namespace UnityEngine
{
	// Token: 0x020001D8 RID: 472
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x000231A7 File Offset: 0x000213A7
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x040007B2 RID: 1970
		public readonly string tooltip;
	}
}
