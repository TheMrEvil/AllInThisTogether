using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000140 RID: 320
	public static class HashSetExtensions
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x00022060 File Offset: 0x00020260
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000220B4 File Offset: 0x000202B4
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002213C File Offset: 0x0002033C
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00022190 File Offset: 0x00020390
		public static void ExceptWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000221E4 File Offset: 0x000203E4
		public static void IntersectWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002226C File Offset: 0x0002046C
		public static void UnionWith<[IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000222C0 File Offset: 0x000204C0
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00022314 File Offset: 0x00020514
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002239C File Offset: 0x0002059C
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000223F0 File Offset: 0x000205F0
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00022444 File Offset: 0x00020644
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B86 RID: 2950 RVA: 0x000224CC File Offset: 0x000206CC
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00022520 File Offset: 0x00020720
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00022574 File Offset: 0x00020774
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B89 RID: 2953 RVA: 0x000225FC File Offset: 0x000207FC
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00022650 File Offset: 0x00020850
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000226A4 File Offset: 0x000208A4
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002272C File Offset: 0x0002092C
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00022780 File Offset: 0x00020980
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000227D4 File Offset: 0x000209D4
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002285C File Offset: 0x00020A5C
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000228B0 File Offset: 0x00020AB0
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00022904 File Offset: 0x00020B04
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002298C File Offset: 0x00020B8C
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000229E0 File Offset: 0x00020BE0
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00022A34 File Offset: 0x00020C34
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B95 RID: 2965 RVA: 0x00022ABC File Offset: 0x00020CBC
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00022B10 File Offset: 0x00020D10
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00022B64 File Offset: 0x00020D64
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B98 RID: 2968 RVA: 0x00022BEC File Offset: 0x00020DEC
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00022C40 File Offset: 0x00020E40
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00022C94 File Offset: 0x00020E94
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B9B RID: 2971 RVA: 0x00022D1C File Offset: 0x00020F1C
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00022D70 File Offset: 0x00020F70
		public static void ExceptWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00022DC4 File Offset: 0x00020FC4
		public static void IntersectWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000B9E RID: 2974 RVA: 0x00022E4C File Offset: 0x0002104C
		public static void UnionWith<[IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}
	}
}
