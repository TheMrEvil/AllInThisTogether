using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x020000E3 RID: 227
	internal static class Globals
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00034692 File Offset: 0x00032892
		internal static XmlQualifiedName IdQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.idQualifiedName == null)
				{
					Globals.idQualifiedName = new XmlQualifiedName("Id", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return Globals.idQualifiedName;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x000346BA File Offset: 0x000328BA
		internal static XmlQualifiedName RefQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.refQualifiedName == null)
				{
					Globals.refQualifiedName = new XmlQualifiedName("Ref", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return Globals.refQualifiedName;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x000346E2 File Offset: 0x000328E2
		internal static Type TypeOfObject
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfObject == null)
				{
					Globals.typeOfObject = typeof(object);
				}
				return Globals.typeOfObject;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00034705 File Offset: 0x00032905
		internal static Type TypeOfValueType
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfValueType == null)
				{
					Globals.typeOfValueType = typeof(ValueType);
				}
				return Globals.typeOfValueType;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00034728 File Offset: 0x00032928
		internal static Type TypeOfArray
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfArray == null)
				{
					Globals.typeOfArray = typeof(Array);
				}
				return Globals.typeOfArray;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0003474B File Offset: 0x0003294B
		internal static Type TypeOfString
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfString == null)
				{
					Globals.typeOfString = typeof(string);
				}
				return Globals.typeOfString;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003476E File Offset: 0x0003296E
		internal static Type TypeOfInt
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfInt == null)
				{
					Globals.typeOfInt = typeof(int);
				}
				return Globals.typeOfInt;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00034791 File Offset: 0x00032991
		internal static Type TypeOfULong
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfULong == null)
				{
					Globals.typeOfULong = typeof(ulong);
				}
				return Globals.typeOfULong;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000347B4 File Offset: 0x000329B4
		internal static Type TypeOfVoid
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfVoid == null)
				{
					Globals.typeOfVoid = typeof(void);
				}
				return Globals.typeOfVoid;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000347D7 File Offset: 0x000329D7
		internal static Type TypeOfByteArray
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfByteArray == null)
				{
					Globals.typeOfByteArray = typeof(byte[]);
				}
				return Globals.typeOfByteArray;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000347FA File Offset: 0x000329FA
		internal static Type TypeOfTimeSpan
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfTimeSpan == null)
				{
					Globals.typeOfTimeSpan = typeof(TimeSpan);
				}
				return Globals.typeOfTimeSpan;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0003481D File Offset: 0x00032A1D
		internal static Type TypeOfGuid
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfGuid == null)
				{
					Globals.typeOfGuid = typeof(Guid);
				}
				return Globals.typeOfGuid;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00034840 File Offset: 0x00032A40
		internal static Type TypeOfDateTimeOffset
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDateTimeOffset == null)
				{
					Globals.typeOfDateTimeOffset = typeof(DateTimeOffset);
				}
				return Globals.typeOfDateTimeOffset;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00034863 File Offset: 0x00032A63
		internal static Type TypeOfDateTimeOffsetAdapter
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDateTimeOffsetAdapter == null)
				{
					Globals.typeOfDateTimeOffsetAdapter = typeof(DateTimeOffsetAdapter);
				}
				return Globals.typeOfDateTimeOffsetAdapter;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00034886 File Offset: 0x00032A86
		internal static Type TypeOfUri
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfUri == null)
				{
					Globals.typeOfUri = typeof(Uri);
				}
				return Globals.typeOfUri;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x000348A9 File Offset: 0x00032AA9
		internal static Type TypeOfTypeEnumerable
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfTypeEnumerable == null)
				{
					Globals.typeOfTypeEnumerable = typeof(IEnumerable<Type>);
				}
				return Globals.typeOfTypeEnumerable;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000348CC File Offset: 0x00032ACC
		internal static Type TypeOfStreamingContext
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfStreamingContext == null)
				{
					Globals.typeOfStreamingContext = typeof(StreamingContext);
				}
				return Globals.typeOfStreamingContext;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000348EF File Offset: 0x00032AEF
		internal static Type TypeOfISerializable
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfISerializable == null)
				{
					Globals.typeOfISerializable = typeof(ISerializable);
				}
				return Globals.typeOfISerializable;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00034912 File Offset: 0x00032B12
		internal static Type TypeOfIDeserializationCallback
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIDeserializationCallback == null)
				{
					Globals.typeOfIDeserializationCallback = typeof(IDeserializationCallback);
				}
				return Globals.typeOfIDeserializationCallback;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00034935 File Offset: 0x00032B35
		internal static Type TypeOfIObjectReference
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIObjectReference == null)
				{
					Globals.typeOfIObjectReference = typeof(IObjectReference);
				}
				return Globals.typeOfIObjectReference;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00034958 File Offset: 0x00032B58
		internal static Type TypeOfXmlFormatClassWriterDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlFormatClassWriterDelegate == null)
				{
					Globals.typeOfXmlFormatClassWriterDelegate = typeof(XmlFormatClassWriterDelegate);
				}
				return Globals.typeOfXmlFormatClassWriterDelegate;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0003497B File Offset: 0x00032B7B
		internal static Type TypeOfXmlFormatCollectionWriterDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlFormatCollectionWriterDelegate == null)
				{
					Globals.typeOfXmlFormatCollectionWriterDelegate = typeof(XmlFormatCollectionWriterDelegate);
				}
				return Globals.typeOfXmlFormatCollectionWriterDelegate;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0003499E File Offset: 0x00032B9E
		internal static Type TypeOfXmlFormatClassReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlFormatClassReaderDelegate == null)
				{
					Globals.typeOfXmlFormatClassReaderDelegate = typeof(XmlFormatClassReaderDelegate);
				}
				return Globals.typeOfXmlFormatClassReaderDelegate;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000349C1 File Offset: 0x00032BC1
		internal static Type TypeOfXmlFormatCollectionReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlFormatCollectionReaderDelegate == null)
				{
					Globals.typeOfXmlFormatCollectionReaderDelegate = typeof(XmlFormatCollectionReaderDelegate);
				}
				return Globals.typeOfXmlFormatCollectionReaderDelegate;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x000349E4 File Offset: 0x00032BE4
		internal static Type TypeOfXmlFormatGetOnlyCollectionReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlFormatGetOnlyCollectionReaderDelegate == null)
				{
					Globals.typeOfXmlFormatGetOnlyCollectionReaderDelegate = typeof(XmlFormatGetOnlyCollectionReaderDelegate);
				}
				return Globals.typeOfXmlFormatGetOnlyCollectionReaderDelegate;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00034A07 File Offset: 0x00032C07
		internal static Type TypeOfKnownTypeAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfKnownTypeAttribute == null)
				{
					Globals.typeOfKnownTypeAttribute = typeof(KnownTypeAttribute);
				}
				return Globals.typeOfKnownTypeAttribute;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00034A2A File Offset: 0x00032C2A
		internal static Type TypeOfDataContractAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDataContractAttribute == null)
				{
					Globals.typeOfDataContractAttribute = typeof(DataContractAttribute);
				}
				return Globals.typeOfDataContractAttribute;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00034A4D File Offset: 0x00032C4D
		internal static Type TypeOfContractNamespaceAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfContractNamespaceAttribute == null)
				{
					Globals.typeOfContractNamespaceAttribute = typeof(ContractNamespaceAttribute);
				}
				return Globals.typeOfContractNamespaceAttribute;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00034A70 File Offset: 0x00032C70
		internal static Type TypeOfDataMemberAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDataMemberAttribute == null)
				{
					Globals.typeOfDataMemberAttribute = typeof(DataMemberAttribute);
				}
				return Globals.typeOfDataMemberAttribute;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00034A93 File Offset: 0x00032C93
		internal static Type TypeOfEnumMemberAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfEnumMemberAttribute == null)
				{
					Globals.typeOfEnumMemberAttribute = typeof(EnumMemberAttribute);
				}
				return Globals.typeOfEnumMemberAttribute;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00034AB6 File Offset: 0x00032CB6
		internal static Type TypeOfCollectionDataContractAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfCollectionDataContractAttribute == null)
				{
					Globals.typeOfCollectionDataContractAttribute = typeof(CollectionDataContractAttribute);
				}
				return Globals.typeOfCollectionDataContractAttribute;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00034AD9 File Offset: 0x00032CD9
		internal static Type TypeOfOptionalFieldAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfOptionalFieldAttribute == null)
				{
					Globals.typeOfOptionalFieldAttribute = typeof(OptionalFieldAttribute);
				}
				return Globals.typeOfOptionalFieldAttribute;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00034AFC File Offset: 0x00032CFC
		internal static Type TypeOfObjectArray
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfObjectArray == null)
				{
					Globals.typeOfObjectArray = typeof(object[]);
				}
				return Globals.typeOfObjectArray;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00034B1F File Offset: 0x00032D1F
		internal static Type TypeOfOnSerializingAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfOnSerializingAttribute == null)
				{
					Globals.typeOfOnSerializingAttribute = typeof(OnSerializingAttribute);
				}
				return Globals.typeOfOnSerializingAttribute;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00034B42 File Offset: 0x00032D42
		internal static Type TypeOfOnSerializedAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfOnSerializedAttribute == null)
				{
					Globals.typeOfOnSerializedAttribute = typeof(OnSerializedAttribute);
				}
				return Globals.typeOfOnSerializedAttribute;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00034B65 File Offset: 0x00032D65
		internal static Type TypeOfOnDeserializingAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfOnDeserializingAttribute == null)
				{
					Globals.typeOfOnDeserializingAttribute = typeof(OnDeserializingAttribute);
				}
				return Globals.typeOfOnDeserializingAttribute;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00034B88 File Offset: 0x00032D88
		internal static Type TypeOfOnDeserializedAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfOnDeserializedAttribute == null)
				{
					Globals.typeOfOnDeserializedAttribute = typeof(OnDeserializedAttribute);
				}
				return Globals.typeOfOnDeserializedAttribute;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00034BAB File Offset: 0x00032DAB
		internal static Type TypeOfFlagsAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfFlagsAttribute == null)
				{
					Globals.typeOfFlagsAttribute = typeof(FlagsAttribute);
				}
				return Globals.typeOfFlagsAttribute;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00034BCE File Offset: 0x00032DCE
		internal static Type TypeOfSerializableAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfSerializableAttribute == null)
				{
					Globals.typeOfSerializableAttribute = typeof(SerializableAttribute);
				}
				return Globals.typeOfSerializableAttribute;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00034BF1 File Offset: 0x00032DF1
		internal static Type TypeOfNonSerializedAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfNonSerializedAttribute == null)
				{
					Globals.typeOfNonSerializedAttribute = typeof(NonSerializedAttribute);
				}
				return Globals.typeOfNonSerializedAttribute;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00034C14 File Offset: 0x00032E14
		internal static Type TypeOfSerializationInfo
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfSerializationInfo == null)
				{
					Globals.typeOfSerializationInfo = typeof(SerializationInfo);
				}
				return Globals.typeOfSerializationInfo;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00034C37 File Offset: 0x00032E37
		internal static Type TypeOfSerializationInfoEnumerator
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfSerializationInfoEnumerator == null)
				{
					Globals.typeOfSerializationInfoEnumerator = typeof(SerializationInfoEnumerator);
				}
				return Globals.typeOfSerializationInfoEnumerator;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00034C5A File Offset: 0x00032E5A
		internal static Type TypeOfSerializationEntry
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfSerializationEntry == null)
				{
					Globals.typeOfSerializationEntry = typeof(SerializationEntry);
				}
				return Globals.typeOfSerializationEntry;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00034C7D File Offset: 0x00032E7D
		internal static Type TypeOfIXmlSerializable
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIXmlSerializable == null)
				{
					Globals.typeOfIXmlSerializable = typeof(IXmlSerializable);
				}
				return Globals.typeOfIXmlSerializable;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00034CA0 File Offset: 0x00032EA0
		internal static Type TypeOfXmlSchemaProviderAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlSchemaProviderAttribute == null)
				{
					Globals.typeOfXmlSchemaProviderAttribute = typeof(XmlSchemaProviderAttribute);
				}
				return Globals.typeOfXmlSchemaProviderAttribute;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00034CC3 File Offset: 0x00032EC3
		internal static Type TypeOfXmlRootAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlRootAttribute == null)
				{
					Globals.typeOfXmlRootAttribute = typeof(XmlRootAttribute);
				}
				return Globals.typeOfXmlRootAttribute;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00034CE6 File Offset: 0x00032EE6
		internal static Type TypeOfXmlQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlQualifiedName == null)
				{
					Globals.typeOfXmlQualifiedName = typeof(XmlQualifiedName);
				}
				return Globals.typeOfXmlQualifiedName;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00034D09 File Offset: 0x00032F09
		internal static Type TypeOfXmlSchemaType
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlSchemaType == null)
				{
					Globals.typeOfXmlSchemaType = typeof(XmlSchemaType);
				}
				return Globals.typeOfXmlSchemaType;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00034D2C File Offset: 0x00032F2C
		internal static Type TypeOfXmlSerializableServices
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlSerializableServices == null)
				{
					Globals.typeOfXmlSerializableServices = typeof(XmlSerializableServices);
				}
				return Globals.typeOfXmlSerializableServices;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00034D4F File Offset: 0x00032F4F
		internal static Type TypeOfXmlNodeArray
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlNodeArray == null)
				{
					Globals.typeOfXmlNodeArray = typeof(XmlNode[]);
				}
				return Globals.typeOfXmlNodeArray;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00034D72 File Offset: 0x00032F72
		internal static Type TypeOfXmlSchemaSet
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlSchemaSet == null)
				{
					Globals.typeOfXmlSchemaSet = typeof(XmlSchemaSet);
				}
				return Globals.typeOfXmlSchemaSet;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00034D95 File Offset: 0x00032F95
		internal static object[] EmptyObjectArray
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.emptyObjectArray == null)
				{
					Globals.emptyObjectArray = new object[0];
				}
				return Globals.emptyObjectArray;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00034DAE File Offset: 0x00032FAE
		internal static Type[] EmptyTypeArray
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.emptyTypeArray == null)
				{
					Globals.emptyTypeArray = new Type[0];
				}
				return Globals.emptyTypeArray;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00034DC7 File Offset: 0x00032FC7
		internal static Type TypeOfIPropertyChange
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIPropertyChange == null)
				{
					Globals.typeOfIPropertyChange = typeof(INotifyPropertyChanged);
				}
				return Globals.typeOfIPropertyChange;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00034DEA File Offset: 0x00032FEA
		internal static Type TypeOfIExtensibleDataObject
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIExtensibleDataObject == null)
				{
					Globals.typeOfIExtensibleDataObject = typeof(IExtensibleDataObject);
				}
				return Globals.typeOfIExtensibleDataObject;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00034E0D File Offset: 0x0003300D
		internal static Type TypeOfExtensionDataObject
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfExtensionDataObject == null)
				{
					Globals.typeOfExtensionDataObject = typeof(ExtensionDataObject);
				}
				return Globals.typeOfExtensionDataObject;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00034E30 File Offset: 0x00033030
		internal static Type TypeOfISerializableDataNode
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfISerializableDataNode == null)
				{
					Globals.typeOfISerializableDataNode = typeof(ISerializableDataNode);
				}
				return Globals.typeOfISerializableDataNode;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00034E53 File Offset: 0x00033053
		internal static Type TypeOfClassDataNode
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfClassDataNode == null)
				{
					Globals.typeOfClassDataNode = typeof(ClassDataNode);
				}
				return Globals.typeOfClassDataNode;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00034E76 File Offset: 0x00033076
		internal static Type TypeOfCollectionDataNode
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfCollectionDataNode == null)
				{
					Globals.typeOfCollectionDataNode = typeof(CollectionDataNode);
				}
				return Globals.typeOfCollectionDataNode;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00034E99 File Offset: 0x00033099
		internal static Type TypeOfXmlDataNode
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlDataNode == null)
				{
					Globals.typeOfXmlDataNode = typeof(XmlDataNode);
				}
				return Globals.typeOfXmlDataNode;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00034EBC File Offset: 0x000330BC
		internal static Type TypeOfNullable
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfNullable == null)
				{
					Globals.typeOfNullable = typeof(Nullable<>);
				}
				return Globals.typeOfNullable;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00034EDF File Offset: 0x000330DF
		internal static Type TypeOfReflectionPointer
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfReflectionPointer == null)
				{
					Globals.typeOfReflectionPointer = typeof(Pointer);
				}
				return Globals.typeOfReflectionPointer;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00034F02 File Offset: 0x00033102
		internal static Type TypeOfIDictionaryGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIDictionaryGeneric == null)
				{
					Globals.typeOfIDictionaryGeneric = typeof(IDictionary<, >);
				}
				return Globals.typeOfIDictionaryGeneric;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00034F25 File Offset: 0x00033125
		internal static Type TypeOfIDictionary
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIDictionary == null)
				{
					Globals.typeOfIDictionary = typeof(IDictionary);
				}
				return Globals.typeOfIDictionary;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00034F48 File Offset: 0x00033148
		internal static Type TypeOfIListGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIListGeneric == null)
				{
					Globals.typeOfIListGeneric = typeof(IList<>);
				}
				return Globals.typeOfIListGeneric;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00034F6B File Offset: 0x0003316B
		internal static Type TypeOfIList
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIList == null)
				{
					Globals.typeOfIList = typeof(IList);
				}
				return Globals.typeOfIList;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00034F8E File Offset: 0x0003318E
		internal static Type TypeOfICollectionGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfICollectionGeneric == null)
				{
					Globals.typeOfICollectionGeneric = typeof(ICollection<>);
				}
				return Globals.typeOfICollectionGeneric;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00034FB1 File Offset: 0x000331B1
		internal static Type TypeOfICollection
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfICollection == null)
				{
					Globals.typeOfICollection = typeof(ICollection);
				}
				return Globals.typeOfICollection;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00034FD4 File Offset: 0x000331D4
		internal static Type TypeOfIEnumerableGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIEnumerableGeneric == null)
				{
					Globals.typeOfIEnumerableGeneric = typeof(IEnumerable<>);
				}
				return Globals.typeOfIEnumerableGeneric;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00034FF7 File Offset: 0x000331F7
		internal static Type TypeOfIEnumerable
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIEnumerable == null)
				{
					Globals.typeOfIEnumerable = typeof(IEnumerable);
				}
				return Globals.typeOfIEnumerable;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0003501A File Offset: 0x0003321A
		internal static Type TypeOfIEnumeratorGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIEnumeratorGeneric == null)
				{
					Globals.typeOfIEnumeratorGeneric = typeof(IEnumerator<>);
				}
				return Globals.typeOfIEnumeratorGeneric;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0003503D File Offset: 0x0003323D
		internal static Type TypeOfIEnumerator
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIEnumerator == null)
				{
					Globals.typeOfIEnumerator = typeof(IEnumerator);
				}
				return Globals.typeOfIEnumerator;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00035060 File Offset: 0x00033260
		internal static Type TypeOfKeyValuePair
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfKeyValuePair == null)
				{
					Globals.typeOfKeyValuePair = typeof(KeyValuePair<, >);
				}
				return Globals.typeOfKeyValuePair;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00035083 File Offset: 0x00033283
		internal static Type TypeOfKeyValue
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfKeyValue == null)
				{
					Globals.typeOfKeyValue = typeof(KeyValue<, >);
				}
				return Globals.typeOfKeyValue;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000350A6 File Offset: 0x000332A6
		internal static Type TypeOfIDictionaryEnumerator
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfIDictionaryEnumerator == null)
				{
					Globals.typeOfIDictionaryEnumerator = typeof(IDictionaryEnumerator);
				}
				return Globals.typeOfIDictionaryEnumerator;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x000350C9 File Offset: 0x000332C9
		internal static Type TypeOfDictionaryEnumerator
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDictionaryEnumerator == null)
				{
					Globals.typeOfDictionaryEnumerator = typeof(CollectionDataContract.DictionaryEnumerator);
				}
				return Globals.typeOfDictionaryEnumerator;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x000350EC File Offset: 0x000332EC
		internal static Type TypeOfGenericDictionaryEnumerator
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfGenericDictionaryEnumerator == null)
				{
					Globals.typeOfGenericDictionaryEnumerator = typeof(CollectionDataContract.GenericDictionaryEnumerator<, >);
				}
				return Globals.typeOfGenericDictionaryEnumerator;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0003510F File Offset: 0x0003330F
		internal static Type TypeOfDictionaryGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDictionaryGeneric == null)
				{
					Globals.typeOfDictionaryGeneric = typeof(Dictionary<, >);
				}
				return Globals.typeOfDictionaryGeneric;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00035132 File Offset: 0x00033332
		internal static Type TypeOfHashtable
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfHashtable == null)
				{
					Globals.typeOfHashtable = typeof(Hashtable);
				}
				return Globals.typeOfHashtable;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00035155 File Offset: 0x00033355
		internal static Type TypeOfListGeneric
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfListGeneric == null)
				{
					Globals.typeOfListGeneric = typeof(List<>);
				}
				return Globals.typeOfListGeneric;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00035178 File Offset: 0x00033378
		internal static Type TypeOfXmlElement
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfXmlElement == null)
				{
					Globals.typeOfXmlElement = typeof(XmlElement);
				}
				return Globals.typeOfXmlElement;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0003519B File Offset: 0x0003339B
		internal static Type TypeOfDBNull
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.typeOfDBNull == null)
				{
					Globals.typeOfDBNull = typeof(DBNull);
				}
				return Globals.typeOfDBNull;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x000351BE File Offset: 0x000333BE
		internal static Uri DataContractXsdBaseNamespaceUri
		{
			[SecuritySafeCritical]
			get
			{
				if (Globals.dataContractXsdBaseNamespaceUri == null)
				{
					Globals.dataContractXsdBaseNamespaceUri = new Uri("http://schemas.datacontract.org/2004/07/");
				}
				return Globals.dataContractXsdBaseNamespaceUri;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000351E1 File Offset: 0x000333E1
		// Note: this type is marked as 'beforefieldinit'.
		static Globals()
		{
		}

		// Token: 0x0400055B RID: 1371
		internal const BindingFlags ScanAllMembers = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400055C RID: 1372
		[SecurityCritical]
		private static XmlQualifiedName idQualifiedName;

		// Token: 0x0400055D RID: 1373
		[SecurityCritical]
		private static XmlQualifiedName refQualifiedName;

		// Token: 0x0400055E RID: 1374
		[SecurityCritical]
		private static Type typeOfObject;

		// Token: 0x0400055F RID: 1375
		[SecurityCritical]
		private static Type typeOfValueType;

		// Token: 0x04000560 RID: 1376
		[SecurityCritical]
		private static Type typeOfArray;

		// Token: 0x04000561 RID: 1377
		[SecurityCritical]
		private static Type typeOfString;

		// Token: 0x04000562 RID: 1378
		[SecurityCritical]
		private static Type typeOfInt;

		// Token: 0x04000563 RID: 1379
		[SecurityCritical]
		private static Type typeOfULong;

		// Token: 0x04000564 RID: 1380
		[SecurityCritical]
		private static Type typeOfVoid;

		// Token: 0x04000565 RID: 1381
		[SecurityCritical]
		private static Type typeOfByteArray;

		// Token: 0x04000566 RID: 1382
		[SecurityCritical]
		private static Type typeOfTimeSpan;

		// Token: 0x04000567 RID: 1383
		[SecurityCritical]
		private static Type typeOfGuid;

		// Token: 0x04000568 RID: 1384
		[SecurityCritical]
		private static Type typeOfDateTimeOffset;

		// Token: 0x04000569 RID: 1385
		[SecurityCritical]
		private static Type typeOfDateTimeOffsetAdapter;

		// Token: 0x0400056A RID: 1386
		[SecurityCritical]
		private static Type typeOfUri;

		// Token: 0x0400056B RID: 1387
		[SecurityCritical]
		private static Type typeOfTypeEnumerable;

		// Token: 0x0400056C RID: 1388
		[SecurityCritical]
		private static Type typeOfStreamingContext;

		// Token: 0x0400056D RID: 1389
		[SecurityCritical]
		private static Type typeOfISerializable;

		// Token: 0x0400056E RID: 1390
		[SecurityCritical]
		private static Type typeOfIDeserializationCallback;

		// Token: 0x0400056F RID: 1391
		[SecurityCritical]
		private static Type typeOfIObjectReference;

		// Token: 0x04000570 RID: 1392
		[SecurityCritical]
		private static Type typeOfXmlFormatClassWriterDelegate;

		// Token: 0x04000571 RID: 1393
		[SecurityCritical]
		private static Type typeOfXmlFormatCollectionWriterDelegate;

		// Token: 0x04000572 RID: 1394
		[SecurityCritical]
		private static Type typeOfXmlFormatClassReaderDelegate;

		// Token: 0x04000573 RID: 1395
		[SecurityCritical]
		private static Type typeOfXmlFormatCollectionReaderDelegate;

		// Token: 0x04000574 RID: 1396
		[SecurityCritical]
		private static Type typeOfXmlFormatGetOnlyCollectionReaderDelegate;

		// Token: 0x04000575 RID: 1397
		[SecurityCritical]
		private static Type typeOfKnownTypeAttribute;

		// Token: 0x04000576 RID: 1398
		[SecurityCritical]
		private static Type typeOfDataContractAttribute;

		// Token: 0x04000577 RID: 1399
		[SecurityCritical]
		private static Type typeOfContractNamespaceAttribute;

		// Token: 0x04000578 RID: 1400
		[SecurityCritical]
		private static Type typeOfDataMemberAttribute;

		// Token: 0x04000579 RID: 1401
		[SecurityCritical]
		private static Type typeOfEnumMemberAttribute;

		// Token: 0x0400057A RID: 1402
		[SecurityCritical]
		private static Type typeOfCollectionDataContractAttribute;

		// Token: 0x0400057B RID: 1403
		[SecurityCritical]
		private static Type typeOfOptionalFieldAttribute;

		// Token: 0x0400057C RID: 1404
		[SecurityCritical]
		private static Type typeOfObjectArray;

		// Token: 0x0400057D RID: 1405
		[SecurityCritical]
		private static Type typeOfOnSerializingAttribute;

		// Token: 0x0400057E RID: 1406
		[SecurityCritical]
		private static Type typeOfOnSerializedAttribute;

		// Token: 0x0400057F RID: 1407
		[SecurityCritical]
		private static Type typeOfOnDeserializingAttribute;

		// Token: 0x04000580 RID: 1408
		[SecurityCritical]
		private static Type typeOfOnDeserializedAttribute;

		// Token: 0x04000581 RID: 1409
		[SecurityCritical]
		private static Type typeOfFlagsAttribute;

		// Token: 0x04000582 RID: 1410
		[SecurityCritical]
		private static Type typeOfSerializableAttribute;

		// Token: 0x04000583 RID: 1411
		[SecurityCritical]
		private static Type typeOfNonSerializedAttribute;

		// Token: 0x04000584 RID: 1412
		[SecurityCritical]
		private static Type typeOfSerializationInfo;

		// Token: 0x04000585 RID: 1413
		[SecurityCritical]
		private static Type typeOfSerializationInfoEnumerator;

		// Token: 0x04000586 RID: 1414
		[SecurityCritical]
		private static Type typeOfSerializationEntry;

		// Token: 0x04000587 RID: 1415
		[SecurityCritical]
		private static Type typeOfIXmlSerializable;

		// Token: 0x04000588 RID: 1416
		[SecurityCritical]
		private static Type typeOfXmlSchemaProviderAttribute;

		// Token: 0x04000589 RID: 1417
		[SecurityCritical]
		private static Type typeOfXmlRootAttribute;

		// Token: 0x0400058A RID: 1418
		[SecurityCritical]
		private static Type typeOfXmlQualifiedName;

		// Token: 0x0400058B RID: 1419
		[SecurityCritical]
		private static Type typeOfXmlSchemaType;

		// Token: 0x0400058C RID: 1420
		[SecurityCritical]
		private static Type typeOfXmlSerializableServices;

		// Token: 0x0400058D RID: 1421
		[SecurityCritical]
		private static Type typeOfXmlNodeArray;

		// Token: 0x0400058E RID: 1422
		[SecurityCritical]
		private static Type typeOfXmlSchemaSet;

		// Token: 0x0400058F RID: 1423
		[SecurityCritical]
		private static object[] emptyObjectArray;

		// Token: 0x04000590 RID: 1424
		[SecurityCritical]
		private static Type[] emptyTypeArray;

		// Token: 0x04000591 RID: 1425
		[SecurityCritical]
		private static Type typeOfIPropertyChange;

		// Token: 0x04000592 RID: 1426
		[SecurityCritical]
		private static Type typeOfIExtensibleDataObject;

		// Token: 0x04000593 RID: 1427
		[SecurityCritical]
		private static Type typeOfExtensionDataObject;

		// Token: 0x04000594 RID: 1428
		[SecurityCritical]
		private static Type typeOfISerializableDataNode;

		// Token: 0x04000595 RID: 1429
		[SecurityCritical]
		private static Type typeOfClassDataNode;

		// Token: 0x04000596 RID: 1430
		[SecurityCritical]
		private static Type typeOfCollectionDataNode;

		// Token: 0x04000597 RID: 1431
		[SecurityCritical]
		private static Type typeOfXmlDataNode;

		// Token: 0x04000598 RID: 1432
		[SecurityCritical]
		private static Type typeOfNullable;

		// Token: 0x04000599 RID: 1433
		[SecurityCritical]
		private static Type typeOfReflectionPointer;

		// Token: 0x0400059A RID: 1434
		[SecurityCritical]
		private static Type typeOfIDictionaryGeneric;

		// Token: 0x0400059B RID: 1435
		[SecurityCritical]
		private static Type typeOfIDictionary;

		// Token: 0x0400059C RID: 1436
		[SecurityCritical]
		private static Type typeOfIListGeneric;

		// Token: 0x0400059D RID: 1437
		[SecurityCritical]
		private static Type typeOfIList;

		// Token: 0x0400059E RID: 1438
		[SecurityCritical]
		private static Type typeOfICollectionGeneric;

		// Token: 0x0400059F RID: 1439
		[SecurityCritical]
		private static Type typeOfICollection;

		// Token: 0x040005A0 RID: 1440
		[SecurityCritical]
		private static Type typeOfIEnumerableGeneric;

		// Token: 0x040005A1 RID: 1441
		[SecurityCritical]
		private static Type typeOfIEnumerable;

		// Token: 0x040005A2 RID: 1442
		[SecurityCritical]
		private static Type typeOfIEnumeratorGeneric;

		// Token: 0x040005A3 RID: 1443
		[SecurityCritical]
		private static Type typeOfIEnumerator;

		// Token: 0x040005A4 RID: 1444
		[SecurityCritical]
		private static Type typeOfKeyValuePair;

		// Token: 0x040005A5 RID: 1445
		[SecurityCritical]
		private static Type typeOfKeyValue;

		// Token: 0x040005A6 RID: 1446
		[SecurityCritical]
		private static Type typeOfIDictionaryEnumerator;

		// Token: 0x040005A7 RID: 1447
		[SecurityCritical]
		private static Type typeOfDictionaryEnumerator;

		// Token: 0x040005A8 RID: 1448
		[SecurityCritical]
		private static Type typeOfGenericDictionaryEnumerator;

		// Token: 0x040005A9 RID: 1449
		[SecurityCritical]
		private static Type typeOfDictionaryGeneric;

		// Token: 0x040005AA RID: 1450
		[SecurityCritical]
		private static Type typeOfHashtable;

		// Token: 0x040005AB RID: 1451
		[SecurityCritical]
		private static Type typeOfListGeneric;

		// Token: 0x040005AC RID: 1452
		[SecurityCritical]
		private static Type typeOfXmlElement;

		// Token: 0x040005AD RID: 1453
		[SecurityCritical]
		private static Type typeOfDBNull;

		// Token: 0x040005AE RID: 1454
		[SecurityCritical]
		private static Uri dataContractXsdBaseNamespaceUri;

		// Token: 0x040005AF RID: 1455
		public const bool DefaultIsRequired = false;

		// Token: 0x040005B0 RID: 1456
		public const bool DefaultEmitDefaultValue = true;

		// Token: 0x040005B1 RID: 1457
		public const int DefaultOrder = 0;

		// Token: 0x040005B2 RID: 1458
		public const bool DefaultIsReference = false;

		// Token: 0x040005B3 RID: 1459
		public static readonly string NewObjectId = string.Empty;

		// Token: 0x040005B4 RID: 1460
		public const string SimpleSRSInternalsVisiblePattern = "^[\\s]*System\\.Runtime\\.Serialization[\\s]*$";

		// Token: 0x040005B5 RID: 1461
		public const string FullSRSInternalsVisiblePattern = "^[\\s]*System\\.Runtime\\.Serialization[\\s]*,[\\s]*PublicKey[\\s]*=[\\s]*(?i:00000000000000000400000000000000)[\\s]*$";

		// Token: 0x040005B6 RID: 1462
		public const string NullObjectId = null;

		// Token: 0x040005B7 RID: 1463
		public const string Space = " ";

		// Token: 0x040005B8 RID: 1464
		public const string OpenBracket = "[";

		// Token: 0x040005B9 RID: 1465
		public const string CloseBracket = "]";

		// Token: 0x040005BA RID: 1466
		public const string Comma = ",";

		// Token: 0x040005BB RID: 1467
		public const string XsiPrefix = "i";

		// Token: 0x040005BC RID: 1468
		public const string XsdPrefix = "x";

		// Token: 0x040005BD RID: 1469
		public const string SerPrefix = "z";

		// Token: 0x040005BE RID: 1470
		public const string SerPrefixForSchema = "ser";

		// Token: 0x040005BF RID: 1471
		public const string ElementPrefix = "q";

		// Token: 0x040005C0 RID: 1472
		public const string DataContractXsdBaseNamespace = "http://schemas.datacontract.org/2004/07/";

		// Token: 0x040005C1 RID: 1473
		public const string DataContractXmlNamespace = "http://schemas.datacontract.org/2004/07/System.Xml";

		// Token: 0x040005C2 RID: 1474
		public const string SchemaInstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040005C3 RID: 1475
		public const string SchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x040005C4 RID: 1476
		public const string XsiNilLocalName = "nil";

		// Token: 0x040005C5 RID: 1477
		public const string XsiTypeLocalName = "type";

		// Token: 0x040005C6 RID: 1478
		public const string TnsPrefix = "tns";

		// Token: 0x040005C7 RID: 1479
		public const string OccursUnbounded = "unbounded";

		// Token: 0x040005C8 RID: 1480
		public const string AnyTypeLocalName = "anyType";

		// Token: 0x040005C9 RID: 1481
		public const string StringLocalName = "string";

		// Token: 0x040005CA RID: 1482
		public const string IntLocalName = "int";

		// Token: 0x040005CB RID: 1483
		public const string True = "true";

		// Token: 0x040005CC RID: 1484
		public const string False = "false";

		// Token: 0x040005CD RID: 1485
		public const string ArrayPrefix = "ArrayOf";

		// Token: 0x040005CE RID: 1486
		public const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040005CF RID: 1487
		public const string XmlnsPrefix = "xmlns";

		// Token: 0x040005D0 RID: 1488
		public const string SchemaLocalName = "schema";

		// Token: 0x040005D1 RID: 1489
		public const string CollectionsNamespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";

		// Token: 0x040005D2 RID: 1490
		public const string DefaultClrNamespace = "GeneratedNamespace";

		// Token: 0x040005D3 RID: 1491
		public const string DefaultTypeName = "GeneratedType";

		// Token: 0x040005D4 RID: 1492
		public const string DefaultGeneratedMember = "GeneratedMember";

		// Token: 0x040005D5 RID: 1493
		public const string DefaultFieldSuffix = "Field";

		// Token: 0x040005D6 RID: 1494
		public const string DefaultPropertySuffix = "Property";

		// Token: 0x040005D7 RID: 1495
		public const string DefaultMemberSuffix = "Member";

		// Token: 0x040005D8 RID: 1496
		public const string NameProperty = "Name";

		// Token: 0x040005D9 RID: 1497
		public const string NamespaceProperty = "Namespace";

		// Token: 0x040005DA RID: 1498
		public const string OrderProperty = "Order";

		// Token: 0x040005DB RID: 1499
		public const string IsReferenceProperty = "IsReference";

		// Token: 0x040005DC RID: 1500
		public const string IsRequiredProperty = "IsRequired";

		// Token: 0x040005DD RID: 1501
		public const string EmitDefaultValueProperty = "EmitDefaultValue";

		// Token: 0x040005DE RID: 1502
		public const string ClrNamespaceProperty = "ClrNamespace";

		// Token: 0x040005DF RID: 1503
		public const string ItemNameProperty = "ItemName";

		// Token: 0x040005E0 RID: 1504
		public const string KeyNameProperty = "KeyName";

		// Token: 0x040005E1 RID: 1505
		public const string ValueNameProperty = "ValueName";

		// Token: 0x040005E2 RID: 1506
		public const string SerializationInfoPropertyName = "SerializationInfo";

		// Token: 0x040005E3 RID: 1507
		public const string SerializationInfoFieldName = "info";

		// Token: 0x040005E4 RID: 1508
		public const string NodeArrayPropertyName = "Nodes";

		// Token: 0x040005E5 RID: 1509
		public const string NodeArrayFieldName = "nodesField";

		// Token: 0x040005E6 RID: 1510
		public const string ExportSchemaMethod = "ExportSchema";

		// Token: 0x040005E7 RID: 1511
		public const string IsAnyProperty = "IsAny";

		// Token: 0x040005E8 RID: 1512
		public const string ContextFieldName = "context";

		// Token: 0x040005E9 RID: 1513
		public const string GetObjectDataMethodName = "GetObjectData";

		// Token: 0x040005EA RID: 1514
		public const string GetEnumeratorMethodName = "GetEnumerator";

		// Token: 0x040005EB RID: 1515
		public const string MoveNextMethodName = "MoveNext";

		// Token: 0x040005EC RID: 1516
		public const string AddValueMethodName = "AddValue";

		// Token: 0x040005ED RID: 1517
		public const string CurrentPropertyName = "Current";

		// Token: 0x040005EE RID: 1518
		public const string ValueProperty = "Value";

		// Token: 0x040005EF RID: 1519
		public const string EnumeratorFieldName = "enumerator";

		// Token: 0x040005F0 RID: 1520
		public const string SerializationEntryFieldName = "entry";

		// Token: 0x040005F1 RID: 1521
		public const string ExtensionDataSetMethod = "set_ExtensionData";

		// Token: 0x040005F2 RID: 1522
		public const string ExtensionDataSetExplicitMethod = "System.Runtime.Serialization.IExtensibleDataObject.set_ExtensionData";

		// Token: 0x040005F3 RID: 1523
		public const string ExtensionDataObjectPropertyName = "ExtensionData";

		// Token: 0x040005F4 RID: 1524
		public const string ExtensionDataObjectFieldName = "extensionDataField";

		// Token: 0x040005F5 RID: 1525
		public const string AddMethodName = "Add";

		// Token: 0x040005F6 RID: 1526
		public const string ParseMethodName = "Parse";

		// Token: 0x040005F7 RID: 1527
		public const string GetCurrentMethodName = "get_Current";

		// Token: 0x040005F8 RID: 1528
		public const string SerializationNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";

		// Token: 0x040005F9 RID: 1529
		public const string ClrTypeLocalName = "Type";

		// Token: 0x040005FA RID: 1530
		public const string ClrAssemblyLocalName = "Assembly";

		// Token: 0x040005FB RID: 1531
		public const string IsValueTypeLocalName = "IsValueType";

		// Token: 0x040005FC RID: 1532
		public const string EnumerationValueLocalName = "EnumerationValue";

		// Token: 0x040005FD RID: 1533
		public const string SurrogateDataLocalName = "Surrogate";

		// Token: 0x040005FE RID: 1534
		public const string GenericTypeLocalName = "GenericType";

		// Token: 0x040005FF RID: 1535
		public const string GenericParameterLocalName = "GenericParameter";

		// Token: 0x04000600 RID: 1536
		public const string GenericNameAttribute = "Name";

		// Token: 0x04000601 RID: 1537
		public const string GenericNamespaceAttribute = "Namespace";

		// Token: 0x04000602 RID: 1538
		public const string GenericParameterNestedLevelAttribute = "NestedLevel";

		// Token: 0x04000603 RID: 1539
		public const string IsDictionaryLocalName = "IsDictionary";

		// Token: 0x04000604 RID: 1540
		public const string ActualTypeLocalName = "ActualType";

		// Token: 0x04000605 RID: 1541
		public const string ActualTypeNameAttribute = "Name";

		// Token: 0x04000606 RID: 1542
		public const string ActualTypeNamespaceAttribute = "Namespace";

		// Token: 0x04000607 RID: 1543
		public const string DefaultValueLocalName = "DefaultValue";

		// Token: 0x04000608 RID: 1544
		public const string EmitDefaultValueAttribute = "EmitDefaultValue";

		// Token: 0x04000609 RID: 1545
		public const string ISerializableFactoryTypeLocalName = "FactoryType";

		// Token: 0x0400060A RID: 1546
		public const string IdLocalName = "Id";

		// Token: 0x0400060B RID: 1547
		public const string RefLocalName = "Ref";

		// Token: 0x0400060C RID: 1548
		public const string ArraySizeLocalName = "Size";

		// Token: 0x0400060D RID: 1549
		public const string KeyLocalName = "Key";

		// Token: 0x0400060E RID: 1550
		public const string ValueLocalName = "Value";

		// Token: 0x0400060F RID: 1551
		public const string MscorlibAssemblyName = "0";

		// Token: 0x04000610 RID: 1552
		public const string MscorlibAssemblySimpleName = "mscorlib";

		// Token: 0x04000611 RID: 1553
		public const string MscorlibFileName = "mscorlib.dll";

		// Token: 0x04000612 RID: 1554
		public const string SerializationSchema = "<?xml version='1.0' encoding='utf-8'?>\n<xs:schema elementFormDefault='qualified' attributeFormDefault='qualified' xmlns:tns='http://schemas.microsoft.com/2003/10/Serialization/' targetNamespace='http://schemas.microsoft.com/2003/10/Serialization/' xmlns:xs='http://www.w3.org/2001/XMLSchema'>\n  <xs:element name='anyType' nillable='true' type='xs:anyType' />\n  <xs:element name='anyURI' nillable='true' type='xs:anyURI' />\n  <xs:element name='base64Binary' nillable='true' type='xs:base64Binary' />\n  <xs:element name='boolean' nillable='true' type='xs:boolean' />\n  <xs:element name='byte' nillable='true' type='xs:byte' />\n  <xs:element name='dateTime' nillable='true' type='xs:dateTime' />\n  <xs:element name='decimal' nillable='true' type='xs:decimal' />\n  <xs:element name='double' nillable='true' type='xs:double' />\n  <xs:element name='float' nillable='true' type='xs:float' />\n  <xs:element name='int' nillable='true' type='xs:int' />\n  <xs:element name='long' nillable='true' type='xs:long' />\n  <xs:element name='QName' nillable='true' type='xs:QName' />\n  <xs:element name='short' nillable='true' type='xs:short' />\n  <xs:element name='string' nillable='true' type='xs:string' />\n  <xs:element name='unsignedByte' nillable='true' type='xs:unsignedByte' />\n  <xs:element name='unsignedInt' nillable='true' type='xs:unsignedInt' />\n  <xs:element name='unsignedLong' nillable='true' type='xs:unsignedLong' />\n  <xs:element name='unsignedShort' nillable='true' type='xs:unsignedShort' />\n  <xs:element name='char' nillable='true' type='tns:char' />\n  <xs:simpleType name='char'>\n    <xs:restriction base='xs:int'/>\n  </xs:simpleType>  \n  <xs:element name='duration' nillable='true' type='tns:duration' />\n  <xs:simpleType name='duration'>\n    <xs:restriction base='xs:duration'>\n      <xs:pattern value='\\-?P(\\d*D)?(T(\\d*H)?(\\d*M)?(\\d*(\\.\\d*)?S)?)?' />\n      <xs:minInclusive value='-P10675199DT2H48M5.4775808S' />\n      <xs:maxInclusive value='P10675199DT2H48M5.4775807S' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:element name='guid' nillable='true' type='tns:guid' />\n  <xs:simpleType name='guid'>\n    <xs:restriction base='xs:string'>\n      <xs:pattern value='[\\da-fA-F]{8}-[\\da-fA-F]{4}-[\\da-fA-F]{4}-[\\da-fA-F]{4}-[\\da-fA-F]{12}' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:attribute name='FactoryType' type='xs:QName' />\n  <xs:attribute name='Id' type='xs:ID' />\n  <xs:attribute name='Ref' type='xs:IDREF' />\n</xs:schema>\n";
	}
}
