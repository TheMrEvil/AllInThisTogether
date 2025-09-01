using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromMemoryAsyncOperation.h")]
	[ExcludeFromPreset]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[NativeHeader("Runtime/Scripting/ScriptingExportUtility.h")]
	[NativeHeader("Runtime/Scripting/ScriptingObjectWithIntPtrField.h")]
	[NativeHeader("Runtime/Scripting/ScriptingUtility.h")]
	[NativeHeader("AssetBundleScriptingClasses.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleSaveAndLoadHelper.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromFileAsyncOperation.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromManagedStreamAsyncOperation.h")]
	public class AssetBundle : Object
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private AssetBundle()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		[Obsolete("mainAsset has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
		public Object mainAsset
		{
			get
			{
				return AssetBundle.returnMainAsset(this);
			}
		}

		// Token: 0x06000003 RID: 3
		[FreeFunction("LoadMainObjectFromAssetBundle", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object returnMainAsset([NotNull("NullExceptionObject")] AssetBundle bundle);

		// Token: 0x06000004 RID: 4
		[FreeFunction("UnloadAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnloadAllAssetBundles(bool unloadAllObjects);

		// Token: 0x06000005 RID: 5
		[FreeFunction("GetAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundle[] GetAllLoadedAssetBundles_Native();

		// Token: 0x06000006 RID: 6 RVA: 0x00002074 File Offset: 0x00000274
		public static IEnumerable<AssetBundle> GetAllLoadedAssetBundles()
		{
			return AssetBundle.GetAllLoadedAssetBundles_Native();
		}

		// Token: 0x06000007 RID: 7
		[FreeFunction("LoadFromFileAsync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundleCreateRequest LoadFromFileAsync_Internal(string path, uint crc, ulong offset);

		// Token: 0x06000008 RID: 8 RVA: 0x0000208C File Offset: 0x0000028C
		public static AssetBundleCreateRequest LoadFromFileAsync(string path)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, 0U, 0UL);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020A8 File Offset: 0x000002A8
		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, crc, 0UL);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020C4 File Offset: 0x000002C4
		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc, ulong offset)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, crc, offset);
		}

		// Token: 0x0600000B RID: 11
		[FreeFunction("LoadFromFile")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundle LoadFromFile_Internal(string path, uint crc, ulong offset);

		// Token: 0x0600000C RID: 12 RVA: 0x000020E0 File Offset: 0x000002E0
		public static AssetBundle LoadFromFile(string path)
		{
			return AssetBundle.LoadFromFile_Internal(path, 0U, 0UL);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020FC File Offset: 0x000002FC
		public static AssetBundle LoadFromFile(string path, uint crc)
		{
			return AssetBundle.LoadFromFile_Internal(path, crc, 0UL);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002118 File Offset: 0x00000318
		public static AssetBundle LoadFromFile(string path, uint crc, ulong offset)
		{
			return AssetBundle.LoadFromFile_Internal(path, crc, offset);
		}

		// Token: 0x0600000F RID: 15
		[FreeFunction("LoadFromMemoryAsync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundleCreateRequest LoadFromMemoryAsync_Internal(byte[] binary, uint crc);

		// Token: 0x06000010 RID: 16 RVA: 0x00002134 File Offset: 0x00000334
		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary)
		{
			return AssetBundle.LoadFromMemoryAsync_Internal(binary, 0U);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002150 File Offset: 0x00000350
		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary, uint crc)
		{
			return AssetBundle.LoadFromMemoryAsync_Internal(binary, crc);
		}

		// Token: 0x06000012 RID: 18
		[FreeFunction("LoadFromMemory")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundle LoadFromMemory_Internal(byte[] binary, uint crc);

		// Token: 0x06000013 RID: 19 RVA: 0x0000216C File Offset: 0x0000036C
		public static AssetBundle LoadFromMemory(byte[] binary)
		{
			return AssetBundle.LoadFromMemory_Internal(binary, 0U);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002188 File Offset: 0x00000388
		public static AssetBundle LoadFromMemory(byte[] binary, uint crc)
		{
			return AssetBundle.LoadFromMemory_Internal(binary, crc);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021A4 File Offset: 0x000003A4
		internal static void ValidateLoadFromStream(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			bool flag2 = !stream.CanRead;
			if (flag2)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			bool flag3 = !stream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002204 File Offset: 0x00000404
		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc, uint managedReadBufferSize)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamAsyncInternal(stream, crc, managedReadBufferSize);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002228 File Offset: 0x00000428
		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamAsyncInternal(stream, crc, 0U);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000224C File Offset: 0x0000044C
		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamAsyncInternal(stream, 0U, 0U);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002270 File Offset: 0x00000470
		public static AssetBundle LoadFromStream(Stream stream, uint crc, uint managedReadBufferSize)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamInternal(stream, crc, managedReadBufferSize);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002294 File Offset: 0x00000494
		public static AssetBundle LoadFromStream(Stream stream, uint crc)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamInternal(stream, crc, 0U);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022B8 File Offset: 0x000004B8
		public static AssetBundle LoadFromStream(Stream stream)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamInternal(stream, 0U, 0U);
		}

		// Token: 0x0600001C RID: 28
		[FreeFunction("LoadFromStreamAsyncInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundleCreateRequest LoadFromStreamAsyncInternal(Stream stream, uint crc, uint managedReadBufferSize);

		// Token: 0x0600001D RID: 29
		[FreeFunction("LoadFromStreamInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundle LoadFromStreamInternal(Stream stream, uint crc, uint managedReadBufferSize);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30
		public extern bool isStreamedSceneAssetBundle { [NativeMethod("GetIsStreamedSceneAssetBundle")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600001F RID: 31
		[NativeMethod("Contains")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Contains(string name);

		// Token: 0x06000020 RID: 32 RVA: 0x000022DC File Offset: 0x000004DC
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Object Load(string name)
		{
			return null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022F0 File Offset: 0x000004F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		public Object Load<T>(string name)
		{
			return null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002304 File Offset: 0x00000504
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		private Object Load(string name, Type type)
		{
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002318 File Offset: 0x00000518
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAsync has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAssetAsync instead and check the documentation for details.", true)]
		private AssetBundleRequest LoadAsync(string name, Type type)
		{
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000232C File Offset: 0x0000052C
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private Object[] LoadAll(Type type)
		{
			return null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002340 File Offset: 0x00000540
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public Object[] LoadAll()
		{
			return null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002354 File Offset: 0x00000554
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public T[] LoadAll<T>() where T : Object
		{
			return null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002368 File Offset: 0x00000568
		public Object LoadAsset(string name)
		{
			return this.LoadAsset(name, typeof(Object));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000238C File Offset: 0x0000058C
		public T LoadAsset<T>(string name) where T : Object
		{
			return (T)((object)this.LoadAsset(name, typeof(T)));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023B4 File Offset: 0x000005B4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public Object LoadAsset(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAsset_Internal(name, type);
		}

		// Token: 0x0600002A RID: 42
		[NativeThrows]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeMethod("LoadAsset_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object LoadAsset_Internal(string name, Type type);

		// Token: 0x0600002B RID: 43 RVA: 0x00002410 File Offset: 0x00000610
		public AssetBundleRequest LoadAssetAsync(string name)
		{
			return this.LoadAssetAsync(name, typeof(Object));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002434 File Offset: 0x00000634
		public AssetBundleRequest LoadAssetAsync<T>(string name)
		{
			return this.LoadAssetAsync(name, typeof(T));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002458 File Offset: 0x00000658
		public AssetBundleRequest LoadAssetAsync(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetAsync_Internal(name, type);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024B4 File Offset: 0x000006B4
		public Object[] LoadAssetWithSubAssets(string name)
		{
			return this.LoadAssetWithSubAssets(name, typeof(Object));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024D8 File Offset: 0x000006D8
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

		// Token: 0x06000030 RID: 48 RVA: 0x00002524 File Offset: 0x00000724
		public T[] LoadAssetWithSubAssets<T>(string name) where T : Object
		{
			return AssetBundle.ConvertObjects<T>(this.LoadAssetWithSubAssets(name, typeof(T)));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000254C File Offset: 0x0000074C
		public Object[] LoadAssetWithSubAssets(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal(name, type);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025A8 File Offset: 0x000007A8
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name)
		{
			return this.LoadAssetWithSubAssetsAsync(name, typeof(Object));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025CC File Offset: 0x000007CC
		public AssetBundleRequest LoadAssetWithSubAssetsAsync<T>(string name)
		{
			return this.LoadAssetWithSubAssetsAsync(name, typeof(T));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025F0 File Offset: 0x000007F0
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal(name, type);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000264C File Offset: 0x0000084C
		public Object[] LoadAllAssets()
		{
			return this.LoadAllAssets(typeof(Object));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002670 File Offset: 0x00000870
		public T[] LoadAllAssets<T>() where T : Object
		{
			return AssetBundle.ConvertObjects<T>(this.LoadAllAssets(typeof(T)));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002698 File Offset: 0x00000898
		public Object[] LoadAllAssets(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal("", type);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026CC File Offset: 0x000008CC
		public AssetBundleRequest LoadAllAssetsAsync()
		{
			return this.LoadAllAssetsAsync(typeof(Object));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026F0 File Offset: 0x000008F0
		public AssetBundleRequest LoadAllAssetsAsync<T>()
		{
			return this.LoadAllAssetsAsync(typeof(T));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002714 File Offset: 0x00000914
		public AssetBundleRequest LoadAllAssetsAsync(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal("", type);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002748 File Offset: 0x00000948
		[Obsolete("This method is deprecated.Use GetAllAssetNames() instead.", false)]
		public string[] AllAssetNames()
		{
			return this.GetAllAssetNames();
		}

		// Token: 0x0600003C RID: 60
		[NativeMethod("LoadAssetAsync_Internal")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AssetBundleRequest LoadAssetAsync_Internal(string name, Type type);

		// Token: 0x0600003D RID: 61
		[NativeMethod("Unload")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Unload(bool unloadAllLoadedObjects);

		// Token: 0x0600003E RID: 62
		[NativeMethod("UnloadAsync")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AsyncOperation UnloadAsync(bool unloadAllLoadedObjects);

		// Token: 0x0600003F RID: 63
		[NativeMethod("GetAllAssetNames")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllAssetNames();

		// Token: 0x06000040 RID: 64
		[NativeMethod("GetAllScenePaths")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetAllScenePaths();

		// Token: 0x06000041 RID: 65
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssets_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Object[] LoadAssetWithSubAssets_Internal(string name, Type type);

		// Token: 0x06000042 RID: 66
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssetsAsync_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AssetBundleRequest LoadAssetWithSubAssetsAsync_Internal(string name, Type type);

		// Token: 0x06000043 RID: 67 RVA: 0x00002760 File Offset: 0x00000960
		public static AssetBundleRecompressOperation RecompressAssetBundleAsync(string inputPath, string outputPath, BuildCompression method, uint expectedCRC = 0U, ThreadPriority priority = ThreadPriority.Low)
		{
			return AssetBundle.RecompressAssetBundleAsync_Internal(inputPath, outputPath, method, expectedCRC, priority);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000277D File Offset: 0x0000097D
		[FreeFunction("RecompressAssetBundleAsync_Internal")]
		[NativeThrows]
		internal static AssetBundleRecompressOperation RecompressAssetBundleAsync_Internal(string inputPath, string outputPath, BuildCompression method, uint expectedCRC, ThreadPriority priority)
		{
			return AssetBundle.RecompressAssetBundleAsync_Internal_Injected(inputPath, outputPath, ref method, expectedCRC, priority);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000278C File Offset: 0x0000098C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000027A3 File Offset: 0x000009A3
		public static uint memoryBudgetKB
		{
			get
			{
				return AssetBundleLoadingCache.memoryBudgetKB;
			}
			set
			{
				AssetBundleLoadingCache.memoryBudgetKB = value;
			}
		}

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AssetBundleRecompressOperation RecompressAssetBundleAsync_Internal_Injected(string inputPath, string outputPath, ref BuildCompression method, uint expectedCRC, ThreadPriority priority);
	}
}
