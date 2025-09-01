using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class AssetListAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AssetListAttribute()
		{
			this.AutoPopulate = false;
			this.Tags = null;
			this.LayerNames = null;
			this.AssetNamePrefix = null;
			this.CustomFilterMethod = null;
		}

		// Token: 0x04000001 RID: 1
		public bool AutoPopulate;

		// Token: 0x04000002 RID: 2
		public string Tags;

		// Token: 0x04000003 RID: 3
		public string LayerNames;

		// Token: 0x04000004 RID: 4
		public string AssetNamePrefix;

		// Token: 0x04000005 RID: 5
		public string Path;

		// Token: 0x04000006 RID: 6
		public string CustomFilterMethod;
	}
}
