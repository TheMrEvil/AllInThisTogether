using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class FolderPathAttribute : Attribute
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002102 File Offset: 0x00000302
		public FolderPathAttribute()
		{
		}

		// Token: 0x04000058 RID: 88
		public bool AbsolutePath;

		// Token: 0x04000059 RID: 89
		public string ParentFolder;

		// Token: 0x0400005A RID: 90
		[Obsolete("Use RequireExistingPath instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool RequireValidPath;

		// Token: 0x0400005B RID: 91
		public bool RequireExistingPath;

		// Token: 0x0400005C RID: 92
		public bool UseBackslashes;
	}
}
