using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C9 RID: 457
public class TeammateUI : MonoBehaviour
{
	// Token: 0x060012AB RID: 4779 RVA: 0x000731CC File Offset: 0x000713CC
	public void Setup(PlayerControl Player)
	{
		this.Player = Player;
		PlayerActions actions = Player.actions;
		actions.coreChanged = (Action<MagicColor>)Delegate.Combine(actions.coreChanged, new Action<MagicColor>(this.CoreChanged));
		this.Username.text = Player.GetUsernameText();
		this.CoreChanged(Player.actions.core.Root.magicColor);
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x00073234 File Offset: 0x00071434
	private void CoreChanged(MagicColor e)
	{
		GameDB.ElementInfo element = GameDB.GetElement(e);
		this.AvatarIcon.sprite = element.Icon;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x0007325C File Offset: 0x0007145C
	private void Update()
	{
		if (this.Player == null)
		{
			return;
		}
		this.DeadDisplay.SetActive(this.Player.IsDead);
		this.unT -= Time.deltaTime;
		if (this.unT <= 0f)
		{
			this.unT = 1f;
			string usernameText = this.Player.GetUsernameText();
			if (this.Username.text != usernameText)
			{
				this.Username.text = usernameText;
			}
		}
		this.HealthFill.fillAmount = Mathf.Lerp(this.HealthFill.fillAmount, this.Player.health.CurrentHealthProportion, Time.deltaTime * 4f);
		if (this.Player.health.MaxShield < 1)
		{
			this.ShieldFill.fillAmount = Mathf.Lerp(this.ShieldFill.fillAmount, (float)((this.Player.health.shield > 0f) ? 1 : 0), Time.deltaTime * 8f);
			return;
		}
		this.ShieldFill.fillAmount = Mathf.Lerp(this.ShieldFill.fillAmount, this.Player.health.CurrentShieldProportion, Time.deltaTime * 4f);
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x000733A5 File Offset: 0x000715A5
	public TeammateUI()
	{
	}

	// Token: 0x040011B3 RID: 4531
	[NonSerialized]
	public PlayerControl Player;

	// Token: 0x040011B4 RID: 4532
	public Image HealthFill;

	// Token: 0x040011B5 RID: 4533
	public Image ShieldFill;

	// Token: 0x040011B6 RID: 4534
	public TextMeshProUGUI Username;

	// Token: 0x040011B7 RID: 4535
	public Image AvatarIcon;

	// Token: 0x040011B8 RID: 4536
	public GameObject DeadDisplay;

	// Token: 0x040011B9 RID: 4537
	private float unT;
}
