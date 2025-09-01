using System;

namespace UnityEngine
{
	// Token: 0x020001D7 RID: 471
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class InspectorNameAttribute : PropertyAttribute
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x00023196 File Offset: 0x00021396
		public InspectorNameAttribute(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x040007B1 RID: 1969
		public readonly string displayName;
	}
}
