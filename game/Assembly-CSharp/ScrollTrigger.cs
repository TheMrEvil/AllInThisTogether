using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class ScrollTrigger : MonoBehaviour
{
	// Token: 0x06000970 RID: 2416 RVA: 0x0003F500 File Offset: 0x0003D700
	private void Awake()
	{
		this._propBlock = new MaterialPropertyBlock();
		this.dissolveAmount = 1f;
		foreach (MeshRenderer meshRenderer in this.Renderers)
		{
			for (int j = 0; j < meshRenderer.sharedMaterials.Length; j++)
			{
				meshRenderer.GetPropertyBlock(this._propBlock, j);
				this._propBlock.SetFloat("_DissolveAmount", 1f);
				this._propBlock.SetFloat("_Opacity", 0f);
				meshRenderer.SetPropertyBlock(this._propBlock, j);
			}
		}
		this.IsAvailable = false;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0003F59A File Offset: 0x0003D79A
	public void Setup(AugmentTree mod, bool isPlayer)
	{
		this.curMod = mod;
		this.IsPlayer = isPlayer;
		this.SetupVisuals();
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0003F5B0 File Offset: 0x0003D7B0
	public void Activate()
	{
		ScrollTrigger.ParticleView view = this.GetView();
		if (view != null)
		{
			view.PlayPassive();
		}
		this.dissolveAmount = 1f;
		this.IsAvailable = true;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0003F5D8 File Offset: 0x0003D7D8
	private void Update()
	{
		if (this.dissolveAmount > 0f && this.IsAvailable)
		{
			this.dissolveAmount = Mathf.Clamp(this.dissolveAmount - Time.deltaTime * 4f, 0f, 1f);
		}
		else
		{
			if (this.dissolveAmount >= 1f || this.IsAvailable)
			{
				return;
			}
			this.dissolveAmount = Mathf.Clamp(this.dissolveAmount + Time.deltaTime * 4f, 0f, 1f);
		}
		foreach (MeshRenderer meshRenderer in this.Renderers)
		{
			for (int j = 0; j < meshRenderer.sharedMaterials.Length; j++)
			{
				meshRenderer.GetPropertyBlock(this._propBlock, j);
				this._propBlock.SetFloat("_DissolveAmount", this.dissolveAmount);
				this._propBlock.SetFloat("_Opacity", 1f - this.dissolveAmount);
				meshRenderer.SetPropertyBlock(this._propBlock, j);
			}
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0003F6DA File Offset: 0x0003D8DA
	public void ConfirmAndDeactivate()
	{
		this.Deactivate();
		ScrollTrigger.ParticleView view = this.GetView();
		if (view == null)
		{
			return;
		}
		view.PlayPulse();
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0003F6F2 File Offset: 0x0003D8F2
	private void SetupVisuals()
	{
		if (!this.IsPlayer)
		{
			this.SetRimColor(Color.red);
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0003F708 File Offset: 0x0003D908
	private ScrollTrigger.ParticleView GetView()
	{
		if (this.curMod == null)
		{
			return null;
		}
		switch (this.curMod.Root.DisplayQuality)
		{
		case AugmentQuality.Basic:
			return this.CommonView;
		case AugmentQuality.Normal:
			return this.RareView;
		case AugmentQuality.Epic:
			return this.EpicView;
		case AugmentQuality.Legendary:
			return this.LegendaryView;
		case AugmentQuality.Artifact:
			return this.ArtifactView;
		default:
			return this.CommonView;
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0003F77C File Offset: 0x0003D97C
	private void SetRimColor(Color c)
	{
		Material[] materials = this.Renderers[0].materials;
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].SetColor("_RimColor", c);
		}
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0003F7B3 File Offset: 0x0003D9B3
	public void Deactivate()
	{
		ScrollTrigger.ParticleView view = this.GetView();
		if (view != null)
		{
			view.StopPassive();
		}
		this.IsAvailable = false;
		this.dissolveAmount = 0f;
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0003F7D8 File Offset: 0x0003D9D8
	public ScrollTrigger()
	{
	}

	// Token: 0x040007C9 RID: 1993
	private bool IsAvailable;

	// Token: 0x040007CA RID: 1994
	public MeshRenderer[] Renderers;

	// Token: 0x040007CB RID: 1995
	public ScrollTrigger.ParticleView CommonView;

	// Token: 0x040007CC RID: 1996
	public ScrollTrigger.ParticleView RareView;

	// Token: 0x040007CD RID: 1997
	public ScrollTrigger.ParticleView EpicView;

	// Token: 0x040007CE RID: 1998
	public ScrollTrigger.ParticleView LegendaryView;

	// Token: 0x040007CF RID: 1999
	public ScrollTrigger.ParticleView ArtifactView;

	// Token: 0x040007D0 RID: 2000
	private float dissolveAmount;

	// Token: 0x040007D1 RID: 2001
	private MaterialPropertyBlock _propBlock;

	// Token: 0x040007D2 RID: 2002
	[NonSerialized]
	public AugmentTree curMod;

	// Token: 0x040007D3 RID: 2003
	[NonSerialized]
	public bool IsPlayer;

	// Token: 0x040007D4 RID: 2004
	private float timeSinceDeactivated;

	// Token: 0x020004C4 RID: 1220
	[Serializable]
	public class ParticleView
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x000C7799 File Offset: 0x000C5999
		public void PlayPulse()
		{
			this.ChosenPulse.Play();
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000C77A6 File Offset: 0x000C59A6
		public void PlayPassive()
		{
			if (this.Passive != null && !this.Passive.isPlaying)
			{
				this.Passive.Play();
			}
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000C77CE File Offset: 0x000C59CE
		public void StopPassive()
		{
			if (this.Passive != null && !this.Passive.isStopped)
			{
				this.Passive.Stop();
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000C77F6 File Offset: 0x000C59F6
		public ParticleView()
		{
		}

		// Token: 0x04002449 RID: 9289
		public ParticleSystem Passive;

		// Token: 0x0400244A RID: 9290
		public ParticleSystem ChosenPulse;
	}
}
