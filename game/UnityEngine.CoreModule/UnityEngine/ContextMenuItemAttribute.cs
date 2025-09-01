using System;

namespace UnityEngine
{
	// Token: 0x020001D6 RID: 470
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class ContextMenuItemAttribute : PropertyAttribute
	{
		// Token: 0x060015DB RID: 5595 RVA: 0x0002317E File Offset: 0x0002137E
		public ContextMenuItemAttribute(string name, string function)
		{
			this.name = name;
			this.function = function;
		}

		// Token: 0x040007AF RID: 1967
		public readonly string name;

		// Token: 0x040007B0 RID: 1968
		public readonly string function;
	}
}
