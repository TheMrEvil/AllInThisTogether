using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000008 RID: 8
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[RequireComponent(typeof(ProBuilderMesh))]
	internal sealed class BezierShape : MonoBehaviour
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002DD1 File Offset: 0x00000FD1
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public bool isEditing
		{
			get
			{
				return this.m_IsEditing;
			}
			set
			{
				this.m_IsEditing = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002DE2 File Offset: 0x00000FE2
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002E04 File Offset: 0x00001004
		public ProBuilderMesh mesh
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

		// Token: 0x06000048 RID: 72 RVA: 0x00002E10 File Offset: 0x00001010
		public void Init()
		{
			Vector3 vector = new Vector3(0f, 0f, 2f);
			Vector3 vector2 = new Vector3(3f, 0f, 0f);
			this.points.Add(new BezierPoint(Vector3.zero, -vector, vector, Quaternion.identity));
			this.points.Add(new BezierPoint(vector2, vector2 + vector, vector2 + -vector, Quaternion.identity));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E94 File Offset: 0x00001094
		public void Refresh()
		{
			if (this.points.Count < 2)
			{
				this.mesh.Clear();
				this.mesh.ToMesh(MeshTopology.Triangles);
				this.mesh.Refresh(RefreshMask.All);
				return;
			}
			ProBuilderMesh mesh = this.mesh;
			Spline.Extrude(this.points, this.radius, this.columns, this.rows, this.closeLoop, this.smooth, ref mesh);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F06 File Offset: 0x00001106
		public BezierShape()
		{
		}

		// Token: 0x04000017 RID: 23
		public List<BezierPoint> points = new List<BezierPoint>();

		// Token: 0x04000018 RID: 24
		public bool closeLoop;

		// Token: 0x04000019 RID: 25
		public float radius = 0.5f;

		// Token: 0x0400001A RID: 26
		public int rows = 8;

		// Token: 0x0400001B RID: 27
		public int columns = 16;

		// Token: 0x0400001C RID: 28
		public bool smooth = true;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private bool m_IsEditing;

		// Token: 0x0400001E RID: 30
		private ProBuilderMesh m_Mesh;
	}
}
