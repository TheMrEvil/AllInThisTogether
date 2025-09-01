using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	[VisibleToOtherModules]
	internal class PreventExecutionInStateAttribute : Attribute, IBindingsPreventExecution
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000260E File Offset: 0x0000080E
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002616 File Offset: 0x00000816
		public object singleFlagValue
		{
			[CompilerGenerated]
			get
			{
				return this.<singleFlagValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<singleFlagValue>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000261F File Offset: 0x0000081F
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002627 File Offset: 0x00000827
		public PreventExecutionSeverity severity
		{
			[CompilerGenerated]
			get
			{
				return this.<severity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<severity>k__BackingField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002630 File Offset: 0x00000830
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002638 File Offset: 0x00000838
		public string howToFix
		{
			[CompilerGenerated]
			get
			{
				return this.<howToFix>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<howToFix>k__BackingField = value;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002641 File Offset: 0x00000841
		public PreventExecutionInStateAttribute(object systemAndFlags, PreventExecutionSeverity reportSeverity, string howToString = "")
		{
			this.singleFlagValue = systemAndFlags;
			this.severity = reportSeverity;
			this.howToFix = howToString;
		}

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object <singleFlagValue>k__BackingField;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PreventExecutionSeverity <severity>k__BackingField;

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <howToFix>k__BackingField;
	}
}
