using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200002F RID: 47
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[ProGridsConditionalSnap]
	public sealed class PolyShape : MonoBehaviour
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00016166 File Offset: 0x00014366
		public ReadOnlyCollection<Vector3> controlPoints
		{
			get
			{
				return new ReadOnlyCollection<Vector3>(this.m_Points);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00016173 File Offset: 0x00014373
		public void SetControlPoints(IList<Vector3> points)
		{
			this.m_Points = points.ToList<Vector3>();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00016181 File Offset: 0x00014381
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00016189 File Offset: 0x00014389
		public float extrude
		{
			get
			{
				return this.m_Extrude;
			}
			set
			{
				this.m_Extrude = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00016192 File Offset: 0x00014392
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0001619A File Offset: 0x0001439A
		internal PolyShape.PolyEditMode polyEditMode
		{
			get
			{
				return this.m_EditMode;
			}
			set
			{
				this.m_EditMode = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000161A3 File Offset: 0x000143A3
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000161AB File Offset: 0x000143AB
		public bool flipNormals
		{
			get
			{
				return this.m_FlipNormals;
			}
			set
			{
				this.m_FlipNormals = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000161B4 File Offset: 0x000143B4
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000161D6 File Offset: 0x000143D6
		internal ProBuilderMesh mesh
		{
			get
			{
				if (this.m_Mesh == null)
				{
					this.m_Mesh = base.GetComponent<ProBuilderMesh>();
				}
				return this.m_Mesh;
			}
			set
			{
				this.m_Mesh = value;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000161DF File Offset: 0x000143DF
		private bool IsSnapEnabled()
		{
			return this.isOnGrid;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000161E7 File Offset: 0x000143E7
		public PolyShape()
		{
		}

		// Token: 0x0400009E RID: 158
		private ProBuilderMesh m_Mesh;

		// Token: 0x0400009F RID: 159
		[FormerlySerializedAs("points")]
		[SerializeField]
		internal List<Vector3> m_Points = new List<Vector3>();

		// Token: 0x040000A0 RID: 160
		[FormerlySerializedAs("extrude")]
		[SerializeField]
		private float m_Extrude;

		// Token: 0x040000A1 RID: 161
		[FormerlySerializedAs("polyEditMode")]
		[SerializeField]
		private PolyShape.PolyEditMode m_EditMode;

		// Token: 0x040000A2 RID: 162
		[FormerlySerializedAs("flipNormals")]
		[SerializeField]
		private bool m_FlipNormals;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		internal bool isOnGrid = true;

		// Token: 0x02000099 RID: 153
		internal enum PolyEditMode
		{
			// Token: 0x040002A1 RID: 673
			None,
			// Token: 0x040002A2 RID: 674
			Path,
			// Token: 0x040002A3 RID: 675
			Height,
			// Token: 0x040002A4 RID: 676
			Edit
		}
	}
}
