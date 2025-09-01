using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class BossRewardTrigger : DiageticOption
{
	// Token: 0x06000864 RID: 2148 RVA: 0x0003A378 File Offset: 0x00038578
	private void Start()
	{
		this.Activate();
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0003A380 File Offset: 0x00038580
	public override void Activate()
	{
		base.Activate();
		this.LoopSFX.Play();
		this.BaseFX.Play();
		if (this.BaseObject != null)
		{
			this.BaseObject.SetActive(true);
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0003A3B8 File Offset: 0x000385B8
	public override void Deactivate()
	{
		if (!this.IsAvailable)
		{
			return;
		}
		this.LoopSFX.Stop();
		if (this.BaseObject != null)
		{
			this.BaseObject.SetActive(false);
		}
		base.Deactivate();
		EntityIndicator entityIndicator = this.indicator;
		if (entityIndicator != null)
		{
			entityIndicator.Deactivate();
		}
		if (this.BaseFX != null)
		{
			this.BaseFX.Stop();
		}
		if (this.InteractFX != null)
		{
			this.InteractFX.Play();
		}
		AudioManager.PlayClipAtPoint(this.InteractSFX.GetRandomClip(-1), base.transform.position, 1f, 1f, 1f, 10f, 250f);
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0003A472 File Offset: 0x00038672
	private void DebugSetupNook(string ID)
	{
		this.NookReward = NookDB.GetItem(ID);
		this.TrySetup();
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0003A488 File Offset: 0x00038688
	public void TrySetup()
	{
		if (this.CurType == Progression.BossRewardType.NookItem && this.NookReward != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.NookReward.Prefab, this.ObjectHolder);
			gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			Physics.SyncTransforms();
			NookItem component = gameObject.GetComponent<NookItem>();
			float size = component.Size;
			gameObject.transform.localScale = Vector3.one * 1f / size;
			if (component.OverlapCollider != null)
			{
				Collider overlapCollider = component.OverlapCollider;
				Vector3 a = this.ObjectHolder.InverseTransformPoint(overlapCollider.bounds.center);
				gameObject.transform.localPosition -= a * 1f / size;
			}
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0003A564 File Offset: 0x00038764
	public override void Select()
	{
		this.Deactivate();
		if (this.CosmeticReward != null)
		{
			GoalManager.instance.OpenReward(base.transform.position, this.CosmeticReward, this.OverrideDetails);
		}
		else if (this.NookReward != null)
		{
			GoalManager.instance.OpenReward(base.transform.position, this.NookReward, this.OverrideDetails);
		}
		else
		{
			GoalManager.instance.OpenReward(base.transform.position, this.CurType, this.Rewards, this.Currency, this.OverrideDetails);
		}
		base.Select();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0003A600 File Offset: 0x00038800
	public BossRewardTrigger()
	{
	}

	// Token: 0x04000708 RID: 1800
	public Progression.BossRewardType CurType;

	// Token: 0x04000709 RID: 1801
	[NonSerialized]
	public List<GraphTree> Rewards = new List<GraphTree>();

	// Token: 0x0400070A RID: 1802
	[NonSerialized]
	public Cosmetic CosmeticReward;

	// Token: 0x0400070B RID: 1803
	[NonSerialized]
	public NookDB.NookObject NookReward;

	// Token: 0x0400070C RID: 1804
	[NonSerialized]
	public int Currency;

	// Token: 0x0400070D RID: 1805
	[Header("Display FX")]
	public ParticleSystem BaseFX;

	// Token: 0x0400070E RID: 1806
	public GameObject BaseObject;

	// Token: 0x0400070F RID: 1807
	public AudioFader LoopSFX;

	// Token: 0x04000710 RID: 1808
	public ParticleSystem InteractFX;

	// Token: 0x04000711 RID: 1809
	public List<AudioClip> InteractSFX;

	// Token: 0x04000712 RID: 1810
	public EntityIndicator indicator;

	// Token: 0x04000713 RID: 1811
	public Transform ObjectHolder;

	// Token: 0x04000714 RID: 1812
	[NonSerialized]
	public string OverrideDetails;
}
