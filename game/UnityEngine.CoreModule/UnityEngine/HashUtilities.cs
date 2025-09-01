using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x020001AF RID: 431
	public static class HashUtilities
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x0001A130 File Offset: 0x00018330
		public unsafe static void AppendHash(ref Hash128 inHash, ref Hash128 outHash)
		{
			fixed (Hash128* ptr = &outHash)
			{
				Hash128* hash = ptr;
				fixed (Hash128* ptr2 = &inHash)
				{
					Hash128* data = ptr2;
					HashUnsafeUtilities.ComputeHash128((void*)data, (ulong)((long)sizeof(Hash128)), hash);
				}
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0001A164 File Offset: 0x00018364
		public unsafe static void QuantisedMatrixHash(ref Matrix4x4 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* hash2 = ptr;
				int* ptr2 = stackalloc int[(UIntPtr)64];
				for (int i = 0; i < 16; i++)
				{
					ptr2[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr2, 64UL, hash2);
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public unsafe static void QuantisedVectorHash(ref Vector3 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* hash2 = ptr;
				int* ptr2 = stackalloc int[(UIntPtr)12];
				for (int i = 0; i < 3; i++)
				{
					ptr2[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr2, 12UL, hash2);
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0001A218 File Offset: 0x00018418
		public unsafe static void ComputeHash128<T>(ref T value, ref Hash128 hash) where T : struct
		{
			void* data = UnsafeUtility.AddressOf<T>(ref value);
			ulong dataSize = (ulong)((long)UnsafeUtility.SizeOf<T>());
			Hash128* hash2 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
			HashUnsafeUtilities.ComputeHash128(data, dataSize, hash2);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0001A244 File Offset: 0x00018444
		public unsafe static void ComputeHash128(byte[] value, ref Hash128 hash)
		{
			fixed (byte* ptr = &value[0])
			{
				byte* data = ptr;
				ulong dataSize = (ulong)((long)value.Length);
				Hash128* hash2 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
				HashUnsafeUtilities.ComputeHash128((void*)data, dataSize, hash2);
			}
		}
	}
}
