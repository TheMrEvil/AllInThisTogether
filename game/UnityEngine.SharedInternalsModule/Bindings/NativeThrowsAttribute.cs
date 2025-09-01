using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativeThrowsAttribute : Attribute, IBindingsThrowsProviderAttribute, IBindingsAttribute
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000025A5 File Offset: 0x000007A5
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000025AD File Offset: 0x000007AD
		public bool ThrowsException
		{
			[CompilerGenerated]
			get
			{
				return this.<ThrowsException>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ThrowsException>k__BackingField = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000025B6 File Offset: 0x000007B6
		public NativeThrowsAttribute()
		{
			this.ThrowsException = true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000025C8 File Offset: 0x000007C8
		public NativeThrowsAttribute(bool throwsException)
		{
			this.ThrowsException = throwsException;
		}

		// Token: 0x04000025 RID: 37
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <ThrowsException>k__BackingField;
	}
}
