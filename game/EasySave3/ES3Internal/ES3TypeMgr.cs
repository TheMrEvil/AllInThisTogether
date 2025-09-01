using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using ES3Types;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Internal
{
	// Token: 0x020000E5 RID: 229
	[Preserve]
	public static class ES3TypeMgr
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x0001E600 File Offset: 0x0001C800
		public static ES3Type GetOrCreateES3Type(Type type, bool throwException = true)
		{
			if (ES3TypeMgr.types == null)
			{
				ES3TypeMgr.Init();
			}
			if (type != typeof(object) && ES3TypeMgr.lastAccessedType != null && ES3TypeMgr.lastAccessedType.type == type)
			{
				return ES3TypeMgr.lastAccessedType;
			}
			if (ES3TypeMgr.types.TryGetValue(type, out ES3TypeMgr.lastAccessedType))
			{
				return ES3TypeMgr.lastAccessedType;
			}
			return ES3TypeMgr.lastAccessedType = ES3TypeMgr.CreateES3Type(type, throwException);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001E66F File Offset: 0x0001C86F
		public static ES3Type GetES3Type(Type type)
		{
			if (ES3TypeMgr.types == null)
			{
				ES3TypeMgr.Init();
			}
			if (ES3TypeMgr.types.TryGetValue(type, out ES3TypeMgr.lastAccessedType))
			{
				return ES3TypeMgr.lastAccessedType;
			}
			return null;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001E698 File Offset: 0x0001C898
		internal static void Add(Type type, ES3Type es3Type)
		{
			if (ES3TypeMgr.types == null)
			{
				ES3TypeMgr.Init();
			}
			ES3Type es3Type2 = ES3TypeMgr.GetES3Type(type);
			if (es3Type2 != null && es3Type2.priority > es3Type.priority)
			{
				return;
			}
			object @lock = ES3TypeMgr._lock;
			lock (@lock)
			{
				ES3TypeMgr.types[type] = es3Type;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001E704 File Offset: 0x0001C904
		internal static ES3Type CreateES3Type(Type type, bool throwException = true)
		{
			if (ES3Reflection.IsEnum(type))
			{
				return new ES3Type_enum(type);
			}
			ES3Type es3Type;
			if (ES3Reflection.TypeIsArray(type))
			{
				int arrayRank = ES3Reflection.GetArrayRank(type);
				if (arrayRank == 1)
				{
					es3Type = new ES3ArrayType(type);
				}
				else if (arrayRank == 2)
				{
					es3Type = new ES32DArrayType(type);
				}
				else if (arrayRank == 3)
				{
					es3Type = new ES33DArrayType(type);
				}
				else
				{
					if (throwException)
					{
						throw new NotSupportedException("Only arrays with up to three dimensions are supported by Easy Save.");
					}
					return null;
				}
			}
			else if (ES3Reflection.IsGenericType(type) && ES3Reflection.ImplementsInterface(type, typeof(IEnumerable)))
			{
				Type genericTypeDefinition = ES3Reflection.GetGenericTypeDefinition(type);
				if (typeof(List<>).IsAssignableFrom(genericTypeDefinition))
				{
					es3Type = new ES3ListType(type);
				}
				else if (typeof(Dictionary<, >).IsAssignableFrom(genericTypeDefinition))
				{
					es3Type = new ES3DictionaryType(type);
				}
				else if (genericTypeDefinition == typeof(Queue<>))
				{
					es3Type = new ES3QueueType(type);
				}
				else if (genericTypeDefinition == typeof(Stack<>))
				{
					es3Type = new ES3StackType(type);
				}
				else if (genericTypeDefinition == typeof(HashSet<>))
				{
					es3Type = new ES3HashSetType(type);
				}
				else if (genericTypeDefinition == typeof(NativeArray<>))
				{
					es3Type = new ES3NativeArrayType(type);
				}
				else if ((es3Type = ES3TypeMgr.GetES3Type(genericTypeDefinition)) != null)
				{
					es3Type = ES3TypeMgr.GetGenericES3Type(type, throwException);
				}
			}
			else if (ES3Reflection.IsPrimitive(type))
			{
				if (ES3TypeMgr.types == null || ES3TypeMgr.types.Count == 0)
				{
					throw new TypeLoadException("ES3Type for primitive could not be found, and the type list is empty. Please contact Easy Save developers at https://www.moodkie.com/contact");
				}
				throw new TypeLoadException("ES3Type for primitive could not be found, but the type list has been initialised and is not empty. Please contact Easy Save developers using the form at https://www.moodkie.com/contact.");
			}
			else if ((es3Type = ES3TypeMgr.GetGenericES3Type(type, false)) == null)
			{
				if (ES3Reflection.IsAssignableFrom(typeof(UnityEngine.Component), type))
				{
					es3Type = new ES3ReflectedComponentType(type);
				}
				else if (ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
				{
					es3Type = new ES3ReflectedScriptableObjectType(type);
				}
				else if (ES3Reflection.IsAssignableFrom(typeof(UnityEngine.Object), type))
				{
					es3Type = new ES3ReflectedUnityObjectType(type);
				}
				else if (ES3Reflection.IsValueType(type))
				{
					es3Type = new ES3ReflectedValueType(type);
				}
				else
				{
					es3Type = new ES3ReflectedObjectType(type);
				}
			}
			if (es3Type != null && !(es3Type.type == null) && !es3Type.isUnsupported)
			{
				ES3TypeMgr.Add(type, es3Type);
				return es3Type;
			}
			if (throwException)
			{
				throw new NotSupportedException(string.Format("ES3Type.type is null when trying to create an ES3Type for {0}, possibly because the element type is not supported.", type));
			}
			return null;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001E940 File Offset: 0x0001CB40
		private static ES3Type GetGenericES3Type(Type type, bool throwException)
		{
			if (!ES3Reflection.IsGenericType(type))
			{
				return null;
			}
			if (type.Name.StartsWith("Tuple`"))
			{
				return new ES3TupleType(type);
			}
			Type genericTypeDefinition = ES3Reflection.GetGenericTypeDefinition(type);
			ES3Type es3Type = ES3TypeMgr.GetES3Type(genericTypeDefinition);
			if (es3Type != null)
			{
				ConstructorInfo constructor = ES3Reflection.GetConstructor(es3Type.GetType(), new Type[]
				{
					typeof(Type)
				});
				if (!(constructor == null))
				{
					return (ES3Type)constructor.Invoke(new object[]
					{
						type
					});
				}
				if (throwException)
				{
					throw new NotSupportedException(string.Format("Generic type {0} is not supported by Easy Save as it's generic type definition {1} does not have a constructor which accepts a Type as a parameter.", type, genericTypeDefinition));
				}
				return null;
			}
			else
			{
				if (throwException)
				{
					throw new NotSupportedException("Generic type \"" + type.ToString() + "\" is not supported by Easy Save.");
				}
				return null;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		internal static void Init()
		{
			object @lock = ES3TypeMgr._lock;
			lock (@lock)
			{
				ES3TypeMgr.types = new Dictionary<Type, ES3Type>();
				ES3Reflection.GetInstances<ES3Type>();
				if (ES3TypeMgr.types == null || ES3TypeMgr.types.Count == 0)
				{
					throw new TypeLoadException("Type list could not be initialised. Please contact Easy Save developers on mail@moodkie.com.");
				}
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001EA60 File Offset: 0x0001CC60
		// Note: this type is marked as 'beforefieldinit'.
		static ES3TypeMgr()
		{
		}

		// Token: 0x04000161 RID: 353
		private static object _lock = new object();

		// Token: 0x04000162 RID: 354
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Dictionary<Type, ES3Type> types = null;

		// Token: 0x04000163 RID: 355
		private static ES3Type lastAccessedType = null;
	}
}
