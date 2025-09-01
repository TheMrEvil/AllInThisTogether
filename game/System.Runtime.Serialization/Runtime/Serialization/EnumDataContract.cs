using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000D0 RID: 208
	internal sealed class EnumDataContract : DataContract
	{
		// Token: 0x06000C03 RID: 3075 RVA: 0x000320F4 File Offset: 0x000302F4
		[SecuritySafeCritical]
		internal EnumDataContract() : base(new EnumDataContract.EnumDataContractCriticalHelper())
		{
			this.helper = (base.Helper as EnumDataContract.EnumDataContractCriticalHelper);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00032112 File Offset: 0x00030312
		[SecuritySafeCritical]
		internal EnumDataContract(Type type) : base(new EnumDataContract.EnumDataContractCriticalHelper(type))
		{
			this.helper = (base.Helper as EnumDataContract.EnumDataContractCriticalHelper);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00032131 File Offset: 0x00030331
		[SecuritySafeCritical]
		internal static XmlQualifiedName GetBaseContractName(Type type)
		{
			return EnumDataContract.EnumDataContractCriticalHelper.GetBaseContractName(type);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00032139 File Offset: 0x00030339
		[SecuritySafeCritical]
		internal static Type GetBaseType(XmlQualifiedName baseContractName)
		{
			return EnumDataContract.EnumDataContractCriticalHelper.GetBaseType(baseContractName);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00032141 File Offset: 0x00030341
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0003214E File Offset: 0x0003034E
		internal XmlQualifiedName BaseContractName
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.BaseContractName;
			}
			[SecurityCritical]
			set
			{
				this.helper.BaseContractName = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0003215C File Offset: 0x0003035C
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00032169 File Offset: 0x00030369
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00032177 File Offset: 0x00030377
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00032184 File Offset: 0x00030384
		internal List<long> Values
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.Values;
			}
			[SecurityCritical]
			set
			{
				this.helper.Values = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00032192 File Offset: 0x00030392
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0003219F File Offset: 0x0003039F
		internal bool IsFlags
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsFlags;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsFlags = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x000321AD File Offset: 0x000303AD
		internal bool IsULong
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsULong;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000321BA File Offset: 0x000303BA
		private XmlDictionaryString[] ChildElementNames
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ChildElementNames;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool CanContainReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000321C8 File Offset: 0x000303C8
		internal void WriteEnumValue(XmlWriterDelegator writer, object value)
		{
			long num = (long)(this.IsULong ? ((IConvertible)value).ToUInt64(null) : ((ulong)((IConvertible)value).ToInt64(null)));
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (num == this.Values[i])
				{
					writer.WriteString(this.ChildElementNames[i].Value);
					return;
				}
			}
			if (!this.IsFlags)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Enum value '{0}' is invalid for type '{1}' and cannot be serialized. Ensure that the necessary enum values are present and are marked with EnumMemberAttribute attribute if the type has DataContractAttribute attribute.", new object[]
				{
					value,
					DataContract.GetClrTypeFullName(base.UnderlyingType)
				})));
			}
			int num2 = -1;
			bool flag = true;
			for (int j = 0; j < this.Values.Count; j++)
			{
				long num3 = this.Values[j];
				if (num3 == 0L)
				{
					num2 = j;
				}
				else
				{
					if (num == 0L)
					{
						break;
					}
					if ((num3 & num) == num3)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							writer.WriteString(DictionaryGlobals.Space.Value);
						}
						writer.WriteString(this.ChildElementNames[j].Value);
						num &= ~num3;
					}
				}
			}
			if (num != 0L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Enum value '{0}' is invalid for type '{1}' and cannot be serialized. Ensure that the necessary enum values are present and are marked with EnumMemberAttribute attribute if the type has DataContractAttribute attribute.", new object[]
				{
					value,
					DataContract.GetClrTypeFullName(base.UnderlyingType)
				})));
			}
			if (flag && num2 >= 0)
			{
				writer.WriteString(this.ChildElementNames[num2].Value);
				return;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00032328 File Offset: 0x00030528
		internal object ReadEnumValue(XmlReaderDelegator reader)
		{
			string text = reader.ReadElementContentAsString();
			long num = 0L;
			int i = 0;
			if (this.IsFlags)
			{
				while (i < text.Length && text[i] == ' ')
				{
					i++;
				}
				int num2 = i;
				int num3;
				while (i < text.Length)
				{
					if (text[i] == ' ')
					{
						num3 = i - num2;
						if (num3 > 0)
						{
							num |= this.ReadEnumValue(text, num2, num3);
						}
						i++;
						while (i < text.Length && text[i] == ' ')
						{
							i++;
						}
						num2 = i;
						if (i == text.Length)
						{
							break;
						}
					}
					i++;
				}
				num3 = i - num2;
				if (num3 > 0)
				{
					num |= this.ReadEnumValue(text, num2, num3);
				}
			}
			else
			{
				if (text.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Invalid enum value '{0}' cannot be deserialized into type '{1}'. Ensure that the necessary enum values are present and are marked with EnumMemberAttribute attribute if the type has DataContractAttribute attribute.", new object[]
					{
						text,
						DataContract.GetClrTypeFullName(base.UnderlyingType)
					})));
				}
				num = this.ReadEnumValue(text, 0, text.Length);
			}
			if (this.IsULong)
			{
				return Enum.ToObject(base.UnderlyingType, (ulong)num);
			}
			return Enum.ToObject(base.UnderlyingType, num);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00032444 File Offset: 0x00030644
		private long ReadEnumValue(string value, int index, int count)
		{
			for (int i = 0; i < this.Members.Count; i++)
			{
				string name = this.Members[i].Name;
				if (name.Length == count && string.CompareOrdinal(value, index, name, 0, count) == 0)
				{
					return this.Values[i];
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Invalid enum value '{0}' cannot be deserialized into type '{1}'. Ensure that the necessary enum values are present and are marked with EnumMemberAttribute attribute if the type has DataContractAttribute attribute.", new object[]
			{
				value.Substring(index, count),
				DataContract.GetClrTypeFullName(base.UnderlyingType)
			})));
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000324CE File Offset: 0x000306CE
		internal string GetStringFromEnumValue(long value)
		{
			if (this.IsULong)
			{
				return XmlConvert.ToString((ulong)value);
			}
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000324E5 File Offset: 0x000306E5
		internal long GetEnumValueFromString(string value)
		{
			if (this.IsULong)
			{
				return (long)XmlConverter.ToUInt64(value);
			}
			return XmlConverter.ToInt64(value);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000324FC File Offset: 0x000306FC
		internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (base.IsEqualOrChecked(other, checkedContracts))
			{
				return true;
			}
			if (base.Equals(other, null))
			{
				EnumDataContract enumDataContract = other as EnumDataContract;
				if (enumDataContract != null)
				{
					if (this.Members.Count != enumDataContract.Members.Count || this.Values.Count != enumDataContract.Values.Count)
					{
						return false;
					}
					string[] array = new string[this.Members.Count];
					string[] array2 = new string[this.Members.Count];
					for (int i = 0; i < this.Members.Count; i++)
					{
						array[i] = this.Members[i].Name;
						array2[i] = enumDataContract.Members[i].Name;
					}
					Array.Sort<string>(array);
					Array.Sort<string>(array2);
					for (int j = 0; j < this.Members.Count; j++)
					{
						if (array[j] != array2[j])
						{
							return false;
						}
					}
					return this.IsFlags == enumDataContract.IsFlags;
				}
			}
			return false;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000262A8 File Offset: 0x000244A8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00032608 File Offset: 0x00030808
		public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			this.WriteEnumValue(xmlWriter, obj);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00032614 File Offset: 0x00030814
		public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
		{
			object obj = this.ReadEnumValue(xmlReader);
			if (context != null)
			{
				context.AddNewObject(obj);
			}
			return obj;
		}

		// Token: 0x0400050B RID: 1291
		[SecurityCritical]
		private EnumDataContract.EnumDataContractCriticalHelper helper;

		// Token: 0x020000D1 RID: 209
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class EnumDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x06000C1B RID: 3099 RVA: 0x00032634 File Offset: 0x00030834
			static EnumDataContractCriticalHelper()
			{
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(sbyte), "byte");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(byte), "unsignedByte");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(short), "short");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(ushort), "unsignedShort");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(int), "int");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(uint), "unsignedInt");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(long), "long");
				EnumDataContract.EnumDataContractCriticalHelper.Add(typeof(ulong), "unsignedLong");
			}

			// Token: 0x06000C1C RID: 3100 RVA: 0x000326F8 File Offset: 0x000308F8
			internal static void Add(Type type, string localName)
			{
				XmlQualifiedName xmlQualifiedName = DataContract.CreateQualifiedName(localName, "http://www.w3.org/2001/XMLSchema");
				EnumDataContract.EnumDataContractCriticalHelper.typeToName.Add(type, xmlQualifiedName);
				EnumDataContract.EnumDataContractCriticalHelper.nameToType.Add(xmlQualifiedName, type);
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x0003272C File Offset: 0x0003092C
			internal static XmlQualifiedName GetBaseContractName(Type type)
			{
				XmlQualifiedName result = null;
				EnumDataContract.EnumDataContractCriticalHelper.typeToName.TryGetValue(type, out result);
				return result;
			}

			// Token: 0x06000C1E RID: 3102 RVA: 0x0003274C File Offset: 0x0003094C
			internal static Type GetBaseType(XmlQualifiedName baseContractName)
			{
				Type result = null;
				EnumDataContract.EnumDataContractCriticalHelper.nameToType.TryGetValue(baseContractName, out result);
				return result;
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x0003276A File Offset: 0x0003096A
			internal EnumDataContractCriticalHelper()
			{
				base.IsValueType = true;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x0003277C File Offset: 0x0003097C
			internal EnumDataContractCriticalHelper(Type type) : base(type)
			{
				base.StableName = DataContract.GetStableName(type, out this.hasDataContract);
				Type underlyingType = Enum.GetUnderlyingType(type);
				this.baseContractName = EnumDataContract.EnumDataContractCriticalHelper.GetBaseContractName(underlyingType);
				this.ImportBaseType(underlyingType);
				this.IsFlags = type.IsDefined(Globals.TypeOfFlagsAttribute, false);
				this.ImportDataMembers();
				XmlDictionary xmlDictionary = new XmlDictionary(2 + this.Members.Count);
				base.Name = xmlDictionary.Add(base.StableName.Name);
				base.Namespace = xmlDictionary.Add(base.StableName.Namespace);
				this.childElementNames = new XmlDictionaryString[this.Members.Count];
				for (int i = 0; i < this.Members.Count; i++)
				{
					this.childElementNames[i] = xmlDictionary.Add(this.Members[i].Name);
				}
				DataContractAttribute dataContractAttribute;
				if (DataContract.TryGetDCAttribute(type, out dataContractAttribute) && dataContractAttribute.IsReference)
				{
					DataContract.ThrowInvalidDataContractException(SR.GetString("Enum type '{0}' cannot have the IsReference setting of '{1}'. Either change the setting to '{2}', or remove it completely.", new object[]
					{
						DataContract.GetClrTypeFullName(type),
						dataContractAttribute.IsReference,
						false
					}), type);
				}
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000C21 RID: 3105 RVA: 0x000328A8 File Offset: 0x00030AA8
			// (set) Token: 0x06000C22 RID: 3106 RVA: 0x000328B0 File Offset: 0x00030AB0
			internal XmlQualifiedName BaseContractName
			{
				get
				{
					return this.baseContractName;
				}
				set
				{
					this.baseContractName = value;
					Type baseType = EnumDataContract.EnumDataContractCriticalHelper.GetBaseType(this.baseContractName);
					if (baseType == null)
					{
						base.ThrowInvalidDataContractException(SR.GetString("Invalid enum base type is specified for type '{0}' in '{1}' namespace, element name is '{2}' in '{3}' namespace.", new object[]
						{
							value.Name,
							value.Namespace,
							base.StableName.Name,
							base.StableName.Namespace
						}));
					}
					this.ImportBaseType(baseType);
				}
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00032924 File Offset: 0x00030B24
			// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0003292C File Offset: 0x00030B2C
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

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00032935 File Offset: 0x00030B35
			// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0003293D File Offset: 0x00030B3D
			internal List<long> Values
			{
				get
				{
					return this.values;
				}
				set
				{
					this.values = value;
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00032946 File Offset: 0x00030B46
			// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0003294E File Offset: 0x00030B4E
			internal bool IsFlags
			{
				get
				{
					return this.isFlags;
				}
				set
				{
					this.isFlags = value;
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00032957 File Offset: 0x00030B57
			internal bool IsULong
			{
				get
				{
					return this.isULong;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0003295F File Offset: 0x00030B5F
			internal XmlDictionaryString[] ChildElementNames
			{
				get
				{
					return this.childElementNames;
				}
			}

			// Token: 0x06000C2B RID: 3115 RVA: 0x00032967 File Offset: 0x00030B67
			private void ImportBaseType(Type baseType)
			{
				this.isULong = (baseType == Globals.TypeOfULong);
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x0003297C File Offset: 0x00030B7C
			private void ImportDataMembers()
			{
				Type underlyingType = base.UnderlyingType;
				FieldInfo[] fields = underlyingType.GetFields(BindingFlags.Static | BindingFlags.Public);
				Dictionary<string, DataMember> memberNamesTable = new Dictionary<string, DataMember>();
				List<DataMember> list = new List<DataMember>(fields.Length);
				List<long> list2 = new List<long>(fields.Length);
				foreach (FieldInfo fieldInfo in fields)
				{
					bool flag = false;
					if (this.hasDataContract)
					{
						object[] customAttributes = fieldInfo.GetCustomAttributes(Globals.TypeOfEnumMemberAttribute, false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							if (customAttributes.Length > 1)
							{
								base.ThrowInvalidDataContractException(SR.GetString("Member '{0}.{1}' has more than one EnumMemberAttribute attribute.", new object[]
								{
									DataContract.GetClrTypeFullName(fieldInfo.DeclaringType),
									fieldInfo.Name
								}));
							}
							EnumMemberAttribute enumMemberAttribute = (EnumMemberAttribute)customAttributes[0];
							DataMember dataMember = new DataMember(fieldInfo);
							if (enumMemberAttribute.IsValueSetExplicitly)
							{
								if (enumMemberAttribute.Value == null || enumMemberAttribute.Value.Length == 0)
								{
									base.ThrowInvalidDataContractException(SR.GetString("'{0}' in type '{1}' cannot have EnumMemberAttribute attribute Value set to null or empty string.", new object[]
									{
										fieldInfo.Name,
										DataContract.GetClrTypeFullName(underlyingType)
									}));
								}
								dataMember.Name = enumMemberAttribute.Value;
							}
							else
							{
								dataMember.Name = fieldInfo.Name;
							}
							ClassDataContract.CheckAndAddMember(list, dataMember, memberNamesTable);
							flag = true;
						}
						object[] customAttributes2 = fieldInfo.GetCustomAttributes(Globals.TypeOfDataMemberAttribute, false);
						if (customAttributes2 != null && customAttributes2.Length != 0)
						{
							base.ThrowInvalidDataContractException(SR.GetString("Member '{0}.{1}' has DataMemberAttribute attribute. Use EnumMemberAttribute attribute instead.", new object[]
							{
								DataContract.GetClrTypeFullName(fieldInfo.DeclaringType),
								fieldInfo.Name
							}));
						}
					}
					else if (!fieldInfo.IsNotSerialized)
					{
						ClassDataContract.CheckAndAddMember(list, new DataMember(fieldInfo)
						{
							Name = fieldInfo.Name
						}, memberNamesTable);
						flag = true;
					}
					if (flag)
					{
						object value = fieldInfo.GetValue(null);
						if (this.isULong)
						{
							list2.Add((long)((IConvertible)value).ToUInt64(null));
						}
						else
						{
							list2.Add(((IConvertible)value).ToInt64(null));
						}
					}
				}
				Thread.MemoryBarrier();
				this.members = list;
				this.values = list2;
			}

			// Token: 0x0400050C RID: 1292
			private static Dictionary<Type, XmlQualifiedName> typeToName = new Dictionary<Type, XmlQualifiedName>();

			// Token: 0x0400050D RID: 1293
			private static Dictionary<XmlQualifiedName, Type> nameToType = new Dictionary<XmlQualifiedName, Type>();

			// Token: 0x0400050E RID: 1294
			private XmlQualifiedName baseContractName;

			// Token: 0x0400050F RID: 1295
			private List<DataMember> members;

			// Token: 0x04000510 RID: 1296
			private List<long> values;

			// Token: 0x04000511 RID: 1297
			private bool isULong;

			// Token: 0x04000512 RID: 1298
			private bool isFlags;

			// Token: 0x04000513 RID: 1299
			private bool hasDataContract;

			// Token: 0x04000514 RID: 1300
			private XmlDictionaryString[] childElementNames;
		}
	}
}
