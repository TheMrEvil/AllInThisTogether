using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x02000147 RID: 327
	internal class XmlObjectSerializerReadContext : XmlObjectSerializerContext
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003FA1B File Offset: 0x0003DC1B
		private HybridObjectCache DeserializedObjects
		{
			get
			{
				if (this.deserializedObjects == null)
				{
					this.deserializedObjects = new HybridObjectCache();
				}
				return this.deserializedObjects;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003FA36 File Offset: 0x0003DC36
		private XmlDocument Document
		{
			get
			{
				if (this.xmlDocument == null)
				{
					this.xmlDocument = new XmlDocument();
				}
				return this.xmlDocument;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003FA51 File Offset: 0x0003DC51
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003FA59 File Offset: 0x0003DC59
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

		// Token: 0x0600101E RID: 4126 RVA: 0x0003FA62 File Offset: 0x0003DC62
		internal object GetCollectionMember()
		{
			return this.getOnlyCollectionValue;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003FA6A File Offset: 0x0003DC6A
		internal void StoreCollectionMemberInfo(object collectionMember)
		{
			this.getOnlyCollectionValue = collectionMember;
			this.isGetOnlyCollection = true;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003FA7A File Offset: 0x0003DC7A
		internal static void ThrowNullValueReturnedForGetOnlyCollectionException(Type type)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("The get-only collection of type '{0}' returned a null value.  The input stream contains collection items which cannot be added if the instance is null.  Consider initializing the collection either in the constructor of the the object or in the getter.", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			})));
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003FA9F File Offset: 0x0003DC9F
		internal static void ThrowArrayExceededSizeException(int arraySize, Type type)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Array length '{0}' provided by the get-only collection of type '{1}' is less than the number of array elements found in the input stream.  Consider increasing the length of the array.", new object[]
			{
				arraySize,
				DataContract.GetClrTypeFullName(type)
			})));
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003FACD File Offset: 0x0003DCCD
		internal static XmlObjectSerializerReadContext CreateContext(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver dataContractResolver)
		{
			if (!serializer.PreserveObjectReferences && serializer.DataContractSurrogate == null)
			{
				return new XmlObjectSerializerReadContext(serializer, rootTypeDataContract, dataContractResolver);
			}
			return new XmlObjectSerializerReadContextComplex(serializer, rootTypeDataContract, dataContractResolver);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003FAF0 File Offset: 0x0003DCF0
		internal static XmlObjectSerializerReadContext CreateContext(NetDataContractSerializer serializer)
		{
			return new XmlObjectSerializerReadContextComplex(serializer);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003FAF8 File Offset: 0x0003DCF8
		internal XmlObjectSerializerReadContext(XmlObjectSerializer serializer, int maxItemsInObjectGraph, StreamingContext streamingContext, bool ignoreExtensionDataObject) : base(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject)
		{
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003FB05 File Offset: 0x0003DD05
		internal XmlObjectSerializerReadContext(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver dataContractResolver) : base(serializer, rootTypeDataContract, dataContractResolver)
		{
			this.attributes = new Attributes();
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003FB1B File Offset: 0x0003DD1B
		protected XmlObjectSerializerReadContext(NetDataContractSerializer serializer) : base(serializer)
		{
			this.attributes = new Attributes();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003FB30 File Offset: 0x0003DD30
		public virtual object InternalDeserialize(XmlReaderDelegator xmlReader, int id, RuntimeTypeHandle declaredTypeHandle, string name, string ns)
		{
			DataContract dataContract = this.GetDataContract(id, declaredTypeHandle);
			return this.InternalDeserialize(xmlReader, name, ns, Type.GetTypeFromHandle(declaredTypeHandle), ref dataContract);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0003FB5C File Offset: 0x0003DD5C
		internal virtual object InternalDeserialize(XmlReaderDelegator xmlReader, Type declaredType, string name, string ns)
		{
			DataContract dataContract = base.GetDataContract(declaredType);
			return this.InternalDeserialize(xmlReader, name, ns, declaredType, ref dataContract);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003FB7E File Offset: 0x0003DD7E
		internal virtual object InternalDeserialize(XmlReaderDelegator xmlReader, Type declaredType, DataContract dataContract, string name, string ns)
		{
			if (dataContract == null)
			{
				base.GetDataContract(declaredType);
			}
			return this.InternalDeserialize(xmlReader, name, ns, declaredType, ref dataContract);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003FB9C File Offset: 0x0003DD9C
		protected bool TryHandleNullOrRef(XmlReaderDelegator reader, Type declaredType, string name, string ns, ref object retObj)
		{
			this.ReadAttributes(reader);
			if (this.attributes.Ref != Globals.NewObjectId)
			{
				if (this.isGetOnlyCollection)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("On type '{1}', attribute '{0}' points to get-only collection, which is not supported.", new object[]
					{
						this.attributes.Ref,
						DataContract.GetClrTypeFullName(declaredType)
					})));
				}
				retObj = this.GetExistingObject(this.attributes.Ref, declaredType, name, ns);
				reader.Skip();
				return true;
			}
			else
			{
				if (this.attributes.XsiNil)
				{
					reader.Skip();
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003FC38 File Offset: 0x0003DE38
		protected object InternalDeserialize(XmlReaderDelegator reader, string name, string ns, Type declaredType, ref DataContract dataContract)
		{
			object result = null;
			if (this.TryHandleNullOrRef(reader, dataContract.UnderlyingType, name, ns, ref result))
			{
				return result;
			}
			bool flag = false;
			if (dataContract.KnownDataContracts != null)
			{
				this.scopedKnownTypes.Push(dataContract.KnownDataContracts);
				flag = true;
			}
			if (this.attributes.XsiTypeName != null)
			{
				dataContract = base.ResolveDataContractFromKnownTypes(this.attributes.XsiTypeName, this.attributes.XsiTypeNamespace, dataContract, declaredType);
				if (dataContract == null)
				{
					if (base.DataContractResolver == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(reader, SR.GetString("Element '{2}:{3}' contains data of the '{0}:{1}' data contract. The deserializer has no knowledge of any type that maps to this contract. Add the type corresponding to '{1}' to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding it to the list of known types passed to DataContractSerializer.", new object[]
						{
							this.attributes.XsiTypeNamespace,
							this.attributes.XsiTypeName,
							reader.NamespaceURI,
							reader.LocalName
						}))));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(reader, SR.GetString("Element '{2}:{3}' contains data from a type that maps to the name '{0}:{1}'. The deserializer has no knowledge of any type that maps to this name. Consider changing the implementation of the ResolveName method on your DataContractResolver to return a non-null value for name '{1}' and namespace '{0}'.", new object[]
					{
						this.attributes.XsiTypeNamespace,
						this.attributes.XsiTypeName,
						reader.NamespaceURI,
						reader.LocalName
					}))));
				}
				else
				{
					flag = this.ReplaceScopedKnownTypesTop(dataContract.KnownDataContracts, flag);
				}
			}
			if (dataContract.IsISerializable && this.attributes.FactoryTypeName != null)
			{
				DataContract dataContract2 = base.ResolveDataContractFromKnownTypes(this.attributes.FactoryTypeName, this.attributes.FactoryTypeNamespace, dataContract, declaredType);
				if (dataContract2 != null)
				{
					if (!dataContract2.IsISerializable)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("For data contract '{1}', factory type '{0}' is not ISerializable.", new object[]
						{
							DataContract.GetClrTypeFullName(dataContract2.UnderlyingType),
							DataContract.GetClrTypeFullName(dataContract.UnderlyingType)
						})));
					}
					dataContract = dataContract2;
					flag = this.ReplaceScopedKnownTypesTop(dataContract.KnownDataContracts, flag);
				}
				else if (DiagnosticUtility.ShouldTraceWarning)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
					dictionary["FactoryType"] = this.attributes.FactoryTypeNamespace + ":" + this.attributes.FactoryTypeName;
					dictionary["ISerializableType"] = dataContract.StableName.Namespace + ":" + dataContract.StableName.Name;
					TraceUtility.Trace(TraceEventType.Warning, 196625, SR.GetString("Factory type not found"), new DictionaryTraceRecord(dictionary));
				}
			}
			if (flag)
			{
				object result2 = this.ReadDataContractValue(dataContract, reader);
				this.scopedKnownTypes.Pop();
				return result2;
			}
			return this.ReadDataContractValue(dataContract, reader);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003FEB3 File Offset: 0x0003E0B3
		private bool ReplaceScopedKnownTypesTop(Dictionary<XmlQualifiedName, DataContract> knownDataContracts, bool knownTypesAddedInCurrentScope)
		{
			if (knownTypesAddedInCurrentScope)
			{
				this.scopedKnownTypes.Pop();
				knownTypesAddedInCurrentScope = false;
			}
			if (knownDataContracts != null)
			{
				this.scopedKnownTypes.Push(knownDataContracts);
				knownTypesAddedInCurrentScope = true;
			}
			return knownTypesAddedInCurrentScope;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003FED9 File Offset: 0x0003E0D9
		public static bool MoveToNextElement(XmlReaderDelegator xmlReader)
		{
			return xmlReader.MoveToContent() != XmlNodeType.EndElement;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
		public int GetMemberIndex(XmlReaderDelegator xmlReader, XmlDictionaryString[] memberNames, XmlDictionaryString[] memberNamespaces, int memberIndex, ExtensionDataObject extensionData)
		{
			for (int i = memberIndex + 1; i < memberNames.Length; i++)
			{
				if (xmlReader.IsStartElement(memberNames[i], memberNamespaces[i]))
				{
					return i;
				}
			}
			this.HandleMemberNotFound(xmlReader, extensionData, memberIndex);
			return memberNames.Length;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003FF24 File Offset: 0x0003E124
		public int GetMemberIndexWithRequiredMembers(XmlReaderDelegator xmlReader, XmlDictionaryString[] memberNames, XmlDictionaryString[] memberNamespaces, int memberIndex, int requiredIndex, ExtensionDataObject extensionData)
		{
			for (int i = memberIndex + 1; i < memberNames.Length; i++)
			{
				if (xmlReader.IsStartElement(memberNames[i], memberNamespaces[i]))
				{
					if (requiredIndex < i)
					{
						XmlObjectSerializerReadContext.ThrowRequiredMemberMissingException(xmlReader, memberIndex, requiredIndex, memberNames);
					}
					return i;
				}
			}
			this.HandleMemberNotFound(xmlReader, extensionData, memberIndex);
			return memberNames.Length;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003FF70 File Offset: 0x0003E170
		public static void ThrowRequiredMemberMissingException(XmlReaderDelegator xmlReader, int memberIndex, int requiredIndex, XmlDictionaryString[] memberNames)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (requiredIndex == memberNames.Length)
			{
				requiredIndex--;
			}
			for (int i = memberIndex + 1; i <= requiredIndex; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(" | ");
				}
				stringBuilder.Append(memberNames[i].Value);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(XmlObjectSerializer.TryAddLineInfo(xmlReader, SR.GetString("'{0}' '{1}' from namespace '{2}' is not expected. Expecting element '{3}'.", new object[]
			{
				xmlReader.NodeType,
				xmlReader.LocalName,
				xmlReader.NamespaceURI,
				stringBuilder.ToString()
			}))));
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00040008 File Offset: 0x0003E208
		protected void HandleMemberNotFound(XmlReaderDelegator xmlReader, ExtensionDataObject extensionData, int memberIndex)
		{
			xmlReader.MoveToContent();
			if (xmlReader.NodeType != XmlNodeType.Element)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
			}
			if (base.IgnoreExtensionDataObject || extensionData == null)
			{
				this.SkipUnknownElement(xmlReader);
				return;
			}
			this.HandleUnknownElement(xmlReader, extensionData, memberIndex);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00040043 File Offset: 0x0003E243
		internal void HandleUnknownElement(XmlReaderDelegator xmlReader, ExtensionDataObject extensionData, int memberIndex)
		{
			if (extensionData.Members == null)
			{
				extensionData.Members = new List<ExtensionDataMember>();
			}
			extensionData.Members.Add(this.ReadExtensionDataMember(xmlReader, memberIndex));
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004006C File Offset: 0x0003E26C
		public void SkipUnknownElement(XmlReaderDelegator xmlReader)
		{
			this.ReadAttributes(xmlReader);
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.Trace(TraceEventType.Verbose, 196615, SR.GetString("Element ignored"), new StringTraceRecord("Element", xmlReader.NamespaceURI + ":" + xmlReader.LocalName));
			}
			xmlReader.Skip();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000400C4 File Offset: 0x0003E2C4
		public string ReadIfNullOrRef(XmlReaderDelegator xmlReader, Type memberType, bool isMemberTypeSerializable)
		{
			if (this.attributes.Ref != Globals.NewObjectId)
			{
				this.CheckIfTypeSerializable(memberType, isMemberTypeSerializable);
				xmlReader.Skip();
				return this.attributes.Ref;
			}
			if (this.attributes.XsiNil)
			{
				this.CheckIfTypeSerializable(memberType, isMemberTypeSerializable);
				xmlReader.Skip();
				return null;
			}
			return Globals.NewObjectId;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00040124 File Offset: 0x0003E324
		internal virtual void ReadAttributes(XmlReaderDelegator xmlReader)
		{
			if (this.attributes == null)
			{
				this.attributes = new Attributes();
			}
			this.attributes.Read(xmlReader);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00040145 File Offset: 0x0003E345
		public void ResetAttributes()
		{
			if (this.attributes != null)
			{
				this.attributes.Reset();
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004015A File Offset: 0x0003E35A
		public string GetObjectId()
		{
			return this.attributes.Id;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00040167 File Offset: 0x0003E367
		internal virtual int GetArraySize()
		{
			return -1;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0004016A File Offset: 0x0003E36A
		public void AddNewObject(object obj)
		{
			this.AddNewObjectWithId(this.attributes.Id, obj);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004017E File Offset: 0x0003E37E
		public void AddNewObjectWithId(string id, object obj)
		{
			if (id != Globals.NewObjectId)
			{
				this.DeserializedObjects.Add(id, obj);
			}
			if (this.extensionDataReader != null)
			{
				this.extensionDataReader.UnderlyingExtensionDataReader.SetDeserializedValue(obj);
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000401B4 File Offset: 0x0003E3B4
		public void ReplaceDeserializedObject(string id, object oldObj, object newObj)
		{
			if (oldObj == newObj)
			{
				return;
			}
			if (id != Globals.NewObjectId)
			{
				if (this.DeserializedObjects.IsObjectReferenced(id))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Factory object contains a reference to self. Old object is '{0}', new object is '{1}'.", new object[]
					{
						DataContract.GetClrTypeFullName(oldObj.GetType()),
						DataContract.GetClrTypeFullName(newObj.GetType()),
						id
					})));
				}
				this.DeserializedObjects.Remove(id);
				this.DeserializedObjects.Add(id, newObj);
			}
			if (this.extensionDataReader != null)
			{
				this.extensionDataReader.UnderlyingExtensionDataReader.SetDeserializedValue(newObj);
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00040250 File Offset: 0x0003E450
		public object GetExistingObject(string id, Type type, string name, string ns)
		{
			object obj = this.DeserializedObjects.GetObject(id);
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Deserialized object with reference id '{0}' not found in stream.", new object[]
				{
					id
				})));
			}
			if (obj is IDataNode)
			{
				IDataNode dataNode = (IDataNode)obj;
				obj = ((dataNode.Value != null && dataNode.IsFinalValue) ? dataNode.Value : this.DeserializeFromExtensionData(dataNode, type, name, ns));
			}
			return obj;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000402C0 File Offset: 0x0003E4C0
		private object GetExistingObjectOrExtensionData(string id)
		{
			object @object = this.DeserializedObjects.GetObject(id);
			if (@object == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Deserialized object with reference id '{0}' not found in stream.", new object[]
				{
					id
				})));
			}
			return @object;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00040300 File Offset: 0x0003E500
		public object GetRealObject(IObjectReference obj, string id)
		{
			object realObject = SurrogateDataContract.GetRealObject(obj, base.GetStreamingContext());
			if (realObject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("On the surrogate data contract for '{0}', GetRealObject method returned null.", new object[]
				{
					DataContract.GetClrTypeFullName(obj.GetType())
				})));
			}
			this.ReplaceDeserializedObject(id, obj, realObject);
			return realObject;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00040350 File Offset: 0x0003E550
		private object DeserializeFromExtensionData(IDataNode dataNode, Type type, string name, string ns)
		{
			ExtensionDataReader extensionDataReader;
			if (this.extensionDataReader == null)
			{
				extensionDataReader = new ExtensionDataReader(this);
				this.extensionDataReader = this.CreateReaderDelegatorForReader(extensionDataReader);
			}
			else
			{
				extensionDataReader = this.extensionDataReader.UnderlyingExtensionDataReader;
			}
			extensionDataReader.SetDataNode(dataNode, name, ns);
			object result = this.InternalDeserialize(this.extensionDataReader, type, name, ns);
			dataNode.Clear();
			extensionDataReader.Reset();
			return result;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000403AD File Offset: 0x0003E5AD
		public static void Read(XmlReaderDelegator xmlReader)
		{
			if (!xmlReader.Read())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected end of file.")));
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000403CC File Offset: 0x0003E5CC
		internal static void ParseQualifiedName(string qname, XmlReaderDelegator xmlReader, out string name, out string ns, out string prefix)
		{
			int num = qname.IndexOf(':');
			prefix = "";
			if (num >= 0)
			{
				prefix = qname.Substring(0, num);
			}
			name = qname.Substring(num + 1);
			ns = xmlReader.LookupNamespace(prefix);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00040410 File Offset: 0x0003E610
		public static T[] EnsureArraySize<T>(T[] array, int index)
		{
			if (array.Length <= index)
			{
				if (index == 2147483647)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("The maximum array length ({0}) has been exceeded while reading XML data for array of type '{1}'.", new object[]
					{
						int.MaxValue,
						DataContract.GetClrTypeFullName(typeof(T))
					})));
				}
				T[] array2 = new T[(index < 1073741823) ? (index * 2) : int.MaxValue];
				Array.Copy(array, 0, array2, 0, array.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00040490 File Offset: 0x0003E690
		public static T[] TrimArraySize<T>(T[] array, int size)
		{
			if (size != array.Length)
			{
				T[] array2 = new T[size];
				Array.Copy(array, 0, array2, 0, size);
				array = array2;
			}
			return array;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000404B8 File Offset: 0x0003E6B8
		public void CheckEndOfArray(XmlReaderDelegator xmlReader, int arraySize, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (xmlReader.NodeType == XmlNodeType.EndElement)
			{
				return;
			}
			while (xmlReader.IsStartElement())
			{
				if (xmlReader.IsStartElement(itemName, itemNamespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Array length '{0}' provided by Size attribute is not equal to the number of array elements '{1}' from namespace '{2}' found.", new object[]
					{
						arraySize,
						itemName.Value,
						itemNamespace.Value
					})));
				}
				this.SkipUnknownElement(xmlReader);
			}
			if (xmlReader.NodeType != XmlNodeType.EndElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.EndElement, xmlReader));
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00040539 File Offset: 0x0003E739
		internal object ReadIXmlSerializable(XmlReaderDelegator xmlReader, XmlDataContract xmlDataContract, bool isMemberType)
		{
			if (this.xmlSerializableReader == null)
			{
				this.xmlSerializableReader = new XmlSerializableReader();
			}
			return XmlObjectSerializerReadContext.ReadIXmlSerializable(this.xmlSerializableReader, xmlReader, xmlDataContract, isMemberType);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004055C File Offset: 0x0003E75C
		internal static object ReadRootIXmlSerializable(XmlReaderDelegator xmlReader, XmlDataContract xmlDataContract, bool isMemberType)
		{
			return XmlObjectSerializerReadContext.ReadIXmlSerializable(new XmlSerializableReader(), xmlReader, xmlDataContract, isMemberType);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004056C File Offset: 0x0003E76C
		internal static object ReadIXmlSerializable(XmlSerializableReader xmlSerializableReader, XmlReaderDelegator xmlReader, XmlDataContract xmlDataContract, bool isMemberType)
		{
			xmlSerializableReader.BeginRead(xmlReader);
			if (isMemberType && !xmlDataContract.HasRoot)
			{
				xmlReader.Read();
				xmlReader.MoveToContent();
			}
			object result;
			if (xmlDataContract.UnderlyingType == Globals.TypeOfXmlElement)
			{
				if (!xmlReader.IsStartElement())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
				}
				result = (XmlElement)new XmlDocument().ReadNode(xmlSerializableReader);
			}
			else if (xmlDataContract.UnderlyingType == Globals.TypeOfXmlNodeArray)
			{
				result = XmlSerializableServices.ReadNodes(xmlSerializableReader);
			}
			else
			{
				IXmlSerializable xmlSerializable = xmlDataContract.CreateXmlSerializableDelegate();
				xmlSerializable.ReadXml(xmlSerializableReader);
				result = xmlSerializable;
			}
			xmlSerializableReader.EndRead();
			return result;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004060C File Offset: 0x0003E80C
		public SerializationInfo ReadSerializationInfo(XmlReaderDelegator xmlReader, Type type)
		{
			SerializationInfo serializationInfo = new SerializationInfo(type, XmlObjectSerializer.FormatterConverter);
			XmlNodeType xmlNodeType;
			while ((xmlNodeType = xmlReader.MoveToContent()) != XmlNodeType.EndElement)
			{
				if (xmlNodeType != XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
				}
				if (xmlReader.NamespaceURI.Length != 0)
				{
					this.SkipUnknownElement(xmlReader);
				}
				else
				{
					string name = XmlConvert.DecodeName(xmlReader.LocalName);
					base.IncrementItemCount(1);
					this.ReadAttributes(xmlReader);
					object value;
					if (this.attributes.Ref != Globals.NewObjectId)
					{
						xmlReader.Skip();
						value = this.GetExistingObject(this.attributes.Ref, null, name, string.Empty);
					}
					else if (this.attributes.XsiNil)
					{
						xmlReader.Skip();
						value = null;
					}
					else
					{
						value = this.InternalDeserialize(xmlReader, Globals.TypeOfObject, name, string.Empty);
					}
					serializationInfo.AddValue(name, value);
				}
			}
			return serializationInfo;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000406E8 File Offset: 0x0003E8E8
		protected virtual DataContract ResolveDataContractFromTypeName()
		{
			if (this.attributes.XsiTypeName != null)
			{
				return base.ResolveDataContractFromKnownTypes(this.attributes.XsiTypeName, this.attributes.XsiTypeNamespace, null, null);
			}
			return null;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00040718 File Offset: 0x0003E918
		private ExtensionDataMember ReadExtensionDataMember(XmlReaderDelegator xmlReader, int memberIndex)
		{
			ExtensionDataMember extensionDataMember = new ExtensionDataMember();
			extensionDataMember.Name = xmlReader.LocalName;
			extensionDataMember.Namespace = xmlReader.NamespaceURI;
			extensionDataMember.MemberIndex = memberIndex;
			if (xmlReader.UnderlyingExtensionDataReader != null)
			{
				extensionDataMember.Value = xmlReader.UnderlyingExtensionDataReader.GetCurrentNode();
			}
			else
			{
				extensionDataMember.Value = this.ReadExtensionDataValue(xmlReader);
			}
			return extensionDataMember;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00040774 File Offset: 0x0003E974
		public IDataNode ReadExtensionDataValue(XmlReaderDelegator xmlReader)
		{
			this.ReadAttributes(xmlReader);
			base.IncrementItemCount(1);
			IDataNode dataNode = null;
			if (this.attributes.Ref != Globals.NewObjectId)
			{
				xmlReader.Skip();
				object existingObjectOrExtensionData = this.GetExistingObjectOrExtensionData(this.attributes.Ref);
				IDataNode dataNode3;
				if (!(existingObjectOrExtensionData is IDataNode))
				{
					IDataNode dataNode2 = new DataNode<object>(existingObjectOrExtensionData);
					dataNode3 = dataNode2;
				}
				else
				{
					dataNode3 = (IDataNode)existingObjectOrExtensionData;
				}
				dataNode = dataNode3;
				dataNode.Id = this.attributes.Ref;
			}
			else if (this.attributes.XsiNil)
			{
				xmlReader.Skip();
				dataNode = null;
			}
			else
			{
				string dataContractName = null;
				string dataContractNamespace = null;
				if (this.attributes.XsiTypeName != null)
				{
					dataContractName = this.attributes.XsiTypeName;
					dataContractNamespace = this.attributes.XsiTypeNamespace;
				}
				if (this.IsReadingCollectionExtensionData(xmlReader))
				{
					XmlObjectSerializerReadContext.Read(xmlReader);
					dataNode = this.ReadUnknownCollectionData(xmlReader, dataContractName, dataContractNamespace);
				}
				else if (this.attributes.FactoryTypeName != null)
				{
					XmlObjectSerializerReadContext.Read(xmlReader);
					dataNode = this.ReadUnknownISerializableData(xmlReader, dataContractName, dataContractNamespace);
				}
				else if (this.IsReadingClassExtensionData(xmlReader))
				{
					XmlObjectSerializerReadContext.Read(xmlReader);
					dataNode = this.ReadUnknownClassData(xmlReader, dataContractName, dataContractNamespace);
				}
				else
				{
					DataContract dataContract = this.ResolveDataContractFromTypeName();
					if (dataContract == null)
					{
						dataNode = this.ReadExtensionDataValue(xmlReader, dataContractName, dataContractNamespace);
					}
					else if (dataContract is XmlDataContract)
					{
						dataNode = this.ReadUnknownXmlData(xmlReader, dataContractName, dataContractNamespace);
					}
					else if (dataContract.IsISerializable)
					{
						XmlObjectSerializerReadContext.Read(xmlReader);
						dataNode = this.ReadUnknownISerializableData(xmlReader, dataContractName, dataContractNamespace);
					}
					else if (dataContract is PrimitiveDataContract)
					{
						if (this.attributes.Id == Globals.NewObjectId)
						{
							XmlObjectSerializerReadContext.Read(xmlReader);
							xmlReader.MoveToContent();
							dataNode = this.ReadUnknownPrimitiveData(xmlReader, dataContract.UnderlyingType, dataContractName, dataContractNamespace);
							xmlReader.ReadEndElement();
						}
						else
						{
							dataNode = new DataNode<object>(xmlReader.ReadElementContentAsAnyType(dataContract.UnderlyingType));
							this.InitializeExtensionDataNode(dataNode, dataContractName, dataContractNamespace);
						}
					}
					else if (dataContract is EnumDataContract)
					{
						dataNode = new DataNode<object>(((EnumDataContract)dataContract).ReadEnumValue(xmlReader));
						this.InitializeExtensionDataNode(dataNode, dataContractName, dataContractNamespace);
					}
					else if (dataContract is ClassDataContract)
					{
						XmlObjectSerializerReadContext.Read(xmlReader);
						dataNode = this.ReadUnknownClassData(xmlReader, dataContractName, dataContractNamespace);
					}
					else if (dataContract is CollectionDataContract)
					{
						XmlObjectSerializerReadContext.Read(xmlReader);
						dataNode = this.ReadUnknownCollectionData(xmlReader, dataContractName, dataContractNamespace);
					}
				}
			}
			return dataNode;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0000A8EE File Offset: 0x00008AEE
		protected virtual void StartReadExtensionDataValue(XmlReaderDelegator xmlReader)
		{
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000409B0 File Offset: 0x0003EBB0
		private IDataNode ReadExtensionDataValue(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			this.StartReadExtensionDataValue(xmlReader);
			if (this.attributes.UnrecognizedAttributesFound)
			{
				return this.ReadUnknownXmlData(xmlReader, dataContractName, dataContractNamespace);
			}
			IDictionary<string, string> namespacesInScope = xmlReader.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
			XmlObjectSerializerReadContext.Read(xmlReader);
			xmlReader.MoveToContent();
			XmlNodeType nodeType = xmlReader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType == XmlNodeType.Text)
				{
					return this.ReadPrimitiveExtensionDataValue(xmlReader, dataContractName, dataContractNamespace);
				}
				if (nodeType != XmlNodeType.EndElement)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
				}
				IDataNode dataNode = this.ReadUnknownPrimitiveData(xmlReader, Globals.TypeOfObject, dataContractName, dataContractNamespace);
				xmlReader.ReadEndElement();
				dataNode.IsFinalValue = false;
				return dataNode;
			}
			else
			{
				if (xmlReader.NamespaceURI.StartsWith("http://schemas.datacontract.org/2004/07/", StringComparison.Ordinal))
				{
					return this.ReadUnknownClassData(xmlReader, dataContractName, dataContractNamespace);
				}
				return this.ReadAndResolveUnknownXmlData(xmlReader, namespacesInScope, dataContractName, dataContractNamespace);
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00040A60 File Offset: 0x0003EC60
		protected virtual IDataNode ReadPrimitiveExtensionDataValue(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			Type valueType = xmlReader.ValueType;
			if (valueType == Globals.TypeOfString)
			{
				IDataNode dataNode = new DataNode<object>(xmlReader.ReadContentAsString());
				this.InitializeExtensionDataNode(dataNode, dataContractName, dataContractNamespace);
				dataNode.IsFinalValue = false;
				xmlReader.ReadEndElement();
				return dataNode;
			}
			IDataNode result = this.ReadUnknownPrimitiveData(xmlReader, valueType, dataContractName, dataContractNamespace);
			xmlReader.ReadEndElement();
			return result;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00040AB8 File Offset: 0x0003ECB8
		protected void InitializeExtensionDataNode(IDataNode dataNode, string dataContractName, string dataContractNamespace)
		{
			dataNode.DataContractName = dataContractName;
			dataNode.DataContractNamespace = dataContractNamespace;
			dataNode.ClrAssemblyName = this.attributes.ClrAssembly;
			dataNode.ClrTypeName = this.attributes.ClrType;
			this.AddNewObject(dataNode);
			dataNode.Id = this.attributes.Id;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00040B10 File Offset: 0x0003ED10
		private IDataNode ReadUnknownPrimitiveData(XmlReaderDelegator xmlReader, Type type, string dataContractName, string dataContractNamespace)
		{
			IDataNode dataNode = xmlReader.ReadExtensionData(type);
			this.InitializeExtensionDataNode(dataNode, dataContractName, dataContractNamespace);
			return dataNode;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00040B30 File Offset: 0x0003ED30
		private ClassDataNode ReadUnknownClassData(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			ClassDataNode classDataNode = new ClassDataNode();
			this.InitializeExtensionDataNode(classDataNode, dataContractName, dataContractNamespace);
			int num = 0;
			XmlNodeType xmlNodeType;
			while ((xmlNodeType = xmlReader.MoveToContent()) != XmlNodeType.EndElement)
			{
				if (xmlNodeType != XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
				}
				if (classDataNode.Members == null)
				{
					classDataNode.Members = new List<ExtensionDataMember>();
				}
				classDataNode.Members.Add(this.ReadExtensionDataMember(xmlReader, num++));
			}
			xmlReader.ReadEndElement();
			return classDataNode;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00040BA0 File Offset: 0x0003EDA0
		private CollectionDataNode ReadUnknownCollectionData(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			CollectionDataNode collectionDataNode = new CollectionDataNode();
			this.InitializeExtensionDataNode(collectionDataNode, dataContractName, dataContractNamespace);
			int arraySZSize = this.attributes.ArraySZSize;
			XmlNodeType xmlNodeType;
			while ((xmlNodeType = xmlReader.MoveToContent()) != XmlNodeType.EndElement)
			{
				if (xmlNodeType != XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
				}
				if (collectionDataNode.ItemName == null)
				{
					collectionDataNode.ItemName = xmlReader.LocalName;
					collectionDataNode.ItemNamespace = xmlReader.NamespaceURI;
				}
				if (xmlReader.IsStartElement(collectionDataNode.ItemName, collectionDataNode.ItemNamespace))
				{
					if (collectionDataNode.Items == null)
					{
						collectionDataNode.Items = new List<IDataNode>();
					}
					collectionDataNode.Items.Add(this.ReadExtensionDataValue(xmlReader));
				}
				else
				{
					this.SkipUnknownElement(xmlReader);
				}
			}
			xmlReader.ReadEndElement();
			if (arraySZSize != -1)
			{
				collectionDataNode.Size = arraySZSize;
				if (collectionDataNode.Items == null)
				{
					if (collectionDataNode.Size > 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Array size attribute is incorrect; must be between {0} and {1}.", new object[]
						{
							arraySZSize,
							0
						})));
					}
				}
				else if (collectionDataNode.Size != collectionDataNode.Items.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Array size attribute is incorrect; must be between {0} and {1}.", new object[]
					{
						arraySZSize,
						collectionDataNode.Items.Count
					})));
				}
			}
			else if (collectionDataNode.Items != null)
			{
				collectionDataNode.Size = collectionDataNode.Items.Count;
			}
			else
			{
				collectionDataNode.Size = 0;
			}
			return collectionDataNode;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00040D0C File Offset: 0x0003EF0C
		private ISerializableDataNode ReadUnknownISerializableData(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			ISerializableDataNode serializableDataNode = new ISerializableDataNode();
			this.InitializeExtensionDataNode(serializableDataNode, dataContractName, dataContractNamespace);
			serializableDataNode.FactoryTypeName = this.attributes.FactoryTypeName;
			serializableDataNode.FactoryTypeNamespace = this.attributes.FactoryTypeNamespace;
			XmlNodeType xmlNodeType;
			while ((xmlNodeType = xmlReader.MoveToContent()) != XmlNodeType.EndElement)
			{
				if (xmlNodeType != XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
				}
				if (xmlReader.NamespaceURI.Length != 0)
				{
					this.SkipUnknownElement(xmlReader);
				}
				else
				{
					ISerializableDataMember serializableDataMember = new ISerializableDataMember();
					serializableDataMember.Name = xmlReader.LocalName;
					serializableDataMember.Value = this.ReadExtensionDataValue(xmlReader);
					if (serializableDataNode.Members == null)
					{
						serializableDataNode.Members = new List<ISerializableDataMember>();
					}
					serializableDataNode.Members.Add(serializableDataMember);
				}
			}
			xmlReader.ReadEndElement();
			return serializableDataNode;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00040DC4 File Offset: 0x0003EFC4
		private IDataNode ReadUnknownXmlData(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			XmlDataNode xmlDataNode = new XmlDataNode();
			this.InitializeExtensionDataNode(xmlDataNode, dataContractName, dataContractNamespace);
			xmlDataNode.OwnerDocument = this.Document;
			if (xmlReader.NodeType == XmlNodeType.EndElement)
			{
				return xmlDataNode;
			}
			IList<XmlAttribute> list = null;
			IList<XmlNode> list2 = null;
			if (xmlReader.MoveToContent() != XmlNodeType.Text)
			{
				while (xmlReader.MoveToNextAttribute())
				{
					string namespaceURI = xmlReader.NamespaceURI;
					if (namespaceURI != "http://schemas.microsoft.com/2003/10/Serialization/" && namespaceURI != "http://www.w3.org/2001/XMLSchema-instance")
					{
						if (list == null)
						{
							list = new List<XmlAttribute>();
						}
						list.Add((XmlAttribute)this.Document.ReadNode(xmlReader.UnderlyingReader));
					}
				}
				XmlObjectSerializerReadContext.Read(xmlReader);
			}
			while (xmlReader.MoveToContent() != XmlNodeType.EndElement)
			{
				if (xmlReader.EOF)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected end of file.")));
				}
				if (list2 == null)
				{
					list2 = new List<XmlNode>();
				}
				list2.Add(this.Document.ReadNode(xmlReader.UnderlyingReader));
			}
			xmlReader.ReadEndElement();
			xmlDataNode.XmlAttributes = list;
			xmlDataNode.XmlChildNodes = list2;
			return xmlDataNode;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00040EBC File Offset: 0x0003F0BC
		private IDataNode ReadAndResolveUnknownXmlData(XmlReaderDelegator xmlReader, IDictionary<string, string> namespaces, string dataContractName, string dataContractNamespace)
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			string strA = null;
			string text = null;
			IList<XmlNode> list = new List<XmlNode>();
			IList<XmlAttribute> list2 = null;
			if (namespaces == null)
			{
				goto IL_194;
			}
			list2 = new List<XmlAttribute>();
			using (IEnumerator<KeyValuePair<string, string>> enumerator = namespaces.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					list2.Add(this.AddNamespaceDeclaration(keyValuePair.Key, keyValuePair.Value));
				}
				goto IL_194;
			}
			IL_6A:
			XmlNodeType nodeType;
			if (nodeType == XmlNodeType.Element)
			{
				string namespaceURI = xmlReader.NamespaceURI;
				string localName = xmlReader.LocalName;
				if (flag)
				{
					flag = (namespaceURI.Length == 0);
				}
				if (flag2)
				{
					if (text == null)
					{
						text = localName;
						strA = namespaceURI;
					}
					else
					{
						flag2 = (string.CompareOrdinal(text, localName) == 0 && string.CompareOrdinal(strA, namespaceURI) == 0);
					}
				}
			}
			else
			{
				if (xmlReader.EOF)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected end of file.")));
				}
				if (this.IsContentNode(xmlReader.NodeType))
				{
					flag = (flag3 = (flag2 = false));
				}
			}
			if (this.attributesInXmlData == null)
			{
				this.attributesInXmlData = new Attributes();
			}
			this.attributesInXmlData.Read(xmlReader);
			XmlNode xmlNode = this.Document.ReadNode(xmlReader.UnderlyingReader);
			list.Add(xmlNode);
			if (namespaces == null)
			{
				if (this.attributesInXmlData.XsiTypeName != null)
				{
					xmlNode.Attributes.Append(this.AddNamespaceDeclaration(this.attributesInXmlData.XsiTypePrefix, this.attributesInXmlData.XsiTypeNamespace));
				}
				if (this.attributesInXmlData.FactoryTypeName != null)
				{
					xmlNode.Attributes.Append(this.AddNamespaceDeclaration(this.attributesInXmlData.FactoryTypePrefix, this.attributesInXmlData.FactoryTypeNamespace));
				}
			}
			IL_194:
			if ((nodeType = xmlReader.NodeType) != XmlNodeType.EndElement)
			{
				goto IL_6A;
			}
			xmlReader.ReadEndElement();
			if (text != null && flag2)
			{
				return this.ReadUnknownCollectionData(this.CreateReaderOverChildNodes(list2, list), dataContractName, dataContractNamespace);
			}
			if (flag)
			{
				return this.ReadUnknownISerializableData(this.CreateReaderOverChildNodes(list2, list), dataContractName, dataContractNamespace);
			}
			if (flag3)
			{
				return this.ReadUnknownClassData(this.CreateReaderOverChildNodes(list2, list), dataContractName, dataContractNamespace);
			}
			XmlDataNode xmlDataNode = new XmlDataNode();
			this.InitializeExtensionDataNode(xmlDataNode, dataContractName, dataContractNamespace);
			xmlDataNode.OwnerDocument = this.Document;
			xmlDataNode.XmlChildNodes = list;
			xmlDataNode.XmlAttributes = list2;
			return xmlDataNode;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00041104 File Offset: 0x0003F304
		private bool IsContentNode(XmlNodeType nodeType)
		{
			switch (nodeType)
			{
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return false;
			}
			return true;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00041134 File Offset: 0x0003F334
		internal XmlReaderDelegator CreateReaderOverChildNodes(IList<XmlAttribute> xmlAttributes, IList<XmlNode> xmlChildNodes)
		{
			XmlNode node = XmlObjectSerializerReadContext.CreateWrapperXmlElement(this.Document, xmlAttributes, xmlChildNodes, null, null, null);
			XmlReaderDelegator xmlReaderDelegator = this.CreateReaderDelegatorForReader(new XmlNodeReader(node));
			xmlReaderDelegator.MoveToContent();
			XmlObjectSerializerReadContext.Read(xmlReaderDelegator);
			return xmlReaderDelegator;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0004116C File Offset: 0x0003F36C
		internal static XmlNode CreateWrapperXmlElement(XmlDocument document, IList<XmlAttribute> xmlAttributes, IList<XmlNode> xmlChildNodes, string prefix, string localName, string ns)
		{
			localName = (localName ?? "wrapper");
			ns = (ns ?? string.Empty);
			XmlNode xmlNode = document.CreateElement(prefix, localName, ns);
			if (xmlAttributes != null)
			{
				for (int i = 0; i < xmlAttributes.Count; i++)
				{
					xmlNode.Attributes.Append(xmlAttributes[i]);
				}
			}
			if (xmlChildNodes != null)
			{
				for (int j = 0; j < xmlChildNodes.Count; j++)
				{
					xmlNode.AppendChild(xmlChildNodes[j]);
				}
			}
			return xmlNode;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000411EC File Offset: 0x0003F3EC
		private XmlAttribute AddNamespaceDeclaration(string prefix, string ns)
		{
			XmlAttribute xmlAttribute = (prefix == null || prefix.Length == 0) ? this.Document.CreateAttribute(null, "xmlns", "http://www.w3.org/2000/xmlns/") : this.Document.CreateAttribute("xmlns", prefix, "http://www.w3.org/2000/xmlns/");
			xmlAttribute.Value = ns;
			return xmlAttribute;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00041239 File Offset: 0x0003F439
		public static Exception CreateUnexpectedStateException(XmlNodeType expectedState, XmlReaderDelegator xmlReader)
		{
			return XmlObjectSerializer.CreateSerializationExceptionWithReaderDetails(SR.GetString("Expecting state '{0}'.", new object[]
			{
				expectedState
			}), xmlReader);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004125A File Offset: 0x0003F45A
		protected virtual object ReadDataContractValue(DataContract dataContract, XmlReaderDelegator reader)
		{
			return dataContract.ReadXmlValue(reader, this);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00041264 File Offset: 0x0003F464
		protected virtual XmlReaderDelegator CreateReaderDelegatorForReader(XmlReader xmlReader)
		{
			return new XmlReaderDelegator(xmlReader);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0004126C File Offset: 0x0003F46C
		protected virtual bool IsReadingCollectionExtensionData(XmlReaderDelegator xmlReader)
		{
			return this.attributes.ArraySZSize != -1;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00003127 File Offset: 0x00001327
		protected virtual bool IsReadingClassExtensionData(XmlReaderDelegator xmlReader)
		{
			return false;
		}

		// Token: 0x04000702 RID: 1794
		internal Attributes attributes;

		// Token: 0x04000703 RID: 1795
		private HybridObjectCache deserializedObjects;

		// Token: 0x04000704 RID: 1796
		private XmlSerializableReader xmlSerializableReader;

		// Token: 0x04000705 RID: 1797
		private XmlDocument xmlDocument;

		// Token: 0x04000706 RID: 1798
		private Attributes attributesInXmlData;

		// Token: 0x04000707 RID: 1799
		private XmlReaderDelegator extensionDataReader;

		// Token: 0x04000708 RID: 1800
		private object getOnlyCollectionValue;

		// Token: 0x04000709 RID: 1801
		private bool isGetOnlyCollection;
	}
}
