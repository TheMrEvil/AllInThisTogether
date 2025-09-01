using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class GameDB : ScriptableObject
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600024B RID: 587 RVA: 0x00013D26 File Offset: 0x00011F26
	// (set) Token: 0x0600024C RID: 588 RVA: 0x00013D3F File Offset: 0x00011F3F
	private static GameDB instance
	{
		get
		{
			if (GameDB._i == null)
			{
				GameDB.EnsureInstance();
			}
			return GameDB._i;
		}
		set
		{
			GameDB._i = value;
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00013D48 File Offset: 0x00011F48
	public static void SetInstance(GameDB db)
	{
		GameDB.instance = db;
		GameDB.instance.RarityData = new Dictionary<Rarity, GameDB.RarityInfo>();
		foreach (GameDB.RarityInfo rarityInfo in db.RarityDetails)
		{
			GameDB.instance.RarityData.Add(rarityInfo.Rarity, rarityInfo);
		}
		GameDB.instance.QualityData = new Dictionary<AugmentQuality, GameDB.QualityInfo>();
		foreach (GameDB.QualityInfo qualityInfo in db.QualityDetails)
		{
			GameDB.instance.QualityData.Add(qualityInfo.Quality, qualityInfo);
		}
		GameDB.instance.ParseData = new Dictionary<string, GameDB.Parsable>();
		foreach (GameDB.Parsable parsable in db.TextParses)
		{
			parsable.AddInfo(ref GameDB.instance.ParseData);
		}
		foreach (GameDB.Parsable parsable2 in db.GenericParses)
		{
			parsable2.AddInfo(ref GameDB.instance.ParseData);
		}
		foreach (GameDB.Parsable parsable3 in db.EnemyParses)
		{
			parsable3.AddInfo(ref GameDB.instance.ParseData);
		}
		foreach (GameDB.Parsable parsable4 in db.GenreParses)
		{
			parsable4.AddInfo(ref GameDB.instance.ParseData);
		}
		GameDB.instance.ElementData = new Dictionary<MagicColor, GameDB.ElementInfo>();
		foreach (GameDB.ElementInfo elementInfo in db.Elements)
		{
			GameDB.instance.ElementData.Add(elementInfo.magicColor, elementInfo);
		}
		GameDB.instance.KeywordDatas = new Dictionary<Keyword, GameDB.KeywordData>();
		foreach (GameDB.KeywordData keywordData in db.Keywords)
		{
			GameDB.instance.KeywordDatas.Add(keywordData.keyword, keywordData);
		}
		GameDB.instance.EnemyTypeData = new Dictionary<EnemyType, GameDB.EnemyTypeInfo>();
		foreach (GameDB.EnemyTypeInfo enemyTypeInfo in db.EnemyTypes)
		{
			GameDB.instance.EnemyTypeData.Add(enemyTypeInfo.Type, enemyTypeInfo);
		}
		GameDB.instance.NumberIconData = new Dictionary<string, GameDB.NumberIcon>();
		foreach (GameDB.NumberIcon numberIcon in db.NumIcons)
		{
			GameDB.instance.NumberIconData.Add(numberIcon.ID.ToLower(), numberIcon);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000140EC File Offset: 0x000122EC
	public static GameDB.RarityInfo Rarity(Rarity r)
	{
		return GameDB.instance.RarityData[r];
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000140FE File Offset: 0x000122FE
	public static GameDB.QualityInfo Quality(AugmentQuality r)
	{
		return GameDB.instance.QualityData[r];
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00014110 File Offset: 0x00012310
	public static GameDB.KeywordData Keyword(Keyword k)
	{
		return GameDB.instance.KeywordDatas[k];
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00014124 File Offset: 0x00012324
	public GameDB.KeywordData GetKeywordDataInternal(Keyword k)
	{
		foreach (GameDB.KeywordData keywordData in this.Keywords)
		{
			if (keywordData.keyword == k)
			{
				return keywordData;
			}
		}
		return null;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00014180 File Offset: 0x00012380
	public static GameDB.Parsable GetParsable(string ID)
	{
		ID = ID.ToLower().Replace("$", "").Replace("keyword-", "");
		if (GameDB.instance.ParseData.ContainsKey(ID))
		{
			return GameDB.instance.ParseData[ID];
		}
		return null;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x000141D7 File Offset: 0x000123D7
	public static GameDB.ElementInfo GetElement(MagicColor magicColor)
	{
		if (GameDB.instance.ElementData.ContainsKey(magicColor))
		{
			return GameDB.instance.ElementData[magicColor];
		}
		return null;
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00014200 File Offset: 0x00012400
	public static MagicColor GetColor(string name)
	{
		name = name.ToLower();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1169454059U)
		{
			if (num <= 96429129U)
			{
				if (num != 18738364U)
				{
					if (num == 96429129U)
					{
						if (name == "yellow")
						{
							return MagicColor.Yellow;
						}
					}
				}
				else if (name == "green")
				{
					return MagicColor.Green;
				}
			}
			else if (num != 576586605U)
			{
				if (num != 1089765596U)
				{
					if (num == 1169454059U)
					{
						if (name == "orange")
						{
							return MagicColor.Orange;
						}
					}
				}
				else if (name == "red")
				{
					return MagicColor.Red;
				}
			}
			else if (name == "pink")
			{
				return MagicColor.Pink;
			}
		}
		else if (num <= 2353732312U)
		{
			if (num != 1452231588U)
			{
				if (num != 2197550541U)
				{
					if (num == 2353732312U)
					{
						if (name == "neutral")
						{
							return MagicColor.Neutral;
						}
					}
				}
				else if (name == "blue")
				{
					return MagicColor.Blue;
				}
			}
			else if (name == "black")
			{
				return MagicColor.Black;
			}
		}
		else if (num != 2590900991U)
		{
			if (num != 2751299231U)
			{
				if (num == 3724674918U)
				{
					if (name == "white")
					{
						return MagicColor.White;
					}
				}
			}
			else if (name == "teal")
			{
				return MagicColor.Teal;
			}
		}
		else if (name == "purple")
		{
			return MagicColor.Purple;
		}
		return MagicColor.Neutral;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x000143A8 File Offset: 0x000125A8
	public static List<GameDB.PickupOption> GetPickupOptions(EntityControl control)
	{
		List<GameDB.PickupOption> list = new List<GameDB.PickupOption>();
		foreach (GameDB.PickupOption pickupOption in GameDB.instance.Pickups)
		{
			if (!(pickupOption.AugmentRequired == null) && (InkManager.HasAugment(pickupOption.AugmentRequired) || GameplayManager.instance.GetGameAugments(ModType.Fountain).TreeIDs.Contains(pickupOption.AugmentRequired.ID)))
			{
				list.Add(pickupOption);
			}
		}
		return list;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00014444 File Offset: 0x00012644
	public static GameDB.PickupOption GetPickupOption(GameObject prefabRef)
	{
		foreach (GameDB.PickupOption pickupOption in GameDB.instance.Pickups)
		{
			if (pickupOption.Ref == prefabRef)
			{
				return pickupOption;
			}
		}
		return null;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x000144AC File Offset: 0x000126AC
	public static GameDB.EnemyTypeInfo GetEnemyInfo(EnemyType enemyType)
	{
		if (GameDB.instance.EnemyTypeData.ContainsKey(enemyType))
		{
			return GameDB.instance.EnemyTypeData[enemyType];
		}
		return null;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x000144D2 File Offset: 0x000126D2
	public static GameDB.NumberIcon GetNumberIcon(string iconID)
	{
		iconID = iconID.ToLower();
		if (GameDB.instance.NumberIconData.ContainsKey(iconID))
		{
			return GameDB.instance.NumberIconData[iconID];
		}
		return null;
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00014500 File Offset: 0x00012700
	public static GameDB.ChanceText GetChanceReplacement(int chance)
	{
		foreach (GameDB.ChanceText chanceText in GameDB.instance.ChanceReplacements)
		{
			if (chance <= chanceText.MaxChance)
			{
				return chanceText;
			}
		}
		return null;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00014560 File Offset: 0x00012760
	private static void EnsureInstance()
	{
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00014562 File Offset: 0x00012762
	public GameDB()
	{
	}

	// Token: 0x0400024A RID: 586
	private static GameDB _i;

	// Token: 0x0400024B RID: 587
	public string BuildData = "-";

	// Token: 0x0400024C RID: 588
	public List<GameDB.PickupOption> Pickups;

	// Token: 0x0400024D RID: 589
	[SerializeField]
	private List<GameDB.KeywordData> Keywords;

	// Token: 0x0400024E RID: 590
	private Dictionary<Keyword, GameDB.KeywordData> KeywordDatas;

	// Token: 0x0400024F RID: 591
	[SerializeField]
	private List<GameDB.ElementInfo> Elements;

	// Token: 0x04000250 RID: 592
	private Dictionary<MagicColor, GameDB.ElementInfo> ElementData;

	// Token: 0x04000251 RID: 593
	[SerializeField]
	private List<GameDB.EnemyTypeInfo> EnemyTypes;

	// Token: 0x04000252 RID: 594
	private Dictionary<EnemyType, GameDB.EnemyTypeInfo> EnemyTypeData;

	// Token: 0x04000253 RID: 595
	[SerializeField]
	private List<GameDB.RarityInfo> RarityDetails;

	// Token: 0x04000254 RID: 596
	private Dictionary<Rarity, GameDB.RarityInfo> RarityData;

	// Token: 0x04000255 RID: 597
	[SerializeField]
	private List<GameDB.QualityInfo> QualityDetails;

	// Token: 0x04000256 RID: 598
	private Dictionary<AugmentQuality, GameDB.QualityInfo> QualityData;

	// Token: 0x04000257 RID: 599
	[SerializeField]
	private List<GameDB.Parsable> GenericParses;

	// Token: 0x04000258 RID: 600
	[SerializeField]
	private List<GameDB.Parsable> TextParses;

	// Token: 0x04000259 RID: 601
	[SerializeField]
	private List<GameDB.Parsable> EnemyParses;

	// Token: 0x0400025A RID: 602
	[SerializeField]
	private List<GameDB.Parsable> GenreParses;

	// Token: 0x0400025B RID: 603
	private Dictionary<string, GameDB.Parsable> ParseData;

	// Token: 0x0400025C RID: 604
	[SerializeField]
	private List<GameDB.NumberIcon> NumIcons;

	// Token: 0x0400025D RID: 605
	private Dictionary<string, GameDB.NumberIcon> NumberIconData;

	// Token: 0x0400025E RID: 606
	[SerializeField]
	private List<GameDB.ChanceText> ChanceReplacements;

	// Token: 0x0200043C RID: 1084
	[Serializable]
	public class KeywordData
	{
		// Token: 0x0600211F RID: 8479 RVA: 0x000C2378 File Offset: 0x000C0578
		public KeywordData()
		{
		}

		// Token: 0x0400218C RID: 8588
		public Keyword keyword;

		// Token: 0x0400218D RID: 8589
		public ActionTree Action;
	}

	// Token: 0x0200043D RID: 1085
	[Serializable]
	public class PickupOption
	{
		// Token: 0x06002120 RID: 8480 RVA: 0x000C2380 File Offset: 0x000C0580
		public PickupOption()
		{
		}

		// Token: 0x0400218E RID: 8590
		public GameObject Ref;

		// Token: 0x0400218F RID: 8591
		public AugmentTree AugmentRequired;

		// Token: 0x04002190 RID: 8592
		[Range(0f, 100f)]
		public float SpawnWeight;

		// Token: 0x04002191 RID: 8593
		public AnimationCurve DropStackCurve;
	}

	// Token: 0x0200043E RID: 1086
	[Serializable]
	public class RarityInfo
	{
		// Token: 0x06002121 RID: 8481 RVA: 0x000C2388 File Offset: 0x000C0588
		public RarityInfo()
		{
		}

		// Token: 0x04002192 RID: 8594
		public Rarity Rarity;

		// Token: 0x04002193 RID: 8595
		public int RelativeChance = 10;
	}

	// Token: 0x0200043F RID: 1087
	[Serializable]
	public class QualityInfo
	{
		// Token: 0x06002122 RID: 8482 RVA: 0x000C2398 File Offset: 0x000C0598
		public QualityInfo()
		{
		}

		// Token: 0x04002194 RID: 8596
		public AugmentQuality Quality;

		// Token: 0x04002195 RID: 8597
		public string Label;

		// Token: 0x04002196 RID: 8598
		[Space(5f)]
		public Color PlayerColor;

		// Token: 0x04002197 RID: 8599
		[Space(5f)]
		public Color EnemyColor;

		// Token: 0x04002198 RID: 8600
		[Space(5f)]
		public Color DarkTextColor;

		// Token: 0x04002199 RID: 8601
		public Sprite Border;

		// Token: 0x0400219A RID: 8602
		public Sprite BorderSmall;
	}

	// Token: 0x02000440 RID: 1088
	[Serializable]
	public class Parsable
	{
		// Token: 0x06002123 RID: 8483 RVA: 0x000C23A0 File Offset: 0x000C05A0
		public string GetID()
		{
			return "keyword-" + this.ID;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000C23B4 File Offset: 0x000C05B4
		public string GetReplacementText(string idType = "", bool ignoreIcons = false)
		{
			string str = (ignoreIcons || string.IsNullOrEmpty(this.InlineIcon)) ? "<nobr>" : ("<nobr><sprite name=\"" + this.InlineIcon + "\">");
			if (this.Alternatives.Count == 0 || string.IsNullOrEmpty(idType))
			{
				return str + "<style=\"keyword\">" + this.ReplaceWith + "</style></nobr>";
			}
			string b = idType.Replace("keyword-", "").Replace("$", "");
			foreach (GameDB.StringTuple stringTuple in this.Alternatives)
			{
				if (stringTuple.Key == b)
				{
					return str + "<style=\"keyword\">" + stringTuple.Value + "</style></nobr>";
				}
			}
			return str + "<style=\"keyword\">" + this.ReplaceWith + "</style></nobr>";
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000C24BC File Offset: 0x000C06BC
		public void AddInfo(ref Dictionary<string, GameDB.Parsable> dict)
		{
			dict.Add(this.ID.ToLower(), this);
			foreach (GameDB.StringTuple stringTuple in this.Alternatives)
			{
				dict.Add(stringTuple.Key.ToLower(), this);
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000C2530 File Offset: 0x000C0730
		public Parsable()
		{
		}

		// Token: 0x0400219B RID: 8603
		public string ID;

		// Token: 0x0400219C RID: 8604
		public List<GameDB.StringTuple> Alternatives = new List<GameDB.StringTuple>();

		// Token: 0x0400219D RID: 8605
		public string ReplaceWith;

		// Token: 0x0400219E RID: 8606
		public string InlineIcon;

		// Token: 0x0400219F RID: 8607
		public Sprite Icon;

		// Token: 0x040021A0 RID: 8608
		[TextArea(2, 4)]
		public string Description;
	}

	// Token: 0x02000441 RID: 1089
	[Serializable]
	public class StringTuple
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x000C2543 File Offset: 0x000C0743
		public StringTuple()
		{
		}

		// Token: 0x040021A1 RID: 8609
		public string Key;

		// Token: 0x040021A2 RID: 8610
		public string Value;
	}

	// Token: 0x02000442 RID: 1090
	[Serializable]
	public class ElementInfo
	{
		// Token: 0x06002128 RID: 8488 RVA: 0x000C254B File Offset: 0x000C074B
		public ElementInfo()
		{
		}

		// Token: 0x040021A3 RID: 8611
		public MagicColor magicColor;

		// Token: 0x040021A4 RID: 8612
		public Sprite Icon;

		// Token: 0x040021A5 RID: 8613
		public AugmentTree Core;

		// Token: 0x040021A6 RID: 8614
		public Sprite ManaIcon;

		// Token: 0x040021A7 RID: 8615
		[TextArea(2, 4)]
		public string ManaDescription;

		// Token: 0x040021A8 RID: 8616
		public AugmentTree BaseAugment;

		// Token: 0x040021A9 RID: 8617
		public AugmentTree AbilityAugment;
	}

	// Token: 0x02000443 RID: 1091
	[Serializable]
	public class NumberIcon
	{
		// Token: 0x06002129 RID: 8489 RVA: 0x000C2553 File Offset: 0x000C0753
		public string GetText()
		{
			if (this.Sprite.Length == 0 || Settings.GetInt(SystemSetting.NumberIcons, 0) == 0)
			{
				return this.Text;
			}
			return "<sprite name=" + this.Sprite + ">";
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000C2588 File Offset: 0x000C0788
		public NumberIcon()
		{
		}

		// Token: 0x040021AA RID: 8618
		public string ID;

		// Token: 0x040021AB RID: 8619
		public string Sprite;

		// Token: 0x040021AC RID: 8620
		public string Text;
	}

	// Token: 0x02000444 RID: 1092
	[Serializable]
	public class ChanceText
	{
		// Token: 0x0600212B RID: 8491 RVA: 0x000C2590 File Offset: 0x000C0790
		private string Title()
		{
			return " <= " + this.MaxChance.ToString() + " -> " + this.Replacement;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000C25B2 File Offset: 0x000C07B2
		public ChanceText()
		{
		}

		// Token: 0x040021AD RID: 8621
		public int MaxChance;

		// Token: 0x040021AE RID: 8622
		public string Replacement;
	}

	// Token: 0x02000445 RID: 1093
	[Serializable]
	public class EnemyTypeInfo
	{
		// Token: 0x0600212D RID: 8493 RVA: 0x000C25BA File Offset: 0x000C07BA
		private string GetLabel()
		{
			return "$enemy-" + this.Type.ToString().ToLower() + "$";
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000C25E1 File Offset: 0x000C07E1
		public EnemyTypeInfo()
		{
		}

		// Token: 0x040021AF RID: 8623
		public EnemyType Type;

		// Token: 0x040021B0 RID: 8624
		public Sprite Icon;

		// Token: 0x040021B1 RID: 8625
		public string Inline;

		// Token: 0x040021B2 RID: 8626
		[TextArea(3, 5)]
		public string Description;

		// Token: 0x040021B3 RID: 8627
		public string NameSimple;
	}
}
