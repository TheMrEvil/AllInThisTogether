using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000017 RID: 23
	public readonly struct SharedStatic<T> where T : struct
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005579 File Offset: 0x00003779
		private unsafe SharedStatic(void* buffer)
		{
			this._buffer = buffer;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005582 File Offset: 0x00003782
		public ref T Data
		{
			get
			{
				return Unsafe.AsRef<T>(this._buffer);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000558F File Offset: 0x0000378F
		public unsafe void* UnsafeDataPointer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005597 File Offset: 0x00003797
		public static SharedStatic<T> GetOrCreate<TContext>(uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64<TContext>(), 0L);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000055A6 File Offset: 0x000037A6
		public static SharedStatic<T> GetOrCreate<TContext, TSubContext>(uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64<TContext>(), BurstRuntime.GetHashCode64<TSubContext>());
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000055B8 File Offset: 0x000037B8
		public static SharedStatic<T> GetOrCreateUnsafe(uint alignment, long hashCode, long subHashCode)
		{
			return new SharedStatic<T>(SharedStatic.GetOrCreateSharedStaticInternal(hashCode, subHashCode, (uint)UnsafeUtility.SizeOf<T>(), (alignment == 0U) ? 16U : alignment));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000055D3 File Offset: 0x000037D3
		public static SharedStatic<T> GetOrCreatePartiallyUnsafeWithHashCode<TSubContext>(uint alignment, long hashCode)
		{
			return new SharedStatic<T>(SharedStatic.GetOrCreateSharedStaticInternal(hashCode, BurstRuntime.GetHashCode64<TSubContext>(), (uint)UnsafeUtility.SizeOf<T>(), (alignment == 0U) ? 16U : alignment));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000055F2 File Offset: 0x000037F2
		public static SharedStatic<T> GetOrCreatePartiallyUnsafeWithSubHashCode<TContext>(uint alignment, long subHashCode)
		{
			return new SharedStatic<T>(SharedStatic.GetOrCreateSharedStaticInternal(BurstRuntime.GetHashCode64<TContext>(), subHashCode, (uint)UnsafeUtility.SizeOf<T>(), (alignment == 0U) ? 16U : alignment));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005611 File Offset: 0x00003811
		public static SharedStatic<T> GetOrCreate(Type contextType, uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64(contextType), 0L);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005621 File Offset: 0x00003821
		public static SharedStatic<T> GetOrCreate(Type contextType, Type subContextType, uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64(contextType), BurstRuntime.GetHashCode64(subContextType));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005635 File Offset: 0x00003835
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIf_T_IsUnmanagedOrThrow()
		{
			if (!UnsafeUtility.IsUnmanaged<T>())
			{
				throw new InvalidOperationException(string.Format("The type {0} used in SharedStatic<{1}> must be unmanaged (contain no managed types).", typeof(T), typeof(T)));
			}
		}

		// Token: 0x04000144 RID: 324
		private unsafe readonly void* _buffer;

		// Token: 0x04000145 RID: 325
		private const uint DefaultAlignment = 16U;
	}
}
