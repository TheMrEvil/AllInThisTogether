using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200091F RID: 2335
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x06004FA0 RID: 20384 RVA: 0x000FA2DB File Offset: 0x000F84DB
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x000FA2EA File Offset: 0x000F84EA
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00084B75 File Offset: 0x00082D75
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x000FA2EA File Offset: 0x000F84EA
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x000FA2EA File Offset: 0x000F84EA
		public int GetToken(SignatureHelper helper)
		{
			return this.m.AddRef(helper);
		}

		// Token: 0x0400314A RID: 12618
		private DynamicMethod m;
	}
}
