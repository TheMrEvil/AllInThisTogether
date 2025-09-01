using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000332 RID: 818
[CreateAssetMenu(order = -5)]
public class AugmentTree : GraphTree
{
	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000AB520 File Offset: 0x000A9720
	public AugmentRootNode Root
	{
		get
		{
			return this.RootNode as AugmentRootNode;
		}
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x000AB52D File Offset: 0x000A972D
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(AugmentRootNode)) as AugmentRootNode);
		base.CreateRootNode();
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x000AB550 File Offset: 0x000A9750
	public override Dictionary<Type, string> GetContextOptions()
	{
		Dictionary<Type, string> dictionary = new Dictionary<Type, string>();
		dictionary.Add(typeof(ModSnippetNode), "Snippet");
		dictionary.Add(typeof(ModPassiveNode), "Passive");
		if (this.Root != null && this.Root.modType == ModType.Fountain)
		{
			dictionary.Add(typeof(AugmentAwardOverrideNode), "Override/Augment Award");
		}
		dictionary.Add(typeof(StatusOverrideNode), "Override/Status Override");
		dictionary.Add(typeof(MovementOverrideNode), "Override/Movement Override");
		dictionary.Add(typeof(KeywordOverrideNode), "Override/Keyword Override");
		dictionary.Add(typeof(WorldOverrideNode), "Override/Game Override");
		dictionary.Add(typeof(ProjectileOverrideNode), "Override/Projectile/- Projectile Override");
		dictionary.Add(typeof(ProjectileMoveProps), "Override/Projectile/Movement/Linear Movement");
		dictionary.Add(typeof(ProjectileMoveProps_Gravity), "Override/Projectile/Movement/Gravity Movement");
		dictionary.Add(typeof(ProjectileMoveProps_Arc), "Override/Projectile/Movement/Arc Movement");
		dictionary.Add(typeof(ProjectileMoveProps_Homing), "Override/Projectile/Movement/Homing Movement");
		dictionary.Add(typeof(ProjectileInteractionNode), "Override/Projectile/Interaction");
		dictionary.Add(typeof(ProjectileLifetimeProps), "Override/Projectile/Lifetime");
		dictionary.Add(typeof(AoEOverrideNode), "Override/AoE/- AoE Override");
		dictionary.Add(typeof(AoEApplicationNode), "Override/AoE/Application");
		dictionary.Add(typeof(SubActionNode), "Override/Effects/Fire Sub-Action");
		dictionary.Add(typeof(ConditionalEffectNode), "Override/Effects/Conditional Effect");
		dictionary.Add(typeof(ActivateKeywordNode), "Override/Effects/Keyword");
		dictionary.Add(typeof(SpawnVFXNode), "Override/Effects/Spawn VFX");
		dictionary.Add(typeof(AddMeshFXNode), "Override/Effects/Spawn MeshFX");
		dictionary.Add(typeof(ApplyForceNode), "Override/Effects/Physics Force");
		dictionary.Add(typeof(ApplyDamageNode), "Override/Effects/Damage");
		dictionary.Add(typeof(HealNode), "Override/Effects/Heal");
		dictionary.Add(typeof(ModifyManaNode), "Override/Effects/Mana");
		dictionary.Add(typeof(CameraEffectNode), "Override/Effects/Camera Effect");
		dictionary.Add(typeof(ApplyStatusNode), "Override/Effects/Add Status Effect");
		dictionary.Add(typeof(RemoveStatusNode), "Override/Effects/Remove Status Effect");
		dictionary.Add(typeof(ModifyCooldownNode), "Override/Effects/Cooldown");
		dictionary.Add(typeof(AudioEffectNode), "Override/Effects/Play Audio Clip");
		dictionary.Add(typeof(AbilityCancelActionNode), "Override/Effects/Cancel Effect");
		dictionary.Add(typeof(CompleteAchievementNode), "Override/Effects/Complete Achievement");
		dictionary.Add(typeof(UpdateStatNode), "Override/Effects/Increment Stat");
		if (this.Root != null && this.Root.modType == ModType.Enemy)
		{
			dictionary.Add(typeof(ModDifficultyNode), "Difficulty Mods");
		}
		return dictionary;
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000AB866 File Offset: 0x000A9A66
	public bool CanUse(EntityControl entity)
	{
		return this.Root.Validate(new EffectProperties(entity));
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x000AB879 File Offset: 0x000A9A79
	public new static GraphTree CreateAndOpenTree(string title)
	{
		return null;
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x000AB87C File Offset: 0x000A9A7C
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x000AB883 File Offset: 0x000A9A83
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x06001BF0 RID: 7152 RVA: 0x000AB88A File Offset: 0x000A9A8A
	public static implicit operator AugmentRootNode(AugmentTree t)
	{
		return t.Root;
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x000AB892 File Offset: 0x000A9A92
	internal override string GetName()
	{
		if (!(this.RootNode == null))
		{
			return this.Root.Name;
		}
		return "-";
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x000AB8B3 File Offset: 0x000A9AB3
	internal override string GetDetail()
	{
		if (!(this.RootNode == null))
		{
			return TextParser.EditorParse(this.Root.Detail);
		}
		return "-";
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x000AB8D9 File Offset: 0x000A9AD9
	public AugmentTree()
	{
	}
}
