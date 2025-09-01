using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200084A RID: 2122
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x060046C9 RID: 18121 RVA: 0x000E71B4 File Offset: 0x000E53B4
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04002D90 RID: 11664
		private string typeName;
	}
}
