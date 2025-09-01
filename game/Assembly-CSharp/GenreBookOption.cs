using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class GenreBookOption : MonoBehaviour
{
	// Token: 0x06000817 RID: 2071 RVA: 0x00038D94 File Offset: 0x00036F94
	private void Awake()
	{
		this.mr = base.GetComponentInChildren<MeshRenderer>();
		Material material = this.MaterialOptions[UnityEngine.Random.Range(0, this.MaterialOptions.Count)];
		this.bookMat = new Material(material.shader);
		this.bookMat.CopyPropertiesFromMaterial(material);
		this.mr.material = this.bookMat;
		this.dissolveAmount = 1f;
		this._propBlock = new MaterialPropertyBlock();
		this.mr.GetPropertyBlock(this._propBlock, 0);
		this._propBlock.SetFloat("_DissolveAmount", this.dissolveAmount);
		this._propBlock.SetFloat("_Opacity", 1f - this.dissolveAmount);
		this.mr.SetPropertyBlock(this._propBlock, 0);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00038E64 File Offset: 0x00037064
	public void Setup(GenreTree genre)
	{
		this.Genre = genre;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00038E70 File Offset: 0x00037070
	public void Activate()
	{
		Material material = this.MaterialOptions[UnityEngine.Random.Range(0, this.MaterialOptions.Count)];
		this.bookMat = new Material(material.shader);
		this.bookMat.CopyPropertiesFromMaterial(material);
		this.mr.material = this.bookMat;
		this.dissolveAmount = 1f;
		this.IsAvailable = true;
		this.VFX.Play();
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00038EE8 File Offset: 0x000370E8
	private void Update()
	{
		if (this.dissolveAmount > 0f && this.IsAvailable)
		{
			this.dissolveAmount = Mathf.Clamp(this.dissolveAmount - Time.deltaTime * 1f, 0f, 1f);
		}
		else
		{
			if (this.dissolveAmount >= 1f || this.IsAvailable)
			{
				return;
			}
			this.dissolveAmount = Mathf.Clamp(this.dissolveAmount + Time.deltaTime * 1f, 0f, 1f);
		}
		for (int i = 0; i < this.mr.sharedMaterials.Length; i++)
		{
			this.mr.GetPropertyBlock(this._propBlock, i);
			this._propBlock.SetFloat("_DissolveAmount", this.dissolveAmount);
			this._propBlock.SetFloat("_Opacity", 1f - this.dissolveAmount);
			this.mr.SetPropertyBlock(this._propBlock, i);
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00038FE0 File Offset: 0x000371E0
	public void Deactivate()
	{
		this.IsAvailable = false;
		this.dissolveAmount = 0f;
		if (this.VFX != null)
		{
			this.VFX.Stop();
		}
		this.timeSinceDeactivated = Time.realtimeSinceStartup;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00039018 File Offset: 0x00037218
	public GenreBookOption()
	{
	}

	// Token: 0x040006D0 RID: 1744
	public Transform UIRoot;

	// Token: 0x040006D1 RID: 1745
	private MeshRenderer mr;

	// Token: 0x040006D2 RID: 1746
	public List<Material> MaterialOptions;

	// Token: 0x040006D3 RID: 1747
	public ParticleSystem VFX;

	// Token: 0x040006D4 RID: 1748
	public ParticleSystem VotePulse;

	// Token: 0x040006D5 RID: 1749
	private Material bookMat;

	// Token: 0x040006D6 RID: 1750
	[NonSerialized]
	public GenreTree Genre;

	// Token: 0x040006D7 RID: 1751
	public bool IsAvailable;

	// Token: 0x040006D8 RID: 1752
	private float dissolveAmount;

	// Token: 0x040006D9 RID: 1753
	private MaterialPropertyBlock _propBlock;

	// Token: 0x040006DA RID: 1754
	private float timeSinceDeactivated;
}
