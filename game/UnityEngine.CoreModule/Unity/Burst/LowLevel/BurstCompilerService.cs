using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Burst.LowLevel
{
	// Token: 0x020000B2 RID: 178
	[NativeHeader("Runtime/Burst/Burst.h")]
	[NativeHeader("Runtime/Burst/BurstDelegateCache.h")]
	[StaticAccessor("BurstCompilerService::Get()", StaticAccessorType.Arrow)]
	internal static class BurstCompilerService
	{
		// Token: 0x0600032D RID: 813
		[NativeMethod("Initialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InitializeInternal(string path, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags);

		// Token: 0x0600032E RID: 814
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetDisassembly(MethodInfo m, string compilerOptions);

		// Token: 0x0600032F RID: 815
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompileAsyncDelegateMethod(object delegateMethod, string compilerOptions);

		// Token: 0x06000330 RID: 816
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetAsyncCompiledAsyncDelegateMethod(int userID);

		// Token: 0x06000331 RID: 817
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetOrCreateSharedMemory(ref Hash128 key, uint size_of, uint alignment);

		// Token: 0x06000332 RID: 818
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetMethodSignature(MethodInfo method);

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000333 RID: 819
		public static extern bool IsInitialized { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000334 RID: 820
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCurrentExecutionMode(uint environment);

		// Token: 0x06000335 RID: 821
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetCurrentExecutionMode();

		// Token: 0x06000336 RID: 822
		[FreeFunction("DefaultBurstLogCallback", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Log(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		// Token: 0x06000337 RID: 823
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool LoadBurstLibrary(string fullPathToLibBurstGenerated);

		// Token: 0x06000338 RID: 824 RVA: 0x00005C14 File Offset: 0x00003E14
		public static void Initialize(string folderRuntime, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags)
		{
			bool flag = folderRuntime == null;
			if (flag)
			{
				throw new ArgumentNullException("folderRuntime");
			}
			bool flag2 = extractCompilerFlags == null;
			if (flag2)
			{
				throw new ArgumentNullException("extractCompilerFlags");
			}
			bool flag3 = !Directory.Exists(folderRuntime);
			if (flag3)
			{
				Debug.LogError("Unable to initialize the burst JIT compiler. The folder `" + folderRuntime + "` does not exist");
			}
			else
			{
				string text = BurstCompilerService.InitializeInternal(folderRuntime, extractCompilerFlags);
				bool flag4 = !string.IsNullOrEmpty(text);
				if (flag4)
				{
					Debug.LogError("Unexpected error while trying to initialize the burst JIT compiler: " + text);
				}
			}
		}

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x0600033A RID: 826
		public delegate bool ExtractCompilerFlags(Type jobType, out string flags);

		// Token: 0x020000B4 RID: 180
		public enum BurstLogType
		{
			// Token: 0x04000240 RID: 576
			Info,
			// Token: 0x04000241 RID: 577
			Warning,
			// Token: 0x04000242 RID: 578
			Error
		}
	}
}
