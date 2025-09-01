using System;
using UnityEngine.ProBuilder.MeshOperations;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000070 RID: 112
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	internal sealed class ProBuilderShape : MonoBehaviour
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00026BEA File Offset: 0x00024DEA
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00026BF2 File Offset: 0x00024DF2
		public Shape shape
		{
			get
			{
				return this.m_Shape;
			}
			set
			{
				this.m_Shape = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00026BFB File Offset: 0x00024DFB
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00026C03 File Offset: 0x00024E03
		public PivotLocation pivotLocation
		{
			get
			{
				return this.m_PivotLocation;
			}
			set
			{
				this.m_PivotLocation = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00026C0C File Offset: 0x00024E0C
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00026C14 File Offset: 0x00024E14
		public Vector3 pivotLocalPosition
		{
			get
			{
				return this.m_PivotPosition;
			}
			set
			{
				this.m_PivotPosition = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00026C1D File Offset: 0x00024E1D
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00026C35 File Offset: 0x00024E35
		public Vector3 pivotGlobalPosition
		{
			get
			{
				return this.mesh.transform.TransformPoint(this.m_PivotPosition);
			}
			set
			{
				this.pivotLocalPosition = this.mesh.transform.InverseTransformPoint(value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00026C4E File Offset: 0x00024E4E
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00026C58 File Offset: 0x00024E58
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size.x = ((Math.Abs(value.x) == 0f) ? (Mathf.Sign(this.m_Size.x) * 0.001f) : value.x);
				this.m_Size.y = value.y;
				this.m_Size.z = ((Math.Abs(value.z) == 0f) ? (Mathf.Sign(this.m_Size.z) * 0.001f) : value.z);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00026CEC File Offset: 0x00024EEC
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00026CF4 File Offset: 0x00024EF4
		public Quaternion rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00026D00 File Offset: 0x00024F00
		public Bounds editionBounds
		{
			get
			{
				this.m_EditionBounds.center = this.m_ShapeBox.center;
				this.m_EditionBounds.size = this.m_Size;
				if (Mathf.Abs(this.m_ShapeBox.size.y) < Mathf.Epsilon)
				{
					this.m_EditionBounds.size = new Vector3(this.m_Size.x, 0f, this.m_Size.z);
				}
				return this.m_EditionBounds;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00026D81 File Offset: 0x00024F81
		public Bounds shapeBox
		{
			get
			{
				return this.m_ShapeBox;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00026D89 File Offset: 0x00024F89
		public bool isEditable
		{
			get
			{
				return this.m_UnmodifiedMeshVersion == this.mesh.versionIndex;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00026DA0 File Offset: 0x00024FA0
		public ProBuilderMesh mesh
		{
			get
			{
				if (this.m_Mesh == null)
				{
					this.m_Mesh = base.GetComponent<ProBuilderMesh>();
				}
				if (this.m_Mesh == null)
				{
					this.m_Mesh = base.gameObject.AddComponent<ProBuilderMesh>();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00026DEC File Offset: 0x00024FEC
		private void OnValidate()
		{
			this.m_Size.x = ((Math.Abs(this.m_Size.x) == 0f) ? 0.001f : this.m_Size.x);
			this.m_Size.z = ((Math.Abs(this.m_Size.z) == 0f) ? 0.001f : this.m_Size.z);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00026E61 File Offset: 0x00025061
		internal void UpdateComponent()
		{
			this.ResetPivot(this.mesh, this.size, this.rotation);
			this.Rebuild();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00026E84 File Offset: 0x00025084
		internal void UpdateBounds(Bounds bounds)
		{
			Vector3 center = this.mesh.transform.InverseTransformPoint(bounds.center);
			Bounds shapeBox = this.m_ShapeBox;
			shapeBox.center = center;
			this.m_ShapeBox = shapeBox;
			this.ResetPivot(this.mesh, this.m_Size, this.m_Rotation);
			this.size = bounds.size;
			this.Rebuild();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00026EEC File Offset: 0x000250EC
		internal void Rebuild(Bounds bounds, Quaternion rotation, Vector3 cornerPivot)
		{
			Transform transform = base.transform;
			transform.position = bounds.center;
			transform.rotation = rotation;
			this.size = bounds.size;
			this.pivotGlobalPosition = ((this.pivotLocation == PivotLocation.Center) ? bounds.center : cornerPivot);
			this.Rebuild();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00026F40 File Offset: 0x00025140
		private void Rebuild()
		{
			if (base.gameObject == null || base.gameObject.hideFlags == HideFlags.HideAndDontSave)
			{
				return;
			}
			this.m_ShapeBox = this.m_Shape.RebuildMesh(this.mesh, this.size, this.rotation);
			this.RebuildPivot(this.size, this.rotation);
			Bounds shapeBox = this.m_ShapeBox;
			shapeBox.size = this.m_ShapeBox.size.Abs();
			MeshUtility.FitToSize(this.mesh, shapeBox, this.size);
			this.m_UnmodifiedMeshVersion = this.mesh.versionIndex;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00026FE4 File Offset: 0x000251E4
		internal void SetShape(Shape shape, PivotLocation location)
		{
			this.m_PivotLocation = location;
			this.m_Shape = shape;
			if (this.m_Shape is Plane || this.m_Shape is Sprite)
			{
				Bounds shapeBox = this.m_ShapeBox;
				Vector3 center = shapeBox.center;
				Vector3 size = shapeBox.size;
				center.y = 0f;
				size.y = 0f;
				shapeBox.center = center;
				shapeBox.size = size;
				this.m_ShapeBox = shapeBox;
				this.m_Size.y = 0f;
			}
			else if (this.pivotLocation == PivotLocation.FirstCorner && this.m_ShapeBox.size.y == 0f && this.size.y != 0f)
			{
				Bounds shapeBox2 = this.m_ShapeBox;
				Vector3 center2 = shapeBox2.center;
				Vector3 size2 = shapeBox2.size;
				center2.y += this.size.y / 2f;
				size2.y = this.size.y;
				shapeBox2.center = center2;
				shapeBox2.size = size2;
				this.m_ShapeBox = shapeBox2;
			}
			this.ResetPivot(this.mesh, this.size, this.rotation);
			this.Rebuild();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00027127 File Offset: 0x00025327
		internal void RotateInsideBounds(Quaternion deltaRotation)
		{
			this.ResetPivot(this.mesh, this.size, this.rotation);
			this.rotation = deltaRotation * this.rotation;
			this.Rebuild();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0002715C File Offset: 0x0002535C
		private void ResetPivot(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			if (mesh != null && mesh.mesh != null)
			{
				Vector3 worldPosition = mesh.transform.TransformPoint(this.m_ShapeBox.center);
				Vector3 position = mesh.transform.TransformPoint(this.m_PivotPosition);
				mesh.SetPivot(worldPosition);
				this.m_PivotPosition = mesh.transform.InverseTransformPoint(position);
				this.m_ShapeBox = this.m_Shape.UpdateBounds(mesh, size, rotation, this.m_ShapeBox);
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000271DC File Offset: 0x000253DC
		internal void RebuildPivot(Vector3 size, Quaternion rotation)
		{
			if (this.mesh != null)
			{
				Vector3 position = this.mesh.transform.TransformPoint(this.m_ShapeBox.center);
				Vector3 vector = this.mesh.transform.TransformPoint(this.m_PivotPosition);
				this.mesh.SetPivot(vector);
				this.m_ShapeBox.center = this.mesh.transform.InverseTransformPoint(position);
				this.m_PivotPosition = this.mesh.transform.InverseTransformPoint(vector);
				this.m_ShapeBox = this.m_Shape.UpdateBounds(this.mesh, size, rotation, this.m_ShapeBox);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0002728B File Offset: 0x0002548B
		public ProBuilderShape()
		{
		}

		// Token: 0x04000243 RID: 579
		[SerializeReference]
		private Shape m_Shape = new Cube();

		// Token: 0x04000244 RID: 580
		[SerializeField]
		private Vector3 m_Size = Vector3.one;

		// Token: 0x04000245 RID: 581
		[SerializeField]
		private Quaternion m_Rotation = Quaternion.identity;

		// Token: 0x04000246 RID: 582
		private ProBuilderMesh m_Mesh;

		// Token: 0x04000247 RID: 583
		[SerializeField]
		private PivotLocation m_PivotLocation;

		// Token: 0x04000248 RID: 584
		[SerializeField]
		private Vector3 m_PivotPosition;

		// Token: 0x04000249 RID: 585
		[SerializeField]
		internal ushort m_UnmodifiedMeshVersion;

		// Token: 0x0400024A RID: 586
		private Bounds m_EditionBounds;

		// Token: 0x0400024B RID: 587
		[SerializeField]
		private Bounds m_ShapeBox;
	}
}
