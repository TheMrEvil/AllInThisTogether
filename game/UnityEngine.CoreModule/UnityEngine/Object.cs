using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000228 RID: 552
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Runtime/Export/Scripting/UnityEngineObject.bindings.h")]
	[NativeHeader("Runtime/GameCode/CloneObject.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x000263DC File Offset: 0x000245DC
		[SecuritySafeCritical]
		public unsafe int GetInstanceID()
		{
			bool flag = this.m_CachedPtr == IntPtr.Zero;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = Object.OffsetOfInstanceIDInCPlusPlusObject == -1;
				if (flag2)
				{
					Object.OffsetOfInstanceIDInCPlusPlusObject = Object.GetOffsetOfInstanceIDInCPlusPlusObject();
				}
				result = *(int*)((void*)new IntPtr(this.m_CachedPtr.ToInt64() + (long)Object.OffsetOfInstanceIDInCPlusPlusObject));
			}
			return result;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0002643C File Offset: 0x0002463C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00026454 File Offset: 0x00024654
		public override bool Equals(object other)
		{
			Object @object = other as Object;
			bool flag = @object == null && other != null && !(other is Object);
			return !flag && Object.CompareBaseObjects(this, @object);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00026498 File Offset: 0x00024698
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000264B4 File Offset: 0x000246B4
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool flag = lhs == null;
			bool flag2 = rhs == null;
			bool flag3 = flag2 && flag;
			bool result;
			if (flag3)
			{
				result = true;
			}
			else
			{
				bool flag4 = flag2;
				if (flag4)
				{
					result = !Object.IsNativeObjectAlive(lhs);
				}
				else
				{
					bool flag5 = flag;
					if (flag5)
					{
						result = !Object.IsNativeObjectAlive(rhs);
					}
					else
					{
						result = (lhs == rhs);
					}
				}
			}
			return result;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00026508 File Offset: 0x00024708
		private void EnsureRunningOnMainThread()
		{
			bool flag = !Object.CurrentThreadIsMainThread();
			if (flag)
			{
				throw new InvalidOperationException("EnsureRunningOnMainThread can only be called from the main thread");
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00026530 File Offset: 0x00024730
		private static bool IsNativeObjectAlive(Object o)
		{
			return o.GetCachedPtr() != IntPtr.Zero;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00026554 File Offset: 0x00024754
		private IntPtr GetCachedPtr()
		{
			return this.m_CachedPtr;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0002656C File Offset: 0x0002476C
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x00026584 File Offset: 0x00024784
		public string name
		{
			get
			{
				return Object.GetName(this);
			}
			set
			{
				Object.SetName(this, value);
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00026590 File Offset: 0x00024790
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = original is ScriptableObject;
			if (flag)
			{
				throw new ArgumentException("Cannot instantiate a ScriptableObject with a position and rotation");
			}
			Object @object = Object.Internal_InstantiateSingle(original, position, rotation);
			bool flag2 = @object == null;
			if (flag2)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000265E8 File Offset: 0x000247E8
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			bool flag = parent == null;
			Object result;
			if (flag)
			{
				result = Object.Instantiate(original, position, rotation);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object @object = Object.Internal_InstantiateSingleWithParent(original, parent, position, rotation);
				bool flag2 = @object == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				result = @object;
			}
			return result;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00026640 File Offset: 0x00024840
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Object.Internal_CloneSingle(original);
			bool flag = @object == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0002667C File Offset: 0x0002487C
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent)
		{
			return Object.Instantiate(original, parent, false);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00026698 File Offset: 0x00024898
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)
		{
			bool flag = parent == null;
			Object result;
			if (flag)
			{
				result = Object.Instantiate(original);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object @object = Object.Internal_CloneSingleWithParent(original, parent, instantiateInWorldSpace);
				bool flag2 = @object == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				result = @object;
			}
			return result;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000266EC File Offset: 0x000248EC
		public static T Instantiate<T>(T original) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T t = (T)((object)Object.Internal_CloneSingle(original));
			bool flag = t == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return t;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0002673C File Offset: 0x0002493C
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation));
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00026760 File Offset: 0x00024960
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation, parent));
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00026788 File Offset: 0x00024988
		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(original, parent, false);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000267A4 File Offset: 0x000249A4
		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return (T)((object)Object.Instantiate(original, parent, worldPositionStays));
		}

		// Token: 0x060017B1 RID: 6065
		[NativeMethod(Name = "Scripting::DestroyObjectFromScripting", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x060017B2 RID: 6066 RVA: 0x000267C8 File Offset: 0x000249C8
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		// Token: 0x060017B3 RID: 6067
		[NativeMethod(Name = "Scripting::DestroyObjectFromScriptingImmediate", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x060017B4 RID: 6068 RVA: 0x000267E4 File Offset: 0x000249E4
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000267FC File Offset: 0x000249FC
		public static Object[] FindObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type, false);
		}

		// Token: 0x060017B6 RID: 6070
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType")]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfType(Type type, bool includeInactive);

		// Token: 0x060017B7 RID: 6071 RVA: 0x00026818 File Offset: 0x00024A18
		public static Object[] FindObjectsByType(Type type, FindObjectsSortMode sortMode)
		{
			return Object.FindObjectsByType(type, FindObjectsInactive.Exclude, sortMode);
		}

		// Token: 0x060017B8 RID: 6072
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsByType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsByType(Type type, FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode);

		// Token: 0x060017B9 RID: 6073
		[FreeFunction("GetSceneManager().DontDestroyOnLoad", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad([NotNull("NullExceptionObject")] Object target);

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060017BA RID: 6074
		// (set) Token: 0x060017BB RID: 6075
		public extern HideFlags hideFlags { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060017BC RID: 6076 RVA: 0x00026832 File Offset: 0x00024A32
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy(obj, t);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00026840 File Offset: 0x00024A40
		[Obsolete("use Object.Destroy instead.")]
		[ExcludeFromDocs]
		public static void DestroyObject(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0002685C File Offset: 0x00024A5C
		[Obsolete("warning use Object.FindObjectsByType instead.")]
		public static Object[] FindSceneObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type);
		}

		// Token: 0x060017BF RID: 6079
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfTypeIncludingAssets")]
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		// Token: 0x060017C0 RID: 6080 RVA: 0x00026874 File Offset: 0x00024A74
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), false));
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0002689C File Offset: 0x00024A9C
		public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsByType(typeof(T), FindObjectsInactive.Exclude, sortMode));
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000268C4 File Offset: 0x00024AC4
		public static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), includeInactive));
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000268EC File Offset: 0x00024AEC
		public static T[] FindObjectsByType<T>(FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsByType(typeof(T), findObjectsInactive, sortMode));
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00026914 File Offset: 0x00024B14
		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), false));
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0002693C File Offset: 0x00024B3C
		public static T FindObjectOfType<T>(bool includeInactive) where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), includeInactive));
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00026964 File Offset: 0x00024B64
		public static T FindFirstObjectByType<T>() where T : Object
		{
			return (T)((object)Object.FindFirstObjectByType(typeof(T), FindObjectsInactive.Exclude));
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0002698C File Offset: 0x00024B8C
		public static T FindAnyObjectByType<T>() where T : Object
		{
			return (T)((object)Object.FindAnyObjectByType(typeof(T), FindObjectsInactive.Exclude));
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000269B4 File Offset: 0x00024BB4
		public static T FindFirstObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
		{
			return (T)((object)Object.FindFirstObjectByType(typeof(T), findObjectsInactive));
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000269DC File Offset: 0x00024BDC
		public static T FindAnyObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
		{
			return (T)((object)Object.FindAnyObjectByType(typeof(T), findObjectsInactive));
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00026A04 File Offset: 0x00024C04
		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00026A1C File Offset: 0x00024C1C
		private static void CheckNullArgument(object arg, string message)
		{
			bool flag = arg == null;
			if (flag)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00026A3C File Offset: 0x00024C3C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type, false);
			bool flag = array.Length != 0;
			Object result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00026A68 File Offset: 0x00024C68
		public static Object FindFirstObjectByType(Type type)
		{
			Object[] array = Object.FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00026A90 File Offset: 0x00024C90
		public static Object FindAnyObjectByType(Type type)
		{
			Object[] array = Object.FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00026AB8 File Offset: 0x00024CB8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type, bool includeInactive)
		{
			Object[] array = Object.FindObjectsOfType(type, includeInactive);
			bool flag = array.Length != 0;
			Object result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00026AE4 File Offset: 0x00024CE4
		public static Object FindFirstObjectByType(Type type, FindObjectsInactive findObjectsInactive)
		{
			Object[] array = Object.FindObjectsByType(type, findObjectsInactive, FindObjectsSortMode.InstanceID);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00026B0C File Offset: 0x00024D0C
		public static Object FindAnyObjectByType(Type type, FindObjectsInactive findObjectsInactive)
		{
			Object[] array = Object.FindObjectsByType(type, findObjectsInactive, FindObjectsSortMode.None);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00026B34 File Offset: 0x00024D34
		public override string ToString()
		{
			return Object.ToString(this);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00026B4C File Offset: 0x00024D4C
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00026B68 File Offset: 0x00024D68
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x060017D5 RID: 6101
		[NativeMethod(Name = "Object::GetOffsetOfInstanceIdMember", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOffsetOfInstanceIDInCPlusPlusObject();

		// Token: 0x060017D6 RID: 6102
		[NativeMethod(Name = "CurrentThreadIsMainThread", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CurrentThreadIsMainThread();

		// Token: 0x060017D7 RID: 6103
		[NativeMethod(Name = "CloneObject", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingle([NotNull("NullExceptionObject")] Object data);

		// Token: 0x060017D8 RID: 6104
		[FreeFunction("CloneObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingleWithParent([NotNull("NullExceptionObject")] Object data, [NotNull("NullExceptionObject")] Transform parent, bool worldPositionStays);

		// Token: 0x060017D9 RID: 6105 RVA: 0x00026B84 File Offset: 0x00024D84
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingle([NotNull("NullExceptionObject")] Object data, Vector3 pos, Quaternion rot)
		{
			return Object.Internal_InstantiateSingle_Injected(data, ref pos, ref rot);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00026B90 File Offset: 0x00024D90
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingleWithParent([NotNull("NullExceptionObject")] Object data, [NotNull("NullExceptionObject")] Transform parent, Vector3 pos, Quaternion rot)
		{
			return Object.Internal_InstantiateSingleWithParent_Injected(data, parent, ref pos, ref rot);
		}

		// Token: 0x060017DB RID: 6107
		[FreeFunction("UnityEngineObjectBindings::ToString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ToString(Object obj);

		// Token: 0x060017DC RID: 6108
		[FreeFunction("UnityEngineObjectBindings::GetName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName([NotNull("NullExceptionObject")] Object obj);

		// Token: 0x060017DD RID: 6109
		[FreeFunction("UnityEngineObjectBindings::IsPersistent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsPersistent([NotNull("NullExceptionObject")] Object obj);

		// Token: 0x060017DE RID: 6110
		[FreeFunction("UnityEngineObjectBindings::SetName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName([NotNull("NullExceptionObject")] Object obj, string name);

		// Token: 0x060017DF RID: 6111
		[NativeMethod(Name = "UnityEngineObjectBindings::DoesObjectWithInstanceIDExist", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		// Token: 0x060017E0 RID: 6112
		[VisibleToOtherModules]
		[FreeFunction("UnityEngineObjectBindings::FindObjectFromInstanceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object FindObjectFromInstanceID(int instanceID);

		// Token: 0x060017E1 RID: 6113
		[FreeFunction("UnityEngineObjectBindings::ForceLoadFromInstanceID")]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object ForceLoadFromInstanceID(int instanceID);

		// Token: 0x060017E2 RID: 6114 RVA: 0x00002072 File Offset: 0x00000272
		public Object()
		{
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00026B9D File Offset: 0x00024D9D
		// Note: this type is marked as 'beforefieldinit'.
		static Object()
		{
		}

		// Token: 0x060017E4 RID: 6116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingle_Injected(Object data, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x060017E5 RID: 6117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingleWithParent_Injected(Object data, Transform parent, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x04000829 RID: 2089
		private IntPtr m_CachedPtr;

		// Token: 0x0400082A RID: 2090
		internal static int OffsetOfInstanceIDInCPlusPlusObject = -1;

		// Token: 0x0400082B RID: 2091
		private const string objectIsNullMessage = "The Object you want to instantiate is null.";

		// Token: 0x0400082C RID: 2092
		private const string cloneDestroyedMessage = "Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.";
	}
}
