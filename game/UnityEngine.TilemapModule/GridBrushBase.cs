using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public abstract class GridBrushBase : ScriptableObject
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002104 File Offset: 0x00000304
		public virtual void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
			for (int i = position.zMin; i < position.zMax; i++)
			{
				for (int j = position.yMin; j < position.yMax; j++)
				{
					for (int k = position.xMin; k < position.xMax; k++)
					{
						this.Paint(gridLayout, brushTarget, new Vector3Int(k, j, i));
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002184 File Offset: 0x00000384
		public virtual void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
			for (int i = position.zMin; i < position.zMax; i++)
			{
				for (int j = position.yMin; j < position.yMax; j++)
				{
					for (int k = position.xMin; k < position.xMax; k++)
					{
						this.Erase(gridLayout, brushTarget, new Vector3Int(k, j, i));
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Select(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Rotate(GridBrushBase.RotationDirection direction, GridLayout.CellLayout layout)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Flip(GridBrushBase.FlipAxis flip, GridLayout.CellLayout layout)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void Move(GridLayout gridLayout, GameObject brushTarget, BoundsInt from, BoundsInt to)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void ChangeZPosition(int change)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002101 File Offset: 0x00000301
		public virtual void ResetZPosition()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002201 File Offset: 0x00000401
		protected GridBrushBase()
		{
		}

		// Token: 0x02000004 RID: 4
		public enum Tool
		{
			// Token: 0x04000006 RID: 6
			Select,
			// Token: 0x04000007 RID: 7
			Move,
			// Token: 0x04000008 RID: 8
			Paint,
			// Token: 0x04000009 RID: 9
			Box,
			// Token: 0x0400000A RID: 10
			Pick,
			// Token: 0x0400000B RID: 11
			Erase,
			// Token: 0x0400000C RID: 12
			FloodFill
		}

		// Token: 0x02000005 RID: 5
		public enum RotationDirection
		{
			// Token: 0x0400000E RID: 14
			Clockwise,
			// Token: 0x0400000F RID: 15
			CounterClockwise
		}

		// Token: 0x02000006 RID: 6
		public enum FlipAxis
		{
			// Token: 0x04000011 RID: 17
			X,
			// Token: 0x04000012 RID: 18
			Y
		}
	}
}
