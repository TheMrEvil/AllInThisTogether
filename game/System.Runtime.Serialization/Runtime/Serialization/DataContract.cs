using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Configuration;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x020000BB RID: 187
	internal abstract class DataContract
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		[SecuritySafeCritical]
		protected DataContract(DataContract.DataContractCriticalHelper helper)
		{
			this.helper = helper;
			this.name = helper.Name;
			this.ns = helper.Namespace;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002CDC7 File Offset: 0x0002AFC7
		internal static DataContract GetDataContract(Type type)
		{
			return DataContract.GetDataContract(type.TypeHandle, type, SerializationMode.SharedContract);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002CDD6 File Offset: 0x0002AFD6
		internal static DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type, SerializationMode mode)
		{
			return DataContract.GetDataContract(DataContract.GetId(typeHandle), typeHandle, mode);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002CDE5 File Offset: 0x0002AFE5
		internal static DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle, SerializationMode mode)
		{
			return DataContract.GetDataContractSkipValidation(id, typeHandle, null).GetValidContract(mode);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002CDF5 File Offset: 0x0002AFF5
		[SecuritySafeCritical]
		internal static DataContract GetDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
		{
			return DataContract.DataContractCriticalHelper.GetDataContractSkipValidation(id, typeHandle, type);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002CE00 File Offset: 0x0002B000
		internal static DataContract GetGetOnlyCollectionDataContract(int id, RuntimeTypeHandle typeHandle, Type type, SerializationMode mode)
		{
			DataContract dataContract = DataContract.GetGetOnlyCollectionDataContractSkipValidation(id, typeHandle, type);
			dataContract = dataContract.GetValidContract(mode);
			if (dataContract is ClassDataContract)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("For '{0}' type, class data contract was returned for get-only collection.", new object[]
				{
					DataContract.GetClrTypeFullName(dataContract.UnderlyingType)
				})));
			}
			return dataContract;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002CE50 File Offset: 0x0002B050
		[SecuritySafeCritical]
		internal static DataContract GetGetOnlyCollectionDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
		{
			return DataContract.DataContractCriticalHelper.GetGetOnlyCollectionDataContractSkipValidation(id, typeHandle, type);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002CE5A File Offset: 0x0002B05A
		[SecuritySafeCritical]
		internal static DataContract GetDataContractForInitialization(int id)
		{
			return DataContract.DataContractCriticalHelper.GetDataContractForInitialization(id);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002CE62 File Offset: 0x0002B062
		[SecuritySafeCritical]
		internal static int GetIdForInitialization(ClassDataContract classContract)
		{
			return DataContract.DataContractCriticalHelper.GetIdForInitialization(classContract);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002CE6A File Offset: 0x0002B06A
		[SecuritySafeCritical]
		internal static int GetId(RuntimeTypeHandle typeHandle)
		{
			return DataContract.DataContractCriticalHelper.GetId(typeHandle);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002CE72 File Offset: 0x0002B072
		[SecuritySafeCritical]
		public static DataContract GetBuiltInDataContract(Type type)
		{
			return DataContract.DataContractCriticalHelper.GetBuiltInDataContract(type);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002CE7A File Offset: 0x0002B07A
		[SecuritySafeCritical]
		public static DataContract GetBuiltInDataContract(string name, string ns)
		{
			return DataContract.DataContractCriticalHelper.GetBuiltInDataContract(name, ns);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002CE83 File Offset: 0x0002B083
		[SecuritySafeCritical]
		public static DataContract GetBuiltInDataContract(string typeName)
		{
			return DataContract.DataContractCriticalHelper.GetBuiltInDataContract(typeName);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002CE8B File Offset: 0x0002B08B
		[SecuritySafeCritical]
		internal static string GetNamespace(string key)
		{
			return DataContract.DataContractCriticalHelper.GetNamespace(key);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002CE93 File Offset: 0x0002B093
		[SecuritySafeCritical]
		internal static XmlDictionaryString GetClrTypeString(string key)
		{
			return DataContract.DataContractCriticalHelper.GetClrTypeString(key);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002CE9B File Offset: 0x0002B09B
		[SecuritySafeCritical]
		internal static void ThrowInvalidDataContractException(string message, Type type)
		{
			DataContract.DataContractCriticalHelper.ThrowInvalidDataContractException(message, type);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002CEA4 File Offset: 0x0002B0A4
		protected DataContract.DataContractCriticalHelper Helper
		{
			[SecurityCritical]
			get
			{
				return this.helper;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002CEAC File Offset: 0x0002B0AC
		internal Type UnderlyingType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.UnderlyingType;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002CEB9 File Offset: 0x0002B0B9
		internal Type OriginalUnderlyingType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.OriginalUnderlyingType;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002CEC6 File Offset: 0x0002B0C6
		internal virtual bool IsBuiltInDataContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsBuiltInDataContract;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0002CED3 File Offset: 0x0002B0D3
		internal Type TypeForInitialization
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.TypeForInitialization;
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002CEE0 File Offset: 0x0002B0E0
		public virtual void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("An internal error has occurred. Unexpected contract type '{0}' for type '{1}' encountered.", new object[]
			{
				DataContract.GetClrTypeFullName(base.GetType()),
				DataContract.GetClrTypeFullName(this.UnderlyingType)
			})));
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002CEE0 File Offset: 0x0002B0E0
		public virtual object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("An internal error has occurred. Unexpected contract type '{0}' for type '{1}' encountered.", new object[]
			{
				DataContract.GetClrTypeFullName(base.GetType()),
				DataContract.GetClrTypeFullName(this.UnderlyingType)
			})));
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002CF18 File Offset: 0x0002B118
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0002CF25 File Offset: 0x0002B125
		internal bool IsValueType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsValueType;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsValueType = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002CF33 File Offset: 0x0002B133
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002CF40 File Offset: 0x0002B140
		internal bool IsReference
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsReference;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsReference = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002CF4E File Offset: 0x0002B14E
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002CF5B File Offset: 0x0002B15B
		internal XmlQualifiedName StableName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.StableName;
			}
			[SecurityCritical]
			set
			{
				this.helper.StableName = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002CF69 File Offset: 0x0002B169
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002CF76 File Offset: 0x0002B176
		internal GenericInfo GenericInfo
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.GenericInfo;
			}
			[SecurityCritical]
			set
			{
				this.helper.GenericInfo = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002CF84 File Offset: 0x0002B184
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002CF91 File Offset: 0x0002B191
		internal virtual Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
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

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002CF9F File Offset: 0x0002B19F
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		internal virtual bool IsISerializable
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002CFBA File Offset: 0x0002B1BA
		internal XmlDictionaryString Name
		{
			[SecuritySafeCritical]
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002CFC2 File Offset: 0x0002B1C2
		public virtual XmlDictionaryString Namespace
		{
			[SecuritySafeCritical]
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000066E8 File Offset: 0x000048E8
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal virtual bool HasRoot
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002CFCA File Offset: 0x0002B1CA
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0002CFD7 File Offset: 0x0002B1D7
		internal virtual XmlDictionaryString TopLevelElementName
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002CFE5 File Offset: 0x0002B1E5
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0002CFF2 File Offset: 0x0002B1F2
		internal virtual XmlDictionaryString TopLevelElementNamespace
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000066E8 File Offset: 0x000048E8
		internal virtual bool CanContainReferences
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00003127 File Offset: 0x00001327
		internal virtual bool IsPrimitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002D000 File Offset: 0x0002B200
		internal virtual void WriteRootElement(XmlWriterDelegator writer, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (ns == DictionaryGlobals.SerializationNamespace && !this.IsPrimitive)
			{
				writer.WriteStartElement("z", name, ns);
				return;
			}
			writer.WriteStartElement(name, ns);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00002068 File Offset: 0x00000268
		internal virtual DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
		{
			return this;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00002068 File Offset: 0x00000268
		internal virtual DataContract GetValidContract(SerializationMode mode)
		{
			return this;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00002068 File Offset: 0x00000268
		internal virtual DataContract GetValidContract()
		{
			return this;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000066E8 File Offset: 0x000048E8
		internal virtual bool IsValidContract(SerializationMode mode)
		{
			return true;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0002D028 File Offset: 0x0002B228
		internal MethodInfo ParseMethod
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ParseMethod;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002D035 File Offset: 0x0002B235
		internal static bool IsTypeSerializable(Type type)
		{
			return DataContract.IsTypeSerializable(type, new Dictionary<Type, object>());
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002D044 File Offset: 0x0002B244
		private static bool IsTypeSerializable(Type type, Dictionary<Type, object> previousCollectionTypes)
		{
			if (type.IsSerializable || type.IsDefined(Globals.TypeOfDataContractAttribute, false) || type.IsInterface || type.IsPointer || Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
			{
				return true;
			}
			Type type2;
			if (CollectionDataContract.IsCollection(type, out type2))
			{
				DataContract.ValidatePreviousCollectionTypes(type, type2, previousCollectionTypes);
				if (DataContract.IsTypeSerializable(type2, previousCollectionTypes))
				{
					return true;
				}
			}
			return DataContract.GetBuiltInDataContract(type) != null || ClassDataContract.IsNonAttributedTypeValidForSerialization(type);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002D0B4 File Offset: 0x0002B2B4
		private static void ValidatePreviousCollectionTypes(Type collectionType, Type itemType, Dictionary<Type, object> previousCollectionTypes)
		{
			previousCollectionTypes.Add(collectionType, collectionType);
			while (itemType.IsArray)
			{
				itemType = itemType.GetElementType();
			}
			List<Type> list = new List<Type>();
			Queue<Type> queue = new Queue<Type>();
			queue.Enqueue(itemType);
			list.Add(itemType);
			while (queue.Count > 0)
			{
				itemType = queue.Dequeue();
				if (previousCollectionTypes.ContainsKey(itemType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' involves recursive collection.", new object[]
					{
						DataContract.GetClrTypeFullName(itemType)
					})));
				}
				if (itemType.IsGenericType)
				{
					foreach (Type item in itemType.GetGenericArguments())
					{
						if (!list.Contains(item))
						{
							queue.Enqueue(item);
							list.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002D174 File Offset: 0x0002B374
		internal static Type UnwrapRedundantNullableType(Type type)
		{
			Type result = type;
			while (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable)
			{
				result = type;
				type = type.GetGenericArguments()[0];
			}
			return result;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		internal static Type UnwrapNullableType(Type type)
		{
			while (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable)
			{
				type = type.GetGenericArguments()[0];
			}
			return type;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002D1D5 File Offset: 0x0002B3D5
		private static bool IsAlpha(char ch)
		{
			return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002D1F2 File Offset: 0x0002B3F2
		private static bool IsDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002D204 File Offset: 0x0002B404
		private static bool IsAsciiLocalName(string localName)
		{
			if (localName.Length == 0)
			{
				return false;
			}
			if (!DataContract.IsAlpha(localName[0]))
			{
				return false;
			}
			for (int i = 1; i < localName.Length; i++)
			{
				char ch = localName[i];
				if (!DataContract.IsAlpha(ch) && !DataContract.IsDigit(ch))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002D257 File Offset: 0x0002B457
		internal static string EncodeLocalName(string localName)
		{
			if (DataContract.IsAsciiLocalName(localName))
			{
				return localName;
			}
			if (DataContract.IsValidNCName(localName))
			{
				return localName;
			}
			return XmlConvert.EncodeLocalName(localName);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002D274 File Offset: 0x0002B474
		internal static bool IsValidNCName(string name)
		{
			bool result;
			try
			{
				XmlConvert.VerifyNCName(name);
				result = true;
			}
			catch (XmlException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002D2A4 File Offset: 0x0002B4A4
		internal static XmlQualifiedName GetStableName(Type type)
		{
			bool flag;
			return DataContract.GetStableName(type, out flag);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002D2B9 File Offset: 0x0002B4B9
		internal static XmlQualifiedName GetStableName(Type type, out bool hasDataContract)
		{
			return DataContract.GetStableName(type, new Dictionary<Type, object>(), out hasDataContract);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		private static XmlQualifiedName GetStableName(Type type, Dictionary<Type, object> previousCollectionTypes, out bool hasDataContract)
		{
			type = DataContract.UnwrapRedundantNullableType(type);
			XmlQualifiedName result;
			DataContractAttribute dataContractAttribute;
			if (DataContract.TryGetBuiltInXmlAndArrayTypeStableName(type, previousCollectionTypes, out result))
			{
				hasDataContract = false;
			}
			else if (DataContract.TryGetDCAttribute(type, out dataContractAttribute))
			{
				result = DataContract.GetDCTypeStableName(type, dataContractAttribute);
				hasDataContract = true;
			}
			else
			{
				result = DataContract.GetNonDCTypeStableName(type, previousCollectionTypes);
				hasDataContract = false;
			}
			return result;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002D310 File Offset: 0x0002B510
		private static XmlQualifiedName GetDCTypeStableName(Type type, DataContractAttribute dataContractAttribute)
		{
			string text;
			if (dataContractAttribute.IsNameSetExplicitly)
			{
				text = dataContractAttribute.Name;
				if (text == null || text.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have DataContractAttribute attribute Name set to null or empty string.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				if (type.IsGenericType && !type.IsGenericTypeDefinition)
				{
					text = DataContract.ExpandGenericParameters(text, type);
				}
				text = DataContract.EncodeLocalName(text);
			}
			else
			{
				text = DataContract.GetDefaultStableLocalName(type);
			}
			string text2;
			if (dataContractAttribute.IsNamespaceSetExplicitly)
			{
				text2 = dataContractAttribute.Namespace;
				if (text2 == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have DataContractAttribute attribute Namespace set to null.", new object[]
					{
						DataContract.GetClrTypeFullName(type)
					})));
				}
				DataContract.CheckExplicitDataContractNamespaceUri(text2, type);
			}
			else
			{
				text2 = DataContract.GetDefaultDataContractNamespace(type);
			}
			return DataContract.CreateQualifiedName(text, text2);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		private static XmlQualifiedName GetNonDCTypeStableName(Type type, Dictionary<Type, object> previousCollectionTypes)
		{
			Type itemType;
			if (CollectionDataContract.IsCollection(type, out itemType))
			{
				DataContract.ValidatePreviousCollectionTypes(type, itemType, previousCollectionTypes);
				CollectionDataContractAttribute collectionDataContractAttribute;
				return DataContract.GetCollectionStableName(type, itemType, previousCollectionTypes, out collectionDataContractAttribute);
			}
			string defaultStableLocalName = DataContract.GetDefaultStableLocalName(type);
			string text;
			if (ClassDataContract.IsNonAttributedTypeValidForSerialization(type))
			{
				text = DataContract.GetDefaultDataContractNamespace(type);
			}
			else
			{
				text = DataContract.GetDefaultStableNamespace(type);
			}
			return DataContract.CreateQualifiedName(defaultStableLocalName, text);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002D424 File Offset: 0x0002B624
		private static bool TryGetBuiltInXmlAndArrayTypeStableName(Type type, Dictionary<Type, object> previousCollectionTypes, out XmlQualifiedName stableName)
		{
			stableName = null;
			DataContract builtInDataContract = DataContract.GetBuiltInDataContract(type);
			if (builtInDataContract != null)
			{
				stableName = builtInDataContract.StableName;
			}
			else if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
			{
				XmlQualifiedName xmlQualifiedName;
				XmlSchemaType xmlSchemaType;
				bool flag;
				SchemaExporter.GetXmlTypeInfo(type, out xmlQualifiedName, out xmlSchemaType, out flag);
				stableName = xmlQualifiedName;
			}
			else if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				DataContract.ValidatePreviousCollectionTypes(type, elementType, previousCollectionTypes);
				CollectionDataContractAttribute collectionDataContractAttribute;
				stableName = DataContract.GetCollectionStableName(type, elementType, previousCollectionTypes, out collectionDataContractAttribute);
			}
			return stableName != null;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002D494 File Offset: 0x0002B694
		[SecuritySafeCritical]
		internal static bool TryGetDCAttribute(Type type, out DataContractAttribute dataContractAttribute)
		{
			dataContractAttribute = null;
			object[] customAttributes = type.GetCustomAttributes(Globals.TypeOfDataContractAttribute, false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				dataContractAttribute = (DataContractAttribute)customAttributes[0];
			}
			return dataContractAttribute != null;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002D4C7 File Offset: 0x0002B6C7
		internal static XmlQualifiedName GetCollectionStableName(Type type, Type itemType, out CollectionDataContractAttribute collectionContractAttribute)
		{
			return DataContract.GetCollectionStableName(type, itemType, new Dictionary<Type, object>(), out collectionContractAttribute);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002D4D8 File Offset: 0x0002B6D8
		private static XmlQualifiedName GetCollectionStableName(Type type, Type itemType, Dictionary<Type, object> previousCollectionTypes, out CollectionDataContractAttribute collectionContractAttribute)
		{
			object[] customAttributes = type.GetCustomAttributes(Globals.TypeOfCollectionDataContractAttribute, false);
			string text;
			string text2;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				collectionContractAttribute = (CollectionDataContractAttribute)customAttributes[0];
				if (collectionContractAttribute.IsNameSetExplicitly)
				{
					text = collectionContractAttribute.Name;
					if (text == null || text.Length == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have CollectionDataContractAttribute attribute Name set to null or empty string.", new object[]
						{
							DataContract.GetClrTypeFullName(type)
						})));
					}
					if (type.IsGenericType && !type.IsGenericTypeDefinition)
					{
						text = DataContract.ExpandGenericParameters(text, type);
					}
					text = DataContract.EncodeLocalName(text);
				}
				else
				{
					text = DataContract.GetDefaultStableLocalName(type);
				}
				if (collectionContractAttribute.IsNamespaceSetExplicitly)
				{
					text2 = collectionContractAttribute.Namespace;
					if (text2 == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("Type '{0}' cannot have CollectionDataContractAttribute attribute Namespace set to null.", new object[]
						{
							DataContract.GetClrTypeFullName(type)
						})));
					}
					DataContract.CheckExplicitDataContractNamespaceUri(text2, type);
				}
				else
				{
					text2 = DataContract.GetDefaultDataContractNamespace(type);
				}
			}
			else
			{
				collectionContractAttribute = null;
				string str = "ArrayOf" + DataContract.GetArrayPrefix(ref itemType);
				bool flag;
				XmlQualifiedName stableName = DataContract.GetStableName(itemType, previousCollectionTypes, out flag);
				text = str + stableName.Name;
				text2 = DataContract.GetCollectionNamespace(stableName.Namespace);
			}
			return DataContract.CreateQualifiedName(text, text2);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		private static string GetArrayPrefix(ref Type itemType)
		{
			string text = string.Empty;
			while (itemType.IsArray && DataContract.GetBuiltInDataContract(itemType) == null)
			{
				text += "ArrayOf";
				itemType = itemType.GetElementType();
			}
			return text;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002D63C File Offset: 0x0002B83C
		internal XmlQualifiedName GetArrayTypeName(bool isNullable)
		{
			XmlQualifiedName xmlQualifiedName;
			if (this.IsValueType && isNullable)
			{
				GenericInfo genericInfo = new GenericInfo(DataContract.GetStableName(Globals.TypeOfNullable), Globals.TypeOfNullable.FullName);
				genericInfo.Add(new GenericInfo(this.StableName, null));
				genericInfo.AddToLevel(0, 1);
				xmlQualifiedName = genericInfo.GetExpandedStableName();
			}
			else
			{
				xmlQualifiedName = this.StableName;
			}
			string collectionNamespace = DataContract.GetCollectionNamespace(xmlQualifiedName.Namespace);
			return new XmlQualifiedName("ArrayOf" + xmlQualifiedName.Name, collectionNamespace);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002D6B7 File Offset: 0x0002B8B7
		internal static string GetCollectionNamespace(string elementNs)
		{
			if (!DataContract.IsBuiltInNamespace(elementNs))
			{
				return elementNs;
			}
			return "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002D6C8 File Offset: 0x0002B8C8
		internal static XmlQualifiedName GetDefaultStableName(Type type)
		{
			return DataContract.CreateQualifiedName(DataContract.GetDefaultStableLocalName(type), DataContract.GetDefaultStableNamespace(type));
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002D6DC File Offset: 0x0002B8DC
		private static string GetDefaultStableLocalName(Type type)
		{
			if (type.IsGenericParameter)
			{
				return "{" + type.GenericParameterPosition.ToString() + "}";
			}
			string text = null;
			if (type.IsArray)
			{
				text = DataContract.GetArrayPrefix(ref type);
			}
			string text2;
			if (type.DeclaringType == null)
			{
				text2 = type.Name;
			}
			else
			{
				int num = (type.Namespace == null) ? 0 : type.Namespace.Length;
				if (num > 0)
				{
					num++;
				}
				text2 = DataContract.GetClrTypeFullName(type).Substring(num).Replace('+', '.');
			}
			if (text != null)
			{
				text2 = text + text2;
			}
			if (type.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				bool flag = true;
				int num2 = text2.IndexOf('[');
				if (num2 >= 0)
				{
					text2 = text2.Substring(0, num2);
				}
				IList<int> dataContractNameForGenericName = DataContract.GetDataContractNameForGenericName(text2, stringBuilder);
				bool isGenericTypeDefinition = type.IsGenericTypeDefinition;
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					Type type2 = genericArguments[i];
					if (isGenericTypeDefinition)
					{
						stringBuilder.Append("{").Append(i).Append("}");
					}
					else
					{
						XmlQualifiedName stableName = DataContract.GetStableName(type2);
						stringBuilder.Append(stableName.Name);
						stringBuilder2.Append(" ").Append(stableName.Namespace);
						if (flag)
						{
							flag = DataContract.IsBuiltInNamespace(stableName.Namespace);
						}
					}
				}
				if (isGenericTypeDefinition)
				{
					stringBuilder.Append("{#}");
				}
				else if (dataContractNameForGenericName.Count > 1 || !flag)
				{
					foreach (int value in dataContractNameForGenericName)
					{
						stringBuilder2.Insert(0, value).Insert(0, " ");
					}
					stringBuilder.Append(DataContract.GetNamespacesDigest(stringBuilder2.ToString()));
				}
				text2 = stringBuilder.ToString();
			}
			return DataContract.EncodeLocalName(text2);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002D8DC File Offset: 0x0002BADC
		private static string GetDefaultDataContractNamespace(Type type)
		{
			string text = type.Namespace;
			if (text == null)
			{
				text = string.Empty;
			}
			string text2 = DataContract.GetGlobalDataContractNamespace(text, type.Module);
			if (text2 == null)
			{
				text2 = DataContract.GetGlobalDataContractNamespace(text, type.Assembly);
			}
			if (text2 == null)
			{
				text2 = DataContract.GetDefaultStableNamespace(type);
			}
			else
			{
				DataContract.CheckExplicitDataContractNamespaceUri(text2, type);
			}
			return text2;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002D92C File Offset: 0x0002BB2C
		internal static IList<int> GetDataContractNameForGenericName(string typeName, StringBuilder localName)
		{
			List<int> list = new List<int>();
			int num = 0;
			int num2;
			for (;;)
			{
				num2 = typeName.IndexOf('`', num);
				if (num2 < 0)
				{
					break;
				}
				if (localName != null)
				{
					localName.Append(typeName.Substring(num, num2 - num));
				}
				while ((num = typeName.IndexOf('.', num + 1, num2 - num - 1)) >= 0)
				{
					list.Add(0);
				}
				num = typeName.IndexOf('.', num2);
				if (num < 0)
				{
					goto Block_5;
				}
				list.Add(int.Parse(typeName.Substring(num2 + 1, num - num2 - 1), CultureInfo.InvariantCulture));
			}
			if (localName != null)
			{
				localName.Append(typeName.Substring(num));
			}
			list.Add(0);
			goto IL_AE;
			Block_5:
			list.Add(int.Parse(typeName.Substring(num2 + 1), CultureInfo.InvariantCulture));
			IL_AE:
			if (localName != null)
			{
				localName.Append("Of");
			}
			return list;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002D9F7 File Offset: 0x0002BBF7
		internal static bool IsBuiltInNamespace(string ns)
		{
			return ns == "http://www.w3.org/2001/XMLSchema" || ns == "http://schemas.microsoft.com/2003/10/Serialization/";
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002DA13 File Offset: 0x0002BC13
		internal static string GetDefaultStableNamespace(Type type)
		{
			if (type.IsGenericParameter)
			{
				return "{ns}";
			}
			return DataContract.GetDefaultStableNamespace(type.Namespace);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002DA2E File Offset: 0x0002BC2E
		internal static XmlQualifiedName CreateQualifiedName(string localName, string ns)
		{
			return new XmlQualifiedName(localName, DataContract.GetNamespace(ns));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002DA3C File Offset: 0x0002BC3C
		internal static string GetDefaultStableNamespace(string clrNs)
		{
			if (clrNs == null)
			{
				clrNs = string.Empty;
			}
			return new Uri(Globals.DataContractXsdBaseNamespaceUri, clrNs).AbsoluteUri;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002DA58 File Offset: 0x0002BC58
		private static void CheckExplicitDataContractNamespaceUri(string dataContractNs, Type type)
		{
			if (dataContractNs.Length > 0)
			{
				string text = dataContractNs.Trim();
				if (text.Length == 0 || text.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					DataContract.ThrowInvalidDataContractException(SR.GetString("DataContract namespace '{0}' is not a valid URI.", new object[]
					{
						dataContractNs
					}), type);
				}
				dataContractNs = text;
			}
			Uri uri;
			if (Uri.TryCreate(dataContractNs, UriKind.RelativeOrAbsolute, out uri))
			{
				if (uri.ToString() == "http://schemas.microsoft.com/2003/10/Serialization/")
				{
					DataContract.ThrowInvalidDataContractException(SR.GetString("DataContract namespace '{0}' cannot be specified since it is reserved.", new object[]
					{
						"http://schemas.microsoft.com/2003/10/Serialization/"
					}), type);
					return;
				}
			}
			else
			{
				DataContract.ThrowInvalidDataContractException(SR.GetString("DataContract namespace '{0}' is not a valid URI.", new object[]
				{
					dataContractNs
				}), type);
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002DAFF File Offset: 0x0002BCFF
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsGenericTypeDefinition || !type.ContainsGenericParameters)
			{
				return type.FullName;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", type.Namespace, type.Name);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002DB34 File Offset: 0x0002BD34
		internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
		{
			hasTypeForwardedFrom = false;
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				TypeForwardedFromAttribute typeForwardedFromAttribute = (TypeForwardedFromAttribute)customAttributes[0];
				hasTypeForwardedFrom = true;
				return typeForwardedFromAttribute.AssemblyFullName;
			}
			return type.Assembly.FullName;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002DB79 File Offset: 0x0002BD79
		internal static string GetClrTypeFullNameUsingTypeForwardedFromAttribute(Type type)
		{
			if (type.IsArray)
			{
				return DataContract.GetClrTypeFullNameForArray(type);
			}
			return DataContract.GetClrTypeFullNameForNonArrayTypes(type);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002DB90 File Offset: 0x0002BD90
		private static string GetClrTypeFullNameForArray(Type type)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", DataContract.GetClrTypeFullNameUsingTypeForwardedFromAttribute(type.GetElementType()), "[", "]");
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002DBB8 File Offset: 0x0002BDB8
		private static string GetClrTypeFullNameForNonArrayTypes(Type type)
		{
			if (!type.IsGenericType)
			{
				return DataContract.GetClrTypeFullName(type);
			}
			Type[] genericArguments = type.GetGenericArguments();
			StringBuilder stringBuilder = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append("[");
			foreach (Type type2 in genericArguments)
			{
				stringBuilder.Append("[").Append(DataContract.GetClrTypeFullNameUsingTypeForwardedFromAttribute(type2)).Append(",");
				bool flag;
				stringBuilder.Append(" ").Append(DataContract.GetClrAssemblyName(type2, out flag));
				stringBuilder.Append("]").Append(",");
			}
			return stringBuilder.Remove(stringBuilder.Length - 1, 1).Append("]").ToString();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002DC78 File Offset: 0x0002BE78
		internal static void GetClrNameAndNamespace(string fullTypeName, out string localName, out string ns)
		{
			int num = fullTypeName.LastIndexOf('.');
			if (num < 0)
			{
				ns = string.Empty;
				localName = fullTypeName.Replace('+', '.');
			}
			else
			{
				ns = fullTypeName.Substring(0, num);
				localName = fullTypeName.Substring(num + 1).Replace('+', '.');
			}
			int num2 = localName.IndexOf('[');
			if (num2 >= 0)
			{
				localName = localName.Substring(0, num2);
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002DCDE File Offset: 0x0002BEDE
		internal static void GetDefaultStableName(string fullTypeName, out string localName, out string ns)
		{
			DataContract.GetDefaultStableName(new CodeTypeReference(fullTypeName), out localName, out ns);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002DCF0 File Offset: 0x0002BEF0
		private static void GetDefaultStableName(CodeTypeReference typeReference, out string localName, out string ns)
		{
			string baseType = typeReference.BaseType;
			DataContract builtInDataContract = DataContract.GetBuiltInDataContract(baseType);
			if (builtInDataContract != null)
			{
				localName = builtInDataContract.StableName.Name;
				ns = builtInDataContract.StableName.Namespace;
				return;
			}
			DataContract.GetClrNameAndNamespace(baseType, out localName, out ns);
			if (typeReference.TypeArguments.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				bool flag = true;
				IList<int> dataContractNameForGenericName = DataContract.GetDataContractNameForGenericName(localName, stringBuilder);
				foreach (object obj in typeReference.TypeArguments)
				{
					string value;
					string value2;
					DataContract.GetDefaultStableName((CodeTypeReference)obj, out value, out value2);
					stringBuilder.Append(value);
					stringBuilder2.Append(" ").Append(value2);
					if (flag)
					{
						flag = DataContract.IsBuiltInNamespace(value2);
					}
				}
				if (dataContractNameForGenericName.Count > 1 || !flag)
				{
					foreach (int value3 in dataContractNameForGenericName)
					{
						stringBuilder2.Insert(0, value3).Insert(0, " ");
					}
					stringBuilder.Append(DataContract.GetNamespacesDigest(stringBuilder2.ToString()));
				}
				localName = stringBuilder.ToString();
			}
			localName = DataContract.EncodeLocalName(localName);
			ns = DataContract.GetDefaultStableNamespace(ns);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002DE60 File Offset: 0x0002C060
		internal static string GetDataContractNamespaceFromUri(string uriString)
		{
			if (!uriString.StartsWith("http://schemas.datacontract.org/2004/07/", StringComparison.Ordinal))
			{
				return uriString;
			}
			return uriString.Substring("http://schemas.datacontract.org/2004/07/".Length);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002DE84 File Offset: 0x0002C084
		private static string GetGlobalDataContractNamespace(string clrNs, ICustomAttributeProvider customAttribuetProvider)
		{
			object[] customAttributes = customAttribuetProvider.GetCustomAttributes(typeof(ContractNamespaceAttribute), false);
			string text = null;
			foreach (ContractNamespaceAttribute contractNamespaceAttribute in customAttributes)
			{
				string text2 = contractNamespaceAttribute.ClrNamespace;
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				if (text2 == clrNs)
				{
					if (contractNamespaceAttribute.ContractNamespace == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("CLR namespace '{0}' cannot have ContractNamespace set to null.", new object[]
						{
							clrNs
						})));
					}
					if (text != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("ContractNamespaceAttribute attribute maps CLR namespace '{2}' to multiple data contract namespaces '{0}' and '{1}'. You can map a CLR namespace to only one data contract namespace.", new object[]
						{
							text,
							contractNamespaceAttribute.ContractNamespace,
							clrNs
						})));
					}
					text = contractNamespaceAttribute.ContractNamespace;
				}
			}
			return text;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002DF40 File Offset: 0x0002C140
		private static string GetNamespacesDigest(string namespaces)
		{
			byte[] inArray = HashHelper.ComputeHash(Encoding.UTF8.GetBytes(namespaces));
			char[] array = new char[24];
			int num = Convert.ToBase64CharArray(inArray, 0, 6, array, 0);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				if (c != '+')
				{
					if (c != '/')
					{
						if (c != '=')
						{
							stringBuilder.Append(c);
						}
					}
					else
					{
						stringBuilder.Append("_S");
					}
				}
				else
				{
					stringBuilder.Append("_P");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		private static string ExpandGenericParameters(string format, Type type)
		{
			GenericNameProvider genericNameProvider = new GenericNameProvider(type);
			return DataContract.ExpandGenericParameters(format, genericNameProvider);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002DFE4 File Offset: 0x0002C1E4
		internal static string ExpandGenericParameters(string format, IGenericNameProvider genericNameProvider)
		{
			string text = null;
			StringBuilder stringBuilder = new StringBuilder();
			IList<int> nestedParameterCounts = genericNameProvider.GetNestedParameterCounts();
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					i++;
					int num = i;
					while (i < format.Length && format[i] != '}')
					{
						i++;
					}
					if (i == format.Length)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("The data contract name '{0}' for type '{1}' has a curly brace '{{' that is not matched with a closing curly brace. Curly braces have special meaning in data contract names - they are used to customize the naming of data contracts for generic types.", new object[]
						{
							format,
							genericNameProvider.GetGenericTypeName()
						})));
					}
					if (format[num] == '#' && i == num + 1)
					{
						if (nestedParameterCounts.Count > 1 || !genericNameProvider.ParametersFromBuiltInNamespaces)
						{
							if (text == null)
							{
								StringBuilder stringBuilder2 = new StringBuilder(genericNameProvider.GetNamespaces());
								foreach (int value in nestedParameterCounts)
								{
									stringBuilder2.Insert(0, value).Insert(0, " ");
								}
								text = DataContract.GetNamespacesDigest(stringBuilder2.ToString());
							}
							stringBuilder.Append(text);
						}
					}
					else
					{
						int num2;
						if (!int.TryParse(format.Substring(num, i - num), out num2) || num2 < 0 || num2 >= genericNameProvider.GetParameterCount())
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("In the data contract name for type '{1}', there are curly braces with '{0}' inside, which is an invalid value. Curly braces have special meaning in data contract names - they are used to customize the naming of data contracts for generic types. Based on the number of generic parameters this type has, the contents of the curly braces must either be a number between 0 and '{2}' to insert the name of the generic parameter at that index or the '#' symbol to insert a digest of the generic parameter namespaces.", new object[]
							{
								format.Substring(num, i - num),
								genericNameProvider.GetGenericTypeName(),
								genericNameProvider.GetParameterCount() - 1
							})));
						}
						stringBuilder.Append(genericNameProvider.GetParameterName(num2));
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002E1A8 File Offset: 0x0002C3A8
		internal static bool IsTypeNullable(Type type)
		{
			return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002E1CE File Offset: 0x0002C3CE
		public static void ThrowTypeNotSerializable(Type type)
		{
			DataContract.ThrowInvalidDataContractException(SR.GetString("Type '{0}' cannot be serialized. Consider marking it with the DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute attribute. Alternatively, you can ensure that the type is public and has a parameterless constructor - all public members of the type will then be serialized, and no attributes will be required.", new object[]
			{
				type
			}), type);
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002E1EA File Offset: 0x0002C3EA
		private static DataContractSerializerSection ConfigSection
		{
			[SecurityCritical]
			get
			{
				if (DataContract.configSection == null)
				{
					DataContract.configSection = DataContractSerializerSection.UnsafeGetSection();
				}
				return DataContract.configSection;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002E204 File Offset: 0x0002C404
		internal static Dictionary<XmlQualifiedName, DataContract> ImportKnownTypeAttributes(Type type)
		{
			Dictionary<XmlQualifiedName, DataContract> result = null;
			Dictionary<Type, Type> typesChecked = new Dictionary<Type, Type>();
			DataContract.ImportKnownTypeAttributes(type, typesChecked, ref result);
			return result;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002E224 File Offset: 0x0002C424
		private static void ImportKnownTypeAttributes(Type type, Dictionary<Type, Type> typesChecked, ref Dictionary<XmlQualifiedName, DataContract> knownDataContracts)
		{
			if (TD.ImportKnownTypesStartIsEnabled())
			{
				TD.ImportKnownTypesStart();
			}
			while (type != null && DataContract.IsTypeSerializable(type))
			{
				if (typesChecked.ContainsKey(type))
				{
					return;
				}
				typesChecked.Add(type, type);
				object[] customAttributes = type.GetCustomAttributes(Globals.TypeOfKnownTypeAttribute, false);
				if (customAttributes != null)
				{
					bool flag = false;
					bool flag2 = false;
					foreach (KnownTypeAttribute knownTypeAttribute in customAttributes)
					{
						if (knownTypeAttribute.Type != null)
						{
							if (flag)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("Type '{0}': If a KnownTypeAttribute attribute specifies a method it must be the only KnownTypeAttribute attribute on that type.", new object[]
								{
									DataContract.GetClrTypeFullName(type)
								}), type);
							}
							DataContract.CheckAndAdd(knownTypeAttribute.Type, typesChecked, ref knownDataContracts);
							flag2 = true;
						}
						else
						{
							if (flag || flag2)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("Type '{0}': If a KnownTypeAttribute attribute specifies a method it must be the only KnownTypeAttribute attribute on that type.", new object[]
								{
									DataContract.GetClrTypeFullName(type)
								}), type);
							}
							string methodName = knownTypeAttribute.MethodName;
							if (methodName == null)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("KnownTypeAttribute attribute on type '{0}' contains no data.", new object[]
								{
									DataContract.GetClrTypeFullName(type)
								}), type);
							}
							if (methodName.Length == 0)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("Method name specified by KnownTypeAttribute attribute on type '{0}' cannot be the empty string.", new object[]
								{
									DataContract.GetClrTypeFullName(type)
								}), type);
							}
							MethodInfo method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
							if (method == null)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("KnownTypeAttribute attribute on type '{1}' specifies a method named '{0}' to provide known types. Static method '{0}()' was not found on this type. Ensure that the method exists and is marked as static.", new object[]
								{
									methodName,
									DataContract.GetClrTypeFullName(type)
								}), type);
							}
							if (!Globals.TypeOfTypeEnumerable.IsAssignableFrom(method.ReturnType))
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("KnownTypeAttribute attribute on type '{0}' specifies a method named '{1}' to provide known types. The return type of this method is invalid because it is not assignable to IEnumerable<Type>. Ensure that the method exists and has a valid signature.", new object[]
								{
									DataContract.GetClrTypeFullName(type),
									methodName
								}), type);
							}
							object obj = method.Invoke(null, Globals.EmptyObjectArray);
							if (obj == null)
							{
								DataContract.ThrowInvalidDataContractException(SR.GetString("Method specified by KnownTypeAttribute attribute on type '{0}' returned null.", new object[]
								{
									DataContract.GetClrTypeFullName(type)
								}), type);
							}
							foreach (Type type2 in ((IEnumerable<Type>)obj))
							{
								if (type2 == null)
								{
									DataContract.ThrowInvalidDataContractException(SR.GetString("Method specified by KnownTypeAttribute attribute on type '{0}' does not expose valid types.", new object[]
									{
										DataContract.GetClrTypeFullName(type)
									}), type);
								}
								DataContract.CheckAndAdd(type2, typesChecked, ref knownDataContracts);
							}
							flag = true;
						}
					}
				}
				DataContract.LoadKnownTypesFromConfig(type, typesChecked, ref knownDataContracts);
				type = type.BaseType;
			}
			if (TD.ImportKnownTypesStopIsEnabled())
			{
				TD.ImportKnownTypesStop();
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002E49C File Offset: 0x0002C69C
		[SecuritySafeCritical]
		private static void LoadKnownTypesFromConfig(Type type, Dictionary<Type, Type> typesChecked, ref Dictionary<XmlQualifiedName, DataContract> knownDataContracts)
		{
			if (DataContract.ConfigSection != null)
			{
				DeclaredTypeElementCollection declaredTypes = DataContract.ConfigSection.DeclaredTypes;
				Type type2 = type;
				Type[] typeArgs = null;
				DataContract.CheckRootTypeInConfigIsGeneric(type, ref type2, ref typeArgs);
				DeclaredTypeElement declaredTypeElement = declaredTypes[type2.AssemblyQualifiedName];
				if (declaredTypeElement != null && DataContract.IsElemTypeNullOrNotEqualToRootType(declaredTypeElement.Type, type2))
				{
					declaredTypeElement = null;
				}
				if (declaredTypeElement == null)
				{
					for (int i = 0; i < declaredTypes.Count; i++)
					{
						if (DataContract.IsCollectionElementTypeEqualToRootType(declaredTypes[i].Type, type2))
						{
							declaredTypeElement = declaredTypes[i];
							break;
						}
					}
				}
				if (declaredTypeElement != null)
				{
					for (int j = 0; j < declaredTypeElement.KnownTypes.Count; j++)
					{
						Type type3 = declaredTypeElement.KnownTypes[j].GetType(declaredTypeElement.Type, typeArgs);
						if (type3 != null)
						{
							DataContract.CheckAndAdd(type3, typesChecked, ref knownDataContracts);
						}
					}
				}
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002E570 File Offset: 0x0002C770
		private static void CheckRootTypeInConfigIsGeneric(Type type, ref Type rootType, ref Type[] genArgs)
		{
			if (rootType.IsGenericType)
			{
				if (!rootType.ContainsGenericParameters)
				{
					genArgs = rootType.GetGenericArguments();
					rootType = rootType.GetGenericTypeDefinition();
					return;
				}
				DataContract.ThrowInvalidDataContractException(SR.GetString("Error while getting known types for Type '{0}'. The type must not be an open or partial generic class.", new object[]
				{
					type
				}), type);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		private static bool IsElemTypeNullOrNotEqualToRootType(string elemTypeName, Type rootType)
		{
			Type type = Type.GetType(elemTypeName, false);
			return type == null || !rootType.Equals(type);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002E5E8 File Offset: 0x0002C7E8
		private static bool IsCollectionElementTypeEqualToRootType(string collectionElementTypeName, Type rootType)
		{
			if (collectionElementTypeName.StartsWith(DataContract.GetClrTypeFullName(rootType), StringComparison.Ordinal))
			{
				Type type = Type.GetType(collectionElementTypeName, false);
				if (type != null)
				{
					if (type.IsGenericType && !DataContract.IsOpenGenericType(type))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("Declared type '{0}' in config cannot be a closed or partial generic type.", new object[]
						{
							collectionElementTypeName
						})));
					}
					if (rootType.Equals(type))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002E650 File Offset: 0x0002C850
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void CheckAndAdd(Type type, Dictionary<Type, Type> typesChecked, ref Dictionary<XmlQualifiedName, DataContract> nameToDataContractTable)
		{
			type = DataContract.UnwrapNullableType(type);
			DataContract dataContract = DataContract.GetDataContract(type);
			DataContract dataContract2;
			if (nameToDataContractTable == null)
			{
				nameToDataContractTable = new Dictionary<XmlQualifiedName, DataContract>();
			}
			else if (nameToDataContractTable.TryGetValue(dataContract.StableName, out dataContract2))
			{
				if (dataContract2.UnderlyingType != DataContract.DataContractCriticalHelper.GetDataContractAdapterType(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Type '{0}' cannot be added to list of known types since another type '{1}' with the same data contract name '{2}:{3}' is already present.", new object[]
					{
						type,
						dataContract2.UnderlyingType,
						dataContract.StableName.Namespace,
						dataContract.StableName.Name
					})));
				}
				return;
			}
			nameToDataContractTable.Add(dataContract.StableName, dataContract);
			DataContract.ImportKnownTypeAttributes(type, typesChecked, ref nameToDataContractTable);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002E6F8 File Offset: 0x0002C8F8
		private static bool IsOpenGenericType(Type t)
		{
			Type[] genericArguments = t.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (!genericArguments[i].IsGenericParameter)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002E727 File Offset: 0x0002C927
		public sealed override bool Equals(object other)
		{
			return this == other || this.Equals(other, new Dictionary<DataContractPairKey, object>());
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002E73C File Offset: 0x0002C93C
		internal virtual bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			DataContract dataContract = other as DataContract;
			return dataContract != null && (this.StableName.Name == dataContract.StableName.Name && this.StableName.Namespace == dataContract.StableName.Namespace) && this.IsReference == dataContract.IsReference;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
		internal bool IsEqualOrChecked(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (this == other)
			{
				return true;
			}
			if (checkedContracts != null)
			{
				DataContractPairKey key = new DataContractPairKey(this, other);
				if (checkedContracts.ContainsKey(key))
				{
					return true;
				}
				checkedContracts.Add(key, null);
			}
			return false;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002E7D2 File Offset: 0x0002C9D2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002E7DA File Offset: 0x0002C9DA
		internal void ThrowInvalidDataContractException(string message)
		{
			DataContract.ThrowInvalidDataContractException(message, this.UnderlyingType);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000066E8 File Offset: 0x000048E8
		internal static bool IsTypeVisible(Type t)
		{
			return true;
		}

		// Token: 0x04000466 RID: 1126
		[SecurityCritical]
		private XmlDictionaryString name;

		// Token: 0x04000467 RID: 1127
		[SecurityCritical]
		private XmlDictionaryString ns;

		// Token: 0x04000468 RID: 1128
		[SecurityCritical]
		private DataContract.DataContractCriticalHelper helper;

		// Token: 0x04000469 RID: 1129
		[SecurityCritical]
		private static DataContractSerializerSection configSection;

		// Token: 0x020000BC RID: 188
		[SecurityCritical(SecurityCriticalScope.Everything)]
		protected class DataContractCriticalHelper
		{
			// Token: 0x06000ADA RID: 2778 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
			static DataContractCriticalHelper()
			{
				DataContract.DataContractCriticalHelper.typeToIDCache = new Dictionary<TypeHandleRef, IntRef>(new TypeHandleRefEqualityComparer());
				DataContract.DataContractCriticalHelper.dataContractCache = new DataContract[32];
				DataContract.DataContractCriticalHelper.dataContractID = 0;
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x0002E854 File Offset: 0x0002CA54
			internal static DataContract GetDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
			{
				DataContract dataContract = DataContract.DataContractCriticalHelper.dataContractCache[id];
				if (dataContract == null)
				{
					return DataContract.DataContractCriticalHelper.CreateDataContract(id, typeHandle, type);
				}
				return dataContract.GetValidContract();
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0002E880 File Offset: 0x0002CA80
			internal static DataContract GetGetOnlyCollectionDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
			{
				DataContract dataContract = DataContract.DataContractCriticalHelper.dataContractCache[id];
				if (dataContract == null)
				{
					dataContract = DataContract.DataContractCriticalHelper.CreateGetOnlyCollectionDataContract(id, typeHandle, type);
					DataContract.DataContractCriticalHelper.AssignDataContractToId(dataContract, id);
				}
				return dataContract;
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x0002E8A9 File Offset: 0x0002CAA9
			internal static DataContract GetDataContractForInitialization(int id)
			{
				DataContract dataContract = DataContract.DataContractCriticalHelper.dataContractCache[id];
				if (dataContract == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("An internal error has occurred. DataContract cache overflow.")));
				}
				return dataContract;
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x0002E8CC File Offset: 0x0002CACC
			internal static int GetIdForInitialization(ClassDataContract classContract)
			{
				int id = DataContract.GetId(classContract.TypeForInitialization.TypeHandle);
				if (id < DataContract.DataContractCriticalHelper.dataContractCache.Length && DataContract.DataContractCriticalHelper.ContractMatches(classContract, DataContract.DataContractCriticalHelper.dataContractCache[id]))
				{
					return id;
				}
				int num = DataContract.DataContractCriticalHelper.dataContractID;
				for (int i = 0; i < num; i++)
				{
					if (DataContract.DataContractCriticalHelper.ContractMatches(classContract, DataContract.DataContractCriticalHelper.dataContractCache[i]))
					{
						return i;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("An internal error has occurred. DataContract cache overflow.")));
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x0002E93C File Offset: 0x0002CB3C
			private static bool ContractMatches(DataContract contract, DataContract cachedContract)
			{
				return cachedContract != null && cachedContract.UnderlyingType == contract.UnderlyingType;
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x0002E954 File Offset: 0x0002CB54
			internal static int GetId(RuntimeTypeHandle typeHandle)
			{
				object obj = DataContract.DataContractCriticalHelper.cacheLock;
				int value;
				lock (obj)
				{
					typeHandle = DataContract.DataContractCriticalHelper.GetDataContractAdapterTypeHandle(typeHandle);
					DataContract.DataContractCriticalHelper.typeHandleRef.Value = typeHandle;
					IntRef nextId;
					if (!DataContract.DataContractCriticalHelper.typeToIDCache.TryGetValue(DataContract.DataContractCriticalHelper.typeHandleRef, out nextId))
					{
						nextId = DataContract.DataContractCriticalHelper.GetNextId();
						try
						{
							DataContract.DataContractCriticalHelper.typeToIDCache.Add(new TypeHandleRef(typeHandle), nextId);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
						}
					}
					value = nextId.Value;
				}
				return value;
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x0002E9FC File Offset: 0x0002CBFC
			private static IntRef GetNextId()
			{
				int num = DataContract.DataContractCriticalHelper.dataContractID++;
				if (num >= DataContract.DataContractCriticalHelper.dataContractCache.Length)
				{
					int num2 = (num < 1073741823) ? (num * 2) : int.MaxValue;
					if (num2 <= num)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("An internal error has occurred. DataContract cache overflow.")));
					}
					Array.Resize<DataContract>(ref DataContract.DataContractCriticalHelper.dataContractCache, num2);
				}
				return new IntRef(num);
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x0002EA60 File Offset: 0x0002CC60
			private static DataContract CreateDataContract(int id, RuntimeTypeHandle typeHandle, Type type)
			{
				DataContract dataContract = DataContract.DataContractCriticalHelper.dataContractCache[id];
				if (dataContract == null)
				{
					object obj = DataContract.DataContractCriticalHelper.createDataContractLock;
					lock (obj)
					{
						dataContract = DataContract.DataContractCriticalHelper.dataContractCache[id];
						if (dataContract == null)
						{
							if (type == null)
							{
								type = Type.GetTypeFromHandle(typeHandle);
							}
							type = DataContract.UnwrapNullableType(type);
							type = DataContract.DataContractCriticalHelper.GetDataContractAdapterType(type);
							dataContract = DataContract.DataContractCriticalHelper.GetBuiltInDataContract(type);
							if (dataContract == null)
							{
								if (type.IsArray)
								{
									dataContract = new CollectionDataContract(type);
								}
								else if (type.IsEnum)
								{
									dataContract = new EnumDataContract(type);
								}
								else if (type.IsGenericParameter)
								{
									dataContract = new GenericParameterDataContract(type);
								}
								else if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
								{
									dataContract = new XmlDataContract(type);
								}
								else
								{
									if (type.IsPointer)
									{
										type = Globals.TypeOfReflectionPointer;
									}
									if (!CollectionDataContract.TryCreate(type, out dataContract))
									{
										if (type.IsSerializable || type.IsDefined(Globals.TypeOfDataContractAttribute, false) || ClassDataContract.IsNonAttributedTypeValidForSerialization(type))
										{
											dataContract = new ClassDataContract(type);
										}
										else
										{
											DataContract.DataContractCriticalHelper.ThrowInvalidDataContractException(SR.GetString("Type '{0}' cannot be serialized. Consider marking it with the DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute attribute. Alternatively, you can ensure that the type is public and has a parameterless constructor - all public members of the type will then be serialized, and no attributes will be required.", new object[]
											{
												type
											}), type);
										}
									}
								}
							}
							DataContract.DataContractCriticalHelper.AssignDataContractToId(dataContract, id);
						}
					}
				}
				return dataContract;
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x0002EB94 File Offset: 0x0002CD94
			[MethodImpl(MethodImplOptions.NoInlining)]
			private static void AssignDataContractToId(DataContract dataContract, int id)
			{
				object obj = DataContract.DataContractCriticalHelper.cacheLock;
				lock (obj)
				{
					DataContract.DataContractCriticalHelper.dataContractCache[id] = dataContract;
				}
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
			private static DataContract CreateGetOnlyCollectionDataContract(int id, RuntimeTypeHandle typeHandle, Type type)
			{
				DataContract dataContract = null;
				object obj = DataContract.DataContractCriticalHelper.createDataContractLock;
				lock (obj)
				{
					dataContract = DataContract.DataContractCriticalHelper.dataContractCache[id];
					if (dataContract == null)
					{
						if (type == null)
						{
							type = Type.GetTypeFromHandle(typeHandle);
						}
						type = DataContract.UnwrapNullableType(type);
						type = DataContract.DataContractCriticalHelper.GetDataContractAdapterType(type);
						if (!CollectionDataContract.TryCreateGetOnlyCollectionDataContract(type, out dataContract))
						{
							DataContract.DataContractCriticalHelper.ThrowInvalidDataContractException(SR.GetString("Type '{0}' cannot be serialized. Consider marking it with the DataContractAttribute attribute, and marking all of its members you want serialized with the DataMemberAttribute attribute. Alternatively, you can ensure that the type is public and has a parameterless constructor - all public members of the type will then be serialized, and no attributes will be required.", new object[]
							{
								type
							}), type);
						}
					}
				}
				return dataContract;
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x0002EC64 File Offset: 0x0002CE64
			internal static Type GetDataContractAdapterType(Type type)
			{
				if (type == Globals.TypeOfDateTimeOffset)
				{
					return Globals.TypeOfDateTimeOffsetAdapter;
				}
				return type;
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x0002EC7A File Offset: 0x0002CE7A
			internal static Type GetDataContractOriginalType(Type type)
			{
				if (type == Globals.TypeOfDateTimeOffsetAdapter)
				{
					return Globals.TypeOfDateTimeOffset;
				}
				return type;
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x0002EC90 File Offset: 0x0002CE90
			private static RuntimeTypeHandle GetDataContractAdapterTypeHandle(RuntimeTypeHandle typeHandle)
			{
				if (Globals.TypeOfDateTimeOffset.TypeHandle.Equals(typeHandle))
				{
					return Globals.TypeOfDateTimeOffsetAdapter.TypeHandle;
				}
				return typeHandle;
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x0002ECC0 File Offset: 0x0002CEC0
			public static DataContract GetBuiltInDataContract(Type type)
			{
				if (type.IsInterface && !CollectionDataContract.IsCollectionInterface(type))
				{
					type = Globals.TypeOfObject;
				}
				object obj = DataContract.DataContractCriticalHelper.initBuiltInContractsLock;
				DataContract result;
				lock (obj)
				{
					if (DataContract.DataContractCriticalHelper.typeToBuiltInContract == null)
					{
						DataContract.DataContractCriticalHelper.typeToBuiltInContract = new Dictionary<Type, DataContract>();
					}
					DataContract dataContract = null;
					if (!DataContract.DataContractCriticalHelper.typeToBuiltInContract.TryGetValue(type, out dataContract))
					{
						DataContract.DataContractCriticalHelper.TryCreateBuiltInDataContract(type, out dataContract);
						DataContract.DataContractCriticalHelper.typeToBuiltInContract.Add(type, dataContract);
					}
					result = dataContract;
				}
				return result;
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x0002ED4C File Offset: 0x0002CF4C
			public static DataContract GetBuiltInDataContract(string name, string ns)
			{
				object obj = DataContract.DataContractCriticalHelper.initBuiltInContractsLock;
				DataContract result;
				lock (obj)
				{
					if (DataContract.DataContractCriticalHelper.nameToBuiltInContract == null)
					{
						DataContract.DataContractCriticalHelper.nameToBuiltInContract = new Dictionary<XmlQualifiedName, DataContract>();
					}
					DataContract dataContract = null;
					XmlQualifiedName key = new XmlQualifiedName(name, ns);
					if (!DataContract.DataContractCriticalHelper.nameToBuiltInContract.TryGetValue(key, out dataContract) && DataContract.DataContractCriticalHelper.TryCreateBuiltInDataContract(name, ns, out dataContract))
					{
						DataContract.DataContractCriticalHelper.nameToBuiltInContract.Add(key, dataContract);
					}
					result = dataContract;
				}
				return result;
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x0002EDCC File Offset: 0x0002CFCC
			public static DataContract GetBuiltInDataContract(string typeName)
			{
				if (!typeName.StartsWith("System.", StringComparison.Ordinal))
				{
					return null;
				}
				object obj = DataContract.DataContractCriticalHelper.initBuiltInContractsLock;
				DataContract result;
				lock (obj)
				{
					if (DataContract.DataContractCriticalHelper.typeNameToBuiltInContract == null)
					{
						DataContract.DataContractCriticalHelper.typeNameToBuiltInContract = new Dictionary<string, DataContract>();
					}
					DataContract dataContract = null;
					if (!DataContract.DataContractCriticalHelper.typeNameToBuiltInContract.TryGetValue(typeName, out dataContract))
					{
						Type type = null;
						string a = typeName.Substring(7);
						if (a == "Char")
						{
							type = typeof(char);
						}
						else if (a == "Boolean")
						{
							type = typeof(bool);
						}
						else if (a == "SByte")
						{
							type = typeof(sbyte);
						}
						else if (a == "Byte")
						{
							type = typeof(byte);
						}
						else if (a == "Int16")
						{
							type = typeof(short);
						}
						else if (a == "UInt16")
						{
							type = typeof(ushort);
						}
						else if (a == "Int32")
						{
							type = typeof(int);
						}
						else if (a == "UInt32")
						{
							type = typeof(uint);
						}
						else if (a == "Int64")
						{
							type = typeof(long);
						}
						else if (a == "UInt64")
						{
							type = typeof(ulong);
						}
						else if (a == "Single")
						{
							type = typeof(float);
						}
						else if (a == "Double")
						{
							type = typeof(double);
						}
						else if (a == "Decimal")
						{
							type = typeof(decimal);
						}
						else if (a == "DateTime")
						{
							type = typeof(DateTime);
						}
						else if (a == "String")
						{
							type = typeof(string);
						}
						else if (a == "Byte[]")
						{
							type = typeof(byte[]);
						}
						else if (a == "Object")
						{
							type = typeof(object);
						}
						else if (a == "TimeSpan")
						{
							type = typeof(TimeSpan);
						}
						else if (a == "Guid")
						{
							type = typeof(Guid);
						}
						else if (a == "Uri")
						{
							type = typeof(Uri);
						}
						else if (a == "Xml.XmlQualifiedName")
						{
							type = typeof(XmlQualifiedName);
						}
						else if (a == "Enum")
						{
							type = typeof(Enum);
						}
						else if (a == "ValueType")
						{
							type = typeof(ValueType);
						}
						else if (a == "Array")
						{
							type = typeof(Array);
						}
						else if (a == "Xml.XmlElement")
						{
							type = typeof(XmlElement);
						}
						else if (a == "Xml.XmlNode[]")
						{
							type = typeof(XmlNode[]);
						}
						if (type != null)
						{
							DataContract.DataContractCriticalHelper.TryCreateBuiltInDataContract(type, out dataContract);
						}
						DataContract.DataContractCriticalHelper.typeNameToBuiltInContract.Add(typeName, dataContract);
					}
					result = dataContract;
				}
				return result;
			}

			// Token: 0x06000AEB RID: 2795 RVA: 0x0002F170 File Offset: 0x0002D370
			public static bool TryCreateBuiltInDataContract(Type type, out DataContract dataContract)
			{
				if (type.IsEnum)
				{
					dataContract = null;
					return false;
				}
				dataContract = null;
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					dataContract = new BooleanDataContract();
					goto IL_24C;
				case TypeCode.Char:
					dataContract = new CharDataContract();
					goto IL_24C;
				case TypeCode.SByte:
					dataContract = new SignedByteDataContract();
					goto IL_24C;
				case TypeCode.Byte:
					dataContract = new UnsignedByteDataContract();
					goto IL_24C;
				case TypeCode.Int16:
					dataContract = new ShortDataContract();
					goto IL_24C;
				case TypeCode.UInt16:
					dataContract = new UnsignedShortDataContract();
					goto IL_24C;
				case TypeCode.Int32:
					dataContract = new IntDataContract();
					goto IL_24C;
				case TypeCode.UInt32:
					dataContract = new UnsignedIntDataContract();
					goto IL_24C;
				case TypeCode.Int64:
					dataContract = new LongDataContract();
					goto IL_24C;
				case TypeCode.UInt64:
					dataContract = new UnsignedLongDataContract();
					goto IL_24C;
				case TypeCode.Single:
					dataContract = new FloatDataContract();
					goto IL_24C;
				case TypeCode.Double:
					dataContract = new DoubleDataContract();
					goto IL_24C;
				case TypeCode.Decimal:
					dataContract = new DecimalDataContract();
					goto IL_24C;
				case TypeCode.DateTime:
					dataContract = new DateTimeDataContract();
					goto IL_24C;
				case TypeCode.String:
					dataContract = new StringDataContract();
					goto IL_24C;
				}
				if (type == typeof(byte[]))
				{
					dataContract = new ByteArrayDataContract();
				}
				else if (type == typeof(object))
				{
					dataContract = new ObjectDataContract();
				}
				else if (type == typeof(Uri))
				{
					dataContract = new UriDataContract();
				}
				else if (type == typeof(XmlQualifiedName))
				{
					dataContract = new QNameDataContract();
				}
				else if (type == typeof(TimeSpan))
				{
					dataContract = new TimeSpanDataContract();
				}
				else if (type == typeof(Guid))
				{
					dataContract = new GuidDataContract();
				}
				else if (type == typeof(Enum) || type == typeof(ValueType))
				{
					dataContract = new SpecialTypeDataContract(type, DictionaryGlobals.ObjectLocalName, DictionaryGlobals.SchemaNamespace);
				}
				else if (type == typeof(Array))
				{
					dataContract = new CollectionDataContract(type);
				}
				else if (type == typeof(XmlElement) || type == typeof(XmlNode[]))
				{
					dataContract = new XmlDataContract(type);
				}
				IL_24C:
				return dataContract != null;
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x0002F3D0 File Offset: 0x0002D5D0
			public static bool TryCreateBuiltInDataContract(string name, string ns, out DataContract dataContract)
			{
				dataContract = null;
				if (ns == DictionaryGlobals.SchemaNamespace.Value)
				{
					if (DictionaryGlobals.BooleanLocalName.Value == name)
					{
						dataContract = new BooleanDataContract();
					}
					else if (DictionaryGlobals.SignedByteLocalName.Value == name)
					{
						dataContract = new SignedByteDataContract();
					}
					else if (DictionaryGlobals.UnsignedByteLocalName.Value == name)
					{
						dataContract = new UnsignedByteDataContract();
					}
					else if (DictionaryGlobals.ShortLocalName.Value == name)
					{
						dataContract = new ShortDataContract();
					}
					else if (DictionaryGlobals.UnsignedShortLocalName.Value == name)
					{
						dataContract = new UnsignedShortDataContract();
					}
					else if (DictionaryGlobals.IntLocalName.Value == name)
					{
						dataContract = new IntDataContract();
					}
					else if (DictionaryGlobals.UnsignedIntLocalName.Value == name)
					{
						dataContract = new UnsignedIntDataContract();
					}
					else if (DictionaryGlobals.LongLocalName.Value == name)
					{
						dataContract = new LongDataContract();
					}
					else if (DictionaryGlobals.integerLocalName.Value == name)
					{
						dataContract = new IntegerDataContract();
					}
					else if (DictionaryGlobals.positiveIntegerLocalName.Value == name)
					{
						dataContract = new PositiveIntegerDataContract();
					}
					else if (DictionaryGlobals.negativeIntegerLocalName.Value == name)
					{
						dataContract = new NegativeIntegerDataContract();
					}
					else if (DictionaryGlobals.nonPositiveIntegerLocalName.Value == name)
					{
						dataContract = new NonPositiveIntegerDataContract();
					}
					else if (DictionaryGlobals.nonNegativeIntegerLocalName.Value == name)
					{
						dataContract = new NonNegativeIntegerDataContract();
					}
					else if (DictionaryGlobals.UnsignedLongLocalName.Value == name)
					{
						dataContract = new UnsignedLongDataContract();
					}
					else if (DictionaryGlobals.FloatLocalName.Value == name)
					{
						dataContract = new FloatDataContract();
					}
					else if (DictionaryGlobals.DoubleLocalName.Value == name)
					{
						dataContract = new DoubleDataContract();
					}
					else if (DictionaryGlobals.DecimalLocalName.Value == name)
					{
						dataContract = new DecimalDataContract();
					}
					else if (DictionaryGlobals.DateTimeLocalName.Value == name)
					{
						dataContract = new DateTimeDataContract();
					}
					else if (DictionaryGlobals.StringLocalName.Value == name)
					{
						dataContract = new StringDataContract();
					}
					else if (DictionaryGlobals.timeLocalName.Value == name)
					{
						dataContract = new TimeDataContract();
					}
					else if (DictionaryGlobals.dateLocalName.Value == name)
					{
						dataContract = new DateDataContract();
					}
					else if (DictionaryGlobals.hexBinaryLocalName.Value == name)
					{
						dataContract = new HexBinaryDataContract();
					}
					else if (DictionaryGlobals.gYearMonthLocalName.Value == name)
					{
						dataContract = new GYearMonthDataContract();
					}
					else if (DictionaryGlobals.gYearLocalName.Value == name)
					{
						dataContract = new GYearDataContract();
					}
					else if (DictionaryGlobals.gMonthDayLocalName.Value == name)
					{
						dataContract = new GMonthDayDataContract();
					}
					else if (DictionaryGlobals.gDayLocalName.Value == name)
					{
						dataContract = new GDayDataContract();
					}
					else if (DictionaryGlobals.gMonthLocalName.Value == name)
					{
						dataContract = new GMonthDataContract();
					}
					else if (DictionaryGlobals.normalizedStringLocalName.Value == name)
					{
						dataContract = new NormalizedStringDataContract();
					}
					else if (DictionaryGlobals.tokenLocalName.Value == name)
					{
						dataContract = new TokenDataContract();
					}
					else if (DictionaryGlobals.languageLocalName.Value == name)
					{
						dataContract = new LanguageDataContract();
					}
					else if (DictionaryGlobals.NameLocalName.Value == name)
					{
						dataContract = new NameDataContract();
					}
					else if (DictionaryGlobals.NCNameLocalName.Value == name)
					{
						dataContract = new NCNameDataContract();
					}
					else if (DictionaryGlobals.XSDIDLocalName.Value == name)
					{
						dataContract = new IDDataContract();
					}
					else if (DictionaryGlobals.IDREFLocalName.Value == name)
					{
						dataContract = new IDREFDataContract();
					}
					else if (DictionaryGlobals.IDREFSLocalName.Value == name)
					{
						dataContract = new IDREFSDataContract();
					}
					else if (DictionaryGlobals.ENTITYLocalName.Value == name)
					{
						dataContract = new ENTITYDataContract();
					}
					else if (DictionaryGlobals.ENTITIESLocalName.Value == name)
					{
						dataContract = new ENTITIESDataContract();
					}
					else if (DictionaryGlobals.NMTOKENLocalName.Value == name)
					{
						dataContract = new NMTOKENDataContract();
					}
					else if (DictionaryGlobals.NMTOKENSLocalName.Value == name)
					{
						dataContract = new NMTOKENDataContract();
					}
					else if (DictionaryGlobals.ByteArrayLocalName.Value == name)
					{
						dataContract = new ByteArrayDataContract();
					}
					else if (DictionaryGlobals.ObjectLocalName.Value == name)
					{
						dataContract = new ObjectDataContract();
					}
					else if (DictionaryGlobals.TimeSpanLocalName.Value == name)
					{
						dataContract = new XsDurationDataContract();
					}
					else if (DictionaryGlobals.UriLocalName.Value == name)
					{
						dataContract = new UriDataContract();
					}
					else if (DictionaryGlobals.QNameLocalName.Value == name)
					{
						dataContract = new QNameDataContract();
					}
				}
				else if (ns == DictionaryGlobals.SerializationNamespace.Value)
				{
					if (DictionaryGlobals.TimeSpanLocalName.Value == name)
					{
						dataContract = new TimeSpanDataContract();
					}
					else if (DictionaryGlobals.GuidLocalName.Value == name)
					{
						dataContract = new GuidDataContract();
					}
					else if (DictionaryGlobals.CharLocalName.Value == name)
					{
						dataContract = new CharDataContract();
					}
					else if ("ArrayOfanyType" == name)
					{
						dataContract = new CollectionDataContract(typeof(Array));
					}
				}
				else if (ns == DictionaryGlobals.AsmxTypesNamespace.Value)
				{
					if (DictionaryGlobals.CharLocalName.Value == name)
					{
						dataContract = new AsmxCharDataContract();
					}
					else if (DictionaryGlobals.GuidLocalName.Value == name)
					{
						dataContract = new AsmxGuidDataContract();
					}
				}
				else if (ns == "http://schemas.datacontract.org/2004/07/System.Xml")
				{
					if (name == "XmlElement")
					{
						dataContract = new XmlDataContract(typeof(XmlElement));
					}
					else if (name == "ArrayOfXmlNode")
					{
						dataContract = new XmlDataContract(typeof(XmlNode[]));
					}
				}
				return dataContract != null;
			}

			// Token: 0x06000AED RID: 2797 RVA: 0x0002FA50 File Offset: 0x0002DC50
			internal static string GetNamespace(string key)
			{
				object obj = DataContract.DataContractCriticalHelper.namespacesLock;
				string result;
				lock (obj)
				{
					if (DataContract.DataContractCriticalHelper.namespaces == null)
					{
						DataContract.DataContractCriticalHelper.namespaces = new Dictionary<string, string>();
					}
					string text;
					if (DataContract.DataContractCriticalHelper.namespaces.TryGetValue(key, out text))
					{
						result = text;
					}
					else
					{
						try
						{
							DataContract.DataContractCriticalHelper.namespaces.Add(key, key);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
						}
						result = key;
					}
				}
				return result;
			}

			// Token: 0x06000AEE RID: 2798 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
			internal static XmlDictionaryString GetClrTypeString(string key)
			{
				object obj = DataContract.DataContractCriticalHelper.clrTypeStringsLock;
				XmlDictionaryString result;
				lock (obj)
				{
					if (DataContract.DataContractCriticalHelper.clrTypeStrings == null)
					{
						DataContract.DataContractCriticalHelper.clrTypeStringsDictionary = new XmlDictionary();
						DataContract.DataContractCriticalHelper.clrTypeStrings = new Dictionary<string, XmlDictionaryString>();
						try
						{
							DataContract.DataContractCriticalHelper.clrTypeStrings.Add(Globals.TypeOfInt.Assembly.FullName, DataContract.DataContractCriticalHelper.clrTypeStringsDictionary.Add("0"));
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
						}
					}
					XmlDictionaryString xmlDictionaryString;
					if (DataContract.DataContractCriticalHelper.clrTypeStrings.TryGetValue(key, out xmlDictionaryString))
					{
						result = xmlDictionaryString;
					}
					else
					{
						xmlDictionaryString = DataContract.DataContractCriticalHelper.clrTypeStringsDictionary.Add(key);
						try
						{
							DataContract.DataContractCriticalHelper.clrTypeStrings.Add(key, xmlDictionaryString);
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex2.Message, ex2);
						}
						result = xmlDictionaryString;
					}
				}
				return result;
			}

			// Token: 0x06000AEF RID: 2799 RVA: 0x0002FBE8 File Offset: 0x0002DDE8
			internal static void ThrowInvalidDataContractException(string message, Type type)
			{
				if (type != null)
				{
					object obj = DataContract.DataContractCriticalHelper.cacheLock;
					lock (obj)
					{
						DataContract.DataContractCriticalHelper.typeHandleRef.Value = DataContract.DataContractCriticalHelper.GetDataContractAdapterTypeHandle(type.TypeHandle);
						try
						{
							DataContract.DataContractCriticalHelper.typeToIDCache.Remove(DataContract.DataContractCriticalHelper.typeHandleRef);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
						}
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(message));
			}

			// Token: 0x06000AF0 RID: 2800 RVA: 0x0000222F File Offset: 0x0000042F
			internal DataContractCriticalHelper()
			{
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x0002FC80 File Offset: 0x0002DE80
			internal DataContractCriticalHelper(Type type)
			{
				this.underlyingType = type;
				this.SetTypeForInitialization(type);
				this.isValueType = type.IsValueType;
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002FCA2 File Offset: 0x0002DEA2
			internal Type UnderlyingType
			{
				get
				{
					return this.underlyingType;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002FCAA File Offset: 0x0002DEAA
			internal Type OriginalUnderlyingType
			{
				get
				{
					if (this.originalUnderlyingType == null)
					{
						this.originalUnderlyingType = DataContract.DataContractCriticalHelper.GetDataContractOriginalType(this.underlyingType);
					}
					return this.originalUnderlyingType;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00003127 File Offset: 0x00001327
			internal virtual bool IsBuiltInDataContract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002FCD1 File Offset: 0x0002DED1
			internal Type TypeForInitialization
			{
				get
				{
					return this.typeForInitialization;
				}
			}

			// Token: 0x06000AF6 RID: 2806 RVA: 0x0002FCD9 File Offset: 0x0002DED9
			[SecuritySafeCritical]
			private void SetTypeForInitialization(Type classType)
			{
				if (classType.IsSerializable || classType.IsDefined(Globals.TypeOfDataContractAttribute, false))
				{
					this.typeForInitialization = classType;
				}
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002FCF8 File Offset: 0x0002DEF8
			// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0002FD00 File Offset: 0x0002DF00
			internal bool IsReference
			{
				get
				{
					return this.isReference;
				}
				set
				{
					this.isReference = value;
				}
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002FD09 File Offset: 0x0002DF09
			// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0002FD11 File Offset: 0x0002DF11
			internal bool IsValueType
			{
				get
				{
					return this.isValueType;
				}
				set
				{
					this.isValueType = value;
				}
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002FD1A File Offset: 0x0002DF1A
			// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0002FD22 File Offset: 0x0002DF22
			internal XmlQualifiedName StableName
			{
				get
				{
					return this.stableName;
				}
				set
				{
					this.stableName = value;
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002FD2B File Offset: 0x0002DF2B
			// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0002FD33 File Offset: 0x0002DF33
			internal GenericInfo GenericInfo
			{
				get
				{
					return this.genericInfo;
				}
				set
				{
					this.genericInfo = value;
				}
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001BF43 File Offset: 0x0001A143
			// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0000A8EE File Offset: 0x00008AEE
			internal virtual Dictionary<XmlQualifiedName, DataContract> KnownDataContracts
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00003127 File Offset: 0x00001327
			// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0002FD3C File Offset: 0x0002DF3C
			internal virtual bool IsISerializable
			{
				get
				{
					return false;
				}
				set
				{
					this.ThrowInvalidDataContractException(SR.GetString("To set IsISerializable, class data cotnract is required."));
				}
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002FD4E File Offset: 0x0002DF4E
			// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0002FD56 File Offset: 0x0002DF56
			internal XmlDictionaryString Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002FD5F File Offset: 0x0002DF5F
			// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0002FD67 File Offset: 0x0002DF67
			public XmlDictionaryString Namespace
			{
				get
				{
					return this.ns;
				}
				set
				{
					this.ns = value;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06000B07 RID: 2823 RVA: 0x000066E8 File Offset: 0x000048E8
			// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0000A8EE File Offset: 0x00008AEE
			internal virtual bool HasRoot
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002FD4E File Offset: 0x0002DF4E
			// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0002FD56 File Offset: 0x0002DF56
			internal virtual XmlDictionaryString TopLevelElementName
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002FD5F File Offset: 0x0002DF5F
			// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0002FD67 File Offset: 0x0002DF67
			internal virtual XmlDictionaryString TopLevelElementNamespace
			{
				get
				{
					return this.ns;
				}
				set
				{
					this.ns = value;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000066E8 File Offset: 0x000048E8
			internal virtual bool CanContainReferences
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00003127 File Offset: 0x00001327
			internal virtual bool IsPrimitive
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002FD70 File Offset: 0x0002DF70
			internal MethodInfo ParseMethod
			{
				get
				{
					if (!this.parseMethodSet)
					{
						MethodInfo method = this.UnderlyingType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							Globals.TypeOfString
						}, null);
						if (method != null && method.ReturnType == this.UnderlyingType)
						{
							this.parseMethod = method;
						}
						this.parseMethodSet = true;
					}
					return this.parseMethod;
				}
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x0002FDD8 File Offset: 0x0002DFD8
			internal virtual void WriteRootElement(XmlWriterDelegator writer, XmlDictionaryString name, XmlDictionaryString ns)
			{
				if (ns == DictionaryGlobals.SerializationNamespace && !this.IsPrimitive)
				{
					writer.WriteStartElement("z", name, ns);
					return;
				}
				writer.WriteStartElement(name, ns);
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x0002FE00 File Offset: 0x0002E000
			internal void SetDataContractName(XmlQualifiedName stableName)
			{
				XmlDictionary xmlDictionary = new XmlDictionary(2);
				this.Name = xmlDictionary.Add(stableName.Name);
				this.Namespace = xmlDictionary.Add(stableName.Namespace);
				this.StableName = stableName;
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x0002FE3F File Offset: 0x0002E03F
			internal void SetDataContractName(XmlDictionaryString name, XmlDictionaryString ns)
			{
				this.Name = name;
				this.Namespace = ns;
				this.StableName = DataContract.CreateQualifiedName(name.Value, ns.Value);
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x0002FE66 File Offset: 0x0002E066
			internal void ThrowInvalidDataContractException(string message)
			{
				DataContract.DataContractCriticalHelper.ThrowInvalidDataContractException(message, this.UnderlyingType);
			}

			// Token: 0x0400046A RID: 1130
			private static Dictionary<TypeHandleRef, IntRef> typeToIDCache;

			// Token: 0x0400046B RID: 1131
			private static DataContract[] dataContractCache;

			// Token: 0x0400046C RID: 1132
			private static int dataContractID;

			// Token: 0x0400046D RID: 1133
			private static Dictionary<Type, DataContract> typeToBuiltInContract;

			// Token: 0x0400046E RID: 1134
			private static Dictionary<XmlQualifiedName, DataContract> nameToBuiltInContract;

			// Token: 0x0400046F RID: 1135
			private static Dictionary<string, DataContract> typeNameToBuiltInContract;

			// Token: 0x04000470 RID: 1136
			private static Dictionary<string, string> namespaces;

			// Token: 0x04000471 RID: 1137
			private static Dictionary<string, XmlDictionaryString> clrTypeStrings;

			// Token: 0x04000472 RID: 1138
			private static XmlDictionary clrTypeStringsDictionary;

			// Token: 0x04000473 RID: 1139
			private static TypeHandleRef typeHandleRef = new TypeHandleRef();

			// Token: 0x04000474 RID: 1140
			private static object cacheLock = new object();

			// Token: 0x04000475 RID: 1141
			private static object createDataContractLock = new object();

			// Token: 0x04000476 RID: 1142
			private static object initBuiltInContractsLock = new object();

			// Token: 0x04000477 RID: 1143
			private static object namespacesLock = new object();

			// Token: 0x04000478 RID: 1144
			private static object clrTypeStringsLock = new object();

			// Token: 0x04000479 RID: 1145
			private readonly Type underlyingType;

			// Token: 0x0400047A RID: 1146
			private Type originalUnderlyingType;

			// Token: 0x0400047B RID: 1147
			private bool isReference;

			// Token: 0x0400047C RID: 1148
			private bool isValueType;

			// Token: 0x0400047D RID: 1149
			private XmlQualifiedName stableName;

			// Token: 0x0400047E RID: 1150
			private GenericInfo genericInfo;

			// Token: 0x0400047F RID: 1151
			private XmlDictionaryString name;

			// Token: 0x04000480 RID: 1152
			private XmlDictionaryString ns;

			// Token: 0x04000481 RID: 1153
			private Type typeForInitialization;

			// Token: 0x04000482 RID: 1154
			private MethodInfo parseMethod;

			// Token: 0x04000483 RID: 1155
			private bool parseMethodSet;
		}
	}
}
