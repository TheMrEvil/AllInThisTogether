using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000B4 RID: 180
	internal sealed class CollectionDataContract : DataContract
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0002B26E File Offset: 0x0002946E
		[SecuritySafeCritical]
		internal CollectionDataContract(CollectionKind kind) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(kind))
		{
			this.InitCollectionDataContract(this);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002B283 File Offset: 0x00029483
		[SecuritySafeCritical]
		internal CollectionDataContract(Type type) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(type))
		{
			this.InitCollectionDataContract(this);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002B298 File Offset: 0x00029498
		[SecuritySafeCritical]
		internal CollectionDataContract(Type type, DataContract itemContract) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(type, itemContract))
		{
			this.InitCollectionDataContract(this);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002B2AE File Offset: 0x000294AE
		[SecuritySafeCritical]
		private CollectionDataContract(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, string serializationExceptionMessage, string deserializationExceptionMessage) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(type, kind, itemType, getEnumeratorMethod, serializationExceptionMessage, deserializationExceptionMessage))
		{
			this.InitCollectionDataContract(this.GetSharedTypeContract(type));
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002B2D1 File Offset: 0x000294D1
		[SecuritySafeCritical]
		private CollectionDataContract(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(type, kind, itemType, getEnumeratorMethod, addMethod, constructor))
		{
			this.InitCollectionDataContract(this.GetSharedTypeContract(type));
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002B2F4 File Offset: 0x000294F4
		[SecuritySafeCritical]
		private CollectionDataContract(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor, bool isConstructorCheckRequired) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(type, kind, itemType, getEnumeratorMethod, addMethod, constructor, isConstructorCheckRequired))
		{
			this.InitCollectionDataContract(this.GetSharedTypeContract(type));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002B319 File Offset: 0x00029519
		[SecuritySafeCritical]
		private CollectionDataContract(Type type, string invalidCollectionInSharedContractMessage) : base(new CollectionDataContract.CollectionDataContractCriticalHelper(type, invalidCollectionInSharedContractMessage))
		{
			this.InitCollectionDataContract(this.GetSharedTypeContract(type));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002B338 File Offset: 0x00029538
		[SecurityCritical]
		private void InitCollectionDataContract(DataContract sharedTypeContract)
		{
			this.helper = (base.Helper as CollectionDataContract.CollectionDataContractCriticalHelper);
			this.collectionItemName = this.helper.CollectionItemName;
			if (this.helper.Kind == CollectionKind.Dictionary || this.helper.Kind == CollectionKind.GenericDictionary)
			{
				this.itemContract = this.helper.ItemContract;
			}
			this.helper.SharedTypeContract = sharedTypeContract;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000A8EE File Offset: 0x00008AEE
		private void InitSharedTypeContract()
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0002B3A0 File Offset: 0x000295A0
		private static Type[] KnownInterfaces
		{
			[SecuritySafeCritical]
			get
			{
				return CollectionDataContract.CollectionDataContractCriticalHelper.KnownInterfaces;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0002B3A7 File Offset: 0x000295A7
		internal CollectionKind Kind
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.Kind;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002B3B4 File Offset: 0x000295B4
		internal Type ItemType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ItemType;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002B3C1 File Offset: 0x000295C1
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0002B3D8 File Offset: 0x000295D8
		public DataContract ItemContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.itemContract ?? this.helper.ItemContract;
			}
			[SecurityCritical]
			set
			{
				this.itemContract = value;
				this.helper.ItemContract = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002B3ED File Offset: 0x000295ED
		internal DataContract SharedTypeContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.SharedTypeContract;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002B3FA File Offset: 0x000295FA
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0002B407 File Offset: 0x00029607
		internal string ItemName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ItemName;
			}
			[SecurityCritical]
			set
			{
				this.helper.ItemName = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002B415 File Offset: 0x00029615
		public XmlDictionaryString CollectionItemName
		{
			[SecuritySafeCritical]
			get
			{
				return this.collectionItemName;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002B41D File Offset: 0x0002961D
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0002B42A File Offset: 0x0002962A
		internal string KeyName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.KeyName;
			}
			[SecurityCritical]
			set
			{
				this.helper.KeyName = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002B438 File Offset: 0x00029638
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002B445 File Offset: 0x00029645
		internal string ValueName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ValueName;
			}
			[SecurityCritical]
			set
			{
				this.helper.ValueName = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002B453 File Offset: 0x00029653
		internal bool IsDictionary
		{
			get
			{
				return this.KeyName != null;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002B460 File Offset: 0x00029660
		public XmlDictionaryString ChildElementNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this.childElementNamespace == null)
				{
					lock (this)
					{
						if (this.childElementNamespace == null)
						{
							if (this.helper.ChildElementNamespace == null && !this.IsDictionary)
							{
								XmlDictionaryString childNamespaceToDeclare = ClassDataContract.GetChildNamespaceToDeclare(this, this.ItemType, new XmlDictionary());
								Thread.MemoryBarrier();
								this.helper.ChildElementNamespace = childNamespaceToDeclare;
							}
							this.childElementNamespace = this.helper.ChildElementNamespace;
						}
					}
				}
				return this.childElementNamespace;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002B4F4 File Offset: 0x000296F4
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0002B501 File Offset: 0x00029701
		internal bool IsItemTypeNullable
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsItemTypeNullable;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsItemTypeNullable = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002B50F File Offset: 0x0002970F
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0002B51C File Offset: 0x0002971C
		internal bool IsConstructorCheckRequired
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsConstructorCheckRequired;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsConstructorCheckRequired = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002B52A File Offset: 0x0002972A
		internal MethodInfo GetEnumeratorMethod
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.GetEnumeratorMethod;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002B537 File Offset: 0x00029737
		internal MethodInfo AddMethod
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.AddMethod;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002B544 File Offset: 0x00029744
		internal ConstructorInfo Constructor
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.Constructor;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002B551 File Offset: 0x00029751
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0002B55E File Offset: 0x0002975E
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002B56C File Offset: 0x0002976C
		internal string InvalidCollectionInSharedContractMessage
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.InvalidCollectionInSharedContractMessage;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0002B579 File Offset: 0x00029779
		internal string SerializationExceptionMessage
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.SerializationExceptionMessage;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002B586 File Offset: 0x00029786
		internal string DeserializationExceptionMessage
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.DeserializationExceptionMessage;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002B593 File Offset: 0x00029793
		internal bool IsReadOnlyContract
		{
			get
			{
				return this.DeserializationExceptionMessage != null;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002B59E File Offset: 0x0002979E
		private bool ItemNameSetExplicit
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ItemNameSetExplicit;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0002B5AC File Offset: 0x000297AC
		internal XmlFormatCollectionWriterDelegate XmlFormatWriterDelegate
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
							XmlFormatCollectionWriterDelegate xmlFormatWriterDelegate = new XmlFormatWriterGenerator().GenerateCollectionWriter(this);
							Thread.MemoryBarrier();
							this.helper.XmlFormatWriterDelegate = xmlFormatWriterDelegate;
						}
					}
				}
				return this.helper.XmlFormatWriterDelegate;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002B624 File Offset: 0x00029824
		internal XmlFormatCollectionReaderDelegate XmlFormatReaderDelegate
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
							XmlFormatCollectionReaderDelegate xmlFormatReaderDelegate = new XmlFormatReaderGenerator().GenerateCollectionReader(this);
							Thread.MemoryBarrier();
							this.helper.XmlFormatReaderDelegate = xmlFormatReaderDelegate;
						}
					}
				}
				return this.helper.XmlFormatReaderDelegate;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002B6B4 File Offset: 0x000298B4
		internal XmlFormatGetOnlyCollectionReaderDelegate XmlFormatGetOnlyCollectionReaderDelegate
		{
			[SecuritySafeCritical]
			get
			{
				if (this.helper.XmlFormatGetOnlyCollectionReaderDelegate == null)
				{
					lock (this)
					{
						if (this.helper.XmlFormatGetOnlyCollectionReaderDelegate == null)
						{
							if (base.UnderlyingType.IsInterface && (this.Kind == CollectionKind.Enumerable || this.Kind == CollectionKind.Collection || this.Kind == CollectionKind.GenericEnumerable))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("On type '{0}', get-only collection must have an Add method.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType)
								})));
							}
							if (this.IsReadOnlyContract)
							{
								DataContract.ThrowInvalidDataContractException(this.helper.DeserializationExceptionMessage, null);
							}
							XmlFormatGetOnlyCollectionReaderDelegate xmlFormatGetOnlyCollectionReaderDelegate = new XmlFormatReaderGenerator().GenerateGetOnlyCollectionReader(this);
							Thread.MemoryBarrier();
							this.helper.XmlFormatGetOnlyCollectionReaderDelegate = xmlFormatGetOnlyCollectionReaderDelegate;
						}
					}
				}
				return this.helper.XmlFormatGetOnlyCollectionReaderDelegate;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002B79C File Offset: 0x0002999C
		private DataContract GetSharedTypeContract(Type type)
		{
			if (type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false))
			{
				return this;
			}
			if (type.IsSerializable || type.IsDefined(Globals.TypeOfDataContractAttribute, false))
			{
				return new ClassDataContract(type);
			}
			return null;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002B7CC File Offset: 0x000299CC
		internal static bool IsCollectionInterface(Type type)
		{
			if (type.IsGenericType)
			{
				type = type.GetGenericTypeDefinition();
			}
			return ((ICollection<Type>)CollectionDataContract.KnownInterfaces).Contains(type);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002B7EC File Offset: 0x000299EC
		internal static bool IsCollection(Type type)
		{
			Type type2;
			return CollectionDataContract.IsCollection(type, out type2);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002B801 File Offset: 0x00029A01
		internal static bool IsCollection(Type type, out Type itemType)
		{
			return CollectionDataContract.IsCollectionHelper(type, out itemType, true, false);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002B80C File Offset: 0x00029A0C
		internal static bool IsCollection(Type type, bool constructorRequired, bool skipIfReadOnlyContract)
		{
			Type type2;
			return CollectionDataContract.IsCollectionHelper(type, out type2, constructorRequired, skipIfReadOnlyContract);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002B824 File Offset: 0x00029A24
		private static bool IsCollectionHelper(Type type, out Type itemType, bool constructorRequired, bool skipIfReadOnlyContract = false)
		{
			if (type.IsArray && DataContract.GetBuiltInDataContract(type) == null)
			{
				itemType = type.GetElementType();
				return true;
			}
			DataContract dataContract;
			return CollectionDataContract.IsCollectionOrTryCreate(type, false, out dataContract, out itemType, constructorRequired, skipIfReadOnlyContract);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002B858 File Offset: 0x00029A58
		internal static bool TryCreate(Type type, out DataContract dataContract)
		{
			Type type2;
			return CollectionDataContract.IsCollectionOrTryCreate(type, true, out dataContract, out type2, true, false);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002B874 File Offset: 0x00029A74
		internal static bool TryCreateGetOnlyCollectionDataContract(Type type, out DataContract dataContract)
		{
			if (type.IsArray)
			{
				dataContract = new CollectionDataContract(type);
				return true;
			}
			Type type2;
			return CollectionDataContract.IsCollectionOrTryCreate(type, true, out dataContract, out type2, false, false);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002B8A0 File Offset: 0x00029AA0
		internal static MethodInfo GetTargetMethodWithName(string name, Type type, Type interfaceType)
		{
			InterfaceMapping interfaceMap = type.GetInterfaceMap(interfaceType);
			for (int i = 0; i < interfaceMap.TargetMethods.Length; i++)
			{
				if (interfaceMap.InterfaceMethods[i].Name == name)
				{
					return interfaceMap.InterfaceMethods[i];
				}
			}
			return null;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002B8E7 File Offset: 0x00029AE7
		private static bool IsArraySegment(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ArraySegment<>);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002B908 File Offset: 0x00029B08
		private static bool IsCollectionOrTryCreate(Type type, bool tryCreate, out DataContract dataContract, out Type itemType, bool constructorRequired, bool skipIfReadOnlyContract = false)
		{
			dataContract = null;
			itemType = Globals.TypeOfObject;
			if (DataContract.GetBuiltInDataContract(type) != null)
			{
				return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, false, false, "{0} is a built-in type and cannot be a collection.", null, ref dataContract);
			}
			bool hasCollectionDataContract = CollectionDataContract.IsCollectionDataContract(type);
			bool flag = false;
			string serializationExceptionMessage = null;
			string deserializationExceptionMessage = null;
			Type baseType = type.BaseType;
			bool flag2 = baseType != null && baseType != Globals.TypeOfObject && baseType != Globals.TypeOfValueType && baseType != Globals.TypeOfUri && CollectionDataContract.IsCollection(baseType) && !type.IsSerializable;
			if (type.IsDefined(Globals.TypeOfDataContractAttribute, false))
			{
				return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, flag2, "{0} has DataContractAttribute attribute.", null, ref dataContract);
			}
			if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type) || CollectionDataContract.IsArraySegment(type))
			{
				return false;
			}
			if (!Globals.TypeOfIEnumerable.IsAssignableFrom(type))
			{
				return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, flag2, "{0} does not implement IEnumerable interface.", null, ref dataContract);
			}
			if (type.IsInterface)
			{
				Type type2 = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
				Type[] knownInterfaces = CollectionDataContract.KnownInterfaces;
				for (int i = 0; i < knownInterfaces.Length; i++)
				{
					if (knownInterfaces[i] == type2)
					{
						MethodInfo methodInfo = null;
						MethodInfo method;
						if (type.IsGenericType)
						{
							Type[] genericArguments = type.GetGenericArguments();
							if (type2 == Globals.TypeOfIDictionaryGeneric)
							{
								itemType = Globals.TypeOfKeyValue.MakeGenericType(genericArguments);
								methodInfo = type.GetMethod("Add");
								method = Globals.TypeOfIEnumerableGeneric.MakeGenericType(new Type[]
								{
									Globals.TypeOfKeyValuePair.MakeGenericType(genericArguments)
								}).GetMethod("GetEnumerator");
							}
							else
							{
								itemType = genericArguments[0];
								if (type2 == Globals.TypeOfICollectionGeneric || type2 == Globals.TypeOfIListGeneric)
								{
									methodInfo = Globals.TypeOfICollectionGeneric.MakeGenericType(new Type[]
									{
										itemType
									}).GetMethod("Add");
								}
								method = Globals.TypeOfIEnumerableGeneric.MakeGenericType(new Type[]
								{
									itemType
								}).GetMethod("GetEnumerator");
							}
						}
						else
						{
							if (type2 == Globals.TypeOfIDictionary)
							{
								itemType = typeof(KeyValue<object, object>);
								methodInfo = type.GetMethod("Add");
							}
							else
							{
								itemType = Globals.TypeOfObject;
								if (type2 == Globals.TypeOfIList)
								{
									methodInfo = Globals.TypeOfIList.GetMethod("Add");
								}
							}
							method = Globals.TypeOfIEnumerable.GetMethod("GetEnumerator");
						}
						if (tryCreate)
						{
							dataContract = new CollectionDataContract(type, (CollectionKind)(i + 1), itemType, method, methodInfo, null);
						}
						return true;
					}
				}
			}
			ConstructorInfo constructorInfo = null;
			if (!type.IsValueType)
			{
				constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
				if (constructorInfo == null && constructorRequired)
				{
					if (type.IsSerializable)
					{
						return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, flag2, "{0} does not have a default constructor.", null, ref dataContract);
					}
					flag = true;
					CollectionDataContract.GetReadOnlyCollectionExceptionMessages(type, hasCollectionDataContract, "{0} does not have a default constructor.", null, out serializationExceptionMessage, out deserializationExceptionMessage);
				}
			}
			Type type3 = null;
			CollectionKind collectionKind = CollectionKind.None;
			bool flag3 = false;
			foreach (Type type4 in type.GetInterfaces())
			{
				Type right = type4.IsGenericType ? type4.GetGenericTypeDefinition() : type4;
				Type[] knownInterfaces2 = CollectionDataContract.KnownInterfaces;
				int k = 0;
				while (k < knownInterfaces2.Length)
				{
					if (knownInterfaces2[k] == right)
					{
						CollectionKind collectionKind2 = (CollectionKind)(k + 1);
						if (collectionKind == CollectionKind.None || collectionKind2 < collectionKind)
						{
							collectionKind = collectionKind2;
							type3 = type4;
							flag3 = false;
							break;
						}
						if ((collectionKind & collectionKind2) == collectionKind2)
						{
							flag3 = true;
							break;
						}
						break;
					}
					else
					{
						k++;
					}
				}
			}
			if (collectionKind == CollectionKind.None)
			{
				return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, flag2, "{0} does not implement IEnumerable interface.", null, ref dataContract);
			}
			if (collectionKind == CollectionKind.Enumerable || collectionKind == CollectionKind.Collection || collectionKind == CollectionKind.GenericEnumerable)
			{
				if (flag3)
				{
					type3 = Globals.TypeOfIEnumerable;
				}
				itemType = (type3.IsGenericType ? type3.GetGenericArguments()[0] : Globals.TypeOfObject);
				MethodInfo methodInfo;
				MethodInfo method;
				CollectionDataContract.GetCollectionMethods(type, type3, new Type[]
				{
					itemType
				}, false, out method, out methodInfo);
				if (methodInfo == null)
				{
					if (type.IsSerializable || skipIfReadOnlyContract)
					{
						return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, flag2 && !skipIfReadOnlyContract, "{0} does not have a valid Add method with parameter of type '{1}'.", DataContract.GetClrTypeFullName(itemType), ref dataContract);
					}
					flag = true;
					CollectionDataContract.GetReadOnlyCollectionExceptionMessages(type, hasCollectionDataContract, "{0} does not have a valid Add method with parameter of type '{1}'.", DataContract.GetClrTypeFullName(itemType), out serializationExceptionMessage, out deserializationExceptionMessage);
				}
				if (tryCreate)
				{
					dataContract = (flag ? new CollectionDataContract(type, collectionKind, itemType, method, serializationExceptionMessage, deserializationExceptionMessage) : new CollectionDataContract(type, collectionKind, itemType, method, methodInfo, constructorInfo, !constructorRequired));
				}
			}
			else
			{
				if (flag3)
				{
					return CollectionDataContract.HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, flag2, "{0} has multiple definitions of interface '{1}'.", CollectionDataContract.KnownInterfaces[(int)(collectionKind - CollectionKind.GenericDictionary)].Name, ref dataContract);
				}
				Type[] array = null;
				switch (collectionKind)
				{
				case CollectionKind.GenericDictionary:
					array = type3.GetGenericArguments();
					itemType = ((type3.IsGenericTypeDefinition || (array[0].IsGenericParameter && array[1].IsGenericParameter)) ? Globals.TypeOfKeyValue : Globals.TypeOfKeyValue.MakeGenericType(array));
					break;
				case CollectionKind.Dictionary:
					array = new Type[]
					{
						Globals.TypeOfObject,
						Globals.TypeOfObject
					};
					itemType = Globals.TypeOfKeyValue.MakeGenericType(array);
					break;
				case CollectionKind.GenericList:
				case CollectionKind.GenericCollection:
					array = type3.GetGenericArguments();
					itemType = array[0];
					break;
				case CollectionKind.List:
					itemType = Globals.TypeOfObject;
					array = new Type[]
					{
						itemType
					};
					break;
				}
				if (tryCreate)
				{
					MethodInfo methodInfo;
					MethodInfo method;
					CollectionDataContract.GetCollectionMethods(type, type3, array, true, out method, out methodInfo);
					dataContract = (flag ? new CollectionDataContract(type, collectionKind, itemType, method, serializationExceptionMessage, deserializationExceptionMessage) : new CollectionDataContract(type, collectionKind, itemType, method, methodInfo, constructorInfo, !constructorRequired));
				}
			}
			return !flag || !skipIfReadOnlyContract;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002BE7B File Offset: 0x0002A07B
		internal static bool IsCollectionDataContract(Type type)
		{
			return type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002BE8C File Offset: 0x0002A08C
		private static bool HandleIfInvalidCollection(Type type, bool tryCreate, bool hasCollectionDataContract, bool createContractWithException, string message, string param, ref DataContract dataContract)
		{
			if (hasCollectionDataContract)
			{
				if (tryCreate)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(CollectionDataContract.GetInvalidCollectionMessage(message, SR.GetString("Type '{0}' with CollectionDataContractAttribute attribute is an invalid collection type since it", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					}), param)));
				}
				return true;
			}
			else
			{
				if (createContractWithException)
				{
					if (tryCreate)
					{
						dataContract = new CollectionDataContract(type, CollectionDataContract.GetInvalidCollectionMessage(message, SR.GetString("Type '{0}' is an invalid collection type since it", new object[]
						{
							DataContract.GetClrTypeFullName(type)
						}), param));
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002BF04 File Offset: 0x0002A104
		private static void GetReadOnlyCollectionExceptionMessages(Type type, bool hasCollectionDataContract, string message, string param, out string serializationExceptionMessage, out string deserializationExceptionMessage)
		{
			serializationExceptionMessage = CollectionDataContract.GetInvalidCollectionMessage(message, SR.GetString(hasCollectionDataContract ? "Type '{0}' with CollectionDataContractAttribute attribute is an invalid collection type since it" : "Type '{0}' is an invalid collection type since it", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			}), param);
			deserializationExceptionMessage = CollectionDataContract.GetInvalidCollectionMessage(message, SR.GetString("Error on deserializing read-only collection: {0}", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			}), param);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002BF61 File Offset: 0x0002A161
		private static string GetInvalidCollectionMessage(string message, string nestedMessage, string param)
		{
			if (param != null)
			{
				return SR.GetString(message, new object[]
				{
					nestedMessage,
					param
				});
			}
			return SR.GetString(message, new object[]
			{
				nestedMessage
			});
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002BF8C File Offset: 0x0002A18C
		private static void FindCollectionMethodsOnInterface(Type type, Type interfaceType, ref MethodInfo addMethod, ref MethodInfo getEnumeratorMethod)
		{
			InterfaceMapping interfaceMap = type.GetInterfaceMap(interfaceType);
			for (int i = 0; i < interfaceMap.TargetMethods.Length; i++)
			{
				if (interfaceMap.InterfaceMethods[i].Name == "Add")
				{
					addMethod = interfaceMap.InterfaceMethods[i];
				}
				else if (interfaceMap.InterfaceMethods[i].Name == "GetEnumerator")
				{
					getEnumeratorMethod = interfaceMap.InterfaceMethods[i];
				}
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		private static void GetCollectionMethods(Type type, Type interfaceType, Type[] addMethodTypeArray, bool addMethodOnInterface, out MethodInfo getEnumeratorMethod, out MethodInfo addMethod)
		{
			MethodInfo methodInfo;
			getEnumeratorMethod = (methodInfo = null);
			addMethod = methodInfo;
			if (addMethodOnInterface)
			{
				addMethod = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, addMethodTypeArray, null);
				if (addMethod == null || addMethod.GetParameters()[0].ParameterType != addMethodTypeArray[0])
				{
					CollectionDataContract.FindCollectionMethodsOnInterface(type, interfaceType, ref addMethod, ref getEnumeratorMethod);
					if (addMethod == null)
					{
						foreach (Type type2 in interfaceType.GetInterfaces())
						{
							if (CollectionDataContract.IsKnownInterface(type2))
							{
								CollectionDataContract.FindCollectionMethodsOnInterface(type, type2, ref addMethod, ref getEnumeratorMethod);
								if (addMethod == null)
								{
									break;
								}
							}
						}
					}
				}
			}
			else
			{
				addMethod = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, addMethodTypeArray, null);
			}
			if (getEnumeratorMethod == null)
			{
				getEnumeratorMethod = type.GetMethod("GetEnumerator", BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
				if (getEnumeratorMethod == null || !Globals.TypeOfIEnumerator.IsAssignableFrom(getEnumeratorMethod.ReturnType))
				{
					Type type3 = interfaceType.GetInterface("System.Collections.Generic.IEnumerable*");
					if (type3 == null)
					{
						type3 = Globals.TypeOfIEnumerable;
					}
					getEnumeratorMethod = CollectionDataContract.GetTargetMethodWithName("GetEnumerator", type, type3);
				}
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002C124 File Offset: 0x0002A324
		private static bool IsKnownInterface(Type type)
		{
			Type left = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
			foreach (Type right in CollectionDataContract.KnownInterfaces)
			{
				if (left == right)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002C168 File Offset: 0x0002A368
		[SecuritySafeCritical]
		internal override DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
		{
			DataContract result;
			if (boundContracts.TryGetValue(this, out result))
			{
				return result;
			}
			CollectionDataContract collectionDataContract = new CollectionDataContract(this.Kind);
			boundContracts.Add(this, collectionDataContract);
			collectionDataContract.ItemContract = this.ItemContract.BindGenericParameters(paramContracts, boundContracts);
			collectionDataContract.IsItemTypeNullable = !collectionDataContract.ItemContract.IsValueType;
			collectionDataContract.ItemName = (this.ItemNameSetExplicit ? this.ItemName : collectionDataContract.ItemContract.StableName.Name);
			collectionDataContract.KeyName = this.KeyName;
			collectionDataContract.ValueName = this.ValueName;
			collectionDataContract.StableName = DataContract.CreateQualifiedName(DataContract.ExpandGenericParameters(XmlConvert.DecodeName(base.StableName.Name), new GenericNameProvider(DataContract.GetClrTypeFullName(base.UnderlyingType), paramContracts)), CollectionDataContract.IsCollectionDataContract(base.UnderlyingType) ? base.StableName.Namespace : DataContract.GetCollectionNamespace(collectionDataContract.ItemContract.StableName.Namespace));
			return collectionDataContract;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002C25E File Offset: 0x0002A45E
		internal override DataContract GetValidContract(SerializationMode mode)
		{
			if (mode == SerializationMode.SharedType)
			{
				if (this.SharedTypeContract == null)
				{
					DataContract.ThrowTypeNotSerializable(base.UnderlyingType);
				}
				return this.SharedTypeContract;
			}
			this.ThrowIfInvalid();
			return this;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002C285 File Offset: 0x0002A485
		private void ThrowIfInvalid()
		{
			if (this.InvalidCollectionInSharedContractMessage != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(this.InvalidCollectionInSharedContractMessage));
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002C2A0 File Offset: 0x0002A4A0
		internal override DataContract GetValidContract()
		{
			if (this.IsConstructorCheckRequired)
			{
				this.CheckConstructor();
			}
			return this;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002C2B1 File Offset: 0x0002A4B1
		[SecuritySafeCritical]
		private void CheckConstructor()
		{
			if (this.Constructor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("{0} does not have a default constructor.", new object[]
				{
					DataContract.GetClrTypeFullName(base.UnderlyingType)
				})));
			}
			this.IsConstructorCheckRequired = false;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002C2F1 File Offset: 0x0002A4F1
		internal override bool IsValidContract(SerializationMode mode)
		{
			if (mode == SerializationMode.SharedType)
			{
				return this.SharedTypeContract != null;
			}
			return this.InvalidCollectionInSharedContractMessage == null;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002C30C File Offset: 0x0002A50C
		internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (base.IsEqualOrChecked(other, checkedContracts))
			{
				return true;
			}
			if (base.Equals(other, checkedContracts))
			{
				CollectionDataContract collectionDataContract = other as CollectionDataContract;
				if (collectionDataContract != null)
				{
					bool flag = this.ItemContract != null && !this.ItemContract.IsValueType;
					bool flag2 = collectionDataContract.ItemContract != null && !collectionDataContract.ItemContract.IsValueType;
					return this.ItemName == collectionDataContract.ItemName && (this.IsItemTypeNullable || flag) == (collectionDataContract.IsItemTypeNullable || flag2) && this.ItemContract.Equals(collectionDataContract.ItemContract, checkedContracts);
				}
			}
			return false;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000262A8 File Offset: 0x000244A8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002C3A8 File Offset: 0x0002A5A8
		public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			context.IsGetOnlyCollection = false;
			this.XmlFormatWriterDelegate(xmlWriter, obj, context, this);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
		{
			xmlReader.Read();
			object result = null;
			if (context.IsGetOnlyCollection)
			{
				context.IsGetOnlyCollection = false;
				this.XmlFormatGetOnlyCollectionReaderDelegate(xmlReader, context, this.CollectionItemName, this.Namespace, this);
			}
			else
			{
				result = this.XmlFormatReaderDelegate(xmlReader, context, this.CollectionItemName, this.Namespace, this);
			}
			xmlReader.ReadEndElement();
			return result;
		}

		// Token: 0x04000432 RID: 1074
		[SecurityCritical]
		private XmlDictionaryString collectionItemName;

		// Token: 0x04000433 RID: 1075
		[SecurityCritical]
		private XmlDictionaryString childElementNamespace;

		// Token: 0x04000434 RID: 1076
		[SecurityCritical]
		private DataContract itemContract;

		// Token: 0x04000435 RID: 1077
		[SecurityCritical]
		private CollectionDataContract.CollectionDataContractCriticalHelper helper;

		// Token: 0x020000B5 RID: 181
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class CollectionDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002C424 File Offset: 0x0002A624
			internal static Type[] KnownInterfaces
			{
				get
				{
					if (CollectionDataContract.CollectionDataContractCriticalHelper._knownInterfaces == null)
					{
						CollectionDataContract.CollectionDataContractCriticalHelper._knownInterfaces = new Type[]
						{
							Globals.TypeOfIDictionaryGeneric,
							Globals.TypeOfIDictionary,
							Globals.TypeOfIListGeneric,
							Globals.TypeOfICollectionGeneric,
							Globals.TypeOfIList,
							Globals.TypeOfIEnumerableGeneric,
							Globals.TypeOfICollection,
							Globals.TypeOfIEnumerable
						};
					}
					return CollectionDataContract.CollectionDataContractCriticalHelper._knownInterfaces;
				}
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x0002C488 File Offset: 0x0002A688
			private void Init(CollectionKind kind, Type itemType, CollectionDataContractAttribute collectionContractAttribute)
			{
				this.kind = kind;
				if (itemType != null)
				{
					this.itemType = itemType;
					this.isItemTypeNullable = DataContract.IsTypeNullable(itemType);
					bool flag = kind == CollectionKind.Dictionary || kind == CollectionKind.GenericDictionary;
					string text = null;
					string text2 = null;
					string text3 = null;
					if (collectionContractAttribute != null)
					{
						if (collectionContractAttribute.IsItemNameSetExplicitly)
						{
							if (collectionContractAttribute.ItemName == null || collectionContractAttribute.ItemName.Length == 0)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have CollectionDataContractAttribute attribute ItemName set to null or empty string.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType)
								})));
							}
							text = DataContract.EncodeLocalName(collectionContractAttribute.ItemName);
							this.itemNameSetExplicit = true;
						}
						if (collectionContractAttribute.IsKeyNameSetExplicitly)
						{
							if (collectionContractAttribute.KeyName == null || collectionContractAttribute.KeyName.Length == 0)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have CollectionDataContractAttribute attribute KeyName set to null or empty string.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType)
								})));
							}
							if (!flag)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("The collection data contract type '{0}' specifies '{1}' for the KeyName property. This is not allowed since the type is not IDictionary. Remove the setting for the KeyName property.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType),
									collectionContractAttribute.KeyName
								})));
							}
							text2 = DataContract.EncodeLocalName(collectionContractAttribute.KeyName);
						}
						if (collectionContractAttribute.IsValueNameSetExplicitly)
						{
							if (collectionContractAttribute.ValueName == null || collectionContractAttribute.ValueName.Length == 0)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have CollectionDataContractAttribute attribute ValueName set to null or empty string.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType)
								})));
							}
							if (!flag)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("The collection data contract type '{0}' specifies '{1}' for the ValueName property. This is not allowed since the type is not IDictionary. Remove the setting for the ValueName property.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType),
									collectionContractAttribute.ValueName
								})));
							}
							text3 = DataContract.EncodeLocalName(collectionContractAttribute.ValueName);
						}
					}
					XmlDictionary xmlDictionary = flag ? new XmlDictionary(5) : new XmlDictionary(3);
					base.Name = xmlDictionary.Add(base.StableName.Name);
					base.Namespace = xmlDictionary.Add(base.StableName.Namespace);
					this.itemName = (text ?? DataContract.GetStableName(DataContract.UnwrapNullableType(itemType)).Name);
					this.collectionItemName = xmlDictionary.Add(this.itemName);
					if (flag)
					{
						this.keyName = (text2 ?? "Key");
						this.valueName = (text3 ?? "Value");
					}
				}
				if (collectionContractAttribute != null)
				{
					base.IsReference = collectionContractAttribute.IsReference;
				}
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x0002C6DD File Offset: 0x0002A8DD
			internal CollectionDataContractCriticalHelper(CollectionKind kind)
			{
				this.Init(kind, null, null);
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x0002C6F0 File Offset: 0x0002A8F0
			internal CollectionDataContractCriticalHelper(Type type) : base(type)
			{
				if (type == Globals.TypeOfArray)
				{
					type = Globals.TypeOfObjectArray;
				}
				if (type.GetArrayRank() > 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Multi-dimensional arrays are not supported.")));
				}
				base.StableName = DataContract.GetStableName(type);
				this.Init(CollectionKind.Array, type.GetElementType(), null);
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x0002C754 File Offset: 0x0002A954
			internal CollectionDataContractCriticalHelper(Type type, DataContract itemContract) : base(type)
			{
				if (type.GetArrayRank() > 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Multi-dimensional arrays are not supported.")));
				}
				base.StableName = DataContract.CreateQualifiedName("ArrayOf" + itemContract.StableName.Name, itemContract.StableName.Namespace);
				this.itemContract = itemContract;
				this.Init(CollectionKind.Array, type.GetElementType(), null);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x0002C7C8 File Offset: 0x0002A9C8
			internal CollectionDataContractCriticalHelper(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, string serializationExceptionMessage, string deserializationExceptionMessage) : base(type)
			{
				if (getEnumeratorMethod == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Collection type '{0}' does not have a valid GetEnumerator method.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				if (itemType == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Collection type '{0}' must have a non-null item type.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				CollectionDataContractAttribute collectionContractAttribute;
				base.StableName = DataContract.GetCollectionStableName(type, itemType, out collectionContractAttribute);
				this.Init(kind, itemType, collectionContractAttribute);
				this.getEnumeratorMethod = getEnumeratorMethod;
				this.serializationExceptionMessage = serializationExceptionMessage;
				this.deserializationExceptionMessage = deserializationExceptionMessage;
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x0002C868 File Offset: 0x0002AA68
			internal CollectionDataContractCriticalHelper(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor) : this(type, kind, itemType, getEnumeratorMethod, null, null)
			{
				if (addMethod == null && !type.IsInterface)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Collection type '{0}' does not have a valid Add method.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				this.addMethod = addMethod;
				this.constructor = constructor;
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
			internal CollectionDataContractCriticalHelper(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor, bool isConstructorCheckRequired) : this(type, kind, itemType, getEnumeratorMethod, addMethod, constructor)
			{
				this.isConstructorCheckRequired = isConstructorCheckRequired;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x0002C8E1 File Offset: 0x0002AAE1
			internal CollectionDataContractCriticalHelper(Type type, string invalidCollectionInSharedContractMessage) : base(type)
			{
				this.Init(CollectionKind.Collection, null, null);
				this.invalidCollectionInSharedContractMessage = invalidCollectionInSharedContractMessage;
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002C8FA File Offset: 0x0002AAFA
			internal CollectionKind Kind
			{
				get
				{
					return this.kind;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002C902 File Offset: 0x0002AB02
			internal Type ItemType
			{
				get
				{
					return this.itemType;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002C90C File Offset: 0x0002AB0C
			// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002C9D9 File Offset: 0x0002ABD9
			internal DataContract ItemContract
			{
				get
				{
					if (this.itemContract == null && base.UnderlyingType != null)
					{
						if (this.IsDictionary)
						{
							if (string.CompareOrdinal(this.KeyName, this.ValueName) == 0)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("The collection data contract type '{0}' specifies the same value '{1}' for both the KeyName and the ValueName properties. This is not allowed. Consider changing either the KeyName or the ValueName property.", new object[]
								{
									DataContract.GetClrTypeFullName(base.UnderlyingType),
									this.KeyName
								}), base.UnderlyingType);
							}
							this.itemContract = ClassDataContract.CreateClassDataContractForKeyValue(this.ItemType, base.Namespace, new string[]
							{
								this.KeyName,
								this.ValueName
							});
							DataContract.GetDataContract(this.ItemType);
						}
						else
						{
							this.itemContract = DataContract.GetDataContract(this.ItemType);
						}
					}
					return this.itemContract;
				}
				set
				{
					this.itemContract = value;
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002C9E2 File Offset: 0x0002ABE2
			// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0002C9EA File Offset: 0x0002ABEA
			internal DataContract SharedTypeContract
			{
				get
				{
					return this.sharedTypeContract;
				}
				set
				{
					this.sharedTypeContract = value;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002C9F3 File Offset: 0x0002ABF3
			// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0002C9FB File Offset: 0x0002ABFB
			internal string ItemName
			{
				get
				{
					return this.itemName;
				}
				set
				{
					this.itemName = value;
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002CA04 File Offset: 0x0002AC04
			// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0002CA0C File Offset: 0x0002AC0C
			internal bool IsConstructorCheckRequired
			{
				get
				{
					return this.isConstructorCheckRequired;
				}
				set
				{
					this.isConstructorCheckRequired = value;
				}
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002CA15 File Offset: 0x0002AC15
			public XmlDictionaryString CollectionItemName
			{
				get
				{
					return this.collectionItemName;
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002CA1D File Offset: 0x0002AC1D
			// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0002CA25 File Offset: 0x0002AC25
			internal string KeyName
			{
				get
				{
					return this.keyName;
				}
				set
				{
					this.keyName = value;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0002CA2E File Offset: 0x0002AC2E
			// (set) Token: 0x06000A2E RID: 2606 RVA: 0x0002CA36 File Offset: 0x0002AC36
			internal string ValueName
			{
				get
				{
					return this.valueName;
				}
				set
				{
					this.valueName = value;
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002CA3F File Offset: 0x0002AC3F
			internal bool IsDictionary
			{
				get
				{
					return this.KeyName != null;
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002CA4A File Offset: 0x0002AC4A
			public string SerializationExceptionMessage
			{
				get
				{
					return this.serializationExceptionMessage;
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002CA52 File Offset: 0x0002AC52
			public string DeserializationExceptionMessage
			{
				get
				{
					return this.deserializationExceptionMessage;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002CA5A File Offset: 0x0002AC5A
			// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0002CA62 File Offset: 0x0002AC62
			public XmlDictionaryString ChildElementNamespace
			{
				get
				{
					return this.childElementNamespace;
				}
				set
				{
					this.childElementNamespace = value;
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002CA6B File Offset: 0x0002AC6B
			// (set) Token: 0x06000A35 RID: 2613 RVA: 0x0002CA73 File Offset: 0x0002AC73
			internal bool IsItemTypeNullable
			{
				get
				{
					return this.isItemTypeNullable;
				}
				set
				{
					this.isItemTypeNullable = value;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002CA7C File Offset: 0x0002AC7C
			internal MethodInfo GetEnumeratorMethod
			{
				get
				{
					return this.getEnumeratorMethod;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002CA84 File Offset: 0x0002AC84
			internal MethodInfo AddMethod
			{
				get
				{
					return this.addMethod;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002CA8C File Offset: 0x0002AC8C
			internal ConstructorInfo Constructor
			{
				get
				{
					return this.constructor;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002CA94 File Offset: 0x0002AC94
			// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002CB0C File Offset: 0x0002AD0C
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

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002CB15 File Offset: 0x0002AD15
			internal string InvalidCollectionInSharedContractMessage
			{
				get
				{
					return this.invalidCollectionInSharedContractMessage;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002CB1D File Offset: 0x0002AD1D
			internal bool ItemNameSetExplicit
			{
				get
				{
					return this.itemNameSetExplicit;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002CB25 File Offset: 0x0002AD25
			// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0002CB2D File Offset: 0x0002AD2D
			internal XmlFormatCollectionWriterDelegate XmlFormatWriterDelegate
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

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002CB36 File Offset: 0x0002AD36
			// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0002CB3E File Offset: 0x0002AD3E
			internal XmlFormatCollectionReaderDelegate XmlFormatReaderDelegate
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

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0002CB47 File Offset: 0x0002AD47
			// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0002CB4F File Offset: 0x0002AD4F
			internal XmlFormatGetOnlyCollectionReaderDelegate XmlFormatGetOnlyCollectionReaderDelegate
			{
				get
				{
					return this.xmlFormatGetOnlyCollectionReaderDelegate;
				}
				set
				{
					this.xmlFormatGetOnlyCollectionReaderDelegate = value;
				}
			}

			// Token: 0x04000436 RID: 1078
			private static Type[] _knownInterfaces;

			// Token: 0x04000437 RID: 1079
			private Type itemType;

			// Token: 0x04000438 RID: 1080
			private bool isItemTypeNullable;

			// Token: 0x04000439 RID: 1081
			private CollectionKind kind;

			// Token: 0x0400043A RID: 1082
			private readonly MethodInfo getEnumeratorMethod;

			// Token: 0x0400043B RID: 1083
			private readonly MethodInfo addMethod;

			// Token: 0x0400043C RID: 1084
			private readonly ConstructorInfo constructor;

			// Token: 0x0400043D RID: 1085
			private readonly string serializationExceptionMessage;

			// Token: 0x0400043E RID: 1086
			private readonly string deserializationExceptionMessage;

			// Token: 0x0400043F RID: 1087
			private DataContract itemContract;

			// Token: 0x04000440 RID: 1088
			private DataContract sharedTypeContract;

			// Token: 0x04000441 RID: 1089
			private Dictionary<XmlQualifiedName, DataContract> knownDataContracts;

			// Token: 0x04000442 RID: 1090
			private bool isKnownTypeAttributeChecked;

			// Token: 0x04000443 RID: 1091
			private string itemName;

			// Token: 0x04000444 RID: 1092
			private bool itemNameSetExplicit;

			// Token: 0x04000445 RID: 1093
			private XmlDictionaryString collectionItemName;

			// Token: 0x04000446 RID: 1094
			private string keyName;

			// Token: 0x04000447 RID: 1095
			private string valueName;

			// Token: 0x04000448 RID: 1096
			private XmlDictionaryString childElementNamespace;

			// Token: 0x04000449 RID: 1097
			private string invalidCollectionInSharedContractMessage;

			// Token: 0x0400044A RID: 1098
			private XmlFormatCollectionReaderDelegate xmlFormatReaderDelegate;

			// Token: 0x0400044B RID: 1099
			private XmlFormatGetOnlyCollectionReaderDelegate xmlFormatGetOnlyCollectionReaderDelegate;

			// Token: 0x0400044C RID: 1100
			private XmlFormatCollectionWriterDelegate xmlFormatWriterDelegate;

			// Token: 0x0400044D RID: 1101
			private bool isConstructorCheckRequired;
		}

		// Token: 0x020000B6 RID: 182
		public class DictionaryEnumerator : IEnumerator<KeyValue<object, object>>, IDisposable, IEnumerator
		{
			// Token: 0x06000A43 RID: 2627 RVA: 0x0002CB58 File Offset: 0x0002AD58
			public DictionaryEnumerator(IDictionaryEnumerator enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x06000A44 RID: 2628 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public void Dispose()
			{
			}

			// Token: 0x06000A45 RID: 2629 RVA: 0x0002CB67 File Offset: 0x0002AD67
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002CB74 File Offset: 0x0002AD74
			public KeyValue<object, object> Current
			{
				get
				{
					return new KeyValue<object, object>(this.enumerator.Key, this.enumerator.Value);
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002CB91 File Offset: 0x0002AD91
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A48 RID: 2632 RVA: 0x0002CB9E File Offset: 0x0002AD9E
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0400044E RID: 1102
			private IDictionaryEnumerator enumerator;
		}

		// Token: 0x020000B7 RID: 183
		public class GenericDictionaryEnumerator<K, V> : IEnumerator<KeyValue<K, V>>, IDisposable, IEnumerator
		{
			// Token: 0x06000A49 RID: 2633 RVA: 0x0002CBAB File Offset: 0x0002ADAB
			public GenericDictionaryEnumerator(IEnumerator<KeyValuePair<K, V>> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x06000A4A RID: 2634 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public void Dispose()
			{
			}

			// Token: 0x06000A4B RID: 2635 RVA: 0x0002CBBA File Offset: 0x0002ADBA
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
			public KeyValue<K, V> Current
			{
				get
				{
					KeyValuePair<K, V> keyValuePair = this.enumerator.Current;
					return new KeyValue<K, V>(keyValuePair.Key, keyValuePair.Value);
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x0002CC01 File Offset: 0x0002AE01
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0400044F RID: 1103
			private IEnumerator<KeyValuePair<K, V>> enumerator;
		}
	}
}
