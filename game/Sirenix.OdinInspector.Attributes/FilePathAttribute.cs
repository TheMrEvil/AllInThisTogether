using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class FilePathAttribute : Attribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000274A File Offset: 0x0000094A
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002752 File Offset: 0x00000952
		[Obsolete("Add a ReadOnly attribute to the property instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ReadOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<ReadOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReadOnly>k__BackingField = value;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000275B File Offset: 0x0000095B
		public FilePathAttribute()
		{
		}

		// Token: 0x04000050 RID: 80
		public bool AbsolutePath;

		// Token: 0x04000051 RID: 81
		public string Extensions;

		// Token: 0x04000052 RID: 82
		public string ParentFolder;

		// Token: 0x04000053 RID: 83
		[Obsolete("Use RequireExistingPath instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool RequireValidPath;

		// Token: 0x04000054 RID: 84
		public bool RequireExistingPath;

		// Token: 0x04000055 RID: 85
		public bool UseBackslashes;

		// Token: 0x04000056 RID: 86
		public bool IncludeFileExtension = true;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		private bool <ReadOnly>k__BackingField;
	}
}
