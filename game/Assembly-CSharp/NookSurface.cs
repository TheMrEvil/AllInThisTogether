using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000125 RID: 293
[RequireComponent(typeof(MeshCollider))]
public class NookSurface : MonoBehaviour
{
	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00058078 File Offset: 0x00056278
	public float Size
	{
		get
		{
			if (this._size > 0f)
			{
				return this._size;
			}
			this._size = Mathf.Max(new float[]
			{
				this.col.bounds.size.x,
				this.col.bounds.size.y,
				this.col.bounds.size.z
			});
			return this._size;
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x00058101 File Offset: 0x00056301
	private void Awake()
	{
		this.col = base.GetComponent<MeshCollider>();
		this.col.convex = true;
		base.gameObject.layer = 5;
		this.RecalculateMesh();
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0005812D File Offset: 0x0005632D
	public bool CanHold(NookItem item)
	{
		return (item.AllowedSurfaces == NookSurface.SurfaceType.Any || item.AllowedSurfaces.HasFlag(this.Surface)) && item.Size <= this.Size;
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x00058168 File Offset: 0x00056368
	public void RecalculateMesh()
	{
		Mesh sharedMesh = NookSurface.CreateMesh(this.Points.ToArray(), -this.extrude, this.extrude);
		this.col.sharedMesh = sharedMesh;
		this._size = 0f;
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x000581AC File Offset: 0x000563AC
	private void OnDrawGizmos()
	{
		if (this.Points.Count < 2)
		{
			return;
		}
		Gizmos.color = Color.green;
		for (int i = 0; i < this.Points.Count - 1; i++)
		{
			Gizmos.DrawLine(this.GetLocalPoint(this.Points[i]), this.GetLocalPoint(this.Points[i + 1]));
		}
		Vector3 localPoint = this.GetLocalPoint(this.Points[0]);
		List<Vector2> points = this.Points;
		int index = points.Count - 1;
		Gizmos.DrawLine(localPoint, this.GetLocalPoint(points[index]));
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00058248 File Offset: 0x00056448
	public bool IsInBounds(NookItem item)
	{
		bool result = true;
		List<Vector3> list = new List<Vector3>();
		if (item.BoundPoints.Count > 0)
		{
			using (List<Transform>.Enumerator enumerator = item.BoundPoints.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Transform transform = enumerator.Current;
					list.Add(transform.position);
				}
				goto IL_1FD;
			}
		}
		MeshRenderer meshRenderer = item.Display[0];
		Bounds localBounds = meshRenderer.localBounds;
		Transform transform2 = meshRenderer.transform;
		Vector3 center = localBounds.center;
		Vector3 extents = localBounds.extents;
		list.Add(transform2.TransformPoint(center + new Vector3(extents.x, extents.y, extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(extents.x, extents.y, -extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(extents.x, -extents.y, extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(extents.x, -extents.y, -extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(-extents.x, extents.y, extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(-extents.x, extents.y, -extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(-extents.x, -extents.y, extents.z)));
		list.Add(transform2.TransformPoint(center + new Vector3(-extents.x, -extents.y, -extents.z)));
		IL_1FD:
		foreach (Vector3 p in list)
		{
			Vector3 surfacePoint = this.GetSurfacePoint(p);
			if (this.col.ClosestPoint(surfacePoint) != surfacePoint)
			{
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x000584C0 File Offset: 0x000566C0
	public Vector3 GetSurfacePoint(Vector3 p)
	{
		Vector3 vector = base.transform.position - p;
		Vector3 vector2 = base.transform.forward * -1f;
		float d = Mathf.Cos(Vector3.Angle(vector.normalized, vector2) * 0.017453292f) * vector.magnitude;
		return p + vector2 * d;
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00058524 File Offset: 0x00056724
	public Vector3 GetInboundSurfacePoint(Vector3 p)
	{
		Vector3 vector = this.GetSurfacePoint(p);
		vector = this.col.ClosestPoint(vector);
		return this.GetSurfacePoint(vector);
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00058550 File Offset: 0x00056750
	public bool AnyIntersections(NookItem item)
	{
		foreach (NookItem nookItem in this.Items)
		{
			if (!(nookItem == item) && nookItem.Overlaps(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000585B8 File Offset: 0x000567B8
	public void AddValidSubsurfaces(NookItem item, ref List<NookSurface> validSurfaces)
	{
		foreach (NookItem nookItem in this.Items)
		{
			nookItem.AddValidSubsurfaces(item, ref validSurfaces);
		}
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0005860C File Offset: 0x0005680C
	public void LoadJSONItems(JSONArray items, PlayerNook nook)
	{
		for (int i = 0; i < items.Count; i++)
		{
			NookItem.CreateItem(items[i] as JSONObject, this, nook);
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00058640 File Offset: 0x00056840
	[return: TupleElementNames(new string[]
	{
		"surface",
		"pt"
	})]
	public static ValueTuple<NookSurface, Vector3> GetAimPoint(List<NookSurface> surfaces)
	{
		if (PlayerControl.MyCamera == null)
		{
			return new ValueTuple<NookSurface, Vector3>(null, Vector3.zero);
		}
		Transform transform = PlayerControl.MyCamera.transform;
		Vector3 position = transform.position;
		Vector3 forward = transform.forward;
		LayerMask surfaceMask = PlayerNook.MyNook.SurfaceMask;
		List<RaycastHit> list = Physics.RaycastAll(position, forward, 32f, surfaceMask).ToList<RaycastHit>();
		if (list.Count > 0)
		{
			list.Sort((RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));
			foreach (RaycastHit raycastHit in list)
			{
				NookSurface component = raycastHit.collider.GetComponent<NookSurface>();
				if (!(component == null) && surfaces.Contains(component))
				{
					return new ValueTuple<NookSurface, Vector3>(component, component.GetSurfacePoint(raycastHit.point));
				}
			}
		}
		float d = 15f;
		List<RaycastHit> list2 = Physics.RaycastAll(position, forward, 32f).ToList<RaycastHit>();
		list2.Sort((RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));
		foreach (RaycastHit raycastHit2 in list2)
		{
			if (!(raycastHit2.collider.GetComponentInParent<NookItem>() != null))
			{
				d = raycastHit2.distance;
				break;
			}
		}
		return new ValueTuple<NookSurface, Vector3>(null, position + forward * d);
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x000587F8 File Offset: 0x000569F8
	private static Mesh CreateMesh(Vector2[] poly, float frontDistance = -10f, float backDistance = 10f)
	{
		frontDistance = Mathf.Min(frontDistance, 0f);
		backDistance = Mathf.Max(backDistance, 0f);
		int[] array = new NookSurface.Triangulator(poly).Triangulate();
		Mesh mesh = new Mesh();
		Vector3[] array2 = new Vector3[poly.Length * 2];
		for (int i = 0; i < poly.Length; i++)
		{
			array2[i].x = poly[i].x;
			array2[i].y = poly[i].y;
			array2[i].z = frontDistance;
			array2[i + poly.Length].x = poly[i].x;
			array2[i + poly.Length].y = poly[i].y;
			array2[i + poly.Length].z = backDistance;
		}
		int[] array3 = new int[array.Length * 2 + poly.Length * 6];
		int num = 0;
		for (int j = 0; j < array.Length; j += 3)
		{
			array3[j] = array[j];
			array3[j + 1] = array[j + 1];
			array3[j + 2] = array[j + 2];
		}
		num += array.Length;
		for (int k = 0; k < array.Length; k += 3)
		{
			array3[num + k] = array[k + 2] + poly.Length;
			array3[num + k + 1] = array[k + 1] + poly.Length;
			array3[num + k + 2] = array[k] + poly.Length;
		}
		num += array.Length;
		for (int l = 0; l < poly.Length; l++)
		{
			int num2 = (l + 1) % poly.Length;
			array3[num] = l;
			array3[num + 1] = num2;
			array3[num + 2] = l + poly.Length;
			array3[num + 3] = num2;
			array3[num + 4] = num2 + poly.Length;
			array3[num + 5] = l + poly.Length;
			num += 6;
		}
		mesh.vertices = array2;
		mesh.triangles = array3;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.Optimize();
		return mesh;
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00058A00 File Offset: 0x00056C00
	private Vector3 GetLocalPoint(Vector2 point)
	{
		return base.transform.position + point.x * base.transform.right + point.y * base.transform.up;
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00058A50 File Offset: 0x00056C50
	public NookSurface()
	{
	}

	// Token: 0x04000B4F RID: 2895
	public NookSurface.SurfaceType Surface = NookSurface.SurfaceType.Flat;

	// Token: 0x04000B50 RID: 2896
	public List<Vector2> Points = new List<Vector2>
	{
		new Vector2(-1f, 1f),
		new Vector2(-1f, -1f),
		new Vector2(1f, -1f),
		new Vector2(1f, 1f)
	};

	// Token: 0x04000B51 RID: 2897
	private MeshCollider col;

	// Token: 0x04000B52 RID: 2898
	private float extrude = 0.075f;

	// Token: 0x04000B53 RID: 2899
	[NonSerialized]
	public NookItem Parent;

	// Token: 0x04000B54 RID: 2900
	[NonSerialized]
	public List<NookItem> Items = new List<NookItem>();

	// Token: 0x04000B55 RID: 2901
	private float _size;

	// Token: 0x02000532 RID: 1330
	public class Triangulator
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x000CCB12 File Offset: 0x000CAD12
		public Triangulator(Vector2[] points)
		{
			this.m_points = new List<Vector2>(points);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000CCB34 File Offset: 0x000CAD34
		public int[] Triangulate()
		{
			List<int> list = new List<int>();
			int count = this.m_points.Count;
			if (count < 3)
			{
				return list.ToArray();
			}
			int[] array = new int[count];
			if (this.Area() > 0f)
			{
				for (int i = 0; i < count; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					array[j] = count - 1 - j;
				}
			}
			int k = count;
			int num = 2 * k;
			int num2 = 0;
			int num3 = k - 1;
			while (k > 2)
			{
				if (num-- <= 0)
				{
					return list.ToArray();
				}
				int num4 = num3;
				if (k <= num4)
				{
					num4 = 0;
				}
				num3 = num4 + 1;
				if (k <= num3)
				{
					num3 = 0;
				}
				int num5 = num3 + 1;
				if (k <= num5)
				{
					num5 = 0;
				}
				if (this.Snip(num4, num3, num5, k, array))
				{
					int item = array[num4];
					int item2 = array[num3];
					int item3 = array[num5];
					list.Add(item);
					list.Add(item2);
					list.Add(item3);
					num2++;
					int num6 = num3;
					for (int l = num3 + 1; l < k; l++)
					{
						array[num6] = array[l];
						num6++;
					}
					k--;
					num = 2 * k;
				}
			}
			list.Reverse();
			return list.ToArray();
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000CCC74 File Offset: 0x000CAE74
		private float Area()
		{
			int count = this.m_points.Count;
			float num = 0f;
			int index = count - 1;
			int i = 0;
			while (i < count)
			{
				Vector2 vector = this.m_points[index];
				Vector2 vector2 = this.m_points[i];
				num += vector.x * vector2.y - vector2.x * vector.y;
				index = i++;
			}
			return num * 0.5f;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000CCCEC File Offset: 0x000CAEEC
		private bool Snip(int u, int v, int w, int n, int[] V)
		{
			Vector2 vector = this.m_points[V[u]];
			Vector2 vector2 = this.m_points[V[v]];
			Vector2 vector3 = this.m_points[V[w]];
			if (Mathf.Epsilon > (vector2.x - vector.x) * (vector3.y - vector.y) - (vector2.y - vector.y) * (vector3.x - vector.x))
			{
				return false;
			}
			for (int i = 0; i < n; i++)
			{
				if (i != u && i != v && i != w)
				{
					Vector2 p = this.m_points[V[i]];
					if (this.InsideTriangle(vector, vector2, vector3, p))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000CCDA4 File Offset: 0x000CAFA4
		private bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
		{
			float num = C.x - B.x;
			float num2 = C.y - B.y;
			float num3 = A.x - C.x;
			float num4 = A.y - C.y;
			float num5 = B.x - A.x;
			float num6 = B.y - A.y;
			float num7 = P.x - A.x;
			float num8 = P.y - A.y;
			float num9 = P.x - B.x;
			float num10 = P.y - B.y;
			float num11 = P.x - C.x;
			float num12 = P.y - C.y;
			float num13 = num * num10 - num2 * num9;
			float num14 = num5 * num8 - num6 * num7;
			float num15 = num3 * num12 - num4 * num11;
			return num13 >= 0f && num15 >= 0f && num14 >= 0f;
		}

		// Token: 0x04002643 RID: 9795
		private List<Vector2> m_points = new List<Vector2>();
	}

	// Token: 0x02000533 RID: 1331
	[Flags]
	public enum SurfaceType
	{
		// Token: 0x04002645 RID: 9797
		Flat = 2,
		// Token: 0x04002646 RID: 9798
		Wall = 4,
		// Token: 0x04002647 RID: 9799
		Any = 6
	}

	// Token: 0x02000534 RID: 1332
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600240E RID: 9230 RVA: 0x000CCEA2 File Offset: 0x000CB0A2
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000CCEAE File Offset: 0x000CB0AE
		public <>c()
		{
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000CCEB8 File Offset: 0x000CB0B8
		internal int <GetAimPoint>b__19_0(RaycastHit a, RaycastHit b)
		{
			return a.distance.CompareTo(b.distance);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000CCEDC File Offset: 0x000CB0DC
		internal int <GetAimPoint>b__19_1(RaycastHit a, RaycastHit b)
		{
			return a.distance.CompareTo(b.distance);
		}

		// Token: 0x04002648 RID: 9800
		public static readonly NookSurface.<>c <>9 = new NookSurface.<>c();

		// Token: 0x04002649 RID: 9801
		public static Comparison<RaycastHit> <>9__19_0;

		// Token: 0x0400264A RID: 9802
		public static Comparison<RaycastHit> <>9__19_1;
	}
}
