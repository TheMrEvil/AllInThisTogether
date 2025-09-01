using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000694 RID: 1684
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x06003E12 RID: 15890 RVA: 0x000D6172 File Offset: 0x000D4372
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000D6181 File Offset: 0x000D4381
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000D6198 File Offset: 0x000D4398
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Unable to find assembly '{0}'.", new object[]
					{
						this.assemblyString
					}));
				}
			}
			return this.assembly;
		}

		// Token: 0x04002834 RID: 10292
		internal string assemblyString;

		// Token: 0x04002835 RID: 10293
		private Assembly assembly;
	}
}
