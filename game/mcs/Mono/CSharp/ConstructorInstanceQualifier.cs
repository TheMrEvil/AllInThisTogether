using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001BF RID: 447
	internal struct ConstructorInstanceQualifier : OverloadResolver.IInstanceQualifier
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0006F875 File Offset: 0x0006DA75
		public ConstructorInstanceQualifier(TypeSpec type)
		{
			this = default(ConstructorInstanceQualifier);
			this.InstanceType = type;
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0006F885 File Offset: 0x0006DA85
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0006F88D File Offset: 0x0006DA8D
		public TypeSpec InstanceType
		{
			[CompilerGenerated]
			get
			{
				return this.<InstanceType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<InstanceType>k__BackingField = value;
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0006F896 File Offset: 0x0006DA96
		public bool CheckProtectedMemberAccess(ResolveContext rc, MemberSpec member)
		{
			return MemberExpr.CheckProtectedMemberAccess<MemberSpec>(rc, member, this.InstanceType);
		}

		// Token: 0x04000972 RID: 2418
		[CompilerGenerated]
		private TypeSpec <InstanceType>k__BackingField;
	}
}
