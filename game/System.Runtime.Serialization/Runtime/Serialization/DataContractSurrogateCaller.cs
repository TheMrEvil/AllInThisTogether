using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020000E6 RID: 230
	internal static class DataContractSurrogateCaller
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x000352E0 File Offset: 0x000334E0
		internal static Type GetDataContractType(IDataContractSurrogate surrogate, Type type)
		{
			if (DataContract.GetBuiltInDataContract(type) != null)
			{
				return type;
			}
			Type dataContractType = surrogate.GetDataContractType(type);
			if (dataContractType == null)
			{
				return type;
			}
			return dataContractType;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003530B File Offset: 0x0003350B
		internal static object GetObjectToSerialize(IDataContractSurrogate surrogate, object obj, Type objType, Type membertype)
		{
			if (obj == null)
			{
				return null;
			}
			if (DataContract.GetBuiltInDataContract(objType) != null)
			{
				return obj;
			}
			return surrogate.GetObjectToSerialize(obj, membertype);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00035324 File Offset: 0x00033524
		internal static object GetDeserializedObject(IDataContractSurrogate surrogate, object obj, Type objType, Type memberType)
		{
			if (obj == null)
			{
				return null;
			}
			if (DataContract.GetBuiltInDataContract(objType) != null)
			{
				return obj;
			}
			return surrogate.GetDeserializedObject(obj, memberType);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003533D File Offset: 0x0003353D
		internal static object GetCustomDataToExport(IDataContractSurrogate surrogate, MemberInfo memberInfo, Type dataContractType)
		{
			return surrogate.GetCustomDataToExport(memberInfo, dataContractType);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00035347 File Offset: 0x00033547
		internal static object GetCustomDataToExport(IDataContractSurrogate surrogate, Type clrType, Type dataContractType)
		{
			if (DataContract.GetBuiltInDataContract(clrType) != null)
			{
				return null;
			}
			return surrogate.GetCustomDataToExport(clrType, dataContractType);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003535B File Offset: 0x0003355B
		internal static void GetKnownCustomDataTypes(IDataContractSurrogate surrogate, Collection<Type> customDataTypes)
		{
			surrogate.GetKnownCustomDataTypes(customDataTypes);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00035364 File Offset: 0x00033564
		internal static Type GetReferencedTypeOnImport(IDataContractSurrogate surrogate, string typeName, string typeNamespace, object customData)
		{
			if (DataContract.GetBuiltInDataContract(typeName, typeNamespace) != null)
			{
				return null;
			}
			return surrogate.GetReferencedTypeOnImport(typeName, typeNamespace, customData);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003537A File Offset: 0x0003357A
		internal static CodeTypeDeclaration ProcessImportedType(IDataContractSurrogate surrogate, CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			return surrogate.ProcessImportedType(typeDeclaration, compileUnit);
		}
	}
}
