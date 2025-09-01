using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000C8 RID: 200
	internal class DataContractSet
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x00030BC3 File Offset: 0x0002EDC3
		internal DataContractSet(IDataContractSurrogate dataContractSurrogate) : this(dataContractSurrogate, null, null)
		{
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00030BCE File Offset: 0x0002EDCE
		internal DataContractSet(IDataContractSurrogate dataContractSurrogate, ICollection<Type> referencedTypes, ICollection<Type> referencedCollectionTypes)
		{
			this.dataContractSurrogate = dataContractSurrogate;
			this.referencedTypes = referencedTypes;
			this.referencedCollectionTypes = referencedCollectionTypes;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00030BEC File Offset: 0x0002EDEC
		internal DataContractSet(DataContractSet dataContractSet)
		{
			if (dataContractSet == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("dataContractSet"));
			}
			this.dataContractSurrogate = dataContractSet.dataContractSurrogate;
			this.referencedTypes = dataContractSet.referencedTypes;
			this.referencedCollectionTypes = dataContractSet.referencedCollectionTypes;
			foreach (KeyValuePair<XmlQualifiedName, DataContract> keyValuePair in dataContractSet)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
			if (dataContractSet.processedContracts != null)
			{
				foreach (KeyValuePair<DataContract, object> keyValuePair2 in dataContractSet.processedContracts)
				{
					this.ProcessedContracts.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00030CDC File Offset: 0x0002EEDC
		private Dictionary<XmlQualifiedName, DataContract> Contracts
		{
			get
			{
				if (this.contracts == null)
				{
					this.contracts = new Dictionary<XmlQualifiedName, DataContract>();
				}
				return this.contracts;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00030CF7 File Offset: 0x0002EEF7
		private Dictionary<DataContract, object> ProcessedContracts
		{
			get
			{
				if (this.processedContracts == null)
				{
					this.processedContracts = new Dictionary<DataContract, object>();
				}
				return this.processedContracts;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00030D12 File Offset: 0x0002EF12
		private Hashtable SurrogateDataTable
		{
			get
			{
				if (this.surrogateDataTable == null)
				{
					this.surrogateDataTable = new Hashtable();
				}
				return this.surrogateDataTable;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00030D2D File Offset: 0x0002EF2D
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00030D35 File Offset: 0x0002EF35
		internal Dictionary<XmlQualifiedName, DataContract> KnownTypesForObject
		{
			get
			{
				return this.knownTypesForObject;
			}
			set
			{
				this.knownTypesForObject = value;
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00030D40 File Offset: 0x0002EF40
		internal void Add(Type type)
		{
			DataContract dataContract = this.GetDataContract(type);
			DataContractSet.EnsureTypeNotGeneric(dataContract.UnderlyingType);
			this.Add(dataContract);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00030D67 File Offset: 0x0002EF67
		internal static void EnsureTypeNotGeneric(Type type)
		{
			if (type.ContainsGenericParameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Generic type '{0}' is not exportable.", new object[]
				{
					type
				})));
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00030D90 File Offset: 0x0002EF90
		private void Add(DataContract dataContract)
		{
			this.Add(dataContract.StableName, dataContract);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00030D9F File Offset: 0x0002EF9F
		public void Add(XmlQualifiedName name, DataContract dataContract)
		{
			if (dataContract.IsBuiltInDataContract)
			{
				return;
			}
			this.InternalAdd(name, dataContract);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00030DB4 File Offset: 0x0002EFB4
		internal void InternalAdd(XmlQualifiedName name, DataContract dataContract)
		{
			DataContract dataContract2 = null;
			if (this.Contracts.TryGetValue(name, out dataContract2))
			{
				if (!dataContract2.Equals(dataContract))
				{
					if (dataContract.UnderlyingType == null || dataContract2.UnderlyingType == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Duplicate contract in data contract set was found, for '{0}' in '{1}' namespace.", new object[]
						{
							dataContract.StableName.Name,
							dataContract.StableName.Namespace
						})));
					}
					bool flag = DataContract.GetClrTypeFullName(dataContract.UnderlyingType) == DataContract.GetClrTypeFullName(dataContract2.UnderlyingType);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Duplicate type contract in data contract set. Type name '{0}', for data contract '{1}' in '{2}' namespace.", new object[]
					{
						flag ? dataContract.UnderlyingType.AssemblyQualifiedName : DataContract.GetClrTypeFullName(dataContract.UnderlyingType),
						flag ? dataContract2.UnderlyingType.AssemblyQualifiedName : DataContract.GetClrTypeFullName(dataContract2.UnderlyingType),
						dataContract.StableName.Name,
						dataContract.StableName.Namespace
					})));
				}
			}
			else
			{
				this.Contracts.Add(name, dataContract);
				if (dataContract is ClassDataContract)
				{
					this.AddClassDataContract((ClassDataContract)dataContract);
					return;
				}
				if (dataContract is CollectionDataContract)
				{
					this.AddCollectionDataContract((CollectionDataContract)dataContract);
					return;
				}
				if (dataContract is XmlDataContract)
				{
					this.AddXmlDataContract((XmlDataContract)dataContract);
				}
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00030F10 File Offset: 0x0002F110
		private void AddClassDataContract(ClassDataContract classDataContract)
		{
			if (classDataContract.BaseContract != null)
			{
				this.Add(classDataContract.BaseContract.StableName, classDataContract.BaseContract);
			}
			if (!classDataContract.IsISerializable && classDataContract.Members != null)
			{
				for (int i = 0; i < classDataContract.Members.Count; i++)
				{
					DataMember dataMember = classDataContract.Members[i];
					DataContract memberTypeDataContract = this.GetMemberTypeDataContract(dataMember);
					if (this.dataContractSurrogate != null && dataMember.MemberInfo != null)
					{
						object customDataToExport = DataContractSurrogateCaller.GetCustomDataToExport(this.dataContractSurrogate, dataMember.MemberInfo, memberTypeDataContract.UnderlyingType);
						if (customDataToExport != null)
						{
							this.SurrogateDataTable.Add(dataMember, customDataToExport);
						}
					}
					this.Add(memberTypeDataContract.StableName, memberTypeDataContract);
				}
			}
			this.AddKnownDataContracts(classDataContract.KnownDataContracts);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		private void AddCollectionDataContract(CollectionDataContract collectionDataContract)
		{
			if (collectionDataContract.IsDictionary)
			{
				ClassDataContract classDataContract = collectionDataContract.ItemContract as ClassDataContract;
				this.AddClassDataContract(classDataContract);
			}
			else
			{
				DataContract itemTypeDataContract = this.GetItemTypeDataContract(collectionDataContract);
				if (itemTypeDataContract != null)
				{
					this.Add(itemTypeDataContract.StableName, itemTypeDataContract);
				}
			}
			this.AddKnownDataContracts(collectionDataContract.KnownDataContracts);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003101E File Offset: 0x0002F21E
		private void AddXmlDataContract(XmlDataContract xmlDataContract)
		{
			this.AddKnownDataContracts(xmlDataContract.KnownDataContracts);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003102C File Offset: 0x0002F22C
		private void AddKnownDataContracts(Dictionary<XmlQualifiedName, DataContract> knownDataContracts)
		{
			if (knownDataContracts != null)
			{
				foreach (DataContract dataContract in knownDataContracts.Values)
				{
					this.Add(dataContract);
				}
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00031084 File Offset: 0x0002F284
		internal XmlQualifiedName GetStableName(Type clrType)
		{
			if (this.dataContractSurrogate != null)
			{
				return DataContract.GetStableName(DataContractSurrogateCaller.GetDataContractType(this.dataContractSurrogate, clrType));
			}
			return DataContract.GetStableName(clrType);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000310A8 File Offset: 0x0002F2A8
		internal DataContract GetDataContract(Type clrType)
		{
			if (this.dataContractSurrogate == null)
			{
				return DataContract.GetDataContract(clrType);
			}
			DataContract dataContract = DataContract.GetBuiltInDataContract(clrType);
			if (dataContract != null)
			{
				return dataContract;
			}
			Type dataContractType = DataContractSurrogateCaller.GetDataContractType(this.dataContractSurrogate, clrType);
			dataContract = DataContract.GetDataContract(dataContractType);
			if (!this.SurrogateDataTable.Contains(dataContract))
			{
				object customDataToExport = DataContractSurrogateCaller.GetCustomDataToExport(this.dataContractSurrogate, clrType, dataContractType);
				if (customDataToExport != null)
				{
					this.SurrogateDataTable.Add(dataContract, customDataToExport);
				}
			}
			return dataContract;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00031114 File Offset: 0x0002F314
		internal DataContract GetMemberTypeDataContract(DataMember dataMember)
		{
			if (!(dataMember.MemberInfo != null))
			{
				return dataMember.MemberTypeContract;
			}
			Type memberType = dataMember.MemberType;
			if (!dataMember.IsGetOnlyCollection)
			{
				return this.GetDataContract(memberType);
			}
			if (this.dataContractSurrogate != null && DataContractSurrogateCaller.GetDataContractType(this.dataContractSurrogate, memberType) != memberType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Surrogates with get-only collections are not supported. Type '{1}' contains '{2}' which is of '{0}' type.", new object[]
				{
					DataContract.GetClrTypeFullName(memberType),
					DataContract.GetClrTypeFullName(dataMember.MemberInfo.DeclaringType),
					dataMember.MemberInfo.Name
				})));
			}
			return DataContract.GetGetOnlyCollectionDataContract(DataContract.GetId(memberType.TypeHandle), memberType.TypeHandle, memberType, SerializationMode.SharedContract);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000311C9 File Offset: 0x0002F3C9
		internal DataContract GetItemTypeDataContract(CollectionDataContract collectionContract)
		{
			if (collectionContract.ItemType != null)
			{
				return this.GetDataContract(collectionContract.ItemType);
			}
			return collectionContract.ItemContract;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000311EC File Offset: 0x0002F3EC
		internal object GetSurrogateData(object key)
		{
			return this.SurrogateDataTable[key];
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000311FA File Offset: 0x0002F3FA
		internal void SetSurrogateData(object key, object surrogateData)
		{
			this.SurrogateDataTable[key] = surrogateData;
		}

		// Token: 0x170001FA RID: 506
		public DataContract this[XmlQualifiedName key]
		{
			get
			{
				DataContract builtInDataContract = DataContract.GetBuiltInDataContract(key.Name, key.Namespace);
				if (builtInDataContract == null)
				{
					this.Contracts.TryGetValue(key, out builtInDataContract);
				}
				return builtInDataContract;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0003123E File Offset: 0x0002F43E
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.dataContractSurrogate;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00031246 File Offset: 0x0002F446
		public bool Remove(XmlQualifiedName key)
		{
			return DataContract.GetBuiltInDataContract(key.Name, key.Namespace) == null && this.Contracts.Remove(key);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00031269 File Offset: 0x0002F469
		public IEnumerator<KeyValuePair<XmlQualifiedName, DataContract>> GetEnumerator()
		{
			return this.Contracts.GetEnumerator();
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0003127B File Offset: 0x0002F47B
		internal bool IsContractProcessed(DataContract dataContract)
		{
			return this.ProcessedContracts.ContainsKey(dataContract);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00031289 File Offset: 0x0002F489
		internal void SetContractProcessed(DataContract dataContract)
		{
			this.ProcessedContracts.Add(dataContract, dataContract);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00031298 File Offset: 0x0002F498
		internal ContractCodeDomInfo GetContractCodeDomInfo(DataContract dataContract)
		{
			object obj;
			if (this.ProcessedContracts.TryGetValue(dataContract, out obj))
			{
				return (ContractCodeDomInfo)obj;
			}
			return null;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000312BD File Offset: 0x0002F4BD
		internal void SetContractCodeDomInfo(DataContract dataContract, ContractCodeDomInfo info)
		{
			this.ProcessedContracts.Add(dataContract, info);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000312CC File Offset: 0x0002F4CC
		private Dictionary<XmlQualifiedName, object> GetReferencedTypes()
		{
			if (this.referencedTypesDictionary == null)
			{
				this.referencedTypesDictionary = new Dictionary<XmlQualifiedName, object>();
				this.referencedTypesDictionary.Add(DataContract.GetStableName(Globals.TypeOfNullable), Globals.TypeOfNullable);
				if (this.referencedTypes != null)
				{
					foreach (Type type in this.referencedTypes)
					{
						if (type == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Referenced types cannot contain null.")));
						}
						this.AddReferencedType(this.referencedTypesDictionary, type);
					}
				}
			}
			return this.referencedTypesDictionary;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003137C File Offset: 0x0002F57C
		private Dictionary<XmlQualifiedName, object> GetReferencedCollectionTypes()
		{
			if (this.referencedCollectionTypesDictionary == null)
			{
				this.referencedCollectionTypesDictionary = new Dictionary<XmlQualifiedName, object>();
				if (this.referencedCollectionTypes != null)
				{
					foreach (Type type in this.referencedCollectionTypes)
					{
						if (type == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Referenced collection types cannot contain null.")));
						}
						this.AddReferencedType(this.referencedCollectionTypesDictionary, type);
					}
				}
				XmlQualifiedName stableName = DataContract.GetStableName(Globals.TypeOfDictionaryGeneric);
				if (!this.referencedCollectionTypesDictionary.ContainsKey(stableName) && this.GetReferencedTypes().ContainsKey(stableName))
				{
					this.AddReferencedType(this.referencedCollectionTypesDictionary, Globals.TypeOfDictionaryGeneric);
				}
			}
			return this.referencedCollectionTypesDictionary;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003144C File Offset: 0x0002F64C
		private void AddReferencedType(Dictionary<XmlQualifiedName, object> referencedTypes, Type type)
		{
			if (DataContractSet.IsTypeReferenceable(type))
			{
				XmlQualifiedName stableName;
				try
				{
					stableName = this.GetStableName(type);
				}
				catch (InvalidDataContractException)
				{
					return;
				}
				catch (InvalidOperationException)
				{
					return;
				}
				object obj;
				if (referencedTypes.TryGetValue(stableName, out obj))
				{
					Type type2 = obj as Type;
					if (type2 != null)
					{
						if (type2 != type)
						{
							referencedTypes.Remove(stableName);
							referencedTypes.Add(stableName, new List<Type>
							{
								type2,
								type
							});
							return;
						}
					}
					else
					{
						List<Type> list = (List<Type>)obj;
						if (!list.Contains(type))
						{
							list.Add(type);
							return;
						}
					}
				}
				else
				{
					referencedTypes.Add(stableName, type);
				}
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000314FC File Offset: 0x0002F6FC
		internal bool TryGetReferencedType(XmlQualifiedName stableName, DataContract dataContract, out Type type)
		{
			return this.TryGetReferencedType(stableName, dataContract, false, out type);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00031508 File Offset: 0x0002F708
		internal bool TryGetReferencedCollectionType(XmlQualifiedName stableName, DataContract dataContract, out Type type)
		{
			return this.TryGetReferencedType(stableName, dataContract, true, out type);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00031514 File Offset: 0x0002F714
		private bool TryGetReferencedType(XmlQualifiedName stableName, DataContract dataContract, bool useReferencedCollectionTypes, out Type type)
		{
			object obj;
			if (!(useReferencedCollectionTypes ? this.GetReferencedCollectionTypes() : this.GetReferencedTypes()).TryGetValue(stableName, out obj))
			{
				type = null;
				return false;
			}
			type = (obj as Type);
			if (type != null)
			{
				return true;
			}
			List<Type> list = (List<Type>)obj;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				Type type2 = list[i];
				if (!flag)
				{
					flag = type2.IsGenericTypeDefinition;
				}
				stringBuilder.AppendFormat("{0}\"{1}\" ", Environment.NewLine, type2.AssemblyQualifiedName);
				if (dataContract != null)
				{
					DataContract dataContract2 = this.GetDataContract(type2);
					stringBuilder.Append(SR.GetString((dataContract2 != null && dataContract2.Equals(dataContract)) ? "Reference type matches." : "Reference type does not match."));
				}
			}
			if (flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString(useReferencedCollectionTypes ? "Ambiguous collection types were referenced: {0}" : "Ambiguous types were referenced: {0}", new object[]
				{
					stringBuilder.ToString()
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString(useReferencedCollectionTypes ? "In '{0}' element in '{1}' namespace, ambiguous collection types were referenced: {2}" : "In '{0}' element in '{1}' namespace, ambiguous types were referenced: {2}", new object[]
			{
				XmlConvert.DecodeName(stableName.Name),
				stableName.Namespace,
				stringBuilder.ToString()
			})));
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00031654 File Offset: 0x0002F854
		private static bool IsTypeReferenceable(Type type)
		{
			try
			{
				Type type2;
				return type.IsSerializable || type.IsDefined(Globals.TypeOfDataContractAttribute, false) || (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type) && !type.IsGenericTypeDefinition) || CollectionDataContract.IsCollection(type, out type2) || ClassDataContract.IsNonAttributedTypeValidForSerialization(type);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x040004AC RID: 1196
		private Dictionary<XmlQualifiedName, DataContract> contracts;

		// Token: 0x040004AD RID: 1197
		private Dictionary<DataContract, object> processedContracts;

		// Token: 0x040004AE RID: 1198
		private IDataContractSurrogate dataContractSurrogate;

		// Token: 0x040004AF RID: 1199
		private Hashtable surrogateDataTable;

		// Token: 0x040004B0 RID: 1200
		private Dictionary<XmlQualifiedName, DataContract> knownTypesForObject;

		// Token: 0x040004B1 RID: 1201
		private ICollection<Type> referencedTypes;

		// Token: 0x040004B2 RID: 1202
		private ICollection<Type> referencedCollectionTypes;

		// Token: 0x040004B3 RID: 1203
		private Dictionary<XmlQualifiedName, object> referencedTypesDictionary;

		// Token: 0x040004B4 RID: 1204
		private Dictionary<XmlQualifiedName, object> referencedCollectionTypesDictionary;
	}
}
