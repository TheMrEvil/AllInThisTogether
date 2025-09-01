using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200041D RID: 1053
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x000759B3 File Offset: 0x00073BB3
		internal static Guid ExtenderProviderKey
		{
			get
			{
				return ReflectTypeDescriptionProvider._extenderProviderKey;
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000759BA File Offset: 0x00073BBA
		internal ReflectTypeDescriptionProvider()
		{
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x000759C4 File Offset: 0x00073BC4
		private static Hashtable IntrinsicTypeConverters
		{
			[PreserveDependency(".ctor()", "System.ComponentModel.BooleanConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.ByteConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.TimeSpanConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.GuidConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.SingleConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.UInt16Converter")]
			[PreserveDependency(".ctor(System.Type)", "System.ComponentModel.NullableConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.UInt32Converter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.UInt16Converter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.TypeConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.Int64Converter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.DateTimeConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.Int16Converter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.DateTimeOffsetConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.CultureInfoConverter")]
			[PreserveDependency(".ctor(System.Type)", "System.ComponentModel.ReferenceConverter")]
			[PreserveDependency(".ctor(System.Type)", "System.ComponentModel.EnumConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.CollectionConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.SByteConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.DecimalConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.ArrayConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.StringConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.Int32Converter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.DoubleConverter")]
			[PreserveDependency(".ctor()", "System.ComponentModel.CharConverter")]
			get
			{
				if (ReflectTypeDescriptionProvider._intrinsicTypeConverters == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable[typeof(bool)] = typeof(BooleanConverter);
					hashtable[typeof(byte)] = typeof(ByteConverter);
					hashtable[typeof(sbyte)] = typeof(SByteConverter);
					hashtable[typeof(char)] = typeof(CharConverter);
					hashtable[typeof(double)] = typeof(DoubleConverter);
					hashtable[typeof(string)] = typeof(StringConverter);
					hashtable[typeof(int)] = typeof(Int32Converter);
					hashtable[typeof(short)] = typeof(Int16Converter);
					hashtable[typeof(long)] = typeof(Int64Converter);
					hashtable[typeof(float)] = typeof(SingleConverter);
					hashtable[typeof(ushort)] = typeof(UInt16Converter);
					hashtable[typeof(uint)] = typeof(UInt32Converter);
					hashtable[typeof(ulong)] = typeof(UInt64Converter);
					hashtable[typeof(object)] = typeof(TypeConverter);
					hashtable[typeof(void)] = typeof(TypeConverter);
					hashtable[typeof(CultureInfo)] = typeof(CultureInfoConverter);
					hashtable[typeof(DateTime)] = typeof(DateTimeConverter);
					hashtable[typeof(DateTimeOffset)] = typeof(DateTimeOffsetConverter);
					hashtable[typeof(decimal)] = typeof(DecimalConverter);
					hashtable[typeof(TimeSpan)] = typeof(TimeSpanConverter);
					hashtable[typeof(Guid)] = typeof(GuidConverter);
					hashtable[typeof(Array)] = typeof(ArrayConverter);
					hashtable[typeof(ICollection)] = typeof(CollectionConverter);
					hashtable[typeof(Enum)] = typeof(EnumConverter);
					hashtable[ReflectTypeDescriptionProvider._intrinsicReferenceKey] = typeof(ReferenceConverter);
					hashtable[ReflectTypeDescriptionProvider._intrinsicNullableKey] = typeof(NullableConverter);
					ReflectTypeDescriptionProvider._intrinsicTypeConverters = hashtable;
				}
				return ReflectTypeDescriptionProvider._intrinsicTypeConverters;
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00075C8C File Offset: 0x00073E8C
		internal static void AddEditorTable(Type editorBaseType, Hashtable table)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				if (ReflectTypeDescriptionProvider._editorTables == null)
				{
					ReflectTypeDescriptionProvider._editorTables = new Hashtable(4);
				}
				if (!ReflectTypeDescriptionProvider._editorTables.ContainsKey(editorBaseType))
				{
					ReflectTypeDescriptionProvider._editorTables[editorBaseType] = table;
				}
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00075D10 File Offset: 0x00073F10
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			object obj;
			if (argTypes != null)
			{
				obj = SecurityUtils.SecureConstructorInvoke(objectType, argTypes, args, true, BindingFlags.ExactBinding);
			}
			else
			{
				if (args != null)
				{
					argTypes = new Type[args.Length];
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i] != null)
						{
							argTypes[i] = args[i].GetType();
						}
						else
						{
							argTypes[i] = typeof(object);
						}
					}
				}
				else
				{
					argTypes = new Type[0];
				}
				obj = SecurityUtils.SecureConstructorInvoke(objectType, argTypes, args, true);
			}
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(objectType, args);
			}
			return obj;
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00075D98 File Offset: 0x00073F98
		private static object CreateInstance(Type objectType, Type callingType)
		{
			object obj = SecurityUtils.SecureConstructorInvoke(objectType, ReflectTypeDescriptionProvider._typeConstructor, new object[]
			{
				callingType
			}, false);
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(objectType);
			}
			return obj;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00075DC7 File Offset: 0x00073FC7
		internal AttributeCollection GetAttributes(Type type)
		{
			return this.GetTypeData(type, true).GetAttributes();
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00075DD8 File Offset: 0x00073FD8
		public override IDictionary GetCache(object instance)
		{
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				IDictionaryService dictionaryService = component.Site.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (dictionaryService != null)
				{
					IDictionary dictionary = dictionaryService.GetValue(ReflectTypeDescriptionProvider._dictionaryKey) as IDictionary;
					if (dictionary == null)
					{
						dictionary = new Hashtable();
						dictionaryService.SetValue(ReflectTypeDescriptionProvider._dictionaryKey, dictionary);
					}
					return dictionary;
				}
			}
			return null;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00075E3E File Offset: 0x0007403E
		internal string GetClassName(Type type)
		{
			return this.GetTypeData(type, true).GetClassName(null);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00075E4E File Offset: 0x0007404E
		internal string GetComponentName(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetComponentName(instance);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00075E5E File Offset: 0x0007405E
		internal TypeConverter GetConverter(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetConverter(instance);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00075E6E File Offset: 0x0007406E
		internal EventDescriptor GetDefaultEvent(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetDefaultEvent(instance);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00075E7E File Offset: 0x0007407E
		internal PropertyDescriptor GetDefaultProperty(Type type, object instance)
		{
			return this.GetTypeData(type, true).GetDefaultProperty(instance);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00075E8E File Offset: 0x0007408E
		internal object GetEditor(Type type, object instance, Type editorBaseType)
		{
			return this.GetTypeData(type, true).GetEditor(instance, editorBaseType);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00075EA0 File Offset: 0x000740A0
		private static Hashtable GetEditorTable(Type editorBaseType)
		{
			if (ReflectTypeDescriptionProvider._editorTables == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._editorTables == null)
					{
						ReflectTypeDescriptionProvider._editorTables = new Hashtable(4);
					}
				}
			}
			object obj = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
			if (obj == null)
			{
				RuntimeHelpers.RunClassConstructor(editorBaseType.TypeHandle);
				obj = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
				if (obj == null)
				{
					object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
					lock (internalSyncObject)
					{
						obj = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
						if (obj == null)
						{
							ReflectTypeDescriptionProvider._editorTables[editorBaseType] = ReflectTypeDescriptionProvider._editorTables;
						}
					}
				}
			}
			if (obj == ReflectTypeDescriptionProvider._editorTables)
			{
				obj = null;
			}
			return (Hashtable)obj;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00075F84 File Offset: 0x00074184
		internal EventDescriptorCollection GetEvents(Type type)
		{
			return this.GetTypeData(type, true).GetEvents();
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00075F93 File Offset: 0x00074193
		internal AttributeCollection GetExtendedAttributes(object instance)
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00075F9A File Offset: 0x0007419A
		internal string GetExtendedClassName(object instance)
		{
			return this.GetClassName(instance.GetType());
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00075FA8 File Offset: 0x000741A8
		internal string GetExtendedComponentName(object instance)
		{
			return this.GetComponentName(instance.GetType(), instance);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00075FB7 File Offset: 0x000741B7
		internal TypeConverter GetExtendedConverter(object instance)
		{
			return this.GetConverter(instance.GetType(), instance);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00002F6A File Offset: 0x0000116A
		internal EventDescriptor GetExtendedDefaultEvent(object instance)
		{
			return null;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00002F6A File Offset: 0x0000116A
		internal PropertyDescriptor GetExtendedDefaultProperty(object instance)
		{
			return null;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00075FC6 File Offset: 0x000741C6
		internal object GetExtendedEditor(object instance, Type editorBaseType)
		{
			return this.GetEditor(instance.GetType(), instance, editorBaseType);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00075FD6 File Offset: 0x000741D6
		internal EventDescriptorCollection GetExtendedEvents(object instance)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00075FE0 File Offset: 0x000741E0
		internal PropertyDescriptorCollection GetExtendedProperties(object instance)
		{
			Type type = instance.GetType();
			IExtenderProvider[] extenderProviders = this.GetExtenderProviders(instance);
			IDictionary cache = TypeDescriptor.GetCache(instance);
			if (extenderProviders.Length == 0)
			{
				return PropertyDescriptorCollection.Empty;
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (cache != null)
			{
				propertyDescriptorCollection = (cache[ReflectTypeDescriptionProvider._extenderPropertiesKey] as PropertyDescriptorCollection);
			}
			if (propertyDescriptorCollection != null)
			{
				return propertyDescriptorCollection;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < extenderProviders.Length; i++)
			{
				PropertyDescriptor[] array = ReflectTypeDescriptionProvider.ReflectGetExtendedProperties(extenderProviders[i]);
				if (arrayList == null)
				{
					arrayList = new ArrayList(array.Length * extenderProviders.Length);
				}
				foreach (PropertyDescriptor propertyDescriptor in array)
				{
					ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = propertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
					if (extenderProvidedPropertyAttribute != null)
					{
						Type receiverType = extenderProvidedPropertyAttribute.ReceiverType;
						if (receiverType != null && receiverType.IsAssignableFrom(type))
						{
							arrayList.Add(propertyDescriptor);
						}
					}
				}
			}
			if (arrayList != null)
			{
				PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
				arrayList.CopyTo(array2, 0);
				propertyDescriptorCollection = new PropertyDescriptorCollection(array2, true);
			}
			else
			{
				propertyDescriptorCollection = PropertyDescriptorCollection.Empty;
			}
			if (cache != null)
			{
				cache[ReflectTypeDescriptionProvider._extenderPropertiesKey] = propertyDescriptorCollection;
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0007610C File Offset: 0x0007430C
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				IExtenderListService extenderListService = component.Site.GetService(typeof(IExtenderListService)) as IExtenderListService;
				IDictionary cache = TypeDescriptor.GetCache(instance);
				if (extenderListService != null)
				{
					return ReflectTypeDescriptionProvider.GetExtenders(extenderListService.GetExtenderProviders(), instance, cache);
				}
				IContainer container = component.Site.Container;
				if (container != null)
				{
					return ReflectTypeDescriptionProvider.GetExtenders(container.Components, instance, cache);
				}
			}
			return new IExtenderProvider[0];
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00076190 File Offset: 0x00074390
		private static IExtenderProvider[] GetExtenders(ICollection components, object instance, IDictionary cache)
		{
			bool flag = false;
			int num = 0;
			IExtenderProvider[] array = null;
			ulong num2 = 0UL;
			int num3 = 64;
			IExtenderProvider[] array2 = components as IExtenderProvider[];
			if (cache != null)
			{
				array = (cache[ReflectTypeDescriptionProvider._extenderProviderKey] as IExtenderProvider[]);
			}
			if (array == null)
			{
				flag = true;
			}
			int i = 0;
			int num4 = 0;
			if (array2 != null)
			{
				for (i = 0; i < array2.Length; i++)
				{
					if (array2[i].CanExtend(instance))
					{
						num++;
						if (i < num3)
						{
							num2 |= 1UL << i;
						}
						if (!flag && (num4 >= array.Length || array2[i] != array[num4++]))
						{
							flag = true;
						}
					}
				}
			}
			else if (components != null)
			{
				foreach (object obj in components)
				{
					IExtenderProvider extenderProvider = obj as IExtenderProvider;
					if (extenderProvider != null && extenderProvider.CanExtend(instance))
					{
						num++;
						if (i < num3)
						{
							num2 |= 1UL << i;
						}
						if (!flag && (num4 >= array.Length || extenderProvider != array[num4++]))
						{
							flag = true;
						}
					}
					i++;
				}
			}
			if (array != null && num != array.Length)
			{
				flag = true;
			}
			if (flag)
			{
				if (array2 == null || num != array2.Length)
				{
					IExtenderProvider[] array3 = new IExtenderProvider[num];
					i = 0;
					num4 = 0;
					if (array2 != null && num > 0)
					{
						while (i < array2.Length)
						{
							if ((i < num3 && (num2 & 1UL << i) != 0UL) || (i >= num3 && array2[i].CanExtend(instance)))
							{
								array3[num4++] = array2[i];
							}
							i++;
						}
					}
					else if (num > 0)
					{
						foreach (object obj2 in components)
						{
							IExtenderProvider extenderProvider2 = obj2 as IExtenderProvider;
							if (extenderProvider2 != null && ((i < num3 && (num2 & 1UL << i) != 0UL) || (i >= num3 && extenderProvider2.CanExtend(instance))))
							{
								array3[num4++] = extenderProvider2;
							}
							i++;
						}
					}
					array2 = array3;
				}
				if (cache != null)
				{
					cache[ReflectTypeDescriptionProvider._extenderProviderKey] = array2;
					cache.Remove(ReflectTypeDescriptionProvider._extenderPropertiesKey);
				}
			}
			else
			{
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000763C4 File Offset: 0x000745C4
		internal object GetExtendedPropertyOwner(object instance, PropertyDescriptor pd)
		{
			return this.GetPropertyOwner(instance.GetType(), instance, pd);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00002F6A File Offset: 0x0000116A
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return null;
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000763D4 File Offset: 0x000745D4
		public override string GetFullComponentName(object component)
		{
			IComponent component2 = component as IComponent;
			if (component2 != null)
			{
				INestedSite nestedSite = component2.Site as INestedSite;
				if (nestedSite != null)
				{
					return nestedSite.FullName;
				}
			}
			return TypeDescriptor.GetComponentName(component);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00076408 File Offset: 0x00074608
		internal Type[] GetPopulatedTypes(Module module)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._typeData)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				Type type = (Type)dictionaryEntry.Key;
				ReflectTypeDescriptionProvider.ReflectedTypeData reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)dictionaryEntry.Value;
				if (type.Module == module && reflectedTypeData.IsPopulated)
				{
					arrayList.Add(type);
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000764B4 File Offset: 0x000746B4
		internal PropertyDescriptorCollection GetProperties(Type type)
		{
			return this.GetTypeData(type, true).GetProperties();
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000764C3 File Offset: 0x000746C3
		internal object GetPropertyOwner(Type type, object instance, PropertyDescriptor pd)
		{
			return TypeDescriptor.GetAssociation(type, instance);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00003914 File Offset: 0x00001B14
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return objectType;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000764CC File Offset: 0x000746CC
		private ReflectTypeDescriptionProvider.ReflectedTypeData GetTypeData(Type type, bool createIfNeeded)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData reflectedTypeData = null;
			if (this._typeData != null)
			{
				reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)this._typeData[type];
				if (reflectedTypeData != null)
				{
					return reflectedTypeData;
				}
			}
			object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				if (this._typeData != null)
				{
					reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)this._typeData[type];
				}
				if (reflectedTypeData == null && createIfNeeded)
				{
					reflectedTypeData = new ReflectTypeDescriptionProvider.ReflectedTypeData(type);
					if (this._typeData == null)
					{
						this._typeData = new Hashtable();
					}
					this._typeData[type] = reflectedTypeData;
				}
			}
			return reflectedTypeData;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00002F6A File Offset: 0x0000116A
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return null;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00076570 File Offset: 0x00074770
		private static Type GetTypeFromName(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					type = Type.GetType(typeName.Substring(0, num));
				}
			}
			return type;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000765AC File Offset: 0x000747AC
		internal bool IsPopulated(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, false);
			return typeData != null && typeData.IsPopulated;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000765D0 File Offset: 0x000747D0
		private static Attribute[] ReflectGetAttributes(Type type)
		{
			object internalSyncObject;
			if (ReflectTypeDescriptionProvider._attributeCache == null)
			{
				internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._attributeCache == null)
					{
						ReflectTypeDescriptionProvider._attributeCache = new Hashtable();
					}
				}
			}
			Attribute[] array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[type];
			if (array != null)
			{
				return array;
			}
			internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[type];
				if (array == null)
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(Attribute), false);
					array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					ReflectTypeDescriptionProvider._attributeCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000766B0 File Offset: 0x000748B0
		internal static Attribute[] ReflectGetAttributes(MemberInfo member)
		{
			object internalSyncObject;
			if (ReflectTypeDescriptionProvider._attributeCache == null)
			{
				internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._attributeCache == null)
					{
						ReflectTypeDescriptionProvider._attributeCache = new Hashtable();
					}
				}
			}
			Attribute[] array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[member];
			if (array != null)
			{
				return array;
			}
			internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[member];
				if (array == null)
				{
					object[] customAttributes = member.GetCustomAttributes(typeof(Attribute), false);
					array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					ReflectTypeDescriptionProvider._attributeCache[member] = array;
				}
			}
			return array;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00076790 File Offset: 0x00074990
		private static EventDescriptor[] ReflectGetEvents(Type type)
		{
			object internalSyncObject;
			if (ReflectTypeDescriptionProvider._eventCache == null)
			{
				internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._eventCache == null)
					{
						ReflectTypeDescriptionProvider._eventCache = new Hashtable();
					}
				}
			}
			EventDescriptor[] array = (EventDescriptor[])ReflectTypeDescriptionProvider._eventCache[type];
			if (array != null)
			{
				return array;
			}
			internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				array = (EventDescriptor[])ReflectTypeDescriptionProvider._eventCache[type];
				if (array == null)
				{
					BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					EventInfo[] events = type.GetEvents(bindingAttr);
					array = new EventDescriptor[events.Length];
					int num = 0;
					foreach (EventInfo eventInfo in events)
					{
						if (eventInfo.DeclaringType.IsPublic || eventInfo.DeclaringType.IsNestedPublic || !(eventInfo.DeclaringType.Assembly == typeof(ReflectTypeDescriptionProvider).Assembly))
						{
							MethodInfo addMethod = eventInfo.GetAddMethod();
							MethodInfo removeMethod = eventInfo.GetRemoveMethod();
							if (addMethod != null && removeMethod != null)
							{
								array[num++] = new ReflectEventDescriptor(type, eventInfo);
							}
						}
					}
					if (num != array.Length)
					{
						EventDescriptor[] array2 = new EventDescriptor[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					ReflectTypeDescriptionProvider._eventCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00076918 File Offset: 0x00074B18
		private static PropertyDescriptor[] ReflectGetExtendedProperties(IExtenderProvider provider)
		{
			IDictionary cache = TypeDescriptor.GetCache(provider);
			PropertyDescriptor[] array;
			if (cache != null)
			{
				array = (cache[ReflectTypeDescriptionProvider._extenderProviderPropertiesKey] as PropertyDescriptor[]);
				if (array != null)
				{
					return array;
				}
			}
			if (ReflectTypeDescriptionProvider._extendedPropertyCache == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._extendedPropertyCache == null)
					{
						ReflectTypeDescriptionProvider._extendedPropertyCache = new Hashtable();
					}
				}
			}
			Type type = provider.GetType();
			ReflectPropertyDescriptor[] array2 = (ReflectPropertyDescriptor[])ReflectTypeDescriptionProvider._extendedPropertyCache[type];
			if (array2 == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					array2 = (ReflectPropertyDescriptor[])ReflectTypeDescriptionProvider._extendedPropertyCache[type];
					if (array2 == null)
					{
						AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
						ArrayList arrayList = new ArrayList(attributes.Count);
						foreach (object obj in attributes)
						{
							ProvidePropertyAttribute providePropertyAttribute = ((Attribute)obj) as ProvidePropertyAttribute;
							if (providePropertyAttribute != null)
							{
								Type typeFromName = ReflectTypeDescriptionProvider.GetTypeFromName(providePropertyAttribute.ReceiverTypeName);
								if (typeFromName != null)
								{
									MethodInfo method = type.GetMethod("Get" + providePropertyAttribute.PropertyName, new Type[]
									{
										typeFromName
									});
									if (method != null && !method.IsStatic && method.IsPublic)
									{
										MethodInfo methodInfo = type.GetMethod("Set" + providePropertyAttribute.PropertyName, new Type[]
										{
											typeFromName,
											method.ReturnType
										});
										if (methodInfo != null && (methodInfo.IsStatic || !methodInfo.IsPublic))
										{
											methodInfo = null;
										}
										arrayList.Add(new ReflectPropertyDescriptor(type, providePropertyAttribute.PropertyName, method.ReturnType, typeFromName, method, methodInfo, null));
									}
								}
							}
						}
						array2 = new ReflectPropertyDescriptor[arrayList.Count];
						arrayList.CopyTo(array2, 0);
						ReflectTypeDescriptionProvider._extendedPropertyCache[type] = array2;
					}
				}
			}
			array = new PropertyDescriptor[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				Attribute[] attributes2 = null;
				IComponent component = provider as IComponent;
				if (component == null || component.Site == null)
				{
					attributes2 = new Attribute[]
					{
						DesignOnlyAttribute.Yes
					};
				}
				ReflectPropertyDescriptor reflectPropertyDescriptor = array2[i];
				ExtendedPropertyDescriptor extendedPropertyDescriptor = new ExtendedPropertyDescriptor(reflectPropertyDescriptor, reflectPropertyDescriptor.ExtenderGetReceiverType(), provider, attributes2);
				array[i] = extendedPropertyDescriptor;
			}
			if (cache != null)
			{
				cache[ReflectTypeDescriptionProvider._extenderProviderPropertiesKey] = array;
			}
			return array;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x00076BF0 File Offset: 0x00074DF0
		private static PropertyDescriptor[] ReflectGetProperties(Type type)
		{
			object internalSyncObject;
			if (ReflectTypeDescriptionProvider._propertyCache == null)
			{
				internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._propertyCache == null)
					{
						ReflectTypeDescriptionProvider._propertyCache = new Hashtable();
					}
				}
			}
			PropertyDescriptor[] array = (PropertyDescriptor[])ReflectTypeDescriptionProvider._propertyCache[type];
			if (array != null)
			{
				return array;
			}
			internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				array = (PropertyDescriptor[])ReflectTypeDescriptionProvider._propertyCache[type];
				if (array == null)
				{
					BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					PropertyInfo[] properties = type.GetProperties(bindingAttr);
					array = new PropertyDescriptor[properties.Length];
					int num = 0;
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetIndexParameters().Length == 0)
						{
							MethodInfo getMethod = propertyInfo.GetGetMethod();
							MethodInfo setMethod = propertyInfo.GetSetMethod();
							string name = propertyInfo.Name;
							if (getMethod != null)
							{
								array[num++] = new ReflectPropertyDescriptor(type, name, propertyInfo.PropertyType, propertyInfo, getMethod, setMethod, null);
							}
						}
					}
					if (num != array.Length)
					{
						PropertyDescriptor[] array2 = new PropertyDescriptor[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					ReflectTypeDescriptionProvider._propertyCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00076D50 File Offset: 0x00074F50
		internal void Refresh(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, false);
			if (typeData != null)
			{
				typeData.Refresh();
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00076D70 File Offset: 0x00074F70
		private static object SearchIntrinsicTable(Hashtable table, Type callingType)
		{
			object obj = null;
			lock (table)
			{
				Type type = callingType;
				while (type != null && type != typeof(object))
				{
					obj = table[type];
					string text = obj as string;
					if (text != null)
					{
						obj = Type.GetType(text);
						if (obj != null)
						{
							table[type] = obj;
						}
					}
					if (obj != null)
					{
						break;
					}
					type = type.BaseType;
				}
				if (obj == null)
				{
					foreach (object obj2 in table)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						Type type2 = dictionaryEntry.Key as Type;
						if (type2 != null && type2.IsInterface && type2.IsAssignableFrom(callingType))
						{
							obj = dictionaryEntry.Value;
							string text2 = obj as string;
							if (text2 != null)
							{
								obj = Type.GetType(text2);
								if (obj != null)
								{
									table[callingType] = obj;
								}
							}
							if (obj != null)
							{
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					if (callingType.IsGenericType && callingType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						obj = table[ReflectTypeDescriptionProvider._intrinsicNullableKey];
					}
					else if (callingType.IsInterface)
					{
						obj = table[ReflectTypeDescriptionProvider._intrinsicReferenceKey];
					}
				}
				if (obj == null)
				{
					obj = table[typeof(object)];
				}
				Type type3 = obj as Type;
				if (type3 != null)
				{
					obj = ReflectTypeDescriptionProvider.CreateInstance(type3, callingType);
					if (type3.GetConstructor(ReflectTypeDescriptionProvider._typeConstructor) == null)
					{
						table[callingType] = obj;
					}
				}
			}
			return obj;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00076F40 File Offset: 0x00075140
		// Note: this type is marked as 'beforefieldinit'.
		static ReflectTypeDescriptionProvider()
		{
		}

		// Token: 0x04001054 RID: 4180
		private Hashtable _typeData;

		// Token: 0x04001055 RID: 4181
		private static Type[] _typeConstructor = new Type[]
		{
			typeof(Type)
		};

		// Token: 0x04001056 RID: 4182
		private static volatile Hashtable _editorTables;

		// Token: 0x04001057 RID: 4183
		private static volatile Hashtable _intrinsicTypeConverters;

		// Token: 0x04001058 RID: 4184
		private static object _intrinsicReferenceKey = new object();

		// Token: 0x04001059 RID: 4185
		private static object _intrinsicNullableKey = new object();

		// Token: 0x0400105A RID: 4186
		private static object _dictionaryKey = new object();

		// Token: 0x0400105B RID: 4187
		private static volatile Hashtable _propertyCache;

		// Token: 0x0400105C RID: 4188
		private static volatile Hashtable _eventCache;

		// Token: 0x0400105D RID: 4189
		private static volatile Hashtable _attributeCache;

		// Token: 0x0400105E RID: 4190
		private static volatile Hashtable _extendedPropertyCache;

		// Token: 0x0400105F RID: 4191
		private static readonly Guid _extenderProviderKey = Guid.NewGuid();

		// Token: 0x04001060 RID: 4192
		private static readonly Guid _extenderPropertiesKey = Guid.NewGuid();

		// Token: 0x04001061 RID: 4193
		private static readonly Guid _extenderProviderPropertiesKey = Guid.NewGuid();

		// Token: 0x04001062 RID: 4194
		private static readonly Type[] _skipInterfaceAttributeList = new Type[]
		{
			typeof(GuidAttribute),
			typeof(ComVisibleAttribute),
			typeof(InterfaceTypeAttribute)
		};

		// Token: 0x04001063 RID: 4195
		private static object _internalSyncObject = new object();

		// Token: 0x0200041E RID: 1054
		private class ReflectedTypeData
		{
			// Token: 0x06002247 RID: 8775 RVA: 0x00076FDD File Offset: 0x000751DD
			internal ReflectedTypeData(Type type)
			{
				this._type = type;
			}

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x06002248 RID: 8776 RVA: 0x00076FEC File Offset: 0x000751EC
			internal bool IsPopulated
			{
				get
				{
					return this._attributes != null | this._events != null | this._properties != null;
				}
			}

			// Token: 0x06002249 RID: 8777 RVA: 0x0007700C File Offset: 0x0007520C
			internal AttributeCollection GetAttributes()
			{
				if (this._attributes == null)
				{
					Attribute[] array = ReflectTypeDescriptionProvider.ReflectGetAttributes(this._type);
					Type baseType = this._type.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						Attribute[] array2 = ReflectTypeDescriptionProvider.ReflectGetAttributes(baseType);
						Attribute[] array3 = new Attribute[array.Length + array2.Length];
						Array.Copy(array, 0, array3, 0, array.Length);
						Array.Copy(array2, 0, array3, array.Length, array2.Length);
						array = array3;
						baseType = baseType.BaseType;
					}
					int num = array.Length;
					foreach (Type type in this._type.GetInterfaces())
					{
						if ((type.Attributes & TypeAttributes.NestedPrivate) != TypeAttributes.NotPublic)
						{
							AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
							if (attributes.Count > 0)
							{
								Attribute[] array4 = new Attribute[array.Length + attributes.Count];
								Array.Copy(array, 0, array4, 0, array.Length);
								attributes.CopyTo(array4, array.Length);
								array = array4;
							}
						}
					}
					OrderedDictionary orderedDictionary = new OrderedDictionary(array.Length);
					for (int j = 0; j < array.Length; j++)
					{
						bool flag = true;
						if (j >= num)
						{
							for (int k = 0; k < ReflectTypeDescriptionProvider._skipInterfaceAttributeList.Length; k++)
							{
								if (ReflectTypeDescriptionProvider._skipInterfaceAttributeList[k].IsInstanceOfType(array[j]))
								{
									flag = false;
									break;
								}
							}
						}
						if (flag && !orderedDictionary.Contains(array[j].TypeId))
						{
							orderedDictionary[array[j].TypeId] = array[j];
						}
					}
					array = new Attribute[orderedDictionary.Count];
					orderedDictionary.Values.CopyTo(array, 0);
					this._attributes = new AttributeCollection(array);
				}
				return this._attributes;
			}

			// Token: 0x0600224A RID: 8778 RVA: 0x000771B5 File Offset: 0x000753B5
			internal string GetClassName(object instance)
			{
				return this._type.FullName;
			}

			// Token: 0x0600224B RID: 8779 RVA: 0x000771C4 File Offset: 0x000753C4
			internal string GetComponentName(object instance)
			{
				IComponent component = instance as IComponent;
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						INestedSite nestedSite = site as INestedSite;
						if (nestedSite != null)
						{
							return nestedSite.FullName;
						}
						return site.Name;
					}
				}
				return null;
			}

			// Token: 0x0600224C RID: 8780 RVA: 0x00077200 File Offset: 0x00075400
			internal TypeConverter GetConverter(object instance)
			{
				TypeConverterAttribute typeConverterAttribute = null;
				if (instance != null)
				{
					typeConverterAttribute = (TypeConverterAttribute)TypeDescriptor.GetAttributes(this._type)[typeof(TypeConverterAttribute)];
					TypeConverterAttribute typeConverterAttribute2 = (TypeConverterAttribute)TypeDescriptor.GetAttributes(instance)[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute != typeConverterAttribute2)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute2.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							return (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName, this._type);
						}
					}
				}
				if (this._converter == null)
				{
					if (typeConverterAttribute == null)
					{
						typeConverterAttribute = (TypeConverterAttribute)TypeDescriptor.GetAttributes(this._type)[typeof(TypeConverterAttribute)];
					}
					if (typeConverterAttribute != null)
					{
						Type typeFromName2 = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName2 != null && typeof(TypeConverter).IsAssignableFrom(typeFromName2))
						{
							this._converter = (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName2, this._type);
						}
					}
					if (this._converter == null)
					{
						this._converter = (TypeConverter)ReflectTypeDescriptionProvider.SearchIntrinsicTable(ReflectTypeDescriptionProvider.IntrinsicTypeConverters, this._type);
					}
				}
				return this._converter;
			}

			// Token: 0x0600224D RID: 8781 RVA: 0x00077324 File Offset: 0x00075524
			internal EventDescriptor GetDefaultEvent(object instance)
			{
				AttributeCollection attributes;
				if (instance != null)
				{
					attributes = TypeDescriptor.GetAttributes(instance);
				}
				else
				{
					attributes = TypeDescriptor.GetAttributes(this._type);
				}
				DefaultEventAttribute defaultEventAttribute = (DefaultEventAttribute)attributes[typeof(DefaultEventAttribute)];
				if (defaultEventAttribute == null || defaultEventAttribute.Name == null)
				{
					return null;
				}
				if (instance != null)
				{
					return TypeDescriptor.GetEvents(instance)[defaultEventAttribute.Name];
				}
				return TypeDescriptor.GetEvents(this._type)[defaultEventAttribute.Name];
			}

			// Token: 0x0600224E RID: 8782 RVA: 0x00077398 File Offset: 0x00075598
			internal PropertyDescriptor GetDefaultProperty(object instance)
			{
				AttributeCollection attributes;
				if (instance != null)
				{
					attributes = TypeDescriptor.GetAttributes(instance);
				}
				else
				{
					attributes = TypeDescriptor.GetAttributes(this._type);
				}
				DefaultPropertyAttribute defaultPropertyAttribute = (DefaultPropertyAttribute)attributes[typeof(DefaultPropertyAttribute)];
				if (defaultPropertyAttribute == null || defaultPropertyAttribute.Name == null)
				{
					return null;
				}
				if (instance != null)
				{
					return TypeDescriptor.GetProperties(instance)[defaultPropertyAttribute.Name];
				}
				return TypeDescriptor.GetProperties(this._type)[defaultPropertyAttribute.Name];
			}

			// Token: 0x0600224F RID: 8783 RVA: 0x0007740C File Offset: 0x0007560C
			internal object GetEditor(object instance, Type editorBaseType)
			{
				EditorAttribute editorAttribute;
				if (instance != null)
				{
					editorAttribute = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(this._type), editorBaseType);
					EditorAttribute editorAttribute2 = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(instance), editorBaseType);
					if (editorAttribute != editorAttribute2)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute2.EditorTypeName);
						if (typeFromName != null && editorBaseType.IsAssignableFrom(typeFromName))
						{
							return ReflectTypeDescriptionProvider.CreateInstance(typeFromName, this._type);
						}
					}
				}
				ReflectTypeDescriptionProvider.ReflectedTypeData obj = this;
				lock (obj)
				{
					for (int i = 0; i < this._editorCount; i++)
					{
						if (this._editorTypes[i] == editorBaseType)
						{
							return this._editors[i];
						}
					}
				}
				object obj2 = null;
				editorAttribute = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(this._type), editorBaseType);
				if (editorAttribute != null)
				{
					Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
					if (typeFromName2 != null && editorBaseType.IsAssignableFrom(typeFromName2))
					{
						obj2 = ReflectTypeDescriptionProvider.CreateInstance(typeFromName2, this._type);
					}
				}
				if (obj2 == null)
				{
					Hashtable editorTable = ReflectTypeDescriptionProvider.GetEditorTable(editorBaseType);
					if (editorTable != null)
					{
						obj2 = ReflectTypeDescriptionProvider.SearchIntrinsicTable(editorTable, this._type);
					}
					if (obj2 != null && !editorBaseType.IsInstanceOfType(obj2))
					{
						obj2 = null;
					}
				}
				if (obj2 != null)
				{
					obj = this;
					lock (obj)
					{
						if (this._editorTypes == null || this._editorTypes.Length == this._editorCount)
						{
							int num = (this._editorTypes == null) ? 4 : (this._editorTypes.Length * 2);
							Type[] array = new Type[num];
							object[] array2 = new object[num];
							if (this._editorTypes != null)
							{
								this._editorTypes.CopyTo(array, 0);
								this._editors.CopyTo(array2, 0);
							}
							this._editorTypes = array;
							this._editors = array2;
							this._editorTypes[this._editorCount] = editorBaseType;
							object[] editors = this._editors;
							int editorCount = this._editorCount;
							this._editorCount = editorCount + 1;
							editors[editorCount] = obj2;
						}
					}
				}
				return obj2;
			}

			// Token: 0x06002250 RID: 8784 RVA: 0x00077610 File Offset: 0x00075810
			private static EditorAttribute GetEditorAttribute(AttributeCollection attributes, Type editorBaseType)
			{
				foreach (object obj in attributes)
				{
					EditorAttribute editorAttribute = ((Attribute)obj) as EditorAttribute;
					if (editorAttribute != null)
					{
						Type type = Type.GetType(editorAttribute.EditorBaseTypeName);
						if (type != null && type == editorBaseType)
						{
							return editorAttribute;
						}
					}
				}
				return null;
			}

			// Token: 0x06002251 RID: 8785 RVA: 0x00077690 File Offset: 0x00075890
			internal EventDescriptorCollection GetEvents()
			{
				if (this._events == null)
				{
					Dictionary<string, EventDescriptor> dictionary = new Dictionary<string, EventDescriptor>(16);
					Type type = this._type;
					Type typeFromHandle = typeof(object);
					EventDescriptor[] array;
					do
					{
						array = ReflectTypeDescriptionProvider.ReflectGetEvents(type);
						foreach (EventDescriptor eventDescriptor in array)
						{
							if (!dictionary.ContainsKey(eventDescriptor.Name))
							{
								dictionary.Add(eventDescriptor.Name, eventDescriptor);
							}
						}
						type = type.BaseType;
					}
					while (type != null && type != typeFromHandle);
					array = new EventDescriptor[dictionary.Count];
					dictionary.Values.CopyTo(array, 0);
					this._events = new EventDescriptorCollection(array, true);
				}
				return this._events;
			}

			// Token: 0x06002252 RID: 8786 RVA: 0x0007774C File Offset: 0x0007594C
			internal PropertyDescriptorCollection GetProperties()
			{
				if (this._properties == null)
				{
					Dictionary<string, PropertyDescriptor> dictionary = new Dictionary<string, PropertyDescriptor>(10);
					Type type = this._type;
					Type typeFromHandle = typeof(object);
					PropertyDescriptor[] array;
					do
					{
						array = ReflectTypeDescriptionProvider.ReflectGetProperties(type);
						foreach (PropertyDescriptor propertyDescriptor in array)
						{
							if (!dictionary.ContainsKey(propertyDescriptor.Name))
							{
								dictionary.Add(propertyDescriptor.Name, propertyDescriptor);
							}
						}
						type = type.BaseType;
					}
					while (type != null && type != typeFromHandle);
					array = new PropertyDescriptor[dictionary.Count];
					dictionary.Values.CopyTo(array, 0);
					this._properties = new PropertyDescriptorCollection(array, true);
				}
				return this._properties;
			}

			// Token: 0x06002253 RID: 8787 RVA: 0x00077808 File Offset: 0x00075A08
			private Type GetTypeFromName(string typeName)
			{
				if (typeName == null || typeName.Length == 0)
				{
					return null;
				}
				int num = typeName.IndexOf(',');
				Type type = null;
				if (num == -1)
				{
					type = this._type.Assembly.GetType(typeName);
				}
				if (type == null)
				{
					type = Type.GetType(typeName);
				}
				if (type == null && num != -1)
				{
					type = Type.GetType(typeName.Substring(0, num));
				}
				return type;
			}

			// Token: 0x06002254 RID: 8788 RVA: 0x0007786F File Offset: 0x00075A6F
			internal void Refresh()
			{
				this._attributes = null;
				this._events = null;
				this._properties = null;
				this._converter = null;
				this._editors = null;
				this._editorTypes = null;
				this._editorCount = 0;
			}

			// Token: 0x04001064 RID: 4196
			private Type _type;

			// Token: 0x04001065 RID: 4197
			private AttributeCollection _attributes;

			// Token: 0x04001066 RID: 4198
			private EventDescriptorCollection _events;

			// Token: 0x04001067 RID: 4199
			private PropertyDescriptorCollection _properties;

			// Token: 0x04001068 RID: 4200
			private TypeConverter _converter;

			// Token: 0x04001069 RID: 4201
			private object[] _editors;

			// Token: 0x0400106A RID: 4202
			private Type[] _editorTypes;

			// Token: 0x0400106B RID: 4203
			private int _editorCount;
		}
	}
}
