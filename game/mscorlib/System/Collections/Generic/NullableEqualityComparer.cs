using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC7 RID: 2759
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x06006298 RID: 25240 RVA: 0x0014A0B1 File Offset: 0x001482B1
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x0014A0EC File Offset: 0x001482EC
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x0014A0FC File Offset: 0x001482FC
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x0014A174 File Offset: 0x00148374
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x0014A1EB File Offset: 0x001483EB
		public override bool Equals(object obj)
		{
			return obj is NullableEqualityComparer<T>;
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x00149C82 File Offset: 0x00147E82
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x0014A1F6 File Offset: 0x001483F6
		public NullableEqualityComparer()
		{
		}
	}
}
