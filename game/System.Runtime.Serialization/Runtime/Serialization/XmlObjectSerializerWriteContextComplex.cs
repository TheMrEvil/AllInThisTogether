using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200014D RID: 333
	internal class XmlObjectSerializerWriteContextComplex : XmlObjectSerializerWriteContext
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x00042C07 File Offset: 0x00040E07
		internal XmlObjectSerializerWriteContextComplex(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver dataContractResolver) : base(serializer, rootTypeDataContract, dataContractResolver)
		{
			this.mode = SerializationMode.SharedContract;
			this.preserveObjectReferences = serializer.PreserveObjectReferences;
			this.dataContractSurrogate = serializer.DataContractSurrogate;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00042C34 File Offset: 0x00040E34
		internal XmlObjectSerializerWriteContextComplex(NetDataContractSerializer serializer, Hashtable surrogateDataContracts) : base(serializer)
		{
			this.mode = SerializationMode.SharedType;
			this.preserveObjectReferences = true;
			this.streamingContext = serializer.Context;
			this.binder = serializer.Binder;
			this.surrogateSelector = serializer.SurrogateSelector;
			this.surrogateDataContracts = surrogateDataContracts;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00042C81 File Offset: 0x00040E81
		internal XmlObjectSerializerWriteContextComplex(XmlObjectSerializer serializer, int maxItemsInObjectGraph, StreamingContext streamingContext, bool ignoreExtensionDataObject) : base(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject)
		{
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00042C8E File Offset: 0x00040E8E
		internal override SerializationMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00042C98 File Offset: 0x00040E98
		internal override DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContract = null;
			if (this.mode == SerializationMode.SharedType && this.surrogateSelector != null)
			{
				dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(this.surrogateSelector, this.streamingContext, typeHandle, type, ref this.surrogateDataContracts);
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

		// Token: 0x060010C4 RID: 4292 RVA: 0x00042D18 File Offset: 0x00040F18
		internal override DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
		{
			DataContract dataContract = null;
			if (this.mode == SerializationMode.SharedType && this.surrogateSelector != null)
			{
				dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(this.surrogateSelector, this.streamingContext, typeHandle, null, ref this.surrogateDataContracts);
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

		// Token: 0x060010C5 RID: 4293 RVA: 0x00042D98 File Offset: 0x00040F98
		internal override DataContract GetDataContractSkipValidation(int typeId, RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContract = null;
			if (this.mode == SerializationMode.SharedType && this.surrogateSelector != null)
			{
				dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(this.surrogateSelector, this.streamingContext, typeHandle, null, ref this.surrogateDataContracts);
			}
			if (dataContract == null)
			{
				return base.GetDataContractSkipValidation(typeId, typeHandle, type);
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

		// Token: 0x060010C6 RID: 4294 RVA: 0x00042E19 File Offset: 0x00041019
		internal override bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, DataContract dataContract)
		{
			if (this.mode == SerializationMode.SharedType)
			{
				NetDataContractSerializer.WriteClrTypeInfo(xmlWriter, dataContract, this.binder);
				return true;
			}
			return false;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00042E34 File Offset: 0x00041034
		internal override bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, Type dataContractType, string clrTypeName, string clrAssemblyName)
		{
			if (this.mode == SerializationMode.SharedType)
			{
				NetDataContractSerializer.WriteClrTypeInfo(xmlWriter, dataContractType, this.binder, clrTypeName, clrAssemblyName);
				return true;
			}
			return false;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00042E52 File Offset: 0x00041052
		internal override bool WriteClrTypeInfo(XmlWriterDelegator xmlWriter, Type dataContractType, SerializationInfo serInfo)
		{
			if (this.mode == SerializationMode.SharedType)
			{
				NetDataContractSerializer.WriteClrTypeInfo(xmlWriter, dataContractType, this.binder, serInfo);
				return true;
			}
			return false;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00042E6E File Offset: 0x0004106E
		public override void WriteAnyType(XmlWriterDelegator xmlWriter, object value)
		{
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteAnyType(value);
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00042E82 File Offset: 0x00041082
		public override void WriteString(XmlWriterDelegator xmlWriter, string value)
		{
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteString(value);
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00042E96 File Offset: 0x00041096
		public override void WriteString(XmlWriterDelegator xmlWriter, string value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				base.WriteNull(xmlWriter, typeof(string), true, name, ns);
				return;
			}
			xmlWriter.WriteStartElementPrimitive(name, ns);
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElementPrimitive();
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00042ED2 File Offset: 0x000410D2
		public override void WriteBase64(XmlWriterDelegator xmlWriter, byte[] value)
		{
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteBase64(value);
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00042EE6 File Offset: 0x000410E6
		public override void WriteBase64(XmlWriterDelegator xmlWriter, byte[] value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				base.WriteNull(xmlWriter, typeof(byte[]), true, name, ns);
				return;
			}
			xmlWriter.WriteStartElementPrimitive(name, ns);
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteBase64(value);
			}
			xmlWriter.WriteEndElementPrimitive();
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00042F22 File Offset: 0x00041122
		public override void WriteUri(XmlWriterDelegator xmlWriter, Uri value)
		{
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteUri(value);
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00042F38 File Offset: 0x00041138
		public override void WriteUri(XmlWriterDelegator xmlWriter, Uri value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				base.WriteNull(xmlWriter, typeof(Uri), true, name, ns);
				return;
			}
			xmlWriter.WriteStartElementPrimitive(name, ns);
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteUri(value);
			}
			xmlWriter.WriteEndElementPrimitive();
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00042F85 File Offset: 0x00041185
		public override void WriteQName(XmlWriterDelegator xmlWriter, XmlQualifiedName value)
		{
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteQName(value);
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00042F9C File Offset: 0x0004119C
		public override void WriteQName(XmlWriterDelegator xmlWriter, XmlQualifiedName value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (value == null)
			{
				base.WriteNull(xmlWriter, typeof(XmlQualifiedName), true, name, ns);
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
			if (!this.OnHandleReference(xmlWriter, value, false))
			{
				xmlWriter.WriteQName(value);
			}
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00043015 File Offset: 0x00041215
		public override void InternalSerialize(XmlWriterDelegator xmlWriter, object obj, bool isDeclaredType, bool writeXsiType, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle)
		{
			if (this.dataContractSurrogate == null)
			{
				base.InternalSerialize(xmlWriter, obj, isDeclaredType, writeXsiType, declaredTypeID, declaredTypeHandle);
				return;
			}
			this.InternalSerializeWithSurrogate(xmlWriter, obj, isDeclaredType, writeXsiType, declaredTypeID, declaredTypeHandle);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00043040 File Offset: 0x00041240
		internal override bool OnHandleReference(XmlWriterDelegator xmlWriter, object obj, bool canContainCyclicReference)
		{
			if (this.preserveObjectReferences && !this.IsGetOnlyCollection)
			{
				bool flag = true;
				int id = base.SerializedObjects.GetId(obj, ref flag);
				if (flag)
				{
					xmlWriter.WriteAttributeInt("z", DictionaryGlobals.IdLocalName, DictionaryGlobals.SerializationNamespace, id);
				}
				else
				{
					xmlWriter.WriteAttributeInt("z", DictionaryGlobals.RefLocalName, DictionaryGlobals.SerializationNamespace, id);
					xmlWriter.WriteAttributeBool("i", DictionaryGlobals.XsiNilLocalName, DictionaryGlobals.SchemaInstanceNamespace, true);
				}
				return !flag;
			}
			return base.OnHandleReference(xmlWriter, obj, canContainCyclicReference);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000430C3 File Offset: 0x000412C3
		internal override void OnEndHandleReference(XmlWriterDelegator xmlWriter, object obj, bool canContainCyclicReference)
		{
			if (this.preserveObjectReferences && !this.IsGetOnlyCollection)
			{
				return;
			}
			base.OnEndHandleReference(xmlWriter, obj, canContainCyclicReference);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000430E0 File Offset: 0x000412E0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool CheckIfTypeSerializableForSharedTypeMode(Type memberType)
		{
			ISurrogateSelector surrogateSelector;
			return this.surrogateSelector.GetSurrogate(memberType, this.streamingContext, out surrogateSelector) != null;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00043104 File Offset: 0x00041304
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

		// Token: 0x060010D7 RID: 4311 RVA: 0x00043184 File Offset: 0x00041384
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

		// Token: 0x060010D8 RID: 4312 RVA: 0x000431EC File Offset: 0x000413EC
		private void InternalSerializeWithSurrogate(XmlWriterDelegator xmlWriter, object obj, bool isDeclaredType, bool writeXsiType, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle)
		{
			RuntimeTypeHandle runtimeTypeHandle = isDeclaredType ? declaredTypeHandle : Type.GetTypeHandle(obj);
			object obj2 = obj;
			int oldObjId = 0;
			Type typeFromHandle = Type.GetTypeFromHandle(runtimeTypeHandle);
			Type type = this.GetSurrogatedType(Type.GetTypeFromHandle(declaredTypeHandle));
			if (TD.DCSerializeWithSurrogateStartIsEnabled())
			{
				TD.DCSerializeWithSurrogateStart(type.FullName);
			}
			declaredTypeHandle = type.TypeHandle;
			obj = DataContractSerializer.SurrogateToDataContractType(this.dataContractSurrogate, obj, type, ref typeFromHandle);
			runtimeTypeHandle = typeFromHandle.TypeHandle;
			if (obj2 != obj)
			{
				oldObjId = base.SerializedObjects.ReassignId(0, obj2, obj);
			}
			if (writeXsiType)
			{
				type = Globals.TypeOfObject;
				this.SerializeWithXsiType(xmlWriter, obj, runtimeTypeHandle, typeFromHandle, -1, type.TypeHandle, type);
			}
			else if (declaredTypeHandle.Equals(runtimeTypeHandle))
			{
				DataContract dataContract = this.GetDataContract(runtimeTypeHandle, typeFromHandle);
				base.SerializeWithoutXsiType(dataContract, xmlWriter, obj, declaredTypeHandle);
			}
			else
			{
				this.SerializeWithXsiType(xmlWriter, obj, runtimeTypeHandle, typeFromHandle, -1, declaredTypeHandle, type);
			}
			if (obj2 != obj)
			{
				base.SerializedObjects.ReassignId(oldObjId, obj, obj2);
			}
			if (TD.DCSerializeWithSurrogateStopIsEnabled())
			{
				TD.DCSerializeWithSurrogateStop();
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000432DA File Offset: 0x000414DA
		internal override void WriteArraySize(XmlWriterDelegator xmlWriter, int size)
		{
			if (this.preserveObjectReferences && size > -1)
			{
				xmlWriter.WriteAttributeInt("z", DictionaryGlobals.ArraySizeLocalName, DictionaryGlobals.SerializationNamespace, size);
			}
		}

		// Token: 0x0400071F RID: 1823
		protected IDataContractSurrogate dataContractSurrogate;

		// Token: 0x04000720 RID: 1824
		private SerializationMode mode;

		// Token: 0x04000721 RID: 1825
		private SerializationBinder binder;

		// Token: 0x04000722 RID: 1826
		private ISurrogateSelector surrogateSelector;

		// Token: 0x04000723 RID: 1827
		private StreamingContext streamingContext;

		// Token: 0x04000724 RID: 1828
		private Hashtable surrogateDataContracts;
	}
}
