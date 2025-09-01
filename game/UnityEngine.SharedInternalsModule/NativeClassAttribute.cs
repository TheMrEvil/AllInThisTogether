using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	internal sealed class NativeClassAttribute : Attribute
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020A4 File Offset: 0x000002A4
		public string QualifiedNativeName
		{
			[CompilerGenerated]
			get
			{
				return this.<QualifiedNativeName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<QualifiedNativeName>k__BackingField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020AD File Offset: 0x000002AD
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020B5 File Offset: 0x000002B5
		public string Declaration
		{
			[CompilerGenerated]
			get
			{
				return this.<Declaration>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Declaration>k__BackingField = value;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020BE File Offset: 0x000002BE
		public NativeClassAttribute(string qualifiedCppName)
		{
			this.QualifiedNativeName = qualifiedCppName;
			this.Declaration = "class " + qualifiedCppName;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020E2 File Offset: 0x000002E2
		public NativeClassAttribute(string qualifiedCppName, string declaration)
		{
			this.QualifiedNativeName = qualifiedCppName;
			this.Declaration = declaration;
		}

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <QualifiedNativeName>k__BackingField;

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Declaration>k__BackingField;
	}
}
