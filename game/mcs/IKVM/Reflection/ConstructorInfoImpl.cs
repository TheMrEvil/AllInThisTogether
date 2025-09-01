using System;

namespace IKVM.Reflection
{
	// Token: 0x0200000B RID: 11
	internal sealed class ConstructorInfoImpl : ConstructorInfo
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003380 File Offset: 0x00001580
		internal ConstructorInfoImpl(MethodInfo method)
		{
			this.method = method;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003390 File Offset: 0x00001590
		public override bool Equals(object obj)
		{
			ConstructorInfoImpl constructorInfoImpl = obj as ConstructorInfoImpl;
			return constructorInfoImpl != null && constructorInfoImpl.method.Equals(this.method);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033C0 File Offset: 0x000015C0
		public override int GetHashCode()
		{
			return this.method.GetHashCode();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033CD File Offset: 0x000015CD
		internal override MethodInfo GetMethodInfo()
		{
			return this.method;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000033D5 File Offset: 0x000015D5
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this.method.GetMethodOnTypeDefinition();
		}

		// Token: 0x04000030 RID: 48
		private readonly MethodInfo method;
	}
}
