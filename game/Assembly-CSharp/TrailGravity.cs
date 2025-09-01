using System;
using UnityEngine;

// Token: 0x0200025F RID: 607
[ExecuteAlways]
[RequireComponent(typeof(TrailRenderer))]
public class TrailGravity : MonoBehaviour
{
	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06001865 RID: 6245 RVA: 0x000986F8 File Offset: 0x000968F8
	private TrailRenderer trail
	{
		get
		{
			if (this._tr == null)
			{
				this._tr = base.GetComponent<TrailRenderer>();
				this.offset = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(5f, 1000f);
			}
			return this._tr;
		}
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x00098744 File Offset: 0x00096944
	private void LateUpdate()
	{
		for (int i = 0; i < this.trail.positionCount; i++)
		{
			Vector3 vector = this.trail.GetPosition(i);
			vector = this.MovePoint(vector, Time.deltaTime);
			this.trail.SetPosition(i, vector);
		}
		if (this.trail.positionCount == 0 || Vector3.Distance(this.trail.GetPosition(this.trail.positionCount - 1), base.transform.position) > this._tr.minVertexDistance)
		{
			this.trail.AddPosition(base.transform.position);
		}
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x000987E8 File Offset: 0x000969E8
	private Vector3 MovePoint(Vector3 v, float delta)
	{
		Vector3 b = Physics.gravity * delta * this.GravityForce;
		this.offset += Vector3.one * delta;
		Vector3 noise = this.GetNoise(v);
		return v + b + noise * delta;
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x00098844 File Offset: 0x00096A44
	private Vector3 GetNoise(Vector3 v)
	{
		if (this.NoiseForce == 0f)
		{
			return Vector3.zero;
		}
		Vector3 vector = this.offset * this.NoiseScale;
		float x = Mathf.Lerp(-1f, 1f, Mathf.PerlinNoise(vector.x, vector.x));
		float y = Mathf.Lerp(-1f, 1f, Mathf.PerlinNoise(vector.y, vector.y));
		float z = Mathf.Lerp(-1f, 1f, Mathf.PerlinNoise(vector.z, vector.z));
		return new Vector3(x, y, z) * this.NoiseForce;
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x000988EC File Offset: 0x00096AEC
	private void OnDrawGizmos()
	{
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x000988EE File Offset: 0x00096AEE
	public TrailGravity()
	{
	}

	// Token: 0x04001838 RID: 6200
	[Range(-5f, 5f)]
	public float GravityForce;

	// Token: 0x04001839 RID: 6201
	[Range(0f, 5f)]
	public float NoiseForce;

	// Token: 0x0400183A RID: 6202
	[Range(0.1f, 10f)]
	public float NoiseScale;

	// Token: 0x0400183B RID: 6203
	private Vector3 offset;

	// Token: 0x0400183C RID: 6204
	private TrailRenderer _tr;

	// Token: 0x0400183D RID: 6205
	private float lastSpawn;
}
