using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000ED RID: 237
	internal sealed class KnownTypeDataContractResolver : DataContractResolver
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00035C8D File Offset: 0x00033E8D
		internal KnownTypeDataContractResolver(XmlObjectSerializerContext context)
		{
			this.context = context;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00035C9C File Offset: 0x00033E9C
		public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			if (type == null)
			{
				typeName = null;
				typeNamespace = null;
				return false;
			}
			if (declaredType != null && declaredType.IsInterface && CollectionDataContract.IsCollectionInterface(declaredType))
			{
				typeName = null;
				typeNamespace = null;
				return true;
			}
			DataContract dataContract = DataContract.GetDataContract(type);
			if (this.context.IsKnownType(dataContract, dataContract.KnownDataContracts, declaredType))
			{
				typeName = dataContract.Name;
				typeNamespace = dataContract.Namespace;
				return true;
			}
			typeName = null;
			typeNamespace = null;
			return false;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00035D18 File Offset: 0x00033F18
		public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			if (typeName == null || typeNamespace == null)
			{
				return null;
			}
			return this.context.ResolveNameFromKnownTypes(new XmlQualifiedName(typeName, typeNamespace));
		}

		// Token: 0x04000644 RID: 1604
		private XmlObjectSerializerContext context;
	}
}
