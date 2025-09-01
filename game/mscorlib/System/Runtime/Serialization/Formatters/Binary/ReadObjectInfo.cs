using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006AF RID: 1711
	internal sealed class ReadObjectInfo
	{
		// Token: 0x06003F0C RID: 16140 RVA: 0x0000259F File Offset: 0x0000079F
		internal ReadObjectInfo()
		{
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void ObjectEnd()
		{
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000DA075 File Offset: 0x000D8275
		internal void PrepareForReuse()
		{
			this.lastPosition = 0;
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000DA080 File Offset: 0x000D8280
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000DA0A4 File Offset: 0x000D82A4
		[SecurityCritical]
		internal void Init(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			this.InitReadConstructor(objectType, surrogateSelector, context);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000DA0E0 File Offset: 0x000D82E0
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, memberNames, memberTypes, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000DA108 File Offset: 0x000D8308
		[SecurityCritical]
		internal void Init(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.wireMemberNames = memberNames;
			this.wireMemberTypes = memberTypes;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			if (memberNames != null)
			{
				this.isNamed = true;
			}
			if (memberTypes != null)
			{
				this.isTyped = true;
			}
			if (objectType != null)
			{
				this.InitReadConstructor(objectType, surrogateSelector, context);
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000DA174 File Offset: 0x000D8374
		[SecurityCritical]
		private void InitReadConstructor(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context)
		{
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.isSi = true;
			}
			if (this.isSi)
			{
				this.InitSiRead();
				return;
			}
			this.InitMemberInfo();
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000DA1E7 File Offset: 0x000D83E7
		private void InitSiRead()
		{
			if (this.memberTypesList != null)
			{
				this.memberTypesList = new List<Type>(20);
			}
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000DA1FE File Offset: 0x000D83FE
		private void InitNoMembers()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000DA214 File Offset: 0x000D8414
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
			this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
			this.count = this.cache.memberInfos.Length;
			this.cache.memberNames = new string[this.count];
			this.cache.memberTypes = new Type[this.count];
			for (int i = 0; i < this.count; i++)
			{
				this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
				this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000DA2EC File Offset: 0x000D84EC
		internal MemberInfo GetMemberInfo(string name)
		{
			if (this.cache == null)
			{
				return null;
			}
			if (this.isSi)
			{
				string key = "MemberInfo cannot be obtained for ISerialized Object '{0}'.";
				object[] array = new object[1];
				int num = 0;
				Type type = this.objectType;
				array[num] = ((type != null) ? type.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(key, array));
			}
			if (this.cache.memberInfos == null)
			{
				string key2 = "No MemberInfo for Object {0}.";
				object[] array2 = new object[1];
				int num2 = 0;
				Type type2 = this.objectType;
				array2[num2] = ((type2 != null) ? type2.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(key2, array2));
			}
			if (this.Position(name) != -1)
			{
				return this.cache.memberInfos[this.Position(name)];
			}
			return null;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x000DA3A4 File Offset: 0x000D85A4
		internal Type GetType(string name)
		{
			int num = this.Position(name);
			if (num == -1)
			{
				return null;
			}
			Type type;
			if (this.isTyped)
			{
				type = this.cache.memberTypes[num];
			}
			else
			{
				type = this.memberTypesList[num];
			}
			if (type == null)
			{
				string key = "Types not available for ISerializable object '{0}'.";
				object[] array = new object[1];
				int num2 = 0;
				Type type2 = this.objectType;
				array[num2] = ((type2 != null) ? type2.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(key, array));
			}
			return type;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000DA420 File Offset: 0x000D8620
		internal void AddValue(string name, object value, ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				si.AddValue(name, value);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				memberData[num] = value;
			}
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000DA454 File Offset: 0x000D8654
		internal void InitDataStore(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				if (si == null)
				{
					si = new SerializationInfo(this.objectType, this.formatterConverter);
					return;
				}
			}
			else if (memberData == null && this.cache != null)
			{
				memberData = new object[this.cache.memberNames.Length];
			}
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000DA4A4 File Offset: 0x000D86A4
		internal void RecordFixup(long objectId, string name, long idRef)
		{
			if (this.isSi)
			{
				this.objectManager.RecordDelayedFixup(objectId, name, idRef);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				this.objectManager.RecordFixup(objectId, this.cache.memberInfos[num], idRef);
			}
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000DA4EE File Offset: 0x000D86EE
		[SecurityCritical]
		internal void PopulateObjectMembers(object obj, object[] memberData)
		{
			if (!this.isSi && memberData != null)
			{
				FormatterServices.PopulateObjectMembers(obj, this.cache.memberInfos, memberData);
			}
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x000DA510 File Offset: 0x000D8710
		[Conditional("SER_LOGGING")]
		private void DumpPopulate(MemberInfo[] memberInfos, object[] memberData)
		{
			for (int i = 0; i < memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("SER_LOGGING")]
		private void DumpPopulateSi()
		{
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x000DA52C File Offset: 0x000D872C
		private int Position(string name)
		{
			if (this.cache == null)
			{
				return -1;
			}
			if (this.cache.memberNames.Length != 0 && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			int num = this.lastPosition + 1;
			this.lastPosition = num;
			if (num < this.cache.memberNames.Length && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			for (int i = 0; i < this.cache.memberNames.Length; i++)
			{
				if (this.cache.memberNames[i].Equals(name))
				{
					this.lastPosition = i;
					return this.lastPosition;
				}
			}
			this.lastPosition = 0;
			return -1;
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x000DA5F8 File Offset: 0x000D87F8
		internal Type[] GetMemberTypes(string[] inMemberNames, Type objectType)
		{
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("Types not available for ISerializable object '{0}'.", new object[]
				{
					objectType
				}));
			}
			if (this.cache == null)
			{
				return null;
			}
			if (this.cache.memberTypes == null)
			{
				this.cache.memberTypes = new Type[this.count];
				for (int i = 0; i < this.count; i++)
				{
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
			}
			bool flag = false;
			if (inMemberNames.Length < this.cache.memberInfos.Length)
			{
				flag = true;
			}
			Type[] array = new Type[this.cache.memberInfos.Length];
			for (int j = 0; j < this.cache.memberInfos.Length; j++)
			{
				if (!flag && inMemberNames[j].Equals(this.cache.memberInfos[j].Name))
				{
					array[j] = this.cache.memberTypes[j];
				}
				else
				{
					bool flag2 = false;
					for (int k = 0; k < inMemberNames.Length; k++)
					{
						if (this.cache.memberInfos[j].Name.Equals(inMemberNames[k]))
						{
							array[j] = this.cache.memberTypes[j];
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						object[] customAttributes = this.cache.memberInfos[j].GetCustomAttributes(typeof(OptionalFieldAttribute), false);
						if ((customAttributes == null || customAttributes.Length == 0) && !this.bSimpleAssembly)
						{
							throw new SerializationException(Environment.GetResourceString("Member '{0}' in class '{1}' is not present in the serialized stream and is not marked with {2}.", new object[]
							{
								this.cache.memberNames[j],
								objectType,
								typeof(OptionalFieldAttribute).FullName
							}));
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000DA7C4 File Offset: 0x000D89C4
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type result;
			if (objMember is FieldInfo)
			{
				result = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(Environment.GetResourceString("MemberInfo type {0} cannot be serialized.", new object[]
					{
						objMember.GetType()
					}));
				}
				result = ((PropertyInfo)objMember).PropertyType;
			}
			return result;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000DA81F File Offset: 0x000D8A1F
		private static ReadObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			return new ReadObjectInfo
			{
				objectInfoId = Interlocked.Increment(ref ReadObjectInfo.readObjectInfoCounter)
			};
		}

		// Token: 0x0400290E RID: 10510
		internal int objectInfoId;

		// Token: 0x0400290F RID: 10511
		internal static int readObjectInfoCounter;

		// Token: 0x04002910 RID: 10512
		internal Type objectType;

		// Token: 0x04002911 RID: 10513
		internal ObjectManager objectManager;

		// Token: 0x04002912 RID: 10514
		internal int count;

		// Token: 0x04002913 RID: 10515
		internal bool isSi;

		// Token: 0x04002914 RID: 10516
		internal bool isNamed;

		// Token: 0x04002915 RID: 10517
		internal bool isTyped;

		// Token: 0x04002916 RID: 10518
		internal bool bSimpleAssembly;

		// Token: 0x04002917 RID: 10519
		internal SerObjectInfoCache cache;

		// Token: 0x04002918 RID: 10520
		internal string[] wireMemberNames;

		// Token: 0x04002919 RID: 10521
		internal Type[] wireMemberTypes;

		// Token: 0x0400291A RID: 10522
		private int lastPosition;

		// Token: 0x0400291B RID: 10523
		internal ISurrogateSelector surrogateSelector;

		// Token: 0x0400291C RID: 10524
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x0400291D RID: 10525
		internal StreamingContext context;

		// Token: 0x0400291E RID: 10526
		internal List<Type> memberTypesList;

		// Token: 0x0400291F RID: 10527
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x04002920 RID: 10528
		internal IFormatterConverter formatterConverter;
	}
}
