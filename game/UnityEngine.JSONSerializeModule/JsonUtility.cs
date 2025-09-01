using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/JSONSerialize/Public/JsonUtility.bindings.h")]
	public static class JsonUtility
	{
		// Token: 0x06000001 RID: 1
		[FreeFunction("ToJsonInternal", true)]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ToJsonInternal([NotNull("ArgumentNullException")] object obj, bool prettyPrint);

		// Token: 0x06000002 RID: 2
		[FreeFunction("FromJsonInternal", true, ThrowsException = true)]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object FromJsonInternal(string json, object objectToOverwrite, Type type);

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		public static string ToJson(object obj)
		{
			return JsonUtility.ToJson(obj, false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
		public static string ToJson(object obj, bool prettyPrint)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = obj is Object && !(obj is MonoBehaviour) && !(obj is ScriptableObject);
				if (flag2)
				{
					throw new ArgumentException("JsonUtility.ToJson does not support engine types.");
				}
				result = JsonUtility.ToJsonInternal(obj, prettyPrint);
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C8 File Offset: 0x000002C8
		public static T FromJson<T>(string json)
		{
			return (T)((object)JsonUtility.FromJson(json, typeof(T)));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F0 File Offset: 0x000002F0
		public static object FromJson(string json, Type type)
		{
			bool flag = string.IsNullOrEmpty(json);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = type == null;
				if (flag2)
				{
					throw new ArgumentNullException("type");
				}
				bool flag3 = type.IsAbstract || type.IsSubclassOf(typeof(Object));
				if (flag3)
				{
					throw new ArgumentException("Cannot deserialize JSON to new instances of type '" + type.Name + ".'");
				}
				result = JsonUtility.FromJsonInternal(json, null, type);
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002168 File Offset: 0x00000368
		public static void FromJsonOverwrite(string json, object objectToOverwrite)
		{
			bool flag = string.IsNullOrEmpty(json);
			if (!flag)
			{
				bool flag2 = objectToOverwrite == null;
				if (flag2)
				{
					throw new ArgumentNullException("objectToOverwrite");
				}
				bool flag3 = objectToOverwrite is Object && !(objectToOverwrite is MonoBehaviour) && !(objectToOverwrite is ScriptableObject);
				if (flag3)
				{
					throw new ArgumentException("Engine types cannot be overwritten from JSON outside of the Editor.");
				}
				JsonUtility.FromJsonInternal(json, objectToOverwrite, objectToOverwrite.GetType());
			}
		}
	}
}
