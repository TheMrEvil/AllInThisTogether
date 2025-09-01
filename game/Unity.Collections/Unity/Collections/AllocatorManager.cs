using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x0200001D RID: 29
	public static class AllocatorManager
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002854 File Offset: 0x00000A54
		internal static AllocatorManager.Block AllocateBlock<[IsUnmanaged] T>(this T t, int sizeOf, int alignOf, int items) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			AllocatorManager.Block result = default(AllocatorManager.Block);
			result.Range.Pointer = IntPtr.Zero;
			result.Range.Items = items;
			result.Range.Allocator = t.Handle;
			result.BytesPerItem = sizeOf;
			result.Alignment = math.max(64, alignOf);
			t.Try(ref result);
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000028C6 File Offset: 0x00000AC6
		internal static AllocatorManager.Block AllocateBlock<[IsUnmanaged] T, [IsUnmanaged] U>(this T t, U u, int items) where T : struct, ValueType, AllocatorManager.IAllocator where U : struct, ValueType
		{
			return ref t.AllocateBlock(UnsafeUtility.SizeOf<U>(), UnsafeUtility.AlignOf<U>(), items);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028D9 File Offset: 0x00000AD9
		internal unsafe static void* Allocate<[IsUnmanaged] T>(this T t, int sizeOf, int alignOf, int items) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			return (void*)ref t.AllocateBlock(sizeOf, alignOf, items).Range.Pointer;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000028F3 File Offset: 0x00000AF3
		internal unsafe static U* Allocate<[IsUnmanaged] T, [IsUnmanaged] U>(this T t, U u, int items) where T : struct, ValueType, AllocatorManager.IAllocator where U : struct, ValueType
		{
			return (U*)ref t.Allocate(UnsafeUtility.SizeOf<U>(), UnsafeUtility.AlignOf<U>(), items);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000028F3 File Offset: 0x00000AF3
		internal unsafe static void* AllocateStruct<[IsUnmanaged] T, U>(this T t, U u, int items) where T : struct, ValueType, AllocatorManager.IAllocator where U : struct
		{
			return ref t.Allocate(UnsafeUtility.SizeOf<U>(), UnsafeUtility.AlignOf<U>(), items);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002906 File Offset: 0x00000B06
		internal static void FreeBlock<[IsUnmanaged] T>(this T t, ref AllocatorManager.Block block) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			block.Range.Items = 0;
			t.Try(ref block);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002924 File Offset: 0x00000B24
		internal unsafe static void Free<[IsUnmanaged] T>(this T t, void* pointer, int sizeOf, int alignOf, int items) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			if (pointer == null)
			{
				return;
			}
			AllocatorManager.Block block = default(AllocatorManager.Block);
			block.AllocatedItems = items;
			block.Range.Pointer = (IntPtr)pointer;
			block.BytesPerItem = sizeOf;
			block.Alignment = alignOf;
			ref t.FreeBlock(ref block);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002972 File Offset: 0x00000B72
		internal unsafe static void Free<[IsUnmanaged] T, [IsUnmanaged] U>(this T t, U* pointer, int items) where T : struct, ValueType, AllocatorManager.IAllocator where U : struct, ValueType
		{
			ref t.Free((void*)pointer, UnsafeUtility.SizeOf<U>(), UnsafeUtility.AlignOf<U>(), items);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002986 File Offset: 0x00000B86
		public unsafe static void* Allocate(AllocatorManager.AllocatorHandle handle, int itemSizeInBytes, int alignmentInBytes, int items = 1)
		{
			return ref handle.Allocate(itemSizeInBytes, alignmentInBytes, items);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002994 File Offset: 0x00000B94
		public unsafe static T* Allocate<[IsUnmanaged] T>(AllocatorManager.AllocatorHandle handle, int items = 1) where T : struct, ValueType
		{
			return ref handle.Allocate(default(T), items);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029B2 File Offset: 0x00000BB2
		public unsafe static void Free(AllocatorManager.AllocatorHandle handle, void* pointer, int itemSizeInBytes, int alignmentInBytes, int items = 1)
		{
			ref handle.Free(pointer, itemSizeInBytes, alignmentInBytes, items);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000029C0 File Offset: 0x00000BC0
		public unsafe static void Free(AllocatorManager.AllocatorHandle handle, void* pointer)
		{
			ref handle.Free((byte*)pointer, 1);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000029CB File Offset: 0x00000BCB
		public unsafe static void Free<[IsUnmanaged] T>(AllocatorManager.AllocatorHandle handle, T* pointer, int items = 1) where T : struct, ValueType
		{
			ref handle.Free(pointer, items);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000029D6 File Offset: 0x00000BD6
		[BurstDiscard]
		private static void CheckDelegate(ref bool useDelegate)
		{
			useDelegate = true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029DC File Offset: 0x00000BDC
		private static bool UseDelegate()
		{
			bool result = false;
			AllocatorManager.CheckDelegate(ref result);
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000029F4 File Offset: 0x00000BF4
		private unsafe static int allocate_block(ref AllocatorManager.Block block)
		{
			AllocatorManager.TableEntry tableEntry = default(AllocatorManager.TableEntry);
			tableEntry = *block.Range.Allocator.TableEntry;
			FunctionPointer<AllocatorManager.TryFunction> functionPointer = new FunctionPointer<AllocatorManager.TryFunction>(tableEntry.function);
			return functionPointer.Invoke(tableEntry.state, ref block);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A40 File Offset: 0x00000C40
		[BurstDiscard]
		private unsafe static void forward_mono_allocate_block(ref AllocatorManager.Block block, ref int error)
		{
			AllocatorManager.TableEntry tableEntry = default(AllocatorManager.TableEntry);
			tableEntry = *block.Range.Allocator.TableEntry;
			if (block.Range.Allocator.Handle.Index >= 32768)
			{
				throw new ArgumentException("Allocator index into TryFunction delegate table exceeds maximum.");
			}
			ref AllocatorManager.TryFunction ptr = ref AllocatorManager.Managed.TryFunctionDelegates[(int)block.Range.Allocator.Handle.Index];
			error = ptr(tableEntry.state, ref block);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AC2 File Offset: 0x00000CC2
		internal static Allocator LegacyOf(AllocatorManager.AllocatorHandle handle)
		{
			if (handle.Value >= 64)
			{
				return Allocator.Persistent;
			}
			return (Allocator)handle.Value;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private unsafe static int TryLegacy(ref AllocatorManager.Block block)
		{
			if (block.Range.Pointer == IntPtr.Zero)
			{
				block.Range.Pointer = (IntPtr)Memory.Unmanaged.Allocate(block.Bytes, block.Alignment, AllocatorManager.LegacyOf(block.Range.Allocator));
				block.AllocatedItems = block.Range.Items;
				if (!(block.Range.Pointer == IntPtr.Zero))
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (block.Bytes == 0L)
				{
					if (AllocatorManager.LegacyOf(block.Range.Allocator) != Allocator.None)
					{
						Memory.Unmanaged.Free((void*)block.Range.Pointer, AllocatorManager.LegacyOf(block.Range.Allocator));
					}
					block.Range.Pointer = IntPtr.Zero;
					block.AllocatedItems = 0;
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public unsafe static int Try(ref AllocatorManager.Block block)
		{
			if (block.Range.Allocator.Value < 64)
			{
				return AllocatorManager.TryLegacy(ref block);
			}
			AllocatorManager.TableEntry tableEntry = default(AllocatorManager.TableEntry);
			tableEntry = *block.Range.Allocator.TableEntry;
			new FunctionPointer<AllocatorManager.TryFunction>(tableEntry.function);
			if (AllocatorManager.UseDelegate())
			{
				int result = 0;
				AllocatorManager.forward_mono_allocate_block(ref block, ref result);
				return result;
			}
			return AllocatorManager.allocate_block(ref block);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C2B File Offset: 0x00000E2B
		public static void Initialize()
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C30 File Offset: 0x00000E30
		internal static void Install(AllocatorManager.AllocatorHandle handle, IntPtr allocatorState, FunctionPointer<AllocatorManager.TryFunction> functionPointer, AllocatorManager.TryFunction function)
		{
			if (functionPointer.Value == IntPtr.Zero)
			{
				ref handle.Unregister<AllocatorManager.AllocatorHandle>();
				return;
			}
			if (ConcurrentMask.Succeeded(ConcurrentMask.TryAllocate<Long1024>(AllocatorManager.SharedStatics.IsInstalled.Ref.Data, handle.Value, 1)))
			{
				handle.Install(new AllocatorManager.TableEntry
				{
					state = allocatorState,
					function = functionPointer.Value
				});
				AllocatorManager.Managed.RegisterDelegate((int)handle.Index, function);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002CA8 File Offset: 0x00000EA8
		internal static void Install(AllocatorManager.AllocatorHandle handle, IntPtr allocatorState, AllocatorManager.TryFunction function)
		{
			FunctionPointer<AllocatorManager.TryFunction> functionPointer = (function == null) ? new FunctionPointer<AllocatorManager.TryFunction>(IntPtr.Zero) : BurstCompiler.CompileFunctionPointer<AllocatorManager.TryFunction>(function);
			AllocatorManager.Install(handle, allocatorState, functionPointer, function);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal static AllocatorManager.AllocatorHandle Register(IntPtr allocatorState, FunctionPointer<AllocatorManager.TryFunction> functionPointer)
		{
			AllocatorManager.TableEntry tableEntry = new AllocatorManager.TableEntry
			{
				state = allocatorState,
				function = functionPointer.Value
			};
			int num;
			int error = ConcurrentMask.TryAllocate<Long1024>(AllocatorManager.SharedStatics.IsInstalled.Ref.Data, out num, 1, AllocatorManager.SharedStatics.IsInstalled.Ref.Data.Length, 1);
			AllocatorManager.AllocatorHandle result = default(AllocatorManager.AllocatorHandle);
			if (ConcurrentMask.Succeeded(error))
			{
				result.Index = (ushort)num;
				result.Install(tableEntry);
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002D44 File Offset: 0x00000F44
		[NotBurstCompatible]
		public static void Register<[IsUnmanaged] T>(this T t) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			FunctionPointer<AllocatorManager.TryFunction> functionPointer = (t.Function == null) ? new FunctionPointer<AllocatorManager.TryFunction>(IntPtr.Zero) : BurstCompiler.CompileFunctionPointer<AllocatorManager.TryFunction>(t.Function);
			t.Handle = AllocatorManager.Register((IntPtr)UnsafeUtility.AddressOf<T>(ref t), functionPointer);
			AllocatorManager.Managed.RegisterDelegate((int)t.Handle.Index, t.Function);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002DBC File Offset: 0x00000FBC
		public static void UnmanagedUnregister<[IsUnmanaged] T>(this T t) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			if (t.Handle.IsInstalled)
			{
				t.Handle.Install(default(AllocatorManager.TableEntry));
				ConcurrentMask.TryFree<Long1024>(AllocatorManager.SharedStatics.IsInstalled.Ref.Data, t.Handle.Value, 1);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E24 File Offset: 0x00001024
		[NotBurstCompatible]
		public static void Unregister<[IsUnmanaged] T>(this T t) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			if (t.Handle.IsInstalled)
			{
				t.Handle.Install(default(AllocatorManager.TableEntry));
				ConcurrentMask.TryFree<Long1024>(AllocatorManager.SharedStatics.IsInstalled.Ref.Data, t.Handle.Value, 1);
				AllocatorManager.Managed.UnregisterDelegate((int)t.Handle.Index);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EA0 File Offset: 0x000010A0
		[NotBurstCompatible]
		internal unsafe static ref T CreateAllocator<[IsUnmanaged] T>(AllocatorManager.AllocatorHandle backingAllocator) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			T* ptr = (T*)Memory.Unmanaged.Allocate((long)UnsafeUtility.SizeOf<T>(), 16, backingAllocator);
			*ptr = default(T);
			ref T ptr2 = ref UnsafeUtility.AsRef<T>((void*)ptr);
			ref ptr2.Register<T>();
			return ref ptr2;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002ECF File Offset: 0x000010CF
		[NotBurstCompatible]
		internal static void DestroyAllocator<[IsUnmanaged] T>(this T t, AllocatorManager.AllocatorHandle backingAllocator) where T : struct, ValueType, AllocatorManager.IAllocator
		{
			ref t.Unregister<T>();
			Memory.Unmanaged.Free(UnsafeUtility.AddressOf<T>(ref t), backingAllocator);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C2B File Offset: 0x00000E2B
		public static void Shutdown()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002EE3 File Offset: 0x000010E3
		internal static bool IsCustomAllocator(AllocatorManager.AllocatorHandle allocator)
		{
			return allocator.Index >= 64;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EF2 File Offset: 0x000010F2
		[Conditional("ENABLE_UNITY_ALLOCATION_CHECKS")]
		internal static void CheckFailedToAllocate(int error)
		{
			if (error != 0)
			{
				throw new ArgumentException("failed to allocate");
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F02 File Offset: 0x00001102
		[Conditional("ENABLE_UNITY_ALLOCATION_CHECKS")]
		internal static void CheckFailedToFree(int error)
		{
			if (error != 0)
			{
				throw new ArgumentException("failed to free");
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_ALLOCATION_CHECKS")]
		internal static void CheckValid(AllocatorManager.AllocatorHandle handle)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F14 File Offset: 0x00001114
		// Note: this type is marked as 'beforefieldinit'.
		static AllocatorManager()
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002FA5 File Offset: 0x000011A5
		public static void Initialize$StackAllocator_Try_00000A45$BurstDirectCall()
		{
			AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Initialize();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002FAC File Offset: 0x000011AC
		public static void Initialize$SlabAllocator_Try_00000A53$BurstDirectCall()
		{
			AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Initialize();
		}

		// Token: 0x0400000B RID: 11
		public static readonly AllocatorManager.AllocatorHandle Invalid = new AllocatorManager.AllocatorHandle
		{
			Index = 0
		};

		// Token: 0x0400000C RID: 12
		public static readonly AllocatorManager.AllocatorHandle None = new AllocatorManager.AllocatorHandle
		{
			Index = 1
		};

		// Token: 0x0400000D RID: 13
		public static readonly AllocatorManager.AllocatorHandle Temp = new AllocatorManager.AllocatorHandle
		{
			Index = 2
		};

		// Token: 0x0400000E RID: 14
		public static readonly AllocatorManager.AllocatorHandle TempJob = new AllocatorManager.AllocatorHandle
		{
			Index = 3
		};

		// Token: 0x0400000F RID: 15
		public static readonly AllocatorManager.AllocatorHandle Persistent = new AllocatorManager.AllocatorHandle
		{
			Index = 4
		};

		// Token: 0x04000010 RID: 16
		public static readonly AllocatorManager.AllocatorHandle AudioKernel = new AllocatorManager.AllocatorHandle
		{
			Index = 5
		};

		// Token: 0x04000011 RID: 17
		public const int kErrorNone = 0;

		// Token: 0x04000012 RID: 18
		public const int kErrorBufferOverflow = -1;

		// Token: 0x04000013 RID: 19
		public const ushort FirstUserIndex = 64;

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000076 RID: 118
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int TryFunction(IntPtr allocatorState, ref AllocatorManager.Block block);

		// Token: 0x0200001F RID: 31
		public struct AllocatorHandle : AllocatorManager.IAllocator, IDisposable
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00002FB3 File Offset: 0x000011B3
			internal ref AllocatorManager.TableEntry TableEntry
			{
				get
				{
					return AllocatorManager.SharedStatics.TableEntry.Ref.Data.ElementAt((int)this.Index);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FCA File Offset: 0x000011CA
			internal unsafe bool IsInstalled
			{
				get
				{
					return (*AllocatorManager.SharedStatics.IsInstalled.Ref.Data.ElementAt(this.Index >> 6) >> (int)this.Index & 1L) != 0L;
				}
			}

			// Token: 0x0600007B RID: 123 RVA: 0x00002C2B File Offset: 0x00000E2B
			internal void IncrementVersion()
			{
			}

			// Token: 0x0600007C RID: 124 RVA: 0x00002C2B File Offset: 0x00000E2B
			internal void Rewind()
			{
			}

			// Token: 0x0600007D RID: 125 RVA: 0x00002FF8 File Offset: 0x000011F8
			internal unsafe void Install(AllocatorManager.TableEntry tableEntry)
			{
				this.Rewind();
				*this.TableEntry = tableEntry;
			}

			// Token: 0x0600007E RID: 126 RVA: 0x0000300C File Offset: 0x0000120C
			public static implicit operator AllocatorManager.AllocatorHandle(Allocator a)
			{
				return new AllocatorManager.AllocatorHandle
				{
					Index = (ushort)(a & (Allocator)65535),
					Version = (ushort)(a >> 16)
				};
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600007F RID: 127 RVA: 0x0000303D File Offset: 0x0000123D
			public int Value
			{
				get
				{
					return (int)this.Index;
				}
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00003048 File Offset: 0x00001248
			public int TryAllocateBlock<T>(out AllocatorManager.Block block, int items) where T : struct
			{
				block = new AllocatorManager.Block
				{
					Range = new AllocatorManager.Range
					{
						Items = items,
						Allocator = this
					},
					BytesPerItem = UnsafeUtility.SizeOf<T>(),
					Alignment = 1 << math.min(3, math.tzcnt(UnsafeUtility.SizeOf<T>()))
				};
				return this.Try(ref block);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x000030B8 File Offset: 0x000012B8
			public AllocatorManager.Block AllocateBlock<T>(int items) where T : struct
			{
				AllocatorManager.Block result;
				this.TryAllocateBlock<T>(out result, items);
				return result;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000030D0 File Offset: 0x000012D0
			[Conditional("ENABLE_UNITY_ALLOCATION_CHECKS")]
			private static void CheckAllocatedSuccessfully(int error)
			{
				if (error != 0)
				{
					throw new ArgumentException(string.Format("Error {0}: Failed to Allocate", error));
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000083 RID: 131 RVA: 0x000030EB File Offset: 0x000012EB
			public AllocatorManager.TryFunction Function
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000084 RID: 132 RVA: 0x000030EE File Offset: 0x000012EE
			public int Try(ref AllocatorManager.Block block)
			{
				block.Range.Allocator = this;
				return AllocatorManager.Try(ref block);
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000085 RID: 133 RVA: 0x00003107 File Offset: 0x00001307
			// (set) Token: 0x06000086 RID: 134 RVA: 0x0000310F File Offset: 0x0000130F
			public AllocatorManager.AllocatorHandle Handle
			{
				get
				{
					return this;
				}
				set
				{
					this = value;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000087 RID: 135 RVA: 0x00003118 File Offset: 0x00001318
			public Allocator ToAllocator
			{
				get
				{
					uint index = (uint)this.Index;
					return (Allocator)((int)this.Version << 16 | (int)index);
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000088 RID: 136 RVA: 0x00002EE3 File Offset: 0x000010E3
			public bool IsCustomAllocator
			{
				get
				{
					return this.Index >= 64;
				}
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00003137 File Offset: 0x00001337
			public void Dispose()
			{
				this.Rewind();
			}

			// Token: 0x04000014 RID: 20
			public ushort Index;

			// Token: 0x04000015 RID: 21
			public ushort Version;
		}

		// Token: 0x02000020 RID: 32
		public struct BlockHandle
		{
			// Token: 0x04000016 RID: 22
			public ushort Value;
		}

		// Token: 0x02000021 RID: 33
		public struct Range : IDisposable
		{
			// Token: 0x0600008A RID: 138 RVA: 0x00003140 File Offset: 0x00001340
			public void Dispose()
			{
				AllocatorManager.Block block = new AllocatorManager.Block
				{
					Range = this
				};
				block.Dispose();
				this = block.Range;
			}

			// Token: 0x04000017 RID: 23
			public IntPtr Pointer;

			// Token: 0x04000018 RID: 24
			public int Items;

			// Token: 0x04000019 RID: 25
			public AllocatorManager.AllocatorHandle Allocator;
		}

		// Token: 0x02000022 RID: 34
		public struct Block : IDisposable
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600008B RID: 139 RVA: 0x00003177 File Offset: 0x00001377
			public long Bytes
			{
				get
				{
					return (long)(this.BytesPerItem * this.Range.Items);
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600008C RID: 140 RVA: 0x0000318C File Offset: 0x0000138C
			public long AllocatedBytes
			{
				get
				{
					return (long)(this.BytesPerItem * this.AllocatedItems);
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600008D RID: 141 RVA: 0x0000319C File Offset: 0x0000139C
			// (set) Token: 0x0600008E RID: 142 RVA: 0x000031A9 File Offset: 0x000013A9
			public int Alignment
			{
				get
				{
					return 1 << (int)this.Log2Alignment;
				}
				set
				{
					this.Log2Alignment = (byte)(32 - math.lzcnt(math.max(1, value) - 1));
				}
			}

			// Token: 0x0600008F RID: 143 RVA: 0x000031C3 File Offset: 0x000013C3
			public void Dispose()
			{
				this.TryFree();
			}

			// Token: 0x06000090 RID: 144 RVA: 0x000031CC File Offset: 0x000013CC
			public int TryAllocate()
			{
				this.Range.Pointer = IntPtr.Zero;
				return AllocatorManager.Try(ref this);
			}

			// Token: 0x06000091 RID: 145 RVA: 0x000031E4 File Offset: 0x000013E4
			public int TryFree()
			{
				this.Range.Items = 0;
				return AllocatorManager.Try(ref this);
			}

			// Token: 0x06000092 RID: 146 RVA: 0x000031F8 File Offset: 0x000013F8
			public void Allocate()
			{
				this.TryAllocate();
			}

			// Token: 0x06000093 RID: 147 RVA: 0x000031C3 File Offset: 0x000013C3
			public void Free()
			{
				this.TryFree();
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00003201 File Offset: 0x00001401
			[Conditional("ENABLE_UNITY_ALLOCATION_CHECKS")]
			private void CheckFailedToAllocate(int error)
			{
				if (error != 0)
				{
					throw new ArgumentException(string.Format("Error {0}: Failed to Allocate {1}", error, this));
				}
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00003227 File Offset: 0x00001427
			[Conditional("ENABLE_UNITY_ALLOCATION_CHECKS")]
			private void CheckFailedToFree(int error)
			{
				if (error != 0)
				{
					throw new ArgumentException(string.Format("Error {0}: Failed to Free {1}", error, this));
				}
			}

			// Token: 0x0400001A RID: 26
			public AllocatorManager.Range Range;

			// Token: 0x0400001B RID: 27
			public int BytesPerItem;

			// Token: 0x0400001C RID: 28
			public int AllocatedItems;

			// Token: 0x0400001D RID: 29
			public byte Log2Alignment;

			// Token: 0x0400001E RID: 30
			public byte Padding0;

			// Token: 0x0400001F RID: 31
			public ushort Padding1;

			// Token: 0x04000020 RID: 32
			public uint Padding2;
		}

		// Token: 0x02000023 RID: 35
		public interface IAllocator : IDisposable
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000096 RID: 150
			AllocatorManager.TryFunction Function { get; }

			// Token: 0x06000097 RID: 151
			int Try(ref AllocatorManager.Block block);

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000098 RID: 152
			// (set) Token: 0x06000099 RID: 153
			AllocatorManager.AllocatorHandle Handle { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600009A RID: 154
			Allocator ToAllocator { get; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600009B RID: 155
			bool IsCustomAllocator { get; }
		}

		// Token: 0x02000024 RID: 36
		[BurstCompile(CompileSynchronously = true)]
		internal struct StackAllocator : AllocatorManager.IAllocator, IDisposable
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600009C RID: 156 RVA: 0x0000324D File Offset: 0x0000144D
			// (set) Token: 0x0600009D RID: 157 RVA: 0x00003255 File Offset: 0x00001455
			public AllocatorManager.AllocatorHandle Handle
			{
				get
				{
					return this.m_handle;
				}
				set
				{
					this.m_handle = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600009E RID: 158 RVA: 0x0000325E File Offset: 0x0000145E
			public Allocator ToAllocator
			{
				get
				{
					return this.m_handle.ToAllocator;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600009F RID: 159 RVA: 0x0000326B File Offset: 0x0000146B
			public bool IsCustomAllocator
			{
				get
				{
					return this.m_handle.IsCustomAllocator;
				}
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x00003278 File Offset: 0x00001478
			public void Initialize(AllocatorManager.Block storage)
			{
				this.m_storage = storage;
				this.m_top = 0L;
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x0000328C File Offset: 0x0000148C
			public unsafe int Try(ref AllocatorManager.Block block)
			{
				if (block.Range.Pointer == IntPtr.Zero)
				{
					if (this.m_top + block.Bytes > this.m_storage.Bytes)
					{
						return -1;
					}
					block.Range.Pointer = (IntPtr)((void*)((byte*)((void*)this.m_storage.Range.Pointer) + this.m_top));
					block.AllocatedItems = block.Range.Items;
					this.m_top += block.Bytes;
					return 0;
				}
				else
				{
					if (block.Bytes != 0L)
					{
						return -1;
					}
					if ((long)((byte*)((void*)block.Range.Pointer) - (byte*)((void*)this.m_storage.Range.Pointer)) == this.m_top - block.AllocatedBytes)
					{
						this.m_top -= block.AllocatedBytes;
						block.Range.Pointer = IntPtr.Zero;
						block.AllocatedItems = 0;
						return 0;
					}
					return -1;
				}
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x0000338E File Offset: 0x0000158E
			[BurstCompile(CompileSynchronously = true)]
			[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
			public static int Try(IntPtr allocatorState, ref AllocatorManager.Block block)
			{
				return AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Invoke(allocatorState, ref block);
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003397 File Offset: 0x00001597
			public AllocatorManager.TryFunction Function
			{
				get
				{
					return new AllocatorManager.TryFunction(AllocatorManager.StackAllocator.Try);
				}
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x000033A5 File Offset: 0x000015A5
			public void Dispose()
			{
				this.m_handle.Rewind();
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x000033B2 File Offset: 0x000015B2
			[BurstCompile(CompileSynchronously = true)]
			[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe static int Try$BurstManaged(IntPtr allocatorState, ref AllocatorManager.Block block)
			{
				return ((AllocatorManager.StackAllocator*)((void*)allocatorState))->Try(ref block);
			}

			// Token: 0x04000021 RID: 33
			internal AllocatorManager.AllocatorHandle m_handle;

			// Token: 0x04000022 RID: 34
			internal AllocatorManager.Block m_storage;

			// Token: 0x04000023 RID: 35
			internal long m_top;

			// Token: 0x02000025 RID: 37
			// (Invoke) Token: 0x060000A7 RID: 167
			public delegate int Try_00000A45$PostfixBurstDelegate(IntPtr allocatorState, ref AllocatorManager.Block block);

			// Token: 0x02000026 RID: 38
			internal static class Try_00000A45$BurstDirectCall
			{
				// Token: 0x060000AA RID: 170 RVA: 0x000033C0 File Offset: 0x000015C0
				[BurstDiscard]
				private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Pointer == 0)
					{
						AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.DeferredCompilation, methodof(AllocatorManager.StackAllocator.Try$BurstManaged(IntPtr, AllocatorManager.Block*)).MethodHandle, typeof(AllocatorManager.StackAllocator.Try_00000A45$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Pointer;
				}

				// Token: 0x060000AB RID: 171 RVA: 0x000033EC File Offset: 0x000015EC
				private static IntPtr GetFunctionPointer()
				{
					IntPtr result = (IntPtr)0;
					AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.GetFunctionPointerDiscard(ref result);
					return result;
				}

				// Token: 0x060000AC RID: 172 RVA: 0x00003404 File Offset: 0x00001604
				public unsafe static void Constructor()
				{
					AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(AllocatorManager.StackAllocator.Try(IntPtr, AllocatorManager.Block*)).MethodHandle);
				}

				// Token: 0x060000AD RID: 173 RVA: 0x00002C2B File Offset: 0x00000E2B
				public static void Initialize()
				{
				}

				// Token: 0x060000AE RID: 174 RVA: 0x00003415 File Offset: 0x00001615
				// Note: this type is marked as 'beforefieldinit'.
				static Try_00000A45$BurstDirectCall()
				{
					AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.Constructor();
				}

				// Token: 0x060000AF RID: 175 RVA: 0x0000341C File Offset: 0x0000161C
				public static int Invoke(IntPtr allocatorState, ref AllocatorManager.Block block)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = AllocatorManager.StackAllocator.Try_00000A45$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							return calli(System.Int32(System.IntPtr,Unity.Collections.AllocatorManager/Block&), allocatorState, ref block, functionPointer);
						}
					}
					return AllocatorManager.StackAllocator.Try$BurstManaged(allocatorState, ref block);
				}

				// Token: 0x04000024 RID: 36
				private static IntPtr Pointer;

				// Token: 0x04000025 RID: 37
				private static IntPtr DeferredCompilation;
			}
		}

		// Token: 0x02000027 RID: 39
		[BurstCompile(CompileSynchronously = true)]
		internal struct SlabAllocator : AllocatorManager.IAllocator, IDisposable
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000344F File Offset: 0x0000164F
			// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003457 File Offset: 0x00001657
			public AllocatorManager.AllocatorHandle Handle
			{
				get
				{
					return this.m_handle;
				}
				set
				{
					this.m_handle = value;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003460 File Offset: 0x00001660
			public Allocator ToAllocator
			{
				get
				{
					return this.m_handle.ToAllocator;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000346D File Offset: 0x0000166D
			public bool IsCustomAllocator
			{
				get
				{
					return this.m_handle.IsCustomAllocator;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000347A File Offset: 0x0000167A
			public long BudgetInBytes
			{
				get
				{
					return this.budgetInBytes;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003482 File Offset: 0x00001682
			public long AllocatedBytes
			{
				get
				{
					return this.allocatedBytes;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000348A File Offset: 0x0000168A
			// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003497 File Offset: 0x00001697
			internal int SlabSizeInBytes
			{
				get
				{
					return 1 << this.Log2SlabSizeInBytes;
				}
				set
				{
					this.Log2SlabSizeInBytes = (int)((byte)(32 - math.lzcnt(math.max(1, value) - 1)));
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x000034B1 File Offset: 0x000016B1
			internal int Slabs
			{
				get
				{
					return (int)(this.Storage.Bytes >> this.Log2SlabSizeInBytes);
				}
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x000034CC File Offset: 0x000016CC
			internal void Initialize(AllocatorManager.Block storage, int slabSizeInBytes, long budget)
			{
				this.Storage = storage;
				this.Log2SlabSizeInBytes = 0;
				this.Occupied = default(FixedList4096Bytes<int>);
				this.budgetInBytes = budget;
				this.allocatedBytes = 0L;
				this.SlabSizeInBytes = slabSizeInBytes;
				this.Occupied.Length = (this.Slabs + 31) / 32;
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00003520 File Offset: 0x00001720
			public int Try(ref AllocatorManager.Block block)
			{
				if (block.Range.Pointer == IntPtr.Zero)
				{
					if (block.Bytes + this.allocatedBytes > this.budgetInBytes)
					{
						return -2;
					}
					if (block.Bytes > (long)this.SlabSizeInBytes)
					{
						return -1;
					}
					for (int i = 0; i < this.Occupied.Length; i++)
					{
						int num = this.Occupied[i];
						if (num != -1)
						{
							for (int j = 0; j < 32; j++)
							{
								if ((num & 1 << j) == 0)
								{
									ref FixedList4096Bytes<int> ptr = ref this.Occupied;
									int index = i;
									ptr[index] |= 1 << j;
									block.Range.Pointer = this.Storage.Range.Pointer + (int)((long)this.SlabSizeInBytes * ((long)i * 32L + (long)j));
									block.AllocatedItems = this.SlabSizeInBytes / block.BytesPerItem;
									this.allocatedBytes += block.Bytes;
									return 0;
								}
							}
						}
					}
					return -1;
				}
				else
				{
					if (block.Bytes == 0L)
					{
						ulong num2 = (ulong)((long)block.Range.Pointer - (long)this.Storage.Range.Pointer) >> this.Log2SlabSizeInBytes;
						int num3 = (int)(num2 >> 5);
						int num4 = (int)(num2 & 31UL);
						ref FixedList4096Bytes<int> ptr = ref this.Occupied;
						int index = num3;
						ptr[index] &= ~(1 << num4);
						block.Range.Pointer = IntPtr.Zero;
						int num5 = block.AllocatedItems * block.BytesPerItem;
						this.allocatedBytes -= (long)num5;
						block.AllocatedItems = 0;
						return 0;
					}
					return -1;
				}
			}

			// Token: 0x060000BB RID: 187 RVA: 0x000036DF File Offset: 0x000018DF
			[BurstCompile(CompileSynchronously = true)]
			[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
			public static int Try(IntPtr allocatorState, ref AllocatorManager.Block block)
			{
				return AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Invoke(allocatorState, ref block);
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000BC RID: 188 RVA: 0x000036E8 File Offset: 0x000018E8
			public AllocatorManager.TryFunction Function
			{
				get
				{
					return new AllocatorManager.TryFunction(AllocatorManager.SlabAllocator.Try);
				}
			}

			// Token: 0x060000BD RID: 189 RVA: 0x000036F6 File Offset: 0x000018F6
			public void Dispose()
			{
				this.m_handle.Rewind();
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00003703 File Offset: 0x00001903
			[BurstCompile(CompileSynchronously = true)]
			[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe static int Try$BurstManaged(IntPtr allocatorState, ref AllocatorManager.Block block)
			{
				return ((AllocatorManager.SlabAllocator*)((void*)allocatorState))->Try(ref block);
			}

			// Token: 0x04000026 RID: 38
			internal AllocatorManager.AllocatorHandle m_handle;

			// Token: 0x04000027 RID: 39
			internal AllocatorManager.Block Storage;

			// Token: 0x04000028 RID: 40
			internal int Log2SlabSizeInBytes;

			// Token: 0x04000029 RID: 41
			internal FixedList4096Bytes<int> Occupied;

			// Token: 0x0400002A RID: 42
			internal long budgetInBytes;

			// Token: 0x0400002B RID: 43
			internal long allocatedBytes;

			// Token: 0x02000028 RID: 40
			// (Invoke) Token: 0x060000C0 RID: 192
			public delegate int Try_00000A53$PostfixBurstDelegate(IntPtr allocatorState, ref AllocatorManager.Block block);

			// Token: 0x02000029 RID: 41
			internal static class Try_00000A53$BurstDirectCall
			{
				// Token: 0x060000C3 RID: 195 RVA: 0x00003711 File Offset: 0x00001911
				[BurstDiscard]
				private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Pointer == 0)
					{
						AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.DeferredCompilation, methodof(AllocatorManager.SlabAllocator.Try$BurstManaged(IntPtr, AllocatorManager.Block*)).MethodHandle, typeof(AllocatorManager.SlabAllocator.Try_00000A53$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Pointer;
				}

				// Token: 0x060000C4 RID: 196 RVA: 0x00003740 File Offset: 0x00001940
				private static IntPtr GetFunctionPointer()
				{
					IntPtr result = (IntPtr)0;
					AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.GetFunctionPointerDiscard(ref result);
					return result;
				}

				// Token: 0x060000C5 RID: 197 RVA: 0x00003758 File Offset: 0x00001958
				public unsafe static void Constructor()
				{
					AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(AllocatorManager.SlabAllocator.Try(IntPtr, AllocatorManager.Block*)).MethodHandle);
				}

				// Token: 0x060000C6 RID: 198 RVA: 0x00002C2B File Offset: 0x00000E2B
				public static void Initialize()
				{
				}

				// Token: 0x060000C7 RID: 199 RVA: 0x00003769 File Offset: 0x00001969
				// Note: this type is marked as 'beforefieldinit'.
				static Try_00000A53$BurstDirectCall()
				{
					AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.Constructor();
				}

				// Token: 0x060000C8 RID: 200 RVA: 0x00003770 File Offset: 0x00001970
				public static int Invoke(IntPtr allocatorState, ref AllocatorManager.Block block)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = AllocatorManager.SlabAllocator.Try_00000A53$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							return calli(System.Int32(System.IntPtr,Unity.Collections.AllocatorManager/Block&), allocatorState, ref block, functionPointer);
						}
					}
					return AllocatorManager.SlabAllocator.Try$BurstManaged(allocatorState, ref block);
				}

				// Token: 0x0400002C RID: 44
				private static IntPtr Pointer;

				// Token: 0x0400002D RID: 45
				private static IntPtr DeferredCompilation;
			}
		}

		// Token: 0x0200002A RID: 42
		internal struct TableEntry
		{
			// Token: 0x0400002E RID: 46
			internal IntPtr function;

			// Token: 0x0400002F RID: 47
			internal IntPtr state;
		}

		// Token: 0x0200002B RID: 43
		internal struct Array16<[IsUnmanaged] T> where T : struct, ValueType
		{
			// Token: 0x04000030 RID: 48
			internal T f0;

			// Token: 0x04000031 RID: 49
			internal T f1;

			// Token: 0x04000032 RID: 50
			internal T f2;

			// Token: 0x04000033 RID: 51
			internal T f3;

			// Token: 0x04000034 RID: 52
			internal T f4;

			// Token: 0x04000035 RID: 53
			internal T f5;

			// Token: 0x04000036 RID: 54
			internal T f6;

			// Token: 0x04000037 RID: 55
			internal T f7;

			// Token: 0x04000038 RID: 56
			internal T f8;

			// Token: 0x04000039 RID: 57
			internal T f9;

			// Token: 0x0400003A RID: 58
			internal T f10;

			// Token: 0x0400003B RID: 59
			internal T f11;

			// Token: 0x0400003C RID: 60
			internal T f12;

			// Token: 0x0400003D RID: 61
			internal T f13;

			// Token: 0x0400003E RID: 62
			internal T f14;

			// Token: 0x0400003F RID: 63
			internal T f15;
		}

		// Token: 0x0200002C RID: 44
		internal struct Array256<[IsUnmanaged] T> where T : struct, ValueType
		{
			// Token: 0x04000040 RID: 64
			internal AllocatorManager.Array16<T> f0;

			// Token: 0x04000041 RID: 65
			internal AllocatorManager.Array16<T> f1;

			// Token: 0x04000042 RID: 66
			internal AllocatorManager.Array16<T> f2;

			// Token: 0x04000043 RID: 67
			internal AllocatorManager.Array16<T> f3;

			// Token: 0x04000044 RID: 68
			internal AllocatorManager.Array16<T> f4;

			// Token: 0x04000045 RID: 69
			internal AllocatorManager.Array16<T> f5;

			// Token: 0x04000046 RID: 70
			internal AllocatorManager.Array16<T> f6;

			// Token: 0x04000047 RID: 71
			internal AllocatorManager.Array16<T> f7;

			// Token: 0x04000048 RID: 72
			internal AllocatorManager.Array16<T> f8;

			// Token: 0x04000049 RID: 73
			internal AllocatorManager.Array16<T> f9;

			// Token: 0x0400004A RID: 74
			internal AllocatorManager.Array16<T> f10;

			// Token: 0x0400004B RID: 75
			internal AllocatorManager.Array16<T> f11;

			// Token: 0x0400004C RID: 76
			internal AllocatorManager.Array16<T> f12;

			// Token: 0x0400004D RID: 77
			internal AllocatorManager.Array16<T> f13;

			// Token: 0x0400004E RID: 78
			internal AllocatorManager.Array16<T> f14;

			// Token: 0x0400004F RID: 79
			internal AllocatorManager.Array16<T> f15;
		}

		// Token: 0x0200002D RID: 45
		internal struct Array4096<[IsUnmanaged] T> where T : struct, ValueType
		{
			// Token: 0x04000050 RID: 80
			internal AllocatorManager.Array256<T> f0;

			// Token: 0x04000051 RID: 81
			internal AllocatorManager.Array256<T> f1;

			// Token: 0x04000052 RID: 82
			internal AllocatorManager.Array256<T> f2;

			// Token: 0x04000053 RID: 83
			internal AllocatorManager.Array256<T> f3;

			// Token: 0x04000054 RID: 84
			internal AllocatorManager.Array256<T> f4;

			// Token: 0x04000055 RID: 85
			internal AllocatorManager.Array256<T> f5;

			// Token: 0x04000056 RID: 86
			internal AllocatorManager.Array256<T> f6;

			// Token: 0x04000057 RID: 87
			internal AllocatorManager.Array256<T> f7;

			// Token: 0x04000058 RID: 88
			internal AllocatorManager.Array256<T> f8;

			// Token: 0x04000059 RID: 89
			internal AllocatorManager.Array256<T> f9;

			// Token: 0x0400005A RID: 90
			internal AllocatorManager.Array256<T> f10;

			// Token: 0x0400005B RID: 91
			internal AllocatorManager.Array256<T> f11;

			// Token: 0x0400005C RID: 92
			internal AllocatorManager.Array256<T> f12;

			// Token: 0x0400005D RID: 93
			internal AllocatorManager.Array256<T> f13;

			// Token: 0x0400005E RID: 94
			internal AllocatorManager.Array256<T> f14;

			// Token: 0x0400005F RID: 95
			internal AllocatorManager.Array256<T> f15;
		}

		// Token: 0x0200002E RID: 46
		internal struct Array32768<[IsUnmanaged] T> : IIndexable<T> where T : struct, ValueType
		{
			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x000037A3 File Offset: 0x000019A3
			// (set) Token: 0x060000CA RID: 202 RVA: 0x00002C2B File Offset: 0x00000E2B
			public int Length
			{
				get
				{
					return 32768;
				}
				set
				{
				}
			}

			// Token: 0x060000CB RID: 203 RVA: 0x000037AC File Offset: 0x000019AC
			public unsafe ref T ElementAt(int index)
			{
				fixed (AllocatorManager.Array4096<T>* ptr = &this.f0)
				{
					return UnsafeUtility.AsRef<T>((void*)((byte*)ptr + (IntPtr)index * (IntPtr)sizeof(T)));
				}
			}

			// Token: 0x04000060 RID: 96
			internal AllocatorManager.Array4096<T> f0;

			// Token: 0x04000061 RID: 97
			internal AllocatorManager.Array4096<T> f1;

			// Token: 0x04000062 RID: 98
			internal AllocatorManager.Array4096<T> f2;

			// Token: 0x04000063 RID: 99
			internal AllocatorManager.Array4096<T> f3;

			// Token: 0x04000064 RID: 100
			internal AllocatorManager.Array4096<T> f4;

			// Token: 0x04000065 RID: 101
			internal AllocatorManager.Array4096<T> f5;

			// Token: 0x04000066 RID: 102
			internal AllocatorManager.Array4096<T> f6;

			// Token: 0x04000067 RID: 103
			internal AllocatorManager.Array4096<T> f7;
		}

		// Token: 0x0200002F RID: 47
		internal sealed class SharedStatics
		{
			// Token: 0x060000CC RID: 204 RVA: 0x000020EA File Offset: 0x000002EA
			public SharedStatics()
			{
			}

			// Token: 0x02000030 RID: 48
			internal sealed class IsInstalled
			{
				// Token: 0x060000CD RID: 205 RVA: 0x000020EA File Offset: 0x000002EA
				public IsInstalled()
				{
				}

				// Token: 0x060000CE RID: 206 RVA: 0x000037D1 File Offset: 0x000019D1
				// Note: this type is marked as 'beforefieldinit'.
				static IsInstalled()
				{
				}

				// Token: 0x04000068 RID: 104
				internal static readonly SharedStatic<Long1024> Ref = SharedStatic<Long1024>.GetOrCreateUnsafe(0U, -4832911380680317357L, 0L);
			}

			// Token: 0x02000031 RID: 49
			internal sealed class TableEntry
			{
				// Token: 0x060000CF RID: 207 RVA: 0x000020EA File Offset: 0x000002EA
				public TableEntry()
				{
				}

				// Token: 0x060000D0 RID: 208 RVA: 0x000037F0 File Offset: 0x000019F0
				// Note: this type is marked as 'beforefieldinit'.
				static TableEntry()
				{
				}

				// Token: 0x04000069 RID: 105
				internal static readonly SharedStatic<AllocatorManager.Array32768<AllocatorManager.TableEntry>> Ref = SharedStatic<AllocatorManager.Array32768<AllocatorManager.TableEntry>>.GetOrCreateUnsafe(0U, -1297938794087215229L, 0L);
			}
		}

		// Token: 0x02000032 RID: 50
		internal static class Managed
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x0000380F File Offset: 0x00001A0F
			[NotBurstCompatible]
			public static void RegisterDelegate(int index, AllocatorManager.TryFunction function)
			{
				if (index >= 32768)
				{
					throw new ArgumentException("index to be registered in TryFunction delegate table exceeds maximum.");
				}
				AllocatorManager.Managed.TryFunctionDelegates[index] = function;
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x0000382C File Offset: 0x00001A2C
			[NotBurstCompatible]
			public static void UnregisterDelegate(int index)
			{
				if (index >= 32768)
				{
					throw new ArgumentException("index to be unregistered in TryFunction delegate table exceeds maximum.");
				}
				AllocatorManager.Managed.TryFunctionDelegates[index] = null;
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00003849 File Offset: 0x00001A49
			// Note: this type is marked as 'beforefieldinit'.
			static Managed()
			{
			}

			// Token: 0x0400006A RID: 106
			internal const int kMaxNumCustomAllocator = 32768;

			// Token: 0x0400006B RID: 107
			internal static AllocatorManager.TryFunction[] TryFunctionDelegates = new AllocatorManager.TryFunction[32768];
		}
	}
}
