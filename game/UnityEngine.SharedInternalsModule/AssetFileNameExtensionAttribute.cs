using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal sealed class AssetFileNameExtensionAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string preferredExtension
		{
			[CompilerGenerated]
			get
			{
				return this.<preferredExtension>k__BackingField;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public IEnumerable<string> otherExtensions
		{
			[CompilerGenerated]
			get
			{
				return this.<otherExtensions>k__BackingField;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public AssetFileNameExtensionAttribute(string preferredExtension, params string[] otherExtensions)
		{
			this.preferredExtension = preferredExtension;
			this.otherExtensions = otherExtensions;
		}

		// Token: 0x04000001 RID: 1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <preferredExtension>k__BackingField;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly IEnumerable<string> <otherExtensions>k__BackingField;
	}
}
