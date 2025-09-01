using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001EB RID: 491
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	public sealed class Resources
	{
		// Token: 0x06001630 RID: 5680 RVA: 0x00023858 File Offset: 0x00021A58
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			bool flag = rawObjects == null;
			T[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				T[] array = new T[rawObjects.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (T)((object)rawObjects[i]);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000238A4 File Offset: 0x00021AA4
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return ResourcesAPI.ActiveAPI.FindObjectsOfTypeAll(type);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000238C4 File Offset: 0x00021AC4
		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000238EC File Offset: 0x00021AEC
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00023910 File Offset: 0x00021B10
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00023938 File Offset: 0x00021B38
		public static Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.Load(path, systemTypeInstance);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00023958 File Offset: 0x00021B58
		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path, typeof(Object));
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0002397C File Offset: 0x00021B7C
		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return Resources.LoadAsync(path, typeof(T));
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000239A0 File Offset: 0x00021BA0
		public static ResourceRequest LoadAsync(string path, Type type)
		{
			return ResourcesAPI.ActiveAPI.LoadAsync(path, type);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000239C0 File Offset: 0x00021BC0
		public static Object[] LoadAll(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.LoadAll(path, systemTypeInstance);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000239E0 File Offset: 0x00021BE0
		public static Object[] LoadAll(string path)
		{
			return Resources.LoadAll(path, typeof(Object));
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00023A04 File Offset: 0x00021C04
		public static T[] LoadAll<T>(string path) where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.LoadAll(path, typeof(T)));
		}

		// Token: 0x0600163C RID: 5692
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction("GetScriptingBuiltinResource", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetBuiltinResource([NotNull("ArgumentNullException")] Type type, string path);

		// Token: 0x0600163D RID: 5693 RVA: 0x00023A2C File Offset: 0x00021C2C
		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00023A53 File Offset: 0x00021C53
		public static void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPI.ActiveAPI.UnloadAsset(assetToUnload);
		}

		// Token: 0x0600163F RID: 5695
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnloadAssetImplResourceManager(Object assetToUnload);

		// Token: 0x06001640 RID: 5696
		[FreeFunction("Resources_Bindings::UnloadUnusedAssets")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation UnloadUnusedAssets();

		// Token: 0x06001641 RID: 5697
		[FreeFunction("Resources_Bindings::InstanceIDToObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object InstanceIDToObject(int instanceID);

		// Token: 0x06001642 RID: 5698
		[FreeFunction("Resources_Bindings::InstanceIDToObjectList")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InstanceIDToObjectList(IntPtr instanceIDs, int instanceCount, List<Object> objects);

		// Token: 0x06001643 RID: 5699 RVA: 0x00023A64 File Offset: 0x00021C64
		public static void InstanceIDToObjectList(NativeArray<int> instanceIDs, List<Object> objects)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = objects == null;
			if (flag2)
			{
				throw new ArgumentNullException("objects");
			}
			bool flag3 = instanceIDs.Length == 0;
			if (flag3)
			{
				objects.Clear();
			}
			else
			{
				Resources.InstanceIDToObjectList((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, objects);
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00002072 File Offset: 0x00000272
		public Resources()
		{
		}
	}
}
