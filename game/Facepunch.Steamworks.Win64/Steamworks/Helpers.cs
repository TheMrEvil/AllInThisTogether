using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x020000B7 RID: 183
	internal static class Helpers
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00012104 File Offset: 0x00010304
		public unsafe static IntPtr TakeMemory()
		{
			bool flag = Helpers.MemoryPool == null;
			if (flag)
			{
				Helpers.MemoryPool = new IntPtr[5];
				for (int i = 0; i < Helpers.MemoryPool.Length; i++)
				{
					Helpers.MemoryPool[i] = Marshal.AllocHGlobal(32768);
				}
			}
			Helpers.MemoryPoolIndex++;
			bool flag2 = Helpers.MemoryPoolIndex >= Helpers.MemoryPool.Length;
			if (flag2)
			{
				Helpers.MemoryPoolIndex = 0;
			}
			IntPtr intPtr = Helpers.MemoryPool[Helpers.MemoryPoolIndex];
			*(byte*)((void*)intPtr) = 0;
			return intPtr;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00012198 File Offset: 0x00010398
		public static byte[] TakeBuffer(int minSize)
		{
			bool flag = Helpers.BufferPool == null;
			if (flag)
			{
				Helpers.BufferPool = new byte[8][];
				for (int i = 0; i < Helpers.BufferPool.Length; i++)
				{
					Helpers.BufferPool[i] = new byte[131072];
				}
			}
			Helpers.BufferPoolIndex++;
			bool flag2 = Helpers.BufferPoolIndex >= Helpers.BufferPool.Length;
			if (flag2)
			{
				Helpers.BufferPoolIndex = 0;
			}
			bool flag3 = Helpers.BufferPool[Helpers.BufferPoolIndex].Length < minSize;
			if (flag3)
			{
				Helpers.BufferPool[Helpers.BufferPoolIndex] = new byte[minSize + 1024];
			}
			return Helpers.BufferPool[Helpers.BufferPoolIndex];
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00012250 File Offset: 0x00010450
		internal unsafe static string MemoryToString(IntPtr ptr)
		{
			int i;
			for (i = 0; i < 32768; i++)
			{
				bool flag = ((byte*)((void*)ptr))[i] == 0;
				if (flag)
				{
					break;
				}
			}
			bool flag2 = i == 0;
			string result;
			if (flag2)
			{
				result = string.Empty;
			}
			else
			{
				result = Encoding.UTF8.GetString((byte*)((void*)ptr), i);
			}
			return result;
		}

		// Token: 0x0400076E RID: 1902
		public const int MaxStringSize = 32768;

		// Token: 0x0400076F RID: 1903
		private static IntPtr[] MemoryPool;

		// Token: 0x04000770 RID: 1904
		private static int MemoryPoolIndex;

		// Token: 0x04000771 RID: 1905
		private static byte[][] BufferPool;

		// Token: 0x04000772 RID: 1906
		private static int BufferPoolIndex;
	}
}
