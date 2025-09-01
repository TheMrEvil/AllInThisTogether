using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000668 RID: 1640
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x06003D52 RID: 15698 RVA: 0x000D46E7 File Offset: 0x000D28E7
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x000D46F6 File Offset: 0x000D28F6
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x04002778 RID: 10104
		private string m_typeName;
	}
}
