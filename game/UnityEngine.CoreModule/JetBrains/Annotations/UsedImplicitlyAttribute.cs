using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000C1 RID: 193
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00005D52 File Offset: 0x00003F52
		public UsedImplicitlyAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00005D5E File Offset: 0x00003F5E
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00005D6A File Offset: 0x00003F6A
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00005D76 File Offset: 0x00003F76
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00005D8E File Offset: 0x00003F8E
		public ImplicitUseKindFlags UseKindFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<UseKindFlags>k__BackingField;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00005D96 File Offset: 0x00003F96
		public ImplicitUseTargetFlags TargetFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetFlags>k__BackingField;
			}
		}

		// Token: 0x0400024A RID: 586
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ImplicitUseKindFlags <UseKindFlags>k__BackingField;

		// Token: 0x0400024B RID: 587
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ImplicitUseTargetFlags <TargetFlags>k__BackingField;
	}
}
