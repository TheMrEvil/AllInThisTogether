using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000003 RID: 3
	public class ByteArraySlicePool
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002140 File Offset: 0x00000340
		public int MinStackIndex
		{
			get
			{
				return this.minStackIndex;
			}
			set
			{
				this.minStackIndex = ((value > 0) ? ((value < 31) ? value : 31) : 1);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000035C
		public int AllocationCounter
		{
			get
			{
				return this.allocationCounter;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public ByteArraySlicePool()
		{
			Stack<ByteArraySlice>[] obj = this.poolTiers;
			lock (obj)
			{
				this.poolTiers[0] = new Stack<ByteArraySlice>();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DC File Offset: 0x000003DC
		public ByteArraySlice Acquire(byte[] buffer, int offset = 0, int count = 0)
		{
			Stack<ByteArraySlice>[] obj = this.poolTiers;
			ByteArraySlice byteArraySlice;
			lock (obj)
			{
				Stack<ByteArraySlice> obj2 = this.poolTiers[0];
				lock (obj2)
				{
					byteArraySlice = this.PopOrCreate(this.poolTiers[0], 0);
				}
			}
			byteArraySlice.Buffer = buffer;
			byteArraySlice.Offset = offset;
			byteArraySlice.Count = count;
			return byteArraySlice;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002278 File Offset: 0x00000478
		public ByteArraySlice Acquire(int minByteCount)
		{
			bool flag = minByteCount < 0;
			if (flag)
			{
				throw new Exception(typeof(ByteArraySlice).Name + " requires a positive minByteCount.");
			}
			int i = this.minStackIndex;
			bool flag2 = minByteCount > 0;
			if (flag2)
			{
				int num = minByteCount - 1;
				while (i < 32)
				{
					bool flag3 = num >> i == 0;
					if (flag3)
					{
						break;
					}
					i++;
				}
			}
			Stack<ByteArraySlice>[] obj = this.poolTiers;
			ByteArraySlice result;
			lock (obj)
			{
				Stack<ByteArraySlice> stack = this.poolTiers[i];
				bool flag5 = stack == null;
				if (flag5)
				{
					stack = new Stack<ByteArraySlice>();
					this.poolTiers[i] = stack;
				}
				Stack<ByteArraySlice> obj2 = stack;
				lock (obj2)
				{
					result = this.PopOrCreate(stack, i);
				}
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000237C File Offset: 0x0000057C
		private ByteArraySlice PopOrCreate(Stack<ByteArraySlice> stack, int stackIndex)
		{
			lock (stack)
			{
				bool flag2 = stack.Count > 0;
				if (flag2)
				{
					return stack.Pop();
				}
			}
			ByteArraySlice result = new ByteArraySlice(this, stackIndex);
			this.allocationCounter++;
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023F0 File Offset: 0x000005F0
		internal bool Release(ByteArraySlice slice, int stackIndex)
		{
			bool flag = slice == null || stackIndex < 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = stackIndex == 0;
				if (flag2)
				{
					slice.Buffer = null;
				}
				Stack<ByteArraySlice>[] obj = this.poolTiers;
				lock (obj)
				{
					Stack<ByteArraySlice> obj2 = this.poolTiers[stackIndex];
					lock (obj2)
					{
						this.poolTiers[stackIndex].Push(slice);
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024A0 File Offset: 0x000006A0
		public void ClearPools(int lower = 0, int upper = 2147483647)
		{
			int num = this.minStackIndex;
			for (int i = 0; i < 32; i++)
			{
				int num2 = 1 << i;
				bool flag = num2 < lower;
				if (!flag)
				{
					bool flag2 = num2 > upper;
					if (!flag2)
					{
						Stack<ByteArraySlice>[] obj = this.poolTiers;
						lock (obj)
						{
							bool flag4 = this.poolTiers[i] != null;
							if (flag4)
							{
								Stack<ByteArraySlice> obj2 = this.poolTiers[i];
								lock (obj2)
								{
									this.poolTiers[i].Clear();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private int minStackIndex = 7;

		// Token: 0x04000007 RID: 7
		internal readonly Stack<ByteArraySlice>[] poolTiers = new Stack<ByteArraySlice>[32];

		// Token: 0x04000008 RID: 8
		private int allocationCounter;
	}
}
