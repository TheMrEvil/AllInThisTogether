using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class DisplayNameAttribute : Attribute
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		public DisplayNameAttribute(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x04000017 RID: 23
		public readonly string displayName;
	}
}
