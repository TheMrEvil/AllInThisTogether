using System;
using System.Runtime.CompilerServices;
using UnityEngine.Events;

namespace UnityEngine.Rendering
{
	// Token: 0x02000095 RID: 149
	internal class AtlasAllocator
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0001624C File Offset: 0x0001444C
		public AtlasAllocator(int width, int height, bool potPadding)
		{
			this.m_Root = new AtlasAllocator.AtlasNode();
			this.m_Root.m_Rect.Set((float)width, (float)height, 0f, 0f);
			this.m_Width = width;
			this.m_Height = height;
			this.powerOfTwoPadding = potPadding;
			this.m_NodePool = new ObjectPool<AtlasAllocator.AtlasNode>(delegate(AtlasAllocator.AtlasNode _)
			{
			}, delegate(AtlasAllocator.AtlasNode _)
			{
			}, true);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000162E8 File Offset: 0x000144E8
		public bool Allocate(ref Vector4 result, int width, int height)
		{
			AtlasAllocator.AtlasNode atlasNode = this.m_Root.Allocate(ref this.m_NodePool, width, height, this.powerOfTwoPadding);
			if (atlasNode != null)
			{
				result = atlasNode.m_Rect;
				return true;
			}
			result = Vector4.zero;
			return false;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001632C File Offset: 0x0001452C
		public void Reset()
		{
			this.m_Root.Release(ref this.m_NodePool);
			this.m_Root.m_Rect.Set((float)this.m_Width, (float)this.m_Height, 0f, 0f);
		}

		// Token: 0x04000310 RID: 784
		private AtlasAllocator.AtlasNode m_Root;

		// Token: 0x04000311 RID: 785
		private int m_Width;

		// Token: 0x04000312 RID: 786
		private int m_Height;

		// Token: 0x04000313 RID: 787
		private bool powerOfTwoPadding;

		// Token: 0x04000314 RID: 788
		private ObjectPool<AtlasAllocator.AtlasNode> m_NodePool;

		// Token: 0x0200016E RID: 366
		private class AtlasNode
		{
			// Token: 0x060008F0 RID: 2288 RVA: 0x00023C80 File Offset: 0x00021E80
			public AtlasAllocator.AtlasNode Allocate(ref ObjectPool<AtlasAllocator.AtlasNode> pool, int width, int height, bool powerOfTwoPadding)
			{
				if (this.m_RightChild != null)
				{
					AtlasAllocator.AtlasNode atlasNode = this.m_RightChild.Allocate(ref pool, width, height, powerOfTwoPadding);
					if (atlasNode == null)
					{
						atlasNode = this.m_BottomChild.Allocate(ref pool, width, height, powerOfTwoPadding);
					}
					return atlasNode;
				}
				int num = 0;
				int num2 = 0;
				if (powerOfTwoPadding)
				{
					num = (int)this.m_Rect.x % width;
					num2 = (int)this.m_Rect.y % height;
				}
				if ((float)width <= this.m_Rect.x - (float)num && (float)height <= this.m_Rect.y - (float)num2)
				{
					this.m_RightChild = pool.Get();
					this.m_BottomChild = pool.Get();
					this.m_Rect.z = this.m_Rect.z + (float)num;
					this.m_Rect.w = this.m_Rect.w + (float)num2;
					this.m_Rect.x = this.m_Rect.x - (float)num;
					this.m_Rect.y = this.m_Rect.y - (float)num2;
					if (width > height)
					{
						this.m_RightChild.m_Rect.z = this.m_Rect.z + (float)width;
						this.m_RightChild.m_Rect.w = this.m_Rect.w;
						this.m_RightChild.m_Rect.x = this.m_Rect.x - (float)width;
						this.m_RightChild.m_Rect.y = (float)height;
						this.m_BottomChild.m_Rect.z = this.m_Rect.z;
						this.m_BottomChild.m_Rect.w = this.m_Rect.w + (float)height;
						this.m_BottomChild.m_Rect.x = this.m_Rect.x;
						this.m_BottomChild.m_Rect.y = this.m_Rect.y - (float)height;
					}
					else
					{
						this.m_RightChild.m_Rect.z = this.m_Rect.z + (float)width;
						this.m_RightChild.m_Rect.w = this.m_Rect.w;
						this.m_RightChild.m_Rect.x = this.m_Rect.x - (float)width;
						this.m_RightChild.m_Rect.y = this.m_Rect.y;
						this.m_BottomChild.m_Rect.z = this.m_Rect.z;
						this.m_BottomChild.m_Rect.w = this.m_Rect.w + (float)height;
						this.m_BottomChild.m_Rect.x = (float)width;
						this.m_BottomChild.m_Rect.y = this.m_Rect.y - (float)height;
					}
					this.m_Rect.x = (float)width;
					this.m_Rect.y = (float)height;
					return this;
				}
				return null;
			}

			// Token: 0x060008F1 RID: 2289 RVA: 0x00023F4C File Offset: 0x0002214C
			public void Release(ref ObjectPool<AtlasAllocator.AtlasNode> pool)
			{
				if (this.m_RightChild != null)
				{
					this.m_RightChild.Release(ref pool);
					this.m_BottomChild.Release(ref pool);
					pool.Release(this.m_RightChild);
					pool.Release(this.m_BottomChild);
				}
				this.m_RightChild = null;
				this.m_BottomChild = null;
				this.m_Rect = Vector4.zero;
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x00023FAC File Offset: 0x000221AC
			public AtlasNode()
			{
			}

			// Token: 0x0400057A RID: 1402
			public AtlasAllocator.AtlasNode m_RightChild;

			// Token: 0x0400057B RID: 1403
			public AtlasAllocator.AtlasNode m_BottomChild;

			// Token: 0x0400057C RID: 1404
			public Vector4 m_Rect = new Vector4(0f, 0f, 0f, 0f);
		}

		// Token: 0x0200016F RID: 367
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060008F3 RID: 2291 RVA: 0x00023FD3 File Offset: 0x000221D3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060008F4 RID: 2292 RVA: 0x00023FDF File Offset: 0x000221DF
			public <>c()
			{
			}

			// Token: 0x060008F5 RID: 2293 RVA: 0x00023FE7 File Offset: 0x000221E7
			internal void <.ctor>b__6_0(AtlasAllocator.AtlasNode _)
			{
			}

			// Token: 0x060008F6 RID: 2294 RVA: 0x00023FE9 File Offset: 0x000221E9
			internal void <.ctor>b__6_1(AtlasAllocator.AtlasNode _)
			{
			}

			// Token: 0x0400057D RID: 1405
			public static readonly AtlasAllocator.<>c <>9 = new AtlasAllocator.<>c();

			// Token: 0x0400057E RID: 1406
			public static UnityAction<AtlasAllocator.AtlasNode> <>9__6_0;

			// Token: 0x0400057F RID: 1407
			public static UnityAction<AtlasAllocator.AtlasNode> <>9__6_1;
		}
	}
}
