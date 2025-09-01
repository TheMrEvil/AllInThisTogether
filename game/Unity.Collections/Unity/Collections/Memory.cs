using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000A9 RID: 169
	[BurstCompatible]
	internal struct Memory
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x00015414 File Offset: 0x00013614
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void CheckByteCountIsReasonable(long size)
		{
			if (size < 0L)
			{
				throw new InvalidOperationException("Attempted to operate on {size} bytes of memory: nonsensical");
			}
			if (size > 1099511627776L)
			{
				throw new InvalidOperationException("Attempted to operate on {size} bytes of memory: too big");
			}
		}

		// Token: 0x0400027C RID: 636
		internal const long k_MaximumRamSizeInBytes = 1099511627776L;

		// Token: 0x020000AA RID: 170
		[BurstCompatible]
		internal struct Unmanaged
		{
			// Token: 0x0600068B RID: 1675 RVA: 0x0001543D File Offset: 0x0001363D
			internal unsafe static void* Allocate(long size, int align, AllocatorManager.AllocatorHandle allocator)
			{
				return Memory.Unmanaged.Array.Resize(null, 0L, 1L, allocator, size, align);
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0001544D File Offset: 0x0001364D
			internal unsafe static void Free(void* pointer, AllocatorManager.AllocatorHandle allocator)
			{
				if (pointer == null)
				{
					return;
				}
				Memory.Unmanaged.Array.Resize(pointer, 1L, 0L, allocator, 1L, 1);
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00015464 File Offset: 0x00013664
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			internal unsafe static T* Allocate<[IsUnmanaged] T>(AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
			{
				return Memory.Unmanaged.Array.Resize<T>(null, 0L, 1L, allocator);
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x00015472 File Offset: 0x00013672
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			internal unsafe static void Free<[IsUnmanaged] T>(T* pointer, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
			{
				if (pointer == null)
				{
					return;
				}
				Memory.Unmanaged.Array.Resize<T>(pointer, 1L, 0L, allocator);
			}

			// Token: 0x020000AB RID: 171
			[BurstCompatible]
			internal struct Array
			{
				// Token: 0x0600068F RID: 1679 RVA: 0x00002EE3 File Offset: 0x000010E3
				private static bool IsCustom(AllocatorManager.AllocatorHandle allocator)
				{
					return allocator.Index >= 64;
				}

				// Token: 0x06000690 RID: 1680 RVA: 0x00015488 File Offset: 0x00013688
				private unsafe static void* CustomResize(void* oldPointer, long oldCount, long newCount, AllocatorManager.AllocatorHandle allocator, long size, int align)
				{
					AllocatorManager.Block block = default(AllocatorManager.Block);
					block.Range.Allocator = allocator;
					block.Range.Items = (int)newCount;
					block.Range.Pointer = (IntPtr)oldPointer;
					block.BytesPerItem = (int)size;
					block.Alignment = align;
					block.AllocatedItems = (int)oldCount;
					AllocatorManager.Try(ref block);
					return (void*)block.Range.Pointer;
				}

				// Token: 0x06000691 RID: 1681 RVA: 0x00015500 File Offset: 0x00013700
				internal unsafe static void* Resize(void* oldPointer, long oldCount, long newCount, AllocatorManager.AllocatorHandle allocator, long size, int align)
				{
					int num = math.max(64, align);
					if (Memory.Unmanaged.Array.IsCustom(allocator))
					{
						return Memory.Unmanaged.Array.CustomResize(oldPointer, oldCount, newCount, allocator, size, num);
					}
					void* ptr = default(void*);
					if (newCount > 0L)
					{
						ptr = UnsafeUtility.Malloc(newCount * size, num, allocator.ToAllocator);
						if (oldCount > 0L)
						{
							long size2 = math.min(oldCount, newCount) * size;
							UnsafeUtility.MemCpy(ptr, oldPointer, size2);
						}
					}
					if (oldCount > 0L)
					{
						UnsafeUtility.Free(oldPointer, allocator.ToAllocator);
					}
					return ptr;
				}

				// Token: 0x06000692 RID: 1682 RVA: 0x00015576 File Offset: 0x00013776
				[BurstCompatible(GenericTypeArguments = new Type[]
				{
					typeof(int)
				})]
				internal unsafe static T* Resize<[IsUnmanaged] T>(T* oldPointer, long oldCount, long newCount, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
				{
					return (T*)Memory.Unmanaged.Array.Resize((void*)oldPointer, oldCount, newCount, allocator, (long)UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>());
				}

				// Token: 0x06000693 RID: 1683 RVA: 0x0001558C File Offset: 0x0001378C
				[BurstCompatible(GenericTypeArguments = new Type[]
				{
					typeof(int)
				})]
				internal unsafe static T* Allocate<[IsUnmanaged] T>(long count, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
				{
					return Memory.Unmanaged.Array.Resize<T>(null, 0L, count, allocator);
				}

				// Token: 0x06000694 RID: 1684 RVA: 0x00015599 File Offset: 0x00013799
				[BurstCompatible(GenericTypeArguments = new Type[]
				{
					typeof(int)
				})]
				internal unsafe static void Free<[IsUnmanaged] T>(T* pointer, long count, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
				{
					if (pointer == null)
					{
						return;
					}
					Memory.Unmanaged.Array.Resize<T>(pointer, count, 0L, allocator);
				}
			}
		}

		// Token: 0x020000AC RID: 172
		[BurstCompatible]
		internal struct Array
		{
			// Token: 0x06000695 RID: 1685 RVA: 0x000155AC File Offset: 0x000137AC
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			internal unsafe static void Set<[IsUnmanaged] T>(T* pointer, long count, T t = default(T)) where T : struct, ValueType
			{
				UnsafeUtility.SizeOf<T>();
				int num = 0;
				while ((long)num < count)
				{
					pointer[(IntPtr)num * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = t;
					num++;
				}
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x000155E0 File Offset: 0x000137E0
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			internal unsafe static void Clear<[IsUnmanaged] T>(T* pointer, long count) where T : struct, ValueType
			{
				long size = count * (long)UnsafeUtility.SizeOf<T>();
				UnsafeUtility.MemClear((void*)pointer, size);
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x00015600 File Offset: 0x00013800
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			internal unsafe static void Copy<[IsUnmanaged] T>(T* dest, T* src, long count) where T : struct, ValueType
			{
				long size = count * (long)UnsafeUtility.SizeOf<T>();
				UnsafeUtility.MemCpy((void*)dest, (void*)src, size);
			}
		}
	}
}
