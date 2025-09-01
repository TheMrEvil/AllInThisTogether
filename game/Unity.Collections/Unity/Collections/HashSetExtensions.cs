using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000C1 RID: 193
	public static class HashSetExtensions
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00016694 File Offset: 0x00014894
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000166E8 File Offset: 0x000148E8
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00016770 File Offset: 0x00014970
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000167C4 File Offset: 0x000149C4
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00016818 File Offset: 0x00014A18
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000168A0 File Offset: 0x00014AA0
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000168F4 File Offset: 0x00014AF4
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00016948 File Offset: 0x00014B48
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000169D0 File Offset: 0x00014BD0
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00016A24 File Offset: 0x00014C24
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00016A78 File Offset: 0x00014C78
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00016B00 File Offset: 0x00014D00
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00016B54 File Offset: 0x00014D54
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00016BA8 File Offset: 0x00014DA8
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00016C30 File Offset: 0x00014E30
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00016C84 File Offset: 0x00014E84
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00016CD8 File Offset: 0x00014ED8
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00016D60 File Offset: 0x00014F60
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00016DB4 File Offset: 0x00014FB4
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00016E08 File Offset: 0x00015008
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00016E90 File Offset: 0x00015090
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00016EE4 File Offset: 0x000150E4
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00016F38 File Offset: 0x00015138
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> other2 = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					other2.Add(item);
				}
			}
			container.Clear();
			container.UnionWith(other2);
			other2.Dispose();
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00016FC0 File Offset: 0x000151C0
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}
	}
}
