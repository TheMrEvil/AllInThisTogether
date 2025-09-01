using System;
using System.Collections;
using System.Reflection;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000136 RID: 310
	internal static class XmlFormatGeneratorStatics
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0003D844 File Offset: 0x0003BA44
		internal static MethodInfo WriteStartElementMethod2
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeStartElementMethod2 == null)
				{
					XmlFormatGeneratorStatics.writeStartElementMethod2 = typeof(XmlWriterDelegator).GetMethod("WriteStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlDictionaryString),
						typeof(XmlDictionaryString)
					}, null);
				}
				return XmlFormatGeneratorStatics.writeStartElementMethod2;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0003D8A0 File Offset: 0x0003BAA0
		internal static MethodInfo WriteStartElementMethod3
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeStartElementMethod3 == null)
				{
					XmlFormatGeneratorStatics.writeStartElementMethod3 = typeof(XmlWriterDelegator).GetMethod("WriteStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(string),
						typeof(XmlDictionaryString),
						typeof(XmlDictionaryString)
					}, null);
				}
				return XmlFormatGeneratorStatics.writeStartElementMethod3;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003D909 File Offset: 0x0003BB09
		internal static MethodInfo WriteEndElementMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeEndElementMethod == null)
				{
					XmlFormatGeneratorStatics.writeEndElementMethod = typeof(XmlWriterDelegator).GetMethod("WriteEndElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
				return XmlFormatGeneratorStatics.writeEndElementMethod;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0003D940 File Offset: 0x0003BB40
		internal static MethodInfo WriteNamespaceDeclMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeNamespaceDeclMethod == null)
				{
					XmlFormatGeneratorStatics.writeNamespaceDeclMethod = typeof(XmlWriterDelegator).GetMethod("WriteNamespaceDecl", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlDictionaryString)
					}, null);
				}
				return XmlFormatGeneratorStatics.writeNamespaceDeclMethod;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0003D98F File Offset: 0x0003BB8F
		internal static PropertyInfo ExtensionDataProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.extensionDataProperty == null)
				{
					XmlFormatGeneratorStatics.extensionDataProperty = typeof(IExtensibleDataObject).GetProperty("ExtensionData");
				}
				return XmlFormatGeneratorStatics.extensionDataProperty;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0003D9BC File Offset: 0x0003BBBC
		internal static MethodInfo BoxPointer
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.boxPointer == null)
				{
					XmlFormatGeneratorStatics.boxPointer = typeof(Pointer).GetMethod("Box");
				}
				return XmlFormatGeneratorStatics.boxPointer;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003D9E9 File Offset: 0x0003BBE9
		internal static ConstructorInfo DictionaryEnumeratorCtor
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.dictionaryEnumeratorCtor == null)
				{
					XmlFormatGeneratorStatics.dictionaryEnumeratorCtor = Globals.TypeOfDictionaryEnumerator.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						Globals.TypeOfIDictionaryEnumerator
					}, null);
				}
				return XmlFormatGeneratorStatics.dictionaryEnumeratorCtor;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003DA1E File Offset: 0x0003BC1E
		internal static MethodInfo MoveNextMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.ienumeratorMoveNextMethod == null)
				{
					XmlFormatGeneratorStatics.ienumeratorMoveNextMethod = typeof(IEnumerator).GetMethod("MoveNext");
				}
				return XmlFormatGeneratorStatics.ienumeratorMoveNextMethod;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003DA4B File Offset: 0x0003BC4B
		internal static MethodInfo GetCurrentMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.ienumeratorGetCurrentMethod == null)
				{
					XmlFormatGeneratorStatics.ienumeratorGetCurrentMethod = typeof(IEnumerator).GetProperty("Current").GetGetMethod();
				}
				return XmlFormatGeneratorStatics.ienumeratorGetCurrentMethod;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003DA7D File Offset: 0x0003BC7D
		internal static MethodInfo GetItemContractMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getItemContractMethod == null)
				{
					XmlFormatGeneratorStatics.getItemContractMethod = typeof(CollectionDataContract).GetProperty("ItemContract", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetGetMethod(true);
				}
				return XmlFormatGeneratorStatics.getItemContractMethod;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
		internal static MethodInfo IsStartElementMethod2
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.isStartElementMethod2 == null)
				{
					XmlFormatGeneratorStatics.isStartElementMethod2 = typeof(XmlReaderDelegator).GetMethod("IsStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlDictionaryString),
						typeof(XmlDictionaryString)
					}, null);
				}
				return XmlFormatGeneratorStatics.isStartElementMethod2;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003DB10 File Offset: 0x0003BD10
		internal static MethodInfo IsStartElementMethod0
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.isStartElementMethod0 == null)
				{
					XmlFormatGeneratorStatics.isStartElementMethod0 = typeof(XmlReaderDelegator).GetMethod("IsStartElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
				return XmlFormatGeneratorStatics.isStartElementMethod0;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003DB48 File Offset: 0x0003BD48
		internal static MethodInfo GetUninitializedObjectMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getUninitializedObjectMethod == null)
				{
					XmlFormatGeneratorStatics.getUninitializedObjectMethod = typeof(XmlFormatReaderGenerator).GetMethod("UnsafeGetUninitializedObject", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(int)
					}, null);
				}
				return XmlFormatGeneratorStatics.getUninitializedObjectMethod;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003DB97 File Offset: 0x0003BD97
		internal static MethodInfo OnDeserializationMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.onDeserializationMethod == null)
				{
					XmlFormatGeneratorStatics.onDeserializationMethod = typeof(IDeserializationCallback).GetMethod("OnDeserialization");
				}
				return XmlFormatGeneratorStatics.onDeserializationMethod;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
		internal static MethodInfo UnboxPointer
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.unboxPointer == null)
				{
					XmlFormatGeneratorStatics.unboxPointer = typeof(Pointer).GetMethod("Unbox");
				}
				return XmlFormatGeneratorStatics.unboxPointer;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003DBF1 File Offset: 0x0003BDF1
		internal static PropertyInfo NodeTypeProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.nodeTypeProperty == null)
				{
					XmlFormatGeneratorStatics.nodeTypeProperty = typeof(XmlReaderDelegator).GetProperty("NodeType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.nodeTypeProperty;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0003DC20 File Offset: 0x0003BE20
		internal static ConstructorInfo SerializationExceptionCtor
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.serializationExceptionCtor == null)
				{
					XmlFormatGeneratorStatics.serializationExceptionCtor = typeof(SerializationException).GetConstructor(new Type[]
					{
						typeof(string)
					});
				}
				return XmlFormatGeneratorStatics.serializationExceptionCtor;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003DC5B File Offset: 0x0003BE5B
		internal static ConstructorInfo ExtensionDataObjectCtor
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.extensionDataObjectCtor == null)
				{
					XmlFormatGeneratorStatics.extensionDataObjectCtor = typeof(ExtensionDataObject).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				}
				return XmlFormatGeneratorStatics.extensionDataObjectCtor;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0003DC8D File Offset: 0x0003BE8D
		internal static ConstructorInfo HashtableCtor
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.hashtableCtor == null)
				{
					XmlFormatGeneratorStatics.hashtableCtor = Globals.TypeOfHashtable.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
				}
				return XmlFormatGeneratorStatics.hashtableCtor;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003DCB9 File Offset: 0x0003BEB9
		internal static MethodInfo GetStreamingContextMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getStreamingContextMethod == null)
				{
					XmlFormatGeneratorStatics.getStreamingContextMethod = typeof(XmlObjectSerializerContext).GetMethod("GetStreamingContext", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getStreamingContextMethod;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		internal static MethodInfo GetCollectionMemberMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getCollectionMemberMethod == null)
				{
					XmlFormatGeneratorStatics.getCollectionMemberMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetCollectionMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getCollectionMemberMethod;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003DD18 File Offset: 0x0003BF18
		internal static MethodInfo StoreCollectionMemberInfoMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.storeCollectionMemberInfoMethod == null)
				{
					XmlFormatGeneratorStatics.storeCollectionMemberInfoMethod = typeof(XmlObjectSerializerReadContext).GetMethod("StoreCollectionMemberInfo", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(object)
					}, null);
				}
				return XmlFormatGeneratorStatics.storeCollectionMemberInfoMethod;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0003DD67 File Offset: 0x0003BF67
		internal static MethodInfo StoreIsGetOnlyCollectionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.storeIsGetOnlyCollectionMethod == null)
				{
					XmlFormatGeneratorStatics.storeIsGetOnlyCollectionMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("StoreIsGetOnlyCollection", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.storeIsGetOnlyCollectionMethod;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003DD96 File Offset: 0x0003BF96
		internal static MethodInfo ThrowNullValueReturnedForGetOnlyCollectionExceptionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.throwNullValueReturnedForGetOnlyCollectionExceptionMethod == null)
				{
					XmlFormatGeneratorStatics.throwNullValueReturnedForGetOnlyCollectionExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ThrowNullValueReturnedForGetOnlyCollectionException", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.throwNullValueReturnedForGetOnlyCollectionExceptionMethod;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003DDC5 File Offset: 0x0003BFC5
		internal static MethodInfo ThrowArrayExceededSizeExceptionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.throwArrayExceededSizeExceptionMethod == null)
				{
					XmlFormatGeneratorStatics.throwArrayExceededSizeExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ThrowArrayExceededSizeException", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.throwArrayExceededSizeExceptionMethod;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		internal static MethodInfo IncrementItemCountMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.incrementItemCountMethod == null)
				{
					XmlFormatGeneratorStatics.incrementItemCountMethod = typeof(XmlObjectSerializerContext).GetMethod("IncrementItemCount", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.incrementItemCountMethod;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003DE23 File Offset: 0x0003C023
		internal static MethodInfo DemandSerializationFormatterPermissionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.demandSerializationFormatterPermissionMethod == null)
				{
					XmlFormatGeneratorStatics.demandSerializationFormatterPermissionMethod = typeof(XmlObjectSerializerContext).GetMethod("DemandSerializationFormatterPermission", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.demandSerializationFormatterPermissionMethod;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003DE52 File Offset: 0x0003C052
		internal static MethodInfo DemandMemberAccessPermissionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.demandMemberAccessPermissionMethod == null)
				{
					XmlFormatGeneratorStatics.demandMemberAccessPermissionMethod = typeof(XmlObjectSerializerContext).GetMethod("DemandMemberAccessPermission", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.demandMemberAccessPermissionMethod;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003DE84 File Offset: 0x0003C084
		internal static MethodInfo InternalDeserializeMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.internalDeserializeMethod == null)
				{
					XmlFormatGeneratorStatics.internalDeserializeMethod = typeof(XmlObjectSerializerReadContext).GetMethod("InternalDeserialize", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlReaderDelegator),
						typeof(int),
						typeof(RuntimeTypeHandle),
						typeof(string),
						typeof(string)
					}, null);
				}
				return XmlFormatGeneratorStatics.internalDeserializeMethod;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0003DF07 File Offset: 0x0003C107
		internal static MethodInfo MoveToNextElementMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.moveToNextElementMethod == null)
				{
					XmlFormatGeneratorStatics.moveToNextElementMethod = typeof(XmlObjectSerializerReadContext).GetMethod("MoveToNextElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.moveToNextElementMethod;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003DF36 File Offset: 0x0003C136
		internal static MethodInfo GetMemberIndexMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getMemberIndexMethod == null)
				{
					XmlFormatGeneratorStatics.getMemberIndexMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetMemberIndex", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getMemberIndexMethod;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0003DF65 File Offset: 0x0003C165
		internal static MethodInfo GetMemberIndexWithRequiredMembersMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getMemberIndexWithRequiredMembersMethod == null)
				{
					XmlFormatGeneratorStatics.getMemberIndexWithRequiredMembersMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetMemberIndexWithRequiredMembers", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getMemberIndexWithRequiredMembersMethod;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0003DF94 File Offset: 0x0003C194
		internal static MethodInfo ThrowRequiredMemberMissingExceptionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.throwRequiredMemberMissingExceptionMethod == null)
				{
					XmlFormatGeneratorStatics.throwRequiredMemberMissingExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ThrowRequiredMemberMissingException", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.throwRequiredMemberMissingExceptionMethod;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0003DFC3 File Offset: 0x0003C1C3
		internal static MethodInfo SkipUnknownElementMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.skipUnknownElementMethod == null)
				{
					XmlFormatGeneratorStatics.skipUnknownElementMethod = typeof(XmlObjectSerializerReadContext).GetMethod("SkipUnknownElement", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.skipUnknownElementMethod;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003DFF4 File Offset: 0x0003C1F4
		internal static MethodInfo ReadIfNullOrRefMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.readIfNullOrRefMethod == null)
				{
					XmlFormatGeneratorStatics.readIfNullOrRefMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ReadIfNullOrRef", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlReaderDelegator),
						typeof(Type),
						typeof(bool)
					}, null);
				}
				return XmlFormatGeneratorStatics.readIfNullOrRefMethod;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003E05D File Offset: 0x0003C25D
		internal static MethodInfo ReadAttributesMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.readAttributesMethod == null)
				{
					XmlFormatGeneratorStatics.readAttributesMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ReadAttributes", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.readAttributesMethod;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003E08C File Offset: 0x0003C28C
		internal static MethodInfo ResetAttributesMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.resetAttributesMethod == null)
				{
					XmlFormatGeneratorStatics.resetAttributesMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ResetAttributes", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.resetAttributesMethod;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003E0BB File Offset: 0x0003C2BB
		internal static MethodInfo GetObjectIdMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getObjectIdMethod == null)
				{
					XmlFormatGeneratorStatics.getObjectIdMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetObjectId", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getObjectIdMethod;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003E0EA File Offset: 0x0003C2EA
		internal static MethodInfo GetArraySizeMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getArraySizeMethod == null)
				{
					XmlFormatGeneratorStatics.getArraySizeMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetArraySize", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getArraySizeMethod;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0003E119 File Offset: 0x0003C319
		internal static MethodInfo AddNewObjectMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.addNewObjectMethod == null)
				{
					XmlFormatGeneratorStatics.addNewObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod("AddNewObject", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.addNewObjectMethod;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003E148 File Offset: 0x0003C348
		internal static MethodInfo AddNewObjectWithIdMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.addNewObjectWithIdMethod == null)
				{
					XmlFormatGeneratorStatics.addNewObjectWithIdMethod = typeof(XmlObjectSerializerReadContext).GetMethod("AddNewObjectWithId", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.addNewObjectWithIdMethod;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0003E177 File Offset: 0x0003C377
		internal static MethodInfo ReplaceDeserializedObjectMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.replaceDeserializedObjectMethod == null)
				{
					XmlFormatGeneratorStatics.replaceDeserializedObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ReplaceDeserializedObject", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.replaceDeserializedObjectMethod;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0003E1A6 File Offset: 0x0003C3A6
		internal static MethodInfo GetExistingObjectMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getExistingObjectMethod == null)
				{
					XmlFormatGeneratorStatics.getExistingObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetExistingObject", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getExistingObjectMethod;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003E1D5 File Offset: 0x0003C3D5
		internal static MethodInfo GetRealObjectMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getRealObjectMethod == null)
				{
					XmlFormatGeneratorStatics.getRealObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod("GetRealObject", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getRealObjectMethod;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0003E204 File Offset: 0x0003C404
		internal static MethodInfo ReadMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.readMethod == null)
				{
					XmlFormatGeneratorStatics.readMethod = typeof(XmlObjectSerializerReadContext).GetMethod("Read", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.readMethod;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003E233 File Offset: 0x0003C433
		internal static MethodInfo EnsureArraySizeMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.ensureArraySizeMethod == null)
				{
					XmlFormatGeneratorStatics.ensureArraySizeMethod = typeof(XmlObjectSerializerReadContext).GetMethod("EnsureArraySize", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.ensureArraySizeMethod;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0003E262 File Offset: 0x0003C462
		internal static MethodInfo TrimArraySizeMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.trimArraySizeMethod == null)
				{
					XmlFormatGeneratorStatics.trimArraySizeMethod = typeof(XmlObjectSerializerReadContext).GetMethod("TrimArraySize", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.trimArraySizeMethod;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003E291 File Offset: 0x0003C491
		internal static MethodInfo CheckEndOfArrayMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.checkEndOfArrayMethod == null)
				{
					XmlFormatGeneratorStatics.checkEndOfArrayMethod = typeof(XmlObjectSerializerReadContext).GetMethod("CheckEndOfArray", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.checkEndOfArrayMethod;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0003E2C0 File Offset: 0x0003C4C0
		internal static MethodInfo GetArrayLengthMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getArrayLengthMethod == null)
				{
					XmlFormatGeneratorStatics.getArrayLengthMethod = Globals.TypeOfArray.GetProperty("Length").GetGetMethod();
				}
				return XmlFormatGeneratorStatics.getArrayLengthMethod;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003E2ED File Offset: 0x0003C4ED
		internal static MethodInfo ReadSerializationInfoMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.readSerializationInfoMethod == null)
				{
					XmlFormatGeneratorStatics.readSerializationInfoMethod = typeof(XmlObjectSerializerReadContext).GetMethod("ReadSerializationInfo", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.readSerializationInfoMethod;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0003E31C File Offset: 0x0003C51C
		internal static MethodInfo CreateUnexpectedStateExceptionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.createUnexpectedStateExceptionMethod == null)
				{
					XmlFormatGeneratorStatics.createUnexpectedStateExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod("CreateUnexpectedStateException", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlNodeType),
						typeof(XmlReaderDelegator)
					}, null);
				}
				return XmlFormatGeneratorStatics.createUnexpectedStateExceptionMethod;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003E378 File Offset: 0x0003C578
		internal static MethodInfo InternalSerializeReferenceMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.internalSerializeReferenceMethod == null)
				{
					XmlFormatGeneratorStatics.internalSerializeReferenceMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("InternalSerializeReference", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.internalSerializeReferenceMethod;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003E3A7 File Offset: 0x0003C5A7
		internal static MethodInfo InternalSerializeMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.internalSerializeMethod == null)
				{
					XmlFormatGeneratorStatics.internalSerializeMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("InternalSerialize", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.internalSerializeMethod;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		internal static MethodInfo WriteNullMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeNullMethod == null)
				{
					XmlFormatGeneratorStatics.writeNullMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("WriteNull", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlWriterDelegator),
						typeof(Type),
						typeof(bool)
					}, null);
				}
				return XmlFormatGeneratorStatics.writeNullMethod;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003E441 File Offset: 0x0003C641
		internal static MethodInfo IncrementArrayCountMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.incrementArrayCountMethod == null)
				{
					XmlFormatGeneratorStatics.incrementArrayCountMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("IncrementArrayCount", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.incrementArrayCountMethod;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003E470 File Offset: 0x0003C670
		internal static MethodInfo IncrementCollectionCountMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.incrementCollectionCountMethod == null)
				{
					XmlFormatGeneratorStatics.incrementCollectionCountMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("IncrementCollectionCount", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(XmlWriterDelegator),
						typeof(ICollection)
					}, null);
				}
				return XmlFormatGeneratorStatics.incrementCollectionCountMethod;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0003E4CC File Offset: 0x0003C6CC
		internal static MethodInfo IncrementCollectionCountGenericMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.incrementCollectionCountGenericMethod == null)
				{
					XmlFormatGeneratorStatics.incrementCollectionCountGenericMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("IncrementCollectionCountGeneric", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.incrementCollectionCountGenericMethod;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003E4FB File Offset: 0x0003C6FB
		internal static MethodInfo GetDefaultValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getDefaultValueMethod == null)
				{
					XmlFormatGeneratorStatics.getDefaultValueMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("GetDefaultValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getDefaultValueMethod;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0003E52A File Offset: 0x0003C72A
		internal static MethodInfo GetNullableValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getNullableValueMethod == null)
				{
					XmlFormatGeneratorStatics.getNullableValueMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("GetNullableValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getNullableValueMethod;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003E559 File Offset: 0x0003C759
		internal static MethodInfo ThrowRequiredMemberMustBeEmittedMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.throwRequiredMemberMustBeEmittedMethod == null)
				{
					XmlFormatGeneratorStatics.throwRequiredMemberMustBeEmittedMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("ThrowRequiredMemberMustBeEmitted", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.throwRequiredMemberMustBeEmittedMethod;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003E588 File Offset: 0x0003C788
		internal static MethodInfo GetHasValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getHasValueMethod == null)
				{
					XmlFormatGeneratorStatics.getHasValueMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("GetHasValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getHasValueMethod;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003E5B7 File Offset: 0x0003C7B7
		internal static MethodInfo WriteISerializableMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeISerializableMethod == null)
				{
					XmlFormatGeneratorStatics.writeISerializableMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("WriteISerializable", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.writeISerializableMethod;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0003E5E6 File Offset: 0x0003C7E6
		internal static MethodInfo WriteExtensionDataMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeExtensionDataMethod == null)
				{
					XmlFormatGeneratorStatics.writeExtensionDataMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("WriteExtensionData", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.writeExtensionDataMethod;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003E615 File Offset: 0x0003C815
		internal static MethodInfo WriteXmlValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.writeXmlValueMethod == null)
				{
					XmlFormatGeneratorStatics.writeXmlValueMethod = typeof(DataContract).GetMethod("WriteXmlValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.writeXmlValueMethod;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0003E644 File Offset: 0x0003C844
		internal static MethodInfo ReadXmlValueMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.readXmlValueMethod == null)
				{
					XmlFormatGeneratorStatics.readXmlValueMethod = typeof(DataContract).GetMethod("ReadXmlValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.readXmlValueMethod;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0003E673 File Offset: 0x0003C873
		internal static MethodInfo ThrowTypeNotSerializableMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.throwTypeNotSerializableMethod == null)
				{
					XmlFormatGeneratorStatics.throwTypeNotSerializableMethod = typeof(DataContract).GetMethod("ThrowTypeNotSerializable", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.throwTypeNotSerializableMethod;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0003E6A2 File Offset: 0x0003C8A2
		internal static PropertyInfo NamespaceProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.namespaceProperty == null)
				{
					XmlFormatGeneratorStatics.namespaceProperty = typeof(DataContract).GetProperty("Namespace", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.namespaceProperty;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003E6D1 File Offset: 0x0003C8D1
		internal static FieldInfo ContractNamespacesField
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.contractNamespacesField == null)
				{
					XmlFormatGeneratorStatics.contractNamespacesField = typeof(ClassDataContract).GetField("ContractNamespaces", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.contractNamespacesField;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003E700 File Offset: 0x0003C900
		internal static FieldInfo MemberNamesField
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.memberNamesField == null)
				{
					XmlFormatGeneratorStatics.memberNamesField = typeof(ClassDataContract).GetField("MemberNames", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.memberNamesField;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0003E72F File Offset: 0x0003C92F
		internal static MethodInfo ExtensionDataSetExplicitMethodInfo
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.extensionDataSetExplicitMethodInfo == null)
				{
					XmlFormatGeneratorStatics.extensionDataSetExplicitMethodInfo = typeof(IExtensibleDataObject).GetMethod("set_ExtensionData");
				}
				return XmlFormatGeneratorStatics.extensionDataSetExplicitMethodInfo;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0003E75C File Offset: 0x0003C95C
		internal static PropertyInfo ChildElementNamespacesProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.childElementNamespacesProperty == null)
				{
					XmlFormatGeneratorStatics.childElementNamespacesProperty = typeof(ClassDataContract).GetProperty("ChildElementNamespaces", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.childElementNamespacesProperty;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003E78B File Offset: 0x0003C98B
		internal static PropertyInfo CollectionItemNameProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.collectionItemNameProperty == null)
				{
					XmlFormatGeneratorStatics.collectionItemNameProperty = typeof(CollectionDataContract).GetProperty("CollectionItemName", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.collectionItemNameProperty;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0003E7BA File Offset: 0x0003C9BA
		internal static PropertyInfo ChildElementNamespaceProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.childElementNamespaceProperty == null)
				{
					XmlFormatGeneratorStatics.childElementNamespaceProperty = typeof(CollectionDataContract).GetProperty("ChildElementNamespace", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.childElementNamespaceProperty;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003E7E9 File Offset: 0x0003C9E9
		internal static MethodInfo GetDateTimeOffsetMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getDateTimeOffsetMethod == null)
				{
					XmlFormatGeneratorStatics.getDateTimeOffsetMethod = typeof(DateTimeOffsetAdapter).GetMethod("GetDateTimeOffset", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getDateTimeOffsetMethod;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0003E818 File Offset: 0x0003CA18
		internal static MethodInfo GetDateTimeOffsetAdapterMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.getDateTimeOffsetAdapterMethod == null)
				{
					XmlFormatGeneratorStatics.getDateTimeOffsetAdapterMethod = typeof(DateTimeOffsetAdapter).GetMethod("GetDateTimeOffsetAdapter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.getDateTimeOffsetAdapterMethod;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003E847 File Offset: 0x0003CA47
		internal static MethodInfo TraceInstructionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.traceInstructionMethod == null)
				{
					XmlFormatGeneratorStatics.traceInstructionMethod = typeof(SerializationTrace).GetMethod("TraceInstruction", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.traceInstructionMethod;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003E878 File Offset: 0x0003CA78
		internal static MethodInfo ThrowInvalidDataContractExceptionMethod
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.throwInvalidDataContractExceptionMethod == null)
				{
					XmlFormatGeneratorStatics.throwInvalidDataContractExceptionMethod = typeof(DataContract).GetMethod("ThrowInvalidDataContractException", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(string),
						typeof(Type)
					}, null);
				}
				return XmlFormatGeneratorStatics.throwInvalidDataContractExceptionMethod;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003E8D4 File Offset: 0x0003CAD4
		internal static PropertyInfo SerializeReadOnlyTypesProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.serializeReadOnlyTypesProperty == null)
				{
					XmlFormatGeneratorStatics.serializeReadOnlyTypesProperty = typeof(XmlObjectSerializerWriteContext).GetProperty("SerializeReadOnlyTypes", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.serializeReadOnlyTypesProperty;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0003E903 File Offset: 0x0003CB03
		internal static PropertyInfo ClassSerializationExceptionMessageProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.classSerializationExceptionMessageProperty == null)
				{
					XmlFormatGeneratorStatics.classSerializationExceptionMessageProperty = typeof(ClassDataContract).GetProperty("SerializationExceptionMessage", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.classSerializationExceptionMessageProperty;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0003E932 File Offset: 0x0003CB32
		internal static PropertyInfo CollectionSerializationExceptionMessageProperty
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlFormatGeneratorStatics.collectionSerializationExceptionMessageProperty == null)
				{
					XmlFormatGeneratorStatics.collectionSerializationExceptionMessageProperty = typeof(CollectionDataContract).GetProperty("SerializationExceptionMessage", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlFormatGeneratorStatics.collectionSerializationExceptionMessageProperty;
			}
		}

		// Token: 0x0400069B RID: 1691
		[SecurityCritical]
		private static MethodInfo writeStartElementMethod2;

		// Token: 0x0400069C RID: 1692
		[SecurityCritical]
		private static MethodInfo writeStartElementMethod3;

		// Token: 0x0400069D RID: 1693
		[SecurityCritical]
		private static MethodInfo writeEndElementMethod;

		// Token: 0x0400069E RID: 1694
		[SecurityCritical]
		private static MethodInfo writeNamespaceDeclMethod;

		// Token: 0x0400069F RID: 1695
		[SecurityCritical]
		private static PropertyInfo extensionDataProperty;

		// Token: 0x040006A0 RID: 1696
		[SecurityCritical]
		private static MethodInfo boxPointer;

		// Token: 0x040006A1 RID: 1697
		[SecurityCritical]
		private static ConstructorInfo dictionaryEnumeratorCtor;

		// Token: 0x040006A2 RID: 1698
		[SecurityCritical]
		private static MethodInfo ienumeratorMoveNextMethod;

		// Token: 0x040006A3 RID: 1699
		[SecurityCritical]
		private static MethodInfo ienumeratorGetCurrentMethod;

		// Token: 0x040006A4 RID: 1700
		[SecurityCritical]
		private static MethodInfo getItemContractMethod;

		// Token: 0x040006A5 RID: 1701
		[SecurityCritical]
		private static MethodInfo isStartElementMethod2;

		// Token: 0x040006A6 RID: 1702
		[SecurityCritical]
		private static MethodInfo isStartElementMethod0;

		// Token: 0x040006A7 RID: 1703
		[SecurityCritical]
		private static MethodInfo getUninitializedObjectMethod;

		// Token: 0x040006A8 RID: 1704
		[SecurityCritical]
		private static MethodInfo onDeserializationMethod;

		// Token: 0x040006A9 RID: 1705
		[SecurityCritical]
		private static MethodInfo unboxPointer;

		// Token: 0x040006AA RID: 1706
		[SecurityCritical]
		private static PropertyInfo nodeTypeProperty;

		// Token: 0x040006AB RID: 1707
		[SecurityCritical]
		private static ConstructorInfo serializationExceptionCtor;

		// Token: 0x040006AC RID: 1708
		[SecurityCritical]
		private static ConstructorInfo extensionDataObjectCtor;

		// Token: 0x040006AD RID: 1709
		[SecurityCritical]
		private static ConstructorInfo hashtableCtor;

		// Token: 0x040006AE RID: 1710
		[SecurityCritical]
		private static MethodInfo getStreamingContextMethod;

		// Token: 0x040006AF RID: 1711
		[SecurityCritical]
		private static MethodInfo getCollectionMemberMethod;

		// Token: 0x040006B0 RID: 1712
		[SecurityCritical]
		private static MethodInfo storeCollectionMemberInfoMethod;

		// Token: 0x040006B1 RID: 1713
		[SecurityCritical]
		private static MethodInfo storeIsGetOnlyCollectionMethod;

		// Token: 0x040006B2 RID: 1714
		[SecurityCritical]
		private static MethodInfo throwNullValueReturnedForGetOnlyCollectionExceptionMethod;

		// Token: 0x040006B3 RID: 1715
		private static MethodInfo throwArrayExceededSizeExceptionMethod;

		// Token: 0x040006B4 RID: 1716
		[SecurityCritical]
		private static MethodInfo incrementItemCountMethod;

		// Token: 0x040006B5 RID: 1717
		[SecurityCritical]
		private static MethodInfo demandSerializationFormatterPermissionMethod;

		// Token: 0x040006B6 RID: 1718
		[SecurityCritical]
		private static MethodInfo demandMemberAccessPermissionMethod;

		// Token: 0x040006B7 RID: 1719
		[SecurityCritical]
		private static MethodInfo internalDeserializeMethod;

		// Token: 0x040006B8 RID: 1720
		[SecurityCritical]
		private static MethodInfo moveToNextElementMethod;

		// Token: 0x040006B9 RID: 1721
		[SecurityCritical]
		private static MethodInfo getMemberIndexMethod;

		// Token: 0x040006BA RID: 1722
		[SecurityCritical]
		private static MethodInfo getMemberIndexWithRequiredMembersMethod;

		// Token: 0x040006BB RID: 1723
		[SecurityCritical]
		private static MethodInfo throwRequiredMemberMissingExceptionMethod;

		// Token: 0x040006BC RID: 1724
		[SecurityCritical]
		private static MethodInfo skipUnknownElementMethod;

		// Token: 0x040006BD RID: 1725
		[SecurityCritical]
		private static MethodInfo readIfNullOrRefMethod;

		// Token: 0x040006BE RID: 1726
		[SecurityCritical]
		private static MethodInfo readAttributesMethod;

		// Token: 0x040006BF RID: 1727
		[SecurityCritical]
		private static MethodInfo resetAttributesMethod;

		// Token: 0x040006C0 RID: 1728
		[SecurityCritical]
		private static MethodInfo getObjectIdMethod;

		// Token: 0x040006C1 RID: 1729
		[SecurityCritical]
		private static MethodInfo getArraySizeMethod;

		// Token: 0x040006C2 RID: 1730
		[SecurityCritical]
		private static MethodInfo addNewObjectMethod;

		// Token: 0x040006C3 RID: 1731
		[SecurityCritical]
		private static MethodInfo addNewObjectWithIdMethod;

		// Token: 0x040006C4 RID: 1732
		[SecurityCritical]
		private static MethodInfo replaceDeserializedObjectMethod;

		// Token: 0x040006C5 RID: 1733
		[SecurityCritical]
		private static MethodInfo getExistingObjectMethod;

		// Token: 0x040006C6 RID: 1734
		[SecurityCritical]
		private static MethodInfo getRealObjectMethod;

		// Token: 0x040006C7 RID: 1735
		[SecurityCritical]
		private static MethodInfo readMethod;

		// Token: 0x040006C8 RID: 1736
		[SecurityCritical]
		private static MethodInfo ensureArraySizeMethod;

		// Token: 0x040006C9 RID: 1737
		[SecurityCritical]
		private static MethodInfo trimArraySizeMethod;

		// Token: 0x040006CA RID: 1738
		[SecurityCritical]
		private static MethodInfo checkEndOfArrayMethod;

		// Token: 0x040006CB RID: 1739
		[SecurityCritical]
		private static MethodInfo getArrayLengthMethod;

		// Token: 0x040006CC RID: 1740
		[SecurityCritical]
		private static MethodInfo readSerializationInfoMethod;

		// Token: 0x040006CD RID: 1741
		[SecurityCritical]
		private static MethodInfo createUnexpectedStateExceptionMethod;

		// Token: 0x040006CE RID: 1742
		[SecurityCritical]
		private static MethodInfo internalSerializeReferenceMethod;

		// Token: 0x040006CF RID: 1743
		[SecurityCritical]
		private static MethodInfo internalSerializeMethod;

		// Token: 0x040006D0 RID: 1744
		[SecurityCritical]
		private static MethodInfo writeNullMethod;

		// Token: 0x040006D1 RID: 1745
		[SecurityCritical]
		private static MethodInfo incrementArrayCountMethod;

		// Token: 0x040006D2 RID: 1746
		[SecurityCritical]
		private static MethodInfo incrementCollectionCountMethod;

		// Token: 0x040006D3 RID: 1747
		[SecurityCritical]
		private static MethodInfo incrementCollectionCountGenericMethod;

		// Token: 0x040006D4 RID: 1748
		[SecurityCritical]
		private static MethodInfo getDefaultValueMethod;

		// Token: 0x040006D5 RID: 1749
		[SecurityCritical]
		private static MethodInfo getNullableValueMethod;

		// Token: 0x040006D6 RID: 1750
		[SecurityCritical]
		private static MethodInfo throwRequiredMemberMustBeEmittedMethod;

		// Token: 0x040006D7 RID: 1751
		[SecurityCritical]
		private static MethodInfo getHasValueMethod;

		// Token: 0x040006D8 RID: 1752
		[SecurityCritical]
		private static MethodInfo writeISerializableMethod;

		// Token: 0x040006D9 RID: 1753
		[SecurityCritical]
		private static MethodInfo writeExtensionDataMethod;

		// Token: 0x040006DA RID: 1754
		[SecurityCritical]
		private static MethodInfo writeXmlValueMethod;

		// Token: 0x040006DB RID: 1755
		[SecurityCritical]
		private static MethodInfo readXmlValueMethod;

		// Token: 0x040006DC RID: 1756
		[SecurityCritical]
		private static MethodInfo throwTypeNotSerializableMethod;

		// Token: 0x040006DD RID: 1757
		[SecurityCritical]
		private static PropertyInfo namespaceProperty;

		// Token: 0x040006DE RID: 1758
		[SecurityCritical]
		private static FieldInfo contractNamespacesField;

		// Token: 0x040006DF RID: 1759
		[SecurityCritical]
		private static FieldInfo memberNamesField;

		// Token: 0x040006E0 RID: 1760
		[SecurityCritical]
		private static MethodInfo extensionDataSetExplicitMethodInfo;

		// Token: 0x040006E1 RID: 1761
		[SecurityCritical]
		private static PropertyInfo childElementNamespacesProperty;

		// Token: 0x040006E2 RID: 1762
		[SecurityCritical]
		private static PropertyInfo collectionItemNameProperty;

		// Token: 0x040006E3 RID: 1763
		[SecurityCritical]
		private static PropertyInfo childElementNamespaceProperty;

		// Token: 0x040006E4 RID: 1764
		[SecurityCritical]
		private static MethodInfo getDateTimeOffsetMethod;

		// Token: 0x040006E5 RID: 1765
		[SecurityCritical]
		private static MethodInfo getDateTimeOffsetAdapterMethod;

		// Token: 0x040006E6 RID: 1766
		[SecurityCritical]
		private static MethodInfo traceInstructionMethod;

		// Token: 0x040006E7 RID: 1767
		[SecurityCritical]
		private static MethodInfo throwInvalidDataContractExceptionMethod;

		// Token: 0x040006E8 RID: 1768
		[SecurityCritical]
		private static PropertyInfo serializeReadOnlyTypesProperty;

		// Token: 0x040006E9 RID: 1769
		[SecurityCritical]
		private static PropertyInfo classSerializationExceptionMessageProperty;

		// Token: 0x040006EA RID: 1770
		[SecurityCritical]
		private static PropertyInfo collectionSerializationExceptionMessageProperty;
	}
}
