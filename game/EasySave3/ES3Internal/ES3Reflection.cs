using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using ES3Types;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000DA RID: 218
	public static class ES3Reflection
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001C434 File Offset: 0x0001A634
		private static Assembly[] Assemblies
		{
			get
			{
				if (ES3Reflection._assemblies == null)
				{
					string[] assemblyNames = new ES3Settings(null, null).assemblyNames;
					List<Assembly> list = new List<Assembly>();
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						try
						{
							if (assemblyNames.Contains(assembly.GetName().Name))
							{
								list.Add(assembly);
							}
						}
						catch
						{
						}
					}
					ES3Reflection._assemblies = list.ToArray();
				}
				return ES3Reflection._assemblies;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public static ConstructorInfo GetConstructor(Type type, Type[] parameters)
		{
			return type.GetTypeInfo().GetConstructor(parameters);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001C4CA File Offset: 0x0001A6CA
		public static Type[] GetElementTypes(Type type)
		{
			if (ES3Reflection.IsGenericType(type))
			{
				return ES3Reflection.GetGenericArguments(type);
			}
			if (type.IsArray)
			{
				return new Type[]
				{
					ES3Reflection.GetElementType(type)
				};
			}
			return null;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		public static List<FieldInfo> GetSerializableFields(Type type, List<FieldInfo> serializableFields = null, bool safe = true, string[] memberNames = null, BindingFlags bindings = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			if (type == null)
			{
				return new List<FieldInfo>();
			}
			FieldInfo[] fields = type.GetFields(bindings);
			if (serializableFields == null)
			{
				serializableFields = new List<FieldInfo>();
			}
			foreach (FieldInfo fieldInfo in fields)
			{
				string name = fieldInfo.Name;
				if (memberNames == null || memberNames.Contains(name))
				{
					Type fieldType = fieldInfo.FieldType;
					if (ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.es3SerializableAttributeType))
					{
						serializableFields.Add(fieldInfo);
					}
					else if (!ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.es3NonSerializableAttributeType) && (!safe || fieldInfo.IsPublic || ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.serializeFieldAttributeType)) && !fieldInfo.IsLiteral && !fieldInfo.IsInitOnly && (!(fieldType == type) || ES3Reflection.IsAssignableFrom(typeof(UnityEngine.Object), fieldType)) && !ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.nonSerializedAttributeType) && !ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.obsoleteAttributeType) && ES3Reflection.TypeIsSerializable(fieldInfo.FieldType) && (!safe || fieldInfo.DeclaringType.Namespace == null || !name.StartsWith("m_") || ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.serializeFieldAttributeType) || !fieldInfo.DeclaringType.Namespace.Contains("UnityEngine")))
					{
						serializableFields.Add(fieldInfo);
					}
				}
			}
			Type left = ES3Reflection.BaseType(type);
			if (left != null && left != typeof(object) && left != typeof(UnityEngine.Object))
			{
				ES3Reflection.GetSerializableFields(ES3Reflection.BaseType(type), serializableFields, safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			return serializableFields;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001C688 File Offset: 0x0001A888
		public static List<PropertyInfo> GetSerializableProperties(Type type, List<PropertyInfo> serializableProperties = null, bool safe = true, string[] memberNames = null, BindingFlags bindings = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			bool flag = ES3Reflection.IsAssignableFrom(typeof(Component), type);
			if (!safe)
			{
				bindings |= BindingFlags.NonPublic;
			}
			PropertyInfo[] properties = type.GetProperties(bindings);
			if (serializableProperties == null)
			{
				serializableProperties = new List<PropertyInfo>();
			}
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.es3SerializableAttributeType))
				{
					serializableProperties.Add(propertyInfo);
				}
				else if (!ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.es3NonSerializableAttributeType))
				{
					string name = propertyInfo.Name;
					if (!ES3Reflection.excludedPropertyNames.Contains(name) && (memberNames == null || memberNames.Contains(name)) && (!safe || ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.serializeFieldAttributeType) || ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.es3SerializableAttributeType)))
					{
						Type propertyType = propertyInfo.PropertyType;
						if ((!(propertyType == type) || ES3Reflection.IsAssignableFrom(typeof(UnityEngine.Object), propertyType)) && propertyInfo.CanRead && propertyInfo.CanWrite && (propertyInfo.GetIndexParameters().Length == 0 || propertyType.IsArray) && ES3Reflection.TypeIsSerializable(propertyType) && (!flag || (!(name == "tag") && !(name == "name"))) && !ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.obsoleteAttributeType) && !ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.nonSerializedAttributeType))
						{
							serializableProperties.Add(propertyInfo);
						}
					}
				}
			}
			Type type2 = ES3Reflection.BaseType(type);
			if (type2 != null && type2 != typeof(object))
			{
				ES3Reflection.GetSerializableProperties(type2, serializableProperties, safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			return serializableProperties;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001C81C File Offset: 0x0001AA1C
		public static bool TypeIsSerializable(Type type)
		{
			if (type == null)
			{
				return false;
			}
			if (ES3Reflection.AttributeIsDefined(type, ES3Reflection.es3NonSerializableAttributeType))
			{
				return false;
			}
			if (ES3Reflection.IsPrimitive(type) || ES3Reflection.IsValueType(type) || ES3Reflection.IsAssignableFrom(typeof(Component), type) || ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				return true;
			}
			ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, false);
			if (orCreateES3Type != null && !orCreateES3Type.isUnsupported)
			{
				return true;
			}
			if (ES3Reflection.TypeIsArray(type))
			{
				return ES3Reflection.TypeIsSerializable(type.GetElementType());
			}
			Type[] genericArguments = type.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (!ES3Reflection.TypeIsSerializable(genericArguments[i]))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001C8CC File Offset: 0x0001AACC
		public static object CreateInstance(Type type)
		{
			if (ES3Reflection.IsAssignableFrom(typeof(Component), type))
			{
				return ES3ComponentType.CreateComponent(type);
			}
			if (ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				return ScriptableObject.CreateInstance(type);
			}
			if (ES3Reflection.HasParameterlessConstructor(type))
			{
				return Activator.CreateInstance(type);
			}
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001C920 File Offset: 0x0001AB20
		public static object CreateInstance(Type type, params object[] args)
		{
			if (ES3Reflection.IsAssignableFrom(typeof(Component), type))
			{
				return ES3ComponentType.CreateComponent(type);
			}
			if (ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				return ScriptableObject.CreateInstance(type);
			}
			return Activator.CreateInstance(type, args);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001C95B File Offset: 0x0001AB5B
		public static Array ArrayCreateInstance(Type type, int length)
		{
			return Array.CreateInstance(type, new int[]
			{
				length
			});
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001C96D File Offset: 0x0001AB6D
		public static Array ArrayCreateInstance(Type type, int[] dimensions)
		{
			return Array.CreateInstance(type, dimensions);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001C976 File Offset: 0x0001AB76
		public static Type MakeGenericType(Type type, Type genericParam)
		{
			return type.MakeGenericType(new Type[]
			{
				genericParam
			});
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001C988 File Offset: 0x0001AB88
		public static ES3Reflection.ES3ReflectedMember[] GetSerializableMembers(Type type, bool safe = true, string[] memberNames = null)
		{
			if (type == null)
			{
				return new ES3Reflection.ES3ReflectedMember[0];
			}
			List<FieldInfo> serializableFields = ES3Reflection.GetSerializableFields(type, new List<FieldInfo>(), safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			List<PropertyInfo> serializableProperties = ES3Reflection.GetSerializableProperties(type, new List<PropertyInfo>(), safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			ES3Reflection.ES3ReflectedMember[] array = new ES3Reflection.ES3ReflectedMember[serializableFields.Count + serializableProperties.Count];
			for (int i = 0; i < serializableFields.Count; i++)
			{
				array[i] = new ES3Reflection.ES3ReflectedMember(serializableFields[i]);
			}
			for (int j = 0; j < serializableProperties.Count; j++)
			{
				array[j + serializableFields.Count] = new ES3Reflection.ES3ReflectedMember(serializableProperties[j]);
			}
			return array;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001CA2E File Offset: 0x0001AC2E
		public static ES3Reflection.ES3ReflectedMember GetES3ReflectedProperty(Type type, string propertyName)
		{
			return new ES3Reflection.ES3ReflectedMember(ES3Reflection.GetProperty(type, propertyName));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public static ES3Reflection.ES3ReflectedMember GetES3ReflectedMember(Type type, string fieldName)
		{
			return new ES3Reflection.ES3ReflectedMember(ES3Reflection.GetField(type, fieldName));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		public static IList<T> GetInstances<T>()
		{
			List<T> list = new List<T>();
			Assembly[] assemblies = ES3Reflection.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				foreach (Type type in assemblies[i].GetTypes())
				{
					if (ES3Reflection.IsAssignableFrom(typeof(T), type) && ES3Reflection.HasParameterlessConstructor(type) && !ES3Reflection.IsAbstract(type))
					{
						list.Add((T)((object)Activator.CreateInstance(type)));
					}
				}
			}
			return list;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001CACC File Offset: 0x0001ACCC
		public static IList<Type> GetDerivedTypes(Type derivedType)
		{
			return (from assembly in ES3Reflection.Assemblies
			from type in assembly.GetTypes()
			where ES3Reflection.IsAssignableFrom(derivedType, type)
			select type).ToList<Type>();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001CB68 File Offset: 0x0001AD68
		public static bool IsAssignableFrom(Type a, Type b)
		{
			return a.IsAssignableFrom(b);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001CB71 File Offset: 0x0001AD71
		public static Type GetGenericTypeDefinition(Type type)
		{
			return type.GetGenericTypeDefinition();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001CB79 File Offset: 0x0001AD79
		public static Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001CB81 File Offset: 0x0001AD81
		public static int GetArrayRank(Type type)
		{
			return type.GetArrayRank();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001CB89 File Offset: 0x0001AD89
		public static string GetAssemblyQualifiedName(Type type)
		{
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001CB91 File Offset: 0x0001AD91
		public static ES3Reflection.ES3ReflectedMethod GetMethod(Type type, string methodName, Type[] genericParameters, Type[] parameterTypes)
		{
			return new ES3Reflection.ES3ReflectedMethod(type, methodName, genericParameters, parameterTypes);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		public static bool TypeIsArray(Type type)
		{
			return type.IsArray;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		public static Type GetElementType(Type type)
		{
			return type.GetElementType();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001CBAC File Offset: 0x0001ADAC
		public static bool IsAbstract(Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
		public static bool IsInterface(Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		public static bool IsGenericType(Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		public static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		public static bool HasParameterlessConstructor(Type type)
		{
			return ES3Reflection.IsValueType(type) || ES3Reflection.GetParameterlessConstructor(type) != null;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
		public static ConstructorInfo GetParameterlessConstructor(Type type)
		{
			foreach (ConstructorInfo constructorInfo in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (constructorInfo.GetParameters().Length == 0)
				{
					return constructorInfo;
				}
			}
			return null;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001CC24 File Offset: 0x0001AE24
		public static string GetShortAssemblyQualifiedName(Type type)
		{
			if (ES3Reflection.IsPrimitive(type))
			{
				return type.ToString();
			}
			return type.FullName + "," + type.Assembly.GetName().Name;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001CC58 File Offset: 0x0001AE58
		public static PropertyInfo GetProperty(Type type, string propertyName)
		{
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null && ES3Reflection.BaseType(type) != typeof(object))
			{
				return ES3Reflection.GetProperty(ES3Reflection.BaseType(type), propertyName);
			}
			return property;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		public static FieldInfo GetField(Type type, string fieldName)
		{
			FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null && ES3Reflection.BaseType(type) != typeof(object))
			{
				return ES3Reflection.GetField(ES3Reflection.BaseType(type), fieldName);
			}
			return field;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		public static MethodInfo[] GetMethods(Type type, string methodName)
		{
			return (from t in type.GetMethods()
			where t.Name == methodName
			select t).ToArray<MethodInfo>();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001CD1E File Offset: 0x0001AF1E
		public static bool IsPrimitive(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(decimal);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		public static bool AttributeIsDefined(MemberInfo info, Type attributeType)
		{
			return Attribute.IsDefined(info, attributeType, true);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001CD56 File Offset: 0x0001AF56
		public static bool AttributeIsDefined(Type type, Type attributeType)
		{
			return type.IsDefined(attributeType, true);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001CD60 File Offset: 0x0001AF60
		public static bool ImplementsInterface(Type type, Type interfaceType)
		{
			return type.GetInterface(interfaceType.Name) != null;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001CD74 File Offset: 0x0001AF74
		public static Type BaseType(Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		public static Type GetType(string typeString)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(typeString);
			if (num <= 2667225454U)
			{
				if (num <= 1625787317U)
				{
					if (num <= 520654156U)
					{
						if (num != 356760993U)
						{
							if (num != 398550328U)
							{
								if (num == 520654156U)
								{
									if (typeString == "decimal")
									{
										return typeof(decimal);
									}
								}
							}
							else if (typeString == "string")
							{
								return typeof(string);
							}
						}
						else if (typeString == "UnityEngine.Object")
						{
							return typeof(UnityEngine.Object);
						}
					}
					else if (num != 718440320U)
					{
						if (num != 1416355490U)
						{
							if (num == 1625787317U)
							{
								if (typeString == "System.Object")
								{
									return typeof(object);
								}
							}
						}
						else if (typeString == "Texture2D")
						{
							return typeof(Texture2D);
						}
					}
					else if (typeString == "Component")
					{
						return typeof(Component);
					}
				}
				else if (num <= 2197844016U)
				{
					if (num != 1630192034U)
					{
						if (num != 1683620383U)
						{
							if (num == 2197844016U)
							{
								if (typeString == "Vector2")
								{
									return typeof(Vector2);
								}
							}
						}
						else if (typeString == "byte")
						{
							return typeof(byte);
						}
					}
					else if (typeString == "ushort")
					{
						return typeof(ushort);
					}
				}
				else if (num <= 2298509730U)
				{
					if (num != 2214621635U)
					{
						if (num == 2298509730U)
						{
							if (typeString == "Vector4")
							{
								return typeof(Vector4);
							}
						}
					}
					else if (typeString == "Vector3")
					{
						return typeof(Vector3);
					}
				}
				else if (num != 2515107422U)
				{
					if (num == 2667225454U)
					{
						if (typeString == "ulong")
						{
							return typeof(ulong);
						}
					}
				}
				else if (typeString == "int")
				{
					return typeof(int);
				}
			}
			else if (num <= 3270303571U)
			{
				if (num <= 2823553821U)
				{
					if (num != 2699759368U)
					{
						if (num != 2797886853U)
						{
							if (num == 2823553821U)
							{
								if (typeString == "char")
								{
									return typeof(char);
								}
							}
						}
						else if (typeString == "float")
						{
							return typeof(float);
						}
					}
					else if (typeString == "double")
					{
						return typeof(double);
					}
				}
				else if (num != 2911022011U)
				{
					if (num != 3122818005U)
					{
						if (num == 3270303571U)
						{
							if (typeString == "long")
							{
								return typeof(long);
							}
						}
					}
					else if (typeString == "short")
					{
						return typeof(short);
					}
				}
				else if (typeString == "Transform")
				{
					return typeof(Transform);
				}
			}
			else if (num <= 3415750305U)
			{
				if (num != 3289806692U)
				{
					if (num != 3365180733U)
					{
						if (num == 3415750305U)
						{
							if (typeString == "uint")
							{
								return typeof(uint);
							}
						}
					}
					else if (typeString == "bool")
					{
						return typeof(bool);
					}
				}
				else if (typeString == "GameObject")
				{
					return typeof(GameObject);
				}
			}
			else if (num <= 3847869726U)
			{
				if (num != 3419754368U)
				{
					if (num == 3847869726U)
					{
						if (typeString == "MeshFilter")
						{
							return typeof(MeshFilter);
						}
					}
				}
				else if (typeString == "Material")
				{
					return typeof(Material);
				}
			}
			else if (num != 3853794552U)
			{
				if (num == 4088464520U)
				{
					if (typeString == "sbyte")
					{
						return typeof(sbyte);
					}
				}
			}
			else if (typeString == "Color")
			{
				return typeof(Color);
			}
			return Type.GetType(typeString);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001D26C File Offset: 0x0001B46C
		public static string GetTypeString(Type type)
		{
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(byte))
			{
				return "byte";
			}
			if (type == typeof(sbyte))
			{
				return "sbyte";
			}
			if (type == typeof(char))
			{
				return "char";
			}
			if (type == typeof(decimal))
			{
				return "decimal";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(uint))
			{
				return "uint";
			}
			if (type == typeof(long))
			{
				return "long";
			}
			if (type == typeof(ulong))
			{
				return "ulong";
			}
			if (type == typeof(short))
			{
				return "short";
			}
			if (type == typeof(ushort))
			{
				return "ushort";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type == typeof(Vector2))
			{
				return "Vector2";
			}
			if (type == typeof(Vector3))
			{
				return "Vector3";
			}
			if (type == typeof(Vector4))
			{
				return "Vector4";
			}
			if (type == typeof(Color))
			{
				return "Color";
			}
			if (type == typeof(Transform))
			{
				return "Transform";
			}
			if (type == typeof(Component))
			{
				return "Component";
			}
			if (type == typeof(GameObject))
			{
				return "GameObject";
			}
			if (type == typeof(MeshFilter))
			{
				return "MeshFilter";
			}
			if (type == typeof(Material))
			{
				return "Material";
			}
			if (type == typeof(Texture2D))
			{
				return "Texture2D";
			}
			if (type == typeof(UnityEngine.Object))
			{
				return "UnityEngine.Object";
			}
			if (type == typeof(object))
			{
				return "System.Object";
			}
			return ES3Reflection.GetShortAssemblyQualifiedName(type);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		// Note: this type is marked as 'beforefieldinit'.
		static ES3Reflection()
		{
		}

		// Token: 0x04000141 RID: 321
		public const string memberFieldPrefix = "m_";

		// Token: 0x04000142 RID: 322
		public const string componentTagFieldName = "tag";

		// Token: 0x04000143 RID: 323
		public const string componentNameFieldName = "name";

		// Token: 0x04000144 RID: 324
		public static readonly string[] excludedPropertyNames = new string[]
		{
			"runInEditMode",
			"useGUILayout",
			"hideFlags"
		};

		// Token: 0x04000145 RID: 325
		public static readonly Type serializableAttributeType = typeof(SerializableAttribute);

		// Token: 0x04000146 RID: 326
		public static readonly Type serializeFieldAttributeType = typeof(SerializeField);

		// Token: 0x04000147 RID: 327
		public static readonly Type obsoleteAttributeType = typeof(ObsoleteAttribute);

		// Token: 0x04000148 RID: 328
		public static readonly Type nonSerializedAttributeType = typeof(NonSerializedAttribute);

		// Token: 0x04000149 RID: 329
		public static readonly Type es3SerializableAttributeType = typeof(ES3Serializable);

		// Token: 0x0400014A RID: 330
		public static readonly Type es3NonSerializableAttributeType = typeof(ES3NonSerializable);

		// Token: 0x0400014B RID: 331
		public static Type[] EmptyTypes = new Type[0];

		// Token: 0x0400014C RID: 332
		private static Assembly[] _assemblies = null;

		// Token: 0x0200010C RID: 268
		public struct ES3ReflectedMember
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00020621 File Offset: 0x0001E821
			public bool IsNull
			{
				get
				{
					return this.fieldInfo == null && this.propertyInfo == null;
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0002063F File Offset: 0x0001E83F
			public string Name
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.Name;
					}
					return this.propertyInfo.Name;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00020660 File Offset: 0x0001E860
			public Type MemberType
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.FieldType;
					}
					return this.propertyInfo.PropertyType;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00020681 File Offset: 0x0001E881
			public bool IsPublic
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.IsPublic;
					}
					return this.propertyInfo.GetGetMethod(true).IsPublic && this.propertyInfo.GetSetMethod(true).IsPublic;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000206BD File Offset: 0x0001E8BD
			public bool IsProtected
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.IsFamily;
					}
					return this.propertyInfo.GetGetMethod(true).IsFamily;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000206E4 File Offset: 0x0001E8E4
			public bool IsStatic
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.IsStatic;
					}
					return this.propertyInfo.GetGetMethod(true).IsStatic;
				}
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0002070C File Offset: 0x0001E90C
			public ES3ReflectedMember(object fieldPropertyInfo)
			{
				if (fieldPropertyInfo == null)
				{
					this.propertyInfo = null;
					this.fieldInfo = null;
					this.isProperty = false;
					return;
				}
				this.isProperty = ES3Reflection.IsAssignableFrom(typeof(PropertyInfo), fieldPropertyInfo.GetType());
				if (this.isProperty)
				{
					this.propertyInfo = (PropertyInfo)fieldPropertyInfo;
					this.fieldInfo = null;
					return;
				}
				this.fieldInfo = (FieldInfo)fieldPropertyInfo;
				this.propertyInfo = null;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0002077C File Offset: 0x0001E97C
			public void SetValue(object obj, object value)
			{
				if (this.isProperty)
				{
					this.propertyInfo.SetValue(obj, value, null);
					return;
				}
				this.fieldInfo.SetValue(obj, value);
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x000207A2 File Offset: 0x0001E9A2
			public object GetValue(object obj)
			{
				if (this.isProperty)
				{
					return this.propertyInfo.GetValue(obj, null);
				}
				return this.fieldInfo.GetValue(obj);
			}

			// Token: 0x0400020A RID: 522
			private FieldInfo fieldInfo;

			// Token: 0x0400020B RID: 523
			private PropertyInfo propertyInfo;

			// Token: 0x0400020C RID: 524
			public bool isProperty;
		}

		// Token: 0x0200010D RID: 269
		public class ES3ReflectedMethod
		{
			// Token: 0x060005BC RID: 1468 RVA: 0x000207C8 File Offset: 0x0001E9C8
			public ES3ReflectedMethod(Type type, string methodName, Type[] genericParameters, Type[] parameterTypes)
			{
				MethodInfo methodInfo = type.GetMethod(methodName, parameterTypes);
				this.method = methodInfo.MakeGenericMethod(genericParameters);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x000207F4 File Offset: 0x0001E9F4
			public ES3ReflectedMethod(Type type, string methodName, Type[] genericParameters, Type[] parameterTypes, BindingFlags bindingAttr)
			{
				MethodInfo methodInfo = type.GetMethod(methodName, bindingAttr, null, parameterTypes, null);
				this.method = methodInfo.MakeGenericMethod(genericParameters);
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x00020822 File Offset: 0x0001EA22
			public object Invoke(object obj, object[] parameters = null)
			{
				return this.method.Invoke(obj, parameters);
			}

			// Token: 0x0400020D RID: 525
			private MethodInfo method;
		}

		// Token: 0x0200010E RID: 270
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_0
		{
			// Token: 0x060005BF RID: 1471 RVA: 0x00020831 File Offset: 0x0001EA31
			public <>c__DisplayClass28_0()
			{
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x00020839 File Offset: 0x0001EA39
			internal bool <GetDerivedTypes>b__2(<>f__AnonymousType0<Assembly, Type> <>h__TransparentIdentifier0)
			{
				return ES3Reflection.IsAssignableFrom(this.derivedType, <>h__TransparentIdentifier0.type);
			}

			// Token: 0x0400020E RID: 526
			public Type derivedType;
		}

		// Token: 0x0200010F RID: 271
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C1 RID: 1473 RVA: 0x0002084C File Offset: 0x0001EA4C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x00020858 File Offset: 0x0001EA58
			public <>c()
			{
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x00020860 File Offset: 0x0001EA60
			internal IEnumerable<Type> <GetDerivedTypes>b__28_0(Assembly assembly)
			{
				return assembly.GetTypes();
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x00020868 File Offset: 0x0001EA68
			internal <>f__AnonymousType0<Assembly, Type> <GetDerivedTypes>b__28_1(Assembly assembly, Type type)
			{
				return new
				{
					assembly,
					type
				};
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x00020871 File Offset: 0x0001EA71
			internal Type <GetDerivedTypes>b__28_3(<>f__AnonymousType0<Assembly, Type> <>h__TransparentIdentifier0)
			{
				return <>h__TransparentIdentifier0.type;
			}

			// Token: 0x0400020F RID: 527
			public static readonly ES3Reflection.<>c <>9 = new ES3Reflection.<>c();

			// Token: 0x04000210 RID: 528
			public static Func<Assembly, IEnumerable<Type>> <>9__28_0;

			// Token: 0x04000211 RID: 529
			public static Func<Assembly, Type, <>f__AnonymousType0<Assembly, Type>> <>9__28_1;

			// Token: 0x04000212 RID: 530
			public static Func<<>f__AnonymousType0<Assembly, Type>, Type> <>9__28_3;
		}

		// Token: 0x02000110 RID: 272
		[CompilerGenerated]
		private sealed class <>c__DisplayClass47_0
		{
			// Token: 0x060005C6 RID: 1478 RVA: 0x00020879 File Offset: 0x0001EA79
			public <>c__DisplayClass47_0()
			{
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x00020881 File Offset: 0x0001EA81
			internal bool <GetMethods>b__0(MethodInfo t)
			{
				return t.Name == this.methodName;
			}

			// Token: 0x04000213 RID: 531
			public string methodName;
		}
	}
}
