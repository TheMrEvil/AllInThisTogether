using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200013E RID: 318
	public class ConstSpec : FieldSpec
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x0004174D File Offset: 0x0003F94D
		public ConstSpec(TypeSpec declaringType, IMemberDefinition definition, TypeSpec memberType, FieldInfo fi, Modifiers mod, Expression value) : base(declaringType, definition, memberType, fi, mod)
		{
			this.value = value;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00041764 File Offset: 0x0003F964
		public Expression Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0004176C File Offset: 0x0003F96C
		public Constant GetConstant(ResolveContext rc)
		{
			if (this.value.eclass != ExprClass.Value)
			{
				this.value = this.value.Resolve(rc);
			}
			return (Constant)this.value;
		}

		// Token: 0x04000731 RID: 1841
		private Expression value;
	}
}
