using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class EnemyUIDisplayManager : MonoBehaviour
{
	// Token: 0x060011A1 RID: 4513 RVA: 0x0006D4D5 File Offset: 0x0006B6D5
	private void Awake()
	{
		EnemyUIDisplayManager.instance = this;
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x0006D4E0 File Offset: 0x0006B6E0
	private void Update()
	{
		if (EntityControl.AllEntities == null)
		{
			return;
		}
		bool flag = Settings.GetInt(SystemSetting.EnemyHealthbar, 0) != 2;
		flag &= (PlayerCamera.myInstance != null && !PlayerControl.myInstance.IsSpectator);
		flag &= (GameHUD.Mode != GameHUD.HUDMode.Off);
		flag &= (PanelManager.CurPanel == PanelType.GameInvisible);
		flag &= !SignatureInkUIControl.IsInPrestige;
		flag &= (!RaidManager.IsInRaid || RaidManager.IsEncounterStarted);
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (flag && !(entityControl is PlayerControl) && !entityControl.IsDead && (!(entityControl is AIControl) || (entityControl.display as AIDisplay).ShowHealthbar))
			{
				AIControl aicontrol = entityControl as AIControl;
				if ((aicontrol.Level.HasFlag(EnemyLevel.Elite) && aicontrol.Targetable) || aicontrol.Display.HealthbarAlwaysOn)
				{
					this.AssignDisplay(entityControl, 1f);
				}
				else if (entityControl.health.TimeSinceLastDamage < 0.5f)
				{
					this.AssignDisplay(entityControl, 1f);
				}
				else if (PlayerControl.myInstance != null && entityControl == PlayerControl.myInstance.RealTarget)
				{
					this.AssignDisplay(entityControl, 0.33f);
				}
			}
		}
		foreach (EnemyUIDisplay enemyUIDisplay in this.Displays)
		{
			if (!enemyUIDisplay.IsFree || enemyUIDisplay.Opacity > 0f)
			{
				enemyUIDisplay.TickUpdate();
			}
		}
		foreach (EnemyUIDisplay enemyUIDisplay2 in this.EliteDisplays)
		{
			if (!enemyUIDisplay2.IsFree || enemyUIDisplay2.Opacity > 0f)
			{
				enemyUIDisplay2.TickUpdate();
			}
		}
		foreach (EnemyUIDisplay enemyUIDisplay3 in this.AllyDisplays)
		{
			if (!enemyUIDisplay3.IsFree || enemyUIDisplay3.Opacity > 0f)
			{
				enemyUIDisplay3.TickUpdate();
			}
		}
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x0006D778 File Offset: 0x0006B978
	private EnemyUIDisplay AssignDisplay(EntityControl control, float focusDuration)
	{
		EnemyUIDisplay currentDisplay = this.GetCurrentDisplay(control);
		if (currentDisplay != null)
		{
			currentDisplay.FocusVal = focusDuration;
			return currentDisplay;
		}
		AIControl aicontrol = control as AIControl;
		bool flag = aicontrol.Level.HasFlag(EnemyLevel.Elite) || aicontrol.Level.HasFlag(EnemyLevel.Boss);
		List<EnemyUIDisplay> list = this.Displays;
		if (control.TeamID == 1 || aicontrol.Display.ShowAsAlly)
		{
			list = this.AllyDisplays;
		}
		else if (flag)
		{
			list = this.EliteDisplays;
		}
		foreach (EnemyUIDisplay enemyUIDisplay in list)
		{
			if (enemyUIDisplay.IsFree)
			{
				enemyUIDisplay.Setup(control, focusDuration);
				return enemyUIDisplay;
			}
		}
		return this.AddDisplay(control, focusDuration);
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x0006D868 File Offset: 0x0006BA68
	private EnemyUIDisplay GetCurrentDisplay(EntityControl control)
	{
		foreach (EnemyUIDisplay enemyUIDisplay in this.Displays)
		{
			if (enemyUIDisplay.followingControl == control)
			{
				return enemyUIDisplay;
			}
		}
		foreach (EnemyUIDisplay enemyUIDisplay2 in this.EliteDisplays)
		{
			if (enemyUIDisplay2.followingControl == control)
			{
				return enemyUIDisplay2;
			}
		}
		foreach (EnemyUIDisplay enemyUIDisplay3 in this.AllyDisplays)
		{
			if (enemyUIDisplay3.followingControl == control)
			{
				return enemyUIDisplay3;
			}
		}
		return null;
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0006D96C File Offset: 0x0006BB6C
	private EnemyUIDisplay AddDisplay(EntityControl control, float focusDuration)
	{
		AIControl aicontrol = control as AIControl;
		if (aicontrol == null)
		{
			return null;
		}
		bool flag = aicontrol.Level.HasFlag(EnemyLevel.Elite);
		GameObject gameObject = this.DisplayRef;
		if (flag)
		{
			gameObject = this.EliteDisplayRef;
		}
		if (control.TeamID == 1 || aicontrol.Display.ShowAsAlly)
		{
			gameObject = this.AllyDisplayRef;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, gameObject.transform.parent);
		gameObject2.SetActive(true);
		EnemyUIDisplay component = gameObject2.GetComponent<EnemyUIDisplay>();
		component.Setup(control, focusDuration);
		if (control.TeamID == 1 || aicontrol.Display.ShowAsAlly)
		{
			this.AllyDisplays.Add(component);
		}
		else if (flag)
		{
			this.EliteDisplays.Add(component);
		}
		else
		{
			this.Displays.Add(component);
		}
		return component;
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x0006DA34 File Offset: 0x0006BC34
	public void TryReleaseDisplay(EntityControl control)
	{
		EnemyUIDisplay currentDisplay = this.GetCurrentDisplay(control);
		if (currentDisplay != null)
		{
			return;
		}
		currentDisplay.FocusVal = 0f;
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0006DA5E File Offset: 0x0006BC5E
	public EnemyUIDisplayManager()
	{
	}

	// Token: 0x04001049 RID: 4169
	public static EnemyUIDisplayManager instance;

	// Token: 0x0400104A RID: 4170
	public GameObject DisplayRef;

	// Token: 0x0400104B RID: 4171
	public GameObject EliteDisplayRef;

	// Token: 0x0400104C RID: 4172
	public GameObject AllyDisplayRef;

	// Token: 0x0400104D RID: 4173
	private List<EnemyUIDisplay> Displays = new List<EnemyUIDisplay>();

	// Token: 0x0400104E RID: 4174
	private List<EnemyUIDisplay> EliteDisplays = new List<EnemyUIDisplay>();

	// Token: 0x0400104F RID: 4175
	private List<EnemyUIDisplay> AllyDisplays = new List<EnemyUIDisplay>();

	// Token: 0x04001050 RID: 4176
	public LayerMask LOSMask;
}
