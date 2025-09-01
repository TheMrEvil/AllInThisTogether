using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe.NotBurstCompatible
{
	// Token: 0x02000157 RID: 343
	public static class Extensions
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00024328 File Offset: 0x00022528
		public static T[] ToArray<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> set) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> nativeArray = set.ToNativeArray(Allocator.TempJob);
			T[] result = nativeArray.ToArray();
			nativeArray.Dispose();
			return result;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00024354 File Offset: 0x00022554
		[NotBurstCompatible]
		public unsafe static void AddNBC(this UnsafeAppendBuffer buffer, string value)
		{
			if (value != null)
			{
				buffer.Add<int>(value.Length);
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					buffer.Add((void*)ptr, 2 * value.Length);
				}
				return;
			}
			buffer.Add<int>(-1);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002439C File Offset: 0x0002259C
		[NotBurstCompatible]
		public unsafe static byte[] ToBytesNBC(this UnsafeAppendBuffer buffer)
		{
			byte[] array2;
			byte[] array = array2 = new byte[buffer.Length];
			byte* destination;
			if (array == null || array2.Length == 0)
			{
				destination = null;
			}
			else
			{
				destination = &array2[0];
			}
			UnsafeUtility.MemCpy((void*)destination, (void*)buffer.Ptr, (long)buffer.Length);
			array2 = null;
			return array;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000243E4 File Offset: 0x000225E4
		[NotBurstCompatible]
		public unsafe static void ReadNextNBC(this UnsafeAppendBuffer.Reader reader, out string value)
		{
			int num;
			reader.ReadNext<int>(out num);
			if (num != -1)
			{
				value = new string('0', num);
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					int num2 = num * 2;
					UnsafeUtility.MemCpy((void*)ptr, reader.ReadNext(num2), (long)num2);
				}
				return;
			}
			value = null;
		}
	}
}
