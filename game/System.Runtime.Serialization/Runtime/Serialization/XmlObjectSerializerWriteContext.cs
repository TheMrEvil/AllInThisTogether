using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Diagnostics;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x0200014C RID: 332
	internal class XmlObjectSerializerWriteContext : XmlObjectSerializerContext
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00041CAA File Offset: 0x0003FEAA
		internal static XmlObjectSerializerWriteContext CreateContext(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver dataContractResolver)
		{
			if (!serializer.PreserveObjectReferences && serializer.DataContractSurrogate == null)
			{
				return new XmlObjectSerializerWriteContext(serializer, rootTypeDataContract, dataContractResolver);
			}
			return new XmlObjectSerializerWriteContextComplex(serializer, rootTypeDataContract, dataContractResolver);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00041CCD File Offset: 0x0003FECD
		internal static XmlObjectSerializerWriteContext CreateContext(NetDataContractSerializer serializer, Hashtable surrogateDataContracts)
		{
			return new XmlObjectSerializerWriteContextComplex(serializer, surrogateDataContracts);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00041CD6 File Offset: 0x0003FED6
		protected XmlObjectSerializerWriteContext(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver resolver) : base(serializer, rootTypeDataContract, resolver)
		{
			this.serializeReadOnlyTypes = serializer.SerializeReadOnlyTypes;
			this.unsafeTypeForwardingEnabled = true;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00041CF4 File Offset: 0x0003FEF4
		protected XmlObjectSerializerWriteContext(NetDataContractSerializer serializer) : base(serializer)
		{
			this.unsafeTypeForwardingEnabled = NetDataContractSerializer.UnsafeTypeForwardingEnabled;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00041D08 File Offset: 0x0003FF08
		internal XmlObjectSerializerWriteContext(XmlObjectSerializer serializer, int maxItemsInObjectGraph, StreamingContext streamingContext, bool ignoreExtensionDataObject) : base(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject)
		{
			this.unsafeTypeForwardingEnabled = true;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00041D1C File Offset: 0x0003FF1C
		protected ObjectToIdCache SerializedObjects
		{
			get
			{
				if (this.serializedObjects == null)
				{
					this.serializedObjects = new ObjectToIdCache();
				}
				return this.serializedObjects;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00041D37 File Offset: 0x0003FF37
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00041D3F File Offset: 0x0003FF3F
		internal override bool IsGetOnlyCollection
		{
			get
			{
				return this.isGetOnlyCollection;
			}
			set
			{
				this.isGetOnlyCollection = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00041D48 File Offset: 0x0003FF48
		internal bool SerializeReadOnlyTypes
		{
			get
			{
				return this.serializeReadOnlyTypes;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00041D50 File Offset: 0x0003FF50
		internal bool UnsafeTypeForwardingEnabled
		{
			get
			{
				return this.unsafeTypeForwardingEnabled;
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00041D58 File Offset: 0x0003FF58
		internal void StoreIsGetOnlyCollection()
		{
			this.isGetOnlyCollection = true;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00041D61 File Offset: 0x0003FF61
		public void InternalSerializeReference(XmlWriterDelegator xmlWriter, object obj, bool isDeclaredType, bool writeXsiType, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle)
		{
			if (!this.OnHandleReference(xmlWriter, obj, true))
			{
				this.InternalSerialize(xmlWriter, obj, isDeclaredType, writeXsiType, declaredTypeID, declaredTypeHandle);
			}
			this.OnEndHandleReference(xmlWriter, obj, true);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00041D88 File Offset: 0x0003FF88
		public virtual void InternalSerialize(XmlWriterDelegator xmlWriter, object obj, bool isDeclaredType, bool writeXsiType, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle)
		{
			if (writeXsiType)
			{
				Type typeOfObject = Globals.TypeOfObject;
				this.SerializeWithXsiType(xmlWriter, obj, Type.GetTypeHandle(obj), null, -1, typeOfObject.TypeHandle, typeOfObject);
				return;
			}
			if (isDeclaredType)
			{
				DataContract dataContract = this.GetDataContract(declaredTypeID, declaredTypeHandle);
				this.SerializeWithoutXsiType(dataContract, xmlWriter, obj, declaredTypeHandle);
				return;
			}
			RuntimeTypeHandle typeHandle = Type.GetTypeHandle(obj);
			if (declaredTypeHandle.Equals(typeHandle))
			{
				DataContract dataContract2 = (declaredTypeID >= 0) ? this.GetDataContract(declaredTypeID, declaredTypeHandle) : this.GetDataContract(declaredTypeHandle, null);
				this.SerializeWithoutXsiType(dataContract2, xmlWriter, obj, declaredTypeHandle);
				return;
			}
			this.SerializeWithXsiType(xmlWriter, obj, typeHandle, null, declaredTypeID, declaredTypeHandle, Type.GetTypeFromHandle(declaredTypeHandle));
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00041E20 File Offset: 0x00040020
		internal void SerializeWithoutXsiType(DataContract dataContract, XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle declaredTypeHandle)
		{
			if (this.OnHandleIsReference(xmlWriter, dataContract, obj))
			{
				return;
			}
			if (dataContract.KnownDataContracts != null)
			{
				this.scopedKnownTypes.Push(dataContract.KnownDataContracts);
				this.WriteDataContractValue(dataContract, xmlWriter, obj, declaredTypeHandle);
				this.scopedKnownTypes.Pop();
				return;
			}
			this.WriteDataContractValue(dataContract, xmlWriter, obj, declaredTypeHandle);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00041E74 File Offset: 0x00040074
		internal virtual void SerializeWithXsiTypeAtTopLevel(DataContract dataContract, XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle originalDeclaredTypeHandle, Type graphType)
		{
			bool verifyKnownType = false;
			Type originalUnderlyingType = this.rootTypeDataContract.OriginalUnderlyingType;
			if (originalUnderlyingType.IsInterface && CollectionDataContract.IsCollectionInterface(originalUnderlyingType))
			{
				if (base.DataContractResolver != null)
				{
					this.WriteResolvedTypeInfo(xmlWriter, graphType, originalUnderlyingType);
				}
			}
			else if (!originalUnderlyingType.IsArray)
			{
				verifyKnownType = this.WriteTypeInfo(xmlWriter, dataContract, this.rootTypeDataContract);
			}
			this.SerializeAndVerifyType(dataContract, xmlWriter, obj, verifyKnownType, originalDeclaredTypeHandle, originalUnderlyingType);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00041ED8 File Offset: 0x000400D8
		protected virtual void SerializeWithXsiType(XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle objectTypeHandle, Type objectType, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle, Type declaredType)
		{
			bool verifyKnownType = false;
			DataContract dataContract;
			if (declaredType.IsInterface && CollectionDataContract.IsCollectionInterface(declaredType))
			{
				dataContract = this.GetDataContractSkipValidation(DataContract.GetId(objectTypeHandle), objectTypeHandle, objectType);
				if (this.OnHandleIsReference(xmlWriter, dataContract, obj))
				{
					return;
				}
				if (this.Mode == SerializationMode.SharedType && dataContract.IsValidContract(this.Mode))
				{
					dataContract = dataContract.GetValidContract(this.Mode);
				}
				else
				{
					dataContract = this.GetDataContract(declaredTypeHandle, declaredType);
				}
				if (!this.WriteClrTypeInfo(xmlWriter, dataContract) && base.DataContractResolver != null)
				{
					if (objectType == null)
					{
						objectType = Type.GetTypeFromHandle(objectTypeHandle);
					}
					this.WriteResolvedTypeInfo(xmlWriter, objectType, declaredType);
				}
			}
			else if (declaredType.IsArray)
			{
				dataContract = this.GetDataContract(objectTypeHandle, objectType);
				this.WriteClrTypeInfo(xmlWriter, dataContract);
				dataContract = this.GetDataContract(declaredTypeHandle, declaredType);
			}
			else
			{
				dataContract = this.GetDataContract(objectTypeHandle, objectType);
				if (this.OnHandleIsReference(xmlWriter, dataContract, obj))
				{
					return;
				}
				if (!this.WriteClrTypeInfo(xmlWriter, dataContract))
				{
					DataContract declaredContract = (declaredTypeID >= 0) ? this.GetDataContract(declaredTypeID, declaredTypeHandle) : this.GetDataContract(declaredTypeHandle, declaredType);
					verifyKnownType = this.WriteTypeInfo(xmlWriter, dataContract, declaredContract);
				}
			}
			this.SerializeAndVerifyType(dataContract, xmlWriter, obj, verifyKnownType, declaredTypeHandle, declaredType);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00042000 File Offset: 0x00040200
		internal bool OnHandleIsReference(XmlWriterDelegator xmlWriter, DataContract contract, object obj)
		{
			if (this.preserveObjectReferences || !contract.IsReference || this.isGetOnlyCollection)
			{
				return false;
			}
			bool flag = true;
			int id = this.SerializedObjects.GetId(obj, ref flag);
			this.byValObjectsInScope.EnsureSetAsIsReference(obj);
			if (flag)
			{
				xmlWriter.WriteAttributeString("z", DictionaryGlobals.IdLocalName, DictionaryGlobals.SerializationNamespace, string.Format(CultureInfo.InvariantCulture, "{0}{1}", "i", id));
				return false;
			}
			xmlWriter.WriteAttributeString("z", DictionaryGlobals.RefLocalName, DictionaryGlobals.SerializationNamespace, string.Format(CultureInfo.InvariantCulture, "{0}{1}", "i", id));
			return true;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000420A8 File Offset: 0x000402A8
		protected void SerializeAndVerifyType(DataContract dataContract, XmlWriterDelegator xmlWriter, object obj, bool verifyKnownType, RuntimeTypeHandle declaredTypeHandle, Type declaredType)
		{
			bool flag = false;
			if (dataContract.KnownDataContracts != null)
			{
				this.scopedKnownTypes.Push(dataContract.KnownDataContracts);
				flag = true;
			}
			if (verifyKnownType && !base.IsKnownType(dataContract, declaredType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Type '{0}' with data contract name '{1}:{2}' is not expected. Add any types not known statically to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding them to the list of known types passed to DataContractSerializer.", new object[]
				{
					DataContract.GetClrTypeFullName(dataContract.UnderlyingType),
					dataContract.StableName.Name,
					dataContract.StableName.Namespace
				})));
			}
			this.WriteDataContractValue(dataContract, xmlWriter, obj, declaredTypeHandle);
			if (flag)
			{
				this.scopedKnownTypes.Pop();
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00003127 File Offset: 0x00001327
		internal virtual bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, DataContract dataContract)
		{
			return false;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00003127 File Offset: 0x00001327
		internal virtual bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, Type dataContractType, string clrTypeName, string clrAssemblyName)
		{
			return false;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00003127 File Offset: 0x00001327
		internal virtual bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, Type dataContractType, SerializationInfo serInfo)
		{
			return false;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00036D9D File Offset: 0x00034F9D
		public virtual void WriteAnyType(XmlWriterDelegator xmlWriter, object value)
		{
			xmlWriter.WriteAnyType(value);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0004213F File Offset: 0x0004033F
		public virtual void WriteString(XmlWriterDelegator xmlWriter, string value)
		{
			xmlWriter.WriteString(value);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00042148 File Offset: 0x00040348
		public virtual void WriteString(XmlWriterDelegator xmlWriter, string value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				this.WriteNull(xmlWriter, typeof(string), true, name, ns);
				return;
			}
			xmlWriter.WriteStartElementPrimitive(name, ns);
			xmlWriter.WriteString(value);
			xmlWriter.WriteEndElementPrimitive();
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00042179 File Offset: 0x00040379
		public virtual void WriteBase64(XmlWriterDelegator xmlWriter, byte[] value)
		{
			xmlWriter.WriteBase64(value);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00042182 File Offset: 0x00040382
		public virtual void WriteBase64(XmlWriterDelegator xmlWriter, byte[] value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				this.WriteNull(xmlWriter, typeof(byte[]), true, name, ns);
				return;
			}
			xmlWriter.WriteStartElementPrimitive(name, ns);
			xmlWriter.WriteBase64(value);
			xmlWriter.WriteEndElementPrimitive();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000421B3 File Offset: 0x000403B3
		public virtual void WriteUri(XmlWriterDelegator xmlWriter, Uri value)
		{
			xmlWriter.WriteUri(value);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000421BC File Offset: 0x000403BC
		public virtual void WriteUri(XmlWriterDelegator xmlWriter, Uri value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				this.WriteNull(xmlWriter, typeof(Uri), true, name, ns);
				return;
			}
			xmlWriter.WriteStartElementPrimitive(name, ns);
			xmlWriter.WriteUri(value);
			xmlWriter.WriteEndElementPrimitive();
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000421F3 File Offset: 0x000403F3
		public virtual void WriteQName(XmlWriterDelegator xmlWriter, XmlQualifiedName value)
		{
			xmlWriter.WriteQName(value);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000421FC File Offset: 0x000403FC
		public virtual void WriteQName(XmlWriterDelegator xmlWriter, XmlQualifiedName value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				this.WriteNull(xmlWriter, typeof(XmlQualifiedName), true, name, ns);
				return;
			}
			if (ns != null && ns.Value != null && ns.Value.Length > 0)
			{
				xmlWriter.WriteStartElement("q", name, ns);
			}
			else
			{
				xmlWriter.WriteStartElement(name, ns);
			}
			xmlWriter.WriteQName(value);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0004226A File Offset: 0x0004046A
		internal void HandleGraphAtTopLevel(XmlWriterDelegator writer, object obj, DataContract contract)
		{
			writer.WriteXmlnsAttribute("i", DictionaryGlobals.SchemaInstanceNamespace);
			if (contract.IsISerializable)
			{
				writer.WriteXmlnsAttribute("x", DictionaryGlobals.SchemaNamespace);
			}
			this.OnHandleReference(writer, obj, true);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000422A0 File Offset: 0x000404A0
		internal virtual bool OnHandleReference(XmlWriterDelegator xmlWriter, object obj, bool canContainCyclicReference)
		{
			if (xmlWriter.depth < 512)
			{
				return false;
			}
			if (canContainCyclicReference)
			{
				if (this.byValObjectsInScope.Count == 0 && DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.Trace(TraceEventType.Warning, 196626, SR.GetString("Object with large depth"));
				}
				if (this.byValObjectsInScope.Contains(obj))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Object graph for type '{0}' contains cycles and cannot be serialized if references are not tracked. Consider using the DataContractAttribute with the IsReference property set to true.", new object[]
					{
						DataContract.GetClrTypeFullName(obj.GetType())
					})));
				}
				this.byValObjectsInScope.Push(obj);
			}
			return false;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004232C File Offset: 0x0004052C
		internal virtual void OnEndHandleReference(XmlWriterDelegator xmlWriter, object obj, bool canContainCyclicReference)
		{
			if (xmlWriter.depth < 512)
			{
				return;
			}
			if (canContainCyclicReference)
			{
				this.byValObjectsInScope.Pop(obj);
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0004234B File Offset: 0x0004054B
		public void WriteNull(XmlWriterDelegator xmlWriter, Type memberType, bool isMemberTypeSerializable)
		{
			this.CheckIfTypeSerializable(memberType, isMemberTypeSerializable);
			this.WriteNull(xmlWriter);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0004235C File Offset: 0x0004055C
		internal void WriteNull(XmlWriterDelegator xmlWriter, Type memberType, bool isMemberTypeSerializable, XmlDictionaryString name, XmlDictionaryString ns)
		{
			xmlWriter.WriteStartElement(name, ns);
			this.WriteNull(xmlWriter, memberType, isMemberTypeSerializable);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00042377 File Offset: 0x00040577
		public void IncrementArrayCount(XmlWriterDelegator xmlWriter, Array array)
		{
			this.IncrementCollectionCount(xmlWriter, array.GetLength(0));
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00042387 File Offset: 0x00040587
		public void IncrementCollectionCount(XmlWriterDelegator xmlWriter, ICollection collection)
		{
			this.IncrementCollectionCount(xmlWriter, collection.Count);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00042396 File Offset: 0x00040596
		public void IncrementCollectionCountGeneric<T>(XmlWriterDelegator xmlWriter, ICollection<T> collection)
		{
			this.IncrementCollectionCount(xmlWriter, collection.Count);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000423A5 File Offset: 0x000405A5
		private void IncrementCollectionCount(XmlWriterDelegator xmlWriter, int size)
		{
			base.IncrementItemCount(size);
			this.WriteArraySize(xmlWriter, size);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal virtual void WriteArraySize(XmlWriterDelegator xmlWriter, int size)
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000423B8 File Offset: 0x000405B8
		public static T GetDefaultValue<T>()
		{
			return default(T);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x000423CE File Offset: 0x000405CE
		public static T GetNullableValue<T>(T? value) where T : struct
		{
			return value.Value;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000423D7 File Offset: 0x000405D7
		public static void ThrowRequiredMemberMustBeEmitted(string memberName, Type type)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Member {0} in type {1} cannot be serialized. This exception is usually caused by trying to use a null value where a null value is not allowed. The '{0}' member is set to its default value (usually null or zero). The member's EmitDefault setting is 'false', indicating that the member should not be serialized. However, the member's IsRequired setting is 'true', indicating that it must be serialized. This conflict cannot be resolved.  Consider setting '{0}' to a non-default value. Alternatively, you can change the EmitDefaultValue property on the DataMemberAttribute attribute to true, or changing the IsRequired property to false.", new object[]
			{
				memberName,
				type.FullName
			})));
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00042400 File Offset: 0x00040600
		public static bool GetHasValue<T>(T? value) where T : struct
		{
			return value != null;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00042409 File Offset: 0x00040609
		internal void WriteIXmlSerializable(XmlWriterDelegator xmlWriter, object obj)
		{
			if (this.xmlSerializableWriter == null)
			{
				this.xmlSerializableWriter = new XmlSerializableWriter();
			}
			XmlObjectSerializerWriteContext.WriteIXmlSerializable(xmlWriter, obj, this.xmlSerializableWriter);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004242B File Offset: 0x0004062B
		internal static void WriteRootIXmlSerializable(XmlWriterDelegator xmlWriter, object obj)
		{
			XmlObjectSerializerWriteContext.WriteIXmlSerializable(xmlWriter, obj, new XmlSerializableWriter());
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004243C File Offset: 0x0004063C
		private static void WriteIXmlSerializable(XmlWriterDelegator xmlWriter, object obj, XmlSerializableWriter xmlSerializableWriter)
		{
			xmlSerializableWriter.BeginWrite(xmlWriter.Writer, obj);
			IXmlSerializable xmlSerializable = obj as IXmlSerializable;
			if (xmlSerializable != null)
			{
				xmlSerializable.WriteXml(xmlSerializableWriter);
			}
			else
			{
				XmlElement xmlElement = obj as XmlElement;
				if (xmlElement != null)
				{
					xmlElement.WriteTo(xmlSerializableWriter);
				}
				else
				{
					XmlNode[] array = obj as XmlNode[];
					if (array == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unknown XML type: '{0}'.", new object[]
						{
							DataContract.GetClrTypeFullName(obj.GetType())
						})));
					}
					XmlNode[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].WriteTo(xmlSerializableWriter);
					}
				}
			}
			xmlSerializableWriter.EndWrite();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000424D5 File Offset: 0x000406D5
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void GetObjectData(ISerializable obj, SerializationInfo serInfo, StreamingContext context)
		{
			obj.GetObjectData(serInfo, context);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000424E0 File Offset: 0x000406E0
		public void WriteISerializable(XmlWriterDelegator xmlWriter, ISerializable obj)
		{
			Type type = obj.GetType();
			SerializationInfo serializationInfo = new SerializationInfo(type, XmlObjectSerializer.FormatterConverter, !this.UnsafeTypeForwardingEnabled);
			this.GetObjectData(obj, serializationInfo, base.GetStreamingContext());
			if (!this.UnsafeTypeForwardingEnabled && serializationInfo.AssemblyName == "0")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("ISerializable AssemblyName is set to \"0\" for type '{0}'.", new object[]
				{
					DataContract.GetClrTypeFullName(obj.GetType())
				})));
			}
			this.WriteSerializationInfo(xmlWriter, type, serializationInfo);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00042564 File Offset: 0x00040764
		internal void WriteSerializationInfo(XmlWriterDelegator xmlWriter, Type objType, SerializationInfo serInfo)
		{
			if (DataContract.GetClrTypeFullName(objType) != serInfo.FullTypeName)
			{
				if (base.DataContractResolver != null)
				{
					XmlDictionaryString name;
					XmlDictionaryString ns;
					if (this.ResolveType(serInfo.ObjectType, objType, out name, out ns))
					{
						xmlWriter.WriteAttributeQualifiedName("z", DictionaryGlobals.ISerializableFactoryTypeLocalName, DictionaryGlobals.SerializationNamespace, name, ns);
					}
				}
				else
				{
					string key;
					string key2;
					DataContract.GetDefaultStableName(serInfo.FullTypeName, out key, out key2);
					xmlWriter.WriteAttributeQualifiedName("z", DictionaryGlobals.ISerializableFactoryTypeLocalName, DictionaryGlobals.SerializationNamespace, DataContract.GetClrTypeString(key), DataContract.GetClrTypeString(key2));
				}
			}
			this.WriteClrTypeInfo(xmlWriter, objType, serInfo);
			base.IncrementItemCount(serInfo.MemberCount);
			foreach (SerializationEntry serializationEntry in serInfo)
			{
				XmlDictionaryString clrTypeString = DataContract.GetClrTypeString(DataContract.EncodeLocalName(serializationEntry.Name));
				xmlWriter.WriteStartElement(clrTypeString, DictionaryGlobals.EmptyString);
				object value = serializationEntry.Value;
				if (value == null)
				{
					this.WriteNull(xmlWriter);
				}
				else
				{
					this.InternalSerializeReference(xmlWriter, value, false, false, -1, Globals.TypeOfObject.TypeHandle);
				}
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004266C File Offset: 0x0004086C
		public void WriteExtensionData(XmlWriterDelegator xmlWriter, ExtensionDataObject extensionData, int memberIndex)
		{
			if (base.IgnoreExtensionDataObject || extensionData == null)
			{
				return;
			}
			if (extensionData.Members != null)
			{
				for (int i = 0; i < extensionData.Members.Count; i++)
				{
					ExtensionDataMember extensionDataMember = extensionData.Members[i];
					if (extensionDataMember.MemberIndex == memberIndex)
					{
						this.WriteExtensionDataMember(xmlWriter, extensionDataMember);
					}
				}
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000426C4 File Offset: 0x000408C4
		private void WriteExtensionDataMember(XmlWriterDelegator xmlWriter, ExtensionDataMember member)
		{
			xmlWriter.WriteStartElement(member.Name, member.Namespace);
			IDataNode value = member.Value;
			this.WriteExtensionDataValue(xmlWriter, value);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000426F8 File Offset: 0x000408F8
		internal virtual void WriteExtensionDataTypeInfo(XmlWriterDelegator xmlWriter, IDataNode dataNode)
		{
			if (dataNode.DataContractName != null)
			{
				this.WriteTypeInfo(xmlWriter, dataNode.DataContractName, dataNode.DataContractNamespace);
			}
			this.WriteClrTypeInfo(xmlWriter, dataNode.DataType, dataNode.ClrTypeName, dataNode.ClrAssemblyName);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00042730 File Offset: 0x00040930
		internal void WriteExtensionDataValue(XmlWriterDelegator xmlWriter, IDataNode dataNode)
		{
			base.IncrementItemCount(1);
			if (dataNode == null)
			{
				this.WriteNull(xmlWriter);
				return;
			}
			if (dataNode.PreservesReferences && this.OnHandleReference(xmlWriter, (dataNode.Value == null) ? dataNode : dataNode.Value, true))
			{
				return;
			}
			Type dataType = dataNode.DataType;
			if (dataType == Globals.TypeOfClassDataNode)
			{
				this.WriteExtensionClassData(xmlWriter, (ClassDataNode)dataNode);
			}
			else if (dataType == Globals.TypeOfCollectionDataNode)
			{
				this.WriteExtensionCollectionData(xmlWriter, (CollectionDataNode)dataNode);
			}
			else if (dataType == Globals.TypeOfXmlDataNode)
			{
				this.WriteExtensionXmlData(xmlWriter, (XmlDataNode)dataNode);
			}
			else if (dataType == Globals.TypeOfISerializableDataNode)
			{
				this.WriteExtensionISerializableData(xmlWriter, (ISerializableDataNode)dataNode);
			}
			else
			{
				this.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
				if (dataType == Globals.TypeOfObject)
				{
					object value = dataNode.Value;
					if (value != null)
					{
						this.InternalSerialize(xmlWriter, value, false, false, -1, value.GetType().TypeHandle);
					}
				}
				else
				{
					xmlWriter.WriteExtensionData(dataNode);
				}
			}
			if (dataNode.PreservesReferences)
			{
				this.OnEndHandleReference(xmlWriter, (dataNode.Value == null) ? dataNode : dataNode.Value, true);
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004284C File Offset: 0x00040A4C
		internal bool TryWriteDeserializedExtensionData(XmlWriterDelegator xmlWriter, IDataNode dataNode)
		{
			object value = dataNode.Value;
			if (value == null)
			{
				return false;
			}
			Type type = (dataNode.DataContractName == null) ? value.GetType() : Globals.TypeOfObject;
			this.InternalSerialize(xmlWriter, value, false, false, -1, type.TypeHandle);
			return true;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00042890 File Offset: 0x00040A90
		private void WriteExtensionClassData(XmlWriterDelegator xmlWriter, ClassDataNode dataNode)
		{
			if (!this.TryWriteDeserializedExtensionData(xmlWriter, dataNode))
			{
				this.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
				IList<ExtensionDataMember> members = dataNode.Members;
				if (members != null)
				{
					for (int i = 0; i < members.Count; i++)
					{
						this.WriteExtensionDataMember(xmlWriter, members[i]);
					}
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000428D8 File Offset: 0x00040AD8
		private void WriteExtensionCollectionData(XmlWriterDelegator xmlWriter, CollectionDataNode dataNode)
		{
			if (!this.TryWriteDeserializedExtensionData(xmlWriter, dataNode))
			{
				this.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
				this.WriteArraySize(xmlWriter, dataNode.Size);
				IList<IDataNode> items = dataNode.Items;
				if (items != null)
				{
					for (int i = 0; i < items.Count; i++)
					{
						xmlWriter.WriteStartElement(dataNode.ItemName, dataNode.ItemNamespace);
						this.WriteExtensionDataValue(xmlWriter, items[i]);
						xmlWriter.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00042948 File Offset: 0x00040B48
		private void WriteExtensionISerializableData(XmlWriterDelegator xmlWriter, ISerializableDataNode dataNode)
		{
			if (!this.TryWriteDeserializedExtensionData(xmlWriter, dataNode))
			{
				this.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
				if (dataNode.FactoryTypeName != null)
				{
					xmlWriter.WriteAttributeQualifiedName("z", DictionaryGlobals.ISerializableFactoryTypeLocalName, DictionaryGlobals.SerializationNamespace, dataNode.FactoryTypeName, dataNode.FactoryTypeNamespace);
				}
				IList<ISerializableDataMember> members = dataNode.Members;
				if (members != null)
				{
					for (int i = 0; i < members.Count; i++)
					{
						ISerializableDataMember serializableDataMember = members[i];
						xmlWriter.WriteStartElement(serializableDataMember.Name, string.Empty);
						this.WriteExtensionDataValue(xmlWriter, serializableDataMember.Value);
						xmlWriter.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000429D8 File Offset: 0x00040BD8
		private void WriteExtensionXmlData(XmlWriterDelegator xmlWriter, XmlDataNode dataNode)
		{
			if (!this.TryWriteDeserializedExtensionData(xmlWriter, dataNode))
			{
				IList<XmlAttribute> xmlAttributes = dataNode.XmlAttributes;
				if (xmlAttributes != null)
				{
					foreach (XmlAttribute xmlAttribute in xmlAttributes)
					{
						xmlAttribute.WriteTo(xmlWriter.Writer);
					}
				}
				this.WriteExtensionDataTypeInfo(xmlWriter, dataNode);
				IList<XmlNode> xmlChildNodes = dataNode.XmlChildNodes;
				if (xmlChildNodes != null)
				{
					foreach (XmlNode xmlNode in xmlChildNodes)
					{
						xmlNode.WriteTo(xmlWriter.Writer);
					}
				}
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00042A84 File Offset: 0x00040C84
		protected virtual void WriteDataContractValue(DataContract dataContract, XmlWriterDelegator xmlWriter, object obj, RuntimeTypeHandle declaredTypeHandle)
		{
			dataContract.WriteXmlValue(xmlWriter, obj, this);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00042A8F File Offset: 0x00040C8F
		protected virtual void WriteNull(XmlWriterDelegator xmlWriter)
		{
			XmlObjectSerializer.WriteNull(xmlWriter);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00042A98 File Offset: 0x00040C98
		private void WriteResolvedTypeInfo(XmlWriterDelegator writer, Type objectType, Type declaredType)
		{
			XmlDictionaryString dataContractName;
			XmlDictionaryString dataContractNamespace;
			if (this.ResolveType(objectType, declaredType, out dataContractName, out dataContractNamespace))
			{
				this.WriteTypeInfo(writer, dataContractName, dataContractNamespace);
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00042ABC File Offset: 0x00040CBC
		private bool ResolveType(Type objectType, Type declaredType, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			if (!base.DataContractResolver.TryResolveType(objectType, declaredType, base.KnownTypeResolver, out typeName, out typeNamespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An object of type '{0}' which derives from DataContractResolver returned false from its TryResolveType method when attempting to resolve the name for an object of type '{1}', indicating that the resolution failed. Change the TryResolveType implementation to return true.", new object[]
				{
					DataContract.GetClrTypeFullName(base.DataContractResolver.GetType()),
					DataContract.GetClrTypeFullName(objectType)
				})));
			}
			if (typeName == null)
			{
				if (typeNamespace == null)
				{
					return false;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An object of type '{0}' which derives from DataContractResolver returned a null typeName or typeNamespace but not both from its TryResolveType method when attempting to resolve the name for an object of type '{1}'. Change the TryResolveType implementation to return non-null values, or to return null values for both typeName and typeNamespace in order to serialize as the declared type.", new object[]
				{
					DataContract.GetClrTypeFullName(base.DataContractResolver.GetType()),
					DataContract.GetClrTypeFullName(objectType)
				})));
			}
			else
			{
				if (typeNamespace == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An object of type '{0}' which derives from DataContractResolver returned a null typeName or typeNamespace but not both from its TryResolveType method when attempting to resolve the name for an object of type '{1}'. Change the TryResolveType implementation to return non-null values, or to return null values for both typeName and typeNamespace in order to serialize as the declared type.", new object[]
					{
						DataContract.GetClrTypeFullName(base.DataContractResolver.GetType()),
						DataContract.GetClrTypeFullName(objectType)
					})));
				}
				return true;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00042B97 File Offset: 0x00040D97
		protected virtual bool WriteTypeInfo(XmlWriterDelegator writer, DataContract contract, DataContract declaredContract)
		{
			if (XmlObjectSerializer.IsContractDeclared(contract, declaredContract))
			{
				return false;
			}
			if (base.DataContractResolver == null)
			{
				this.WriteTypeInfo(writer, contract.Name, contract.Namespace);
				return true;
			}
			this.WriteResolvedTypeInfo(writer, contract.OriginalUnderlyingType, declaredContract.OriginalUnderlyingType);
			return false;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00042BD5 File Offset: 0x00040DD5
		protected virtual void WriteTypeInfo(XmlWriterDelegator writer, string dataContractName, string dataContractNamespace)
		{
			writer.WriteAttributeQualifiedName("i", DictionaryGlobals.XsiTypeLocalName, DictionaryGlobals.SchemaInstanceNamespace, dataContractName, dataContractNamespace);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00042BEE File Offset: 0x00040DEE
		protected virtual void WriteTypeInfo(XmlWriterDelegator writer, XmlDictionaryString dataContractName, XmlDictionaryString dataContractNamespace)
		{
			writer.WriteAttributeQualifiedName("i", DictionaryGlobals.XsiTypeLocalName, DictionaryGlobals.SchemaInstanceNamespace, dataContractName, dataContractNamespace);
		}

		// Token: 0x04000717 RID: 1815
		private ObjectReferenceStack byValObjectsInScope;

		// Token: 0x04000718 RID: 1816
		private XmlSerializableWriter xmlSerializableWriter;

		// Token: 0x04000719 RID: 1817
		private const int depthToCheckCyclicReference = 512;

		// Token: 0x0400071A RID: 1818
		protected bool preserveObjectReferences;

		// Token: 0x0400071B RID: 1819
		private ObjectToIdCache serializedObjects;

		// Token: 0x0400071C RID: 1820
		private bool isGetOnlyCollection;

		// Token: 0x0400071D RID: 1821
		private readonly bool unsafeTypeForwardingEnabled;

		// Token: 0x0400071E RID: 1822
		protected bool serializeReadOnlyTypes;
	}
}
