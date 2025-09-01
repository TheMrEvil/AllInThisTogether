using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200000D RID: 13
	internal class DTSweepDebugContext : TriangulationDebugContext
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004836 File Offset: 0x00002A36
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000483E File Offset: 0x00002A3E
		public DelaunayTriangle PrimaryTriangle
		{
			get
			{
				return this._primaryTriangle;
			}
			set
			{
				this._primaryTriangle = value;
				this._tcx.Update("set PrimaryTriangle");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004857 File Offset: 0x00002A57
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000485F File Offset: 0x00002A5F
		public DelaunayTriangle SecondaryTriangle
		{
			get
			{
				return this._secondaryTriangle;
			}
			set
			{
				this._secondaryTriangle = value;
				this._tcx.Update("set SecondaryTriangle");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004878 File Offset: 0x00002A78
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004880 File Offset: 0x00002A80
		public TriangulationPoint ActivePoint
		{
			get
			{
				return this._activePoint;
			}
			set
			{
				this._activePoint = value;
				this._tcx.Update("set ActivePoint");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004899 File Offset: 0x00002A99
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000048A1 File Offset: 0x00002AA1
		public AdvancingFrontNode ActiveNode
		{
			get
			{
				return this._activeNode;
			}
			set
			{
				this._activeNode = value;
				this._tcx.Update("set ActiveNode");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000048BA File Offset: 0x00002ABA
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000048C2 File Offset: 0x00002AC2
		public DTSweepConstraint ActiveConstraint
		{
			get
			{
				return this._activeConstraint;
			}
			set
			{
				this._activeConstraint = value;
				this._tcx.Update("set ActiveConstraint");
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000048DB File Offset: 0x00002ADB
		public DTSweepDebugContext(DTSweepContext tcx) : base(tcx)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000048E4 File Offset: 0x00002AE4
		public bool IsDebugContext
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000048E7 File Offset: 0x00002AE7
		public override void Clear()
		{
			this.PrimaryTriangle = null;
			this.SecondaryTriangle = null;
			this.ActivePoint = null;
			this.ActiveNode = null;
			this.ActiveConstraint = null;
		}

		// Token: 0x04000025 RID: 37
		private DelaunayTriangle _primaryTriangle;

		// Token: 0x04000026 RID: 38
		private DelaunayTriangle _secondaryTriangle;

		// Token: 0x04000027 RID: 39
		private TriangulationPoint _activePoint;

		// Token: 0x04000028 RID: 40
		private AdvancingFrontNode _activeNode;

		// Token: 0x04000029 RID: 41
		private DTSweepConstraint _activeConstraint;
	}
}
