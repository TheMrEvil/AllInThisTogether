using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000146 RID: 326
	internal class XmlObjectSerializerContext
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x0003F561 File Offset: 0x0003D761
		internal XmlObjectSerializerContext(XmlObjectSerializer serializer, int maxItemsInObjectGraph, StreamingContext streamingContext, bool ignoreExtensionDataObject, DataContractResolver dataContractResolver)
		{
			this.serializer = serializer;
			this.itemCount = 1;
			this.maxItemsInObjectGraph = maxItemsInObjectGraph;
			this.streamingContext = streamingContext;
			this.ignoreExtensionDataObject = ignoreExtensionDataObject;
			this.dataContractResolver = dataContractResolver;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003F595 File Offset: 0x0003D795
		internal XmlObjectSerializerContext(XmlObjectSerializer serializer, int maxItemsInObjectGraph, StreamingContext streamingContext, bool ignoreExtensionDataObject) : this(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject, null)
		{
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003F5A3 File Offset: 0x0003D7A3
		internal XmlObjectSerializerContext(DataContractSerializer serializer, DataContract rootTypeDataContract, DataContractResolver dataContractResolver) : this(serializer, serializer.MaxItemsInObjectGraph, new StreamingContext(StreamingContextStates.All), serializer.IgnoreExtensionDataObject, dataContractResolver)
		{
			this.rootTypeDataContract = rootTypeDataContract;
			this.serializerKnownTypeList = serializer.knownTypeList;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003F5D6 File Offset: 0x0003D7D6
		internal XmlObjectSerializerContext(NetDataContractSerializer serializer) : this(serializer, serializer.MaxItemsInObjectGraph, serializer.Context, serializer.IgnoreExtensionDataObject)
		{
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00003127 File Offset: 0x00001327
		internal virtual SerializationMode Mode
		{
			get
			{
				return SerializationMode.SharedContract;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00003127 File Offset: 0x00001327
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal virtual bool IsGetOnlyCollection
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0000A8EE File Offset: 0x00008AEE
		[SecuritySafeCritical]
		public void DemandSerializationFormatterPermission()
		{
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0000A8EE File Offset: 0x00008AEE
		[SecuritySafeCritical]
		public void DemandMemberAccessPermission()
		{
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0003F5F1 File Offset: 0x0003D7F1
		public StreamingContext GetStreamingContext()
		{
			return this.streamingContext;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003F5F9 File Offset: 0x0003D7F9
		internal static MethodInfo IncrementItemCountMethod
		{
			get
			{
				if (XmlObjectSerializerContext.incrementItemCountMethod == null)
				{
					XmlObjectSerializerContext.incrementItemCountMethod = typeof(XmlObjectSerializerContext).GetMethod("IncrementItemCount", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return XmlObjectSerializerContext.incrementItemCountMethod;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003F628 File Offset: 0x0003D828
		public void IncrementItemCount(int count)
		{
			if (count > this.maxItemsInObjectGraph - this.itemCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Maximum number of items that can be serialized or deserialized in an object graph is '{0}'.", new object[]
				{
					this.maxItemsInObjectGraph
				})));
			}
			this.itemCount += count;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0003F67C File Offset: 0x0003D87C
		internal int RemainingItemCount
		{
			get
			{
				return this.maxItemsInObjectGraph - this.itemCount;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0003F68B File Offset: 0x0003D88B
		internal bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0003F693 File Offset: 0x0003D893
		protected DataContractResolver DataContractResolver
		{
			get
			{
				return this.dataContractResolver;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0003F69B File Offset: 0x0003D89B
		protected KnownTypeDataContractResolver KnownTypeResolver
		{
			get
			{
				if (this.knownTypeResolver == null)
				{
					this.knownTypeResolver = new KnownTypeDataContractResolver(this);
				}
				return this.knownTypeResolver;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003F6B7 File Offset: 0x0003D8B7
		internal DataContract GetDataContract(Type type)
		{
			return this.GetDataContract(type.TypeHandle, type);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003F6C6 File Offset: 0x0003D8C6
		internal virtual DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
		{
			if (this.IsGetOnlyCollection)
			{
				return DataContract.GetGetOnlyCollectionDataContract(DataContract.GetId(typeHandle), typeHandle, type, this.Mode);
			}
			return DataContract.GetDataContract(typeHandle, type, this.Mode);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003F6F1 File Offset: 0x0003D8F1
		internal virtual DataContract GetDataContractSkipValidation(int typeId, RuntimeTypeHandle typeHandle, Type type)
		{
			if (this.IsGetOnlyCollection)
			{
				return DataContract.GetGetOnlyCollectionDataContractSkipValidation(typeId, typeHandle, type);
			}
			return DataContract.GetDataContractSkipValidation(typeId, typeHandle, type);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003F70C File Offset: 0x0003D90C
		internal virtual DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
		{
			if (this.IsGetOnlyCollection)
			{
				return DataContract.GetGetOnlyCollectionDataContract(id, typeHandle, null, this.Mode);
			}
			return DataContract.GetDataContract(id, typeHandle, this.Mode);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003F732 File Offset: 0x0003D932
		internal virtual void CheckIfTypeSerializable(Type memberType, bool isMemberTypeSerializable)
		{
			if (!isMemberTypeSerializable)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot be serialized. Consider marking it with the DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute attribute. Alternatively, you can ensure that the type is public and has a parameterless constructor - all public members of the type will then be serialized, and no attributes will be required.", new object[]
				{
					memberType
				})));
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0000206B File Offset: 0x0000026B
		internal virtual Type GetSurrogatedType(Type type)
		{
			return type;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0003F756 File Offset: 0x0003D956
		private Dictionary<XmlQualifiedName, DataContract> SerializerKnownDataContracts
		{
			get
			{
				if (!this.isSerializerKnownDataContractsSetExplicit)
				{
					this.serializerKnownDataContracts = this.serializer.KnownDataContracts;
					this.isSerializerKnownDataContractsSetExplicit = true;
				}
				return this.serializerKnownDataContracts;
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003F780 File Offset: 0x0003D980
		private DataContract GetDataContractFromSerializerKnownTypes(XmlQualifiedName qname)
		{
			Dictionary<XmlQualifiedName, DataContract> dictionary = this.SerializerKnownDataContracts;
			if (dictionary == null)
			{
				return null;
			}
			DataContract result;
			if (!dictionary.TryGetValue(qname, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0003F7A8 File Offset: 0x0003D9A8
		internal static Dictionary<XmlQualifiedName, DataContract> GetDataContractsForKnownTypes(IList<Type> knownTypeList)
		{
			if (knownTypeList == null)
			{
				return null;
			}
			Dictionary<XmlQualifiedName, DataContract> result = new Dictionary<XmlQualifiedName, DataContract>();
			Dictionary<Type, Type> typesChecked = new Dictionary<Type, Type>();
			for (int i = 0; i < knownTypeList.Count; i++)
			{
				Type type = knownTypeList[i];
				if (type == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("One of the known types provided to the serializer via '{0}' argument was invalid because it was null. All known types specified must be non-null values.", new object[]
					{
						"knownTypes"
					})));
				}
				DataContract.CheckAndAdd(type, typesChecked, ref result);
			}
			return result;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0003F818 File Offset: 0x0003DA18
		internal bool IsKnownType(DataContract dataContract, Dictionary<XmlQualifiedName, DataContract> knownDataContracts, Type declaredType)
		{
			bool flag = false;
			if (knownDataContracts != null)
			{
				this.scopedKnownTypes.Push(knownDataContracts);
				flag = true;
			}
			bool result = this.IsKnownType(dataContract, declaredType);
			if (flag)
			{
				this.scopedKnownTypes.Pop();
			}
			return result;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0003F850 File Offset: 0x0003DA50
		internal bool IsKnownType(DataContract dataContract, Type declaredType)
		{
			DataContract dataContract2 = this.ResolveDataContractFromKnownTypes(dataContract.StableName.Name, dataContract.StableName.Namespace, null, declaredType);
			return dataContract2 != null && dataContract2.UnderlyingType == dataContract.UnderlyingType;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003F894 File Offset: 0x0003DA94
		private DataContract ResolveDataContractFromKnownTypes(XmlQualifiedName typeName)
		{
			DataContract dataContract = PrimitiveDataContract.GetPrimitiveDataContract(typeName.Name, typeName.Namespace);
			if (dataContract == null)
			{
				dataContract = this.scopedKnownTypes.GetDataContract(typeName);
				if (dataContract == null)
				{
					dataContract = this.GetDataContractFromSerializerKnownTypes(typeName);
				}
			}
			return dataContract;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003F8D0 File Offset: 0x0003DAD0
		private DataContract ResolveDataContractFromDataContractResolver(XmlQualifiedName typeName, Type declaredType)
		{
			if (TD.DCResolverResolveIsEnabled())
			{
				TD.DCResolverResolve(typeName.Name + ":" + typeName.Namespace);
			}
			Type type = this.DataContractResolver.ResolveName(typeName.Name, typeName.Namespace, declaredType, this.KnownTypeResolver);
			if (type == null)
			{
				return null;
			}
			return this.GetDataContract(type);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003F930 File Offset: 0x0003DB30
		internal Type ResolveNameFromKnownTypes(XmlQualifiedName typeName)
		{
			DataContract dataContract = this.ResolveDataContractFromKnownTypes(typeName);
			if (dataContract == null)
			{
				return null;
			}
			return dataContract.OriginalUnderlyingType;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003F950 File Offset: 0x0003DB50
		protected DataContract ResolveDataContractFromKnownTypes(string typeName, string typeNs, DataContract memberTypeContract, Type declaredType)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(typeName, typeNs);
			DataContract dataContract;
			if (this.DataContractResolver == null)
			{
				dataContract = this.ResolveDataContractFromKnownTypes(xmlQualifiedName);
			}
			else
			{
				dataContract = this.ResolveDataContractFromDataContractResolver(xmlQualifiedName, declaredType);
			}
			if (dataContract == null)
			{
				if (memberTypeContract != null && !memberTypeContract.UnderlyingType.IsInterface && memberTypeContract.StableName == xmlQualifiedName)
				{
					dataContract = memberTypeContract;
				}
				if (dataContract == null && this.rootTypeDataContract != null)
				{
					dataContract = this.ResolveDataContractFromRootDataContract(xmlQualifiedName);
				}
			}
			return dataContract;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
		protected virtual DataContract ResolveDataContractFromRootDataContract(XmlQualifiedName typeQName)
		{
			if (this.rootTypeDataContract.StableName == typeQName)
			{
				return this.rootTypeDataContract;
			}
			DataContract dataContract;
			for (CollectionDataContract collectionDataContract = this.rootTypeDataContract as CollectionDataContract; collectionDataContract != null; collectionDataContract = (dataContract as CollectionDataContract))
			{
				dataContract = this.GetDataContract(this.GetSurrogatedType(collectionDataContract.ItemType));
				if (dataContract.StableName == typeQName)
				{
					return dataContract;
				}
			}
			return null;
		}

		// Token: 0x040006F3 RID: 1779
		protected XmlObjectSerializer serializer;

		// Token: 0x040006F4 RID: 1780
		protected DataContract rootTypeDataContract;

		// Token: 0x040006F5 RID: 1781
		internal ScopedKnownTypes scopedKnownTypes;

		// Token: 0x040006F6 RID: 1782
		protected Dictionary<XmlQualifiedName, DataContract> serializerKnownDataContracts;

		// Token: 0x040006F7 RID: 1783
		private bool isSerializerKnownDataContractsSetExplicit;

		// Token: 0x040006F8 RID: 1784
		protected IList<Type> serializerKnownTypeList;

		// Token: 0x040006F9 RID: 1785
		[SecurityCritical]
		private bool demandedSerializationFormatterPermission;

		// Token: 0x040006FA RID: 1786
		[SecurityCritical]
		private bool demandedMemberAccessPermission;

		// Token: 0x040006FB RID: 1787
		private int itemCount;

		// Token: 0x040006FC RID: 1788
		private int maxItemsInObjectGraph;

		// Token: 0x040006FD RID: 1789
		private StreamingContext streamingContext;

		// Token: 0x040006FE RID: 1790
		private bool ignoreExtensionDataObject;

		// Token: 0x040006FF RID: 1791
		private DataContractResolver dataContractResolver;

		// Token: 0x04000700 RID: 1792
		private KnownTypeDataContractResolver knownTypeResolver;

		// Token: 0x04000701 RID: 1793
		private static MethodInfo incrementItemCountMethod;
	}
}
