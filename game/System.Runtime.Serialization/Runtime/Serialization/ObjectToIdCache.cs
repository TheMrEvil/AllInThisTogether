using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F0 RID: 240
	internal class ObjectToIdCache
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x00036794 File Offset: 0x00034994
		public ObjectToIdCache()
		{
			this.m_currentCount = 1;
			this.m_ids = new int[ObjectToIdCache.GetPrime(1)];
			this.m_objs = new object[this.m_ids.Length];
			this.m_isWrapped = new bool[this.m_ids.Length];
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000367E8 File Offset: 0x000349E8
		public int GetId(object obj, ref bool newId)
		{
			bool flag;
			bool flag2;
			int num = this.FindElement(obj, out flag, out flag2);
			if (!flag)
			{
				newId = false;
				return this.m_ids[num];
			}
			if (!newId)
			{
				return -1;
			}
			int currentCount = this.m_currentCount;
			this.m_currentCount = currentCount + 1;
			int num2 = currentCount;
			this.m_objs[num] = obj;
			this.m_ids[num] = num2;
			this.m_isWrapped[num] = flag2;
			if (this.m_currentCount >= this.m_objs.Length - 1)
			{
				this.Rehash();
			}
			return num2;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00036860 File Offset: 0x00034A60
		public int ReassignId(int oldObjId, object oldObj, object newObj)
		{
			bool flag;
			bool flag2;
			int num = this.FindElement(oldObj, out flag, out flag2);
			if (flag)
			{
				return 0;
			}
			int num2 = this.m_ids[num];
			if (oldObjId > 0)
			{
				this.m_ids[num] = oldObjId;
			}
			else
			{
				this.RemoveAt(num);
			}
			num = this.FindElement(newObj, out flag, out flag2);
			int result = 0;
			if (!flag)
			{
				result = this.m_ids[num];
			}
			this.m_objs[num] = newObj;
			this.m_ids[num] = num2;
			this.m_isWrapped[num] = flag2;
			return result;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000368D8 File Offset: 0x00034AD8
		private int FindElement(object obj, out bool isEmpty, out bool isWrapped)
		{
			isWrapped = false;
			int num = this.ComputeStartPosition(obj);
			for (int num2 = num; num2 != num - 1; num2++)
			{
				if (this.m_objs[num2] == null)
				{
					isEmpty = true;
					return num2;
				}
				if (this.m_objs[num2] == obj)
				{
					isEmpty = false;
					return num2;
				}
				if (num2 == this.m_objs.Length - 1)
				{
					isWrapped = true;
					num2 = -1;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An internal error has occurred. Object table overflow. This could be caused by serializing or deserializing extremely large object graphs.")));
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00036944 File Offset: 0x00034B44
		private void RemoveAt(int position)
		{
			int num = this.m_objs.Length;
			int num2 = position;
			for (int num3 = (position == num - 1) ? 0 : (position + 1); num3 != position; num3++)
			{
				if (this.m_objs[num3] == null)
				{
					this.m_objs[num2] = null;
					this.m_ids[num2] = 0;
					this.m_isWrapped[num2] = false;
					return;
				}
				int num4 = this.ComputeStartPosition(this.m_objs[num3]);
				bool flag = num3 < position && !this.m_isWrapped[num3];
				bool flag2 = num2 < position;
				if ((num4 <= num2 && (!flag || flag2)) || (flag2 && !flag))
				{
					this.m_objs[num2] = this.m_objs[num3];
					this.m_ids[num2] = this.m_ids[num3];
					this.m_isWrapped[num2] = (this.m_isWrapped[num3] && num3 > num2);
					num2 = num3;
				}
				if (num3 == num - 1)
				{
					num3 = -1;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("An internal error has occurred. Object table overflow. This could be caused by serializing or deserializing extremely large object graphs.")));
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00036A2E File Offset: 0x00034C2E
		private int ComputeStartPosition(object o)
		{
			return (RuntimeHelpers.GetHashCode(o) & int.MaxValue) % this.m_objs.Length;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00036A48 File Offset: 0x00034C48
		private void Rehash()
		{
			int prime = ObjectToIdCache.GetPrime(this.m_objs.Length * 2);
			int[] ids = this.m_ids;
			object[] objs = this.m_objs;
			this.m_ids = new int[prime];
			this.m_objs = new object[prime];
			this.m_isWrapped = new bool[prime];
			for (int i = 0; i < objs.Length; i++)
			{
				object obj = objs[i];
				if (obj != null)
				{
					bool flag;
					bool flag2;
					int num = this.FindElement(obj, out flag, out flag2);
					this.m_objs[num] = obj;
					this.m_ids[num] = ids[i];
					this.m_isWrapped[num] = flag2;
				}
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00036AE0 File Offset: 0x00034CE0
		private static int GetPrime(int min)
		{
			for (int i = 0; i < ObjectToIdCache.primes.Length; i++)
			{
				int num = ObjectToIdCache.primes[i];
				if (num >= min)
				{
					return num;
				}
			}
			for (int j = min | 1; j < 2147483647; j += 2)
			{
				if (ObjectToIdCache.IsPrime(j))
				{
					return j;
				}
			}
			return min;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00036B2C File Offset: 0x00034D2C
		private static bool IsPrime(int candidate)
		{
			if ((candidate & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)candidate);
				for (int i = 3; i <= num; i += 2)
				{
					if (candidate % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return candidate == 2;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00036B60 File Offset: 0x00034D60
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectToIdCache()
		{
		}

		// Token: 0x04000656 RID: 1622
		internal int m_currentCount;

		// Token: 0x04000657 RID: 1623
		internal int[] m_ids;

		// Token: 0x04000658 RID: 1624
		internal object[] m_objs;

		// Token: 0x04000659 RID: 1625
		private bool[] m_isWrapped;

		// Token: 0x0400065A RID: 1626
		internal static readonly int[] primes = new int[]
		{
			3,
			7,
			17,
			37,
			89,
			197,
			431,
			919,
			1931,
			4049,
			8419,
			17519,
			36353,
			75431,
			156437,
			324449,
			672827,
			1395263,
			2893249,
			5999471
		};
	}
}
