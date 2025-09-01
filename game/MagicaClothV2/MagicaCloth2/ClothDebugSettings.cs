using System;

namespace MagicaCloth2
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public class ClothDebugSettings
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00005302 File Offset: 0x00003502
		public bool CheckParticleDrawing(int index)
		{
			return true;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00005302 File Offset: 0x00003502
		public bool CheckTriangleDrawing(int index)
		{
			return true;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00005302 File Offset: 0x00003502
		public bool CheckRadiusDrawing()
		{
			return true;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001CD67 File Offset: 0x0001AF67
		public float GetPointSize()
		{
			return 0.01f;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001CD6E File Offset: 0x0001AF6E
		public float GetLineSize()
		{
			return 0.03f;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001CD67 File Offset: 0x0001AF67
		public float GetInertiaCenterRadius()
		{
			return 0.01f;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00005307 File Offset: 0x00003507
		public bool IsReferOldPos()
		{
			return false;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001CD75 File Offset: 0x0001AF75
		public ClothDebugSettings()
		{
		}

		// Token: 0x040005D7 RID: 1495
		public bool enable;

		// Token: 0x040005D8 RID: 1496
		public bool ztest;

		// Token: 0x040005D9 RID: 1497
		public bool position = true;

		// Token: 0x040005DA RID: 1498
		public ClothDebugSettings.DebugAxis axis;

		// Token: 0x040005DB RID: 1499
		public bool shape;

		// Token: 0x040005DC RID: 1500
		public bool baseLine;

		// Token: 0x040005DD RID: 1501
		public bool depth;

		// Token: 0x040005DE RID: 1502
		public bool collider = true;

		// Token: 0x040005DF RID: 1503
		public bool animatedPosition;

		// Token: 0x040005E0 RID: 1504
		public ClothDebugSettings.DebugAxis animatedAxis;

		// Token: 0x040005E1 RID: 1505
		public bool animatedShape;

		// Token: 0x020000C0 RID: 192
		public enum DebugAxis
		{
			// Token: 0x040005E3 RID: 1507
			None,
			// Token: 0x040005E4 RID: 1508
			Normal,
			// Token: 0x040005E5 RID: 1509
			All
		}
	}
}
