using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	// Token: 0x020002AA RID: 682
	internal static class ConfigUtil
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x00055AD4 File Offset: 0x00053CD4
		internal static T GetCustomAttribute<T>(MemberInfo m, bool inherit)
		{
			object[] customAttributes = m.GetCustomAttributes(typeof(T), false);
			if (customAttributes.Length == 0)
			{
				return default(T);
			}
			return (T)((object)customAttributes[0]);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00055B0C File Offset: 0x00053D0C
		internal static ConfigurationProperty BuildProperty(Type t, string name)
		{
			PropertyInfo property = t.GetProperty(name);
			ConfigurationPropertyAttribute customAttribute = ConfigUtil.GetCustomAttribute<ConfigurationPropertyAttribute>(property, false);
			TypeConverterAttribute customAttribute2 = ConfigUtil.GetCustomAttribute<TypeConverterAttribute>(property, false);
			ConfigurationValidatorAttribute customAttribute3 = ConfigUtil.GetCustomAttribute<ConfigurationValidatorAttribute>(property, false);
			return new ConfigurationProperty(customAttribute.Name, property.PropertyType, customAttribute.DefaultValue, (customAttribute2 != null) ? ((TypeConverter)Activator.CreateInstance(Type.GetType(customAttribute2.ConverterTypeName))) : null, (customAttribute3 != null) ? customAttribute3.ValidatorInstance : null, customAttribute.Options);
		}
	}
}
