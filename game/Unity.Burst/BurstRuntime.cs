using System;
using System.Diagnostics;
using Unity.Burst.LowLevel;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000010 RID: 16
	public static class BurstRuntime
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002E43 File Offset: 0x00001043
		public static int GetHashCode32<T>()
		{
			return BurstRuntime.HashCode32<T>.Value;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E4A File Offset: 0x0000104A
		public static int GetHashCode32(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A32(type.AssemblyQualifiedName);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E57 File Offset: 0x00001057
		public static long GetHashCode64<T>()
		{
			return BurstRuntime.HashCode64<T>.Value;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E5E File Offset: 0x0000105E
		public static long GetHashCode64(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A64(type.AssemblyQualifiedName);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E6C File Offset: 0x0000106C
		internal static int HashStringWithFNV1A32(string text)
		{
			uint num = 2166136261U;
			foreach (char c in text)
			{
				num = 16777619U * (num ^ (uint)((byte)(c & 'ÿ')));
				num = 16777619U * (num ^ (uint)((byte)(c >> 8)));
			}
			return (int)num;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EBC File Offset: 0x000010BC
		internal static long HashStringWithFNV1A64(string text)
		{
			ulong num = 14695981039346656037UL;
			foreach (char c in text)
			{
				num = 1099511628211UL * (num ^ (ulong)((byte)(c & 'ÿ')));
				num = 1099511628211UL * (num ^ (ulong)((byte)(c >> 8)));
			}
			return (long)num;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F17 File Offset: 0x00001117
		public static bool LoadAdditionalLibrary(string pathToLibBurstGenerated)
		{
			return BurstCompiler.IsLoadAdditionalLibrarySupported() && BurstRuntime.LoadAdditionalLibraryInternal(pathToLibBurstGenerated);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F28 File Offset: 0x00001128
		internal static bool LoadAdditionalLibraryInternal(string pathToLibBurstGenerated)
		{
			return (bool)typeof(BurstCompilerService).GetMethod("LoadBurstLibrary").Invoke(null, new object[]
			{
				pathToLibBurstGenerated
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F53 File Offset: 0x00001153
		internal static void Initialize()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F55 File Offset: 0x00001155
		[BurstRuntime.PreserveAttribute]
		internal static void PreventRequiredAttributeStrip()
		{
			new BurstDiscardAttribute();
			new ConditionalAttribute("HEJSA");
			new JobProducerTypeAttribute(typeof(BurstRuntime));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F78 File Offset: 0x00001178
		[BurstRuntime.PreserveAttribute]
		internal unsafe static void Log(byte* message, int logType, byte* fileName, int lineNumber)
		{
			BurstCompilerService.Log(null, (BurstCompilerService.BurstLogType)logType, message, null, lineNumber);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F86 File Offset: 0x00001186
		public unsafe static byte* GetUTF8LiteralPointer(string str, out int byteCount)
		{
			throw new NotImplementedException("This function only works from Burst");
		}

		// Token: 0x0200002F RID: 47
		private struct HashCode32<T>
		{
			// Token: 0x0600014C RID: 332 RVA: 0x00007B35 File Offset: 0x00005D35
			// Note: this type is marked as 'beforefieldinit'.
			static HashCode32()
			{
			}

			// Token: 0x04000244 RID: 580
			public static readonly int Value = BurstRuntime.HashStringWithFNV1A32(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x02000030 RID: 48
		private struct HashCode64<T>
		{
			// Token: 0x0600014D RID: 333 RVA: 0x00007B50 File Offset: 0x00005D50
			// Note: this type is marked as 'beforefieldinit'.
			static HashCode64()
			{
			}

			// Token: 0x04000245 RID: 581
			public static readonly long Value = BurstRuntime.HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x02000031 RID: 49
		internal class PreserveAttribute : Attribute
		{
			// Token: 0x0600014E RID: 334 RVA: 0x00007B6B File Offset: 0x00005D6B
			public PreserveAttribute()
			{
			}
		}
	}
}
