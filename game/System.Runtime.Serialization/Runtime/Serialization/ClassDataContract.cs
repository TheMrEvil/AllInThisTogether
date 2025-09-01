using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000AC RID: 172
	internal sealed class ClassDataContract : DataContract
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x000258C9 File Offset: 0x00023AC9
		[SecuritySafeCritical]
		internal ClassDataContract() : base(new ClassDataContract.ClassDataContractCriticalHelper())
		{
			this.InitClassDataContract();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000258DC File Offset: 0x00023ADC
		[SecuritySafeCritical]
		internal ClassDataContract(Type type) : base(new ClassDataContract.ClassDataContractCriticalHelper(type))
		{
			this.InitClassDataContract();
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000258F0 File Offset: 0x00023AF0
		[SecuritySafeCritical]
		private ClassDataContract(Type type, XmlDictionaryString ns, string[] memberNames) : base(new ClassDataContract.ClassDataContractCriticalHelper(type, ns, memberNames))
		{
			this.InitClassDataContract();
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00025908 File Offset: 0x00023B08
		[SecurityCritical]
		private void InitClassDataContract()
		{
			this.helper = (base.Helper as ClassDataContract.ClassDataContractCriticalHelper);
			this.ContractNamespaces = this.helper.ContractNamespaces;
			this.MemberNames = this.helper.MemberNames;
			this.MemberNamespaces = this.helper.MemberNamespaces;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00025959 File Offset: 0x00023B59
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00025966 File Offset: 0x00023B66
		internal ClassDataContract BaseContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.BaseContract;
			}
			[SecurityCritical]
			set
			{
				this.helper.BaseContract = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00025974 File Offset: 0x00023B74
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00025981 File Offset: 0x00023B81
		internal List<DataMember> Members
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.Members;
			}
			[SecurityCritical]
			set
			{
				this.helper.Members = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00025990 File Offset: 0x00023B90
		public XmlDictionaryString[] ChildElementNamespaces
		{
			[SecuritySafeCritical]
			get
			{
				if (this.childElementNamespaces == null)
				{
					lock (this)
					{
						if (this.childElementNamespaces == null)
						{
							if (this.helper.ChildElementNamespaces == null)
							{
								XmlDictionaryString[] array = this.CreateChildElementNamespaces();
								Thread.MemoryBarrier();
								this.helper.ChildElementNamespaces = array;
							}
							this.childElementNamespaces = this.helper.ChildElementNamespaces;
						}
					}
				}
				return this.childElementNamespaces;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00025A14 File Offset: 0x00023C14
		internal MethodInfo OnSerializing
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.OnSerializing;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00025A21 File Offset: 0x00023C21
		internal MethodInfo OnSerialized
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.OnSerialized;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00025A2E File Offset: 0x00023C2E
		internal MethodInfo OnDeserializing
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.OnDeserializing;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00025A3B File Offset: 0x00023C3B
		internal MethodInfo OnDeserialized
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.OnDeserialized;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00025A48 File Offset: 0x00023C48
		internal MethodInfo ExtensionDataSetMethod
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ExtensionDataSetMethod;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00025A55 File Offset: 0x00023C55
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00025A62 File Offset: 0x00023C62
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00025A70 File Offset: 0x00023C70
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00025A7D File Offset: 0x00023C7D
		internal override bool IsISerializable
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsISerializable;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsISerializable = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00025A8B File Offset: 0x00023C8B
		internal bool IsNonAttributedType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsNonAttributedType;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00025A98 File Offset: 0x00023C98
		internal bool HasDataContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.HasDataContract;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00025AA5 File Offset: 0x00023CA5
		internal bool HasExtensionData
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.HasExtensionData;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00025AB2 File Offset: 0x00023CB2
		internal string SerializationExceptionMessage
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.SerializationExceptionMessage;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00025ABF File Offset: 0x00023CBF
		internal string DeserializationExceptionMessage
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.DeserializationExceptionMessage;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00025ACC File Offset: 0x00023CCC
		internal bool IsReadOnlyContract
		{
			get
			{
				return this.DeserializationExceptionMessage != null;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00025AD7 File Offset: 0x00023CD7
		[SecuritySafeCritical]
		internal ConstructorInfo GetISerializableConstructor()
		{
			return this.helper.GetISerializableConstructor();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00025AE4 File Offset: 0x00023CE4
		[SecuritySafeCritical]
		internal ConstructorInfo GetNonAttributedTypeConstructor()
		{
			return this.helper.GetNonAttributedTypeConstructor();
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00025AF4 File Offset: 0x00023CF4
		internal XmlFormatClassWriterDelegate XmlFormatWriterDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.XmlFormatWriterDelegate == null)
				{
					lock (this)
					{
						if (this.helper.XmlFormatWriterDelegate == null)
						{
							XmlFormatClassWriterDelegate xmlFormatWriterDelegate = new XmlFormatWriterGenerator().GenerateClassWriter(this);
							Thread.MemoryBarrier();
							this.helper.XmlFormatWriterDelegate = xmlFormatWriterDelegate;
						}
					}
				}
				return this.helper.XmlFormatWriterDelegate;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00025B6C File Offset: 0x00023D6C
		internal XmlFormatClassReaderDelegate XmlFormatReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.XmlFormatReaderDelegate == null)
				{
					lock (this)
					{
						if (this.helper.XmlFormatReaderDelegate == null)
						{
							if (this.IsReadOnlyContract)
							{
								DataContract.ThrowInvalidDataContractException(this.helper.DeserializationExceptionMessage, null);
							}
							XmlFormatClassReaderDelegate xmlFormatReaderDelegate = new XmlFormatReaderGenerator().GenerateClassReader(this);
							Thread.MemoryBarrier();
							this.helper.XmlFormatReaderDelegate = xmlFormatReaderDelegate;
						}
					}
				}
				return this.helper.XmlFormatReaderDelegate;
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00025BFC File Offset: 0x00023DFC
		internal static ClassDataContract CreateClassDataContractForKeyValue(Type type, XmlDictionaryString ns, string[] memberNames)
		{
			return new ClassDataContract(type, ns, memberNames);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00025C08 File Offset: 0x00023E08
		internal static void CheckAndAddMember(List<DataMember> members, DataMember memberContract, Dictionary<string, DataMember> memberNamesTable)
		{
			DataMember dataMember;
			if (memberNamesTable.TryGetValue(memberContract.Name, out dataMember))
			{
				Type declaringType = memberContract.MemberInfo.DeclaringType;
				DataContract.ThrowInvalidDataContractException(SR.GetString(declaringType.IsEnum ? "Type '{2}' contains two members '{0}' 'and '{1}' with the same name '{3}'. Multiple members with the same name in one type are not supported. Consider changing one of the member names using EnumMemberAttribute attribute." : "Type '{2}' contains two members '{0}' 'and '{1}' with the same data member name '{3}'. Multiple members with the same name in one type are not supported. Consider changing one of the member names using DataMemberAttribute attribute.", new object[]
				{
					dataMember.MemberInfo.Name,
					memberContract.MemberInfo.Name,
					DataContract.GetClrTypeFullName(declaringType),
					memberContract.Name
				}), declaringType);
			}
			memberNamesTable.Add(memberContract.Name, memberContract);
			members.Add(memberContract);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00025C98 File Offset: 0x00023E98
		internal static XmlDictionaryString GetChildNamespaceToDeclare(DataContract dataContract, Type childType, XmlDictionary dictionary)
		{
			childType = DataContract.UnwrapNullableType(childType);
			if (!childType.IsEnum && !Globals.TypeOfIXmlSerializable.IsAssignableFrom(childType) && DataContract.GetBuiltInDataContract(childType) == null && childType != Globals.TypeOfDBNull)
			{
				string @namespace = DataContract.GetStableName(childType).Namespace;
				if (@namespace.Length > 0 && @namespace != dataContract.Namespace.Value)
				{
					return dictionary.Add(@namespace);
				}
			}
			return null;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00025D08 File Offset: 0x00023F08
		internal static bool IsNonAttributedTypeValidForSerialization(Type type)
		{
			if (type.IsArray)
			{
				return false;
			}
			if (type.IsEnum)
			{
				return false;
			}
			if (type.IsGenericParameter)
			{
				return false;
			}
			if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
			{
				return false;
			}
			if (type.IsPointer)
			{
				return false;
			}
			if (type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false))
			{
				return false;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (CollectionDataContract.IsCollectionInterface(interfaces[i]))
				{
					return false;
				}
			}
			if (type.IsSerializable)
			{
				return false;
			}
			if (Globals.TypeOfISerializable.IsAssignableFrom(type))
			{
				return false;
			}
			if (type.IsDefined(Globals.TypeOfDataContractAttribute, false))
			{
				return false;
			}
			if (type == Globals.TypeOfExtensionDataObject)
			{
				return false;
			}
			if (type.IsValueType)
			{
				return type.IsVisible;
			}
			return type.IsVisible && type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null) != null;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00025DE4 File Offset: 0x00023FE4
		private XmlDictionaryString[] CreateChildElementNamespaces()
		{
			if (this.Members == null)
			{
				return null;
			}
			XmlDictionaryString[] array = null;
			if (this.BaseContract != null)
			{
				array = this.BaseContract.ChildElementNamespaces;
			}
			int num = (array != null) ? array.Length : 0;
			XmlDictionaryString[] array2 = new XmlDictionaryString[this.Members.Count + num];
			if (num > 0)
			{
				Array.Copy(array, 0, array2, 0, array.Length);
			}
			XmlDictionary dictionary = new XmlDictionary();
			for (int i = 0; i < this.Members.Count; i++)
			{
				array2[i + num] = ClassDataContract.GetChildNamespaceToDeclare(this, this.Members[i].MemberType, dictionary);
			}
			return array2;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00025E7E File Offset: 0x0002407E
		[SecuritySafeCritical]
		private void EnsureMethodsImported()
		{
			this.helper.EnsureMethodsImported();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00025E8B File Offset: 0x0002408B
		public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			this.XmlFormatWriterDelegate(xmlWriter, obj, context, this);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00025E9C File Offset: 0x0002409C
		public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
		{
			xmlReader.Read();
			object result = this.XmlFormatReaderDelegate(xmlReader, context, this.MemberNames, this.MemberNamespaces);
			xmlReader.ReadEndElement();
			return result;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00025EC4 File Offset: 0x000240C4
		[SecuritySafeCritical]
		internal override DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
		{
			Type underlyingType = base.UnderlyingType;
			if (!underlyingType.IsGenericType || !underlyingType.ContainsGenericParameters)
			{
				return this;
			}
			DataContract result;
			lock (this)
			{
				DataContract dataContract;
				if (boundContracts.TryGetValue(this, out dataContract))
				{
					result = dataContract;
				}
				else
				{
					ClassDataContract classDataContract = new ClassDataContract();
					boundContracts.Add(this, classDataContract);
					XmlQualifiedName stableName;
					object[] array;
					if (underlyingType.IsGenericTypeDefinition)
					{
						stableName = base.StableName;
						array = paramContracts;
					}
					else
					{
						stableName = DataContract.GetStableName(underlyingType.GetGenericTypeDefinition());
						Type[] genericArguments = underlyingType.GetGenericArguments();
						array = new object[genericArguments.Length];
						for (int i = 0; i < genericArguments.Length; i++)
						{
							Type type = genericArguments[i];
							if (type.IsGenericParameter)
							{
								array[i] = paramContracts[type.GenericParameterPosition];
							}
							else
							{
								array[i] = type;
							}
						}
					}
					classDataContract.StableName = DataContract.CreateQualifiedName(DataContract.ExpandGenericParameters(XmlConvert.DecodeName(stableName.Name), new GenericNameProvider(DataContract.GetClrTypeFullName(base.UnderlyingType), array)), stableName.Namespace);
					if (this.BaseContract != null)
					{
						classDataContract.BaseContract = (ClassDataContract)this.BaseContract.BindGenericParameters(paramContracts, boundContracts);
					}
					classDataContract.IsISerializable = this.IsISerializable;
					classDataContract.IsValueType = base.IsValueType;
					classDataContract.IsReference = base.IsReference;
					if (this.Members != null)
					{
						classDataContract.Members = new List<DataMember>(this.Members.Count);
						foreach (DataMember dataMember in this.Members)
						{
							classDataContract.Members.Add(dataMember.BindGenericParameters(paramContracts, boundContracts));
						}
					}
					result = classDataContract;
				}
			}
			return result;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000260B8 File Offset: 0x000242B8
		internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (base.IsEqualOrChecked(other, checkedContracts))
			{
				return true;
			}
			if (base.Equals(other, checkedContracts))
			{
				ClassDataContract classDataContract = other as ClassDataContract;
				if (classDataContract != null)
				{
					if (this.IsISerializable)
					{
						if (!classDataContract.IsISerializable)
						{
							return false;
						}
					}
					else
					{
						if (classDataContract.IsISerializable)
						{
							return false;
						}
						if (this.Members == null)
						{
							if (classDataContract.Members != null && !this.IsEveryDataMemberOptional(classDataContract.Members))
							{
								return false;
							}
						}
						else if (classDataContract.Members == null)
						{
							if (!this.IsEveryDataMemberOptional(this.Members))
							{
								return false;
							}
						}
						else
						{
							Dictionary<string, DataMember> dictionary = new Dictionary<string, DataMember>(this.Members.Count);
							List<DataMember> list = new List<DataMember>();
							for (int i = 0; i < this.Members.Count; i++)
							{
								dictionary.Add(this.Members[i].Name, this.Members[i]);
							}
							for (int j = 0; j < classDataContract.Members.Count; j++)
							{
								DataMember dataMember;
								if (dictionary.TryGetValue(classDataContract.Members[j].Name, out dataMember))
								{
									if (!dataMember.Equals(classDataContract.Members[j], checkedContracts))
									{
										return false;
									}
									dictionary.Remove(dataMember.Name);
								}
								else
								{
									list.Add(classDataContract.Members[j]);
								}
							}
							if (!this.IsEveryDataMemberOptional(dictionary.Values))
							{
								return false;
							}
							if (!this.IsEveryDataMemberOptional(list))
							{
								return false;
							}
						}
					}
					if (this.BaseContract == null)
					{
						return classDataContract.BaseContract == null;
					}
					return classDataContract.BaseContract != null && this.BaseContract.Equals(classDataContract.BaseContract, checkedContracts);
				}
			}
			return false;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00026258 File Offset: 0x00024458
		private bool IsEveryDataMemberOptional(IEnumerable<DataMember> dataMembers)
		{
			using (IEnumerator<DataMember> enumerator = dataMembers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsRequired)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000262A8 File Offset: 0x000244A8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040003FB RID: 1019
		public XmlDictionaryString[] ContractNamespaces;

		// Token: 0x040003FC RID: 1020
		public XmlDictionaryString[] MemberNames;

		// Token: 0x040003FD RID: 1021
		public XmlDictionaryString[] MemberNamespaces;

		// Token: 0x040003FE RID: 1022
		[SecurityCritical]
		private XmlDictionaryString[] childElementNamespaces;

		// Token: 0x040003FF RID: 1023
		[SecurityCritical]
		private ClassDataContract.ClassDataContractCriticalHelper helper;

		// Token: 0x020000AD RID: 173
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class ClassDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x000262B0 File Offset: 0x000244B0
			internal ClassDataContractCriticalHelper()
			{
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x000262B8 File Offset: 0x000244B8
			internal ClassDataContractCriticalHelper(Type type) : base(type)
			{
				XmlQualifiedName stableNameAndSetHasDataContract = this.GetStableNameAndSetHasDataContract(type);
				if (type == Globals.TypeOfDBNull)
				{
					base.StableName = stableNameAndSetHasDataContract;
					this.members = new List<DataMember>();
					XmlDictionary xmlDictionary = new XmlDictionary(2);
					base.Name = xmlDictionary.Add(base.StableName.Name);
					base.Namespace = xmlDictionary.Add(base.StableName.Namespace);
					this.ContractNamespaces = (this.MemberNames = (this.MemberNamespaces = new XmlDictionaryString[0]));
					this.EnsureMethodsImported();
					return;
				}
				Type type2 = type.BaseType;
				this.isISerializable = Globals.TypeOfISerializable.IsAssignableFrom(type);
				this.SetIsNonAttributedType(type);
				if (this.isISerializable)
				{
					if (this.HasDataContract)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("ISerializable type '{0}' cannot have DataContract.", new object[]
						{
							DataContract.GetClrTypeFullName(type)
						})));
					}
					if (type2 != null && (!type2.IsSerializable || !Globals.TypeOfISerializable.IsAssignableFrom(type2)))
					{
						type2 = null;
					}
				}
				base.IsValueType = type.IsValueType;
				if (type2 != null && type2 != Globals.TypeOfObject && type2 != Globals.TypeOfValueType && type2 != Globals.TypeOfUri)
				{
					DataContract dataContract = DataContract.GetDataContract(type2);
					if (dataContract is CollectionDataContract)
					{
						this.BaseContract = (((CollectionDataContract)dataContract).SharedTypeContract as ClassDataContract);
					}
					else
					{
						this.BaseContract = (dataContract as ClassDataContract);
					}
					if (this.BaseContract != null && this.BaseContract.IsNonAttributedType && !this.isNonAttributedType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot inherit from a type that is not marked with DataContractAttribute or SerializableAttribute.  Consider marking the base type '{1}' with DataContractAttribute or SerializableAttribute, or removing them from the derived type.", new object[]
						{
							DataContract.GetClrTypeFullName(type),
							DataContract.GetClrTypeFullName(type2)
						})));
					}
				}
				else
				{
					this.BaseContract = null;
				}
				this.hasExtensionData = Globals.TypeOfIExtensibleDataObject.IsAssignableFrom(type);
				if (this.hasExtensionData && !this.HasDataContract && !this.IsNonAttributedType)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("On '{0}' type, only DataContract types can have extension data.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				if (this.isISerializable)
				{
					base.SetDataContractName(stableNameAndSetHasDataContract);
				}
				else
				{
					base.StableName = stableNameAndSetHasDataContract;
					this.ImportDataMembers();
					XmlDictionary xmlDictionary2 = new XmlDictionary(2 + this.Members.Count);
					base.Name = xmlDictionary2.Add(base.StableName.Name);
					base.Namespace = xmlDictionary2.Add(base.StableName.Namespace);
					int num = 0;
					int num2 = 0;
					if (this.BaseContract == null)
					{
						this.MemberNames = new XmlDictionaryString[this.Members.Count];
						this.MemberNamespaces = new XmlDictionaryString[this.Members.Count];
						this.ContractNamespaces = new XmlDictionaryString[1];
					}
					else
					{
						if (this.BaseContract.IsReadOnlyContract)
						{
							this.serializationExceptionMessage = this.BaseContract.SerializationExceptionMessage;
						}
						num = this.BaseContract.MemberNames.Length;
						this.MemberNames = new XmlDictionaryString[this.Members.Count + num];
						Array.Copy(this.BaseContract.MemberNames, this.MemberNames, num);
						this.MemberNamespaces = new XmlDictionaryString[this.Members.Count + num];
						Array.Copy(this.BaseContract.MemberNamespaces, this.MemberNamespaces, num);
						num2 = this.BaseContract.ContractNamespaces.Length;
						this.ContractNamespaces = new XmlDictionaryString[1 + num2];
						Array.Copy(this.BaseContract.ContractNamespaces, this.ContractNamespaces, num2);
					}
					this.ContractNamespaces[num2] = base.Namespace;
					for (int i = 0; i < this.Members.Count; i++)
					{
						this.MemberNames[i + num] = xmlDictionary2.Add(this.Members[i].Name);
						this.MemberNamespaces[i + num] = base.Namespace;
					}
				}
				this.EnsureMethodsImported();
			}

			// Token: 0x0600093F RID: 2367 RVA: 0x000266B8 File Offset: 0x000248B8
			internal ClassDataContractCriticalHelper(Type type, XmlDictionaryString ns, string[] memberNames) : base(type)
			{
				base.StableName = new XmlQualifiedName(this.GetStableNameAndSetHasDataContract(type).Name, ns.Value);
				this.ImportDataMembers();
				XmlDictionary xmlDictionary = new XmlDictionary(1 + this.Members.Count);
				base.Name = xmlDictionary.Add(base.StableName.Name);
				base.Namespace = ns;
				this.ContractNamespaces = new XmlDictionaryString[]
				{
					base.Namespace
				};
				this.MemberNames = new XmlDictionaryString[this.Members.Count];
				this.MemberNamespaces = new XmlDictionaryString[this.Members.Count];
				for (int i = 0; i < this.Members.Count; i++)
				{
					this.Members[i].Name = memberNames[i];
					this.MemberNames[i] = xmlDictionary.Add(this.Members[i].Name);
					this.MemberNamespaces[i] = base.Namespace;
				}
				this.EnsureMethodsImported();
			}

			// Token: 0x06000940 RID: 2368 RVA: 0x000267C0 File Offset: 0x000249C0
			private void EnsureIsReferenceImported(Type type)
			{
				bool flag = false;
				DataContractAttribute dataContractAttribute;
				bool flag2 = DataContract.TryGetDCAttribute(type, out dataContractAttribute);
				if (this.BaseContract != null)
				{
					if (flag2 && dataContractAttribute.IsReferenceSetExplicitly)
					{
						bool isReference = this.BaseContract.IsReference;
						if ((isReference && !dataContractAttribute.IsReference) || (!isReference && dataContractAttribute.IsReference))
						{
							DataContract.ThrowInvalidDataContractException(SR.GetString("The IsReference setting for type '{0}' is '{1}', but the same setting for its parent class '{2}' is '{3}'. Derived types must have the same value for IsReference as the base type. Change the setting on type '{0}' to '{3}', or on type '{2}' to '{1}', or do not set IsReference explicitly.", new object[]
							{
								DataContract.GetClrTypeFullName(type),
								dataContractAttribute.IsReference,
								DataContract.GetClrTypeFullName(this.BaseContract.UnderlyingType),
								this.BaseContract.IsReference
							}), type);
						}
						else
						{
							flag = dataContractAttribute.IsReference;
						}
					}
					else
					{
						flag = this.BaseContract.IsReference;
					}
				}
				else if (flag2 && dataContractAttribute.IsReference)
				{
					flag = dataContractAttribute.IsReference;
				}
				if (flag && type.IsValueType)
				{
					DataContract.ThrowInvalidDataContractException(SR.GetString("Value type '{0}' cannot have the IsReference setting of '{1}'. Either change the setting to '{2}', or remove it completely.", new object[]
					{
						DataContract.GetClrTypeFullName(type),
						true,
						false
					}), type);
					return;
				}
				base.IsReference = flag;
			}

			// Token: 0x06000941 RID: 2369 RVA: 0x000268D8 File Offset: 0x00024AD8
			private void ImportDataMembers()
			{
				Type underlyingType = base.UnderlyingType;
				this.EnsureIsReferenceImported(underlyingType);
				List<DataMember> list = new List<DataMember>();
				Dictionary<string, DataMember> memberNamesTable = new Dictionary<string, DataMember>();
				MemberInfo[] array;
				if (this.isNonAttributedType)
				{
					array = underlyingType.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
				}
				else
				{
					array = underlyingType.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				}
				foreach (MemberInfo memberInfo in array)
				{
					if (this.HasDataContract)
					{
						object[] customAttributes = memberInfo.GetCustomAttributes(typeof(DataMemberAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							if (customAttributes.Length > 1)
							{
								base.ThrowInvalidDataContractException(SR.GetString("Member '{0}.{1}' has more than one DataMemberAttribute attribute.", new object[]
								{
									DataContract.GetClrTypeFullName(memberInfo.DeclaringType),
									memberInfo.Name
								}));
							}
							DataMember dataMember = new DataMember(memberInfo);
							if (memberInfo.MemberType == MemberTypes.Property)
							{
								PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
								MethodInfo getMethod = propertyInfo.GetGetMethod(true);
								if (getMethod != null && ClassDataContract.ClassDataContractCriticalHelper.IsMethodOverriding(getMethod))
								{
									goto IL_53D;
								}
								MethodInfo setMethod = propertyInfo.GetSetMethod(true);
								if (setMethod != null && ClassDataContract.ClassDataContractCriticalHelper.IsMethodOverriding(setMethod))
								{
									goto IL_53D;
								}
								if (getMethod == null)
								{
									base.ThrowInvalidDataContractException(SR.GetString("No get method for property '{1}' in type '{0}'.", new object[]
									{
										propertyInfo.DeclaringType,
										propertyInfo.Name
									}));
								}
								if (setMethod == null && !this.SetIfGetOnlyCollection(dataMember, false))
								{
									this.serializationExceptionMessage = SR.GetString("No set method for property '{1}' in type '{0}'.", new object[]
									{
										propertyInfo.DeclaringType,
										propertyInfo.Name
									});
								}
								if (getMethod.GetParameters().Length != 0)
								{
									base.ThrowInvalidDataContractException(SR.GetString("Property '{1}' in type '{0}' cannot be serialized because serialization of indexed properties is not supported.", new object[]
									{
										propertyInfo.DeclaringType,
										propertyInfo.Name
									}));
								}
							}
							else if (memberInfo.MemberType != MemberTypes.Field)
							{
								base.ThrowInvalidDataContractException(SR.GetString("Member '{0}.{1}' cannot be serialized since it is neither a field nor a property, and therefore cannot be marked with the DataMemberAttribute attribute. Remove the DataMemberAttribute attribute from the '{1}' member.", new object[]
								{
									DataContract.GetClrTypeFullName(underlyingType),
									memberInfo.Name
								}));
							}
							DataMemberAttribute dataMemberAttribute = (DataMemberAttribute)customAttributes[0];
							if (dataMemberAttribute.IsNameSetExplicitly)
							{
								if (dataMemberAttribute.Name == null || dataMemberAttribute.Name.Length == 0)
								{
									base.ThrowInvalidDataContractException(SR.GetString("Member '{0}' in type '{1}' cannot have DataMemberAttribute attribute Name set to null or empty string.", new object[]
									{
										memberInfo.Name,
										DataContract.GetClrTypeFullName(underlyingType)
									}));
								}
								dataMember.Name = dataMemberAttribute.Name;
							}
							else
							{
								dataMember.Name = memberInfo.Name;
							}
							dataMember.Name = DataContract.EncodeLocalName(dataMember.Name);
							dataMember.IsNullable = DataContract.IsTypeNullable(dataMember.MemberType);
							dataMember.IsRequired = dataMemberAttribute.IsRequired;
							if (dataMemberAttribute.IsRequired && base.IsReference)
							{
								DataContract.DataContractCriticalHelper.ThrowInvalidDataContractException(SR.GetString("'{0}.{1}' has the IsRequired setting of '{2}. However, '{0}' has the IsReference setting of '{2}', because either it is set explicitly, or it is derived from a base class. Set IsRequired on '{0}.{1}' to false, or disable IsReference on '{0}'.", new object[]
								{
									DataContract.GetClrTypeFullName(memberInfo.DeclaringType),
									memberInfo.Name,
									true
								}), underlyingType);
							}
							dataMember.EmitDefaultValue = dataMemberAttribute.EmitDefaultValue;
							dataMember.Order = dataMemberAttribute.Order;
							ClassDataContract.CheckAndAddMember(list, dataMember, memberNamesTable);
						}
					}
					else if (this.isNonAttributedType)
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						PropertyInfo propertyInfo2 = memberInfo as PropertyInfo;
						if ((!(fieldInfo == null) || !(propertyInfo2 == null)) && (!(fieldInfo != null) || !fieldInfo.IsInitOnly))
						{
							object[] customAttributes2 = memberInfo.GetCustomAttributes(typeof(IgnoreDataMemberAttribute), false);
							if (customAttributes2 != null && customAttributes2.Length != 0)
							{
								if (customAttributes2.Length <= 1)
								{
									goto IL_53D;
								}
								base.ThrowInvalidDataContractException(SR.GetString("Member '{0}.{1}' has more than one IgnoreDataMemberAttribute attribute.", new object[]
								{
									DataContract.GetClrTypeFullName(memberInfo.DeclaringType),
									memberInfo.Name
								}));
							}
							DataMember dataMember2 = new DataMember(memberInfo);
							if (propertyInfo2 != null)
							{
								MethodInfo getMethod2 = propertyInfo2.GetGetMethod();
								if (getMethod2 == null || ClassDataContract.ClassDataContractCriticalHelper.IsMethodOverriding(getMethod2) || getMethod2.GetParameters().Length != 0)
								{
									goto IL_53D;
								}
								MethodInfo setMethod2 = propertyInfo2.GetSetMethod(true);
								if (setMethod2 == null)
								{
									if (!this.SetIfGetOnlyCollection(dataMember2, true))
									{
										goto IL_53D;
									}
								}
								else if (!setMethod2.IsPublic || ClassDataContract.ClassDataContractCriticalHelper.IsMethodOverriding(setMethod2))
								{
									goto IL_53D;
								}
								if (this.hasExtensionData && dataMember2.MemberType == Globals.TypeOfExtensionDataObject && memberInfo.Name == "ExtensionData")
								{
									goto IL_53D;
								}
							}
							dataMember2.Name = DataContract.EncodeLocalName(memberInfo.Name);
							dataMember2.IsNullable = DataContract.IsTypeNullable(dataMember2.MemberType);
							ClassDataContract.CheckAndAddMember(list, dataMember2, memberNamesTable);
						}
					}
					else
					{
						FieldInfo fieldInfo2 = memberInfo as FieldInfo;
						if (fieldInfo2 != null && !fieldInfo2.IsNotSerialized)
						{
							DataMember dataMember3 = new DataMember(memberInfo);
							dataMember3.Name = DataContract.EncodeLocalName(memberInfo.Name);
							object[] customAttributes3 = fieldInfo2.GetCustomAttributes(Globals.TypeOfOptionalFieldAttribute, false);
							if (customAttributes3 == null || customAttributes3.Length == 0)
							{
								if (base.IsReference)
								{
									DataContract.DataContractCriticalHelper.ThrowInvalidDataContractException(SR.GetString("For type '{0}', non-optional field member '{1}' is on the Serializable type that has IsReference as {2}.", new object[]
									{
										DataContract.GetClrTypeFullName(memberInfo.DeclaringType),
										memberInfo.Name,
										true
									}), underlyingType);
								}
								dataMember3.IsRequired = true;
							}
							dataMember3.IsNullable = DataContract.IsTypeNullable(dataMember3.MemberType);
							ClassDataContract.CheckAndAddMember(list, dataMember3, memberNamesTable);
						}
					}
					IL_53D:;
				}
				if (list.Count > 1)
				{
					list.Sort(ClassDataContract.DataMemberComparer.Singleton);
				}
				this.SetIfMembersHaveConflict(list);
				Thread.MemoryBarrier();
				this.members = list;
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x00026E59 File Offset: 0x00025059
			private bool SetIfGetOnlyCollection(DataMember memberContract, bool skipIfReadOnlyContract)
			{
				if (CollectionDataContract.IsCollection(memberContract.MemberType, false, skipIfReadOnlyContract) && !memberContract.MemberType.IsValueType)
				{
					memberContract.IsGetOnlyCollection = true;
					return true;
				}
				return false;
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x00026E84 File Offset: 0x00025084
			private void SetIfMembersHaveConflict(List<DataMember> members)
			{
				if (this.BaseContract == null)
				{
					return;
				}
				int num = 0;
				List<ClassDataContract.ClassDataContractCriticalHelper.Member> list = new List<ClassDataContract.ClassDataContractCriticalHelper.Member>();
				foreach (DataMember member in members)
				{
					list.Add(new ClassDataContract.ClassDataContractCriticalHelper.Member(member, base.StableName.Namespace, num));
				}
				for (ClassDataContract classDataContract = this.BaseContract; classDataContract != null; classDataContract = classDataContract.BaseContract)
				{
					num++;
					foreach (DataMember member2 in classDataContract.Members)
					{
						list.Add(new ClassDataContract.ClassDataContractCriticalHelper.Member(member2, classDataContract.StableName.Namespace, num));
					}
				}
				IComparer<ClassDataContract.ClassDataContractCriticalHelper.Member> singleton = ClassDataContract.ClassDataContractCriticalHelper.DataMemberConflictComparer.Singleton;
				list.Sort(singleton);
				for (int i = 0; i < list.Count - 1; i++)
				{
					int num2 = i;
					int num3 = i;
					bool flag = false;
					while (num3 < list.Count - 1 && string.CompareOrdinal(list[num3].member.Name, list[num3 + 1].member.Name) == 0 && string.CompareOrdinal(list[num3].ns, list[num3 + 1].ns) == 0)
					{
						list[num3].member.ConflictingMember = list[num3 + 1].member;
						if (!flag)
						{
							flag = (list[num3 + 1].member.HasConflictingNameAndType || list[num3].member.MemberType != list[num3 + 1].member.MemberType);
						}
						num3++;
					}
					if (flag)
					{
						for (int j = num2; j <= num3; j++)
						{
							list[j].member.HasConflictingNameAndType = true;
						}
					}
					i = num3 + 1;
				}
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x0002709C File Offset: 0x0002529C
			[SecuritySafeCritical]
			private XmlQualifiedName GetStableNameAndSetHasDataContract(Type type)
			{
				return DataContract.GetStableName(type, out this.hasDataContract);
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x000270AA File Offset: 0x000252AA
			private void SetIsNonAttributedType(Type type)
			{
				this.isNonAttributedType = (!type.IsSerializable && !this.hasDataContract && ClassDataContract.IsNonAttributedTypeValidForSerialization(type));
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x000270CB File Offset: 0x000252CB
			private static bool IsMethodOverriding(MethodInfo method)
			{
				return method.IsVirtual && (method.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.PrivateScope;
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x000270E8 File Offset: 0x000252E8
			internal void EnsureMethodsImported()
			{
				if (!this.isMethodChecked && base.UnderlyingType != null)
				{
					lock (this)
					{
						if (!this.isMethodChecked)
						{
							foreach (MethodInfo methodInfo in base.UnderlyingType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
							{
								Type type = null;
								ParameterInfo[] parameters = methodInfo.GetParameters();
								if (this.HasExtensionData && this.IsValidExtensionDataSetMethod(methodInfo, parameters))
								{
									if (methodInfo.Name == "System.Runtime.Serialization.IExtensibleDataObject.set_ExtensionData" || !methodInfo.IsPublic)
									{
										this.extensionDataSetMethod = XmlFormatGeneratorStatics.ExtensionDataSetExplicitMethodInfo;
									}
									else
									{
										this.extensionDataSetMethod = methodInfo;
									}
								}
								if (ClassDataContract.ClassDataContractCriticalHelper.IsValidCallback(methodInfo, parameters, Globals.TypeOfOnSerializingAttribute, this.onSerializing, ref type))
								{
									this.onSerializing = methodInfo;
								}
								if (ClassDataContract.ClassDataContractCriticalHelper.IsValidCallback(methodInfo, parameters, Globals.TypeOfOnSerializedAttribute, this.onSerialized, ref type))
								{
									this.onSerialized = methodInfo;
								}
								if (ClassDataContract.ClassDataContractCriticalHelper.IsValidCallback(methodInfo, parameters, Globals.TypeOfOnDeserializingAttribute, this.onDeserializing, ref type))
								{
									this.onDeserializing = methodInfo;
								}
								if (ClassDataContract.ClassDataContractCriticalHelper.IsValidCallback(methodInfo, parameters, Globals.TypeOfOnDeserializedAttribute, this.onDeserialized, ref type))
								{
									this.onDeserialized = methodInfo;
								}
							}
							Thread.MemoryBarrier();
							this.isMethodChecked = true;
						}
					}
				}
			}

			// Token: 0x06000948 RID: 2376 RVA: 0x00027254 File Offset: 0x00025454
			private bool IsValidExtensionDataSetMethod(MethodInfo method, ParameterInfo[] parameters)
			{
				if (method.Name == "System.Runtime.Serialization.IExtensibleDataObject.set_ExtensionData" || method.Name == "set_ExtensionData")
				{
					if (this.extensionDataSetMethod != null)
					{
						base.ThrowInvalidDataContractException(SR.GetString("Duplicate extension data set method was found, for method '{0}', existing method is '{1}', on data contract type '{2}'.", new object[]
						{
							method,
							this.extensionDataSetMethod,
							DataContract.GetClrTypeFullName(method.DeclaringType)
						}));
					}
					if (method.ReturnType != Globals.TypeOfVoid)
					{
						DataContract.ThrowInvalidDataContractException(SR.GetString("For type '{0}' method '{1}', extension data set method must return void.", new object[]
						{
							DataContract.GetClrTypeFullName(method.DeclaringType),
							method
						}), method.DeclaringType);
					}
					if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != Globals.TypeOfExtensionDataObject)
					{
						DataContract.ThrowInvalidDataContractException(SR.GetString("For type '{0}' method '{1}', extension data set method has invalid type of parameter '{2}'.", new object[]
						{
							DataContract.GetClrTypeFullName(method.DeclaringType),
							method,
							Globals.TypeOfExtensionDataObject
						}), method.DeclaringType);
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000949 RID: 2377 RVA: 0x0002735C File Offset: 0x0002555C
			private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
			{
				if (method.IsDefined(attributeType, false))
				{
					if (currentCallback != null)
					{
						DataContract.ThrowInvalidDataContractException(SR.GetString("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.", new object[]
						{
							method,
							currentCallback,
							DataContract.GetClrTypeFullName(method.DeclaringType),
							attributeType
						}), method.DeclaringType);
					}
					else if (prevAttributeType != null)
					{
						DataContract.ThrowInvalidDataContractException(SR.GetString("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.", new object[]
						{
							prevAttributeType,
							attributeType,
							DataContract.GetClrTypeFullName(method.DeclaringType),
							method
						}), method.DeclaringType);
					}
					else if (method.IsVirtual)
					{
						DataContract.ThrowInvalidDataContractException(SR.GetString("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.", new object[]
						{
							method,
							DataContract.GetClrTypeFullName(method.DeclaringType),
							attributeType
						}), method.DeclaringType);
					}
					else
					{
						if (method.ReturnType != Globals.TypeOfVoid)
						{
							DataContract.ThrowInvalidDataContractException(SR.GetString("Serialization Callback '{1}' in type '{0}' must return void.", new object[]
							{
								DataContract.GetClrTypeFullName(method.DeclaringType),
								method
							}), method.DeclaringType);
						}
						if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != Globals.TypeOfStreamingContext)
						{
							DataContract.ThrowInvalidDataContractException(SR.GetString("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.", new object[]
							{
								DataContract.GetClrTypeFullName(method.DeclaringType),
								method,
								Globals.TypeOfStreamingContext
							}), method.DeclaringType);
						}
						prevAttributeType = attributeType;
					}
					return true;
				}
				return false;
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600094A RID: 2378 RVA: 0x000274D6 File Offset: 0x000256D6
			// (set) Token: 0x0600094B RID: 2379 RVA: 0x000274E0 File Offset: 0x000256E0
			internal ClassDataContract BaseContract
			{
				get
				{
					return this.baseContract;
				}
				set
				{
					this.baseContract = value;
					if (this.baseContract != null && base.IsValueType)
					{
						base.ThrowInvalidDataContractException(SR.GetString("Data contract '{0}' from namespace '{1}' is a value type and cannot have base contract '{2}' from namespace '{3}'.", new object[]
						{
							base.StableName.Name,
							base.StableName.Namespace,
							this.baseContract.StableName.Name,
							this.baseContract.StableName.Namespace
						}));
					}
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002755C File Offset: 0x0002575C
			// (set) Token: 0x0600094D RID: 2381 RVA: 0x00027564 File Offset: 0x00025764
			internal List<DataMember> Members
			{
				get
				{
					return this.members;
				}
				set
				{
					this.members = value;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002756D File Offset: 0x0002576D
			internal MethodInfo OnSerializing
			{
				get
				{
					this.EnsureMethodsImported();
					return this.onSerializing;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002757B File Offset: 0x0002577B
			internal MethodInfo OnSerialized
			{
				get
				{
					this.EnsureMethodsImported();
					return this.onSerialized;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000950 RID: 2384 RVA: 0x00027589 File Offset: 0x00025789
			internal MethodInfo OnDeserializing
			{
				get
				{
					this.EnsureMethodsImported();
					return this.onDeserializing;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000951 RID: 2385 RVA: 0x00027597 File Offset: 0x00025797
			internal MethodInfo OnDeserialized
			{
				get
				{
					this.EnsureMethodsImported();
					return this.onDeserialized;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000952 RID: 2386 RVA: 0x000275A5 File Offset: 0x000257A5
			internal MethodInfo ExtensionDataSetMethod
			{
				get
				{
					this.EnsureMethodsImported();
					return this.extensionDataSetMethod;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000953 RID: 2387 RVA: 0x000275B4 File Offset: 0x000257B4
			// (set) Token: 0x06000954 RID: 2388 RVA: 0x0002762C File Offset: 0x0002582C
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

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000955 RID: 2389 RVA: 0x00027635 File Offset: 0x00025835
			internal string SerializationExceptionMessage
			{
				get
				{
					return this.serializationExceptionMessage;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002763D File Offset: 0x0002583D
			internal string DeserializationExceptionMessage
			{
				get
				{
					if (this.serializationExceptionMessage == null)
					{
						return null;
					}
					return SR.GetString("Error on deserializing read-only members in the class: {0}", new object[]
					{
						this.serializationExceptionMessage
					});
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000957 RID: 2391 RVA: 0x00027662 File Offset: 0x00025862
			// (set) Token: 0x06000958 RID: 2392 RVA: 0x0002766A File Offset: 0x0002586A
			internal override bool IsISerializable
			{
				get
				{
					return this.isISerializable;
				}
				set
				{
					this.isISerializable = value;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x00027673 File Offset: 0x00025873
			internal bool HasDataContract
			{
				get
				{
					return this.hasDataContract;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002767B File Offset: 0x0002587B
			internal bool HasExtensionData
			{
				get
				{
					return this.hasExtensionData;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x0600095B RID: 2395 RVA: 0x00027683 File Offset: 0x00025883
			internal bool IsNonAttributedType
			{
				get
				{
					return this.isNonAttributedType;
				}
			}

			// Token: 0x0600095C RID: 2396 RVA: 0x0002768C File Offset: 0x0002588C
			internal ConstructorInfo GetISerializableConstructor()
			{
				if (!this.IsISerializable)
				{
					return null;
				}
				ConstructorInfo constructor = base.UnderlyingType.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, ClassDataContract.ClassDataContractCriticalHelper.SerInfoCtorArgs, null);
				if (constructor == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Constructor that takes SerializationInfo and StreamingContext is not found for '{0}'.", new object[]
					{
						DataContract.GetClrTypeFullName(base.UnderlyingType)
					})));
				}
				return constructor;
			}

			// Token: 0x0600095D RID: 2397 RVA: 0x000276EC File Offset: 0x000258EC
			internal ConstructorInfo GetNonAttributedTypeConstructor()
			{
				if (!this.IsNonAttributedType)
				{
					return null;
				}
				Type underlyingType = base.UnderlyingType;
				if (underlyingType.IsValueType)
				{
					return null;
				}
				ConstructorInfo constructor = underlyingType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
				if (constructor == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("The Type '{0}' must have a parameterless constructor.", new object[]
					{
						DataContract.GetClrTypeFullName(underlyingType)
					})));
				}
				return constructor;
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x0600095E RID: 2398 RVA: 0x00027752 File Offset: 0x00025952
			// (set) Token: 0x0600095F RID: 2399 RVA: 0x0002775A File Offset: 0x0002595A
			internal XmlFormatClassWriterDelegate XmlFormatWriterDelegate
			{
				get
				{
					return this.xmlFormatWriterDelegate;
				}
				set
				{
					this.xmlFormatWriterDelegate = value;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000960 RID: 2400 RVA: 0x00027763 File Offset: 0x00025963
			// (set) Token: 0x06000961 RID: 2401 RVA: 0x0002776B File Offset: 0x0002596B
			internal XmlFormatClassReaderDelegate XmlFormatReaderDelegate
			{
				get
				{
					return this.xmlFormatReaderDelegate;
				}
				set
				{
					this.xmlFormatReaderDelegate = value;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000962 RID: 2402 RVA: 0x00027774 File Offset: 0x00025974
			// (set) Token: 0x06000963 RID: 2403 RVA: 0x0002777C File Offset: 0x0002597C
			public XmlDictionaryString[] ChildElementNamespaces
			{
				get
				{
					return this.childElementNamespaces;
				}
				set
				{
					this.childElementNamespaces = value;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000964 RID: 2404 RVA: 0x00027785 File Offset: 0x00025985
			private static Type[] SerInfoCtorArgs
			{
				get
				{
					if (ClassDataContract.ClassDataContractCriticalHelper.serInfoCtorArgs == null)
					{
						ClassDataContract.ClassDataContractCriticalHelper.serInfoCtorArgs = new Type[]
						{
							typeof(SerializationInfo),
							typeof(StreamingContext)
						};
					}
					return ClassDataContract.ClassDataContractCriticalHelper.serInfoCtorArgs;
				}
			}

			// Token: 0x04000400 RID: 1024
			private ClassDataContract baseContract;

			// Token: 0x04000401 RID: 1025
			private List<DataMember> members;

			// Token: 0x04000402 RID: 1026
			private MethodInfo onSerializing;

			// Token: 0x04000403 RID: 1027
			private MethodInfo onSerialized;

			// Token: 0x04000404 RID: 1028
			private MethodInfo onDeserializing;

			// Token: 0x04000405 RID: 1029
			private MethodInfo onDeserialized;

			// Token: 0x04000406 RID: 1030
			private MethodInfo extensionDataSetMethod;

			// Token: 0x04000407 RID: 1031
			private Dictionary<XmlQualifiedName, DataContract> knownDataContracts;

			// Token: 0x04000408 RID: 1032
			private string serializationExceptionMessage;

			// Token: 0x04000409 RID: 1033
			private bool isISerializable;

			// Token: 0x0400040A RID: 1034
			private bool isKnownTypeAttributeChecked;

			// Token: 0x0400040B RID: 1035
			private bool isMethodChecked;

			// Token: 0x0400040C RID: 1036
			private bool hasExtensionData;

			// Token: 0x0400040D RID: 1037
			private bool isNonAttributedType;

			// Token: 0x0400040E RID: 1038
			private bool hasDataContract;

			// Token: 0x0400040F RID: 1039
			private XmlDictionaryString[] childElementNamespaces;

			// Token: 0x04000410 RID: 1040
			private XmlFormatClassReaderDelegate xmlFormatReaderDelegate;

			// Token: 0x04000411 RID: 1041
			private XmlFormatClassWriterDelegate xmlFormatWriterDelegate;

			// Token: 0x04000412 RID: 1042
			public XmlDictionaryString[] ContractNamespaces;

			// Token: 0x04000413 RID: 1043
			public XmlDictionaryString[] MemberNames;

			// Token: 0x04000414 RID: 1044
			public XmlDictionaryString[] MemberNamespaces;

			// Token: 0x04000415 RID: 1045
			private static Type[] serInfoCtorArgs;

			// Token: 0x020000AE RID: 174
			internal struct Member
			{
				// Token: 0x06000965 RID: 2405 RVA: 0x000277B8 File Offset: 0x000259B8
				internal Member(DataMember member, string ns, int baseTypeIndex)
				{
					this.member = member;
					this.ns = ns;
					this.baseTypeIndex = baseTypeIndex;
				}

				// Token: 0x04000416 RID: 1046
				internal DataMember member;

				// Token: 0x04000417 RID: 1047
				internal string ns;

				// Token: 0x04000418 RID: 1048
				internal int baseTypeIndex;
			}

			// Token: 0x020000AF RID: 175
			internal class DataMemberConflictComparer : IComparer<ClassDataContract.ClassDataContractCriticalHelper.Member>
			{
				// Token: 0x06000966 RID: 2406 RVA: 0x000277D0 File Offset: 0x000259D0
				public int Compare(ClassDataContract.ClassDataContractCriticalHelper.Member x, ClassDataContract.ClassDataContractCriticalHelper.Member y)
				{
					int num = string.CompareOrdinal(x.ns, y.ns);
					if (num != 0)
					{
						return num;
					}
					int num2 = string.CompareOrdinal(x.member.Name, y.member.Name);
					if (num2 != 0)
					{
						return num2;
					}
					return x.baseTypeIndex - y.baseTypeIndex;
				}

				// Token: 0x06000967 RID: 2407 RVA: 0x0000222F File Offset: 0x0000042F
				public DataMemberConflictComparer()
				{
				}

				// Token: 0x06000968 RID: 2408 RVA: 0x00027822 File Offset: 0x00025A22
				// Note: this type is marked as 'beforefieldinit'.
				static DataMemberConflictComparer()
				{
				}

				// Token: 0x04000419 RID: 1049
				internal static ClassDataContract.ClassDataContractCriticalHelper.DataMemberConflictComparer Singleton = new ClassDataContract.ClassDataContractCriticalHelper.DataMemberConflictComparer();
			}
		}

		// Token: 0x020000B0 RID: 176
		internal class DataMemberComparer : IComparer<DataMember>
		{
			// Token: 0x06000969 RID: 2409 RVA: 0x00027830 File Offset: 0x00025A30
			public int Compare(DataMember x, DataMember y)
			{
				int num = x.Order - y.Order;
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.Name, y.Name);
			}

			// Token: 0x0600096A RID: 2410 RVA: 0x0000222F File Offset: 0x0000042F
			public DataMemberComparer()
			{
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x00027861 File Offset: 0x00025A61
			// Note: this type is marked as 'beforefieldinit'.
			static DataMemberComparer()
			{
			}

			// Token: 0x0400041A RID: 1050
			internal static ClassDataContract.DataMemberComparer Singleton = new ClassDataContract.DataMemberComparer();
		}
	}
}
