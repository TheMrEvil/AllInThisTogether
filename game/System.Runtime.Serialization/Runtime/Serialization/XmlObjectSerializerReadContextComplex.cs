using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Serialization
{
	// Token: 0x02000148 RID: 328
	internal class XmlObjectSerializerReadContextComplex : XmlObjectSerializerReadContext
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x0004127F File Offset: 0x0003F47F
		internal XmlObjectSerializerReadContextComplex(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver dataContractResolver) : base(serializer, rootTypeDataContract, dataContractResolver)
		{
			this.mode = SerializationMode.SharedContract;
			this.preserveObjectReferences = serializer.PreserveObjectReferences;
			this.dataContractSurrogate = serializer.DataContractSurrogate;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000412A9 File Offset: 0x0003F4A9
		internal XmlObjectSerializerReadContextComplex(NetDataContractSerializer serializer) : base(serializer)
		{
			this.mode = SerializationMode.SharedType;
			this.preserveObjectReferences = true;
			this.binder = serializer.Binder;
			this.surrogateSelector = serializer.SurrogateSelector;
			this.assemblyFormat = serializer.AssemblyFormat;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000412E4 File Offset: 0x0003F4E4
		internal XmlObjectSerializerReadContextComplex(XmlObjectSerializer serializer, int maxItemsInObjectGraph, StreamingContext streamingContext, bool ignoreExtensionDataObject) : base(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject)
		{
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000412F1 File Offset: 0x0003F4F1
		internal override SerializationMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000412FC File Offset: 0x0003F4FC
		internal override DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
		{
			DataContract dataContract = null;
			if (this.mode == SerializationMode.SharedType && this.surrogateSelector != null)
			{
				dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(this.surrogateSelector, base.GetStreamingContext(), typeHandle, null, ref this.surrogateDataContracts);
			}
			if (dataContract == null)
			{
				return base.GetDataContract(id, typeHandle);
			}
			if (this.IsGetOnlyCollection && dataContract is SurrogateDataContract)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Surrogates with get-only collections are not supported. Found on type '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(dataContract.UnderlyingType)
				})));
			}
			return dataContract;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004137C File Offset: 0x0003F57C
		internal override DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContract = null;
			if (this.mode == SerializationMode.SharedType && this.surrogateSelector != null)
			{
				dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(this.surrogateSelector, base.GetStreamingContext(), typeHandle, type, ref this.surrogateDataContracts);
			}
			if (dataContract == null)
			{
				return base.GetDataContract(typeHandle, type);
			}
			if (this.IsGetOnlyCollection && dataContract is SurrogateDataContract)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Surrogates with get-only collections are not supported. Found on type '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(dataContract.UnderlyingType)
				})));
			}
			return dataContract;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000413FC File Offset: 0x0003F5FC
		public override object InternalDeserialize(XmlReaderDelegator xmlReader, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle, string name, string ns)
		{
			if (this.mode != SerializationMode.SharedContract)
			{
				return this.InternalDeserializeInSharedTypeMode(xmlReader, declaredTypeID, Type.GetTypeFromHandle(declaredTypeHandle), name, ns);
			}
			if (this.dataContractSurrogate == null)
			{
				return base.InternalDeserialize(xmlReader, declaredTypeID, declaredTypeHandle, name, ns);
			}
			return this.InternalDeserializeWithSurrogate(xmlReader, Type.GetTypeFromHandle(declaredTypeHandle), null, name, ns);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004144C File Offset: 0x0003F64C
		internal override object InternalDeserialize(XmlReaderDelegator xmlReader, Type declaredType, string name, string ns)
		{
			if (this.mode != SerializationMode.SharedContract)
			{
				return this.InternalDeserializeInSharedTypeMode(xmlReader, -1, declaredType, name, ns);
			}
			if (this.dataContractSurrogate == null)
			{
				return base.InternalDeserialize(xmlReader, declaredType, name, ns);
			}
			return this.InternalDeserializeWithSurrogate(xmlReader, declaredType, null, name, ns);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00041483 File Offset: 0x0003F683
		internal override object InternalDeserialize(XmlReaderDelegator xmlReader, Type declaredType, DataContract dataContract, string name, string ns)
		{
			if (this.mode != SerializationMode.SharedContract)
			{
				return this.InternalDeserializeInSharedTypeMode(xmlReader, -1, declaredType, name, ns);
			}
			if (this.dataContractSurrogate == null)
			{
				return base.InternalDeserialize(xmlReader, declaredType, dataContract, name, ns);
			}
			return this.InternalDeserializeWithSurrogate(xmlReader, declaredType, dataContract, name, ns);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000414C0 File Offset: 0x0003F6C0
		private object InternalDeserializeInSharedTypeMode(XmlReaderDelegator xmlReader, int declaredTypeID, Type declaredType, string name, string ns)
		{
			object result = null;
			if (base.TryHandleNullOrRef(xmlReader, declaredType, name, ns, ref result))
			{
				return result;
			}
			string clrAssembly = this.attributes.ClrAssembly;
			string clrType = this.attributes.ClrType;
			DataContract dataContract;
			if (clrAssembly != null && clrType != null)
			{
				Assembly assembly;
				Type left;
				dataContract = this.ResolveDataContractInSharedTypeMode(clrAssembly, clrType, out assembly, out left);
				if (dataContract == null)
				{
					if (assembly == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Assembly '{0}' was not found.", new object[]
						{
							clrAssembly
						})));
					}
					if (left == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("CLR type '{1}' in assembly '{0}' is not found.", new object[]
						{
							assembly.FullName,
							clrType
						})));
					}
				}
				if (declaredType != null && declaredType.IsArray)
				{
					dataContract = ((declaredTypeID < 0) ? base.GetDataContract(declaredType) : this.GetDataContract(declaredTypeID, declaredType.TypeHandle));
				}
			}
			else
			{
				if (clrAssembly != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(xmlReader, SR.GetString("Attribute was not found for CLR type '{1}' in namespace '{0}'. XML reader node is on {2}, '{4}' node in '{3}' namespace.", new object[]
					{
						"http://schemas.microsoft.com/2003/10/Serialization/",
						"Type",
						xmlReader.NodeType,
						xmlReader.NamespaceURI,
						xmlReader.LocalName
					}))));
				}
				if (clrType != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(xmlReader, SR.GetString("Attribute was not found for CLR type '{1}' in namespace '{0}'. XML reader node is on {2}, '{4}' node in '{3}' namespace.", new object[]
					{
						"http://schemas.microsoft.com/2003/10/Serialization/",
						"Assembly",
						xmlReader.NodeType,
						xmlReader.NamespaceURI,
						xmlReader.LocalName
					}))));
				}
				if (declaredType == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(xmlReader, SR.GetString("Attribute was not found for CLR type '{1}' in namespace '{0}'. XML reader node is on {2}, '{4}' node in '{3}' namespace.", new object[]
					{
						"http://schemas.microsoft.com/2003/10/Serialization/",
						"Type",
						xmlReader.NodeType,
						xmlReader.NamespaceURI,
						xmlReader.LocalName
					}))));
				}
				dataContract = ((declaredTypeID < 0) ? base.GetDataContract(declaredType) : this.GetDataContract(declaredTypeID, declaredType.TypeHandle));
			}
			return this.ReadDataContractValue(dataContract, xmlReader);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000416D0 File Offset: 0x0003F8D0
		private object InternalDeserializeWithSurrogate(XmlReaderDelegator xmlReader, Type declaredType, DataContract surrogateDataContract, string name, string ns)
		{
			if (TD.DCDeserializeWithSurrogateStartIsEnabled())
			{
				TD.DCDeserializeWithSurrogateStart(declaredType.FullName);
			}
			DataContract dataContract = surrogateDataContract ?? base.GetDataContract(DataContractSurrogateCaller.GetDataContractType(this.dataContractSurrogate, declaredType));
			if (this.IsGetOnlyCollection && dataContract.UnderlyingType != declaredType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Surrogates with get-only collections are not supported. Found on type '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(declaredType)
				})));
			}
			this.ReadAttributes(xmlReader);
			string objectId = base.GetObjectId();
			object obj = base.InternalDeserialize(xmlReader, name, ns, declaredType, ref dataContract);
			object deserializedObject = DataContractSurrogateCaller.GetDeserializedObject(this.dataContractSurrogate, obj, dataContract.UnderlyingType, declaredType);
			base.ReplaceDeserializedObject(objectId, obj, deserializedObject);
			if (TD.DCDeserializeWithSurrogateStopIsEnabled())
			{
				TD.DCDeserializeWithSurrogateStop();
			}
			return deserializedObject;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00041788 File Offset: 0x0003F988
		private Type ResolveDataContractTypeInSharedTypeMode(string assemblyName, string typeName, out Assembly assembly)
		{
			assembly = null;
			Type type = null;
			if (this.binder != null)
			{
				type = this.binder.BindToType(assemblyName, typeName);
			}
			if (type == null)
			{
				XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeKey key = new XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeKey(assemblyName, typeName);
				XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeInfo xmlObjectDataContractTypeInfo = (XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeInfo)XmlObjectSerializerReadContextComplex.dataContractTypeCache[key];
				if (xmlObjectDataContractTypeInfo == null)
				{
					if (this.assemblyFormat == FormatterAssemblyStyle.Full)
					{
						if (assemblyName == "0")
						{
							assembly = Globals.TypeOfInt.Assembly;
						}
						else
						{
							assembly = Assembly.Load(assemblyName);
						}
						if (assembly != null)
						{
							type = assembly.GetType(typeName);
						}
					}
					else
					{
						assembly = XmlObjectSerializerReadContextComplex.ResolveSimpleAssemblyName(assemblyName);
						if (assembly != null)
						{
							try
							{
								type = assembly.GetType(typeName);
							}
							catch (TypeLoadException)
							{
							}
							catch (FileNotFoundException)
							{
							}
							catch (FileLoadException)
							{
							}
							catch (BadImageFormatException)
							{
							}
							if (type == null)
							{
								type = Type.GetType(typeName, new Func<AssemblyName, Assembly>(XmlObjectSerializerReadContextComplex.ResolveSimpleAssemblyName), new Func<Assembly, string, bool, Type>(new XmlObjectSerializerReadContextComplex.TopLevelAssemblyTypeResolver(assembly).ResolveType), false);
							}
						}
					}
					if (!(type != null))
					{
						return type;
					}
					XmlObjectSerializerReadContextComplex.CheckTypeForwardedTo(assembly, type.Assembly, type);
					xmlObjectDataContractTypeInfo = new XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeInfo(assembly, type);
					Hashtable obj = XmlObjectSerializerReadContextComplex.dataContractTypeCache;
					lock (obj)
					{
						if (!XmlObjectSerializerReadContextComplex.dataContractTypeCache.ContainsKey(key))
						{
							XmlObjectSerializerReadContextComplex.dataContractTypeCache[key] = xmlObjectDataContractTypeInfo;
						}
						return type;
					}
				}
				assembly = xmlObjectDataContractTypeInfo.Assembly;
				type = xmlObjectDataContractTypeInfo.Type;
			}
			return type;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004191C File Offset: 0x0003FB1C
		private DataContract ResolveDataContractInSharedTypeMode(string assemblyName, string typeName, out Assembly assembly, out Type type)
		{
			type = this.ResolveDataContractTypeInSharedTypeMode(assemblyName, typeName, out assembly);
			if (type != null)
			{
				return base.GetDataContract(type);
			}
			return null;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00041940 File Offset: 0x0003FB40
		protected override DataContract ResolveDataContractFromTypeName()
		{
			if (this.mode == SerializationMode.SharedContract)
			{
				return base.ResolveDataContractFromTypeName();
			}
			if (this.attributes.ClrAssembly != null && this.attributes.ClrType != null)
			{
				Assembly assembly;
				Type type;
				return this.ResolveDataContractInSharedTypeMode(this.attributes.ClrAssembly, this.attributes.ClrType, out assembly, out type);
			}
			return null;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00041998 File Offset: 0x0003FB98
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool CheckIfTypeSerializableForSharedTypeMode(Type memberType)
		{
			ISurrogateSelector surrogateSelector;
			return this.surrogateSelector.GetSurrogate(memberType, base.GetStreamingContext(), out surrogateSelector) != null;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000419BC File Offset: 0x0003FBBC
		internal override void CheckIfTypeSerializable(Type memberType, bool isMemberTypeSerializable)
		{
			if (this.mode == SerializationMode.SharedType && this.surrogateSelector != null && this.CheckIfTypeSerializableForSharedTypeMode(memberType))
			{
				return;
			}
			if (this.dataContractSurrogate == null)
			{
				base.CheckIfTypeSerializable(memberType, isMemberTypeSerializable);
				return;
			}
			while (memberType.IsArray)
			{
				memberType = memberType.GetElementType();
			}
			memberType = DataContractSurrogateCaller.GetDataContractType(this.dataContractSurrogate, memberType);
			if (!DataContract.IsTypeSerializable(memberType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot be serialized. Consider marking it with the DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute attribute. Alternatively, you can ensure that the type is public and has a parameterless constructor - all public members of the type will then be serialized, and no attributes will be required.", new object[]
				{
					memberType
				})));
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00041A3C File Offset: 0x0003FC3C
		internal override Type GetSurrogatedType(Type type)
		{
			if (this.dataContractSurrogate == null)
			{
				return base.GetSurrogatedType(type);
			}
			type = DataContract.UnwrapNullableType(type);
			Type surrogatedType = DataContractSerializer.GetSurrogatedType(this.dataContractSurrogate, type);
			if (this.IsGetOnlyCollection && surrogatedType != type)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Surrogates with get-only collections are not supported. Found on type '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(type)
				})));
			}
			return surrogatedType;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00041AA4 File Offset: 0x0003FCA4
		internal override int GetArraySize()
		{
			if (!this.preserveObjectReferences)
			{
				return -1;
			}
			return this.attributes.ArraySZSize;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00041ABB File Offset: 0x0003FCBB
		private static Assembly ResolveSimpleAssemblyName(AssemblyName assemblyName)
		{
			return XmlObjectSerializerReadContextComplex.ResolveSimpleAssemblyName(assemblyName.FullName);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00041AC8 File Offset: 0x0003FCC8
		private static Assembly ResolveSimpleAssemblyName(string assemblyName)
		{
			Assembly assembly;
			if (assemblyName == "0")
			{
				assembly = Globals.TypeOfInt.Assembly;
			}
			else
			{
				assembly = Assembly.LoadWithPartialName(assemblyName);
				if (assembly == null)
				{
					assembly = Assembly.LoadWithPartialName(new AssemblyName(assemblyName)
					{
						Version = null
					}.FullName);
				}
			}
			return assembly;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00041B18 File Offset: 0x0003FD18
		[SecuritySafeCritical]
		private static void CheckTypeForwardedTo(Assembly sourceAssembly, Assembly destinationAssembly, Type resolvedType)
		{
			if (sourceAssembly != destinationAssembly && !NetDataContractSerializer.UnsafeTypeForwardingEnabled && !sourceAssembly.IsFullyTrusted && !destinationAssembly.PermissionSet.IsSubsetOf(sourceAssembly.PermissionSet))
			{
				TypeInformation typeInformation = NetDataContractSerializer.GetTypeInformation(resolvedType);
				if (typeInformation.HasTypeForwardedFrom)
				{
					Assembly left = null;
					try
					{
						left = Assembly.Load(typeInformation.AssemblyString);
					}
					catch
					{
					}
					if (left == sourceAssembly)
					{
						return;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Cannot deserialize forwarded type '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(resolvedType)
				})));
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00041BB0 File Offset: 0x0003FDB0
		// Note: this type is marked as 'beforefieldinit'.
		static XmlObjectSerializerReadContextComplex()
		{
		}

		// Token: 0x0400070A RID: 1802
		private static Hashtable dataContractTypeCache = new Hashtable();

		// Token: 0x0400070B RID: 1803
		private bool preserveObjectReferences;

		// Token: 0x0400070C RID: 1804
		protected IDataContractSurrogate dataContractSurrogate;

		// Token: 0x0400070D RID: 1805
		private SerializationMode mode;

		// Token: 0x0400070E RID: 1806
		private SerializationBinder binder;

		// Token: 0x0400070F RID: 1807
		private ISurrogateSelector surrogateSelector;

		// Token: 0x04000710 RID: 1808
		private FormatterAssemblyStyle assemblyFormat;

		// Token: 0x04000711 RID: 1809
		private Hashtable surrogateDataContracts;

		// Token: 0x02000149 RID: 329
		private sealed class TopLevelAssemblyTypeResolver
		{
			// Token: 0x06001075 RID: 4213 RVA: 0x00041BBC File Offset: 0x0003FDBC
			public TopLevelAssemblyTypeResolver(Assembly topLevelAssembly)
			{
				this.topLevelAssembly = topLevelAssembly;
			}

			// Token: 0x06001076 RID: 4214 RVA: 0x00041BCB File Offset: 0x0003FDCB
			public Type ResolveType(Assembly assembly, string simpleTypeName, bool ignoreCase)
			{
				if (assembly == null)
				{
					assembly = this.topLevelAssembly;
				}
				return assembly.GetType(simpleTypeName, false, ignoreCase);
			}

			// Token: 0x04000712 RID: 1810
			private Assembly topLevelAssembly;
		}

		// Token: 0x0200014A RID: 330
		private class XmlObjectDataContractTypeInfo
		{
			// Token: 0x06001077 RID: 4215 RVA: 0x00041BE7 File Offset: 0x0003FDE7
			public XmlObjectDataContractTypeInfo(Assembly assembly, Type type)
			{
				this.assembly = assembly;
				this.type = type;
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06001078 RID: 4216 RVA: 0x00041BFD File Offset: 0x0003FDFD
			public Assembly Assembly
			{
				get
				{
					return this.assembly;
				}
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x06001079 RID: 4217 RVA: 0x00041C05 File Offset: 0x0003FE05
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000713 RID: 1811
			private Assembly assembly;

			// Token: 0x04000714 RID: 1812
			private Type type;
		}

		// Token: 0x0200014B RID: 331
		private class XmlObjectDataContractTypeKey
		{
			// Token: 0x0600107A RID: 4218 RVA: 0x00041C0D File Offset: 0x0003FE0D
			public XmlObjectDataContractTypeKey(string assemblyName, string typeName)
			{
				this.assemblyName = assemblyName;
				this.typeName = typeName;
			}

			// Token: 0x0600107B RID: 4219 RVA: 0x00041C24 File Offset: 0x0003FE24
			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeKey xmlObjectDataContractTypeKey = obj as XmlObjectSerializerReadContextComplex.XmlObjectDataContractTypeKey;
				return xmlObjectDataContractTypeKey != null && !(this.assemblyName != xmlObjectDataContractTypeKey.assemblyName) && !(this.typeName != xmlObjectDataContractTypeKey.typeName);
			}

			// Token: 0x0600107C RID: 4220 RVA: 0x00041C70 File Offset: 0x0003FE70
			public override int GetHashCode()
			{
				int num = 0;
				if (this.assemblyName != null)
				{
					num = this.assemblyName.GetHashCode();
				}
				if (this.typeName != null)
				{
					num ^= this.typeName.GetHashCode();
				}
				return num;
			}

			// Token: 0x04000715 RID: 1813
			private string assemblyName;

			// Token: 0x04000716 RID: 1814
			private string typeName;
		}
	}
}
