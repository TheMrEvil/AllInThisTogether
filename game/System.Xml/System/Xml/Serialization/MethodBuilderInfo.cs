using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200026F RID: 623
	internal class MethodBuilderInfo
	{
		// Token: 0x060017B8 RID: 6072 RVA: 0x0008B488 File Offset: 0x00089688
		public MethodBuilderInfo(MethodBuilder methodBuilder, Type[] parameterTypes)
		{
			this.MethodBuilder = methodBuilder;
			this.ParameterTypes = parameterTypes;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0000B528 File Offset: 0x00009728
		public void Validate(Type returnType, Type[] parameterTypes, MethodAttributes attributes)
		{
		}

		// Token: 0x0400187D RID: 6269
		public readonly MethodBuilder MethodBuilder;

		// Token: 0x0400187E RID: 6270
		public readonly Type[] ParameterTypes;
	}
}
