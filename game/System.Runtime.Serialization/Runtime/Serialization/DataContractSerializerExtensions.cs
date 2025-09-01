using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.Serialization
{
	/// <summary>Extends the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class by providing methods for setting and getting an <see cref="T:System.Runtime.Serialization.ISerializationSurrogateProvider" />.</summary>
	// Token: 0x02000157 RID: 343
	public static class DataContractSerializerExtensions
	{
		/// <summary>Returns the surrogate serialization provider for this serializer.</summary>
		/// <param name="serializer">The serializer which is being surrogated.</param>
		/// <returns>The surrogate serializer.</returns>
		// Token: 0x06001249 RID: 4681 RVA: 0x00047058 File Offset: 0x00045258
		public static ISerializationSurrogateProvider GetSerializationSurrogateProvider(this DataContractSerializer serializer)
		{
			DataContractSerializerExtensions.SurrogateProviderAdapter surrogateProviderAdapter = serializer.DataContractSurrogate as DataContractSerializerExtensions.SurrogateProviderAdapter;
			if (surrogateProviderAdapter != null)
			{
				return surrogateProviderAdapter.Provider;
			}
			return null;
		}

		/// <summary>Specifies a surrogate serialization provider for this <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
		/// <param name="serializer">The serializer which is being surrogated.</param>
		/// <param name="provider">The surrogate serialization provider.</param>
		// Token: 0x0600124A RID: 4682 RVA: 0x0004707C File Offset: 0x0004527C
		public static void SetSerializationSurrogateProvider(this DataContractSerializer serializer, ISerializationSurrogateProvider provider)
		{
			IDataContractSurrogate value = (provider != null) ? new DataContractSerializerExtensions.SurrogateProviderAdapter(provider) : null;
			typeof(DataContractSerializer).GetField("dataContractSurrogate", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(serializer, value);
		}

		// Token: 0x02000158 RID: 344
		private class SurrogateProviderAdapter : IDataContractSurrogate
		{
			// Token: 0x0600124B RID: 4683 RVA: 0x000470B3 File Offset: 0x000452B3
			public SurrogateProviderAdapter(ISerializationSurrogateProvider provider)
			{
				this._provider = provider;
			}

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x0600124C RID: 4684 RVA: 0x000470C2 File Offset: 0x000452C2
			public ISerializationSurrogateProvider Provider
			{
				get
				{
					return this._provider;
				}
			}

			// Token: 0x0600124D RID: 4685 RVA: 0x000470CA File Offset: 0x000452CA
			public object GetCustomDataToExport(Type clrType, Type dataContractType)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x000470CA File Offset: 0x000452CA
			public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x000470D1 File Offset: 0x000452D1
			public Type GetDataContractType(Type type)
			{
				return this._provider.GetSurrogateType(type);
			}

			// Token: 0x06001250 RID: 4688 RVA: 0x000470DF File Offset: 0x000452DF
			public object GetDeserializedObject(object obj, Type targetType)
			{
				return this._provider.GetDeserializedObject(obj, targetType);
			}

			// Token: 0x06001251 RID: 4689 RVA: 0x000470CA File Offset: 0x000452CA
			public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x06001252 RID: 4690 RVA: 0x000470EE File Offset: 0x000452EE
			public object GetObjectToSerialize(object obj, Type targetType)
			{
				return this._provider.GetObjectToSerialize(obj, targetType);
			}

			// Token: 0x06001253 RID: 4691 RVA: 0x000470CA File Offset: 0x000452CA
			public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x000470CA File Offset: 0x000452CA
			public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
			{
				throw NotImplemented.ByDesign;
			}

			// Token: 0x04000744 RID: 1860
			private ISerializationSurrogateProvider _provider;
		}
	}
}
