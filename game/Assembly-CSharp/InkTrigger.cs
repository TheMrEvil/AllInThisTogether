using System;
using Unity.Collections;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class InkTrigger : MonoBehaviour
{
	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600086B RID: 2155 RVA: 0x0003A613 File Offset: 0x00038813
	public AugmentRootNode Augment
	{
		get
		{
			InkTalent storeOption = this.StoreOption;
			return (storeOption != null) ? storeOption.Augment : null;
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0003A62C File Offset: 0x0003882C
	private void Awake()
	{
		this.IsAvailable = false;
		this.mr = base.GetComponentInChildren<MeshRenderer>();
		this.dissolveAmount = 1f;
		this._propBlock = new MaterialPropertyBlock();
		this.mr.GetPropertyBlock(this._propBlock, 0);
		this._propBlock.SetFloat(InkTrigger.DissolveAmount, this.dissolveAmount);
		this._propBlock.SetFloat(InkTrigger.Opacity, 1f - this.dissolveAmount);
		this.mr.SetPropertyBlock(this._propBlock, 0);
		this.StoreOption = null;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0003A6C0 File Offset: 0x000388C0
	public void Setup(InkTalent p)
	{
		Debug.Log("Trigger: Updating purchase option");
		if (this.StoreOption != null && p != null && p.Augment == this.StoreOption.Augment)
		{
			this.StoreOption = p;
			return;
		}
		this.StoreOption = p;
		if (p == null)
		{
			this.Deactivate();
			return;
		}
		this.Activate();
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0003A71C File Offset: 0x0003891C
	private void Update()
	{
		this.CheckShouldBeActive();
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
			this._propBlock.SetFloat(InkTrigger.DissolveAmount, this.dissolveAmount);
			this._propBlock.SetFloat(InkTrigger.Opacity, 1f - this.dissolveAmount);
			this.mr.SetPropertyBlock(this._propBlock, i);
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0003A81A File Offset: 0x00038A1A
	private void CheckShouldBeActive()
	{
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0003A81C File Offset: 0x00038A1C
	public void Activate()
	{
		this.IsAvailable = true;
		this.VFX.Play();
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0003A830 File Offset: 0x00038A30
	public void Deactivate()
	{
		this.IsAvailable = false;
		if (this.VFX != null)
		{
			this.VFX.Stop();
		}
		this.timeSinceDeactivated = Time.realtimeSinceStartup;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0003A85D File Offset: 0x00038A5D
	private void OnEnable()
	{
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0003A85F File Offset: 0x00038A5F
	private void OnDisable()
	{
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0003A861 File Offset: 0x00038A61
	public InkTrigger()
	{
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0003A869 File Offset: 0x00038A69
	// Note: this type is marked as 'beforefieldinit'.
	static InkTrigger()
	{
	}

	// Token: 0x04000715 RID: 1813
	[ReadOnly]
	public bool IsAvailable;

	// Token: 0x04000716 RID: 1814
	public Transform UIRoot;

	// Token: 0x04000717 RID: 1815
	[NonSerialized]
	public InkTalent StoreOption;

	// Token: 0x04000718 RID: 1816
	[Header("Display FX")]
	public MeshRenderer mr;

	// Token: 0x04000719 RID: 1817
	private float dissolveAmount;

	// Token: 0x0400071A RID: 1818
	private MaterialPropertyBlock _propBlock;

	// Token: 0x0400071B RID: 1819
	private float timeSinceDeactivated;

	// Token: 0x0400071C RID: 1820
	private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");

	// Token: 0x0400071D RID: 1821
	private static readonly int Opacity = Shader.PropertyToID("_Opacity");

	// Token: 0x0400071E RID: 1822
	public ParticleSystem VFX;

	// Token: 0x0400071F RID: 1823
	public ParticleSystem VotePulse;
}
