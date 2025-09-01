using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000939 RID: 2361
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x060051E6 RID: 20966 RVA: 0x0010049F File Offset: 0x000FE69F
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x001004AE File Offset: 0x000FE6AE
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x001004BC File Offset: 0x000FE6BC
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.mb.GetToken(member, create_open_instance);
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x001004CB File Offset: 0x000FE6CB
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x001004DA File Offset: 0x000FE6DA
		public int GetToken(SignatureHelper helper)
		{
			return this.mb.GetToken(helper);
		}

		// Token: 0x040031FE RID: 12798
		private ModuleBuilder mb;
	}
}
