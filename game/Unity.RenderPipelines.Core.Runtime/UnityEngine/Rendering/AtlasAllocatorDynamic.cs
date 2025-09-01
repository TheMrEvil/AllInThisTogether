using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000097 RID: 151
	internal class AtlasAllocatorDynamic
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00016D2C File Offset: 0x00014F2C
		public AtlasAllocatorDynamic(int width, int height, int capacityAllocations)
		{
			int num = capacityAllocations * 2;
			this.m_Pool = new AtlasAllocatorDynamic.AtlasNodePool((short)num);
			this.m_NodeFromID = new Dictionary<int, short>(capacityAllocations);
			short parent = -1;
			this.m_Root = this.m_Pool.AtlasNodeCreate(parent);
			this.m_Pool.m_Nodes[(int)this.m_Root].m_Rect.Set((float)width, (float)height, 0f, 0f);
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016DAC File Offset: 0x00014FAC
		public bool Allocate(out Vector4 result, int key, int width, int height)
		{
			short num = this.m_Pool.m_Nodes[(int)this.m_Root].Allocate(this.m_Pool, width, height);
			if (num >= 0)
			{
				result = this.m_Pool.m_Nodes[(int)num].m_Rect;
				this.m_NodeFromID.Add(key, num);
				return true;
			}
			result = Vector4.zero;
			return false;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00016E1C File Offset: 0x0001501C
		public void Release(int key)
		{
			short num;
			if (this.m_NodeFromID.TryGetValue(key, out num))
			{
				this.m_Pool.m_Nodes[(int)num].ReleaseAndMerge(this.m_Pool);
				this.m_NodeFromID.Remove(key);
				return;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00016E64 File Offset: 0x00015064
		public void Release()
		{
			this.m_Pool.Clear();
			this.m_Root = this.m_Pool.AtlasNodeCreate(-1);
			this.m_Pool.m_Nodes[(int)this.m_Root].m_Rect.Set((float)this.m_Width, (float)this.m_Height, 0f, 0f);
			this.m_NodeFromID.Clear();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016ED4 File Offset: 0x000150D4
		public string DebugStringFromRoot(int depthMax = -1)
		{
			string result = "";
			this.DebugStringFromNode(ref result, this.m_Root, 0, depthMax);
			return result;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016EF8 File Offset: 0x000150F8
		private void DebugStringFromNode(ref string res, short n, int depthCurrent = 0, int depthMax = -1)
		{
			res = string.Concat(new string[]
			{
				res,
				"{[",
				depthCurrent.ToString(),
				"], isOccupied = ",
				this.m_Pool.m_Nodes[(int)n].IsOccupied() ? "true" : "false",
				", self = ",
				this.m_Pool.m_Nodes[(int)n].m_Self.ToString(),
				", ",
				this.m_Pool.m_Nodes[(int)n].m_Rect.x.ToString(),
				",",
				this.m_Pool.m_Nodes[(int)n].m_Rect.y.ToString(),
				", ",
				this.m_Pool.m_Nodes[(int)n].m_Rect.z.ToString(),
				", ",
				this.m_Pool.m_Nodes[(int)n].m_Rect.w.ToString(),
				"}\n"
			});
			if (depthMax == -1 || depthCurrent < depthMax)
			{
				if (this.m_Pool.m_Nodes[(int)n].m_LeftChild >= 0)
				{
					this.DebugStringFromNode(ref res, this.m_Pool.m_Nodes[(int)n].m_LeftChild, depthCurrent + 1, depthMax);
				}
				if (this.m_Pool.m_Nodes[(int)n].m_RightChild >= 0)
				{
					this.DebugStringFromNode(ref res, this.m_Pool.m_Nodes[(int)n].m_RightChild, depthCurrent + 1, depthMax);
				}
			}
		}

		// Token: 0x04000324 RID: 804
		private int m_Width;

		// Token: 0x04000325 RID: 805
		private int m_Height;

		// Token: 0x04000326 RID: 806
		private AtlasAllocatorDynamic.AtlasNodePool m_Pool;

		// Token: 0x04000327 RID: 807
		private short m_Root;

		// Token: 0x04000328 RID: 808
		private Dictionary<int, short> m_NodeFromID;

		// Token: 0x02000171 RID: 369
		private class AtlasNodePool
		{
			// Token: 0x060008F7 RID: 2295 RVA: 0x00023FEB File Offset: 0x000221EB
			public AtlasNodePool(short capacity)
			{
				this.m_Nodes = new AtlasAllocatorDynamic.AtlasNode[(int)capacity];
				this.m_Next = 0;
				this.m_FreelistHead = -1;
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x0002400D File Offset: 0x0002220D
			public void Dispose()
			{
				this.Clear();
				this.m_Nodes = null;
			}

			// Token: 0x060008F9 RID: 2297 RVA: 0x0002401C File Offset: 0x0002221C
			public void Clear()
			{
				this.m_Next = 0;
				this.m_FreelistHead = -1;
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x0002402C File Offset: 0x0002222C
			public short AtlasNodeCreate(short parent)
			{
				if (this.m_FreelistHead != -1)
				{
					short freelistNext = this.m_Nodes[(int)this.m_FreelistHead].m_FreelistNext;
					this.m_Nodes[(int)this.m_FreelistHead] = new AtlasAllocatorDynamic.AtlasNode(this.m_FreelistHead, parent);
					short freelistHead = this.m_FreelistHead;
					this.m_FreelistHead = freelistNext;
					return freelistHead;
				}
				this.m_Nodes[(int)this.m_Next] = new AtlasAllocatorDynamic.AtlasNode(this.m_Next, parent);
				short next = this.m_Next;
				this.m_Next = next + 1;
				return next;
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x000240B3 File Offset: 0x000222B3
			public void AtlasNodeFree(short index)
			{
				this.m_Nodes[(int)index].m_FreelistNext = this.m_FreelistHead;
				this.m_FreelistHead = index;
			}

			// Token: 0x04000585 RID: 1413
			internal AtlasAllocatorDynamic.AtlasNode[] m_Nodes;

			// Token: 0x04000586 RID: 1414
			private short m_Next;

			// Token: 0x04000587 RID: 1415
			private short m_FreelistHead;
		}

		// Token: 0x02000172 RID: 370
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		private struct AtlasNode
		{
			// Token: 0x060008FC RID: 2300 RVA: 0x000240D3 File Offset: 0x000222D3
			public AtlasNode(short self, short parent)
			{
				this.m_Self = self;
				this.m_Parent = parent;
				this.m_LeftChild = -1;
				this.m_RightChild = -1;
				this.m_Flags = 0;
				this.m_FreelistNext = -1;
				this.m_Rect = Vector4.zero;
			}

			// Token: 0x060008FD RID: 2301 RVA: 0x0002410A File Offset: 0x0002230A
			public bool IsOccupied()
			{
				return (this.m_Flags & 1) > 0;
			}

			// Token: 0x060008FE RID: 2302 RVA: 0x00024118 File Offset: 0x00022318
			public void SetIsOccupied()
			{
				ushort num = 1;
				this.m_Flags |= num;
			}

			// Token: 0x060008FF RID: 2303 RVA: 0x00024138 File Offset: 0x00022338
			public void ClearIsOccupied()
			{
				ushort num = 1;
				this.m_Flags &= ~num;
			}

			// Token: 0x06000900 RID: 2304 RVA: 0x00024158 File Offset: 0x00022358
			public bool IsLeafNode()
			{
				return this.m_LeftChild == -1;
			}

			// Token: 0x06000901 RID: 2305 RVA: 0x00024164 File Offset: 0x00022364
			public short Allocate(AtlasAllocatorDynamic.AtlasNodePool pool, int width, int height)
			{
				if (Mathf.Min(width, height) < 1)
				{
					return -1;
				}
				if (!this.IsLeafNode())
				{
					short num = pool.m_Nodes[(int)this.m_LeftChild].Allocate(pool, width, height);
					if (num == -1)
					{
						num = pool.m_Nodes[(int)this.m_RightChild].Allocate(pool, width, height);
					}
					return num;
				}
				if (this.IsOccupied())
				{
					return -1;
				}
				if ((float)width > this.m_Rect.x || (float)height > this.m_Rect.y)
				{
					return -1;
				}
				this.m_LeftChild = pool.AtlasNodeCreate(this.m_Self);
				this.m_RightChild = pool.AtlasNodeCreate(this.m_Self);
				float num2 = this.m_Rect.x - (float)width;
				float num3 = this.m_Rect.y - (float)height;
				if (num2 >= num3)
				{
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.x = (float)width;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.y = this.m_Rect.y;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.z = this.m_Rect.z;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.w = this.m_Rect.w;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.x = num2;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.y = this.m_Rect.y;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.z = this.m_Rect.z + (float)width;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.w = this.m_Rect.w;
					if (num3 < 1f)
					{
						pool.m_Nodes[(int)this.m_LeftChild].SetIsOccupied();
						return this.m_LeftChild;
					}
					short num4 = pool.m_Nodes[(int)this.m_LeftChild].Allocate(pool, width, height);
					if (num4 >= 0)
					{
						pool.m_Nodes[(int)num4].SetIsOccupied();
					}
					return num4;
				}
				else
				{
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.x = this.m_Rect.x;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.y = (float)height;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.z = this.m_Rect.z;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.w = this.m_Rect.w;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.x = this.m_Rect.x;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.y = num3;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.z = this.m_Rect.z;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.w = this.m_Rect.w + (float)height;
					if (num2 < 1f)
					{
						pool.m_Nodes[(int)this.m_LeftChild].SetIsOccupied();
						return this.m_LeftChild;
					}
					short num5 = pool.m_Nodes[(int)this.m_LeftChild].Allocate(pool, width, height);
					if (num5 >= 0)
					{
						pool.m_Nodes[(int)num5].SetIsOccupied();
					}
					return num5;
				}
			}

			// Token: 0x06000902 RID: 2306 RVA: 0x00024524 File Offset: 0x00022724
			public void ReleaseChildren(AtlasAllocatorDynamic.AtlasNodePool pool)
			{
				if (this.IsLeafNode())
				{
					return;
				}
				pool.m_Nodes[(int)this.m_LeftChild].ReleaseChildren(pool);
				pool.m_Nodes[(int)this.m_RightChild].ReleaseChildren(pool);
				pool.AtlasNodeFree(this.m_LeftChild);
				pool.AtlasNodeFree(this.m_RightChild);
				this.m_LeftChild = -1;
				this.m_RightChild = -1;
			}

			// Token: 0x06000903 RID: 2307 RVA: 0x00024590 File Offset: 0x00022790
			public void ReleaseAndMerge(AtlasAllocatorDynamic.AtlasNodePool pool)
			{
				short num = this.m_Self;
				do
				{
					pool.m_Nodes[(int)num].ReleaseChildren(pool);
					pool.m_Nodes[(int)num].ClearIsOccupied();
					num = pool.m_Nodes[(int)num].m_Parent;
				}
				while (num >= 0 && pool.m_Nodes[(int)num].IsMergeNeeded(pool));
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x000245F4 File Offset: 0x000227F4
			public bool IsMergeNeeded(AtlasAllocatorDynamic.AtlasNodePool pool)
			{
				return pool.m_Nodes[(int)this.m_LeftChild].IsLeafNode() && !pool.m_Nodes[(int)this.m_LeftChild].IsOccupied() && pool.m_Nodes[(int)this.m_RightChild].IsLeafNode() && !pool.m_Nodes[(int)this.m_RightChild].IsOccupied();
			}

			// Token: 0x04000588 RID: 1416
			[FieldOffset(0)]
			public short m_Self;

			// Token: 0x04000589 RID: 1417
			[FieldOffset(2)]
			public short m_Parent;

			// Token: 0x0400058A RID: 1418
			[FieldOffset(4)]
			public short m_LeftChild;

			// Token: 0x0400058B RID: 1419
			[FieldOffset(6)]
			public short m_RightChild;

			// Token: 0x0400058C RID: 1420
			[FieldOffset(8)]
			public short m_FreelistNext;

			// Token: 0x0400058D RID: 1421
			[FieldOffset(10)]
			public ushort m_Flags;

			// Token: 0x0400058E RID: 1422
			[FieldOffset(16)]
			public Vector4 m_Rect;

			// Token: 0x0200019A RID: 410
			private enum AtlasNodeFlags : uint
			{
				// Token: 0x040005EB RID: 1515
				IsOccupied = 1U
			}
		}
	}
}
