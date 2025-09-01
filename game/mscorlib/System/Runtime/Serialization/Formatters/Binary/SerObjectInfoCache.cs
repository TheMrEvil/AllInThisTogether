using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006B1 RID: 1713
	internal sealed class SerObjectInfoCache
	{
		// Token: 0x06003F24 RID: 16164 RVA: 0x000DA860 File Offset: 0x000D8A60
		internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = typeName;
			this.assemblyString = assemblyName;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x000DA880 File Offset: 0x000D8A80
		internal SerObjectInfoCache(Type type)
		{
			TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
			this.fullTypeName = typeInformation.FullTypeName;
			this.assemblyString = typeInformation.AssemblyString;
			this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
		}

		// Token: 0x04002924 RID: 10532
		internal string fullTypeName;

		// Token: 0x04002925 RID: 10533
		internal string assemblyString;

		// Token: 0x04002926 RID: 10534
		internal bool hasTypeForwardedFrom;

		// Token: 0x04002927 RID: 10535
		internal MemberInfo[] memberInfos;

		// Token: 0x04002928 RID: 10536
		internal string[] memberNames;

		// Token: 0x04002929 RID: 10537
		internal Type[] memberTypes;
	}
}
