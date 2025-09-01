using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000195 RID: 405
	internal class XmlObjectSerializerWriteContextComplexJson : XmlObjectSerializerWriteContextComplex
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x00050894 File Offset: 0x0004EA94
		public XmlObjectSerializerWriteContextComplexJson(DataContractJsonSerializer serializer, DataContract rootTypeDataContract) : base(serializer, serializer.MaxItemsInObjectGraph, new StreamingContext(StreamingContextStates.All), serializer.IgnoreExtensionDataObject)
		{
			this.emitXsiType = serializer.EmitTypeInformation;
			this.rootTypeDataContract = rootTypeDataContract;
			this.serializerKnownTypeList = serializer.knownTypeList;
			this.dataContractSurrogate = serializer.DataContractSurrogate;
			this.serializeReadOnlyTypes = serializer.SerializeReadOnlyTypes;
			this.useSimpleDictionaryFormat = serializer.UseSimpleDictionaryFormat;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00050901 File Offset: 0x0004EB01
		internal static XmlObjectSerializerWriteContextComplexJson CreateContext(DataContractJsonSerializer serializer, DataContract rootTypeDataContract)
		{
			return new XmlObjectSerializerWriteContextComplexJson(serializer, rootTypeDataContract);
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x000503A1 File Offset: 0x0004E5A1
		internal IList<Type> SerializerKnownTypeList
		{
			get
			{
				return this.serializerKnownTypeList;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0005090A File Offset: 0x0004EB0A
		public bool UseSimpleDictionaryFormat
		{
			get
			{
				return this.useSimpleDictionaryFormat;
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, Type dataContractType, string clrTypeName, string clrAssemblyName)
		{
			return false;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, DataContract dataContract)
		{
			return false;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal override void WriteArraySize(XmlWriterDelegator xmlWriter, int size)
		{
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00050912 File Offset: 0x0004EB12
		protected override void WriteTypeInfo(XmlWriterDelegator writer, string dataContractName, string dataContractNamespace)
		{
			if (this.emitXsiType != EmitTypeInformation.Never)
			{
				if (string.IsNullOrEmpty(dataContractNamespace))
				{
					this.WriteTypeInfo(writer, dataContractName);
					return;
				}
				this.WriteTypeInfo(writer, dataContractName + ":" + XmlObjectSerializerWriteContextComplexJson.TruncateDefaultDataContractNamespace(dataContractNamespace));
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00050948 File Offset: 0x0004EB48
		internal static string TruncateDefaultDataContractNamespace(string dataContractNamespace)
		{
			if (!string.IsNullOrEmpty(dataContractNamespace))
			{
				if (dataContractNamespace[0] == '#')
				{
					return "\\" + dataContractNamespace;
				}
				if (dataContractNamespace[0] == '\\')
				{
					return "\\" + dataContractNamespace;
				}
				if (dataContractNamespace.StartsWith("http://schemas.datacontract.org/2004/07/", StringComparison.Ordinal))
				{
					return "#" + dataContractNamespace.Substring(JsonGlobals.DataContractXsdBaseNamespaceLength);
				}
			}
			return dataContractNamespace;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x000509B0 File Offset: 0x0004EBB0
		private static bool RequiresJsonTypeInfo(DataContract contract)
		{
			return contract is ClassDataContract;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x000509BB File Offset: 0x0004EBBB
		private void WriteTypeInfo(XmlWriterDelegator writer, string typeInformation)
		{
			writer.WriteAttributeString(null, "__type", null, typeInformation);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000509CC File Offset: 0x0004EBCC
		protected override bool WriteTypeInfo(XmlWriterDelegator writer, DataContract contract, DataContract declaredContract)
		{
			if ((contract.Name != declaredContract.Name || contract.Namespace != declaredContract.Namespace) && (!(contract.Name.Value == declaredContract.Name.Value) || !(contract.Namespace.Value == declaredContract.Namespace.Value)) && contract.UnderlyingType != Globals.TypeOfObjectArray && this.emitXsiType != EmitTypeInformation.Never)
			{
				if (XmlObjectSerializerWriteContextComplexJson.RequiresJsonTypeInfo(contract))
				{
					this.perCallXsiTypeAlreadyEmitted = true;
					this.WriteTypeInfo(writer, contract.Name.Value, contract.Namespace.Value);
				}
				else if (declaredContract.UnderlyingType == typeof(Enum))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Enum type is not supported by DataContractJsonSerializer. The underlying type is '{0}'.", new object[]
					{
						declaredContract.UnderlyingType
					})));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00050ABC File Offset: 0x0004ECBC
		internal void WriteJsonISerializable(XmlWriterDelegator xmlWriter, ISerializable obj)
		{
			Type type = obj.GetType();
			SerializationInfo serializationInfo = new SerializationInfo(type, XmlObjectSerializer.FormatterConverter);
			base.GetObjectData(obj, serializationInfo, base.GetStreamingContext());
			if (DataContract.GetClrTypeFullName(type) != serializationInfo.FullTypeName)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Changing full type name is not supported. Serialization type name: '{0}', data contract type name: '{1}'.", new object[]
				{
					serializationInfo.FullTypeName,
					DataContract.GetClrTypeFullName(type)
				})));
			}
			base.WriteSerializationInfo(xmlWriter, type, serializationInfo);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00050B33 File Offset: 0x0004ED33
		internal static DataContract GetRevisedItemContract(DataContract oldItemContract)
		{
			if (oldItemContract != null && oldItemContract.UnderlyingType.IsGenericType && oldItemContract.UnderlyingType.GetGenericTypeDefinition() == Globals.TypeOfKeyValue)
			{
				return DataContract.GetDataContract(oldItemContract.UnderlyingType);
			}
			return oldItemContract;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00050B6C File Offset: 0x0004ED6C
		protected override void WriteDataContractValue(DataContract dataContract, XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle declaredTypeHandle)
		{
			JsonDataContract jsonDataContract = JsonDataContract.GetJsonDataContract(dataContract);
			if (this.emitXsiType == EmitTypeInformation.Always && !this.perCallXsiTypeAlreadyEmitted && XmlObjectSerializerWriteContextComplexJson.RequiresJsonTypeInfo(dataContract))
			{
				this.WriteTypeInfo(xmlWriter, jsonDataContract.TypeName);
			}
			this.perCallXsiTypeAlreadyEmitted = false;
			DataContractJsonSerializer.WriteJsonValue(jsonDataContract, xmlWriter, obj, this, declaredTypeHandle);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00050BB8 File Offset: 0x0004EDB8
		protected override void WriteNull(XmlWriterDelegator xmlWriter)
		{
			DataContractJsonSerializer.WriteJsonNull(xmlWriter);
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00050BC0 File Offset: 0x0004EDC0
		internal XmlDictionaryString CollectionItemName
		{
			get
			{
				return JsonGlobals.itemDictionaryString;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00050BC7 File Offset: 0x0004EDC7
		internal static void WriteJsonNameWithMapping(XmlWriterDelegator xmlWriter, XmlDictionaryString[] memberNames, int index)
		{
			xmlWriter.WriteStartElement("a", "item", "item");
			xmlWriter.WriteAttributeString(null, "item", null, memberNames[index].Value);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00050BF4 File Offset: 0x0004EDF4
		internal override void WriteExtensionDataTypeInfo(XmlWriterDelegator xmlWriter, IDataNode dataNode)
		{
			Type dataType = dataNode.DataType;
			if (dataType == Globals.TypeOfClassDataNode || dataType == Globals.TypeOfISerializableDataNode)
			{
				xmlWriter.WriteAttributeString(null, "type", null, "object");
				base.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
				return;
			}
			if (dataType == Globals.TypeOfCollectionDataNode)
			{
				xmlWriter.WriteAttributeString(null, "type", null, "array");
				return;
			}
			if (!(dataType == Globals.TypeOfXmlDataNode) && dataType == Globals.TypeOfObject && dataNode.Value != null && XmlObjectSerializerWriteContextComplexJson.RequiresJsonTypeInfo(base.GetDataContract(dataNode.Value.GetType())))
			{
				base.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00050CA0 File Offset: 0x0004EEA0
		protected override void SerializeWithXsiType(XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle objectTypeHandle, Type objectType, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle, Type declaredType)
		{
			bool verifyKnownType = false;
			bool isInterface = declaredType.IsInterface;
			DataContract dataContract;
			if (isInterface && CollectionDataContract.IsCollectionInterface(declaredType))
			{
				dataContract = this.GetDataContract(declaredTypeHandle, declaredType);
			}
			else if (declaredType.IsArray)
			{
				dataContract = this.GetDataContract(declaredTypeHandle, declaredType);
			}
			else
			{
				dataContract = this.GetDataContract(objectTypeHandle, objectType);
				DataContract declaredContract = (declaredTypeID >= 0) ? this.GetDataContract(declaredTypeID, declaredTypeHandle) : this.GetDataContract(declaredTypeHandle, declaredType);
				verifyKnownType = this.WriteTypeInfo(xmlWriter, dataContract, declaredContract);
				this.HandleCollectionAssignedToObject(declaredType, ref dataContract, ref obj, ref verifyKnownType);
			}
			if (isInterface)
			{
				XmlObjectSerializerWriteContextComplexJson.VerifyObjectCompatibilityWithInterface(dataContract, obj, declaredType);
			}
			base.SerializeAndVerifyType(dataContract, xmlWriter, obj, verifyKnownType, declaredType.TypeHandle, declaredType);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00050D44 File Offset: 0x0004EF44
		private static void VerifyObjectCompatibilityWithInterface(DataContract contract, object graph, Type declaredType)
		{
			Type type = contract.GetType();
			if (type == typeof(XmlDataContract) && !Globals.TypeOfIXmlSerializable.IsAssignableFrom(declaredType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Object of type '{0}' is assigned to an incompatible interface '{1}'.", new object[]
				{
					graph.GetType(),
					declaredType
				})));
			}
			if (type == typeof(CollectionDataContract) && !CollectionDataContract.IsCollectionInterface(declaredType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Collection of type '{0}' is assigned to an incompatible interface '{1}'", new object[]
				{
					graph.GetType(),
					declaredType
				})));
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00050DE4 File Offset: 0x0004EFE4
		private void HandleCollectionAssignedToObject(Type declaredType, ref DataContract dataContract, ref object obj, ref bool verifyKnownType)
		{
			if (declaredType != dataContract.UnderlyingType && dataContract is CollectionDataContract)
			{
				if (verifyKnownType)
				{
					this.VerifyType(dataContract, declaredType);
					verifyKnownType = false;
				}
				if (((CollectionDataContract)dataContract).Kind == CollectionKind.Dictionary)
				{
					IDictionary dictionary = obj as IDictionary;
					Dictionary<object, object> dictionary2 = new Dictionary<object, object>();
					foreach (object obj2 in dictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						dictionary2.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					}
					obj = dictionary2;
				}
				dataContract = base.GetDataContract(Globals.TypeOfIEnumerable);
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00050EA0 File Offset: 0x0004F0A0
		internal override void SerializeWithXsiTypeAtTopLevel(DataContract dataContract, XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle originalDeclaredTypeHandle, Type graphType)
		{
			bool verifyKnownType = false;
			Type underlyingType = this.rootTypeDataContract.UnderlyingType;
			bool isInterface = underlyingType.IsInterface;
			if ((!isInterface || !CollectionDataContract.IsCollectionInterface(underlyingType)) && !underlyingType.IsArray)
			{
				verifyKnownType = this.WriteTypeInfo(xmlWriter, dataContract, this.rootTypeDataContract);
				this.HandleCollectionAssignedToObject(underlyingType, ref dataContract, ref obj, ref verifyKnownType);
			}
			if (isInterface)
			{
				XmlObjectSerializerWriteContextComplexJson.VerifyObjectCompatibilityWithInterface(dataContract, obj, underlyingType);
			}
			base.SerializeAndVerifyType(dataContract, xmlWriter, obj, verifyKnownType, underlyingType.TypeHandle, underlyingType);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00050F0C File Offset: 0x0004F10C
		private void VerifyType(DataContract dataContract, Type declaredType)
		{
			bool flag = false;
			if (dataContract.KnownDataContracts != null)
			{
				this.scopedKnownTypes.Push(dataContract.KnownDataContracts);
				flag = true;
			}
			if (!base.IsKnownType(dataContract, declaredType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Type '{0}' with data contract name '{1}:{2}' is not expected. Add any types not known statically to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding them to the list of known types passed to DataContractSerializer.", new object[]
				{
					DataContract.GetClrTypeFullName(dataContract.UnderlyingType),
					dataContract.StableName.Name,
					dataContract.StableName.Namespace
				})));
			}
			if (flag)
			{
				this.scopedKnownTypes.Pop();
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00050F93 File Offset: 0x0004F193
		internal override DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContract = base.GetDataContract(typeHandle, type);
			DataContractJsonSerializer.CheckIfTypeIsReference(dataContract);
			return dataContract;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00050FA3 File Offset: 0x0004F1A3
		internal override DataContract GetDataContractSkipValidation(int typeId, RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContractSkipValidation = base.GetDataContractSkipValidation(typeId, typeHandle, type);
			DataContractJsonSerializer.CheckIfTypeIsReference(dataContractSkipValidation);
			return dataContractSkipValidation;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00050FB4 File Offset: 0x0004F1B4
		internal override DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
		{
			DataContract dataContract = base.GetDataContract(id, typeHandle);
			DataContractJsonSerializer.CheckIfTypeIsReference(dataContract);
			return dataContract;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		internal static DataContract ResolveJsonDataContractFromRootDataContract(XmlObjectSerializerContext context, XmlQualifiedName typeQName, DataContract rootTypeDataContract)
		{
			if (rootTypeDataContract.StableName == typeQName)
			{
				return rootTypeDataContract;
			}
			DataContract dataContract;
			for (CollectionDataContract collectionDataContract = rootTypeDataContract as CollectionDataContract; collectionDataContract != null; collectionDataContract = (dataContract as CollectionDataContract))
			{
				if (collectionDataContract.ItemType.IsGenericType && collectionDataContract.ItemType.GetGenericTypeDefinition() == typeof(KeyValue<, >))
				{
					dataContract = context.GetDataContract(Globals.TypeOfKeyValuePair.MakeGenericType(collectionDataContract.ItemType.GetGenericArguments()));
				}
				else
				{
					dataContract = context.GetDataContract(context.GetSurrogatedType(collectionDataContract.ItemType));
				}
				if (dataContract.StableName == typeQName)
				{
					return dataContract;
				}
			}
			return null;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00050884 File Offset: 0x0004EA84
		protected override DataContract ResolveDataContractFromRootDataContract(XmlQualifiedName typeQName)
		{
			return XmlObjectSerializerWriteContextComplexJson.ResolveJsonDataContractFromRootDataContract(this, typeQName, this.rootTypeDataContract);
		}

		// Token: 0x04000A40 RID: 2624
		private EmitTypeInformation emitXsiType;

		// Token: 0x04000A41 RID: 2625
		private bool perCallXsiTypeAlreadyEmitted;

		// Token: 0x04000A42 RID: 2626
		private bool useSimpleDictionaryFormat;
	}
}
