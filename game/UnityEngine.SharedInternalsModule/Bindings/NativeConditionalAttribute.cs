using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativeConditionalAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002130 File Offset: 0x00000330
		public string Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002139 File Offset: 0x00000339
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002141 File Offset: 0x00000341
		public string StubReturnStatement
		{
			[CompilerGenerated]
			get
			{
				return this.<StubReturnStatement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StubReturnStatement>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000214A File Offset: 0x0000034A
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002152 File Offset: 0x00000352
		public bool Enabled
		{
			[CompilerGenerated]
			get
			{
				return this.<Enabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002078 File Offset: 0x00000278
		public NativeConditionalAttribute()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000215B File Offset: 0x0000035B
		public NativeConditionalAttribute(string condition)
		{
			this.Condition = condition;
			this.Enabled = true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002175 File Offset: 0x00000375
		public NativeConditionalAttribute(bool enabled)
		{
			this.Enabled = enabled;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002187 File Offset: 0x00000387
		public NativeConditionalAttribute(string condition, bool enabled) : this(condition)
		{
			this.Enabled = enabled;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000219A File Offset: 0x0000039A
		public NativeConditionalAttribute(string condition, string stubReturnStatement, bool enabled) : this(condition, stubReturnStatement)
		{
			this.Enabled = enabled;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000021AE File Offset: 0x000003AE
		public NativeConditionalAttribute(string condition, string stubReturnStatement) : this(condition)
		{
			this.StubReturnStatement = stubReturnStatement;
		}

		// Token: 0x04000006 RID: 6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <Condition>k__BackingField;

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <StubReturnStatement>k__BackingField;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <Enabled>k__BackingField;
	}
}
