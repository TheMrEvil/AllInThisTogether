using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class DangerIndicatorManager : MonoBehaviour
{
	// Token: 0x06000764 RID: 1892 RVA: 0x00035400 File Offset: 0x00033600
	private void Awake()
	{
		this.Control = base.GetComponentInParent<PlayerControl>();
		if (!this.Control.IsMine)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00035422 File Offset: 0x00033622
	private void Update()
	{
		this.TickUpdates();
		if (Settings.GetBool(SystemSetting.DangerIndicators, true))
		{
			this.AddNewIndicators();
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0003543C File Offset: 0x0003363C
	private void TickUpdates()
	{
		for (int i = this.indicators.Count - 1; i >= 0; i--)
		{
			if (this.indicators[i] == null)
			{
				this.indicators.RemoveAt(i);
			}
			else
			{
				this.indicators[i].TickUpdate();
			}
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00035494 File Offset: 0x00033694
	private void AddNewIndicators()
	{
		foreach (AreaOfEffect areaOfEffect in AreaOfEffect.AllAreas)
		{
			if (areaOfEffect.IsNegative && !areaOfEffect.isFinished && (areaOfEffect.IsEnemyEffect || areaOfEffect.ForceDangerIndicator) && areaOfEffect.Danger != DangerIndicator.DangerLevel.None && !this.HasIndicator(areaOfEffect))
			{
				float num = DangerIndicatorManager.AoEDangerDistance(areaOfEffect);
				if (Vector3.Distance(areaOfEffect.transform.position, base.transform.position) <= num)
				{
					this.CreateIndicator().Setup(areaOfEffect);
				}
			}
		}
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.IsDead && entityControl.TeamID == 2 && !this.HasIndicator(entityControl))
			{
				Ability currentActiveAbility = entityControl.GetCurrentActiveAbility();
				if (currentActiveAbility != null && currentActiveAbility.props.Danger != DangerIndicator.DangerLevel.None && (!currentActiveAbility.props.Targeted || !(entityControl.currentTarget != PlayerControl.myInstance)))
				{
					this.CreateIndicator().Setup(entityControl, currentActiveAbility);
				}
			}
		}
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x000355E4 File Offset: 0x000337E4
	public static float AoEDangerDistance(AreaOfEffect e)
	{
		float num = e.Radius * e.CurrentScale;
		float num2;
		switch (e.Danger)
		{
		case DangerIndicator.DangerLevel.Low:
			num2 = 1.5f;
			break;
		case DangerIndicator.DangerLevel.Medium:
			num2 = 2f;
			break;
		case DangerIndicator.DangerLevel.High:
			num2 = 2.5f;
			break;
		default:
			num2 = 1f;
			break;
		}
		float num3 = num2;
		return 5f + num * num3;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00035648 File Offset: 0x00033848
	private DangerIndicator CreateIndicator()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.IndicatorRef, this.IndicatorRef.transform.parent);
		gameObject.SetActive(true);
		DangerIndicator component = gameObject.GetComponent<DangerIndicator>();
		this.indicators.Add(component);
		return component;
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0003568C File Offset: 0x0003388C
	private bool HasIndicator(ActionEffect eft)
	{
		using (List<DangerIndicator>.Enumerator enumerator = this.indicators.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.action == eft)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x000356EC File Offset: 0x000338EC
	private bool HasIndicator(EntityControl ent)
	{
		using (List<DangerIndicator>.Enumerator enumerator = this.indicators.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.entity == ent)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0003574C File Offset: 0x0003394C
	public DangerIndicatorManager()
	{
	}

	// Token: 0x04000607 RID: 1543
	public GameObject IndicatorRef;

	// Token: 0x04000608 RID: 1544
	private PlayerControl Control;

	// Token: 0x04000609 RID: 1545
	private List<DangerIndicator> indicators = new List<DangerIndicator>();
}
