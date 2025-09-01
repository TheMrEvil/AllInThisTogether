using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000BD RID: 189
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00005CE1 File Offset: 0x00003EE1
		public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false)
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00005CED File Offset: 0x00003EED
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00005D05 File Offset: 0x00003F05
		[NotNull]
		public string Contract
		{
			[CompilerGenerated]
			get
			{
				return this.<Contract>k__BackingField;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00005D0D File Offset: 0x00003F0D
		public bool ForceFullStates
		{
			[CompilerGenerated]
			get
			{
				return this.<ForceFullStates>k__BackingField;
			}
		}

		// Token: 0x04000246 RID: 582
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <Contract>k__BackingField;

		// Token: 0x04000247 RID: 583
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <ForceFullStates>k__BackingField;
	}
}
