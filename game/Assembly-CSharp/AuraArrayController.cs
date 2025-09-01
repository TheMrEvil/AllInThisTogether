using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[ExecuteInEditMode]
public class AuraArrayController : MonoBehaviour
{
	// Token: 0x0600002A RID: 42 RVA: 0x00004484 File Offset: 0x00002684
	private void Start()
	{
		this.bounds = new Bounds(this.affectors[0].position, new Vector3((this.affectors[0].lossyScale.x + this.auraMargin) * 2f, (this.affectors[0].lossyScale.y + this.auraMargin) * 2f, (this.affectors[0].lossyScale.z + this.auraMargin) * 2f));
		this.projector = base.GetComponent<Projector>();
		this.projector.material = new Material(this.sourceMaterial);
		this.UpdateMaterialAndProjector();
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00004533 File Offset: 0x00002733
	private void Update()
	{
		this.UpdateMaterialAndProjector();
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000453C File Offset: 0x0000273C
	public void UpdateMaterialAndProjector()
	{
		this.bounds = new Bounds(this.affectors[0].position, new Vector3((this.affectors[0].lossyScale.x + this.auraMargin) * 2f, (this.affectors[0].lossyScale.y + this.auraMargin) * 2f, (this.affectors[0].lossyScale.z + this.auraMargin) * 2f));
		foreach (Transform transform in this.affectors)
		{
			Bounds bounds = new Bounds(transform.position, new Vector3((transform.lossyScale.x + this.auraMargin) * 2f, (transform.lossyScale.y + this.auraMargin) * 2f, (transform.lossyScale.z + this.auraMargin) * 2f));
			this.bounds.Encapsulate(bounds);
		}
		this.projector.gameObject.transform.position = new Vector3(this.bounds.center.x, this.bounds.max.y, this.bounds.center.z);
		this.projector.orthographicSize = Mathf.Max(this.bounds.extents.z, this.bounds.extents.x);
		this.projector.farClipPlane = this.bounds.size.y;
		this.positions = new Vector4[this.affectors.Length];
		for (int j = 0; j < this.positions.Length; j++)
		{
			this.positions[j] = this.affectors[j].position;
		}
		this.projector.material.SetVectorArray("_AffectorPositions", this.positions);
		this.scales = new float[this.affectors.Length];
		for (int k = 0; k < this.scales.Length; k++)
		{
			this.scales[k] = this.affectors[k].lossyScale.x;
		}
		this.projector.material.SetFloatArray("_AffectorScales", this.scales);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000479D File Offset: 0x0000299D
	public void AssignSourceMaterial()
	{
		this.projector.material = this.sourceMaterial;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000047B0 File Offset: 0x000029B0
	public void UpdateAllMaterialInstances()
	{
		AuraController[] array = UnityEngine.Object.FindObjectsOfType<AuraController>();
		for (int i = 0; i < array.Length; i++)
		{
			Projector component = array[i].gameObject.GetComponent<Projector>();
			if (component.material.name == this.sourceMaterial.name)
			{
				component.material = new Material(this.sourceMaterial);
			}
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000480D File Offset: 0x00002A0D
	public AuraArrayController()
	{
	}

	// Token: 0x04000015 RID: 21
	public Transform[] affectors;

	// Token: 0x04000016 RID: 22
	public Material sourceMaterial;

	// Token: 0x04000017 RID: 23
	public float auraMargin = 0.75f;

	// Token: 0x04000018 RID: 24
	private Projector projector;

	// Token: 0x04000019 RID: 25
	private Bounds bounds;

	// Token: 0x0400001A RID: 26
	private Vector4[] positions;

	// Token: 0x0400001B RID: 27
	private float[] scales;
}
