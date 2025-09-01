using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000219 RID: 537
	public class DefiniteAssignmentBitSet
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x00084A47 File Offset: 0x00082C47
		public DefiniteAssignmentBitSet(int length)
		{
			if (length > 31)
			{
				this.large_bits = new int[(length + 31) / 32];
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00084A68 File Offset: 0x00082C68
		public DefiniteAssignmentBitSet(DefiniteAssignmentBitSet source)
		{
			if (source.large_bits != null)
			{
				this.large_bits = source.large_bits;
				this.bits = (source.bits | 2147483648U);
				return;
			}
			this.bits = (source.bits & 2147483647U);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00084AB4 File Offset: 0x00082CB4
		public static DefiniteAssignmentBitSet operator &(DefiniteAssignmentBitSet a, DefiniteAssignmentBitSet b)
		{
			if (DefiniteAssignmentBitSet.AreEqual(a, b))
			{
				return a;
			}
			DefiniteAssignmentBitSet definiteAssignmentBitSet;
			if (a.large_bits == null)
			{
				definiteAssignmentBitSet = new DefiniteAssignmentBitSet(a);
				definiteAssignmentBitSet.bits &= (b.bits & 2147483647U);
				return definiteAssignmentBitSet;
			}
			definiteAssignmentBitSet = new DefiniteAssignmentBitSet(a);
			definiteAssignmentBitSet.Clone();
			int[] array = definiteAssignmentBitSet.large_bits;
			int[] array2 = b.large_bits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] &= array2[i];
			}
			return definiteAssignmentBitSet;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00084B30 File Offset: 0x00082D30
		public static DefiniteAssignmentBitSet operator |(DefiniteAssignmentBitSet a, DefiniteAssignmentBitSet b)
		{
			if (DefiniteAssignmentBitSet.AreEqual(a, b))
			{
				return a;
			}
			DefiniteAssignmentBitSet definiteAssignmentBitSet;
			if (a.large_bits == null)
			{
				definiteAssignmentBitSet = new DefiniteAssignmentBitSet(a);
				definiteAssignmentBitSet.bits |= b.bits;
				definiteAssignmentBitSet.bits &= 2147483647U;
				return definiteAssignmentBitSet;
			}
			definiteAssignmentBitSet = new DefiniteAssignmentBitSet(a);
			definiteAssignmentBitSet.Clone();
			int[] array = definiteAssignmentBitSet.large_bits;
			int[] array2 = b.large_bits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] |= array2[i];
			}
			return definiteAssignmentBitSet;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00084BB8 File Offset: 0x00082DB8
		public static DefiniteAssignmentBitSet And(List<DefiniteAssignmentBitSet> das)
		{
			if (das.Count == 0)
			{
				throw new ArgumentException("Empty das");
			}
			DefiniteAssignmentBitSet definiteAssignmentBitSet = das[0];
			for (int i = 1; i < das.Count; i++)
			{
				definiteAssignmentBitSet &= das[i];
			}
			return definiteAssignmentBitSet;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00084C00 File Offset: 0x00082E00
		private bool CopyOnWrite
		{
			get
			{
				return (this.bits & 2147483648U) > 0U;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00084C11 File Offset: 0x00082E11
		private int Length
		{
			get
			{
				if (this.large_bits != null)
				{
					return this.large_bits.Length * 32;
				}
				return 31;
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00084C29 File Offset: 0x00082E29
		public void Set(int index)
		{
			if (this.CopyOnWrite && !this[index])
			{
				this.Clone();
			}
			this.SetBit(index);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00084C4C File Offset: 0x00082E4C
		public void Set(int index, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (this.CopyOnWrite && !this[index + i])
				{
					this.Clone();
				}
				this.SetBit(index + i);
			}
		}

		// Token: 0x1700062B RID: 1579
		public bool this[int index]
		{
			get
			{
				return this.GetBit(index);
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00084C90 File Offset: 0x00082E90
		public override string ToString()
		{
			int length = this.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(this[i] ? '1' : '0');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00084CD3 File Offset: 0x00082ED3
		private void Clone()
		{
			this.large_bits = (int[])this.large_bits.Clone();
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00084CEB File Offset: 0x00082EEB
		private bool GetBit(int index)
		{
			if (this.large_bits != null)
			{
				return (this.large_bits[index >> 5] & 1 << index) != 0;
			}
			return ((ulong)this.bits & (ulong)(1L << (index & 31))) > 0UL;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00084D20 File Offset: 0x00082F20
		private void SetBit(int index)
		{
			if (this.large_bits == null)
			{
				this.bits |= 1U << index;
				return;
			}
			this.large_bits[index >> 5] |= 1 << index;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00084D5C File Offset: 0x00082F5C
		public static bool AreEqual(DefiniteAssignmentBitSet a, DefiniteAssignmentBitSet b)
		{
			if (a.large_bits == null)
			{
				return (a.bits & 2147483647U) == (b.bits & 2147483647U);
			}
			for (int i = 0; i < a.large_bits.Length; i++)
			{
				if (a.large_bits[i] != b.large_bits[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00084DB4 File Offset: 0x00082FB4
		// Note: this type is marked as 'beforefieldinit'.
		static DefiniteAssignmentBitSet()
		{
		}

		// Token: 0x04000A2E RID: 2606
		private const uint copy_on_write_flag = 2147483648U;

		// Token: 0x04000A2F RID: 2607
		private uint bits;

		// Token: 0x04000A30 RID: 2608
		private int[] large_bits;

		// Token: 0x04000A31 RID: 2609
		public static readonly DefiniteAssignmentBitSet Empty = new DefiniteAssignmentBitSet(0);
	}
}
