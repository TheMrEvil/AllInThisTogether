using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000170 RID: 368
public class FriendListItem : MonoBehaviour, IDeselectHandler, IEventSystemHandler, ISelectHandler
{
	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00063AF7 File Offset: 0x00061CF7
	private bool CanInvite
	{
		get
		{
			return Time.realtimeSinceStartup - this.LastInviteTime > 3f;
		}
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00063B0C File Offset: 0x00061D0C
	public void Setup(FriendList.FriendItem friend)
	{
		this.Friend = friend;
		this.Refresh();
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x00063B1B File Offset: 0x00061D1B
	public void TickUpdate()
	{
		this.InviteDisplay.UpdateOpacity(this.selected && this.CanInvite, 3f, true);
		this.InvitePending.UpdateOpacity(!this.CanInvite, 3f, true);
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00063B5C File Offset: 0x00061D5C
	public void Refresh()
	{
		this.UsernameText.text = this.Friend.Username;
		this.UsernameGlowText.text = this.Friend.Username;
		if (this.Friend.IsInGame)
		{
			this.StatusIcon.sprite = this.InGameIcon;
			return;
		}
		if (this.Friend.IsOnline)
		{
			this.StatusIcon.sprite = this.OnlineIcon;
			return;
		}
		this.StatusIcon.sprite = this.OfflineIcon;
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00063BE4 File Offset: 0x00061DE4
	public void Invite()
	{
		if (!this.CanInvite)
		{
			return;
		}
		this.LastInviteTime = Time.realtimeSinceStartup;
		this.Friend.Invite();
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00063C05 File Offset: 0x00061E05
	public void OnSelect(BaseEventData eventData)
	{
		this.selected = true;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00063C0E File Offset: 0x00061E0E
	public void OnDeselect(BaseEventData eventData)
	{
		this.selected = false;
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00063C17 File Offset: 0x00061E17
	public FriendListItem()
	{
	}

	// Token: 0x04000DE6 RID: 3558
	public TextMeshProUGUI UsernameText;

	// Token: 0x04000DE7 RID: 3559
	public TextMeshProUGUI UsernameGlowText;

	// Token: 0x04000DE8 RID: 3560
	public FriendList.FriendItem Friend;

	// Token: 0x04000DE9 RID: 3561
	public Image StatusIcon;

	// Token: 0x04000DEA RID: 3562
	public CanvasGroup InviteDisplay;

	// Token: 0x04000DEB RID: 3563
	public CanvasGroup InvitePending;

	// Token: 0x04000DEC RID: 3564
	public Sprite InGameIcon;

	// Token: 0x04000DED RID: 3565
	public Sprite OnlineIcon;

	// Token: 0x04000DEE RID: 3566
	public Sprite OfflineIcon;

	// Token: 0x04000DEF RID: 3567
	private bool selected;

	// Token: 0x04000DF0 RID: 3568
	private float LastInviteTime;
}
