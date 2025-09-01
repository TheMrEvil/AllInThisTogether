using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000EB RID: 235
	internal static class JsonFormatGeneratorStatics
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00035460 File Offset: 0x00033660
		public static MethodInfo BoxPointer
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.boxPointer == null)
				{
					JsonFormatGeneratorStatics.boxPointer = typeof(Pointer).GetMethod("Box");
				}
				return JsonFormatGeneratorStatics.boxPointer;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003548D File Offset: 0x0003368D
		public static PropertyInfo CollectionItemNameProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.collectionItemNameProperty == null)
				{
					JsonFormatGeneratorStatics.collectionItemNameProperty = typeof(XmlObjectSerializerWriteContextComplexJson).GetProperty("CollectionItemName", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.collectionItemNameProperty;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000354BC File Offset: 0x000336BC
		public static ConstructorInfo ExtensionDataObjectCtor
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.extensionDataObjectCtor == null)
				{
					JsonFormatGeneratorStatics.extensionDataObjectCtor = typeof(ExtensionDataObject).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
				return JsonFormatGeneratorStatics.extensionDataObjectCtor;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x000354EE File Offset: 0x000336EE
		public static PropertyInfo ExtensionDataProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.extensionDataProperty == null)
				{
					JsonFormatGeneratorStatics.extensionDataProperty = typeof(IExtensibleDataObject).GetProperty("ExtensionData");
				}
				return JsonFormatGeneratorStatics.extensionDataProperty;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0003551B File Offset: 0x0003371B
		public static MethodInfo GetCurrentMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.ienumeratorGetCurrentMethod == null)
				{
					JsonFormatGeneratorStatics.ienumeratorGetCurrentMethod = typeof(IEnumerator).GetProperty("Current").GetGetMethod();
				}
				return JsonFormatGeneratorStatics.ienumeratorGetCurrentMethod;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0003554D File Offset: 0x0003374D
		public static MethodInfo GetItemContractMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.getItemContractMethod == null)
				{
					JsonFormatGeneratorStatics.getItemContractMethod = typeof(CollectionDataContract).GetProperty("ItemContract", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetGetMethod(true);
				}
				return JsonFormatGeneratorStatics.getItemContractMethod;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00035582 File Offset: 0x00033782
		public static MethodInfo GetJsonDataContractMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.getJsonDataContractMethod == null)
				{
					JsonFormatGeneratorStatics.getJsonDataContractMethod = typeof(JsonDataContract).GetMethod("GetJsonDataContract", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.getJsonDataContractMethod;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x000355B1 File Offset: 0x000337B1
		public static MethodInfo GetJsonMemberIndexMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.getJsonMemberIndexMethod == null)
				{
					JsonFormatGeneratorStatics.getJsonMemberIndexMethod = typeof(XmlObjectSerializerReadContextComplexJson).GetMethod("GetJsonMemberIndex", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.getJsonMemberIndexMethod;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x000355E0 File Offset: 0x000337E0
		public static MethodInfo GetRevisedItemContractMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.getRevisedItemContractMethod == null)
				{
					JsonFormatGeneratorStatics.getRevisedItemContractMethod = typeof(XmlObjectSerializerWriteContextComplexJson).GetMethod("GetRevisedItemContract", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.getRevisedItemContractMethod;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00035610 File Offset: 0x00033810
		public static MethodInfo GetUninitializedObjectMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.getUninitializedObjectMethod == null)
				{
					JsonFormatGeneratorStatics.getUninitializedObjectMethod = typeof(XmlFormatReaderGenerator).GetMethod("UnsafeGetUninitializedObject", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(int)
					}, null);
				}
				return JsonFormatGeneratorStatics.getUninitializedObjectMethod;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003565F File Offset: 0x0003385F
		public static MethodInfo IsStartElementMethod0
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.isStartElementMethod0 == null)
				{
					JsonFormatGeneratorStatics.isStartElementMethod0 = typeof(XmlReaderDelegator).GetMethod("IsStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
				return JsonFormatGeneratorStatics.isStartElementMethod0;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00035698 File Offset: 0x00033898
		public static MethodInfo IsStartElementMethod2
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.isStartElementMethod2 == null)
				{
					JsonFormatGeneratorStatics.isStartElementMethod2 = typeof(XmlReaderDelegator).GetMethod("IsStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlDictionaryString),
						typeof(XmlDictionaryString)
					}, null);
				}
				return JsonFormatGeneratorStatics.isStartElementMethod2;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000356F4 File Offset: 0x000338F4
		public static PropertyInfo LocalNameProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.localNameProperty == null)
				{
					JsonFormatGeneratorStatics.localNameProperty = typeof(XmlReaderDelegator).GetProperty("LocalName", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.localNameProperty;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00035723 File Offset: 0x00033923
		public static PropertyInfo NamespaceProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.namespaceProperty == null)
				{
					JsonFormatGeneratorStatics.namespaceProperty = typeof(XmlReaderDelegator).GetProperty("NamespaceProperty", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.namespaceProperty;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00035752 File Offset: 0x00033952
		public static MethodInfo MoveNextMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.ienumeratorMoveNextMethod == null)
				{
					JsonFormatGeneratorStatics.ienumeratorMoveNextMethod = typeof(IEnumerator).GetMethod("MoveNext");
				}
				return JsonFormatGeneratorStatics.ienumeratorMoveNextMethod;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0003577F File Offset: 0x0003397F
		public static MethodInfo MoveToContentMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.moveToContentMethod == null)
				{
					JsonFormatGeneratorStatics.moveToContentMethod = typeof(XmlReaderDelegator).GetMethod("MoveToContent", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.moveToContentMethod;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000357AE File Offset: 0x000339AE
		public static PropertyInfo NodeTypeProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.nodeTypeProperty == null)
				{
					JsonFormatGeneratorStatics.nodeTypeProperty = typeof(XmlReaderDelegator).GetProperty("NodeType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.nodeTypeProperty;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000357DD File Offset: 0x000339DD
		public static MethodInfo OnDeserializationMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.onDeserializationMethod == null)
				{
					JsonFormatGeneratorStatics.onDeserializationMethod = typeof(IDeserializationCallback).GetMethod("OnDeserialization");
				}
				return JsonFormatGeneratorStatics.onDeserializationMethod;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0003580A File Offset: 0x00033A0A
		public static MethodInfo ReadJsonValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.readJsonValueMethod == null)
				{
					JsonFormatGeneratorStatics.readJsonValueMethod = typeof(DataContractJsonSerializer).GetMethod("ReadJsonValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.readJsonValueMethod;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00035839 File Offset: 0x00033A39
		public static ConstructorInfo SerializationExceptionCtor
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.serializationExceptionCtor == null)
				{
					JsonFormatGeneratorStatics.serializationExceptionCtor = typeof(SerializationException).GetConstructor(new Type[]
					{
						typeof(string)
					});
				}
				return JsonFormatGeneratorStatics.serializationExceptionCtor;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00035874 File Offset: 0x00033A74
		public static Type[] SerInfoCtorArgs
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.serInfoCtorArgs == null)
				{
					JsonFormatGeneratorStatics.serInfoCtorArgs = new Type[]
					{
						typeof(SerializationInfo),
						typeof(StreamingContext)
					};
				}
				return JsonFormatGeneratorStatics.serInfoCtorArgs;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x000358A7 File Offset: 0x00033AA7
		public static MethodInfo ThrowDuplicateMemberExceptionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.throwDuplicateMemberExceptionMethod == null)
				{
					JsonFormatGeneratorStatics.throwDuplicateMemberExceptionMethod = typeof(XmlObjectSerializerReadContextComplexJson).GetMethod("ThrowDuplicateMemberException", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.throwDuplicateMemberExceptionMethod;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x000358D6 File Offset: 0x00033AD6
		public static MethodInfo ThrowMissingRequiredMembersMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.throwMissingRequiredMembersMethod == null)
				{
					JsonFormatGeneratorStatics.throwMissingRequiredMembersMethod = typeof(XmlObjectSerializerReadContextComplexJson).GetMethod("ThrowMissingRequiredMembers", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.throwMissingRequiredMembersMethod;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00035905 File Offset: 0x00033B05
		public static PropertyInfo TypeHandleProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.typeHandleProperty == null)
				{
					JsonFormatGeneratorStatics.typeHandleProperty = typeof(Type).GetProperty("TypeHandle");
				}
				return JsonFormatGeneratorStatics.typeHandleProperty;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00035932 File Offset: 0x00033B32
		public static MethodInfo UnboxPointer
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.unboxPointer == null)
				{
					JsonFormatGeneratorStatics.unboxPointer = typeof(Pointer).GetMethod("Unbox");
				}
				return JsonFormatGeneratorStatics.unboxPointer;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0003595F File Offset: 0x00033B5F
		public static PropertyInfo UseSimpleDictionaryFormatReadProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.useSimpleDictionaryFormatReadProperty == null)
				{
					JsonFormatGeneratorStatics.useSimpleDictionaryFormatReadProperty = typeof(XmlObjectSerializerReadContextComplexJson).GetProperty("UseSimpleDictionaryFormat", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.useSimpleDictionaryFormatReadProperty;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0003598E File Offset: 0x00033B8E
		public static PropertyInfo UseSimpleDictionaryFormatWriteProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.useSimpleDictionaryFormatWriteProperty == null)
				{
					JsonFormatGeneratorStatics.useSimpleDictionaryFormatWriteProperty = typeof(XmlObjectSerializerWriteContextComplexJson).GetProperty("UseSimpleDictionaryFormat", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.useSimpleDictionaryFormatWriteProperty;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x000359C0 File Offset: 0x00033BC0
		public static MethodInfo WriteAttributeStringMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeAttributeStringMethod == null)
				{
					JsonFormatGeneratorStatics.writeAttributeStringMethod = typeof(XmlWriterDelegator).GetMethod("WriteAttributeString", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string)
					}, null);
				}
				return JsonFormatGeneratorStatics.writeAttributeStringMethod;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00035A36 File Offset: 0x00033C36
		public static MethodInfo WriteEndElementMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeEndElementMethod == null)
				{
					JsonFormatGeneratorStatics.writeEndElementMethod = typeof(XmlWriterDelegator).GetMethod("WriteEndElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
				return JsonFormatGeneratorStatics.writeEndElementMethod;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x00035A6D File Offset: 0x00033C6D
		public static MethodInfo WriteJsonISerializableMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeJsonISerializableMethod == null)
				{
					JsonFormatGeneratorStatics.writeJsonISerializableMethod = typeof(XmlObjectSerializerWriteContextComplexJson).GetMethod("WriteJsonISerializable", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.writeJsonISerializableMethod;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00035A9C File Offset: 0x00033C9C
		public static MethodInfo WriteJsonNameWithMappingMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeJsonNameWithMappingMethod == null)
				{
					JsonFormatGeneratorStatics.writeJsonNameWithMappingMethod = typeof(XmlObjectSerializerWriteContextComplexJson).GetMethod("WriteJsonNameWithMapping", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.writeJsonNameWithMappingMethod;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00035ACB File Offset: 0x00033CCB
		public static MethodInfo WriteJsonValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeJsonValueMethod == null)
				{
					JsonFormatGeneratorStatics.writeJsonValueMethod = typeof(DataContractJsonSerializer).GetMethod("WriteJsonValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return JsonFormatGeneratorStatics.writeJsonValueMethod;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00035AFC File Offset: 0x00033CFC
		public static MethodInfo WriteStartElementMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeStartElementMethod == null)
				{
					JsonFormatGeneratorStatics.writeStartElementMethod = typeof(XmlWriterDelegator).GetMethod("WriteStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlDictionaryString),
						typeof(XmlDictionaryString)
					}, null);
				}
				return JsonFormatGeneratorStatics.writeStartElementMethod;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00035B58 File Offset: 0x00033D58
		public static MethodInfo WriteStartElementStringMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.writeStartElementStringMethod == null)
				{
					JsonFormatGeneratorStatics.writeStartElementStringMethod = typeof(XmlWriterDelegator).GetMethod("WriteStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(string),
						typeof(string)
					}, null);
				}
				return JsonFormatGeneratorStatics.writeStartElementStringMethod;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00035BB4 File Offset: 0x00033DB4
		public static MethodInfo ParseEnumMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.parseEnumMethod == null)
				{
					JsonFormatGeneratorStatics.parseEnumMethod = typeof(Enum).GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(Type),
						typeof(string)
					}, null);
				}
				return JsonFormatGeneratorStatics.parseEnumMethod;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00035C10 File Offset: 0x00033E10
		public static MethodInfo GetJsonMemberNameMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonFormatGeneratorStatics.getJsonMemberNameMethod == null)
				{
					JsonFormatGeneratorStatics.getJsonMemberNameMethod = typeof(XmlObjectSerializerReadContextComplexJson).GetMethod("GetJsonMemberName", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlReaderDelegator)
					}, null);
				}
				return JsonFormatGeneratorStatics.getJsonMemberNameMethod;
			}
		}

		// Token: 0x0400061E RID: 1566
		[SecurityCritical]
		private static MethodInfo boxPointer;

		// Token: 0x0400061F RID: 1567
		[SecurityCritical]
		private static PropertyInfo collectionItemNameProperty;

		// Token: 0x04000620 RID: 1568
		[SecurityCritical]
		private static ConstructorInfo extensionDataObjectCtor;

		// Token: 0x04000621 RID: 1569
		[SecurityCritical]
		private static PropertyInfo extensionDataProperty;

		// Token: 0x04000622 RID: 1570
		[SecurityCritical]
		private static MethodInfo getItemContractMethod;

		// Token: 0x04000623 RID: 1571
		[SecurityCritical]
		private static MethodInfo getJsonDataContractMethod;

		// Token: 0x04000624 RID: 1572
		[SecurityCritical]
		private static MethodInfo getJsonMemberIndexMethod;

		// Token: 0x04000625 RID: 1573
		[SecurityCritical]
		private static MethodInfo getRevisedItemContractMethod;

		// Token: 0x04000626 RID: 1574
		[SecurityCritical]
		private static MethodInfo getUninitializedObjectMethod;

		// Token: 0x04000627 RID: 1575
		[SecurityCritical]
		private static MethodInfo ienumeratorGetCurrentMethod;

		// Token: 0x04000628 RID: 1576
		[SecurityCritical]
		private static MethodInfo ienumeratorMoveNextMethod;

		// Token: 0x04000629 RID: 1577
		[SecurityCritical]
		private static MethodInfo isStartElementMethod0;

		// Token: 0x0400062A RID: 1578
		[SecurityCritical]
		private static MethodInfo isStartElementMethod2;

		// Token: 0x0400062B RID: 1579
		[SecurityCritical]
		private static PropertyInfo localNameProperty;

		// Token: 0x0400062C RID: 1580
		[SecurityCritical]
		private static PropertyInfo namespaceProperty;

		// Token: 0x0400062D RID: 1581
		[SecurityCritical]
		private static MethodInfo moveToContentMethod;

		// Token: 0x0400062E RID: 1582
		[SecurityCritical]
		private static PropertyInfo nodeTypeProperty;

		// Token: 0x0400062F RID: 1583
		[SecurityCritical]
		private static MethodInfo onDeserializationMethod;

		// Token: 0x04000630 RID: 1584
		[SecurityCritical]
		private static MethodInfo readJsonValueMethod;

		// Token: 0x04000631 RID: 1585
		[SecurityCritical]
		private static ConstructorInfo serializationExceptionCtor;

		// Token: 0x04000632 RID: 1586
		[SecurityCritical]
		private static Type[] serInfoCtorArgs;

		// Token: 0x04000633 RID: 1587
		[SecurityCritical]
		private static MethodInfo throwDuplicateMemberExceptionMethod;

		// Token: 0x04000634 RID: 1588
		[SecurityCritical]
		private static MethodInfo throwMissingRequiredMembersMethod;

		// Token: 0x04000635 RID: 1589
		[SecurityCritical]
		private static PropertyInfo typeHandleProperty;

		// Token: 0x04000636 RID: 1590
		[SecurityCritical]
		private static MethodInfo unboxPointer;

		// Token: 0x04000637 RID: 1591
		[SecurityCritical]
		private static PropertyInfo useSimpleDictionaryFormatReadProperty;

		// Token: 0x04000638 RID: 1592
		[SecurityCritical]
		private static PropertyInfo useSimpleDictionaryFormatWriteProperty;

		// Token: 0x04000639 RID: 1593
		[SecurityCritical]
		private static MethodInfo writeAttributeStringMethod;

		// Token: 0x0400063A RID: 1594
		[SecurityCritical]
		private static MethodInfo writeEndElementMethod;

		// Token: 0x0400063B RID: 1595
		[SecurityCritical]
		private static MethodInfo writeJsonISerializableMethod;

		// Token: 0x0400063C RID: 1596
		[SecurityCritical]
		private static MethodInfo writeJsonNameWithMappingMethod;

		// Token: 0x0400063D RID: 1597
		[SecurityCritical]
		private static MethodInfo writeJsonValueMethod;

		// Token: 0x0400063E RID: 1598
		[SecurityCritical]
		private static MethodInfo writeStartElementMethod;

		// Token: 0x0400063F RID: 1599
		[SecurityCritical]
		private static MethodInfo writeStartElementStringMethod;

		// Token: 0x04000640 RID: 1600
		[SecurityCritical]
		private static MethodInfo parseEnumMethod;

		// Token: 0x04000641 RID: 1601
		[SecurityCritical]
		private static MethodInfo getJsonMemberNameMethod;
	}
}
