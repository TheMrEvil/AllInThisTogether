using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000156 RID: 342
	internal static class CodeInterpreter
	{
		// Token: 0x06001244 RID: 4676 RVA: 0x00046EF0 File Offset: 0x000450F0
		internal static object ConvertValue(object arg, Type source, Type target)
		{
			return CodeInterpreter.InternalConvert(arg, source, target, false);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00046EFB File Offset: 0x000450FB
		private static bool CanConvert(TypeCode typeCode)
		{
			return typeCode - TypeCode.Boolean <= 11;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00046F08 File Offset: 0x00045108
		private static object InternalConvert(object arg, Type source, Type target, bool isAddress)
		{
			if (target == source)
			{
				return arg;
			}
			if (target.IsValueType)
			{
				if (source.IsValueType)
				{
					if (!CodeInterpreter.CanConvert(Type.GetTypeCode(target)))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An internal error has occurred. No conversion is possible to '{0}' - error generating code for serialization.", new object[]
						{
							DataContract.GetClrTypeFullName(target)
						})));
					}
					return target;
				}
				else
				{
					if (source.IsAssignableFrom(target))
					{
						return arg;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An internal error has occurred. '{0}' is not assignable from '{1}' - error generating code for serialization.", new object[]
					{
						DataContract.GetClrTypeFullName(target),
						DataContract.GetClrTypeFullName(source)
					})));
				}
			}
			else
			{
				if (target.IsAssignableFrom(source))
				{
					return arg;
				}
				if (source.IsAssignableFrom(target))
				{
					return arg;
				}
				if (target.IsInterface || source.IsInterface)
				{
					return arg;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An internal error has occurred. '{0}' is not assignable from '{1}' - error generating code for serialization.", new object[]
				{
					DataContract.GetClrTypeFullName(target),
					DataContract.GetClrTypeFullName(source)
				})));
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00046FF0 File Offset: 0x000451F0
		public static object GetMember(MemberInfo memberInfo, object instance)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.GetValue(instance);
			}
			return ((FieldInfo)memberInfo).GetValue(instance);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00047024 File Offset: 0x00045224
		public static void SetMember(MemberInfo memberInfo, object instance, object value)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(instance, value);
				return;
			}
			((FieldInfo)memberInfo).SetValue(instance, value);
		}
	}
}
