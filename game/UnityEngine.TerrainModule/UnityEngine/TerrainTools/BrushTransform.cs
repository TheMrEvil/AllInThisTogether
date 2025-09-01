using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainTools
{
	// Token: 0x02000021 RID: 33
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public struct BrushTransform
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004E6E File Offset: 0x0000306E
		public readonly Vector2 brushOrigin
		{
			[CompilerGenerated]
			get
			{
				return this.<brushOrigin>k__BackingField;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004E76 File Offset: 0x00003076
		public readonly Vector2 brushU
		{
			[CompilerGenerated]
			get
			{
				return this.<brushU>k__BackingField;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004E7E File Offset: 0x0000307E
		public readonly Vector2 brushV
		{
			[CompilerGenerated]
			get
			{
				return this.<brushV>k__BackingField;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004E86 File Offset: 0x00003086
		public readonly Vector2 targetOrigin
		{
			[CompilerGenerated]
			get
			{
				return this.<targetOrigin>k__BackingField;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004E8E File Offset: 0x0000308E
		public readonly Vector2 targetX
		{
			[CompilerGenerated]
			get
			{
				return this.<targetX>k__BackingField;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004E96 File Offset: 0x00003096
		public readonly Vector2 targetY
		{
			[CompilerGenerated]
			get
			{
				return this.<targetY>k__BackingField;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004EA0 File Offset: 0x000030A0
		public BrushTransform(Vector2 brushOrigin, Vector2 brushU, Vector2 brushV)
		{
			float num = brushU.x * brushV.y - brushU.y * brushV.x;
			float d = Mathf.Approximately(num, 0f) ? 1f : (1f / num);
			Vector2 vector = new Vector2(brushV.y, -brushU.y) * d;
			Vector2 vector2 = new Vector2(-brushV.x, brushU.x) * d;
			Vector2 vector3 = -brushOrigin.x * vector - brushOrigin.y * vector2;
			this.brushOrigin = brushOrigin;
			this.brushU = brushU;
			this.brushV = brushV;
			this.targetOrigin = vector3;
			this.targetX = vector;
			this.targetY = vector2;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004F64 File Offset: 0x00003164
		public Rect GetBrushXYBounds()
		{
			Vector2 vector = this.brushOrigin + this.brushU;
			Vector2 vector2 = this.brushOrigin + this.brushV;
			Vector2 vector3 = this.brushOrigin + this.brushU + this.brushV;
			float xmin = Mathf.Min(Mathf.Min(this.brushOrigin.x, vector.x), Mathf.Min(vector2.x, vector3.x));
			float xmax = Mathf.Max(Mathf.Max(this.brushOrigin.x, vector.x), Mathf.Max(vector2.x, vector3.x));
			float ymin = Mathf.Min(Mathf.Min(this.brushOrigin.y, vector.y), Mathf.Min(vector2.y, vector3.y));
			float ymax = Mathf.Max(Mathf.Max(this.brushOrigin.y, vector.y), Mathf.Max(vector2.y, vector3.y));
			return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000507C File Offset: 0x0000327C
		public static BrushTransform FromRect(Rect brushRect)
		{
			Vector2 min = brushRect.min;
			Vector2 brushU = new Vector2(brushRect.width, 0f);
			Vector2 brushV = new Vector2(0f, brushRect.height);
			return new BrushTransform(min, brushU, brushV);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000050C4 File Offset: 0x000032C4
		public Vector2 ToBrushUV(Vector2 targetXY)
		{
			return targetXY.x * this.targetX + targetXY.y * this.targetY + this.targetOrigin;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005108 File Offset: 0x00003308
		public Vector2 FromBrushUV(Vector2 brushUV)
		{
			return brushUV.x * this.brushU + brushUV.y * this.brushV + this.brushOrigin;
		}

		// Token: 0x04000076 RID: 118
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Vector2 <brushOrigin>k__BackingField;

		// Token: 0x04000077 RID: 119
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector2 <brushU>k__BackingField;

		// Token: 0x04000078 RID: 120
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Vector2 <brushV>k__BackingField;

		// Token: 0x04000079 RID: 121
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector2 <targetOrigin>k__BackingField;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Vector2 <targetX>k__BackingField;

		// Token: 0x0400007B RID: 123
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Vector2 <targetY>k__BackingField;
	}
}
