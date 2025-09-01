using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class GraphTree : ScriptableObject
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0009AD23 File Offset: 0x00098F23
	public string ID
	{
		get
		{
			if (this.RootNode == null)
			{
				return "UNDEFINED";
			}
			return this.RootNode.guid;
		}
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x0009AD44 File Offset: 0x00098F44
	public virtual void CreateRootNode()
	{
		this.VerifyRootNode();
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x0009AD4C File Offset: 0x00098F4C
	public void VerifyRootNode()
	{
		if (this.RootNode != null)
		{
			RootNode rootNode = this.RootNode as RootNode;
			if (rootNode != null && rootNode.tree == null)
			{
				rootNode.tree = this;
			}
		}
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x0009AD8B File Offset: 0x00098F8B
	public Node CreateNode(Type type)
	{
		return null;
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x0009AD8E File Offset: 0x00098F8E
	public void DeleteNode(Node node)
	{
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x0009AD90 File Offset: 0x00098F90
	public void ClearInvalidNodes()
	{
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x0009AD94 File Offset: 0x00098F94
	public GraphTree Clone()
	{
		GraphTree graphTree = ScriptableObject.CreateInstance(base.GetType()) as GraphTree;
		graphTree.name = base.name + " (Runtime)";
		graphTree.RootNode = this.RootNode.Clone(null, true);
		graphTree.nodes = graphTree.RootNode.GetConnectedNodes(null);
		graphTree.isRuntime = true;
		return graphTree;
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x0009ADF3 File Offset: 0x00098FF3
	public void ResetGUIDS()
	{
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x0009ADF5 File Offset: 0x00098FF5
	public static GraphTree CreateAndOpenTree(string title)
	{
		return null;
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x0009ADF8 File Offset: 0x00098FF8
	public virtual Dictionary<Type, string> GetContextOptions()
	{
		return null;
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x0009ADFB File Offset: 0x00098FFB
	public virtual string GetGraphUXML()
	{
		return "";
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x0009AE02 File Offset: 0x00099002
	public virtual string GetNodeUXML()
	{
		return "";
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x0009AE09 File Offset: 0x00099009
	internal virtual string GetName()
	{
		return "Graph";
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x0009AE10 File Offset: 0x00099010
	internal virtual string GetDetail()
	{
		return "";
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x0009AE17 File Offset: 0x00099017
	public GraphTree()
	{
	}

	// Token: 0x040018A6 RID: 6310
	public Node RootNode;

	// Token: 0x040018A7 RID: 6311
	public List<Node> nodes = new List<Node>();

	// Token: 0x040018A8 RID: 6312
	[NonSerialized]
	public bool isRuntime;

	// Token: 0x040018A9 RID: 6313
	[HideInInspector]
	public Vector3 savedViewPoint = new Vector3(300f, 300f, 1f);
}
