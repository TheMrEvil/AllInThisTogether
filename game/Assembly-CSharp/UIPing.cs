using System;
using System.Collections.Generic;
using FIMSpace;
using Photon.Pun;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class UIPing : MonoBehaviour
{
	// Token: 0x060015D3 RID: 5587 RVA: 0x00089ACC File Offset: 0x00087CCC
	private void Awake()
	{
		UIPing.instance = this;
		this.FeedItemRef.SetActive(false);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FeedItemRef, this.FeedItemRef.transform.parent);
		gameObject.SetActive(true);
		UIPingFeedItem component = gameObject.GetComponent<UIPingFeedItem>();
		component.Setup(null);
		component.Fader.alpha = 0f;
		this.WorldEntry = component;
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x00089B34 File Offset: 0x00087D34
	private void Update()
	{
		bool flag = (PhotonNetwork.InRoom && !PhotonNetwork.OfflineMode) || RaidManager.IsInRaid;
		this.Fader.UpdateOpacity(flag, 4f, false);
		if (!flag)
		{
			return;
		}
		if (this.pcd > 0f)
		{
			this.pcd -= Time.deltaTime;
		}
		if (UIPing.CurSelectedPingable != null && InputManager.Actions.Ping.WasPressed)
		{
			UIPing.CurSelectedPingable.TryPing();
		}
		foreach (UIPingFeedItem uipingFeedItem in this.FeedEntries)
		{
			uipingFeedItem.TickUpdate();
		}
		this.WorldEntry.TickUpdate();
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x00089C04 File Offset: 0x00087E04
	public void SendPing(UIPingable p)
	{
		if (this.pcd > 0f || PanelManager.CurPanel == PanelType.GameInvisible)
		{
			return;
		}
		this.pcd = this.PingCooldown;
		string context = p.ContextData;
		if (!p.DynamicPing && p.MessageOptions.Count > 0)
		{
			context = p.MessageOptions[UnityEngine.Random.Range(0, p.MessageOptions.Count)];
		}
		PlayerControl.myInstance.Net.SendUIPing(p.ID, p.PingType, context);
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x00089C8C File Offset: 0x00087E8C
	public void GotPing(PlayerControl plr, string pingID, UIPing.UIPingType pingType, string context)
	{
		UIPingable uipingable = null;
		foreach (UIPingable uipingable2 in this.AllPings)
		{
			if (!(uipingable2.ID != pingID))
			{
				uipingable = uipingable2;
				break;
			}
		}
		if (uipingable != null && this.IsUIItemVisible(uipingable.gameObject))
		{
			this.PingDisplay.position = uipingable.GetPingLocation();
			this.PingEffect.Play();
			AudioManager.PlaySFX2D(this.PingEyeSFX, 1f, 0.1f);
		}
		UIPingFeedItem feedItem = this.GetFeedItem(plr);
		if (feedItem != null)
		{
			feedItem.ShowDisplay(this.GetFeedText(plr, pingID, pingType, context), 2f);
			AudioManager.PlayInterfaceSFX(this.FeedSFX, 1f, 0f);
		}
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x00089D70 File Offset: 0x00087F70
	public static void WorldEventPing(string info, float duration = 2f)
	{
		UIPing.instance.WorldEntry.ShowDisplay(info, duration);
		AudioManager.PlayInterfaceSFX(UIPing.instance.WorldPingSFX, 1f, 0f);
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x00089D9C File Offset: 0x00087F9C
	public UIPingFeedItem GetFeedItem(PlayerControl plr)
	{
		for (int i = this.FeedEntries.Count - 1; i >= 0; i--)
		{
			if (this.FeedEntries[i].plrRef == null)
			{
				UnityEngine.Object.Destroy(this.FeedEntries[i].gameObject);
				this.FeedEntries.RemoveAt(i);
			}
		}
		foreach (UIPingFeedItem uipingFeedItem in this.FeedEntries)
		{
			if (uipingFeedItem.plrRef == plr)
			{
				return uipingFeedItem;
			}
		}
		return this.CreateFeedEntry(plr);
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x00089E58 File Offset: 0x00088058
	private UIPingFeedItem CreateFeedEntry(PlayerControl plr)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FeedItemRef, this.FeedItemRef.transform.parent);
		gameObject.SetActive(true);
		UIPingFeedItem component = gameObject.GetComponent<UIPingFeedItem>();
		component.Setup(plr);
		this.FeedEntries.Add(component);
		return component;
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x00089EA4 File Offset: 0x000880A4
	private string GetFeedText(PlayerControl plr, string pid, UIPing.UIPingType pingType, string graphID)
	{
		string str = plr.Username;
		if (this.IsExplicitMessage(pingType))
		{
			str += " ";
		}
		else
		{
			str = str + "<color=#C0C0C0> " + this.Verbs[UnityEngine.Random.Range(0, this.Verbs.Count)] + " </color>";
		}
		return str + this.GetUIPingText(pid, pingType, graphID);
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x00089F10 File Offset: 0x00088110
	private bool IsUIItemVisible(GameObject o)
	{
		CanvasGroup[] componentsInParent = o.GetComponentsInParent<CanvasGroup>();
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			if (componentsInParent[i].alpha == 0f)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x00089F44 File Offset: 0x00088144
	public static void Register(UIPingable p)
	{
		if (UIPing.instance.AllPings.Contains(p))
		{
			return;
		}
		UIPing.instance.AllPings.Add(p);
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00089F69 File Offset: 0x00088169
	public static void Unregister(UIPingable p)
	{
		UIPing.instance.AllPings.Remove(p);
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x00089F7C File Offset: 0x0008817C
	private string GetUIPingText(string pid, UIPing.UIPingType pingType, string context)
	{
		if (this.IsAugmentPing(pingType))
		{
			AugmentTree augment = GraphDB.GetAugment(context);
			if (augment == null)
			{
				return "INVALID AUGMENT";
			}
			string text = "pos";
			if (pingType == UIPing.UIPingType.Augment_Binding || pingType == UIPing.UIPingType.Augment_Enemy)
			{
				text = "neg";
			}
			if (pingType == UIPing.UIPingType.Augment_Player || pingType == UIPing.UIPingType.Augment_Font)
			{
				Color playerColor = GameDB.Quality(augment.Root.DisplayQuality).PlayerColor;
				return "<font=\"Aleg_Numbers\"><color=" + playerColor.ColorToHex(true) + "><b>" + "[" + augment.Root.Name + "]</b></color></font>";
			}
			return string.Concat(new string[]
			{
				"<style=",
				text,
				">[",
				augment.Root.Name,
				"]</style>"
			});
		}
		else
		{
			if (pingType == UIPing.UIPingType.Loadout_Ability)
			{
				AbilityTree ability = GraphDB.GetAbility(context);
				return "[<b>" + ((ability != null) ? ability.Root.Name : null) + "</b>]";
			}
			if (pingType == UIPing.UIPingType.Tome || pingType == UIPing.UIPingType.TomeWorldObj)
			{
				GenreTree genre = GraphDB.GetGenre(context);
				return "[<b>" + ((genre != null) ? genre.Root.ShortName : null) + "</b>]";
			}
			if (pingType == UIPing.UIPingType.Boss)
			{
				return "a Torn Boss";
			}
			if (pingType == UIPing.UIPingType.Boss_Splice)
			{
				return "a <color=\"white\"><font=\"GPro-Torn\">" + GameDB.GetEnemyInfo(EnemyType.Splice).Inline + "</b></font> Boss</color>";
			}
			if (pingType == UIPing.UIPingType.Boss_Tangent)
			{
				return "a <color=\"white\"><font=\"GPro-Torn\">" + GameDB.GetEnemyInfo(EnemyType.Tangent).Inline + "</b></font> Boss</color>";
			}
			if (pingType == UIPing.UIPingType.Boss_Raving)
			{
				return "a <color=\"white\"><font=\"GPro-Torn\">" + GameDB.GetEnemyInfo(EnemyType.Raving).Inline + "</b></font> Boss</color>";
			}
			if (pingType == UIPing.UIPingType.Chapter)
			{
				string[] array = pid.Split('|', StringSplitOptions.None);
				return array[array.Length - 1];
			}
			if (pingType == UIPing.UIPingType.Vignette)
			{
				return "a Vignette";
			}
			if (pingType == UIPing.UIPingType.RaidEncounter)
			{
				return "an Encounter";
			}
			if (pingType == UIPing.UIPingType.TomePower)
			{
				return "the <b>Tome Power</b>";
			}
			if (pingType == UIPing.UIPingType.AttunementLevel)
			{
				string str = "<sprite name=\"binding\"> Attunement ";
				string[] array2 = pid.Split('|', StringSplitOptions.None);
				return str + array2[array2.Length - 1];
			}
			if (pingType == UIPing.UIPingType.FontPoints)
			{
				return "<font=\"Aleg_Numbers\">" + context + "</font> <sprite name=\"font_point\">";
			}
			if (pingType == UIPing.UIPingType.PlrRevives)
			{
				return "<font=\"Aleg_Numbers\">" + context + "</font> <sprite name=\"revive\">";
			}
			if (pingType == UIPing.UIPingType.StaticMessage)
			{
				return context;
			}
			if (pingType == UIPing.UIPingType.Vote_Continue || pingType == UIPing.UIPingType.Vote_Appendix || pingType == UIPing.UIPingType.Vote_ReturnLibrary)
			{
				return context;
			}
			return "Undefined";
		}
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x0008A1C4 File Offset: 0x000883C4
	private bool IsAugmentPing(UIPing.UIPingType p)
	{
		bool result;
		switch (p)
		{
		case UIPing.UIPingType.Augment_Player:
			result = true;
			break;
		case UIPing.UIPingType.Augment_Enemy:
			result = true;
			break;
		case UIPing.UIPingType.Augment_Binding:
			result = true;
			break;
		case UIPing.UIPingType.Augment_Font:
			result = true;
			break;
		case UIPing.UIPingType.Augment_InkLevel:
			result = true;
			break;
		case UIPing.UIPingType.Augment_Inscription:
			result = true;
			break;
		default:
			result = (p == UIPing.UIPingType.Loadout_Signature);
			break;
		}
		return result;
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x0008A21C File Offset: 0x0008841C
	private bool IsExplicitMessage(UIPing.UIPingType p)
	{
		bool result;
		if (p != UIPing.UIPingType.StaticMessage)
		{
			switch (p)
			{
			case UIPing.UIPingType.Vote_Continue:
				result = true;
				break;
			case UIPing.UIPingType.Vote_Appendix:
				result = true;
				break;
			case UIPing.UIPingType.Vote_ReturnLibrary:
				result = true;
				break;
			case UIPing.UIPingType.Vote_Bindings:
				result = true;
				break;
			default:
				result = false;
				break;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x0008A262 File Offset: 0x00088462
	public UIPing()
	{
	}

	// Token: 0x0400157D RID: 5501
	public static UIPing instance;

	// Token: 0x0400157E RID: 5502
	public CanvasGroup Fader;

	// Token: 0x0400157F RID: 5503
	public Transform PingDisplay;

	// Token: 0x04001580 RID: 5504
	public ParticleSystem PingEffect;

	// Token: 0x04001581 RID: 5505
	public AudioClip PingEyeSFX;

	// Token: 0x04001582 RID: 5506
	public GameObject FeedItemRef;

	// Token: 0x04001583 RID: 5507
	public AudioClip FeedSFX;

	// Token: 0x04001584 RID: 5508
	public AudioClip WorldPingSFX;

	// Token: 0x04001585 RID: 5509
	private UIPingFeedItem WorldEntry;

	// Token: 0x04001586 RID: 5510
	private List<UIPingFeedItem> FeedEntries = new List<UIPingFeedItem>();

	// Token: 0x04001587 RID: 5511
	private List<UIPingable> AllPings = new List<UIPingable>();

	// Token: 0x04001588 RID: 5512
	public static UIPingable CurSelectedPingable;

	// Token: 0x04001589 RID: 5513
	public List<string> Verbs;

	// Token: 0x0400158A RID: 5514
	public float PingCooldown = 1f;

	// Token: 0x0400158B RID: 5515
	private float pcd;

	// Token: 0x020005E4 RID: 1508
	public enum UIPingType
	{
		// Token: 0x040028FD RID: 10493
		Chapter,
		// Token: 0x040028FE RID: 10494
		Vignette,
		// Token: 0x040028FF RID: 10495
		TomePower,
		// Token: 0x04002900 RID: 10496
		AttunementLevel,
		// Token: 0x04002901 RID: 10497
		Tome,
		// Token: 0x04002902 RID: 10498
		TomeWorldObj,
		// Token: 0x04002903 RID: 10499
		FontPoints,
		// Token: 0x04002904 RID: 10500
		PlrRevives,
		// Token: 0x04002905 RID: 10501
		StaticMessage,
		// Token: 0x04002906 RID: 10502
		WorldEvent,
		// Token: 0x04002907 RID: 10503
		Augment_Player = 64,
		// Token: 0x04002908 RID: 10504
		Augment_Enemy,
		// Token: 0x04002909 RID: 10505
		Augment_Binding,
		// Token: 0x0400290A RID: 10506
		Augment_Font,
		// Token: 0x0400290B RID: 10507
		Augment_InkLevel,
		// Token: 0x0400290C RID: 10508
		Augment_Inscription,
		// Token: 0x0400290D RID: 10509
		Loadout_Ability = 128,
		// Token: 0x0400290E RID: 10510
		Loadout_Signature,
		// Token: 0x0400290F RID: 10511
		Boss = 200,
		// Token: 0x04002910 RID: 10512
		Boss_Splice,
		// Token: 0x04002911 RID: 10513
		Boss_Tangent,
		// Token: 0x04002912 RID: 10514
		Boss_Raving,
		// Token: 0x04002913 RID: 10515
		RaidEncounter = 225,
		// Token: 0x04002914 RID: 10516
		Vote_Continue = 256,
		// Token: 0x04002915 RID: 10517
		Vote_Appendix,
		// Token: 0x04002916 RID: 10518
		Vote_ReturnLibrary,
		// Token: 0x04002917 RID: 10519
		Vote_Bindings,
		// Token: 0x04002918 RID: 10520
		Vote_RaidDifficulty
	}
}
