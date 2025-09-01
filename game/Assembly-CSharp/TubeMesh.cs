using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class TubeMesh : MonoBehaviour
{
	// Token: 0x0600186B RID: 6251 RVA: 0x000988F8 File Offset: 0x00096AF8
	public void BuildMesh()
	{
		if (this.filter == null)
		{
			this.filter = base.GetComponent<MeshFilter>();
		}
		this.radius = this.Radius;
		if (this.mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(this.mesh);
		}
		this.mesh = new Mesh();
		this.mesh.MarkDynamic();
		this.filter.sharedMesh = this.mesh;
		this.UpdateSize(this.Length, 1f, true);
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x00098980 File Offset: 0x00096B80
	private void Awake()
	{
		this.radius = this.Radius;
		this.filter = base.GetComponent<MeshFilter>();
		this.mat = base.GetComponent<MeshRenderer>().material;
		this.mat.SetFloat("_ScrollOffset", UnityEngine.Random.Range(0f, 5f));
		this.mesh = new Mesh();
		this.mesh.MarkDynamic();
		this.filter.sharedMesh = this.mesh;
		this.UpdateSize(this.Length, 1f, true);
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x00098A10 File Offset: 0x00096C10
	public void UpdateSize(float length, float radiusMult, bool force = false)
	{
		if (!force && this.updateT > 0f && Mathf.Approximately(this.radius, this.Radius * radiusMult))
		{
			this.updateT -= Time.deltaTime;
			return;
		}
		if (!force && Mathf.Approximately(this.Length, length) && Mathf.Approximately(this.radius, this.Radius * radiusMult))
		{
			return;
		}
		this.updateT = UnityEngine.Random.Range(0.05f, 0.15f);
		this.Length = length;
		this.Length = Mathf.Max(this.Length, this.SegmentLengthMult * this.segmentLength + 0.01f);
		this.radius = this.Radius * radiusMult;
		int num = Mathf.Max(1, Mathf.RoundToInt(this.Length / (this.SegmentLengthMult * this.segmentLength)));
		if (num != this.previousTubeSegments || this.RadialSegments != this.previousRadialSegments || this.mesh == null)
		{
			this.BuildMeshTopology(num, this.RadialSegments);
			this.previousTubeSegments = num;
			this.previousRadialSegments = this.RadialSegments;
		}
		this.UpdateMeshGeometry(num, this.RadialSegments);
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x00098B3C File Offset: 0x00096D3C
	private void BuildMeshTopology(int tubeSegments, int radialSegments)
	{
		int num = (tubeSegments + 1) * (radialSegments + 1);
		if (this.verticesArray == null || this.verticesArray.Length != num)
		{
			this.verticesArray = new Vector3[num];
			this.normalsArray = new Vector3[num];
			this.tangentsArray = new Vector4[num];
			this.uvsArray = new Vector2[num];
		}
		int num2 = tubeSegments * radialSegments * 6;
		if (this.indicesArray == null || this.indicesArray.Length != num2)
		{
			this.indicesArray = new int[num2];
		}
		int num3 = 0;
		for (int i = 1; i <= tubeSegments; i++)
		{
			for (int j = 1; j <= radialSegments; j++)
			{
				int num4 = (radialSegments + 1) * (i - 1) + (j - 1);
				int num5 = (radialSegments + 1) * i + (j - 1);
				int num6 = (radialSegments + 1) * i + j;
				int num7 = (radialSegments + 1) * (i - 1) + j;
				this.indicesArray[num3++] = num4;
				this.indicesArray[num3++] = num7;
				this.indicesArray[num3++] = num5;
				this.indicesArray[num3++] = num5;
				this.indicesArray[num3++] = num7;
				this.indicesArray[num3++] = num6;
			}
		}
		this.mesh.Clear();
		this.mesh.vertices = this.verticesArray;
		this.mesh.normals = this.normalsArray;
		this.mesh.tangents = this.tangentsArray;
		this.mesh.uv = this.uvsArray;
		this.mesh.SetIndices(this.indicesArray, MeshTopology.Triangles, 0);
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x00098CCC File Offset: 0x00096ECC
	private void UpdateMeshGeometry(int tubeSegments, int radialSegments)
	{
		float num = 1f / (float)radialSegments;
		float num2 = 6.2831855f * num;
		int num3 = 0;
		for (int i = 0; i <= tubeSegments; i++)
		{
			float num4 = (float)i / (float)tubeSegments;
			float d = (float)i * (this.SegmentLengthMult * this.segmentLength);
			Vector3 a = Vector3.forward * d;
			Vector3 a2 = -Vector3.up;
			Vector3 right = Vector3.right;
			float d2 = this.radius * this.RadiusCurve.Evaluate(num4);
			for (int j = 0; j <= radialSegments; j++)
			{
				float x = (float)j / (float)radialSegments;
				float f = (float)j * num2;
				float d3 = Mathf.Sin(f);
				Vector3 normalized = (Mathf.Cos(f) * a2 + d3 * right).normalized;
				this.verticesArray[num3] = a + d2 * normalized;
				this.normalsArray[num3] = normalized;
				Vector3 forward = Vector3.forward;
				this.tangentsArray[num3] = new Vector4(forward.x, forward.y, forward.z, 0f);
				this.uvsArray[num3] = new Vector2(x, num4);
				num3++;
			}
		}
		this.mesh.vertices = this.verticesArray;
		this.mesh.normals = this.normalsArray;
		this.mesh.tangents = this.tangentsArray;
		this.mesh.uv = this.uvsArray;
		this.mesh.RecalculateBounds();
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x00098E64 File Offset: 0x00097064
	public void TickClipOut(float delta)
	{
		float num = this.mat.GetFloat("_ClipStrength");
		if (num < 1f)
		{
			num = Mathf.Clamp(num + delta, 0f, 1f);
			this.mat.SetFloat("_ClipStrength", num);
		}
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x00098EAE File Offset: 0x000970AE
	private void OnDestroy()
	{
		if (this.mesh != null)
		{
			UnityEngine.Object.Destroy(this.mesh);
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x00098ECC File Offset: 0x000970CC
	public TubeMesh()
	{
	}

	// Token: 0x0400183E RID: 6206
	public float Length = 3f;

	// Token: 0x0400183F RID: 6207
	public float Radius = 0.5f;

	// Token: 0x04001840 RID: 6208
	private float radius = 0.5f;

	// Token: 0x04001841 RID: 6209
	[Range(3f, 10f)]
	public int RadialSegments = 6;

	// Token: 0x04001842 RID: 6210
	public AnimationCurve RadiusCurve;

	// Token: 0x04001843 RID: 6211
	public float SegmentLengthMult = 1f;

	// Token: 0x04001844 RID: 6212
	private float segmentLength = 1f;

	// Token: 0x04001845 RID: 6213
	private Mesh mesh;

	// Token: 0x04001846 RID: 6214
	private Vector3[] verticesArray;

	// Token: 0x04001847 RID: 6215
	private Vector3[] normalsArray;

	// Token: 0x04001848 RID: 6216
	private Vector4[] tangentsArray;

	// Token: 0x04001849 RID: 6217
	private Vector2[] uvsArray;

	// Token: 0x0400184A RID: 6218
	private int[] indicesArray;

	// Token: 0x0400184B RID: 6219
	private int previousTubeSegments = -1;

	// Token: 0x0400184C RID: 6220
	private int previousRadialSegments = -1;

	// Token: 0x0400184D RID: 6221
	private MeshFilter filter;

	// Token: 0x0400184E RID: 6222
	private Material mat;

	// Token: 0x0400184F RID: 6223
	private float updateT;
}
