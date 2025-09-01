using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000D1 RID: 209
	[NativeContainer]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct NativeReference<[IsUnmanaged] T> : INativeDisposable, IDisposable, IEquatable<NativeReference<T>> where T : struct, ValueType
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00017CC2 File Offset: 0x00015EC2
		public NativeReference(AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeReference<T>.Allocate(allocator, out this);
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(this.m_Data, (long)UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00017CE0 File Offset: 0x00015EE0
		public unsafe NativeReference(T value, AllocatorManager.AllocatorHandle allocator)
		{
			NativeReference<T>.Allocate(allocator, out this);
			*(T*)this.m_Data = value;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00017CF5 File Offset: 0x00015EF5
		private static void Allocate(AllocatorManager.AllocatorHandle allocator, out NativeReference<T> reference)
		{
			reference = default(NativeReference<T>);
			reference.m_Data = Memory.Unmanaged.Allocate((long)UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), allocator);
			reference.m_AllocatorLabel = allocator;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00017D1C File Offset: 0x00015F1C
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00017D29 File Offset: 0x00015F29
		public unsafe T Value
		{
			get
			{
				return *(T*)this.m_Data;
			}
			set
			{
				*(T*)this.m_Data = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00017D37 File Offset: 0x00015F37
		public bool IsCreated
		{
			get
			{
				return this.m_Data != null;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00017D46 File Offset: 0x00015F46
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.m_AllocatorLabel))
			{
				Memory.Unmanaged.Free(this.m_Data, this.m_AllocatorLabel);
				this.m_AllocatorLabel = Allocator.Invalid;
			}
			this.m_Data = null;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00017D7C File Offset: 0x00015F7C
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (CollectionHelper.ShouldDeallocate(this.m_AllocatorLabel))
			{
				JobHandle result = new NativeReferenceDisposeJob
				{
					Data = new NativeReferenceDispose
					{
						m_Data = this.m_Data,
						m_AllocatorLabel = this.m_AllocatorLabel
					}
				}.Schedule(inputDeps);
				this.m_Data = null;
				this.m_AllocatorLabel = Allocator.Invalid;
				return result;
			}
			this.m_Data = null;
			return inputDeps;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00017DED File Offset: 0x00015FED
		public void CopyFrom(NativeReference<T> reference)
		{
			NativeReference<T>.Copy(this, reference);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00017DFB File Offset: 0x00015FFB
		public void CopyTo(NativeReference<T> reference)
		{
			NativeReference<T>.Copy(reference, this);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00017E0C File Offset: 0x0001600C
		[NotBurstCompatible]
		public bool Equals(NativeReference<T> other)
		{
			T value = this.Value;
			return value.Equals(other.Value);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00017E39 File Offset: 0x00016039
		[NotBurstCompatible]
		public override bool Equals(object obj)
		{
			return obj != null && obj is NativeReference<T> && this.Equals((NativeReference<T>)obj);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00017E58 File Offset: 0x00016058
		public override int GetHashCode()
		{
			T value = this.Value;
			return value.GetHashCode();
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00017E79 File Offset: 0x00016079
		public static bool operator ==(NativeReference<T> left, NativeReference<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00017E83 File Offset: 0x00016083
		public static bool operator !=(NativeReference<T> left, NativeReference<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00017E90 File Offset: 0x00016090
		public static void Copy(NativeReference<T> dst, NativeReference<T> src)
		{
			UnsafeUtility.MemCpy(dst.m_Data, src.m_Data, (long)UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00017EA9 File Offset: 0x000160A9
		public NativeReference<T>.ReadOnly AsReadOnly()
		{
			return new NativeReference<T>.ReadOnly(this.m_Data);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00017EB6 File Offset: 0x000160B6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNotDisposed()
		{
			if (this.m_Data == null)
			{
				throw new ObjectDisposedException("The NativeReference is already disposed.");
			}
		}

		// Token: 0x040002B2 RID: 690
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Data;

		// Token: 0x040002B3 RID: 691
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x020000D2 RID: 210
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ReadOnly
		{
			// Token: 0x060007D0 RID: 2000 RVA: 0x00017ECD File Offset: 0x000160CD
			internal unsafe ReadOnly(void* data)
			{
				this.m_Data = data;
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00017ED6 File Offset: 0x000160D6
			public unsafe T Value
			{
				get
				{
					return *(T*)this.m_Data;
				}
			}

			// Token: 0x040002B4 RID: 692
			[NativeDisableUnsafePtrRestriction]
			private unsafe readonly void* m_Data;
		}
	}
}
