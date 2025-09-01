using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000108 RID: 264
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	[DebuggerTypeProxy(typeof(WordStorageDebugView))]
	public struct WordStorage
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001DA68 File Offset: 0x0001BC68
		[NotBurstCompatible]
		public static ref WordStorage Instance
		{
			get
			{
				WordStorage.Initialize();
				return ref WordStorageStatic.Ref.Data;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001DA79 File Offset: 0x0001BC79
		public int Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001DA84 File Offset: 0x0001BC84
		[NotBurstCompatible]
		public static void Initialize()
		{
			if (WordStorageStatic.Ref.Data.buffer.IsCreated)
			{
				return;
			}
			WordStorageStatic.Ref.Data.buffer = new NativeArray<byte>(2097152, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			WordStorageStatic.Ref.Data.entry = new NativeArray<WordStorage.Entry>(16384, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			WordStorageStatic.Ref.Data.hash = new NativeParallelMultiHashMap<int, int>(16384, Allocator.Persistent);
			WordStorage.Clear();
			AppDomain.CurrentDomain.DomainUnload += delegate(object _, EventArgs __)
			{
				WordStorage.Shutdown();
			};
			AppDomain.CurrentDomain.ProcessExit += delegate(object _, EventArgs __)
			{
				WordStorage.Shutdown();
			};
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001DB54 File Offset: 0x0001BD54
		[NotBurstCompatible]
		public static void Shutdown()
		{
			if (!WordStorageStatic.Ref.Data.buffer.IsCreated)
			{
				return;
			}
			WordStorageStatic.Ref.Data.buffer.Dispose();
			WordStorageStatic.Ref.Data.entry.Dispose();
			WordStorageStatic.Ref.Data.hash.Dispose();
			WordStorageStatic.Ref.Data = default(WordStorage);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		[NotBurstCompatible]
		public static void Clear()
		{
			WordStorage.Initialize();
			WordStorageStatic.Ref.Data.chars = 0;
			WordStorageStatic.Ref.Data.entries = 0;
			WordStorageStatic.Ref.Data.hash.Clear();
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			WordStorageStatic.Ref.Data.GetOrCreateIndex<FixedString32Bytes>(ref fixedString32Bytes);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001DC24 File Offset: 0x0001BE24
		[NotBurstCompatible]
		public static void Setup()
		{
			WordStorage.Clear();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public unsafe void GetFixedString<T>(int index, ref T temp) where T : IUTF8Bytes, INativeList<byte>
		{
			WordStorage.Entry entry = this.entry[index];
			temp.Length = entry.length;
			UnsafeUtility.MemCpy((void*)temp.GetUnsafePtr(), (void*)((byte*)this.buffer.GetUnsafePtr<byte>() + entry.offset), (long)temp.Length);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001DC88 File Offset: 0x0001BE88
		public int GetIndexFromHashAndFixedString<T>(int h, ref T temp) where T : IUTF8Bytes, INativeList<byte>
		{
			int num;
			NativeParallelMultiHashMapIterator<int> nativeParallelMultiHashMapIterator;
			if (this.hash.TryGetFirstValue(h, out num, out nativeParallelMultiHashMapIterator))
			{
				for (;;)
				{
					WordStorage.Entry entry = this.entry[num];
					if (entry.length == temp.Length)
					{
						int num2 = 0;
						while (num2 < entry.length && temp[num2] == this.buffer[entry.offset + num2])
						{
							num2++;
						}
						if (num2 == temp.Length)
						{
							break;
						}
					}
					if (!this.hash.TryGetNextValue(out num, ref nativeParallelMultiHashMapIterator))
					{
						return -1;
					}
				}
				return num;
			}
			return -1;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001DD20 File Offset: 0x0001BF20
		public bool Contains<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			int hashCode = value.GetHashCode();
			return this.GetIndexFromHashAndFixedString<T>(hashCode, ref value) != -1;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001DD48 File Offset: 0x0001BF48
		[NotBurstCompatible]
		public bool Contains(string value)
		{
			FixedString512Bytes fixedString512Bytes = value;
			return this.Contains<FixedString512Bytes>(ref fixedString512Bytes);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public int GetOrCreateIndex<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			int hashCode = value.GetHashCode();
			int indexFromHashAndFixedString = this.GetIndexFromHashAndFixedString<T>(hashCode, ref value);
			if (indexFromHashAndFixedString != -1)
			{
				return indexFromHashAndFixedString;
			}
			int offset = this.chars;
			ushort num = (ushort)value.Length;
			int num2;
			for (int i = 0; i < (int)num; i++)
			{
				num2 = this.chars;
				this.chars = num2 + 1;
				this.buffer[num2] = value[i];
			}
			this.entry[this.entries] = new WordStorage.Entry
			{
				offset = offset,
				length = (int)num
			};
			this.hash.Add(hashCode, this.entries);
			num2 = this.entries;
			this.entries = num2 + 1;
			return num2;
		}

		// Token: 0x0400033E RID: 830
		private NativeArray<byte> buffer;

		// Token: 0x0400033F RID: 831
		private NativeArray<WordStorage.Entry> entry;

		// Token: 0x04000340 RID: 832
		private NativeParallelMultiHashMap<int, int> hash;

		// Token: 0x04000341 RID: 833
		private int chars;

		// Token: 0x04000342 RID: 834
		private int entries;

		// Token: 0x04000343 RID: 835
		private const int kMaxEntries = 16384;

		// Token: 0x04000344 RID: 836
		private const int kMaxChars = 2097152;

		// Token: 0x04000345 RID: 837
		public const int kMaxCharsPerEntry = 4096;

		// Token: 0x02000109 RID: 265
		private struct Entry
		{
			// Token: 0x04000346 RID: 838
			public int offset;

			// Token: 0x04000347 RID: 839
			public int length;
		}

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060009E3 RID: 2531 RVA: 0x0001DE30 File Offset: 0x0001C030
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x000020EA File Offset: 0x000002EA
			public <>c()
			{
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x0001DE3C File Offset: 0x0001C03C
			internal void <Initialize>b__13_0(object _, EventArgs __)
			{
				WordStorage.Shutdown();
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x0001DE3C File Offset: 0x0001C03C
			internal void <Initialize>b__13_1(object _, EventArgs __)
			{
				WordStorage.Shutdown();
			}

			// Token: 0x04000348 RID: 840
			public static readonly WordStorage.<>c <>9 = new WordStorage.<>c();

			// Token: 0x04000349 RID: 841
			public static EventHandler <>9__13_0;

			// Token: 0x0400034A RID: 842
			public static EventHandler <>9__13_1;
		}
	}
}
