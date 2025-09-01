using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

// Token: 0x02000344 RID: 836
[Serializable]
public class ModFilterList
{
	// Token: 0x06001C4D RID: 7245 RVA: 0x000AD097 File Offset: 0x000AB297
	public bool HasFilter(ModFilter filter)
	{
		return this.Filters.Contains(filter);
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x000AD0A8 File Offset: 0x000AB2A8
	public bool Matches(ModFilterList tags, bool any = false)
	{
		if (any)
		{
			foreach (ModFilter item in this.Filters)
			{
				if (tags.Filters.Contains(item))
				{
					return true;
				}
			}
			return false;
		}
		bool flag = true;
		foreach (ModFilter item2 in this.Filters)
		{
			flag &= tags.Filters.Contains(item2);
		}
		return flag;
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x000AD15C File Offset: 0x000AB35C
	public ModFilterList Copy()
	{
		ModFilterList modFilterList = base.MemberwiseClone() as ModFilterList;
		modFilterList.Filters = new List<ModFilter>();
		foreach (ModFilter item in this.Filters)
		{
			modFilterList.Filters.Add(item);
		}
		return modFilterList;
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x000AD1CC File Offset: 0x000AB3CC
	public ModFilterList()
	{
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x000AD1E0 File Offset: 0x000AB3E0
	// Note: this type is marked as 'beforefieldinit'.
	static ModFilterList()
	{
	}

	// Token: 0x04001CEC RID: 7404
	public List<ModFilter> Filters = new List<ModFilter>();

	// Token: 0x04001CED RID: 7405
	private static IEnumerable Format = new ValueDropdownList<ModFilter>
	{
		{
			"General/Offense",
			ModFilter.General_Offense
		},
		{
			"General/Defense",
			ModFilter.General_Defense
		},
		{
			"General/Movement",
			ModFilter.General_Movement
		},
		{
			"General/Simple",
			ModFilter.General_Simple
		},
		{
			"General/Conditional",
			ModFilter.General_Conditional
		},
		{
			"General/Tradeoff",
			ModFilter.Mech_Tradeoff
		},
		{
			"Combat/Ranged",
			ModFilter.General_Ranged
		},
		{
			"Combat/Melee",
			ModFilter.General_Melee
		},
		{
			"Combat/Support",
			ModFilter.General_Support
		},
		{
			"Counter/Anti-Movement",
			ModFilter.Anti_Movement
		},
		{
			"Counter/Anti-Range",
			ModFilter.Anti_Ranged
		},
		{
			"Counter/Anti-Melee",
			ModFilter.Anti_Melee
		},
		{
			"Counter/Anti-Single Target",
			ModFilter.Anti_Target
		},
		{
			"Counter/Anti-AoE",
			ModFilter.Anti_AoE
		},
		{
			"Counter/Anti-Heavy Dmg",
			ModFilter.Anti_HeavyDmg
		},
		{
			"Counter/Anti-Light Dmg",
			ModFilter.Anti_LightDmg
		},
		{
			"Mechanic/Passive Stat",
			ModFilter.Mech_Statmod
		},
		{
			"Mechanic/Projectile",
			ModFilter.Mech_Projectile
		},
		{
			"Mechanic/AoE",
			ModFilter.Mech_AoE
		},
		{
			"Mechanic/Beam",
			ModFilter.Mech_Beam
		},
		{
			"Mechanic/Buff",
			ModFilter.Mech_Buff
		},
		{
			"Mechanic/Debuff",
			ModFilter.Mech_Debuff
		},
		{
			"Mechanic/Cooldown",
			ModFilter.Mech_Cooldown
		},
		{
			"Mechanic/Keyword Add",
			ModFilter.Mech_AddKeyword
		},
		{
			"Mechanic/Keyword Mod",
			ModFilter.Mech_Keyword
		},
		{
			"Mechanic/Drops",
			ModFilter.Mech_Drop
		},
		{
			"Mechanic/Health",
			ModFilter.Mech_Health
		},
		{
			"Mechanic/Barrier",
			ModFilter.Mech_Barrier
		},
		{
			"Mechanic/Physics Force",
			ModFilter.Mech_PhysForce
		},
		{
			"Mechanic/Forced Move",
			ModFilter.Mech_ForceMove
		},
		{
			"Mechanic/On Spawn",
			ModFilter.Mech_Teleport
		},
		{
			"Mechanic/Summon",
			ModFilter.Mech_Summon
		},
		{
			"Mechanic/Teleport",
			ModFilter.Mech_Teleport
		},
		{
			"Player/Core",
			ModFilter.Player_Core
		},
		{
			"Player/Core Keyword",
			ModFilter.Player_CoreKeyword
		},
		{
			"Player/Core Ability",
			ModFilter.Player_CoreAbility
		},
		{
			"Player/Primary",
			ModFilter.Player_Primary
		},
		{
			"Player/Secondary",
			ModFilter.Player_Secondary
		},
		{
			"Player/Movement",
			ModFilter.Player_Movement
		},
		{
			"Player/Mana",
			ModFilter.Mech_Mana
		},
		{
			"Enemy/On Death",
			ModFilter.Mech_OnDeath
		},
		{
			"Enemy/On Spawn",
			ModFilter.Mech_OnSpawn
		},
		{
			"Enemy/Boss",
			ModFilter.Enemy_Boss
		},
		{
			"Fountain/From Fountain",
			ModFilter.Fountain
		},
		{
			"Fountain/Map",
			ModFilter.Fountain_MapInteract
		},
		{
			"Fountain/Game Loop",
			ModFilter.Fountain_GameLoop
		},
		{
			"Fountain/Enemy Affect",
			ModFilter.Fountain_AntiEnemy
		},
		{
			"Fountain/Bonus Objective",
			ModFilter.Fountain_Bonus
		},
		{
			"Fountain/Longterm",
			ModFilter.Fountain_Longterm
		},
		{
			"Fountain/Tome Specific",
			ModFilter.Fountain_Tome
		},
		{
			"Fountain/Tome - Breadth",
			ModFilter.Genre_Breadth
		},
		{
			"Fountain/Tome - Depth",
			ModFilter.Genre_Depth
		},
		{
			"Fountain/Tome - Special",
			ModFilter.Genre_Special
		},
		{
			"Special/GameChanger",
			ModFilter.GameChanger
		},
		{
			"Special/Augment Choice",
			ModFilter.AugmentChoice
		},
		{
			"Special/Ability Changer",
			ModFilter.AbilityChanger
		}
	};
}
