using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class Node : ScriptableObject
{
	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06001923 RID: 6435 RVA: 0x0009CCEF File Offset: 0x0009AEEF
	internal virtual bool CanSkipClone
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06001924 RID: 6436 RVA: 0x0009CCF2 File Offset: 0x0009AEF2
	internal GraphTree EditorTreeRef
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
	public virtual Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		if (alreadyCloned == null)
		{
			alreadyCloned = new Dictionary<string, Node>();
		}
		Node result;
		if (alreadyCloned.TryGetValue(this.guid, out result))
		{
			return result;
		}
		if (alreadyCloned.Count > 999)
		{
			Debug.LogError("Too many nodes - recursion issue somewhere");
			return null;
		}
		if (this.CanSkipClone && !fullClone)
		{
			alreadyCloned[this.guid] = this;
			return this;
		}
		Type type = base.GetType();
		FieldInfo[] fields;
		if (!Node._fieldsCache.TryGetValue(type, out fields))
		{
			fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			Node._fieldsCache[type] = fields;
		}
		Node node = ScriptableObject.CreateInstance(type) as Node;
		if (node == null)
		{
			return null;
		}
		node.guid = this.guid;
		alreadyCloned[this.guid] = node;
		foreach (FieldInfo fieldInfo in fields)
		{
			object value = fieldInfo.GetValue(this);
			if (fieldInfo.FieldType == typeof(Node))
			{
				Node node2 = value as Node;
				if (node2 != null && !(node2 is AIRootNode))
				{
					fieldInfo.SetValue(node, node2.Clone(alreadyCloned, fullClone));
				}
			}
			else if (fieldInfo.FieldType == typeof(List<Node>))
			{
				List<Node> list = value as List<Node>;
				if (list != null)
				{
					List<Node> list2 = new List<Node>(list.Count);
					for (int j = 0; j < list.Count; j++)
					{
						Node node3 = list[j];
						if (node3 != null)
						{
							list2.Add(node3.Clone(alreadyCloned, fullClone));
						}
					}
					fieldInfo.SetValue(node, list2);
				}
			}
			else if (fieldInfo.FieldType.IsList())
			{
				Type type2 = fieldInfo.FieldType.GetGenericArguments()[0];
				IList list3 = value as IList;
				if (list3 != null)
				{
					IList list4 = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
					{
						type2
					}));
					for (int k = 0; k < list3.Count; k++)
					{
						object obj = list3[k];
						if (obj != null)
						{
							list4.Add(obj);
						}
					}
					fieldInfo.SetValue(node, list4);
				}
			}
			else
			{
				fieldInfo.SetValue(node, value);
			}
		}
		node.OnCloned();
		return node;
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x0009CF49 File Offset: 0x0009B149
	public virtual void OnCloned()
	{
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x0009CF4C File Offset: 0x0009B14C
	public void FixLists()
	{
		foreach (FieldInfo fieldInfo in base.GetType().GetFields())
		{
			if (fieldInfo.FieldType == typeof(List<Node>))
			{
				List<Node> list = fieldInfo.GetValue(this) as List<Node>;
				if (list != null)
				{
					for (int j = list.Count - 1; j >= 0; j--)
					{
						if (list[j] == null)
						{
							list.RemoveAt(j);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x0009CFCD File Offset: 0x0009B1CD
	public virtual bool InProgress()
	{
		return false;
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x0009CFD0 File Offset: 0x0009B1D0
	public virtual Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps();
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x0009CFD7 File Offset: 0x0009B1D7
	public virtual void OnConnectionsChanged()
	{
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x0009CFDC File Offset: 0x0009B1DC
	public List<Node> GetConnectedNodes(List<Node> nodes = null)
	{
		if (nodes == null)
		{
			nodes = new List<Node>();
		}
		using (List<Node>.Enumerator enumerator = nodes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.guid == this.guid)
				{
					return nodes;
				}
			}
		}
		nodes.Add(this);
		foreach (FieldInfo fieldInfo in base.GetType().GetFields())
		{
			if (fieldInfo.FieldType == typeof(Node))
			{
				Node node = fieldInfo.GetValue(this) as Node;
				if (node != null)
				{
					node.GetConnectedNodes(nodes);
				}
			}
			else if (fieldInfo.FieldType == typeof(List<Node>))
			{
				new List<Node>();
				foreach (Node node2 in (fieldInfo.GetValue(this) as List<Node>))
				{
					if (node2 != null)
					{
						node2.GetConnectedNodes(nodes);
					}
				}
			}
		}
		return nodes;
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x0009D124 File Offset: 0x0009B324
	public void Resort()
	{
		bool sortX = this.GetInspectorProps().SortX;
		foreach (FieldInfo fieldInfo in base.GetType().GetFields())
		{
			if (fieldInfo.FieldType == typeof(List<Node>))
			{
				List<Node> list = fieldInfo.GetValue(this) as List<Node>;
				if (list != null)
				{
					if (sortX)
					{
						list.Sort((Node x, Node y) => x.position.x.CompareTo(y.position.x));
					}
					else
					{
						list.Sort((Node x, Node y) => x.position.y.CompareTo(y.position.y));
					}
				}
			}
		}
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x0009D1DB File Offset: 0x0009B3DB
	public bool IsRootNode()
	{
		return this is RootNode || this is AbilityRootNode || this is AIRootNode;
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x0009D1F8 File Offset: 0x0009B3F8
	public Node()
	{
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x0009D20B File Offset: 0x0009B40B
	// Note: this type is marked as 'beforefieldinit'.
	static Node()
	{
	}

	// Token: 0x0400193D RID: 6461
	private static readonly Dictionary<Type, FieldInfo[]> _fieldsCache = new Dictionary<Type, FieldInfo[]>();

	// Token: 0x0400193E RID: 6462
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(Node), true, "", PortLocation.Default)]
	public Node CalledFrom;

	// Token: 0x0400193F RID: 6463
	[HideInInspector]
	public Vector3 position;

	// Token: 0x04001940 RID: 6464
	[HideInInspector]
	public string guid;

	// Token: 0x04001941 RID: 6465
	[HideInInspector]
	public string titleOverride = "";

	// Token: 0x04001942 RID: 6466
	[NonSerialized]
	public NodeState EditorStateDisplay;

	// Token: 0x04001943 RID: 6467
	[HideInInspector]
	[SerializeField]
	public Node RootNode;

	// Token: 0x0200063B RID: 1595
	public class InspectorProps
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x000D68D1 File Offset: 0x000D4AD1
		public InspectorProps()
		{
		}

		// Token: 0x04002A8E RID: 10894
		public string Title = "UNDEFINED";

		// Token: 0x04002A8F RID: 10895
		public bool ShowInspectorView = true;

		// Token: 0x04002A90 RID: 10896
		public bool ShowInputNode = true;

		// Token: 0x04002A91 RID: 10897
		public Vector2 MinInspectorSize = Vector2.zero;

		// Token: 0x04002A92 RID: 10898
		public Vector2 MaxInspectorSize = Vector2.zero;

		// Token: 0x04002A93 RID: 10899
		public bool SortX;

		// Token: 0x04002A94 RID: 10900
		public bool AllowMultipleInputs;
	}

	// Token: 0x0200063C RID: 1596
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600279E RID: 10142 RVA: 0x000D6908 File Offset: 0x000D4B08
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000D6914 File Offset: 0x000D4B14
		public <>c()
		{
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000D691C File Offset: 0x000D4B1C
		internal int <Resort>b__19_0(Node x, Node y)
		{
			return x.position.x.CompareTo(y.position.x);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000D6939 File Offset: 0x000D4B39
		internal int <Resort>b__19_1(Node x, Node y)
		{
			return x.position.y.CompareTo(y.position.y);
		}

		// Token: 0x04002A95 RID: 10901
		public static readonly Node.<>c <>9 = new Node.<>c();

		// Token: 0x04002A96 RID: 10902
		public static Comparison<Node> <>9__19_0;

		// Token: 0x04002A97 RID: 10903
		public static Comparison<Node> <>9__19_1;
	}
}
