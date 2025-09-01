using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000DC RID: 220
	[NativeContainer]
	[BurstCompatible]
	public struct NativeStream : IDisposable
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00018E4C File Offset: 0x0001704C
		public NativeStream(int bufferCount, AllocatorManager.AllocatorHandle allocator)
		{
			NativeStream.AllocateBlock(out this, allocator);
			this.m_Stream.AllocateForEach(bufferCount);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00018E64 File Offset: 0x00017064
		[NotBurstCompatible]
		public unsafe static JobHandle ScheduleConstruct<[IsUnmanaged] T>(out NativeStream stream, NativeList<T> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeStream.AllocateBlock(out stream, allocator);
			return new NativeStream.ConstructJobList
			{
				List = (UntypedUnsafeList*)bufferCount.GetUnsafeList(),
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00018EA4 File Offset: 0x000170A4
		[NotBurstCompatible]
		public static JobHandle ScheduleConstruct(out NativeStream stream, NativeArray<int> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator)
		{
			NativeStream.AllocateBlock(out stream, allocator);
			return new NativeStream.ConstructJob
			{
				Length = bufferCount,
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00018EDC File Offset: 0x000170DC
		public bool IsEmpty()
		{
			return this.m_Stream.IsEmpty();
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00018EE9 File Offset: 0x000170E9
		public bool IsCreated
		{
			get
			{
				return this.m_Stream.IsCreated;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00018EF6 File Offset: 0x000170F6
		public int ForEachCount
		{
			get
			{
				return this.m_Stream.ForEachCount;
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00018F03 File Offset: 0x00017103
		public NativeStream.Reader AsReader()
		{
			return new NativeStream.Reader(ref this);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00018F0B File Offset: 0x0001710B
		public NativeStream.Writer AsWriter()
		{
			return new NativeStream.Writer(ref this);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00018F13 File Offset: 0x00017113
		public int Count()
		{
			return this.m_Stream.Count();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00018F20 File Offset: 0x00017120
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public NativeArray<T> ToNativeArray<T>(AllocatorManager.AllocatorHandle allocator) where T : struct
		{
			return this.m_Stream.ToNativeArray<T>(allocator);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00018F2E File Offset: 0x0001712E
		public void Dispose()
		{
			this.m_Stream.Dispose();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00018F3B File Offset: 0x0001713B
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Stream.Dispose(inputDeps);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00018F49 File Offset: 0x00017149
		private static void AllocateBlock(out NativeStream stream, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeStream.AllocateBlock(out stream.m_Stream, allocator);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00018F57 File Offset: 0x00017157
		private void AllocateForEach(int forEachCount)
		{
			this.m_Stream.AllocateForEach(forEachCount);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00018F65 File Offset: 0x00017165
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckForEachCountGreaterThanZero(int forEachCount)
		{
			if (forEachCount <= 0)
			{
				throw new ArgumentException("foreachCount must be > 0", "foreachCount");
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadAccess()
		{
		}

		// Token: 0x040002CC RID: 716
		private UnsafeStream m_Stream;

		// Token: 0x020000DD RID: 221
		[BurstCompile]
		private struct ConstructJobList : IJob
		{
			// Token: 0x06000823 RID: 2083 RVA: 0x00018F7B File Offset: 0x0001717B
			public unsafe void Execute()
			{
				this.Container.AllocateForEach(this.List->m_length);
			}

			// Token: 0x040002CD RID: 717
			public NativeStream Container;

			// Token: 0x040002CE RID: 718
			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe UntypedUnsafeList* List;
		}

		// Token: 0x020000DE RID: 222
		[BurstCompile]
		private struct ConstructJob : IJob
		{
			// Token: 0x06000824 RID: 2084 RVA: 0x00018F93 File Offset: 0x00017193
			public void Execute()
			{
				this.Container.AllocateForEach(this.Length[0]);
			}

			// Token: 0x040002CF RID: 719
			public NativeStream Container;

			// Token: 0x040002D0 RID: 720
			[ReadOnly]
			public NativeArray<int> Length;
		}

		// Token: 0x020000DF RID: 223
		[NativeContainer]
		[NativeContainerSupportsMinMaxWriteRestriction]
		[BurstCompatible]
		public struct Writer
		{
			// Token: 0x06000825 RID: 2085 RVA: 0x00018FAC File Offset: 0x000171AC
			internal Writer(ref NativeStream stream)
			{
				this.m_Writer = stream.m_Stream.AsWriter();
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x00018FBF File Offset: 0x000171BF
			public int ForEachCount
			{
				get
				{
					return this.m_Writer.ForEachCount;
				}
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void PatchMinMaxRange(int foreEachIndex)
			{
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x00018FCC File Offset: 0x000171CC
			public void BeginForEachIndex(int foreachIndex)
			{
				this.m_Writer.BeginForEachIndex(foreachIndex);
			}

			// Token: 0x06000829 RID: 2089 RVA: 0x00018FDA File Offset: 0x000171DA
			public void EndForEachIndex()
			{
				this.m_Writer.EndForEachIndex();
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x00018FE7 File Offset: 0x000171E7
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void Write<T>(T value) where T : struct
			{
				*this.Allocate<T>() = value;
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x00018FF8 File Offset: 0x000171F8
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe ref T Allocate<T>() where T : struct
			{
				int size = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.Allocate(size));
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x00019017 File Offset: 0x00017217
			public unsafe byte* Allocate(int size)
			{
				return this.m_Writer.Allocate(size);
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckBeginForEachIndex(int foreachIndex)
			{
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckEndForEachIndex()
			{
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckAllocateSize(int size)
			{
			}

			// Token: 0x040002D1 RID: 721
			private UnsafeStream.Writer m_Writer;
		}

		// Token: 0x020000E0 RID: 224
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[BurstCompatible]
		public struct Reader
		{
			// Token: 0x06000830 RID: 2096 RVA: 0x00019025 File Offset: 0x00017225
			internal Reader(ref NativeStream stream)
			{
				this.m_Reader = stream.m_Stream.AsReader();
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x00019038 File Offset: 0x00017238
			public int BeginForEachIndex(int foreachIndex)
			{
				return this.m_Reader.BeginForEachIndex(foreachIndex);
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x00019046 File Offset: 0x00017246
			public void EndForEachIndex()
			{
				this.m_Reader.EndForEachIndex();
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000833 RID: 2099 RVA: 0x00019053 File Offset: 0x00017253
			public int ForEachCount
			{
				get
				{
					return this.m_Reader.ForEachCount;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000834 RID: 2100 RVA: 0x00019060 File Offset: 0x00017260
			public int RemainingItemCount
			{
				get
				{
					return this.m_Reader.RemainingItemCount;
				}
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x00019070 File Offset: 0x00017270
			public unsafe byte* ReadUnsafePtr(int size)
			{
				this.m_Reader.m_RemainingItemCount = this.m_Reader.m_RemainingItemCount - 1;
				byte* currentPtr = this.m_Reader.m_CurrentPtr;
				this.m_Reader.m_CurrentPtr = this.m_Reader.m_CurrentPtr + size;
				if (this.m_Reader.m_CurrentPtr != this.m_Reader.m_CurrentBlockEnd)
				{
					this.m_Reader.m_CurrentBlock = this.m_Reader.m_CurrentBlock->Next;
					this.m_Reader.m_CurrentPtr = &this.m_Reader.m_CurrentBlock->Data.FixedElementField;
					this.m_Reader.m_CurrentBlockEnd = (byte*)(this.m_Reader.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
					currentPtr = this.m_Reader.m_CurrentPtr;
					this.m_Reader.m_CurrentPtr = this.m_Reader.m_CurrentPtr + size;
				}
				return currentPtr;
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x00019138 File Offset: 0x00017338
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe ref T Read<T>() where T : struct
			{
				int size = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.ReadUnsafePtr(size));
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x00019157 File Offset: 0x00017357
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public ref T Peek<T>() where T : struct
			{
				UnsafeUtility.SizeOf<T>();
				return this.m_Reader.Peek<T>();
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x0001916A File Offset: 0x0001736A
			public int Count()
			{
				return this.m_Reader.Count();
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckNotReadingOutOfBounds(int size)
			{
			}

			// Token: 0x0600083A RID: 2106 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckRead()
			{
			}

			// Token: 0x0600083B RID: 2107 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckReadSize(int size)
			{
			}

			// Token: 0x0600083C RID: 2108 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckBeginForEachIndex(int forEachIndex)
			{
			}

			// Token: 0x0600083D RID: 2109 RVA: 0x00019177 File Offset: 0x00017377
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckEndForEachIndex()
			{
				if (this.m_Reader.m_RemainingItemCount != 0)
				{
					throw new ArgumentException("Not all elements (Count) have been read. If this is intentional, simply skip calling EndForEachIndex();");
				}
				if (this.m_Reader.m_CurrentBlockEnd != this.m_Reader.m_CurrentPtr)
				{
					throw new ArgumentException("Not all data (Data Size) has been read. If this is intentional, simply skip calling EndForEachIndex();");
				}
			}

			// Token: 0x040002D2 RID: 722
			private UnsafeStream.Reader m_Reader;
		}
	}
}
