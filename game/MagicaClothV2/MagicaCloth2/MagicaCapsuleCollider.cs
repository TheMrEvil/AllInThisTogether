using System;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200001A RID: 26
	public class MagicaCapsuleCollider : ColliderComponent
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000054E3 File Offset: 0x000036E3
		public override ColliderManager.ColliderType GetColliderType()
		{
			if (this.direction == MagicaCapsuleCollider.Direction.X)
			{
				return ColliderManager.ColliderType.CapsuleX;
			}
			if (this.direction == MagicaCapsuleCollider.Direction.Y)
			{
				return ColliderManager.ColliderType.CapsuleY;
			}
			return ColliderManager.ColliderType.CapsuleZ;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000054FB File Offset: 0x000036FB
		public void SetSize(float startRadius, float endRadius, float length)
		{
			base.SetSize(new Vector3(startRadius, endRadius, length));
			this.radiusSeparation = (startRadius == endRadius);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005515 File Offset: 0x00003715
		public override Vector3 GetSize()
		{
			if (this.radiusSeparation)
			{
				return this.size;
			}
			return new Vector3(this.size.x, this.size.x, this.size.z);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000554C File Offset: 0x0000374C
		public Vector3 GetLocalDir()
		{
			if (this.direction == MagicaCapsuleCollider.Direction.X)
			{
				return Vector3.right;
			}
			if (this.direction == MagicaCapsuleCollider.Direction.Y)
			{
				return Vector3.up;
			}
			return Vector3.forward;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005570 File Offset: 0x00003770
		public Vector3 GetLocalUp()
		{
			if (this.direction == MagicaCapsuleCollider.Direction.X)
			{
				return Vector3.up;
			}
			if (this.direction == MagicaCapsuleCollider.Direction.Y)
			{
				return Vector3.forward;
			}
			return Vector3.up;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005594 File Offset: 0x00003794
		public override void DataValidate()
		{
			this.size.x = Mathf.Max(this.size.x, 0.001f);
			this.size.y = Mathf.Max(this.size.y, 0.001f);
			this.size.z = Mathf.Max(this.size.z, 0.001f);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005601 File Offset: 0x00003801
		public MagicaCapsuleCollider()
		{
		}

		// Token: 0x040000A6 RID: 166
		public MagicaCapsuleCollider.Direction direction;

		// Token: 0x040000A7 RID: 167
		public bool radiusSeparation;

		// Token: 0x0200001B RID: 27
		public enum Direction
		{
			// Token: 0x040000A9 RID: 169
			[InspectorName("X-Axis")]
			X,
			// Token: 0x040000AA RID: 170
			[InspectorName("Y-Axis")]
			Y,
			// Token: 0x040000AB RID: 171
			[InspectorName("Z-Axis")]
			Z
		}
	}
}
