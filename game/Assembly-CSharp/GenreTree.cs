using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000363 RID: 867
[CreateAssetMenu(order = -5)]
public class GenreTree : GraphTree
{
	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x000AFE13 File Offset: 0x000AE013
	public GenreRootNode Root
	{
		get
		{
			if (this.RootNode == null)
			{
				return null;
			}
			return this.RootNode as GenreRootNode;
		}
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x000AFE30 File Offset: 0x000AE030
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(GenreRootNode)) as GenreRootNode);
		base.CreateRootNode();
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x000AFE54 File Offset: 0x000AE054
	public override Dictionary<Type, string> GetContextOptions()
	{
		return new Dictionary<Type, string>
		{
			{
				typeof(GenreWaveNode),
				"Wave"
			},
			{
				typeof(GenreSpawnNode),
				"Spawn Config"
			},
			{
				typeof(GenreSpawnGroupNode),
				"Spawn Group"
			},
			{
				typeof(GenreFountainNode),
				"Fountain"
			},
			{
				typeof(GenreRewardNode),
				"Wave Rewards"
			},
			{
				typeof(GenreRewardOptionNode),
				"Augment Filter"
			},
			{
				typeof(GenreMapNode),
				"Map Filter"
			},
			{
				typeof(GenreVignetteNode),
				"Vignette Filter"
			},
			{
				typeof(GenreWorldNode),
				"World Options"
			}
		};
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x000AFF23 File Offset: 0x000AE123
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x000AFF2A File Offset: 0x000AE12A
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x000AFF31 File Offset: 0x000AE131
	public static implicit operator GenreRootNode(GenreTree t)
	{
		return t.RootNode as GenreRootNode;
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x000AFF3E File Offset: 0x000AE13E
	internal override string GetName()
	{
		return this.Root.ShortName;
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x000AFF4B File Offset: 0x000AE14B
	internal override string GetDetail()
	{
		return base.ID;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x000AFF53 File Offset: 0x000AE153
	public GenreTree()
	{
	}
}
