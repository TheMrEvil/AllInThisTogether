using System;

namespace IKVM.Reflection
{
	// Token: 0x02000009 RID: 9
	internal sealed class DefaultBinder : Binder
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002CDC File Offset: 0x00000EDC
		public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
		{
			int num = 0;
			foreach (MethodBase methodBase in match)
			{
				if (DefaultBinder.MatchParameterTypes(methodBase.GetParameters(), types))
				{
					match[num++] = methodBase;
				}
			}
			if (num == 0)
			{
				return null;
			}
			MethodBase result = match[0];
			bool flag = false;
			for (int j = 1; j < num; j++)
			{
				DefaultBinder.SelectBestMatch(match[j], types, ref result, ref flag);
			}
			if (flag)
			{
				throw new AmbiguousMatchException();
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D50 File Offset: 0x00000F50
		private static bool MatchParameterTypes(ParameterInfo[] parameters, Type[] types)
		{
			if (parameters.Length != types.Length)
			{
				return false;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				Type type = types[i];
				Type parameterType = parameters[i].ParameterType;
				if (type != parameterType && !parameterType.IsAssignableFrom(type) && !DefaultBinder.IsAllowedPrimitiveConversion(type, parameterType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DA0 File Offset: 0x00000FA0
		private static void SelectBestMatch(MethodBase candidate, Type[] types, ref MethodBase currentBest, ref bool ambiguous)
		{
			int num = DefaultBinder.MatchSignatures(currentBest.MethodSignature, candidate.MethodSignature, types);
			if (num == 1)
			{
				return;
			}
			if (num != 2)
			{
				if (currentBest.MethodSignature.MatchParameterTypes(candidate.MethodSignature))
				{
					int inheritanceDepth = DefaultBinder.GetInheritanceDepth(currentBest.DeclaringType);
					int inheritanceDepth2 = DefaultBinder.GetInheritanceDepth(candidate.DeclaringType);
					if (inheritanceDepth > inheritanceDepth2)
					{
						return;
					}
					if (inheritanceDepth < inheritanceDepth2)
					{
						ambiguous = false;
						currentBest = candidate;
						return;
					}
				}
				ambiguous = true;
				return;
			}
			ambiguous = false;
			currentBest = candidate;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E14 File Offset: 0x00001014
		private static int GetInheritanceDepth(Type type)
		{
			int num = 0;
			while (type != null)
			{
				num++;
				type = type.BaseType;
			}
			return num;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E3C File Offset: 0x0000103C
		private static int MatchSignatures(MethodSignature sig1, MethodSignature sig2, Type[] types)
		{
			for (int i = 0; i < sig1.GetParameterCount(); i++)
			{
				Type parameterType = sig1.GetParameterType(i);
				Type parameterType2 = sig2.GetParameterType(i);
				if (parameterType != parameterType2)
				{
					return DefaultBinder.MatchTypes(parameterType, parameterType2, types[i]);
				}
			}
			return 0;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E80 File Offset: 0x00001080
		private static int MatchSignatures(PropertySignature sig1, PropertySignature sig2, Type[] types)
		{
			for (int i = 0; i < sig1.ParameterCount; i++)
			{
				Type parameter = sig1.GetParameter(i);
				Type parameter2 = sig2.GetParameter(i);
				if (parameter != parameter2)
				{
					return DefaultBinder.MatchTypes(parameter, parameter2, types[i]);
				}
			}
			return 0;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EC4 File Offset: 0x000010C4
		private static int MatchTypes(Type type1, Type type2, Type type)
		{
			if (type1 == type)
			{
				return 1;
			}
			if (type2 == type)
			{
				return 2;
			}
			bool flag = type1.IsAssignableFrom(type2);
			if (flag == type2.IsAssignableFrom(type1))
			{
				return 0;
			}
			if (!flag)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F04 File Offset: 0x00001104
		private static bool IsAllowedPrimitiveConversion(Type source, Type target)
		{
			if (!source.IsPrimitive || !target.IsPrimitive)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(source);
			TypeCode typeCode2 = Type.GetTypeCode(target);
			switch (typeCode)
			{
			case TypeCode.Char:
				switch (typeCode2)
				{
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				default:
					return false;
				}
				break;
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				}
				return false;
			case TypeCode.Byte:
				switch (typeCode2)
				{
				case TypeCode.Char:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				}
				return false;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				}
				return false;
			case TypeCode.UInt16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				default:
					return false;
				}
				break;
			case TypeCode.Int32:
				switch (typeCode2)
				{
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				}
				return false;
			case TypeCode.UInt32:
				switch (typeCode2)
				{
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				default:
					return false;
				}
				break;
			case TypeCode.Int64:
				return typeCode2 == TypeCode.Single || typeCode2 == TypeCode.Double;
			case TypeCode.UInt64:
				return typeCode2 == TypeCode.Single || typeCode2 == TypeCode.Double;
			case TypeCode.Single:
				return typeCode2 == TypeCode.Double;
			default:
				return false;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030AC File Offset: 0x000012AC
		public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
		{
			int num = 0;
			foreach (PropertyInfo propertyInfo in match)
			{
				if (indexes == null || DefaultBinder.MatchParameterTypes(propertyInfo.GetIndexParameters(), indexes))
				{
					if (returnType != null)
					{
						if (propertyInfo.PropertyType.IsPrimitive)
						{
							if (!DefaultBinder.IsAllowedPrimitiveConversion(returnType, propertyInfo.PropertyType))
							{
								goto IL_63;
							}
						}
						else if (!propertyInfo.PropertyType.IsAssignableFrom(returnType))
						{
							goto IL_63;
						}
					}
					match[num++] = propertyInfo;
				}
				IL_63:;
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				return match[0];
			}
			PropertyInfo propertyInfo2 = match[0];
			bool flag = false;
			for (int j = 1; j < num; j++)
			{
				int num2 = DefaultBinder.MatchTypes(propertyInfo2.PropertyType, match[j].PropertyType, returnType);
				if (num2 == 0 && indexes != null)
				{
					num2 = DefaultBinder.MatchSignatures(propertyInfo2.PropertySignature, match[j].PropertySignature, indexes);
				}
				if (num2 == 0)
				{
					int inheritanceDepth = DefaultBinder.GetInheritanceDepth(propertyInfo2.DeclaringType);
					int inheritanceDepth2 = DefaultBinder.GetInheritanceDepth(match[j].DeclaringType);
					if (propertyInfo2.Name == match[j].Name && inheritanceDepth != inheritanceDepth2)
					{
						if (inheritanceDepth > inheritanceDepth2)
						{
							num2 = 1;
						}
						else
						{
							num2 = 2;
						}
					}
					else
					{
						flag = true;
					}
				}
				if (num2 == 2)
				{
					flag = false;
					propertyInfo2 = match[j];
				}
			}
			if (flag)
			{
				throw new AmbiguousMatchException();
			}
			return propertyInfo2;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000031EE File Offset: 0x000013EE
		public DefaultBinder()
		{
		}
	}
}
