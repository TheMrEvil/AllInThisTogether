using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon.StructWrapping
{
	// Token: 0x02000044 RID: 68
	public class StructWrapperPool<T> : StructWrapperPool
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0001A0B0 File Offset: 0x000182B0
		public StructWrapperPool(bool isStaticPool)
		{
			this.pool = new Stack<StructWrapper<T>>();
			this.isStaticPool = isStaticPool;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001A0FC File Offset: 0x000182FC
		public StructWrapper<T> Acquire()
		{
			bool flag = this.pool.Count == 0;
			StructWrapper<T> structWrapper;
			if (flag)
			{
				int num = 1;
				for (;;)
				{
					Pooling releasing = this.isStaticPool ? ((Pooling)3) : Pooling.Connected;
					structWrapper = new StructWrapper<T>(releasing, this.tType, this.wType);
					structWrapper.ReturnPool = this;
					bool flag2 = num == 4;
					if (flag2)
					{
						break;
					}
					this.pool.Push(structWrapper);
					num++;
				}
			}
			else
			{
				structWrapper = this.pool.Pop();
			}
			structWrapper.pooling |= Pooling.CheckedOut;
			return structWrapper;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001A194 File Offset: 0x00018394
		public StructWrapper<T> Acquire(T value)
		{
			StructWrapper<T> structWrapper = this.Acquire();
			structWrapper.value = value;
			return structWrapper;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001A1B8 File Offset: 0x000183B8
		public int Count
		{
			get
			{
				return this.pool.Count;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001A1D5 File Offset: 0x000183D5
		internal void Release(StructWrapper<T> obj)
		{
			obj.pooling &= (Pooling)(-9);
			this.pool.Push(obj);
		}

		// Token: 0x040001F1 RID: 497
		public const int GROWBY = 4;

		// Token: 0x040001F2 RID: 498
		public readonly Type tType = typeof(T);

		// Token: 0x040001F3 RID: 499
		public readonly WrappedType wType = StructWrapperPool.GetWrappedType(typeof(T));

		// Token: 0x040001F4 RID: 500
		public Stack<StructWrapper<T>> pool;

		// Token: 0x040001F5 RID: 501
		public readonly bool isStaticPool;
	}
}
