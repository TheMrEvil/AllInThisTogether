using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000214 RID: 532
public static class TextParser
{
	// Token: 0x06001678 RID: 5752 RVA: 0x0008D4E8 File Offset: 0x0008B6E8
	public static string AugmentDetail(string input, AugmentTree augment, EntityControl owner = null, bool parseConditionals = false)
	{
		PlayerControl playerControl = owner as PlayerControl;
		if (playerControl != null)
		{
			input = TextParser.InlineAbility(input, augment, playerControl, false);
		}
		else
		{
			input = TextParser.InlineAbility(input, augment, null, parseConditionals);
		}
		input = TextParser.InlineInput(input);
		input = TextParser.InlineEnemyInfo(input);
		input = TextParser.InlineMana(input, owner);
		input = TextParser.InlineKeywords(input);
		input = TextParser.InlineChance(input);
		input = TextParser.InlineNumber(input);
		input = TextParser.InlinePlayerCount(input);
		input = TextParser.InlineTags(input);
		return input;
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x0008D55C File Offset: 0x0008B75C
	public static List<GameDB.Parsable> GetKeywords(string input, EntityControl owner = null)
	{
		List<GameDB.Parsable> list = new List<GameDB.Parsable>();
		if (input == null)
		{
			return list;
		}
		foreach (object obj in TextParser.keywordExp.Matches(input))
		{
			GameDB.Parsable parsable = GameDB.GetParsable(obj.ToString());
			if (parsable == null)
			{
				parsable = TextParser.GetExplicitKeywordParse(obj.ToString(), owner);
			}
			if (parsable != null)
			{
				list.Add(parsable);
			}
		}
		foreach (object obj2 in TextParser.abilityExp.Matches(input))
		{
			PlayerAbilityType item = TextParser.GetAbilityType(obj2.ToString()).Item1;
			if (item != PlayerAbilityType.Any && !(owner == null))
			{
				PlayerControl playerControl = owner as PlayerControl;
				if (playerControl != null)
				{
					Ability ability = playerControl.actions.GetAbility(item);
					list.Add(new GameDB.Parsable
					{
						ReplaceWith = ability.props.AbilityMetadata.Name,
						Icon = ability.props.AbilityMetadata.Icon,
						Description = TextParser.GetAbilityDescription(item, ability),
						ID = ability.props.AbilityMetadata.Name
					});
				}
			}
		}
		foreach (object obj3 in TextParser.enemyExp.Matches(input))
		{
			ValueTuple<EnemyType, string> enemyType = TextParser.GetEnemyType(obj3.ToString());
			EnemyType item2 = enemyType.Item1;
			string item3 = enemyType.Item2;
			GameDB.EnemyTypeInfo enemyInfo = GameDB.GetEnemyInfo(item2);
			if (enemyInfo != null)
			{
				list.Add(new GameDB.Parsable
				{
					ReplaceWith = string.Concat(new string[]
					{
						"<color=\"white\"><font=\"GPro-Torn\" material=\"",
						item3,
						"\">",
						enemyInfo.Inline,
						"</b></font></color>"
					}),
					Icon = enemyInfo.Icon,
					Description = enemyInfo.Description,
					ID = enemyInfo.Inline
				});
			}
		}
		foreach (object obj4 in TextParser.manaExp.Matches(input))
		{
			string text = obj4.ToString().ToLower().Replace("$", "").Replace("mana-", "").Replace("temp-", "");
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			MagicColor magicColor;
			if (num <= 1452231588U)
			{
				if (num <= 576586605U)
				{
					if (num != 18738364U)
					{
						if (num != 96429129U)
						{
							if (num != 576586605U)
							{
								goto IL_481;
							}
							if (!(text == "pink"))
							{
								goto IL_481;
							}
							magicColor = MagicColor.Pink;
						}
						else
						{
							if (!(text == "yellow"))
							{
								goto IL_481;
							}
							magicColor = MagicColor.Yellow;
						}
					}
					else
					{
						if (!(text == "green"))
						{
							goto IL_481;
						}
						magicColor = MagicColor.Green;
					}
				}
				else if (num != 1089765596U)
				{
					if (num != 1169454059U)
					{
						if (num != 1452231588U)
						{
							goto IL_481;
						}
						if (!(text == "black"))
						{
							goto IL_481;
						}
						magicColor = MagicColor.Black;
					}
					else
					{
						if (!(text == "orange"))
						{
							goto IL_481;
						}
						magicColor = MagicColor.Orange;
					}
				}
				else
				{
					if (!(text == "red"))
					{
						goto IL_481;
					}
					magicColor = MagicColor.Red;
				}
			}
			else if (num <= 2590900991U)
			{
				if (num != 2197550541U)
				{
					if (num != 2353732312U)
					{
						if (num != 2590900991U)
						{
							goto IL_481;
						}
						if (!(text == "purple"))
						{
							goto IL_481;
						}
						magicColor = MagicColor.Purple;
					}
					else
					{
						if (!(text == "neutral"))
						{
							goto IL_481;
						}
						magicColor = MagicColor.Neutral;
					}
				}
				else
				{
					if (!(text == "blue"))
					{
						goto IL_481;
					}
					magicColor = MagicColor.Blue;
				}
			}
			else if (num != 2751299231U)
			{
				if (num != 3713949822U)
				{
					if (num != 3724674918U)
					{
						goto IL_481;
					}
					if (!(text == "white"))
					{
						goto IL_481;
					}
					magicColor = MagicColor.White;
				}
				else
				{
					if (!(text == "core"))
					{
						goto IL_481;
					}
					PlayerControl playerControl2 = owner as PlayerControl;
					magicColor = ((playerControl2 != null) ? playerControl2.SignatureColor : MagicColor.Neutral);
				}
			}
			else
			{
				if (!(text == "teal"))
				{
					goto IL_481;
				}
				magicColor = MagicColor.Teal;
			}
			IL_484:
			MagicColor magicColor2 = magicColor;
			if (magicColor2 != MagicColor.Any && magicColor2 != MagicColor.Neutral)
			{
				GameDB.ElementInfo element = GameDB.GetElement((magicColor2 == MagicColor.Any) ? MagicColor.Neutral : magicColor2);
				GameDB.Parsable parsable2 = new GameDB.Parsable();
				if (magicColor2 != MagicColor.Any)
				{
					parsable2.ReplaceWith = magicColor2.ToString() + " Mana";
				}
				else
				{
					parsable2.ReplaceWith = "Mana";
				}
				parsable2.Icon = element.ManaIcon;
				parsable2.Description = element.ManaDescription;
				if (obj4.ToString().Contains("temp"))
				{
					parsable2.ReplaceWith = "Temporary Mana";
					parsable2.Description += "<line-height=70%>\n\n<line-height=100%>Temporary mana is removed when used.";
				}
				list.Add(parsable2);
				continue;
			}
			continue;
			IL_481:
			magicColor = MagicColor.Any;
			goto IL_484;
		}
		return list;
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0008DB28 File Offset: 0x0008BD28
	private static GameDB.Parsable GetExplicitKeywordParse(string test, EntityControl owner = null)
	{
		if (!(test == "$keyword-core$"))
		{
			return null;
		}
		PlayerControl myInstance = PlayerControl.myInstance;
		AugmentTree core;
		if (myInstance == null)
		{
			core = null;
		}
		else
		{
			PlayerActions actions = myInstance.actions;
			core = ((actions != null) ? actions.core : null);
		}
		PlayerDB.CoreDisplay core2 = PlayerDB.GetCore(core);
		if (core2 == null)
		{
			return null;
		}
		return new GameDB.Parsable
		{
			ReplaceWith = core2.core.GetName(),
			Icon = core2.core.Root.Icon,
			Description = TextParser.AugmentDetail(core2.core.GetDetail(), core2.core, owner, false)
		};
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x0008DBB8 File Offset: 0x0008BDB8
	private static string InlineKeywords(string input)
	{
		if (input == null)
		{
			return input;
		}
		foreach (object obj in TextParser.keywordExp.Matches(input))
		{
			GameDB.Parsable parsable = GameDB.GetParsable(obj.ToString());
			if (parsable == null)
			{
				input = TextParser.InlineExplicitKeyword(input, obj.ToString());
			}
			else
			{
				string text = obj.ToString();
				input = input.Replace(text, parsable.GetReplacementText(text, false));
			}
		}
		return input;
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x0008DC4C File Offset: 0x0008BE4C
	private static string InlineExplicitKeyword(string input, string test)
	{
		string text = "";
		string a = test.ToLower();
		if (a == "$keyword-core$")
		{
			PlayerControl myInstance = PlayerControl.myInstance;
			AugmentTree core;
			if (myInstance == null)
			{
				core = null;
			}
			else
			{
				PlayerActions actions = myInstance.actions;
				core = ((actions != null) ? actions.core : null);
			}
			PlayerDB.CoreDisplay core2 = PlayerDB.GetCore(core);
			if (core2 == null)
			{
				return input;
			}
			text = "<b>" + core2.core.GetName() + "</b>";
		}
		if (a == "$scribes$")
		{
			text = "<b>Scribes</b>";
		}
		if (a == "$scribe$")
		{
			text = "<b>Scribe</b>";
		}
		if (a == "$torn$")
		{
			text = "<color=\"white\"><font=\"GPro-Torn\">Torn</font></color>";
		}
		if (a == "$binding$")
		{
			text = "<sprite name=\"binding\"><b>Binding</b>";
		}
		if (a == "$barrier$")
		{
			text = "<sprite name=\"sym_bar\"><b>Barrier</b>";
		}
		if (a == "$boss$")
		{
			text = "<b>Boss</b>";
		}
		if (text.Length > 0)
		{
			input = input.Replace(test, text);
		}
		return input;
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x0008DD3C File Offset: 0x0008BF3C
	private static string InlineEnemyInfo(string input)
	{
		if (input == null)
		{
			return "";
		}
		foreach (object obj in TextParser.enemyExp.Matches(input))
		{
			string text = obj.ToString().ToLower().Replace("$", "").Replace("enemy-", "");
			string text2 = (text[text.Length - 1] == 's') ? "s" : "";
			ValueTuple<EnemyType, string> enemyType = TextParser.GetEnemyType(obj.ToString());
			EnemyType item = enemyType.Item1;
			string item2 = enemyType.Item2;
			if (item != EnemyType.Any)
			{
				GameDB.EnemyTypeInfo enemyInfo = GameDB.GetEnemyInfo(item);
				if (enemyInfo != null)
				{
					string text3 = enemyInfo.Inline;
					text3 = string.Concat(new string[]
					{
						"<color=\"white\"><font=\"GPro-Torn\" material=\"",
						item2,
						"\">",
						text3,
						text2,
						"</b></font></color>"
					});
					input = input.Replace(obj.ToString(), text3);
				}
			}
		}
		return input;
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x0008DE5C File Offset: 0x0008C05C
	private static string InlineInput(string input)
	{
		if (input == null)
		{
			return "";
		}
		foreach (object obj in TextParser.inputExp.Matches(input))
		{
			PlayerDB.InputSprite mainBinding = PlayerDB.GetMainBinding(InputActions.GetInputFromString(obj.ToString().ToLower().Replace("$", "").Replace("input-", "")));
			if (mainBinding == null || string.IsNullOrEmpty(mainBinding.SpriteID))
			{
				input = input.Replace(obj.ToString(), "");
			}
			else
			{
				input = input.Replace(obj.ToString(), "<sprite name=" + mainBinding.SpriteID + ">");
			}
		}
		return input;
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x0008DF3C File Offset: 0x0008C13C
	private static ValueTuple<EnemyType, string> GetEnemyType(string input)
	{
		string text = input.ToLower().Replace("$", "").Replace("enemy-", "");
		if (((text[text.Length - 1] == 's') ? "s" : "") == "s")
		{
			text = text.Substring(0, text.Length - 1);
		}
		ValueTuple<EnemyType, string> result;
		if (!(text == "_"))
		{
			if (!(text == "raving"))
			{
				if (!(text == "elite"))
				{
					if (!(text == "tangent"))
					{
						if (!(text == "splice"))
						{
							result = new ValueTuple<EnemyType, string>(EnemyType.Any, "GPro-Torn");
						}
						else
						{
							result = new ValueTuple<EnemyType, string>(EnemyType.Splice, "GPro-Splice");
						}
					}
					else
					{
						result = new ValueTuple<EnemyType, string>(EnemyType.Tangent, "GPro-Tangent");
					}
				}
				else
				{
					result = new ValueTuple<EnemyType, string>(EnemyType.__, "GPro-Torn");
				}
			}
			else
			{
				result = new ValueTuple<EnemyType, string>(EnemyType.Raving, "GPro-Raving");
			}
		}
		else
		{
			result = new ValueTuple<EnemyType, string>(EnemyType._, "GPro-Torn");
		}
		return result;
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x0008E040 File Offset: 0x0008C240
	private static string InlineAbility(string input, AugmentTree tree, PlayerControl player, bool parseConditionals = false)
	{
		if (input == null)
		{
			return input;
		}
		foreach (object obj in TextParser.abilityExp.Matches(input))
		{
			ValueTuple<PlayerAbilityType, string> abilityType = TextParser.GetAbilityType(obj.ToString());
			PlayerAbilityType item = abilityType.Item1;
			string str = abilityType.Item2;
			if (item != PlayerAbilityType.Any)
			{
				if (player != null)
				{
					Ability ability = player.actions.GetAbility(item);
					if (ability == null || ability.rootNode.Usage == null)
					{
						continue;
					}
					str = ability.rootNode.Usage.AbilityMetadata.Name;
				}
				else if (tree != null && tree.Root.modType == ModType.Player && parseConditionals)
				{
					foreach (Node node in tree.nodes)
					{
						Logic_Ability logic_Ability = node as Logic_Ability;
						if (logic_Ability != null)
						{
							if (logic_Ability.Test == Logic_Ability.AbilityTest.HasAbility && UnlockManager.IsAbilityUnlocked(logic_Ability.Graph))
							{
								str = logic_Ability.Graph.Root.Usage.AbilityMetadata.Name;
								break;
							}
							break;
						}
					}
				}
				input = input.Replace(obj.ToString(), TextParser.GetAbilityActionSprite(item) + "<style=\"ability\">" + str + "</style>");
			}
		}
		return input;
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x0008E1E4 File Offset: 0x0008C3E4
	private static string InlineMana(string input, EntityControl owner)
	{
		if (input == null)
		{
			return input;
		}
		foreach (object obj in TextParser.manaExp.Matches(input))
		{
			string str = obj.ToString().ToLower().Replace("$", "").Replace("mana-", "").Replace("temp-", "");
			if (obj.ToString() == "$mana-any$")
			{
				str = "neutral";
			}
			else if (obj.ToString() == "$mana-core$")
			{
				PlayerControl playerControl = owner as PlayerControl;
				str = ((playerControl != null) ? playerControl.SignatureColor.ToString().ToLower() : PlayerControl.myInstance.SignatureColor.ToString().ToLower());
			}
			string text = "<sprite name=\"mana_" + str + "\">";
			if (obj.ToString().Contains("temp"))
			{
				text = "<b>Temporary</b> " + text;
			}
			input = input.Replace(obj.ToString(), text);
		}
		return input;
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x0008E33C File Offset: 0x0008C53C
	private static string InlineChance(string input)
	{
		if (input == null)
		{
			return null;
		}
		foreach (object obj in TextParser.chanceExp.Matches(input))
		{
			string[] array = obj.ToString().Replace("$", "").Split('|', StringSplitOptions.None);
			if (array.Length >= 2)
			{
				TextParser.Connotation connotation = TextParser.Connotation.Neutral;
				if (array.Length == 3)
				{
					connotation = ((array[1] == "-") ? TextParser.Connotation.Negative : TextParser.Connotation.Positive);
				}
				string[] array2 = array;
				int chance;
				int.TryParse(array2[array2.Length - 1], out chance);
				string text = (Settings.GetInt(SystemSetting.ChanceParse, 0) == 0) ? (chance.ToString() + "%") : GameDB.GetChanceReplacement(chance).Replacement;
				string text2;
				if (connotation != TextParser.Connotation.Positive)
				{
					if (connotation != TextParser.Connotation.Negative)
					{
						text2 = text + " chance";
					}
					else
					{
						text2 = "<neg><nobr>" + text + "</neg> Chance</nobr>";
					}
				}
				else
				{
					text2 = "<pos><nobr>" + text + "</pos> Chance</nobr>";
				}
				string newValue = text2;
				input = input.Replace(obj.ToString(), newValue);
			}
		}
		return input;
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x0008E474 File Offset: 0x0008C674
	private static string InlineNumber(string input)
	{
		if (input == null)
		{
			return null;
		}
		foreach (object obj in TextParser.numExp.Matches(input))
		{
			string[] array = obj.ToString().Replace("$", "").Split('|', StringSplitOptions.None);
			if (array.Length >= 2)
			{
				TextParser.Connotation connotation = (array[0] == "-") ? TextParser.Connotation.Negative : TextParser.Connotation.Positive;
				float num = 0f;
				string str = "";
				int num2 = 1;
				if (!float.TryParse(array[num2], NumberStyles.Any, CultureInfo.InvariantCulture, out num) && array.Length > num2 + 1)
				{
					str = array[num2];
					num2++;
					float.TryParse(array[num2], NumberStyles.Any, CultureInfo.InvariantCulture, out num);
				}
				string str2 = "";
				string text = "";
				if (array.Length > num2 + 2)
				{
					str2 = array[num2 + 1];
					text = array[num2 + 2];
				}
				else if (array.Length > num2 + 1)
				{
					if (GameDB.GetNumberIcon(array[num2 + 1]) != null)
					{
						text = array[num2 + 1];
					}
					else
					{
						str2 = array[num2 + 1];
					}
				}
				if (text.Length > 0 && GameDB.GetNumberIcon(text) != null)
				{
					text = GameDB.GetNumberIcon(text).GetText();
				}
				string text2 = str + num.ToString() + str2;
				string text3;
				if (connotation != TextParser.Connotation.Positive)
				{
					if (connotation != TextParser.Connotation.Negative)
					{
						text3 = text2 + text;
					}
					else
					{
						text3 = "<neg><nobr>" + text2 + text + "</nobr></neg>";
					}
				}
				else
				{
					text3 = "<pos><nobr>" + text2 + text + "</nobr></pos>";
				}
				text2 = text3;
				input = input.Replace(obj.ToString(), text2);
			}
		}
		return input;
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x0008E650 File Offset: 0x0008C850
	public static string InlineTags(string input)
	{
		input = input.Replace("<pos>", "<font=\"Aleg_Numbers\"><color=#66f75f><b>");
		input = input.Replace("</pos>", "</b></color></font>");
		input = input.Replace("<neg>", "<font=\"Aleg_Numbers\"><color=#ff555d><b>");
		input = input.Replace("</neg>", "</b></color></font>");
		return input;
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x0008E6A8 File Offset: 0x0008C8A8
	public static string InlinePlayerCount(string input)
	{
		foreach (object obj in TextParser.pcountExp.Matches(input))
		{
			string[] array = obj.ToString().Replace("$", "").Split('|', StringSplitOptions.None);
			if (array.Length < 2)
			{
				return input;
			}
			int b = Mathf.Max(1, PlayerControl.PlayerCount);
			string newValue = array[Mathf.Min(array.Length - 1, b)];
			input = input.Replace(obj.ToString(), newValue);
		}
		return input;
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x0008E758 File Offset: 0x0008C958
	private static ValueTuple<PlayerAbilityType, string> GetAbilityType(string match)
	{
		string text = match.ToLower().Replace("$", "").Replace("ability-", "");
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 1605069165U)
		{
			if (num <= 308562864U)
			{
				if (num != 89534040U)
				{
					if (num == 308562864U)
					{
						if (text == "spender")
						{
							return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Secondary, "Spender");
						}
					}
				}
				else if (text == "movement")
				{
					return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Movement, "Movement Spell");
				}
			}
			else if (num != 1266453457U)
			{
				if (num != 1512988633U)
				{
					if (num == 1605069165U)
					{
						if (text == "signature")
						{
							return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Utility, "Signature Spell");
						}
					}
				}
				else if (text == "primary")
				{
					return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Primary, "Generator");
				}
			}
			else if (text == "secondary")
			{
				return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Secondary, "Spender");
			}
		}
		else if (num <= 2017461200U)
		{
			if (num != 1860974018U)
			{
				if (num == 2017461200U)
				{
					if (text == "ghost")
					{
						return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Ghost, "Ghost Fire");
					}
				}
			}
			else if (text == "generator")
			{
				return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Primary, "Generator");
			}
		}
		else if (num != 3217471839U)
		{
			if (num != 3713949822U)
			{
				if (num == 3945505414U)
				{
					if (text == "ultimate")
					{
						return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Utility, "Signature Spell");
					}
				}
			}
			else if (text == "core")
			{
				return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Utility, "Signature Spell");
			}
		}
		else if (text == "utility")
		{
			return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Utility, "Signature Spell");
		}
		return new ValueTuple<PlayerAbilityType, string>(PlayerAbilityType.Any, "INVALID ABILITY");
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x0008E990 File Offset: 0x0008CB90
	public static string GetAbilityActionSprite(PlayerAbilityType ability)
	{
		InputActions.InputAction action;
		switch (ability)
		{
		case PlayerAbilityType.Primary:
			action = InputActions.InputAction.Ability_Primary;
			break;
		case PlayerAbilityType.Secondary:
			action = InputActions.InputAction.Ability_Secondary;
			break;
		case PlayerAbilityType.Utility:
			action = InputActions.InputAction.Ability_Signature;
			break;
		case PlayerAbilityType.Movement:
			action = InputActions.InputAction.Ability_Movement;
			break;
		default:
			action = InputActions.InputAction.Ability_Primary;
			break;
		}
		PlayerDB.InputSprite mainBinding = PlayerDB.GetMainBinding(action);
		if (mainBinding == null || string.IsNullOrEmpty(mainBinding.SpriteID))
		{
			return "";
		}
		return "<sprite name=" + mainBinding.SpriteID + ">";
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x0008EA00 File Offset: 0x0008CC00
	private static string GetAbilityDescription(PlayerAbilityType aType, Ability ability)
	{
		string abilityActionSprite = TextParser.GetAbilityActionSprite(aType);
		string result;
		switch (aType)
		{
		case PlayerAbilityType.Primary:
			result = abilityActionSprite + " Mana <b>Generating</b> ability";
			break;
		case PlayerAbilityType.Secondary:
			result = abilityActionSprite + " Mana <b>Spending</b> ability";
			break;
		case PlayerAbilityType.Utility:
			result = abilityActionSprite + " Signature ability";
			break;
		case PlayerAbilityType.Movement:
			result = abilityActionSprite + " Movement ability";
			break;
		case PlayerAbilityType.Ghost:
			result = TextParser.GetAbilityActionSprite(PlayerAbilityType.Primary) + " Ghost ability, usable while dead";
			break;
		default:
			result = "";
			break;
		}
		return result;
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x0008EA84 File Offset: 0x0008CC84
	public static string EditorParse(string input)
	{
		if (input == null || input.Length == 0)
		{
			return input;
		}
		string text = TextParser.AugmentDetail(input, null, null, false);
		foreach (object obj in TextParser.bbcExp.Matches(text))
		{
			string text2 = obj.ToString();
			if (text2.Contains("sprite"))
			{
				text = text.Replace(obj.ToString(), "");
			}
			else if (text2.Contains('/'))
			{
				text = text.Replace(obj.ToString(), "</b>");
			}
			else
			{
				text = text.Replace(obj.ToString(), "<b>");
			}
		}
		return text;
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x0008EB4C File Offset: 0x0008CD4C
	// Note: this type is marked as 'beforefieldinit'.
	static TextParser()
	{
	}

	// Token: 0x04001608 RID: 5640
	private const string AbilityPattern = "\\$.bility-[^\\$]*\\$";

	// Token: 0x04001609 RID: 5641
	private const string ManaPattern = "\\$(mana-|temp-mana-)[^\\$]*\\$";

	// Token: 0x0400160A RID: 5642
	private const string KeywordPattern = "\\$.[^\\$]*\\$";

	// Token: 0x0400160B RID: 5643
	private const string EnemyPattern = "\\$.nemy-[^\\$]*\\$";

	// Token: 0x0400160C RID: 5644
	private const string ChancePattern = "\\$.hance[^\\$]*\\$";

	// Token: 0x0400160D RID: 5645
	private const string NumPattern = "\\$[+-]\\|[^\\$]*\\$";

	// Token: 0x0400160E RID: 5646
	private const string InputPattern = "\\$.nput-[^\\$]*\\$";

	// Token: 0x0400160F RID: 5647
	private const string BBCTagPattern = "\\<[^\\<]*\\>";

	// Token: 0x04001610 RID: 5648
	private const string PlayerCountPattern = "\\$.layers[^\\$]*\\$";

	// Token: 0x04001611 RID: 5649
	private static Regex keywordExp = new Regex("\\$.[^\\$]*\\$");

	// Token: 0x04001612 RID: 5650
	private static Regex abilityExp = new Regex("\\$.bility-[^\\$]*\\$");

	// Token: 0x04001613 RID: 5651
	private static Regex manaExp = new Regex("\\$(mana-|temp-mana-)[^\\$]*\\$");

	// Token: 0x04001614 RID: 5652
	private static Regex enemyExp = new Regex("\\$.nemy-[^\\$]*\\$");

	// Token: 0x04001615 RID: 5653
	private static Regex chanceExp = new Regex("\\$.hance[^\\$]*\\$");

	// Token: 0x04001616 RID: 5654
	private static Regex numExp = new Regex("\\$[+-]\\|[^\\$]*\\$");

	// Token: 0x04001617 RID: 5655
	private static Regex inputExp = new Regex("\\$.nput-[^\\$]*\\$");

	// Token: 0x04001618 RID: 5656
	private static Regex bbcExp = new Regex("\\<[^\\<]*\\>");

	// Token: 0x04001619 RID: 5657
	private static Regex pcountExp = new Regex("\\$.layers[^\\$]*\\$");

	// Token: 0x020005F8 RID: 1528
	private enum Connotation
	{
		// Token: 0x04002958 RID: 10584
		Neutral,
		// Token: 0x04002959 RID: 10585
		Positive,
		// Token: 0x0400295A RID: 10586
		Negative
	}
}
