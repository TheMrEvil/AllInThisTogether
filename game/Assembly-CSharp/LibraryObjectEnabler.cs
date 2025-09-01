using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class LibraryObjectEnabler : MonoBehaviour
{
	// Token: 0x06000CFF RID: 3327 RVA: 0x00052F33 File Offset: 0x00051133
	private void Start()
	{
		LibraryObjectEnabler.instance = this;
		NetworkManager.LeftRoom = (Action)Delegate.Combine(NetworkManager.LeftRoom, new Action(this.OnLeftRoom));
		this.Reset();
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x00052F64 File Offset: 0x00051164
	public static string GetToggleValues()
	{
		string text = "";
		if (LibraryObjectEnabler.instance == null)
		{
			return text;
		}
		foreach (LibraryObjectEnabler.ActivatableRef activatableRef in LibraryObjectEnabler.instance.Activatables)
		{
			text += (activatableRef.RequirementMet() ? "1" : "0");
		}
		return text;
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x00052FE8 File Offset: 0x000511E8
	public void LoadRoomObjects(List<bool> roomObjects)
	{
		if (roomObjects.Count != this.Activatables.Count)
		{
			Debug.LogError("Activatable list length does not match room object list length!");
			return;
		}
		for (int i = 0; i < roomObjects.Count; i++)
		{
			this.Activatables[i].OverrideActive(roomObjects[i]);
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0005303C File Offset: 0x0005123C
	private void Reset()
	{
		foreach (LibraryObjectEnabler.ActivatableRef activatableRef in this.Activatables)
		{
			activatableRef.OverrideActive(false);
			activatableRef.TryEnable();
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x00053094 File Offset: 0x00051294
	private void OnLeftRoom()
	{
		this.Reset();
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0005309C File Offset: 0x0005129C
	private void OnDestroy()
	{
		NetworkManager.LeftRoom = (Action)Delegate.Remove(NetworkManager.LeftRoom, new Action(this.OnLeftRoom));
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x000530BE File Offset: 0x000512BE
	public LibraryObjectEnabler()
	{
	}

	// Token: 0x04000A66 RID: 2662
	public List<LibraryObjectEnabler.ActivatableRef> Activatables;

	// Token: 0x04000A67 RID: 2663
	private static LibraryObjectEnabler instance;

	// Token: 0x02000519 RID: 1305
	[Serializable]
	public class ActivatableRef
	{
		// Token: 0x060023CC RID: 9164 RVA: 0x000CBF7E File Offset: 0x000CA17E
		public void TryEnable()
		{
			if (this.RequirementMet())
			{
				this.ObjectToEnable.SetActive(true);
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000CBF94 File Offset: 0x000CA194
		public void OverrideActive(bool active)
		{
			this.ObjectToEnable.SetActive(active);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000CBFA4 File Offset: 0x000CA1A4
		public bool RequirementMet()
		{
			bool result;
			switch (this.Requirement)
			{
			case LibraryObjectEnabler.Requirement.None:
				result = true;
				break;
			case LibraryObjectEnabler.Requirement.Tome_Completed:
				result = (GameStats.GetTomeStat(this.Tome, GameStats.Stat.TomesWon, 0) > 0);
				break;
			case LibraryObjectEnabler.Requirement.Tome_Unlocked:
				result = UnlockManager.IsGenreUnlocked(this.Tome);
				break;
			case LibraryObjectEnabler.Requirement.Tome_Binding:
				result = (GameStats.GetTomeStat(this.Tome, GameStats.Stat.MaxBinding, 0) >= this.NumValue);
				break;
			case LibraryObjectEnabler.Requirement.Binding_Attunement:
				result = (Progression.BindingAttunement >= this.NumValue);
				break;
			case LibraryObjectEnabler.Requirement.Achievement:
				result = AchievementManager.IsUnlocked(this.ID);
				break;
			case LibraryObjectEnabler.Requirement.InkLevel:
				result = (Progression.InkLevel >= this.NumValue);
				break;
			case LibraryObjectEnabler.Requirement.CoreUnlocked:
				result = UnlockManager.IsCoreUnlocked(this.Augment);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000CC06C File Offset: 0x000CA26C
		private bool NeedsNumber()
		{
			switch (this.Requirement)
			{
			case LibraryObjectEnabler.Requirement.Tome_Binding:
				return true;
			case LibraryObjectEnabler.Requirement.Binding_Attunement:
				return true;
			case LibraryObjectEnabler.Requirement.InkLevel:
				return true;
			}
			return false;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000CC0AC File Offset: 0x000CA2AC
		private bool NeedsTome()
		{
			bool result;
			switch (this.Requirement)
			{
			case LibraryObjectEnabler.Requirement.Tome_Completed:
				result = true;
				break;
			case LibraryObjectEnabler.Requirement.Tome_Unlocked:
				result = true;
				break;
			case LibraryObjectEnabler.Requirement.Tome_Binding:
				result = true;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000CC0E8 File Offset: 0x000CA2E8
		private bool NeedsID()
		{
			return this.Requirement == LibraryObjectEnabler.Requirement.Achievement;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000CC108 File Offset: 0x000CA308
		private bool NeedsAugment()
		{
			return this.Requirement == LibraryObjectEnabler.Requirement.CoreUnlocked;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000CC125 File Offset: 0x000CA325
		public ActivatableRef()
		{
		}

		// Token: 0x040025CE RID: 9678
		[Header("Requirement")]
		public LibraryObjectEnabler.Requirement Requirement;

		// Token: 0x040025CF RID: 9679
		[Header("")]
		public GenreTree Tome;

		// Token: 0x040025D0 RID: 9680
		[Header("")]
		public AugmentTree Augment;

		// Token: 0x040025D1 RID: 9681
		[Header("")]
		public int NumValue;

		// Token: 0x040025D2 RID: 9682
		[Header("")]
		public string ID;

		// Token: 0x040025D3 RID: 9683
		public GameObject ObjectToEnable;
	}

	// Token: 0x0200051A RID: 1306
	public enum Requirement
	{
		// Token: 0x040025D5 RID: 9685
		None,
		// Token: 0x040025D6 RID: 9686
		Tome_Completed,
		// Token: 0x040025D7 RID: 9687
		Tome_Unlocked,
		// Token: 0x040025D8 RID: 9688
		Tome_Binding,
		// Token: 0x040025D9 RID: 9689
		Binding_Attunement,
		// Token: 0x040025DA RID: 9690
		Achievement,
		// Token: 0x040025DB RID: 9691
		InkLevel,
		// Token: 0x040025DC RID: 9692
		CoreUnlocked
	}
}
