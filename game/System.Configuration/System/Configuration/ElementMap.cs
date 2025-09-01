using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x0200001B RID: 27
	internal class ElementMap
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000046AC File Offset: 0x000028AC
		public static ElementMap GetMap(Type t)
		{
			ElementMap elementMap = ElementMap.elementMaps[t] as ElementMap;
			if (elementMap != null)
			{
				return elementMap;
			}
			elementMap = new ElementMap(t);
			ElementMap.elementMaps[t] = elementMap;
			return elementMap;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000046E4 File Offset: 0x000028E4
		public ElementMap(Type t)
		{
			this.properties = new ConfigurationPropertyCollection();
			this.collectionAttribute = (Attribute.GetCustomAttribute(t, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
			foreach (PropertyInfo propertyInfo in t.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ConfigurationPropertyAttribute configurationPropertyAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;
				if (configurationPropertyAttribute != null)
				{
					string name = (configurationPropertyAttribute.Name != null) ? configurationPropertyAttribute.Name : propertyInfo.Name;
					ConfigurationValidatorAttribute configurationValidatorAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationValidatorAttribute)) as ConfigurationValidatorAttribute;
					ConfigurationValidatorBase validator = (configurationValidatorAttribute != null) ? configurationValidatorAttribute.ValidatorInstance : null;
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute));
					TypeConverter typeConverter = (typeConverterAttribute != null) ? ((TypeConverter)Activator.CreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName), true)) : null;
					ConfigurationProperty configurationProperty = new ConfigurationProperty(name, propertyInfo.PropertyType, configurationPropertyAttribute.DefaultValue, typeConverter, validator, configurationPropertyAttribute.Options);
					configurationProperty.CollectionAttribute = (Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
					this.properties.Add(configurationProperty);
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004814 File Offset: 0x00002A14
		public ConfigurationCollectionAttribute CollectionAttribute
		{
			get
			{
				return this.collectionAttribute;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000481C File Offset: 0x00002A1C
		public bool HasProperties
		{
			get
			{
				return this.properties.Count > 0;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000482C File Offset: 0x00002A2C
		public ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004834 File Offset: 0x00002A34
		// Note: this type is marked as 'beforefieldinit'.
		static ElementMap()
		{
		}

		// Token: 0x04000070 RID: 112
		private static readonly Hashtable elementMaps = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000071 RID: 113
		private readonly ConfigurationPropertyCollection properties;

		// Token: 0x04000072 RID: 114
		private readonly ConfigurationCollectionAttribute collectionAttribute;
	}
}
