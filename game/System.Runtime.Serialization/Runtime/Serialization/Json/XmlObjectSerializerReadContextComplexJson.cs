using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000194 RID: 404
	internal class XmlObjectSerializerReadContextComplexJson : XmlObjectSerializerReadContextComplex
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x00050340 File Offset: 0x0004E540
		public XmlObjectSerializerReadContextComplexJson(DataContractJsonSerializer serializer, DataContract rootTypeDataContract) : base(serializer, serializer.MaxItemsInObjectGraph, new StreamingContext(StreamingContextStates.All), serializer.IgnoreExtensionDataObject)
		{
			this.rootTypeDataContract = rootTypeDataContract;
			this.serializerKnownTypeList = serializer.knownTypeList;
			this.dataContractSurrogate = serializer.DataContractSurrogate;
			this.dateTimeFormat = serializer.DateTimeFormat;
			this.useSimpleDictionaryFormat = serializer.UseSimpleDictionaryFormat;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x000503A1 File Offset: 0x0004E5A1
		internal IList<Type> SerializerKnownTypeList
		{
			get
			{
				return this.serializerKnownTypeList;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x000503A9 File Offset: 0x0004E5A9
		public bool UseSimpleDictionaryFormat
		{
			get
			{
				return this.useSimpleDictionaryFormat;
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000503B1 File Offset: 0x0004E5B1
		protected override void StartReadExtensionDataValue(XmlReaderDelegator xmlReader)
		{
			this.extensionDataValueType = xmlReader.GetAttribute("type");
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000503C4 File Offset: 0x0004E5C4
		protected override IDataNode ReadPrimitiveExtensionDataValue(XmlReaderDelegator xmlReader, string dataContractName, string dataContractNamespace)
		{
			string text = this.extensionDataValueType;
			IDataNode result;
			if (text != null && !(text == "string"))
			{
				if (!(text == "boolean"))
				{
					if (!(text == "number"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected attribute value '{0}'.", new object[]
						{
							this.extensionDataValueType
						})));
					}
					result = this.ReadNumericalPrimitiveExtensionDataValue(xmlReader);
				}
				else
				{
					result = new DataNode<bool>(xmlReader.ReadContentAsBoolean());
				}
			}
			else
			{
				result = new DataNode<string>(xmlReader.ReadContentAsString());
			}
			xmlReader.ReadEndElement();
			return result;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00050458 File Offset: 0x0004E658
		private IDataNode ReadNumericalPrimitiveExtensionDataValue(XmlReaderDelegator xmlReader)
		{
			TypeCode typeCode;
			object obj = JsonObjectDataContract.ParseJsonNumber(xmlReader.ReadContentAsString(), out typeCode);
			switch (typeCode)
			{
			case TypeCode.SByte:
				return new DataNode<sbyte>((sbyte)obj);
			case TypeCode.Byte:
				return new DataNode<byte>((byte)obj);
			case TypeCode.Int16:
				return new DataNode<short>((short)obj);
			case TypeCode.UInt16:
				return new DataNode<ushort>((ushort)obj);
			case TypeCode.Int32:
				return new DataNode<int>((int)obj);
			case TypeCode.UInt32:
				return new DataNode<uint>((uint)obj);
			case TypeCode.Int64:
				return new DataNode<long>((long)obj);
			case TypeCode.UInt64:
				return new DataNode<ulong>((ulong)obj);
			case TypeCode.Single:
				return new DataNode<float>((float)obj);
			case TypeCode.Double:
				return new DataNode<double>((double)obj);
			case TypeCode.Decimal:
				return new DataNode<decimal>((decimal)obj);
			default:
				throw Fx.AssertAndThrow("JsonObjectDataContract.ParseJsonNumber shouldn't return a TypeCode that we're not expecting");
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0005053A File Offset: 0x0004E73A
		internal static XmlObjectSerializerReadContextComplexJson CreateContext(DataContractJsonSerializer serializer, DataContract rootTypeDataContract)
		{
			return new XmlObjectSerializerReadContextComplexJson(serializer, rootTypeDataContract);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00040167 File Offset: 0x0003E367
		internal override int GetArraySize()
		{
			return -1;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00050543 File Offset: 0x0004E743
		protected override object ReadDataContractValue(DataContract dataContract, XmlReaderDelegator reader)
		{
			return DataContractJsonSerializer.ReadJsonValue(dataContract, reader, this);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00050550 File Offset: 0x0004E750
		internal override void ReadAttributes(XmlReaderDelegator xmlReader)
		{
			if (this.attributes == null)
			{
				this.attributes = new Attributes();
			}
			this.attributes.Reset();
			if (xmlReader.MoveToAttribute("type") && xmlReader.Value == "null")
			{
				this.attributes.XsiNil = true;
			}
			else if (xmlReader.MoveToAttribute("__type"))
			{
				XmlQualifiedName xmlQualifiedName = JsonReaderDelegator.ParseQualifiedName(xmlReader.Value);
				this.attributes.XsiTypeName = xmlQualifiedName.Name;
				string text = xmlQualifiedName.Namespace;
				if (!string.IsNullOrEmpty(text))
				{
					char c = text[0];
					if (c != '#')
					{
						if (c == '\\')
						{
							if (text.Length >= 2)
							{
								char c2 = text[1];
								if (c2 == '#' || c2 == '\\')
								{
									text = text.Substring(1);
								}
							}
						}
					}
					else
					{
						text = "http://schemas.datacontract.org/2004/07/" + text.Substring(1);
					}
				}
				this.attributes.XsiTypeNamespace = text;
			}
			xmlReader.MoveToElement();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00050648 File Offset: 0x0004E848
		public int GetJsonMemberIndex(XmlReaderDelegator xmlReader, XmlDictionaryString[] memberNames, int memberIndex, ExtensionDataObject extensionData)
		{
			int num = memberNames.Length;
			if (num != 0)
			{
				int i = 0;
				int num2 = (memberIndex + 1) % num;
				while (i < num)
				{
					if (xmlReader.IsStartElement(memberNames[num2], XmlDictionaryString.Empty))
					{
						return num2;
					}
					i++;
					num2 = (num2 + 1) % num;
				}
				string b;
				if (XmlObjectSerializerReadContextComplexJson.TryGetJsonLocalName(xmlReader, out b))
				{
					int j = 0;
					int num3 = (memberIndex + 1) % num;
					while (j < num)
					{
						if (memberNames[num3].Value == b)
						{
							return num3;
						}
						j++;
						num3 = (num3 + 1) % num;
					}
				}
			}
			base.HandleMemberNotFound(xmlReader, extensionData, memberIndex);
			return num;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000506CE File Offset: 0x0004E8CE
		internal static bool TryGetJsonLocalName(XmlReaderDelegator xmlReader, out string name)
		{
			if (xmlReader.IsStartElement(JsonGlobals.itemDictionaryString, JsonGlobals.itemDictionaryString) && xmlReader.MoveToAttribute("item"))
			{
				name = xmlReader.Value;
				return true;
			}
			name = null;
			return false;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00050700 File Offset: 0x0004E900
		public static string GetJsonMemberName(XmlReaderDelegator xmlReader)
		{
			string localName;
			if (!XmlObjectSerializerReadContextComplexJson.TryGetJsonLocalName(xmlReader, out localName))
			{
				localName = xmlReader.LocalName;
			}
			return localName;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00050720 File Offset: 0x0004E920
		public static void ThrowMissingRequiredMembers(object obj, XmlDictionaryString[] memberNames, byte[] expectedElements, byte[] requiredElements)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 0; i < memberNames.Length; i++)
			{
				if (XmlObjectSerializerReadContextComplexJson.IsBitSet(expectedElements, i) && XmlObjectSerializerReadContextComplexJson.IsBitSet(requiredElements, i))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(memberNames[i]);
					num++;
				}
			}
			if (num == 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Required member {1} in type '{0}' is not found.", new object[]
				{
					DataContract.GetClrTypeFullName(obj.GetType()),
					stringBuilder.ToString()
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Required members {0} in type '{1}' are not found.", new object[]
			{
				DataContract.GetClrTypeFullName(obj.GetType()),
				stringBuilder.ToString()
			})));
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000507DE File Offset: 0x0004E9DE
		public static void ThrowDuplicateMemberException(object obj, XmlDictionaryString[] memberNames, int memberIndex)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Duplicate member '{0}' is found in JSON input.", new object[]
			{
				DataContract.GetClrTypeFullName(obj.GetType()),
				memberNames[memberIndex]
			})));
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0005080E File Offset: 0x0004EA0E
		[SecuritySafeCritical]
		private static bool IsBitSet(byte[] bytes, int bitIndex)
		{
			return BitFlagsGenerator.IsBitSet(bytes, bitIndex);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00050817 File Offset: 0x0004EA17
		protected override bool IsReadingCollectionExtensionData(XmlReaderDelegator xmlReader)
		{
			return xmlReader.GetAttribute("type") == "array";
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0005082E File Offset: 0x0004EA2E
		protected override bool IsReadingClassExtensionData(XmlReaderDelegator xmlReader)
		{
			return xmlReader.GetAttribute("type") == "object";
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00050845 File Offset: 0x0004EA45
		protected override XmlReaderDelegator CreateReaderDelegatorForReader(XmlReader xmlReader)
		{
			return new JsonReaderDelegator(xmlReader, this.dateTimeFormat);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00050853 File Offset: 0x0004EA53
		internal override DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContract = base.GetDataContract(typeHandle, type);
			DataContractJsonSerializer.CheckIfTypeIsReference(dataContract);
			return dataContract;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00050863 File Offset: 0x0004EA63
		internal override DataContract GetDataContractSkipValidation(int typeId, RuntimeTypeHandle typeHandle, Type type)
		{
			DataContract dataContractSkipValidation = base.GetDataContractSkipValidation(typeId, typeHandle, type);
			DataContractJsonSerializer.CheckIfTypeIsReference(dataContractSkipValidation);
			return dataContractSkipValidation;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00050874 File Offset: 0x0004EA74
		internal override DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
		{
			DataContract dataContract = base.GetDataContract(id, typeHandle);
			DataContractJsonSerializer.CheckIfTypeIsReference(dataContract);
			return dataContract;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00050884 File Offset: 0x0004EA84
		protected override DataContract ResolveDataContractFromRootDataContract(XmlQualifiedName typeQName)
		{
			return XmlObjectSerializerWriteContextComplexJson.ResolveJsonDataContractFromRootDataContract(this, typeQName, this.rootTypeDataContract);
		}

		// Token: 0x04000A3D RID: 2621
		private string extensionDataValueType;

		// Token: 0x04000A3E RID: 2622
		private DateTimeFormat dateTimeFormat;

		// Token: 0x04000A3F RID: 2623
		private bool useSimpleDictionaryFormat;
	}
}
