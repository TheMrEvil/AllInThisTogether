using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[ExecuteInEditMode]
public class AuraController : MonoBehaviour
{
	// Token: 0x06000030 RID: 48 RVA: 0x00004820 File Offset: 0x00002A20
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(base.transform.position, this.resultRadius);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00004842 File Offset: 0x00002A42
	private void Start()
	{
		this.projector = base.GetComponent<Projector>();
		this.projector.material = new Material(this.sourceMaterial);
		this.UpdateMaterialAndProjector();
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000486C File Offset: 0x00002A6C
	private void Update()
	{
		this.UpdateMaterialAndProjector();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004874 File Offset: 0x00002A74
	public void UpdateMaterialAndProjector()
	{
		this.resultRadius = base.gameObject.transform.lossyScale.x + this.auraMargin;
		this.projector.material.SetVector("_AuraSourcePosition", base.transform.position);
		this.projector.material.SetFloat("_MaskDistance", base.gameObject.transform.lossyScale.x);
		this.projector.material.SetFloat("_Opacity", this.Opacity);
		this.projector.nearClipPlane = -this.resultRadius;
		this.projector.farClipPlane = this.resultRadius;
		this.projector.orthographicSize = this.resultRadius;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004941 File Offset: 0x00002B41
	public void AssignSourceMaterial()
	{
		this.projector.material = this.sourceMaterial;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00004954 File Offset: 0x00002B54
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

	// Token: 0x06000036 RID: 54 RVA: 0x000049B1 File Offset: 0x00002BB1
	public AuraController()
	{
	}

	// Token: 0x0400001C RID: 28
	public Material sourceMaterial;

	// Token: 0x0400001D RID: 29
	public float auraMargin = 0.75f;

	// Token: 0x0400001E RID: 30
	[Range(0f, 1f)]
	public float Opacity = 1f;

	// Token: 0x0400001F RID: 31
	private Projector projector;

	// Token: 0x04000020 RID: 32
	private float resultRadius;
}
