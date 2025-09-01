using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000E1 RID: 225
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	[BurstCompatible]
	public struct NativeText : INativeList<byte>, IIndexable<byte>, INativeDisposable, IDisposable, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<NativeText>, IEquatable<NativeText>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x000191B4 File Offset: 0x000173B4
		[NotBurstCompatible]
		public NativeText(string source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000191C4 File Offset: 0x000173C4
		[NotBurstCompatible]
		public unsafe NativeText(string source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(source.Length * 2, allocator);
			this.Length = source.Length * 2;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int length;
				if (UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out length, this.Capacity, ptr, source.Length) != CopyError.None)
				{
					this.m_Data->Dispose();
					void* data = ref allocator.Allocate(sizeof(UnsafeText), 16, 1);
					this.m_Data = (UnsafeText*)data;
					*this.m_Data = default(UnsafeText);
				}
				this.Length = length;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00019254 File Offset: 0x00017454
		private unsafe NativeText(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeText);
			void* data = ref allocator.Allocate(sizeof(UnsafeText), 16, 1);
			this.m_Data = (UnsafeText*)data;
			*this.m_Data = new UnsafeText(capacity, allocator);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00019292 File Offset: 0x00017492
		public NativeText(int capacity, Allocator allocator)
		{
			this = new NativeText(capacity, allocator);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000192A1 File Offset: 0x000174A1
		public NativeText(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(capacity, allocator, 2);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000192AC File Offset: 0x000174AC
		public NativeText(Allocator allocator)
		{
			this = new NativeText(allocator);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000192BA File Offset: 0x000174BA
		public NativeText(AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(512, allocator);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000192C8 File Offset: 0x000174C8
		public unsafe NativeText(in FixedString32Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* source2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)source2, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00019312 File Offset: 0x00017512
		public NativeText(in FixedString32Bytes source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00019324 File Offset: 0x00017524
		public unsafe NativeText(in FixedString64Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* source2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)source2, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001936E File Offset: 0x0001756E
		public NativeText(in FixedString64Bytes source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00019380 File Offset: 0x00017580
		public unsafe NativeText(in FixedString128Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* source2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)source2, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000193CA File Offset: 0x000175CA
		public NativeText(in FixedString128Bytes source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000193DC File Offset: 0x000175DC
		public unsafe NativeText(in FixedString512Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* source2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)source2, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00019426 File Offset: 0x00017626
		public NativeText(in FixedString512Bytes source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00019438 File Offset: 0x00017638
		public unsafe NativeText(in FixedString4096Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* source2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)source2, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00019482 File Offset: 0x00017682
		public NativeText(in FixedString4096Bytes source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00019491 File Offset: 0x00017691
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0001949E File Offset: 0x0001769E
		public unsafe int Length
		{
			get
			{
				return this.m_Data->Length;
			}
			set
			{
				this.m_Data->Length = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000194AC File Offset: 0x000176AC
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x000194B9 File Offset: 0x000176B9
		public unsafe int Capacity
		{
			get
			{
				return this.m_Data->Capacity;
			}
			set
			{
				this.m_Data->Capacity = value;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000194C7 File Offset: 0x000176C7
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			this.Length = newLength;
			return true;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000194D1 File Offset: 0x000176D1
		public unsafe bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_Data->IsEmpty;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000194E8 File Offset: 0x000176E8
		public bool IsCreated
		{
			get
			{
				return this.m_Data != null;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000194F7 File Offset: 0x000176F7
		public unsafe byte* GetUnsafePtr()
		{
			return this.m_Data->GetUnsafePtr();
		}

		// Token: 0x170000E9 RID: 233
		public unsafe byte this[int index]
		{
			get
			{
				return *this.m_Data->ElementAt(index);
			}
			set
			{
				*this.m_Data->ElementAt(index) = value;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00019523 File Offset: 0x00017723
		public unsafe ref byte ElementAt(int index)
		{
			return this.m_Data->ElementAt(index);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00019531 File Offset: 0x00017731
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001953C File Offset: 0x0001773C
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00019562 File Offset: 0x00017762
		public unsafe int CompareTo(NativeText other)
		{
			return ref this.CompareTo(*other.m_Data);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00019570 File Offset: 0x00017770
		public unsafe bool Equals(NativeText other)
		{
			return ref this.Equals(*other.m_Data);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001957E File Offset: 0x0001777E
		public int CompareTo(NativeText.ReadOnly other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00019588 File Offset: 0x00017788
		public unsafe bool Equals(NativeText.ReadOnly other)
		{
			return ref this.Equals(*other.m_Data);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00019596 File Offset: 0x00017796
		public unsafe void Dispose()
		{
			AllocatorManager.AllocatorHandle allocator = this.m_Data->m_UntypedListData.Allocator;
			this.m_Data->Dispose();
			AllocatorManager.Free<UnsafeText>(allocator, this.m_Data, 1);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000195BF File Offset: 0x000177BF
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data->Dispose(inputDeps);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000195CD File Offset: 0x000177CD
		[CreateProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotBurstCompatible]
		public string Value
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000195DB File Offset: 0x000177DB
		public NativeText.Enumerator GetEnumerator()
		{
			return new NativeText.Enumerator(this);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000195E8 File Offset: 0x000177E8
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000195FC File Offset: 0x000177FC
		[NotBurstCompatible]
		public bool Equals(string other)
		{
			return this.ToString().Equals(other);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00019610 File Offset: 0x00017810
		public int CompareTo(FixedString32Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001961C File Offset: 0x0001781C
		public unsafe static bool operator ==(in NativeText a, in FixedString32Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001965F File Offset: 0x0001785F
		public static bool operator !=(in NativeText a, in FixedString32Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001966B File Offset: 0x0001786B
		public bool Equals(FixedString32Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00019675 File Offset: 0x00017875
		public int CompareTo(FixedString64Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00019680 File Offset: 0x00017880
		public unsafe static bool operator ==(in NativeText a, in FixedString64Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000196C3 File Offset: 0x000178C3
		public static bool operator !=(in NativeText a, in FixedString64Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000196CF File Offset: 0x000178CF
		public bool Equals(FixedString64Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000196D9 File Offset: 0x000178D9
		public int CompareTo(FixedString128Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000196E4 File Offset: 0x000178E4
		public unsafe static bool operator ==(in NativeText a, in FixedString128Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00019727 File Offset: 0x00017927
		public static bool operator !=(in NativeText a, in FixedString128Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00019733 File Offset: 0x00017933
		public bool Equals(FixedString128Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001973D File Offset: 0x0001793D
		public int CompareTo(FixedString512Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00019748 File Offset: 0x00017948
		public unsafe static bool operator ==(in NativeText a, in FixedString512Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001978B File Offset: 0x0001798B
		public static bool operator !=(in NativeText a, in FixedString512Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00019797 File Offset: 0x00017997
		public bool Equals(FixedString512Bytes other)
		{
			return this == other;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000197A1 File Offset: 0x000179A1
		public int CompareTo(FixedString4096Bytes other)
		{
			return ref this.CompareTo(other);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000197AC File Offset: 0x000179AC
		public unsafe static bool operator ==(in NativeText a, in FixedString4096Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000197EF File Offset: 0x000179EF
		public static bool operator !=(in NativeText a, in FixedString4096Bytes b)
		{
			return !(a == b);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000197FB File Offset: 0x000179FB
		public bool Equals(FixedString4096Bytes other)
		{
			return this == other;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00019805 File Offset: 0x00017A05
		[NotBurstCompatible]
		public override string ToString()
		{
			if (this.m_Data == null)
			{
				return "";
			}
			return ref this.ConvertToString<NativeText>();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001981D File Offset: 0x00017A1D
		public override int GetHashCode()
		{
			return ref this.ComputeHashCode<NativeText>();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00019828 File Offset: 0x00017A28
		[NotBurstCompatible]
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			string text = other as string;
			if (text != null)
			{
				return this.Equals(text);
			}
			if (other is NativeText)
			{
				NativeText other2 = (NativeText)other;
				return this.Equals(other2);
			}
			if (other is NativeText.ReadOnly)
			{
				NativeText.ReadOnly other3 = (NativeText.ReadOnly)other;
				return this.Equals(other3);
			}
			if (other is FixedString32Bytes)
			{
				FixedString32Bytes other4 = (FixedString32Bytes)other;
				return this.Equals(other4);
			}
			if (other is FixedString64Bytes)
			{
				FixedString64Bytes other5 = (FixedString64Bytes)other;
				return this.Equals(other5);
			}
			if (other is FixedString128Bytes)
			{
				FixedString128Bytes other6 = (FixedString128Bytes)other;
				return this.Equals(other6);
			}
			if (other is FixedString512Bytes)
			{
				FixedString512Bytes other7 = (FixedString512Bytes)other;
				return this.Equals(other7);
			}
			if (other is FixedString4096Bytes)
			{
				FixedString4096Bytes other8 = (FixedString4096Bytes)other;
				return this.Equals(other8);
			}
			return false;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000198F6 File Offset: 0x00017AF6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal unsafe static void CheckNull(void* dataPtr)
		{
			if (dataPtr == null)
			{
				throw new Exception("NativeText has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteAndBumpSecondaryVersion()
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00019908 File Offset: 0x00017B08
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in NativeText of {1} length.", index, this.Length));
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00019959 File Offset: 0x00017B59
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("NativeText: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00019971 File Offset: 0x00017B71
		public NativeText.ReadOnly AsReadOnly()
		{
			return new NativeText.ReadOnly(this.m_Data);
		}

		// Token: 0x040002D3 RID: 723
		[NativeDisableUnsafePtrRestriction]
		private unsafe UnsafeText* m_Data;

		// Token: 0x020000E2 RID: 226
		public struct Enumerator : IEnumerator<Unicode.Rune>, IEnumerator, IDisposable
		{
			// Token: 0x06000884 RID: 2180 RVA: 0x0001997E File Offset: 0x00017B7E
			public Enumerator(NativeText source)
			{
				this.target = source.AsReadOnly();
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x000199A0 File Offset: 0x00017BA0
			public Enumerator(NativeText.ReadOnly source)
			{
				this.target = source;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x000199BC File Offset: 0x00017BBC
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x000199FC File Offset: 0x00017BFC
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000889 RID: 2185 RVA: 0x00019A11 File Offset: 0x00017C11
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x00019A1E File Offset: 0x00017C1E
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x040002D4 RID: 724
			private NativeText.ReadOnly target;

			// Token: 0x040002D5 RID: 725
			private int offset;

			// Token: 0x040002D6 RID: 726
			private Unicode.Rune current;
		}

		// Token: 0x020000E3 RID: 227
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct ReadOnly : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<NativeText>, IEquatable<NativeText>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
		{
			// Token: 0x0600088B RID: 2187 RVA: 0x00019A26 File Offset: 0x00017C26
			internal unsafe ReadOnly(UnsafeText* text)
			{
				this.m_Data = text;
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600088C RID: 2188 RVA: 0x00019A2F File Offset: 0x00017C2F
			// (set) Token: 0x0600088D RID: 2189 RVA: 0x00002C2B File Offset: 0x00000E2B
			public unsafe int Capacity
			{
				get
				{
					return this.m_Data->Capacity;
				}
				set
				{
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x00019A3C File Offset: 0x00017C3C
			// (set) Token: 0x0600088F RID: 2191 RVA: 0x00002C2B File Offset: 0x00000E2B
			public unsafe bool IsEmpty
			{
				get
				{
					return this.m_Data == null || this.m_Data->IsEmpty;
				}
				set
				{
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000890 RID: 2192 RVA: 0x00019A55 File Offset: 0x00017C55
			// (set) Token: 0x06000891 RID: 2193 RVA: 0x00002C2B File Offset: 0x00000E2B
			public unsafe int Length
			{
				get
				{
					return this.m_Data->Length;
				}
				set
				{
				}
			}

			// Token: 0x170000F0 RID: 240
			public unsafe byte this[int index]
			{
				get
				{
					return *this.m_Data->ElementAt(index);
				}
				set
				{
				}
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Clear()
			{
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x00019A71 File Offset: 0x00017C71
			public ref byte ElementAt(int index)
			{
				throw new NotSupportedException("Trying to retrieve non-readonly ref to NativeText.ReadOnly data. This is not permitted.");
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x00019A7D File Offset: 0x00017C7D
			public unsafe byte* GetUnsafePtr()
			{
				return this.m_Data->GetUnsafePtr();
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x00019A8A File Offset: 0x00017C8A
			public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
			{
				return false;
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x00019A8D File Offset: 0x00017C8D
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			internal unsafe static void CheckNull(void* dataPtr)
			{
				if (dataPtr == null)
				{
					throw new Exception("NativeText.ReadOnly has yet to be created or has been destroyed!");
				}
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckRead()
			{
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void ErrorWrite()
			{
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x00019A9F File Offset: 0x00017C9F
			[NotBurstCompatible]
			public unsafe int CompareTo(string other)
			{
				return this.m_Data->ToString().CompareTo(other);
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x00019AB8 File Offset: 0x00017CB8
			[NotBurstCompatible]
			public unsafe bool Equals(string other)
			{
				return this.m_Data->ToString().Equals(other);
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x00019AD1 File Offset: 0x00017CD1
			public unsafe int CompareTo(NativeText.ReadOnly other)
			{
				return ref *this.m_Data.CompareTo(*other.m_Data);
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x00019AE4 File Offset: 0x00017CE4
			public unsafe bool Equals(NativeText.ReadOnly other)
			{
				return ref *this.m_Data.Equals(*other.m_Data);
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x00019AF7 File Offset: 0x00017CF7
			public unsafe int CompareTo(NativeText other)
			{
				return ref this.CompareTo(*other.m_Data);
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x00019B05 File Offset: 0x00017D05
			public unsafe bool Equals(NativeText other)
			{
				return ref this.Equals(*other.m_Data);
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x00019B13 File Offset: 0x00017D13
			public int CompareTo(FixedString32Bytes other)
			{
				return ref this.CompareTo(other);
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x00019B20 File Offset: 0x00017D20
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString32Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00019B63 File Offset: 0x00017D63
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString32Bytes b)
			{
				return !(a == b);
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x00019B6F File Offset: 0x00017D6F
			public bool Equals(FixedString32Bytes other)
			{
				return this == other;
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x00019B79 File Offset: 0x00017D79
			public int CompareTo(FixedString64Bytes other)
			{
				return ref this.CompareTo(other);
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x00019B84 File Offset: 0x00017D84
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString64Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x00019BC7 File Offset: 0x00017DC7
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString64Bytes b)
			{
				return !(a == b);
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x00019BD3 File Offset: 0x00017DD3
			public bool Equals(FixedString64Bytes other)
			{
				return this == other;
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x00019BDD File Offset: 0x00017DDD
			public int CompareTo(FixedString128Bytes other)
			{
				return ref this.CompareTo(other);
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x00019BE8 File Offset: 0x00017DE8
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString128Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x00019C2B File Offset: 0x00017E2B
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString128Bytes b)
			{
				return !(a == b);
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x00019C37 File Offset: 0x00017E37
			public bool Equals(FixedString128Bytes other)
			{
				return this == other;
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x00019C41 File Offset: 0x00017E41
			public int CompareTo(FixedString512Bytes other)
			{
				return ref this.CompareTo(other);
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x00019C4C File Offset: 0x00017E4C
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString512Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00019C8F File Offset: 0x00017E8F
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString512Bytes b)
			{
				return !(a == b);
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x00019C9B File Offset: 0x00017E9B
			public bool Equals(FixedString512Bytes other)
			{
				return this == other;
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x00019CA5 File Offset: 0x00017EA5
			public int CompareTo(FixedString4096Bytes other)
			{
				return ref this.CompareTo(other);
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x00019CB0 File Offset: 0x00017EB0
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString4096Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* bBytes = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00019CF3 File Offset: 0x00017EF3
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString4096Bytes b)
			{
				return !(a == b);
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x00019CFF File Offset: 0x00017EFF
			public bool Equals(FixedString4096Bytes other)
			{
				return this == other;
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x00019D09 File Offset: 0x00017F09
			[NotBurstCompatible]
			public override string ToString()
			{
				if (this.m_Data == null)
				{
					return "";
				}
				return ref this.ConvertToString<NativeText.ReadOnly>();
			}

			// Token: 0x060008B6 RID: 2230 RVA: 0x00019D21 File Offset: 0x00017F21
			public override int GetHashCode()
			{
				return ref this.ComputeHashCode<NativeText.ReadOnly>();
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x00019D2C File Offset: 0x00017F2C
			[NotBurstCompatible]
			public override bool Equals(object other)
			{
				if (other == null)
				{
					return false;
				}
				string text = other as string;
				if (text != null)
				{
					return this.Equals(text);
				}
				if (other is NativeText)
				{
					NativeText other2 = (NativeText)other;
					return this.Equals(other2);
				}
				if (other is NativeText.ReadOnly)
				{
					NativeText.ReadOnly other3 = (NativeText.ReadOnly)other;
					return this.Equals(other3);
				}
				if (other is FixedString32Bytes)
				{
					FixedString32Bytes other4 = (FixedString32Bytes)other;
					return this.Equals(other4);
				}
				if (other is FixedString64Bytes)
				{
					FixedString64Bytes other5 = (FixedString64Bytes)other;
					return this.Equals(other5);
				}
				if (other is FixedString128Bytes)
				{
					FixedString128Bytes other6 = (FixedString128Bytes)other;
					return this.Equals(other6);
				}
				if (other is FixedString512Bytes)
				{
					FixedString512Bytes other7 = (FixedString512Bytes)other;
					return this.Equals(other7);
				}
				if (other is FixedString4096Bytes)
				{
					FixedString4096Bytes other8 = (FixedString4096Bytes)other;
					return this.Equals(other8);
				}
				return false;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00019DFA File Offset: 0x00017FFA
			[CreateProperty]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[NotBurstCompatible]
			public string Value
			{
				get
				{
					return this.ToString();
				}
			}

			// Token: 0x060008B9 RID: 2233 RVA: 0x00019E08 File Offset: 0x00018008
			public NativeText.Enumerator GetEnumerator()
			{
				return new NativeText.Enumerator(this);
			}

			// Token: 0x040002D7 RID: 727
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeText* m_Data;
		}
	}
}
