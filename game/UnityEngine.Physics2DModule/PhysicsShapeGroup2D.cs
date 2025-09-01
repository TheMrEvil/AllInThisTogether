using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	public class PhysicsShapeGroup2D
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005834 File Offset: 0x00003A34
		internal List<Vector2> groupVertices
		{
			get
			{
				return this.m_GroupState.m_Vertices;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005854 File Offset: 0x00003A54
		internal List<PhysicsShape2D> groupShapes
		{
			get
			{
				return this.m_GroupState.m_Shapes;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00005874 File Offset: 0x00003A74
		public int shapeCount
		{
			get
			{
				return this.m_GroupState.m_Shapes.Count;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005898 File Offset: 0x00003A98
		public int vertexCount
		{
			get
			{
				return this.m_GroupState.m_Vertices.Count;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000058BC File Offset: 0x00003ABC
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000058D9 File Offset: 0x00003AD9
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_GroupState.m_LocalToWorld;
			}
			set
			{
				this.m_GroupState.m_LocalToWorld = value;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000058E8 File Offset: 0x00003AE8
		public PhysicsShapeGroup2D([DefaultValue("1")] int shapeCapacity = 1, [DefaultValue("8")] int vertexCapacity = 8)
		{
			this.m_GroupState = new PhysicsShapeGroup2D.GroupState
			{
				m_Shapes = new List<PhysicsShape2D>(shapeCapacity),
				m_Vertices = new List<Vector2>(vertexCapacity),
				m_LocalToWorld = Matrix4x4.identity
			};
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005932 File Offset: 0x00003B32
		public void Clear()
		{
			this.m_GroupState.ClearGeometry();
			this.m_GroupState.m_LocalToWorld = Matrix4x4.identity;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005954 File Offset: 0x00003B54
		public void Add(PhysicsShapeGroup2D physicsShapeGroup)
		{
			bool flag = physicsShapeGroup == null;
			if (flag)
			{
				throw new ArgumentNullException("Cannot merge a NULL PhysicsShapeGroup2D.");
			}
			bool flag2 = physicsShapeGroup == this;
			if (flag2)
			{
				throw new ArgumentException("Cannot merge a PhysicsShapeGroup2D with itself.");
			}
			bool flag3 = physicsShapeGroup.shapeCount == 0;
			if (!flag3)
			{
				int count = this.groupShapes.Count;
				int vertexCount = physicsShapeGroup.vertexCount;
				this.groupShapes.AddRange(physicsShapeGroup.groupShapes);
				this.groupVertices.AddRange(physicsShapeGroup.groupVertices);
				bool flag4 = count > 0;
				if (flag4)
				{
					for (int i = count; i < this.m_GroupState.m_Shapes.Count; i++)
					{
						PhysicsShape2D value = this.m_GroupState.m_Shapes[i];
						value.vertexStartIndex += vertexCount;
						this.m_GroupState.m_Shapes[i] = value;
					}
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005A3F File Offset: 0x00003C3F
		public void GetShapeData(List<PhysicsShape2D> shapes, List<Vector2> vertices)
		{
			shapes.AddRange(this.groupShapes);
			vertices.AddRange(this.groupVertices);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005A5C File Offset: 0x00003C5C
		public void GetShapeData(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices)
		{
			bool flag = !shapes.IsCreated || shapes.Length != this.shapeCount;
			if (flag)
			{
				throw new ArgumentException(string.Format("Cannot get shape data as the native shapes array length must be identical to the current custom shape count of {0}.", this.shapeCount), "shapes");
			}
			bool flag2 = !vertices.IsCreated || vertices.Length != this.vertexCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Cannot get shape data as the native vertices array length must be identical to the current custom vertex count of {0}.", this.shapeCount), "vertices");
			}
			for (int i = 0; i < this.shapeCount; i++)
			{
				shapes[i] = this.m_GroupState.m_Shapes[i];
			}
			for (int j = 0; j < this.vertexCount; j++)
			{
				vertices[j] = this.m_GroupState.m_Vertices[j];
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005B54 File Offset: 0x00003D54
		public void GetShapeVertices(int shapeIndex, List<Vector2> vertices)
		{
			PhysicsShape2D shape = this.GetShape(shapeIndex);
			int vertexCount = shape.vertexCount;
			vertices.Clear();
			bool flag = vertices.Capacity < vertexCount;
			if (flag)
			{
				vertices.Capacity = vertexCount;
			}
			List<Vector2> groupVertices = this.groupVertices;
			int vertexStartIndex = shape.vertexStartIndex;
			for (int i = 0; i < vertexCount; i++)
			{
				vertices.Add(groupVertices[vertexStartIndex++]);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00005BCC File Offset: 0x00003DCC
		public Vector2 GetShapeVertex(int shapeIndex, int vertexIndex)
		{
			int num = this.GetShape(shapeIndex).vertexStartIndex + vertexIndex;
			bool flag = num < 0 || num >= this.groupVertices.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get shape-vertex at index {0}. There are {1} shape-vertices.", num, this.shapeCount));
			}
			return this.groupVertices[num];
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00005C3C File Offset: 0x00003E3C
		public void SetShapeVertex(int shapeIndex, int vertexIndex, Vector2 vertex)
		{
			int num = this.GetShape(shapeIndex).vertexStartIndex + vertexIndex;
			bool flag = num < 0 || num >= this.groupVertices.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set shape-vertex at index {0}. There are {1} shape-vertices.", num, this.shapeCount));
			}
			this.groupVertices[num] = vertex;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public void SetShapeRadius(int shapeIndex, float radius)
		{
			PhysicsShape2D shape = this.GetShape(shapeIndex);
			switch (shape.shapeType)
			{
			case PhysicsShapeType2D.Circle:
			{
				bool flag = radius <= 0f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Circle radius {0} must be greater than zero.", radius));
				}
				break;
			}
			case PhysicsShapeType2D.Capsule:
			{
				bool flag2 = radius <= 1E-05f;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Capsule radius: {0} is too small.", radius));
				}
				break;
			}
			case PhysicsShapeType2D.Polygon:
			case PhysicsShapeType2D.Edges:
				radius = Mathf.Max(0f, radius);
				break;
			}
			shape.radius = radius;
			this.groupShapes[shapeIndex] = shape;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005D58 File Offset: 0x00003F58
		public void SetShapeAdjacentVertices(int shapeIndex, bool useAdjacentStart, bool useAdjacentEnd, Vector2 adjacentStart, Vector2 adjacentEnd)
		{
			bool flag = shapeIndex < 0 || shapeIndex >= this.shapeCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set shape adjacent vertices at index {0}. There are {1} shapes(s).", shapeIndex, this.shapeCount));
			}
			PhysicsShape2D value = this.groupShapes[shapeIndex];
			bool flag2 = value.shapeType != PhysicsShapeType2D.Edges;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Cannot set shape adjacent vertices at index {0}. The shape must be of type {1} but it is of typee {2}.", shapeIndex, PhysicsShapeType2D.Edges, value.shapeType));
			}
			value.useAdjacentStart = useAdjacentStart;
			value.useAdjacentEnd = useAdjacentEnd;
			value.adjacentStart = adjacentStart;
			value.adjacentEnd = adjacentEnd;
			this.groupShapes[shapeIndex] = value;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005E18 File Offset: 0x00004018
		public void DeleteShape(int shapeIndex)
		{
			bool flag = shapeIndex < 0 || shapeIndex >= this.shapeCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot delete shape at index {0}. There are {1} shapes(s).", shapeIndex, this.shapeCount));
			}
			PhysicsShape2D physicsShape2D = this.groupShapes[shapeIndex];
			int vertexCount = physicsShape2D.vertexCount;
			this.groupShapes.RemoveAt(shapeIndex);
			this.groupVertices.RemoveRange(physicsShape2D.vertexStartIndex, vertexCount);
			while (shapeIndex < this.groupShapes.Count)
			{
				PhysicsShape2D value = this.m_GroupState.m_Shapes[shapeIndex];
				value.vertexStartIndex -= vertexCount;
				this.m_GroupState.m_Shapes[shapeIndex++] = value;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005EE8 File Offset: 0x000040E8
		public PhysicsShape2D GetShape(int shapeIndex)
		{
			bool flag = shapeIndex < 0 || shapeIndex >= this.shapeCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get shape at index {0}. There are {1} shapes(s).", shapeIndex, this.shapeCount));
			}
			return this.groupShapes[shapeIndex];
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00005F40 File Offset: 0x00004140
		public int AddCircle(Vector2 center, float radius)
		{
			bool flag = radius <= 0f;
			if (flag)
			{
				throw new ArgumentException(string.Format("radius {0} must be greater than zero.", radius));
			}
			int count = this.groupVertices.Count;
			this.groupVertices.Add(center);
			this.groupShapes.Add(new PhysicsShape2D
			{
				shapeType = PhysicsShapeType2D.Circle,
				radius = radius,
				vertexStartIndex = count,
				vertexCount = 1
			});
			return this.groupShapes.Count - 1;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005FD8 File Offset: 0x000041D8
		public int AddCapsule(Vector2 vertex0, Vector2 vertex1, float radius)
		{
			bool flag = radius <= 1E-05f;
			if (flag)
			{
				throw new ArgumentException(string.Format("radius: {0} is too small.", radius));
			}
			int count = this.groupVertices.Count;
			this.groupVertices.Add(vertex0);
			this.groupVertices.Add(vertex1);
			this.groupShapes.Add(new PhysicsShape2D
			{
				shapeType = PhysicsShapeType2D.Capsule,
				radius = radius,
				vertexStartIndex = count,
				vertexCount = 2
			});
			return this.groupShapes.Count - 1;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000607C File Offset: 0x0000427C
		public int AddBox(Vector2 center, Vector2 size, [DefaultValue("0f")] float angle = 0f, [DefaultValue("0f")] float edgeRadius = 0f)
		{
			bool flag = size.x <= 0.0025f || size.y <= 0.0025f;
			if (flag)
			{
				throw new ArgumentException(string.Format("size: {0} is too small. Vertex need to be separated by at least {1}", size, 0.0025f));
			}
			edgeRadius = Mathf.Max(0f, edgeRadius);
			angle *= 0.017453292f;
			float cos = Mathf.Cos(angle);
			float sin = Mathf.Sin(angle);
			Vector2 vector = size * 0.5f;
			Vector2 item = center + PhysicsShapeGroup2D.<AddBox>g__Rotate|28_0(cos, sin, -vector);
			Vector2 item2 = center + PhysicsShapeGroup2D.<AddBox>g__Rotate|28_0(cos, sin, new Vector2(vector.x, -vector.y));
			Vector2 item3 = center + PhysicsShapeGroup2D.<AddBox>g__Rotate|28_0(cos, sin, vector);
			Vector2 item4 = center + PhysicsShapeGroup2D.<AddBox>g__Rotate|28_0(cos, sin, new Vector2(-vector.x, vector.y));
			int count = this.groupVertices.Count;
			this.groupVertices.Add(item);
			this.groupVertices.Add(item2);
			this.groupVertices.Add(item3);
			this.groupVertices.Add(item4);
			this.groupShapes.Add(new PhysicsShape2D
			{
				shapeType = PhysicsShapeType2D.Polygon,
				radius = edgeRadius,
				vertexStartIndex = count,
				vertexCount = 4
			});
			return this.groupShapes.Count - 1;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000061FC File Offset: 0x000043FC
		public int AddPolygon(List<Vector2> vertices)
		{
			int count = vertices.Count;
			bool flag = count < 3 || count > 8;
			if (flag)
			{
				throw new ArgumentException(string.Format("Vertex Count {0} must be >= 3 and <= {1}.", count, 8));
			}
			float num = 6.25E-06f;
			for (int i = 1; i < count; i++)
			{
				Vector2 vector = vertices[i - 1];
				Vector2 vector2 = vertices[i];
				bool flag2 = (vector2 - vector).sqrMagnitude <= num;
				if (flag2)
				{
					throw new ArgumentException(string.Format("vertices: {0} and {1} are too close. Vertices need to be separated by at least {2}", vector, vector2, num));
				}
			}
			int count2 = this.groupVertices.Count;
			this.groupVertices.AddRange(vertices);
			this.groupShapes.Add(new PhysicsShape2D
			{
				shapeType = PhysicsShapeType2D.Polygon,
				radius = 0f,
				vertexStartIndex = count2,
				vertexCount = count
			});
			return this.groupShapes.Count - 1;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000631C File Offset: 0x0000451C
		public int AddEdges(List<Vector2> vertices, [DefaultValue("0f")] float edgeRadius = 0f)
		{
			return this.AddEdges(vertices, false, false, Vector2.zero, Vector2.zero, edgeRadius);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006344 File Offset: 0x00004544
		public int AddEdges(List<Vector2> vertices, bool useAdjacentStart, bool useAdjacentEnd, Vector2 adjacentStart, Vector2 adjacentEnd, [DefaultValue("0f")] float edgeRadius = 0f)
		{
			int count = vertices.Count;
			bool flag = count < 2;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Vertex Count {0} must be >= 2.", count));
			}
			edgeRadius = Mathf.Max(0f, edgeRadius);
			int count2 = this.groupVertices.Count;
			this.groupVertices.AddRange(vertices);
			this.groupShapes.Add(new PhysicsShape2D
			{
				shapeType = PhysicsShapeType2D.Edges,
				radius = edgeRadius,
				vertexStartIndex = count2,
				vertexCount = count,
				useAdjacentStart = useAdjacentStart,
				useAdjacentEnd = useAdjacentEnd,
				adjacentStart = adjacentStart,
				adjacentEnd = adjacentEnd
			});
			return this.groupShapes.Count - 1;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006414 File Offset: 0x00004614
		[CompilerGenerated]
		internal static Vector2 <AddBox>g__Rotate|28_0(float cos, float sin, Vector2 value)
		{
			return new Vector2(cos * value.x - sin * value.y, sin * value.x + cos * value.y);
		}

		// Token: 0x04000045 RID: 69
		internal PhysicsShapeGroup2D.GroupState m_GroupState;

		// Token: 0x04000046 RID: 70
		private const float MinVertexSeparation = 0.0025f;

		// Token: 0x02000014 RID: 20
		[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
		internal struct GroupState
		{
			// Token: 0x06000204 RID: 516 RVA: 0x0000644D File Offset: 0x0000464D
			public void ClearGeometry()
			{
				this.m_Shapes.Clear();
				this.m_Vertices.Clear();
			}

			// Token: 0x04000047 RID: 71
			[NativeName("shapesList")]
			public List<PhysicsShape2D> m_Shapes;

			// Token: 0x04000048 RID: 72
			[NativeName("verticesList")]
			public List<Vector2> m_Vertices;

			// Token: 0x04000049 RID: 73
			[NativeName("localToWorld")]
			public Matrix4x4 m_LocalToWorld;
		}
	}
}
