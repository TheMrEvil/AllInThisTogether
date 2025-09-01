using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016F RID: 367
public class FriendList : MonoBehaviour
{
	// Token: 0x06000FC5 RID: 4037 RVA: 0x00063710 File Offset: 0x00061910
	public void Refresh()
	{
		foreach (FriendListItem friendListItem in this.FriendObjects)
		{
			UnityEngine.Object.Destroy(friendListItem.gameObject);
		}
		this.FriendObjects.Clear();
		List<FriendList.FriendItem> list = this.GetFriends();
		list = list.GetRange(0, Mathf.Min(list.Count, 40));
		foreach (FriendList.FriendItem friend in list)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FriendItemRef, this.FriendItemRef.transform.parent);
			gameObject.SetActive(true);
			FriendListItem component = gameObject.GetComponent<FriendListItem>();
			component.Setup(friend);
			component.GetComponent<Button>().SetNavigation(PausePanel.instance.InviteButtonInternal, UIDirection.Left, false);
			this.FriendObjects.Add(component);
		}
		this.UpdateValues();
		if (this.FriendObjects.Count > 0)
		{
			UISelector.SelectSelectable(this.FriendObjects[0].GetComponent<Button>());
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00063844 File Offset: 0x00061A44
	public void TickUpdate()
	{
		foreach (FriendListItem friendListItem in this.FriendObjects)
		{
			friendListItem.TickUpdate();
		}
		if (InputManager.IsUsingController)
		{
			this.Scroller.TickUpdate();
		}
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x000638A8 File Offset: 0x00061AA8
	public void UpdateValues()
	{
		this.FriendObjects.Sort((FriendListItem y, FriendListItem x) => y.Friend.Username.CompareTo(x.Friend.Username));
		this.FriendObjects.Sort((FriendListItem y, FriendListItem x) => x.Friend.IsOnline.CompareTo(y.Friend.IsOnline));
		this.FriendObjects.Sort((FriendListItem y, FriendListItem x) => x.Friend.IsInGame.CompareTo(y.Friend.IsInGame));
		int num = 0;
		foreach (FriendListItem friendListItem in this.FriendObjects)
		{
			friendListItem.Refresh();
			friendListItem.transform.SetSiblingIndex(num);
			num++;
		}
		if (this.FriendObjects.Count > 0)
		{
			Button component = this.FriendObjects[0].GetComponent<Button>();
			UISelector.SetupVerticalListNav<FriendListItem>(this.FriendObjects, null, null, true);
			PausePanel.instance.InviteButtonInternal.SetNavigation(component, UIDirection.Right, false);
		}
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x000639C8 File Offset: 0x00061BC8
	private List<FriendList.FriendItem> GetFriends()
	{
		List<FriendList.FriendItem> list = new List<FriendList.FriendItem>();
		if (!PlatformSetup.Initialized)
		{
			return list;
		}
		List<CSteamID> list2 = new List<CSteamID>();
		for (int i = 0; i < SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll); i++)
		{
			list2.Add(SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagAll));
		}
		foreach (CSteamID csteamID in list2)
		{
			EPersonaState friendPersonaState = SteamFriends.GetFriendPersonaState(csteamID);
			if (friendPersonaState != EPersonaState.k_EPersonaStateOffline && friendPersonaState != EPersonaState.k_EPersonaStateInvisible)
			{
				list.Add(new FriendList.FriendItem
				{
					Username = SteamFriends.GetFriendPersonaName(csteamID),
					ID = csteamID.ToString(),
					SteamID = csteamID
				});
			}
		}
		list.Sort((FriendList.FriendItem x, FriendList.FriendItem y) => y.IsOnline.CompareTo(x.IsOnline));
		list.Sort((FriendList.FriendItem x, FriendList.FriendItem y) => y.IsInGame.CompareTo(x.IsInGame));
		return list;
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x00063AE4 File Offset: 0x00061CE4
	public FriendList()
	{
	}

	// Token: 0x04000DE3 RID: 3555
	public GameObject FriendItemRef;

	// Token: 0x04000DE4 RID: 3556
	private List<FriendListItem> FriendObjects = new List<FriendListItem>();

	// Token: 0x04000DE5 RID: 3557
	public AutoScrollRect Scroller;

	// Token: 0x02000557 RID: 1367
	[Serializable]
	public class FriendItem
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000CE6C4 File Offset: 0x000CC8C4
		public bool IsOnline
		{
			get
			{
				EPersonaState friendPersonaState = SteamFriends.GetFriendPersonaState(this.SteamID);
				return friendPersonaState != EPersonaState.k_EPersonaStateOffline && friendPersonaState != EPersonaState.k_EPersonaStateInvisible;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000CE6EC File Offset: 0x000CC8EC
		public bool IsInGame
		{
			get
			{
				FriendGameInfo_t friendGameInfo_t;
				return SteamFriends.GetFriendGamePlayed(this.SteamID, out friendGameInfo_t) && (long)((int)friendGameInfo_t.m_gameID.m_GameID) == 917950L;
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000CE71F File Offset: 0x000CC91F
		public void Invite()
		{
			SteamFriends.InviteUserToGame(this.SteamID, NetworkManager.RoomCode);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000CE732 File Offset: 0x000CC932
		public FriendItem()
		{
		}

		// Token: 0x040026C9 RID: 9929
		public string Username;

		// Token: 0x040026CA RID: 9930
		public string ID;

		// Token: 0x040026CB RID: 9931
		public CSteamID SteamID;
	}

	// Token: 0x02000558 RID: 1368
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600248B RID: 9355 RVA: 0x000CE73A File Offset: 0x000CC93A
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000CE746 File Offset: 0x000CC946
		public <>c()
		{
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000CE74E File Offset: 0x000CC94E
		internal int <UpdateValues>b__5_0(FriendListItem y, FriendListItem x)
		{
			return y.Friend.Username.CompareTo(x.Friend.Username);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000CE76C File Offset: 0x000CC96C
		internal int <UpdateValues>b__5_1(FriendListItem y, FriendListItem x)
		{
			return x.Friend.IsOnline.CompareTo(y.Friend.IsOnline);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000CE798 File Offset: 0x000CC998
		internal int <UpdateValues>b__5_2(FriendListItem y, FriendListItem x)
		{
			return x.Friend.IsInGame.CompareTo(y.Friend.IsInGame);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000CE7C4 File Offset: 0x000CC9C4
		internal int <GetFriends>b__6_0(FriendList.FriendItem x, FriendList.FriendItem y)
		{
			return y.IsOnline.CompareTo(x.IsOnline);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000CE7E8 File Offset: 0x000CC9E8
		internal int <GetFriends>b__6_1(FriendList.FriendItem x, FriendList.FriendItem y)
		{
			return y.IsInGame.CompareTo(x.IsInGame);
		}

		// Token: 0x040026CC RID: 9932
		public static readonly FriendList.<>c <>9 = new FriendList.<>c();

		// Token: 0x040026CD RID: 9933
		public static Comparison<FriendListItem> <>9__5_0;

		// Token: 0x040026CE RID: 9934
		public static Comparison<FriendListItem> <>9__5_1;

		// Token: 0x040026CF RID: 9935
		public static Comparison<FriendListItem> <>9__5_2;

		// Token: 0x040026D0 RID: 9936
		public static Comparison<FriendList.FriendItem> <>9__6_0;

		// Token: 0x040026D1 RID: 9937
		public static Comparison<FriendList.FriendItem> <>9__6_1;
	}
}
