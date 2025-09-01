using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class LibraryJumpPuzzle : MonoBehaviour
{
	// Token: 0x06000CCD RID: 3277 RVA: 0x00051EE0 File Offset: 0x000500E0
	private void Awake()
	{
		LibraryManager libraryManager = UnityEngine.Object.FindObjectOfType<LibraryManager>();
		libraryManager.OnPlayerLoadoutChanged = (Action)Delegate.Combine(libraryManager.OnPlayerLoadoutChanged, new Action(this.OnPlayerLoadoutChanged));
		this.OnPlayerLoadoutChanged();
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x00051F10 File Offset: 0x00050110
	private void OnPlayerLoadoutChanged()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		Ability ability = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Movement);
		foreach (LibraryJumpPuzzle.JumpPuzzle jumpPuzzle in this.Puzzles)
		{
			if (jumpPuzzle.Abilities.Contains(ability.AbilityTree))
			{
				jumpPuzzle.Puzzle.SetActive(true);
				if (jumpPuzzle.ShowIfInactive != null)
				{
					jumpPuzzle.ShowIfInactive.SetActive(false);
				}
			}
			else
			{
				jumpPuzzle.Puzzle.SetActive(false);
				if (jumpPuzzle.ShowIfInactive != null)
				{
					jumpPuzzle.ShowIfInactive.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00051FDC File Offset: 0x000501DC
	public LibraryJumpPuzzle()
	{
	}

	// Token: 0x04000A30 RID: 2608
	public List<LibraryJumpPuzzle.JumpPuzzle> Puzzles;

	// Token: 0x02000511 RID: 1297
	[Serializable]
	public class JumpPuzzle
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x000CB9F6 File Offset: 0x000C9BF6
		public JumpPuzzle()
		{
		}

		// Token: 0x040025B2 RID: 9650
		public GameObject Puzzle;

		// Token: 0x040025B3 RID: 9651
		public GameObject ShowIfInactive;

		// Token: 0x040025B4 RID: 9652
		public List<AbilityTree> Abilities;
	}
}
