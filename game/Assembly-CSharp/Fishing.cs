using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class Fishing : MonoBehaviour
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00038085 File Offset: 0x00036285
	public static bool CanStartFishing
	{
		get
		{
			return !Fishing.isFishing && Fishing.instance.canFish;
		}
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0003809A File Offset: 0x0003629A
	private void Awake()
	{
		Fishing.instance = this;
		Fishing.isFishing = false;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x000380A8 File Offset: 0x000362A8
	public void StartFishing()
	{
		PlayerControl.myInstance.AddStatusEffect(this.Status.HashCode, PlayerControl.MyViewID, 0f, 1, false, 0);
		Vector3 position = this.FishingTargets[UnityEngine.Random.Range(0, this.FishingTargets.Count)].position;
		StateManager.instance.StartedFishing(position);
		Fishing.isFishing = true;
		this.fishingTime = 0f;
		this.localFishPoint = position;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00038120 File Offset: 0x00036320
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			Fishing.isFishing = false;
			return;
		}
		if (!Fishing.isFishing)
		{
			this.TryOfferFishing();
			return;
		}
		this.fishingTime += Time.deltaTime;
		if (InputManager.Actions.Interact.WasPressed && this.fishingTime > 1f)
		{
			this.PullIn();
		}
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00038188 File Offset: 0x00036388
	private void TryOfferFishing()
	{
		this.canFish = false;
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0003819C File Offset: 0x0003639C
	private void PullIn()
	{
		this.StopFishing();
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			this.OpenFishingReward();
		}, 0.65f);
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x000381BF File Offset: 0x000363BF
	public void StopFishing()
	{
		PlayerControl.myInstance.RemoveStatusEffect(this.Status.ID.GetHashCode(), PlayerControl.MyViewID, 0, false, true);
		StateManager.instance.StopFishing();
		Fishing.isFishing = false;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x000381F3 File Offset: 0x000363F3
	private void OpenFishingReward()
	{
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x000381F8 File Offset: 0x000363F8
	public void ApplyFishing(PlayerControl player, Vector3 landPos)
	{
		Debug.Log("Applying fishing to " + ((player != null) ? player.ToString() : null));
		if (player == null || this.Rods.ContainsKey(player))
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FishingRodRef);
		gameObject.SetActive(true);
		FishingRod component = gameObject.GetComponent<FishingRod>();
		component.Setup(player, landPos);
		this.Rods.Add(player, component);
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00038268 File Offset: 0x00036468
	public void RemoveFishing(PlayerControl player)
	{
		Debug.Log("Removing Fishing from " + ((player != null) ? player.ToString() : null));
		if (player == null || !this.Rods.ContainsKey(player))
		{
			return;
		}
		this.Rods[player].Finish();
		this.Rods.Remove(player);
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x000382C7 File Offset: 0x000364C7
	public Fishing()
	{
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x000382E5 File Offset: 0x000364E5
	[CompilerGenerated]
	private void <PullIn>b__17_0()
	{
		this.OpenFishingReward();
	}

	// Token: 0x04000699 RID: 1689
	public static Fishing instance;

	// Token: 0x0400069A RID: 1690
	public GameObject FishingRodRef;

	// Token: 0x0400069B RID: 1691
	public StatusTree Status;

	// Token: 0x0400069C RID: 1692
	public List<Transform> FishingTargets;

	// Token: 0x0400069D RID: 1693
	public Transform AvailabilityPoint;

	// Token: 0x0400069E RID: 1694
	public float MaxDistance = 5f;

	// Token: 0x0400069F RID: 1695
	public static bool isFishing;

	// Token: 0x040006A0 RID: 1696
	private bool canFish;

	// Token: 0x040006A1 RID: 1697
	private Dictionary<PlayerControl, FishingRod> Rods = new Dictionary<PlayerControl, FishingRod>();

	// Token: 0x040006A2 RID: 1698
	private Vector3 localFishPoint;

	// Token: 0x040006A3 RID: 1699
	private float fishingTime;
}
