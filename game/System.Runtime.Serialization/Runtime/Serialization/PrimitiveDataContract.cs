using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F1 RID: 241
	internal abstract class PrimitiveDataContract : DataContract
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00036B79 File Offset: 0x00034D79
		[SecuritySafeCritical]
		protected PrimitiveDataContract(Type type, XmlDictionaryString name, XmlDictionaryString ns) : base(new PrimitiveDataContract.PrimitiveDataContractCriticalHelper(type, name, ns))
		{
			this.helper = (base.Helper as PrimitiveDataContract.PrimitiveDataContractCriticalHelper);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00036B9A File Offset: 0x00034D9A
		internal static PrimitiveDataContract GetPrimitiveDataContract(Type type)
		{
			return DataContract.GetBuiltInDataContract(type) as PrimitiveDataContract;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00036BA7 File Offset: 0x00034DA7
		internal static PrimitiveDataContract GetPrimitiveDataContract(string name, string ns)
		{
			return DataContract.GetBuiltInDataContract(name, ns) as PrimitiveDataContract;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000DCB RID: 3531
		internal abstract string WriteMethodName { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000DCC RID: 3532
		internal abstract string ReadMethodName { get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00036BB5 File Offset: 0x00034DB5
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal override XmlDictionaryString TopLevelElementNamespace
		{
			get
			{
				return DictionaryGlobals.SerializationNamespace;
			}
			set
			{
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool CanContainReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x000066E8 File Offset: 0x000048E8
		internal override bool IsPrimitive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000066E8 File Offset: 0x000048E8
		internal override bool IsBuiltInDataContract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00036BBC File Offset: 0x00034DBC
		internal MethodInfo XmlFormatWriterMethod
		{
			[PreserveDependency("WriteDouble", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteFloat", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteBoolean", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteAnyType", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteBase64", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteString", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteQName", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteGuid", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteTimeSpan", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUri", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteLong", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteAnyType", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteBase64", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteString", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteDateTime", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteDecimal", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteChar", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedLong", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteQName", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteUri", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteInt", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedInt", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteShort", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedByte", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteSignedByte", "System.Runtime.Serialization.XmlWriterDelegator")]
			[SecuritySafeCritical]
			[PreserveDependency("WriteUnsignedShort", "System.Runtime.Serialization.XmlWriterDelegator")]
			get
			{
				if (this.helper.XmlFormatWriterMethod == null)
				{
					if (base.UnderlyingType.IsValueType)
					{
						this.helper.XmlFormatWriterMethod = typeof(XmlWriterDelegator).GetMethod(this.WriteMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
						{
							base.UnderlyingType,
							typeof(XmlDictionaryString),
							typeof(XmlDictionaryString)
						}, null);
					}
					else
					{
						this.helper.XmlFormatWriterMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(this.WriteMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
						{
							typeof(XmlWriterDelegator),
							base.UnderlyingType,
							typeof(XmlDictionaryString),
							typeof(XmlDictionaryString)
						}, null);
					}
				}
				return this.helper.XmlFormatWriterMethod;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00036CA0 File Offset: 0x00034EA0
		internal MethodInfo XmlFormatContentWriterMethod
		{
			[PreserveDependency("WriteTimeSpan", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUri", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteChar", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteQName", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteBase64", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteString", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteBase64", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteAnyType", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteGuid", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteAnyType", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteDouble", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteDateTime", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteDecimal", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteSignedByte", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedByte", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteShort", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedShort", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUri", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteInt", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedInt", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteLong", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteUnsignedLong", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteFloat", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteString", "System.Runtime.Serialization.XmlWriterDelegator")]
			[PreserveDependency("WriteQName", "System.Runtime.Serialization.XmlObjectSerializerWriteContext")]
			[PreserveDependency("WriteBoolean", "System.Runtime.Serialization.XmlWriterDelegator")]
			[SecuritySafeCritical]
			get
			{
				if (this.helper.XmlFormatContentWriterMethod == null)
				{
					if (base.UnderlyingType.IsValueType)
					{
						this.helper.XmlFormatContentWriterMethod = typeof(XmlWriterDelegator).GetMethod(this.WriteMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
						{
							base.UnderlyingType
						}, null);
					}
					else
					{
						this.helper.XmlFormatContentWriterMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(this.WriteMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
						{
							typeof(XmlWriterDelegator),
							base.UnderlyingType
						}, null);
					}
				}
				return this.helper.XmlFormatContentWriterMethod;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00036D50 File Offset: 0x00034F50
		internal MethodInfo XmlFormatReaderMethod
		{
			[PreserveDependency("ReadElementContentAsChar", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsDecimal", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsDateTime", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsString", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsBase64", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsAnyType", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsTimeSpan", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsGuid", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsUri", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsQName", "System.Runtime.Serialization.XmlReaderDelegator")]
			[SecuritySafeCritical]
			[PreserveDependency("ReadElementContentAsFloat", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsDouble", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsUnsignedLong", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsLong", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsUnsignedInt", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsInt", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsUnsignedShort", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsShort", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsUnsignedByte", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsSignedByte", "System.Runtime.Serialization.XmlReaderDelegator")]
			[PreserveDependency("ReadElementContentAsBoolean", "System.Runtime.Serialization.XmlReaderDelegator")]
			get
			{
				if (this.helper.XmlFormatReaderMethod == null)
				{
					this.helper.XmlFormatReaderMethod = typeof(XmlReaderDelegator).GetMethod(this.ReadMethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return this.helper.XmlFormatReaderMethod;
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00036D9D File Offset: 0x00034F9D
		public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			xmlWriter.WriteAnyType(obj);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00036DA6 File Offset: 0x00034FA6
		protected object HandleReadValue(object obj, XmlObjectSerializerReadContext context)
		{
			context.AddNewObject(obj);
			return obj;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00036DB0 File Offset: 0x00034FB0
		protected bool TryReadNullAtTopLevel(XmlReaderDelegator reader)
		{
			Attributes attributes = new Attributes();
			attributes.Read(reader);
			if (attributes.Ref != Globals.NewObjectId)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Cannot deserialize since root element references unrecognized object with id '{0}'.", new object[]
				{
					attributes.Ref
				})));
			}
			if (attributes.XsiNil)
			{
				reader.Skip();
				return true;
			}
			return false;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00036E14 File Offset: 0x00035014
		internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (other is PrimitiveDataContract)
			{
				Type type = base.GetType();
				Type type2 = other.GetType();
				return type.Equals(type2) || type.IsSubclassOf(type2) || type2.IsSubclassOf(type);
			}
			return false;
		}

		// Token: 0x0400065B RID: 1627
		[SecurityCritical]
		private PrimitiveDataContract.PrimitiveDataContractCriticalHelper helper;

		// Token: 0x020000F2 RID: 242
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class PrimitiveDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x06000DD9 RID: 3545 RVA: 0x00036E54 File Offset: 0x00035054
			internal PrimitiveDataContractCriticalHelper(Type type, XmlDictionaryString name, XmlDictionaryString ns) : base(type)
			{
				base.SetDataContractName(name, ns);
			}

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00036E65 File Offset: 0x00035065
			// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00036E6D File Offset: 0x0003506D
			internal MethodInfo XmlFormatWriterMethod
			{
				get
				{
					return this.xmlFormatWriterMethod;
				}
				set
				{
					this.xmlFormatWriterMethod = value;
				}
			}

			// Token: 0x170002FF RID: 767
			// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00036E76 File Offset: 0x00035076
			// (set) Token: 0x06000DDD RID: 3549 RVA: 0x00036E7E File Offset: 0x0003507E
			internal MethodInfo XmlFormatContentWriterMethod
			{
				get
				{
					return this.xmlFormatContentWriterMethod;
				}
				set
				{
					this.xmlFormatContentWriterMethod = value;
				}
			}

			// Token: 0x17000300 RID: 768
			// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00036E87 File Offset: 0x00035087
			// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00036E8F File Offset: 0x0003508F
			internal MethodInfo XmlFormatReaderMethod
			{
				get
				{
					return this.xmlFormatReaderMethod;
				}
				set
				{
					this.xmlFormatReaderMethod = value;
				}
			}

			// Token: 0x0400065C RID: 1628
			private MethodInfo xmlFormatWriterMethod;

			// Token: 0x0400065D RID: 1629
			private MethodInfo xmlFormatContentWriterMethod;

			// Token: 0x0400065E RID: 1630
			private MethodInfo xmlFormatReaderMethod;
		}
	}
}
