using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000183 RID: 387
public class UIPingable : MonoBehaviour, ISelectHandler, IEventSystemHandler, IPointerClickHandler, IDeselectHandler
{
	// Token: 0x0600104E RID: 4174 RVA: 0x00066800 File Offset: 0x00064A00
	private void Start()
	{
		if (this.PingLocation == null)
		{
			this.PingLocation = base.transform;
		}
		UIPing.Register(this);
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x00066822 File Offset: 0x00064A22
	public void Setup(AbilityTree a)
	{
		if (a == null)
		{
			return;
		}
		this.ID = this.Prefix + a.ID;
		this.ContextData = a.ID;
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x00066851 File Offset: 0x00064A51
	public void Setup(AugmentTree a)
	{
		if (a == null)
		{
			return;
		}
		this.DynamicPing = true;
		this.ID = this.Prefix + a.ID;
		this.ContextData = a.ID;
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x00066887 File Offset: 0x00064A87
	public void SetupAsHiddenEnemyPage()
	{
		this.DynamicPing = false;
		this.PingType = UIPing.UIPingType.StaticMessage;
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x00066897 File Offset: 0x00064A97
	public void Setup(AugmentRootNode a)
	{
		if (a == null)
		{
			return;
		}
		this.ID = this.Prefix + a.guid;
		this.ContextData = a.guid;
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x000668C6 File Offset: 0x00064AC6
	public void Setup(GenreTree t)
	{
		if (t == null)
		{
			return;
		}
		this.ID = this.Prefix + t.ID;
		this.ContextData = t.ID;
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x000668F5 File Offset: 0x00064AF5
	public void SetupAsAttunement(int attunement)
	{
		this.PingType = UIPing.UIPingType.AttunementLevel;
		this.ContextData = attunement.ToString();
		this.ID = this.Prefix + "|" + attunement.ToString();
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x00066928 File Offset: 0x00064B28
	public void SetupAsChapter(int wave)
	{
		this.PingType = UIPing.UIPingType.Chapter;
		this.ContextData = wave.ToString();
		this.ID = this.Prefix + "|Chapter " + wave.ToString();
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0006695B File Offset: 0x00064B5B
	public void SetupAsVignette(int wave)
	{
		this.PingType = UIPing.UIPingType.Vignette;
		this.ContextData = wave.ToString();
		this.ID = this.Prefix + "Vignette_" + wave.ToString();
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0006698E File Offset: 0x00064B8E
	public void SetupAsRaidEncounter(int encounter)
	{
		this.PingType = UIPing.UIPingType.RaidEncounter;
		this.ContextData = encounter.ToString();
		this.ID = this.Prefix + "|Encounter " + encounter.ToString();
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x000669C5 File Offset: 0x00064BC5
	public void SetNumberValue(int value)
	{
		this.ContextData = value.ToString();
		this.ID = this.Prefix + "_" + value.ToString();
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x000669F4 File Offset: 0x00064BF4
	public void SetupAsBoss(EnemyType e, int wave = 0)
	{
		UIPing.UIPingType pingType;
		if (e != EnemyType.Splice)
		{
			if (e != EnemyType.Tangent)
			{
				if (e == EnemyType.Raving)
				{
					pingType = UIPing.UIPingType.Boss_Raving;
				}
				else
				{
					pingType = UIPing.UIPingType.Boss;
				}
			}
			else
			{
				pingType = UIPing.UIPingType.Boss_Tangent;
			}
		}
		else
		{
			pingType = UIPing.UIPingType.Boss_Splice;
		}
		this.PingType = pingType;
		this.ID = this.Prefix + "_Boss_" + wave.ToString();
		this.ContextData = "";
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00066A5A File Offset: 0x00064C5A
	public void TryPing()
	{
		UIPing instance = UIPing.instance;
		if (instance == null)
		{
			return;
		}
		instance.SendPing(this);
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x00066A6C File Offset: 0x00064C6C
	public Vector3 GetPingLocation()
	{
		if (this.PingType == UIPing.UIPingType.TomeWorldObj)
		{
			return Tooltip.GetLocationFromWorld(this.PingLocation.position);
		}
		return this.PingLocation.position;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x00066A93 File Offset: 0x00064C93
	public void OnSelect(BaseEventData ev)
	{
		UIPing.CurSelectedPingable = this;
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x00066A9B File Offset: 0x00064C9B
	public void OnDeselect(BaseEventData ev)
	{
		if (UIPing.CurSelectedPingable == this)
		{
			UIPing.CurSelectedPingable = null;
		}
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x00066AB0 File Offset: 0x00064CB0
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		if (pointerEventData.button == PointerEventData.InputButton.Middle)
		{
			this.TryPing();
		}
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x00066AC1 File Offset: 0x00064CC1
	private void OnDestroy()
	{
		UIPing.Unregister(this);
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x00066AC9 File Offset: 0x00064CC9
	public UIPingable()
	{
	}

	// Token: 0x04000E82 RID: 3714
	public bool DynamicPing;

	// Token: 0x04000E83 RID: 3715
	public string ID;

	// Token: 0x04000E84 RID: 3716
	public string Prefix;

	// Token: 0x04000E85 RID: 3717
	public List<string> MessageOptions;

	// Token: 0x04000E86 RID: 3718
	[NonSerialized]
	public string ContextData;

	// Token: 0x04000E87 RID: 3719
	public UIPing.UIPingType PingType;

	// Token: 0x04000E88 RID: 3720
	public Transform PingLocation;
}
