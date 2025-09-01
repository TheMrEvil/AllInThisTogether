using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x02000134 RID: 308
	internal sealed class XmlDataContract : DataContract
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D23B File Offset: 0x0003B43B
		[SecuritySafeCritical]
		internal XmlDataContract() : base(new XmlDataContract.XmlDataContractCriticalHelper())
		{
			this.helper = (base.Helper as XmlDataContract.XmlDataContractCriticalHelper);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D259 File Offset: 0x0003B459
		[SecuritySafeCritical]
		internal XmlDataContract(Type type) : base(new XmlDataContract.XmlDataContractCriticalHelper(type))
		{
			this.helper = (base.Helper as XmlDataContract.XmlDataContractCriticalHelper);
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0003D278 File Offset: 0x0003B478
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0003D285 File Offset: 0x0003B485
		internal override Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.KnownDataContracts;
			}
			[SecurityCritical]
			set
			{
				this.helper.KnownDataContracts = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003D293 File Offset: 0x0003B493
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
		internal XmlSchemaType XsdType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.XsdType;
			}
			[SecurityCritical]
			set
			{
				this.helper.XsdType = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003D2AE File Offset: 0x0003B4AE
		internal bool IsAnonymous
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsAnonymous;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003D2BB File Offset: 0x0003B4BB
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0003D2C8 File Offset: 0x0003B4C8
		internal override bool HasRoot
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.HasRoot;
			}
			[SecurityCritical]
			set
			{
				this.helper.HasRoot = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0003D2D6 File Offset: 0x0003B4D6
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0003D2E3 File Offset: 0x0003B4E3
		internal override XmlDictionaryString TopLevelElementName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TopLevelElementName;
			}
			[SecurityCritical]
			set
			{
				this.helper.TopLevelElementName = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0003D2F1 File Offset: 0x0003B4F1
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0003D2FE File Offset: 0x0003B4FE
		internal override XmlDictionaryString TopLevelElementNamespace
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TopLevelElementNamespace;
			}
			[SecurityCritical]
			set
			{
				this.helper.TopLevelElementNamespace = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003D30C File Offset: 0x0003B50C
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0003D319 File Offset: 0x0003B519
		internal bool IsTopLevelElementNullable
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsTopLevelElementNullable;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsTopLevelElementNullable = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0003D327 File Offset: 0x0003B527
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x0003D334 File Offset: 0x0003B534
		internal bool IsTypeDefinedOnImport
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsTypeDefinedOnImport;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsTypeDefinedOnImport = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0003D344 File Offset: 0x0003B544
		internal CreateXmlSerializableDelegate CreateXmlSerializableDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.CreateXmlSerializableDelegate == null)
				{
					lock (this)
					{
						if (this.helper.CreateXmlSerializableDelegate == null)
						{
							CreateXmlSerializableDelegate createXmlSerializableDelegate = this.GenerateCreateXmlSerializableDelegate();
							Thread.MemoryBarrier();
							this.helper.CreateXmlSerializableDelegate = createXmlSerializableDelegate;
						}
					}
				}
				return this.helper.CreateXmlSerializableDelegate;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool CanContainReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0003D3B8 File Offset: 0x0003B5B8
		internal override bool IsBuiltInDataContract
		{
			get
			{
				return base.UnderlyingType == Globals.TypeOfXmlElement || base.UnderlyingType == Globals.TypeOfXmlNodeArray;
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		private ConstructorInfo GetConstructor()
		{
			Type underlyingType = base.UnderlyingType;
			if (underlyingType.IsValueType)
			{
				return null;
			}
			ConstructorInfo constructor = underlyingType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
			if (constructor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("IXmlSerializable Type '{0}' must have default constructor.", new object[]
				{
					DataContract.GetClrTypeFullName(underlyingType)
				})));
			}
			return constructor;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003D43C File Offset: 0x0003B63C
		[SecurityCritical]
		internal void SetTopLevelElementName(XmlQualifiedName elementName)
		{
			if (elementName != null)
			{
				XmlDictionary xmlDictionary = new XmlDictionary();
				this.TopLevelElementName = xmlDictionary.Add(elementName.Name);
				this.TopLevelElementNamespace = xmlDictionary.Add(elementName.Namespace);
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003D47C File Offset: 0x0003B67C
		internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (base.IsEqualOrChecked(other, checkedContracts))
			{
				return true;
			}
			XmlDataContract xmlDataContract = other as XmlDataContract;
			if (xmlDataContract == null)
			{
				return false;
			}
			if (this.HasRoot != xmlDataContract.HasRoot)
			{
				return false;
			}
			if (this.IsAnonymous)
			{
				return xmlDataContract.IsAnonymous;
			}
			return base.StableName.Name == xmlDataContract.StableName.Name && base.StableName.Namespace == xmlDataContract.StableName.Namespace;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000262A8 File Offset: 0x000244A8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003D4FA File Offset: 0x0003B6FA
		public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			if (context == null)
			{
				XmlObjectSerializerWriteContext.WriteRootIXmlSerializable(xmlWriter, obj);
				return;
			}
			context.WriteIXmlSerializable(xmlWriter, obj);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003D510 File Offset: 0x0003B710
		public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
		{
			object obj;
			if (context == null)
			{
				obj = XmlObjectSerializerReadContext.ReadRootIXmlSerializable(xmlReader, this, true);
			}
			else
			{
				obj = context.ReadIXmlSerializable(xmlReader, this, true);
				context.AddNewObject(obj);
			}
			xmlReader.ReadEndElement();
			return obj;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003D543 File Offset: 0x0003B743
		internal CreateXmlSerializableDelegate GenerateCreateXmlSerializableDelegate()
		{
			return () => new XmlDataContractInterpreter(this).CreateXmlSerializable();
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003D551 File Offset: 0x0003B751
		[CompilerGenerated]
		private IXmlSerializable <GenerateCreateXmlSerializableDelegate>b__39_0()
		{
			return new XmlDataContractInterpreter(this).CreateXmlSerializable();
		}

		// Token: 0x04000691 RID: 1681
		[SecurityCritical]
		private XmlDataContract.XmlDataContractCriticalHelper helper;

		// Token: 0x02000135 RID: 309
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class XmlDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x06000F37 RID: 3895 RVA: 0x000262B0 File Offset: 0x000244B0
			internal XmlDataContractCriticalHelper()
			{
			}

			// Token: 0x06000F38 RID: 3896 RVA: 0x0003D560 File Offset: 0x0003B760
			internal XmlDataContractCriticalHelper(Type type) : base(type)
			{
				if (type.IsDefined(Globals.TypeOfDataContractAttribute, false))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot be IXmlSerializable and have DataContractAttribute attribute.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				if (type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot be IXmlSerializable and have CollectionDataContractAttribute attribute.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				XmlQualifiedName stableName;
				XmlSchemaType xmlSchemaType;
				bool flag;
				SchemaExporter.GetXmlTypeInfo(type, out stableName, out xmlSchemaType, out flag);
				base.StableName = stableName;
				this.XsdType = xmlSchemaType;
				this.HasRoot = flag;
				XmlDictionary xmlDictionary = new XmlDictionary();
				base.Name = xmlDictionary.Add(base.StableName.Name);
				base.Namespace = xmlDictionary.Add(base.StableName.Namespace);
				object[] array = (base.UnderlyingType == null) ? null : base.UnderlyingType.GetCustomAttributes(Globals.TypeOfXmlRootAttribute, false);
				if (array == null || array.Length == 0)
				{
					if (flag)
					{
						this.topLevelElementName = base.Name;
						this.topLevelElementNamespace = ((base.StableName.Namespace == "http://www.w3.org/2001/XMLSchema") ? DictionaryGlobals.EmptyString : base.Namespace);
						this.isTopLevelElementNullable = true;
						return;
					}
					return;
				}
				else
				{
					if (flag)
					{
						XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)array[0];
						this.isTopLevelElementNullable = xmlRootAttribute.IsNullable;
						string elementName = xmlRootAttribute.ElementName;
						this.topLevelElementName = ((elementName == null || elementName.Length == 0) ? base.Name : xmlDictionary.Add(DataContract.EncodeLocalName(elementName)));
						string @namespace = xmlRootAttribute.Namespace;
						this.topLevelElementNamespace = ((@namespace == null || @namespace.Length == 0) ? DictionaryGlobals.EmptyString : xmlDictionary.Add(@namespace));
						return;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot specify an XmlRootAttribute attribute because its IsAny setting is 'true'. This type must write all its contents including the root element. Verify that the IXmlSerializable implementation is correct.", new object[]
					{
						DataContract.GetClrTypeFullName(base.UnderlyingType)
					})));
				}
			}

			// Token: 0x17000357 RID: 855
			// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0003D740 File Offset: 0x0003B940
			// (set) Token: 0x06000F3A RID: 3898 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
			internal override Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
			{
				get
				{
					if (!this.isKnownTypeAttributeChecked && base.UnderlyingType != null)
					{
						lock (this)
						{
							if (!this.isKnownTypeAttributeChecked)
							{
								this.knownDataContracts = DataContract.ImportKnownTypeAttributes(base.UnderlyingType);
								Thread.MemoryBarrier();
								this.isKnownTypeAttributeChecked = true;
							}
						}
					}
					return this.knownDataContracts;
				}
				set
				{
					this.knownDataContracts = value;
				}
			}

			// Token: 0x17000358 RID: 856
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0003D7C1 File Offset: 0x0003B9C1
			// (set) Token: 0x06000F3C RID: 3900 RVA: 0x0003D7C9 File Offset: 0x0003B9C9
			internal XmlSchemaType XsdType
			{
				get
				{
					return this.xsdType;
				}
				set
				{
					this.xsdType = value;
				}
			}

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0003D7D2 File Offset: 0x0003B9D2
			internal bool IsAnonymous
			{
				get
				{
					return this.xsdType != null;
				}
			}

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003D7DD File Offset: 0x0003B9DD
			// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0003D7E5 File Offset: 0x0003B9E5
			internal override bool HasRoot
			{
				get
				{
					return this.hasRoot;
				}
				set
				{
					this.hasRoot = value;
				}
			}

			// Token: 0x1700035B RID: 859
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0003D7EE File Offset: 0x0003B9EE
			// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0003D7F6 File Offset: 0x0003B9F6
			internal override XmlDictionaryString TopLevelElementName
			{
				get
				{
					return this.topLevelElementName;
				}
				set
				{
					this.topLevelElementName = value;
				}
			}

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0003D7FF File Offset: 0x0003B9FF
			// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0003D807 File Offset: 0x0003BA07
			internal override XmlDictionaryString TopLevelElementNamespace
			{
				get
				{
					return this.topLevelElementNamespace;
				}
				set
				{
					this.topLevelElementNamespace = value;
				}
			}

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0003D810 File Offset: 0x0003BA10
			// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0003D818 File Offset: 0x0003BA18
			internal bool IsTopLevelElementNullable
			{
				get
				{
					return this.isTopLevelElementNullable;
				}
				set
				{
					this.isTopLevelElementNullable = value;
				}
			}

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0003D821 File Offset: 0x0003BA21
			// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0003D829 File Offset: 0x0003BA29
			internal bool IsTypeDefinedOnImport
			{
				get
				{
					return this.isTypeDefinedOnImport;
				}
				set
				{
					this.isTypeDefinedOnImport = value;
				}
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003D832 File Offset: 0x0003BA32
			// (set) Token: 0x06000F49 RID: 3913 RVA: 0x0003D83A File Offset: 0x0003BA3A
			internal CreateXmlSerializableDelegate CreateXmlSerializableDelegate
			{
				get
				{
					return this.createXmlSerializable;
				}
				set
				{
					this.createXmlSerializable = value;
				}
			}

			// Token: 0x04000692 RID: 1682
			private Dictionary<XmlQualifiedName, DataContract> knownDataContracts;

			// Token: 0x04000693 RID: 1683
			private bool isKnownTypeAttributeChecked;

			// Token: 0x04000694 RID: 1684
			private XmlDictionaryString topLevelElementName;

			// Token: 0x04000695 RID: 1685
			private XmlDictionaryString topLevelElementNamespace;

			// Token: 0x04000696 RID: 1686
			private bool isTopLevelElementNullable;

			// Token: 0x04000697 RID: 1687
			private bool isTypeDefinedOnImport;

			// Token: 0x04000698 RID: 1688
			private XmlSchemaType xsdType;

			// Token: 0x04000699 RID: 1689
			private bool hasRoot;

			// Token: 0x0400069A RID: 1690
			private CreateXmlSerializableDelegate createXmlSerializable;
		}
	}
}
