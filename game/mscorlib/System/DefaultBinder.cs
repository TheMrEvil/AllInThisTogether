using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020001F0 RID: 496
	[Serializable]
	internal class DefaultBinder : Binder
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x00053998 File Offset: 0x00051B98
		[SecuritySafeCritical]
		public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo cultureInfo, string[] names, out object state)
		{
			if (match == null || match.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Array may not be empty."), "match");
			}
			MethodBase[] array = (MethodBase[])match.Clone();
			state = null;
			int[][] array2 = new int[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo[] parametersNoCopy = array[i].GetParametersNoCopy();
				array2[i] = new int[(parametersNoCopy.Length > args.Length) ? parametersNoCopy.Length : args.Length];
				if (names == null)
				{
					for (int j = 0; j < args.Length; j++)
					{
						array2[i][j] = j;
					}
				}
				else if (!DefaultBinder.CreateParamOrder(array2[i], parametersNoCopy, names))
				{
					array[i] = null;
				}
			}
			Type[] array3 = new Type[array.Length];
			Type[] array4 = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] != null)
				{
					array4[i] = args[i].GetType();
				}
			}
			int num = 0;
			bool flag = (bindingAttr & BindingFlags.OptionalParamBinding) > BindingFlags.Default;
			for (int i = 0; i < array.Length; i++)
			{
				Type type = null;
				if (!(array[i] == null))
				{
					ParameterInfo[] parametersNoCopy2 = array[i].GetParametersNoCopy();
					if (parametersNoCopy2.Length == 0)
					{
						if (args.Length == 0 || (array[i].CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
						{
							array2[num] = array2[i];
							array[num++] = array[i];
						}
					}
					else
					{
						int j;
						if (parametersNoCopy2.Length > args.Length)
						{
							j = args.Length;
							while (j < parametersNoCopy2.Length - 1 && parametersNoCopy2[j].DefaultValue != DBNull.Value)
							{
								j++;
							}
							if (j != parametersNoCopy2.Length - 1)
							{
								goto IL_426;
							}
							if (parametersNoCopy2[j].DefaultValue == DBNull.Value)
							{
								if (!parametersNoCopy2[j].ParameterType.IsArray || !parametersNoCopy2[j].IsDefined(typeof(ParamArrayAttribute), true))
								{
									goto IL_426;
								}
								type = parametersNoCopy2[j].ParameterType.GetElementType();
							}
						}
						else if (parametersNoCopy2.Length < args.Length)
						{
							int num2 = parametersNoCopy2.Length - 1;
							if (!parametersNoCopy2[num2].ParameterType.IsArray || !parametersNoCopy2[num2].IsDefined(typeof(ParamArrayAttribute), true) || array2[i][num2] != num2)
							{
								goto IL_426;
							}
							type = parametersNoCopy2[num2].ParameterType.GetElementType();
						}
						else
						{
							int num3 = parametersNoCopy2.Length - 1;
							if (parametersNoCopy2[num3].ParameterType.IsArray && parametersNoCopy2[num3].IsDefined(typeof(ParamArrayAttribute), true) && array2[i][num3] == num3 && !parametersNoCopy2[num3].ParameterType.IsAssignableFrom(array4[num3]))
							{
								type = parametersNoCopy2[num3].ParameterType.GetElementType();
							}
						}
						int num4 = (type != null) ? (parametersNoCopy2.Length - 1) : args.Length;
						for (j = 0; j < num4; j++)
						{
							Type type2 = parametersNoCopy2[j].ParameterType;
							if (type2.IsByRef)
							{
								type2 = type2.GetElementType();
							}
							if (!(type2 == array4[array2[i][j]]) && (!flag || args[array2[i][j]] != Type.Missing) && args[array2[i][j]] != null && !(type2 == typeof(object)))
							{
								if (type2.IsPrimitive)
								{
									if (array4[array2[i][j]] == null)
									{
										break;
									}
									if (!DefaultBinder.CanConvertPrimitiveObjectToType(args[array2[i][j]], (RuntimeType)type2))
									{
										break;
									}
								}
								else if (!(array4[array2[i][j]] == null) && !type2.IsAssignableFrom(array4[array2[i][j]]) && (!array4[array2[i][j]].IsCOMObject || !type2.IsInstanceOfType(args[array2[i][j]])))
								{
									break;
								}
							}
						}
						if (type != null && j == parametersNoCopy2.Length - 1)
						{
							while (j < args.Length)
							{
								if (type.IsPrimitive)
								{
									if (array4[j] == null)
									{
										break;
									}
									if (!DefaultBinder.CanConvertPrimitiveObjectToType(args[j], (RuntimeType)type))
									{
										break;
									}
								}
								else if (!(array4[j] == null) && !type.IsAssignableFrom(array4[j]) && (!array4[j].IsCOMObject || !type.IsInstanceOfType(args[j])))
								{
									break;
								}
								j++;
							}
						}
						if (j == args.Length)
						{
							array2[num] = array2[i];
							array3[num] = type;
							array[num++] = array[i];
						}
					}
				}
				IL_426:;
			}
			if (num == 0)
			{
				throw new MissingMethodException(Environment.GetResourceString("Member not found."));
			}
			if (num == 1)
			{
				if (names != null)
				{
					state = new DefaultBinder.BinderState((int[])array2[0].Clone(), args.Length, array3[0] != null);
					DefaultBinder.ReorderParams(array2[0], args);
				}
				ParameterInfo[] parametersNoCopy3 = array[0].GetParametersNoCopy();
				if (parametersNoCopy3.Length == args.Length)
				{
					if (array3[0] != null)
					{
						object[] array5 = new object[parametersNoCopy3.Length];
						int num5 = parametersNoCopy3.Length - 1;
						Array.Copy(args, 0, array5, 0, num5);
						array5[num5] = Array.UnsafeCreateInstance(array3[0], new int[]
						{
							1
						});
						((Array)array5[num5]).SetValue(args[num5], 0);
						args = array5;
					}
				}
				else if (parametersNoCopy3.Length > args.Length)
				{
					object[] array6 = new object[parametersNoCopy3.Length];
					int i;
					for (i = 0; i < args.Length; i++)
					{
						array6[i] = args[i];
					}
					while (i < parametersNoCopy3.Length - 1)
					{
						array6[i] = parametersNoCopy3[i].DefaultValue;
						i++;
					}
					if (array3[0] != null)
					{
						array6[i] = Array.UnsafeCreateInstance(array3[0], new int[1]);
					}
					else
					{
						array6[i] = parametersNoCopy3[i].DefaultValue;
					}
					args = array6;
				}
				else if ((array[0].CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
				{
					object[] array7 = new object[parametersNoCopy3.Length];
					int num6 = parametersNoCopy3.Length - 1;
					Array.Copy(args, 0, array7, 0, num6);
					array7[num6] = Array.UnsafeCreateInstance(array3[0], new int[]
					{
						args.Length - num6
					});
					Array.Copy(args, num6, (Array)array7[num6], 0, args.Length - num6);
					args = array7;
				}
				return array[0];
			}
			int num7 = 0;
			bool flag2 = false;
			for (int i = 1; i < num; i++)
			{
				int num8 = DefaultBinder.FindMostSpecificMethod(array[num7], array2[num7], array3[num7], array[i], array2[i], array3[i], array4, args);
				if (num8 == 0)
				{
					flag2 = true;
				}
				else if (num8 == 2)
				{
					num7 = i;
					flag2 = false;
				}
			}
			if (flag2)
			{
				throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
			}
			if (names != null)
			{
				state = new DefaultBinder.BinderState((int[])array2[num7].Clone(), args.Length, array3[num7] != null);
				DefaultBinder.ReorderParams(array2[num7], args);
			}
			ParameterInfo[] parametersNoCopy4 = array[num7].GetParametersNoCopy();
			if (parametersNoCopy4.Length == args.Length)
			{
				if (array3[num7] != null)
				{
					object[] array8 = new object[parametersNoCopy4.Length];
					int num9 = parametersNoCopy4.Length - 1;
					Array.Copy(args, 0, array8, 0, num9);
					array8[num9] = Array.UnsafeCreateInstance(array3[num7], new int[]
					{
						1
					});
					((Array)array8[num9]).SetValue(args[num9], 0);
					args = array8;
				}
			}
			else if (parametersNoCopy4.Length > args.Length)
			{
				object[] array9 = new object[parametersNoCopy4.Length];
				int i;
				for (i = 0; i < args.Length; i++)
				{
					array9[i] = args[i];
				}
				while (i < parametersNoCopy4.Length - 1)
				{
					array9[i] = parametersNoCopy4[i].DefaultValue;
					i++;
				}
				if (array3[num7] != null)
				{
					array9[i] = Array.UnsafeCreateInstance(array3[num7], new int[1]);
				}
				else
				{
					array9[i] = parametersNoCopy4[i].DefaultValue;
				}
				args = array9;
			}
			else if ((array[num7].CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				object[] array10 = new object[parametersNoCopy4.Length];
				int num10 = parametersNoCopy4.Length - 1;
				Array.Copy(args, 0, array10, 0, num10);
				array10[num10] = Array.UnsafeCreateInstance(array3[num7], new int[]
				{
					args.Length - num10
				});
				Array.Copy(args, num10, (Array)array10[num10], 0, args.Length - num10);
				args = array10;
			}
			return array[num7];
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00054184 File Offset: 0x00052384
		[SecuritySafeCritical]
		public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo cultureInfo)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			FieldInfo[] array = (FieldInfo[])match.Clone();
			if ((bindingAttr & BindingFlags.SetField) != BindingFlags.Default)
			{
				Type type = value.GetType();
				for (int i = 0; i < array.Length; i++)
				{
					Type fieldType = array[i].FieldType;
					if (fieldType == type)
					{
						array[num++] = array[i];
					}
					else if (value == Empty.Value && fieldType.IsClass)
					{
						array[num++] = array[i];
					}
					else if (fieldType == typeof(object))
					{
						array[num++] = array[i];
					}
					else if (fieldType.IsPrimitive)
					{
						if (DefaultBinder.CanConvertPrimitiveObjectToType(value, (RuntimeType)fieldType))
						{
							array[num++] = array[i];
						}
					}
					else if (fieldType.IsAssignableFrom(type))
					{
						array[num++] = array[i];
					}
				}
				if (num == 0)
				{
					throw new MissingFieldException(Environment.GetResourceString("Field not found."));
				}
				if (num == 1)
				{
					return array[0];
				}
			}
			int num2 = 0;
			bool flag = false;
			for (int i = 1; i < num; i++)
			{
				int num3 = DefaultBinder.FindMostSpecificField(array[num2], array[i]);
				if (num3 == 0)
				{
					flag = true;
				}
				else if (num3 == 2)
				{
					num2 = i;
					flag = false;
				}
			}
			if (flag)
			{
				throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
			}
			return array[num2];
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000542D4 File Offset: 0x000524D4
		[SecuritySafeCritical]
		public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
		{
			if (indexes != null)
			{
				if (!Contract.ForAll<Type>(indexes, (Type t) => t != null))
				{
					throw new ArgumentNullException("indexes");
				}
			}
			if (match == null || match.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Array may not be empty."), "match");
			}
			PropertyInfo[] array = (PropertyInfo[])match.Clone();
			int i = 0;
			int num = 0;
			int num2 = (indexes != null) ? indexes.Length : 0;
			int j = 0;
			while (j < array.Length)
			{
				if (indexes == null)
				{
					goto IL_114;
				}
				ParameterInfo[] indexParameters = array[j].GetIndexParameters();
				if (indexParameters.Length == num2)
				{
					for (i = 0; i < num2; i++)
					{
						Type parameterType = indexParameters[i].ParameterType;
						if (!(parameterType == indexes[i]) && !(parameterType == typeof(object)))
						{
							if (parameterType.IsPrimitive)
							{
								if (!(indexes[i].UnderlyingSystemType is RuntimeType))
								{
									break;
								}
								if (!DefaultBinder.CanConvertPrimitive((RuntimeType)indexes[i].UnderlyingSystemType, (RuntimeType)parameterType.UnderlyingSystemType))
								{
									break;
								}
							}
							else if (!parameterType.IsAssignableFrom(indexes[i]))
							{
								break;
							}
						}
					}
					goto IL_114;
				}
				IL_17E:
				j++;
				continue;
				IL_114:
				if (i == num2)
				{
					if (returnType != null)
					{
						if (array[j].PropertyType.IsPrimitive)
						{
							if (!(returnType.UnderlyingSystemType is RuntimeType))
							{
								goto IL_17E;
							}
							if (!DefaultBinder.CanConvertPrimitive((RuntimeType)returnType.UnderlyingSystemType, (RuntimeType)array[j].PropertyType.UnderlyingSystemType))
							{
								goto IL_17E;
							}
						}
						else if (!array[j].PropertyType.IsAssignableFrom(returnType))
						{
							goto IL_17E;
						}
					}
					array[num++] = array[j];
					goto IL_17E;
				}
				goto IL_17E;
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				return array[0];
			}
			int num3 = 0;
			bool flag = false;
			int[] array2 = new int[num2];
			for (j = 0; j < num2; j++)
			{
				array2[j] = j;
			}
			for (j = 1; j < num; j++)
			{
				int num4 = DefaultBinder.FindMostSpecificType(array[num3].PropertyType, array[j].PropertyType, returnType);
				if (num4 == 0 && indexes != null)
				{
					num4 = DefaultBinder.FindMostSpecific(array[num3].GetIndexParameters(), array2, null, array[j].GetIndexParameters(), array2, null, indexes, null);
				}
				if (num4 == 0)
				{
					num4 = DefaultBinder.FindMostSpecificProperty(array[num3], array[j]);
					if (num4 == 0)
					{
						flag = true;
					}
				}
				if (num4 == 2)
				{
					flag = false;
					num3 = j;
				}
			}
			if (flag)
			{
				throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
			}
			return array[num3];
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00054524 File Offset: 0x00052724
		public override object ChangeType(object value, Type type, CultureInfo cultureInfo)
		{
			throw new NotSupportedException(Environment.GetResourceString("ChangeType operation is not supported."));
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00054538 File Offset: 0x00052738
		public override void ReorderArgumentArray(ref object[] args, object state)
		{
			DefaultBinder.BinderState binderState = (DefaultBinder.BinderState)state;
			DefaultBinder.ReorderParams(binderState.m_argsMap, args);
			if (!binderState.m_isParamArray)
			{
				if (args.Length > binderState.m_originalSize)
				{
					object[] array = new object[binderState.m_originalSize];
					Array.Copy(args, 0, array, 0, binderState.m_originalSize);
					args = array;
				}
				return;
			}
			int num = args.Length - 1;
			if (args.Length == binderState.m_originalSize)
			{
				args[num] = ((object[])args[num])[0];
				return;
			}
			object[] array2 = new object[args.Length];
			Array.Copy(args, 0, array2, 0, num);
			int i = num;
			int num2 = 0;
			while (i < array2.Length)
			{
				array2[i] = ((object[])args[num])[num2];
				i++;
				num2++;
			}
			args = array2;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000545F4 File Offset: 0x000527F4
		public static MethodBase ExactBinding(MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			MethodBase[] array = new MethodBase[match.Length];
			int num = 0;
			for (int i = 0; i < match.Length; i++)
			{
				ParameterInfo[] parametersNoCopy = match[i].GetParametersNoCopy();
				if (parametersNoCopy.Length != 0)
				{
					int num2 = 0;
					while (num2 < types.Length && parametersNoCopy[num2].ParameterType.Equals(types[num2]))
					{
						num2++;
					}
					if (num2 >= types.Length)
					{
						array[num] = match[i];
						num++;
					}
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				return array[0];
			}
			return DefaultBinder.FindMostDerivedNewSlotMeth(array, num);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00054680 File Offset: 0x00052880
		public static PropertyInfo ExactPropertyBinding(PropertyInfo[] match, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			PropertyInfo propertyInfo = null;
			int num = (types != null) ? types.Length : 0;
			for (int i = 0; i < match.Length; i++)
			{
				ParameterInfo[] indexParameters = match[i].GetIndexParameters();
				int num2 = 0;
				while (num2 < num && !(indexParameters[num2].ParameterType != types[num2]))
				{
					num2++;
				}
				if (num2 >= num && (!(returnType != null) || !(returnType != match[i].PropertyType)))
				{
					if (propertyInfo != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					propertyInfo = match[i];
				}
			}
			return propertyInfo;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00054720 File Offset: 0x00052920
		private static int FindMostSpecific(ParameterInfo[] p1, int[] paramOrder1, Type paramArrayType1, ParameterInfo[] p2, int[] paramOrder2, Type paramArrayType2, Type[] types, object[] args)
		{
			if (paramArrayType1 != null && paramArrayType2 == null)
			{
				return 2;
			}
			if (paramArrayType2 != null && paramArrayType1 == null)
			{
				return 1;
			}
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < types.Length; i++)
			{
				if (args == null || args[i] != Type.Missing)
				{
					Type type;
					if (paramArrayType1 != null && paramOrder1[i] >= p1.Length - 1)
					{
						type = paramArrayType1;
					}
					else
					{
						type = p1[paramOrder1[i]].ParameterType;
					}
					Type type2;
					if (paramArrayType2 != null && paramOrder2[i] >= p2.Length - 1)
					{
						type2 = paramArrayType2;
					}
					else
					{
						type2 = p2[paramOrder2[i]].ParameterType;
					}
					if (!(type == type2))
					{
						switch (DefaultBinder.FindMostSpecificType(type, type2, types[i]))
						{
						case 0:
							return 0;
						case 1:
							flag = true;
							break;
						case 2:
							flag2 = true;
							break;
						}
					}
				}
			}
			if (flag == flag2)
			{
				if (!flag && args != null)
				{
					if (p1.Length > p2.Length)
					{
						return 1;
					}
					if (p2.Length > p1.Length)
					{
						return 2;
					}
				}
				return 0;
			}
			if (!flag)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00054828 File Offset: 0x00052A28
		[SecuritySafeCritical]
		private static int FindMostSpecificType(Type c1, Type c2, Type t)
		{
			if (c1 == c2)
			{
				return 0;
			}
			if (c1 == t)
			{
				return 1;
			}
			if (c2 == t)
			{
				return 2;
			}
			if (c1.IsByRef || c2.IsByRef)
			{
				if (c1.IsByRef && c2.IsByRef)
				{
					c1 = c1.GetElementType();
					c2 = c2.GetElementType();
				}
				else if (c1.IsByRef)
				{
					if (c1.GetElementType() == c2)
					{
						return 2;
					}
					c1 = c1.GetElementType();
				}
				else
				{
					if (c2.GetElementType() == c1)
					{
						return 1;
					}
					c2 = c2.GetElementType();
				}
			}
			bool flag;
			bool flag2;
			if (c1.IsPrimitive && c2.IsPrimitive)
			{
				flag = DefaultBinder.CanConvertPrimitive((RuntimeType)c2, (RuntimeType)c1);
				flag2 = DefaultBinder.CanConvertPrimitive((RuntimeType)c1, (RuntimeType)c2);
			}
			else
			{
				flag = c1.IsAssignableFrom(c2);
				flag2 = c2.IsAssignableFrom(c1);
			}
			if (flag == flag2)
			{
				return 0;
			}
			if (flag)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00054914 File Offset: 0x00052B14
		private static int FindMostSpecificMethod(MethodBase m1, int[] paramOrder1, Type paramArrayType1, MethodBase m2, int[] paramOrder2, Type paramArrayType2, Type[] types, object[] args)
		{
			int num = DefaultBinder.FindMostSpecific(m1.GetParametersNoCopy(), paramOrder1, paramArrayType1, m2.GetParametersNoCopy(), paramOrder2, paramArrayType2, types, args);
			if (num != 0)
			{
				return num;
			}
			if (!DefaultBinder.CompareMethodSigAndName(m1, m2))
			{
				return 0;
			}
			int hierarchyDepth = DefaultBinder.GetHierarchyDepth(m1.DeclaringType);
			int hierarchyDepth2 = DefaultBinder.GetHierarchyDepth(m2.DeclaringType);
			if (hierarchyDepth == hierarchyDepth2)
			{
				return 0;
			}
			if (hierarchyDepth < hierarchyDepth2)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00054974 File Offset: 0x00052B74
		private static int FindMostSpecificField(FieldInfo cur1, FieldInfo cur2)
		{
			if (!(cur1.Name == cur2.Name))
			{
				return 0;
			}
			int hierarchyDepth = DefaultBinder.GetHierarchyDepth(cur1.DeclaringType);
			int hierarchyDepth2 = DefaultBinder.GetHierarchyDepth(cur2.DeclaringType);
			if (hierarchyDepth == hierarchyDepth2)
			{
				return 0;
			}
			if (hierarchyDepth < hierarchyDepth2)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000549BC File Offset: 0x00052BBC
		private static int FindMostSpecificProperty(PropertyInfo cur1, PropertyInfo cur2)
		{
			if (!(cur1.Name == cur2.Name))
			{
				return 0;
			}
			int hierarchyDepth = DefaultBinder.GetHierarchyDepth(cur1.DeclaringType);
			int hierarchyDepth2 = DefaultBinder.GetHierarchyDepth(cur2.DeclaringType);
			if (hierarchyDepth == hierarchyDepth2)
			{
				return 0;
			}
			if (hierarchyDepth < hierarchyDepth2)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00054A04 File Offset: 0x00052C04
		internal static bool CompareMethodSigAndName(MethodBase m1, MethodBase m2)
		{
			ParameterInfo[] parametersNoCopy = m1.GetParametersNoCopy();
			ParameterInfo[] parametersNoCopy2 = m2.GetParametersNoCopy();
			if (parametersNoCopy.Length != parametersNoCopy2.Length)
			{
				return false;
			}
			int num = parametersNoCopy.Length;
			for (int i = 0; i < num; i++)
			{
				if (parametersNoCopy[i].ParameterType != parametersNoCopy2[i].ParameterType)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00054A54 File Offset: 0x00052C54
		internal static int GetHierarchyDepth(Type t)
		{
			int num = 0;
			Type type = t;
			do
			{
				num++;
				type = type.BaseType;
			}
			while (type != null);
			return num;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00054A7C File Offset: 0x00052C7C
		internal static MethodBase FindMostDerivedNewSlotMeth(MethodBase[] match, int cMatches)
		{
			int num = 0;
			MethodBase result = null;
			for (int i = 0; i < cMatches; i++)
			{
				int hierarchyDepth = DefaultBinder.GetHierarchyDepth(match[i].DeclaringType);
				if (hierarchyDepth == num)
				{
					throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
				}
				if (hierarchyDepth > num)
				{
					num = hierarchyDepth;
					result = match[i];
				}
			}
			return result;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00054AC8 File Offset: 0x00052CC8
		private static void ReorderParams(int[] paramOrder, object[] vars)
		{
			object[] array = new object[vars.Length];
			for (int i = 0; i < vars.Length; i++)
			{
				array[i] = vars[i];
			}
			for (int j = 0; j < vars.Length; j++)
			{
				vars[j] = array[paramOrder[j]];
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00054B08 File Offset: 0x00052D08
		private static bool CreateParamOrder(int[] paramOrder, ParameterInfo[] pars, string[] names)
		{
			bool[] array = new bool[pars.Length];
			for (int i = 0; i < pars.Length; i++)
			{
				paramOrder[i] = -1;
			}
			for (int j = 0; j < names.Length; j++)
			{
				int k;
				for (k = 0; k < pars.Length; k++)
				{
					if (names[j].Equals(pars[k].Name))
					{
						paramOrder[k] = j;
						array[j] = true;
						break;
					}
				}
				if (k == pars.Length)
				{
					return false;
				}
			}
			int l = 0;
			for (int m = 0; m < pars.Length; m++)
			{
				if (paramOrder[m] == -1)
				{
					while (l < pars.Length)
					{
						if (!array[l])
						{
							paramOrder[m] = l;
							l++;
							break;
						}
						l++;
					}
				}
			}
			return true;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00054BB0 File Offset: 0x00052DB0
		private static bool CanConvertPrimitive(RuntimeType source, RuntimeType target)
		{
			if (source.IsEnum)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(source);
			TypeCode typeCode2 = Type.GetTypeCode(target);
			if (typeCode == typeCode2 && source.IsPrimitive)
			{
				return true;
			}
			switch (typeCode2)
			{
			case TypeCode.Char:
				return typeCode == TypeCode.Byte || typeCode == TypeCode.UInt16;
			case TypeCode.Int16:
				return typeCode - TypeCode.SByte <= 1;
			case TypeCode.UInt16:
				return typeCode == TypeCode.Char || typeCode == TypeCode.Byte;
			case TypeCode.Int32:
				return typeCode - TypeCode.Char <= 4;
			case TypeCode.UInt32:
				switch (typeCode)
				{
				case TypeCode.Char:
				case TypeCode.Byte:
				case TypeCode.UInt16:
					return true;
				}
				return false;
			case TypeCode.Int64:
				return typeCode - TypeCode.Char <= 6;
			case TypeCode.UInt64:
				switch (typeCode)
				{
				case TypeCode.Char:
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
					return true;
				}
				return false;
			case TypeCode.Single:
				return typeCode - TypeCode.Char <= 8;
			case TypeCode.Double:
				return typeCode - TypeCode.Char <= 9;
			}
			if (target == typeof(IntPtr))
			{
				return source == target;
			}
			return target == typeof(UIntPtr) && source == target;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00054CE8 File Offset: 0x00052EE8
		private static bool CanConvertPrimitiveObjectToType(object source, RuntimeType type)
		{
			if (source == null)
			{
				return true;
			}
			Type type2 = source.GetType();
			return type2 == type || DefaultBinder.CanConvertPrimitive((RuntimeType)type2, type);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00054D18 File Offset: 0x00052F18
		internal static bool CompareMethodSig(MethodBase m1, MethodBase m2)
		{
			ParameterInfo[] parametersNoCopy = m1.GetParametersNoCopy();
			ParameterInfo[] parametersNoCopy2 = m2.GetParametersNoCopy();
			if (parametersNoCopy.Length != parametersNoCopy2.Length)
			{
				return false;
			}
			int num = parametersNoCopy.Length;
			for (int i = 0; i < num; i++)
			{
				if (parametersNoCopy[i].ParameterType != parametersNoCopy2[i].ParameterType)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00054D68 File Offset: 0x00052F68
		public sealed override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
		{
			Type[] array = new Type[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = types[i].UnderlyingSystemType;
				if (!array[i].IsRuntimeImplemented() && !(array[i] is SignatureType))
				{
					throw new ArgumentException("Type must be a type provided by the runtime.", "types");
				}
			}
			types = array;
			if (match == null || match.Length == 0)
			{
				throw new ArgumentException("Array may not be empty.", "match");
			}
			MethodBase[] array2 = (MethodBase[])match.Clone();
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
			{
				ParameterInfo[] parametersNoCopy = array2[i].GetParametersNoCopy();
				if (parametersNoCopy.Length == types.Length)
				{
					int j;
					for (j = 0; j < types.Length; j++)
					{
						Type parameterType = parametersNoCopy[j].ParameterType;
						if (!types[j].MatchesParameterTypeExactly(parametersNoCopy[j]) && !(parameterType == typeof(object)))
						{
							Type type = types[j];
							SignatureType signatureType = type as SignatureType;
							if (signatureType != null)
							{
								MethodInfo methodInfo = array2[i] as MethodInfo;
								if (methodInfo == null)
								{
									break;
								}
								type = signatureType.TryResolveAgainstGenericMethod(methodInfo);
								if (type == null)
								{
									break;
								}
							}
							if (parameterType.IsPrimitive)
							{
								if (!type.UnderlyingSystemType.IsRuntimeImplemented())
								{
									break;
								}
								if (!DefaultBinder.CanChangePrimitive(type.UnderlyingSystemType, parameterType.UnderlyingSystemType))
								{
									break;
								}
							}
							else if (!parameterType.IsAssignableFrom(type))
							{
								break;
							}
						}
					}
					if (j == types.Length)
					{
						array2[num++] = array2[i];
					}
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				return array2[0];
			}
			int num2 = 0;
			bool flag = false;
			int[] array3 = new int[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array3[i] = i;
			}
			for (int i = 1; i < num; i++)
			{
				int num3 = DefaultBinder.FindMostSpecificMethod(array2[num2], array3, null, array2[i], array3, null, types, null);
				if (num3 == 0)
				{
					flag = true;
				}
				else if (num3 == 2)
				{
					flag = false;
					num2 = i;
				}
			}
			if (flag)
			{
				throw new AmbiguousMatchException("Ambiguous match found.");
			}
			return array2[num2];
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00054F50 File Offset: 0x00053150
		private static bool CanChangePrimitive(Type source, Type target)
		{
			return DefaultBinder.CanPrimitiveWiden(source, target);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00054F59 File Offset: 0x00053159
		private static bool CanChangePrimitiveObjectToType(object source, Type type)
		{
			return DefaultBinder.CanPrimitiveWiden(source.GetType(), type);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00054F68 File Offset: 0x00053168
		private static bool CanPrimitiveWiden(Type source, Type target)
		{
			DefaultBinder.Primitives primitives = DefaultBinder._primitiveConversions[(int)Type.GetTypeCode(source)];
			DefaultBinder.Primitives primitives2 = (DefaultBinder.Primitives)(1 << (int)Type.GetTypeCode(target));
			return (primitives & primitives2) > (DefaultBinder.Primitives)0;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00054F94 File Offset: 0x00053194
		public DefaultBinder()
		{
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00054F9C File Offset: 0x0005319C
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultBinder()
		{
		}

		// Token: 0x040014F7 RID: 5367
		private static DefaultBinder.Primitives[] _primitiveConversions = new DefaultBinder.Primitives[]
		{
			(DefaultBinder.Primitives)0,
			(DefaultBinder.Primitives)0,
			(DefaultBinder.Primitives)0,
			DefaultBinder.Primitives.Boolean,
			DefaultBinder.Primitives.Char | DefaultBinder.Primitives.UInt16 | DefaultBinder.Primitives.Int32 | DefaultBinder.Primitives.UInt32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.UInt64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.SByte | DefaultBinder.Primitives.Int16 | DefaultBinder.Primitives.Int32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Char | DefaultBinder.Primitives.Byte | DefaultBinder.Primitives.Int16 | DefaultBinder.Primitives.UInt16 | DefaultBinder.Primitives.Int32 | DefaultBinder.Primitives.UInt32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.UInt64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Int16 | DefaultBinder.Primitives.Int32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.UInt16 | DefaultBinder.Primitives.Int32 | DefaultBinder.Primitives.UInt32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.UInt64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Int32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.UInt32 | DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.UInt64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Int64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.UInt64 | DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Single | DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Double,
			DefaultBinder.Primitives.Decimal,
			DefaultBinder.Primitives.DateTime,
			(DefaultBinder.Primitives)0,
			DefaultBinder.Primitives.String
		};

		// Token: 0x020001F1 RID: 497
		internal class BinderState
		{
			// Token: 0x0600156D RID: 5485 RVA: 0x00054FB5 File Offset: 0x000531B5
			internal BinderState(int[] argsMap, int originalSize, bool isParamArray)
			{
				this.m_argsMap = argsMap;
				this.m_originalSize = originalSize;
				this.m_isParamArray = isParamArray;
			}

			// Token: 0x040014F8 RID: 5368
			internal int[] m_argsMap;

			// Token: 0x040014F9 RID: 5369
			internal int m_originalSize;

			// Token: 0x040014FA RID: 5370
			internal bool m_isParamArray;
		}

		// Token: 0x020001F2 RID: 498
		[Flags]
		private enum Primitives
		{
			// Token: 0x040014FC RID: 5372
			Boolean = 8,
			// Token: 0x040014FD RID: 5373
			Char = 16,
			// Token: 0x040014FE RID: 5374
			SByte = 32,
			// Token: 0x040014FF RID: 5375
			Byte = 64,
			// Token: 0x04001500 RID: 5376
			Int16 = 128,
			// Token: 0x04001501 RID: 5377
			UInt16 = 256,
			// Token: 0x04001502 RID: 5378
			Int32 = 512,
			// Token: 0x04001503 RID: 5379
			UInt32 = 1024,
			// Token: 0x04001504 RID: 5380
			Int64 = 2048,
			// Token: 0x04001505 RID: 5381
			UInt64 = 4096,
			// Token: 0x04001506 RID: 5382
			Single = 8192,
			// Token: 0x04001507 RID: 5383
			Double = 16384,
			// Token: 0x04001508 RID: 5384
			Decimal = 32768,
			// Token: 0x04001509 RID: 5385
			DateTime = 65536,
			// Token: 0x0400150A RID: 5386
			String = 262144
		}

		// Token: 0x020001F3 RID: 499
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600156E RID: 5486 RVA: 0x00054FD2 File Offset: 0x000531D2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600156F RID: 5487 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06001570 RID: 5488 RVA: 0x00054FDE File Offset: 0x000531DE
			internal bool <SelectProperty>b__2_0(Type t)
			{
				return t != null;
			}

			// Token: 0x0400150B RID: 5387
			public static readonly DefaultBinder.<>c <>9 = new DefaultBinder.<>c();

			// Token: 0x0400150C RID: 5388
			public static Predicate<Type> <>9__2_0;
		}
	}
}
