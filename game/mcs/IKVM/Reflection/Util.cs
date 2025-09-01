using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000075 RID: 117
	internal static class Util
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00013A54 File Offset: 0x00011C54
		internal static int[] Copy(int[] array)
		{
			if (array == null || array.Length == 0)
			{
				return Empty<int>.Array;
			}
			int[] array2 = new int[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00013A84 File Offset: 0x00011C84
		internal static Type[] Copy(Type[] array)
		{
			if (array == null || array.Length == 0)
			{
				return Type.EmptyTypes;
			}
			Type[] array2 = new Type[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00013AB4 File Offset: 0x00011CB4
		internal static T[] ToArray<T, V>(List<V> list, T[] empty) where V : T
		{
			if (list == null || list.Count == 0)
			{
				return empty;
			}
			T[] array = new T[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)list[i]);
			}
			return array;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00013B01 File Offset: 0x00011D01
		internal static T[] ToArray<T>(IEnumerable<T> values)
		{
			if (values != null)
			{
				return new List<T>(values).ToArray();
			}
			return Empty<T>.Array;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00013B18 File Offset: 0x00011D18
		internal static bool ArrayEquals(Type[] t1, Type[] t2)
		{
			if (t1 == t2)
			{
				return true;
			}
			if (t1 == null)
			{
				return t2.Length == 0;
			}
			if (t2 == null)
			{
				return t1.Length == 0;
			}
			if (t1.Length == t2.Length)
			{
				for (int i = 0; i < t1.Length; i++)
				{
					if (!Util.TypeEquals(t1[i], t2[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00013B65 File Offset: 0x00011D65
		internal static bool TypeEquals(Type t1, Type t2)
		{
			return t1 == t2 || (!(t1 == null) && t1.Equals(t2));
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00013B84 File Offset: 0x00011D84
		internal static int GetHashCode(Type[] types)
		{
			if (types == null)
			{
				return 0;
			}
			int num = 0;
			foreach (Type type in types)
			{
				if (type != null)
				{
					num *= 3;
					num ^= type.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00013BC4 File Offset: 0x00011DC4
		internal static bool ArrayEquals(CustomModifiers[] m1, CustomModifiers[] m2)
		{
			if (m1 == null || m2 == null)
			{
				return m1 == m2;
			}
			if (m1.Length != m2.Length)
			{
				return false;
			}
			for (int i = 0; i < m1.Length; i++)
			{
				if (!m1[i].Equals(m2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00013C0C File Offset: 0x00011E0C
		internal static int GetHashCode(CustomModifiers[] mods)
		{
			int num = 0;
			if (mods != null)
			{
				foreach (CustomModifiers customModifiers in mods)
				{
					num ^= customModifiers.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00013C48 File Offset: 0x00011E48
		internal static T NullSafeElementAt<T>(T[] array, int index)
		{
			if (array != null)
			{
				return array[index];
			}
			return default(T);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00013C69 File Offset: 0x00011E69
		internal static int NullSafeLength<T>(T[] array)
		{
			if (array != null)
			{
				return array.Length;
			}
			return 0;
		}
	}
}
