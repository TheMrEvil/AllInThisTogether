using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class PlayerDB : ScriptableObject
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002A1 RID: 673 RVA: 0x00016A9F File Offset: 0x00014C9F
	public static int InkLevelCount
	{
		get
		{
			return PlayerDB.instance.NextLevelExp.Count;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002A2 RID: 674 RVA: 0x00016AB0 File Offset: 0x00014CB0
	public static List<PlayerDB.TalentRow> InkLevels
	{
		get
		{
			return PlayerDB.instance.TalentLevels;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060002A3 RID: 675 RVA: 0x00016ABC File Offset: 0x00014CBC
	public static List<PlayerDB.LibraryTalent> LibraryTalents
	{
		get
		{
			return PlayerDB.instance.LibraryTals;
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x00016AC8 File Offset: 0x00014CC8
	public static void SetInstance(PlayerDB db)
	{
		PlayerDB.instance = db;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x00016AD0 File Offset: 0x00014CD0
	public static PlayerDB.InputSprite GetMainBinding(InputActions.InputAction action)
	{
		PlayerDB.SpriteType allowedType = global::InputManager.IsUsingController ? PlayerDB.SpriteType.Controller : PlayerDB.SpriteType.KBM;
		List<PlayerDB.InputSprite> bindings = PlayerDB.GetBindings(action, allowedType);
		if (bindings.Count == 0 || bindings[0].Sprite == null)
		{
			return null;
		}
		return bindings[0];
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x00016B18 File Offset: 0x00014D18
	public static List<PlayerDB.InputSprite> GetBindings(InputActions.InputAction action, PlayerDB.SpriteType allowedType = PlayerDB.SpriteType.Any)
	{
		List<PlayerDB.InputSprite> list = new List<PlayerDB.InputSprite>();
		if (!Application.isPlaying || global::InputManager.Actions == null)
		{
			return list;
		}
		PlayerAction action2 = global::InputManager.Actions.GetAction(action);
		if (action2 == null)
		{
			return list;
		}
		foreach (BindingSource bindingSource in action2.Bindings)
		{
			PlayerDB.InputSprite inputSprite = new PlayerDB.InputSprite();
			KeyBindingSource keyBindingSource = bindingSource as KeyBindingSource;
			if (keyBindingSource != null && allowedType != PlayerDB.SpriteType.Controller)
			{
				PlayerDB.KeyboardSprite keyboardSprite = PlayerDB.GetKeyboardSprite(keyBindingSource.Control.GetInclude(0));
				if (keyboardSprite != null)
				{
					inputSprite.Sprite = keyboardSprite.Sprite;
					inputSprite.SpriteID = keyboardSprite.SpriteText;
					list.Add(inputSprite);
				}
			}
			else
			{
				MouseBindingSource mouseBindingSource = bindingSource as MouseBindingSource;
				if (mouseBindingSource != null && allowedType != PlayerDB.SpriteType.Controller)
				{
					PlayerDB.MouseSprite mouseSprite = PlayerDB.GetMouseSprite(mouseBindingSource.Control);
					if (mouseSprite != null)
					{
						inputSprite.Sprite = mouseSprite.Sprite;
						inputSprite.SpriteID = mouseSprite.SpriteText;
						list.Add(inputSprite);
					}
				}
				else
				{
					DeviceBindingSource deviceBindingSource = bindingSource as DeviceBindingSource;
					if (deviceBindingSource != null && allowedType != PlayerDB.SpriteType.KBM)
					{
						PlayerDB.ControllerSprite controllerSprite = PlayerDB.GetControllerSprite(deviceBindingSource.Control);
						if (controllerSprite != null)
						{
							if (controllerSprite.UniqueController)
							{
								if (global::InputManager.Controller == global::InputManager.ControllerType.Playstation)
								{
									inputSprite.Sprite = controllerSprite.PlaySprite;
									inputSprite.SpriteID = controllerSprite.PlayID;
								}
								else
								{
									inputSprite.Sprite = controllerSprite.XboxSprite;
									inputSprite.SpriteID = controllerSprite.XboxID;
								}
							}
							else
							{
								inputSprite.Sprite = controllerSprite.Sprite;
								inputSprite.SpriteID = controllerSprite.SpriteText;
							}
							list.Add(inputSprite);
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00016CE0 File Offset: 0x00014EE0
	private static PlayerDB.KeyboardSprite GetKeyboardSprite(Key m)
	{
		foreach (PlayerDB.KeyboardSprite keyboardSprite in PlayerDB.instance.KeyboardInputs)
		{
			if (keyboardSprite.Input == m)
			{
				return keyboardSprite;
			}
		}
		return null;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00016D40 File Offset: 0x00014F40
	private static PlayerDB.MouseSprite GetMouseSprite(Mouse m)
	{
		foreach (PlayerDB.MouseSprite mouseSprite in PlayerDB.instance.MouseInputs)
		{
			if (mouseSprite.Input == m)
			{
				return mouseSprite;
			}
		}
		return null;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00016DA0 File Offset: 0x00014FA0
	private static PlayerDB.ControllerSprite GetControllerSprite(InputControlType m)
	{
		foreach (PlayerDB.ControllerSprite controllerSprite in PlayerDB.instance.ControllerInputs)
		{
			if (controllerSprite.Input == m)
			{
				return controllerSprite;
			}
		}
		return null;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00016E00 File Offset: 0x00015000
	public static PlayerDB.CoreDisplay GetCore(AugmentTree core)
	{
		if (core == null)
		{
			return null;
		}
		foreach (PlayerDB.CoreDisplay coreDisplay in PlayerDB.instance.Cores)
		{
			if (coreDisplay.core == core)
			{
				return coreDisplay;
			}
		}
		return null;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00016E70 File Offset: 0x00015070
	public static PlayerDB.CoreDisplay GetCore(MagicColor color)
	{
		foreach (PlayerDB.CoreDisplay coreDisplay in PlayerDB.instance.Cores)
		{
			if (coreDisplay.color == color)
			{
				return coreDisplay;
			}
		}
		return null;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00016ED0 File Offset: 0x000150D0
	public static PlayerDB.CoreDisplay GetCore(AbilityTree coreAbility)
	{
		if (coreAbility == null)
		{
			return null;
		}
		foreach (PlayerDB.CoreDisplay coreDisplay in PlayerDB.instance.Cores)
		{
			if (coreDisplay.Ability == coreAbility)
			{
				return coreDisplay;
			}
		}
		return null;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00016F40 File Offset: 0x00015140
	public static PlayerDB.PingDisplay GetPing(PlayerDB.PingType type)
	{
		if (PlayerDB.instance == null)
		{
			return null;
		}
		foreach (PlayerDB.PingDisplay pingDisplay in PlayerDB.instance.Pings)
		{
			if (pingDisplay.Type == type)
			{
				return pingDisplay;
			}
		}
		return null;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00016FB0 File Offset: 0x000151B0
	public static int GetNextInkLevelRequirement(int AtLevel)
	{
		if (AtLevel >= PlayerDB.InkLevelCount || AtLevel < 0)
		{
			return -1;
		}
		return PlayerDB.instance.NextLevelExp[AtLevel];
	}

	// Token: 0x060002AF RID: 687 RVA: 0x00016FD0 File Offset: 0x000151D0
	public static PlayerDB.TalentTree GetTalentTree(MagicColor color)
	{
		switch (color)
		{
		case MagicColor.Neutral:
			return PlayerDB.instance.NeutralTree;
		case MagicColor.Red:
			return PlayerDB.instance.Red;
		case MagicColor.Yellow:
			return PlayerDB.instance.Yellow;
		case MagicColor.Green:
			return PlayerDB.instance.Green;
		case MagicColor.Blue:
			return PlayerDB.instance.BlueTree;
		case MagicColor.Pink:
			return PlayerDB.instance.Pink;
		case MagicColor.Orange:
			return PlayerDB.instance.Orange;
		case MagicColor.Teal:
			return PlayerDB.instance.Teal;
		}
		return null;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00017080 File Offset: 0x00015280
	public static int GetUnlockedTalentLevel(MagicColor color)
	{
		switch (color)
		{
		case MagicColor.Neutral:
			return PlayerDB.instance.NeutralTree.GetUnlockedCount();
		case MagicColor.Red:
			return PlayerDB.instance.Red.GetUnlockedCount();
		case MagicColor.Yellow:
			return PlayerDB.instance.Yellow.GetUnlockedCount();
		case MagicColor.Green:
			return PlayerDB.instance.Green.GetUnlockedCount();
		case MagicColor.Blue:
			return PlayerDB.instance.BlueTree.GetUnlockedCount();
		case MagicColor.Pink:
			return PlayerDB.instance.Pink.GetUnlockedCount();
		case MagicColor.Orange:
			return PlayerDB.instance.Orange.GetUnlockedCount();
		case MagicColor.Teal:
			return PlayerDB.instance.Teal.GetUnlockedCount();
		}
		return 0;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0001715C File Offset: 0x0001535C
	public static int GetTalentRowsAvailable(MagicColor color, int level)
	{
		switch (color)
		{
		case MagicColor.Neutral:
			return PlayerDB.instance.NeutralTree.GetRowsAvailable(level);
		case MagicColor.Red:
			return PlayerDB.instance.Red.GetRowsAvailable(level);
		case MagicColor.Yellow:
			return PlayerDB.instance.Yellow.GetRowsAvailable(level);
		case MagicColor.Green:
			return PlayerDB.instance.Green.GetRowsAvailable(level);
		case MagicColor.Blue:
			return PlayerDB.instance.BlueTree.GetRowsAvailable(level);
		case MagicColor.Pink:
			return PlayerDB.instance.Pink.GetRowsAvailable(level);
		case MagicColor.Orange:
			return PlayerDB.instance.Orange.GetRowsAvailable(level);
		case MagicColor.Teal:
			return PlayerDB.instance.Teal.GetRowsAvailable(level);
		}
		return 0;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00017240 File Offset: 0x00015440
	public static Augments GetCurrentTalents()
	{
		Augments augments = new Augments();
		if (PlayerDB.instance == null)
		{
			return augments;
		}
		int inkLevel = Progression.InkLevel;
		foreach (PlayerDB.TalentRow talentRow in PlayerDB.instance.TalentLevels)
		{
			if (talentRow.Level <= inkLevel && talentRow.Type == PlayerDB.LevelType.Single)
			{
				augments.Add(talentRow.Talent_1, 1);
			}
		}
		augments.Add(PlayerDB.instance.NeutralTree.GetEquippedTalents());
		if (MapManager.InLobbyScene)
		{
			augments.Add(Progression.LibraryBuild.GetEquippedTalents());
		}
		PlayerControl myInstance = PlayerControl.myInstance;
		PlayerDB.TalentTree talentTree = PlayerDB.GetTalentTree((myInstance != null) ? myInstance.actions.core.Root.magicColor : MagicColor.Any);
		if (talentTree != null)
		{
			augments.Add(talentTree.GetEquippedTalents());
		}
		if (MapManager.InLobbyScene)
		{
			augments.Add(Progression.LibraryBuild.GetEquippedTalents());
		}
		return augments;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0001734C File Offset: 0x0001554C
	public PlayerDB()
	{
	}

	// Token: 0x04000297 RID: 663
	private static PlayerDB instance;

	// Token: 0x04000298 RID: 664
	public List<PlayerDB.CoreDisplay> Cores;

	// Token: 0x04000299 RID: 665
	public List<PlayerDB.PingDisplay> Pings;

	// Token: 0x0400029A RID: 666
	public List<PlayerDB.MouseSprite> MouseInputs;

	// Token: 0x0400029B RID: 667
	public List<PlayerDB.KeyboardSprite> KeyboardInputs;

	// Token: 0x0400029C RID: 668
	public List<PlayerDB.ControllerSprite> ControllerInputs;

	// Token: 0x0400029D RID: 669
	public List<int> NextLevelExp;

	// Token: 0x0400029E RID: 670
	public List<PlayerDB.TalentRow> TalentLevels;

	// Token: 0x0400029F RID: 671
	public PlayerDB.TalentTree NeutralTree;

	// Token: 0x040002A0 RID: 672
	public PlayerDB.TalentTree BlueTree;

	// Token: 0x040002A1 RID: 673
	public PlayerDB.TalentTree Yellow;

	// Token: 0x040002A2 RID: 674
	public PlayerDB.TalentTree Green;

	// Token: 0x040002A3 RID: 675
	public PlayerDB.TalentTree Red;

	// Token: 0x040002A4 RID: 676
	public PlayerDB.TalentTree Pink;

	// Token: 0x040002A5 RID: 677
	public PlayerDB.TalentTree Orange;

	// Token: 0x040002A6 RID: 678
	public PlayerDB.TalentTree Teal;

	// Token: 0x040002A7 RID: 679
	public List<PlayerDB.LibraryTalent> LibraryTals;

	// Token: 0x02000456 RID: 1110
	[Serializable]
	public class CoreDisplay
	{
		// Token: 0x0600215C RID: 8540 RVA: 0x000C2D8C File Offset: 0x000C0F8C
		public CoreDisplay()
		{
		}

		// Token: 0x04002210 RID: 8720
		public MagicColor color;

		// Token: 0x04002211 RID: 8721
		public AugmentTree core;

		// Token: 0x04002212 RID: 8722
		public AbilityTree Ability;

		// Token: 0x04002213 RID: 8723
		public Sprite MajorIcon;

		// Token: 0x04002214 RID: 8724
		public Sprite MajorIcon_Disabled;

		// Token: 0x04002215 RID: 8725
		public Sprite BorderGlowIcon;

		// Token: 0x04002216 RID: 8726
		public Sprite BigIcon;

		// Token: 0x04002217 RID: 8727
		public Sprite MinorIcon;

		// Token: 0x04002218 RID: 8728
		public Material AbilityGlow;

		// Token: 0x04002219 RID: 8729
		public Color ParticleColor;

		// Token: 0x0400221A RID: 8730
		public Color EnemyHitColor;

		// Token: 0x0400221B RID: 8731
		[ColorUsage(false, true)]
		public Color BodyGlowColor;

		// Token: 0x0400221C RID: 8732
		public Color LightColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x0400221D RID: 8733
		public Material ParticleMaterial;

		// Token: 0x0400221E RID: 8734
		public List<string> Adjectives;
	}

	// Token: 0x02000457 RID: 1111
	[Serializable]
	public class PlayerInfo
	{
		// Token: 0x0600215D RID: 8541 RVA: 0x000C2DB4 File Offset: 0x000C0FB4
		public PlayerInfo()
		{
		}

		// Token: 0x0400221F RID: 8735
		public Color IconTint = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04002220 RID: 8736
		public Color TextTint = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x02000458 RID: 1112
	[Serializable]
	public class PingDisplay
	{
		// Token: 0x0600215E RID: 8542 RVA: 0x000C2E05 File Offset: 0x000C1005
		public PingDisplay()
		{
		}

		// Token: 0x04002221 RID: 8737
		public PlayerDB.PingType Type;

		// Token: 0x04002222 RID: 8738
		public Color Color;

		// Token: 0x04002223 RID: 8739
		public Sprite Icon;

		// Token: 0x04002224 RID: 8740
		public Sprite Arrow;
	}

	// Token: 0x02000459 RID: 1113
	public enum PingType
	{
		// Token: 0x04002226 RID: 8742
		Generic,
		// Token: 0x04002227 RID: 8743
		Yes,
		// Token: 0x04002228 RID: 8744
		No,
		// Token: 0x04002229 RID: 8745
		Attack,
		// Token: 0x0400222A RID: 8746
		Danger,
		// Token: 0x0400222B RID: 8747
		Eye,
		// Token: 0x0400222C RID: 8748
		Location,
		// Token: 0x0400222D RID: 8749
		Help,
		// Token: 0x0400222E RID: 8750
		SignatureStatus
	}

	// Token: 0x0200045A RID: 1114
	public class InputSprite
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x000C2E0D File Offset: 0x000C100D
		public InputSprite()
		{
		}

		// Token: 0x0400222F RID: 8751
		public Sprite Sprite;

		// Token: 0x04002230 RID: 8752
		public string SpriteID;
	}

	// Token: 0x0200045B RID: 1115
	[Serializable]
	public class KeyboardSprite
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x000C2E15 File Offset: 0x000C1015
		public KeyboardSprite()
		{
		}

		// Token: 0x04002231 RID: 8753
		public Key Input;

		// Token: 0x04002232 RID: 8754
		public Sprite Sprite;

		// Token: 0x04002233 RID: 8755
		public string SpriteText;
	}

	// Token: 0x0200045C RID: 1116
	[Serializable]
	public class MouseSprite
	{
		// Token: 0x06002161 RID: 8545 RVA: 0x000C2E1D File Offset: 0x000C101D
		public MouseSprite()
		{
		}

		// Token: 0x04002234 RID: 8756
		public Mouse Input;

		// Token: 0x04002235 RID: 8757
		public Sprite Sprite;

		// Token: 0x04002236 RID: 8758
		public string SpriteText;
	}

	// Token: 0x0200045D RID: 1117
	[Serializable]
	public class ControllerSprite
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x000C2E25 File Offset: 0x000C1025
		public ControllerSprite()
		{
		}

		// Token: 0x04002237 RID: 8759
		public InputControlType Input;

		// Token: 0x04002238 RID: 8760
		public bool UniqueController;

		// Token: 0x04002239 RID: 8761
		public Sprite Sprite;

		// Token: 0x0400223A RID: 8762
		public string SpriteText;

		// Token: 0x0400223B RID: 8763
		[Header("XBox")]
		public Sprite XboxSprite;

		// Token: 0x0400223C RID: 8764
		public string XboxID;

		// Token: 0x0400223D RID: 8765
		[Header("Playstation")]
		public Sprite PlaySprite;

		// Token: 0x0400223E RID: 8766
		public string PlayID;
	}

	// Token: 0x0200045E RID: 1118
	public enum SpriteType
	{
		// Token: 0x04002240 RID: 8768
		Any,
		// Token: 0x04002241 RID: 8769
		KBM,
		// Token: 0x04002242 RID: 8770
		Controller
	}

	// Token: 0x0200045F RID: 1119
	[Serializable]
	public class TalentTree
	{
		// Token: 0x06002163 RID: 8547 RVA: 0x000C2E30 File Offset: 0x000C1030
		public List<AugmentTree> GetEquippedTalents()
		{
			List<AugmentTree> list = new List<AugmentTree>();
			if (Progression.TalentBuild == null)
			{
				return list;
			}
			List<int> list2 = new List<int>();
			List<int> list3;
			if (Progression.TalentBuild.SelectedTalents.TryGetValue(this.Color, out list3))
			{
				list2 = list3;
			}
			int inkLevel = Progression.InkLevel;
			for (int i = 0; i < this.Rows.Count; i++)
			{
				PlayerDB.TalentRow talentRow = this.Rows[i];
				if (talentRow.Type != PlayerDB.LevelType.Info_Augment && talentRow.Level <= inkLevel)
				{
					List<AugmentTree> talents = talentRow.Talents;
					AugmentTree augmentTree = talents[0];
					if (talents.Count > 1 && i < list2.Count)
					{
						int num = list2[i];
						if (num >= 0 && num < talentRow.Talents.Count)
						{
							augmentTree = talents[num];
						}
					}
					if (!(augmentTree == null))
					{
						list.Add(augmentTree);
					}
				}
			}
			return list;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000C2F1C File Offset: 0x000C111C
		public int GetUnlockedCount()
		{
			int num = 0;
			foreach (PlayerDB.TalentRow talentRow in this.Rows)
			{
				num += talentRow.Talents.Count;
			}
			return num;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000C2F7C File Offset: 0x000C117C
		public int GetRowsAvailable(int curLevel)
		{
			int num = 0;
			foreach (PlayerDB.TalentRow talentRow in this.Rows)
			{
				if (curLevel >= talentRow.Level)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000C2FD8 File Offset: 0x000C11D8
		public TalentTree()
		{
		}

		// Token: 0x04002243 RID: 8771
		public MagicColor Color;

		// Token: 0x04002244 RID: 8772
		public List<PlayerDB.TalentRow> Rows;
	}

	// Token: 0x02000460 RID: 1120
	[Serializable]
	public class TalentRow
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000C2FE0 File Offset: 0x000C11E0
		private bool NeedsTalent2
		{
			get
			{
				PlayerDB.LevelType type = this.Type;
				return type == PlayerDB.LevelType.Choice_2 || type == PlayerDB.LevelType.Choice_3;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000C3008 File Offset: 0x000C1208
		public List<AugmentTree> Talents
		{
			get
			{
				if (this.Type == PlayerDB.LevelType.Choice_3)
				{
					return new List<AugmentTree>
					{
						this.Talent_1,
						this.Talent_2,
						this.Talent_3
					};
				}
				if (this.Type == PlayerDB.LevelType.Choice_2)
				{
					return new List<AugmentTree>
					{
						this.Talent_1,
						this.Talent_2
					};
				}
				return new List<AugmentTree>
				{
					this.Talent_1
				};
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000C3080 File Offset: 0x000C1280
		public TalentRow()
		{
		}

		// Token: 0x04002245 RID: 8773
		public int Level;

		// Token: 0x04002246 RID: 8774
		public PlayerDB.LevelType Type;

		// Token: 0x04002247 RID: 8775
		public AugmentTree Talent_1;

		// Token: 0x04002248 RID: 8776
		public AugmentTree Talent_2;

		// Token: 0x04002249 RID: 8777
		public AugmentTree Talent_3;

		// Token: 0x0400224A RID: 8778
		public PlayerDB.TalentRow.Importance importance;

		// Token: 0x0400224B RID: 8779
		public PlayerDB.TalentRow.ButtonType Button;

		// Token: 0x020006BB RID: 1723
		public enum Importance
		{
			// Token: 0x04002CCD RID: 11469
			Base,
			// Token: 0x04002CCE RID: 11470
			Medium,
			// Token: 0x04002CCF RID: 11471
			High
		}

		// Token: 0x020006BC RID: 1724
		public enum ButtonType
		{
			// Token: 0x04002CD1 RID: 11473
			None,
			// Token: 0x04002CD2 RID: 11474
			NeutralTalent,
			// Token: 0x04002CD3 RID: 11475
			ColorTalent
		}
	}

	// Token: 0x02000461 RID: 1121
	[Serializable]
	public class LibraryTalent
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000C3088 File Offset: 0x000C1288
		public List<AugmentTree> Talents
		{
			get
			{
				if (this.Talent_2 != null && this.Talent_3 != null)
				{
					return new List<AugmentTree>
					{
						this.Talent_1,
						this.Talent_2,
						this.Talent_3
					};
				}
				if (this.Talent_2 != null)
				{
					return new List<AugmentTree>
					{
						this.Talent_1,
						this.Talent_2
					};
				}
				return new List<AugmentTree>
				{
					this.Talent_1
				};
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000C3118 File Offset: 0x000C1318
		public LibraryTalent()
		{
		}

		// Token: 0x0400224C RID: 8780
		public string RowName;

		// Token: 0x0400224D RID: 8781
		public int UnlockCost;

		// Token: 0x0400224E RID: 8782
		public AugmentTree Talent_1;

		// Token: 0x0400224F RID: 8783
		public AugmentTree Talent_2;

		// Token: 0x04002250 RID: 8784
		public AugmentTree Talent_3;
	}

	// Token: 0x02000462 RID: 1122
	public enum LevelType
	{
		// Token: 0x04002252 RID: 8786
		Single,
		// Token: 0x04002253 RID: 8787
		Choice_2,
		// Token: 0x04002254 RID: 8788
		Choice_3,
		// Token: 0x04002255 RID: 8789
		Info_Augment
	}
}
