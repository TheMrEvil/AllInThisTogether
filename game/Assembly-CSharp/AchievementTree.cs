using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A8 RID: 680
[CreateAssetMenu(order = -5)]
public class AchievementTree : GraphTree
{
	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0009F93D File Offset: 0x0009DB3D
	public AchievementRootNode Root
	{
		get
		{
			return this.RootNode as AchievementRootNode;
		}
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x0009F94A File Offset: 0x0009DB4A
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(AchievementRootNode)) as AchievementRootNode);
		base.CreateRootNode();
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x0009F96D File Offset: 0x0009DB6D
	public override Dictionary<Type, string> GetContextOptions()
	{
		return new Dictionary<Type, string>();
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x0009F974 File Offset: 0x0009DB74
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x0009F97B File Offset: 0x0009DB7B
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x0009F982 File Offset: 0x0009DB82
	public static implicit operator AchievementRootNode(AchievementTree t)
	{
		return t.Root;
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x0009F98A File Offset: 0x0009DB8A
	internal override string GetName()
	{
		if (!(this.RootNode == null))
		{
			return this.Root.Name + " [ " + this.Root.ID + " ]";
		}
		return "-";
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x0009F9C5 File Offset: 0x0009DBC5
	internal override string GetDetail()
	{
		if (!(this.RootNode == null))
		{
			return TextParser.EditorParse(this.Root.Detail);
		}
		return "-";
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x0009F9EB File Offset: 0x0009DBEB
	public AchievementTree()
	{
	}
}
