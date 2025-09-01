using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld.Debugger;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000350 RID: 848
	internal class ActionFrame : IStackFrame
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000DAB93 File Offset: 0x000D8D93
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x000DAB9B File Offset: 0x000D8D9B
		internal PrefixQName CalulatedName
		{
			get
			{
				return this.calulatedName;
			}
			set
			{
				this.calulatedName = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000DABA4 File Offset: 0x000D8DA4
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x000DABAC File Offset: 0x000D8DAC
		internal string StoredOutput
		{
			get
			{
				return this.storedOutput;
			}
			set
			{
				this.storedOutput = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x000DABB5 File Offset: 0x000D8DB5
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x000DABBD File Offset: 0x000D8DBD
		internal int State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000DABC6 File Offset: 0x000D8DC6
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x000DABCE File Offset: 0x000D8DCE
		internal int Counter
		{
			get
			{
				return this.counter;
			}
			set
			{
				this.counter = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000DABD7 File Offset: 0x000D8DD7
		internal ActionFrame Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000DABDF File Offset: 0x000D8DDF
		internal XPathNavigator Node
		{
			get
			{
				if (this.nodeSet != null)
				{
					return this.nodeSet.Current;
				}
				return null;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000DABF6 File Offset: 0x000D8DF6
		internal XPathNodeIterator NodeSet
		{
			get
			{
				return this.nodeSet;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000DABFE File Offset: 0x000D8DFE
		internal XPathNodeIterator NewNodeSet
		{
			get
			{
				return this.newNodeSet;
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000DAC08 File Offset: 0x000D8E08
		internal int IncrementCounter()
		{
			int result = this.counter + 1;
			this.counter = result;
			return result;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000DAC26 File Offset: 0x000D8E26
		internal void AllocateVariables(int count)
		{
			if (0 < count)
			{
				this.variables = new object[count];
				return;
			}
			this.variables = null;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000DAC40 File Offset: 0x000D8E40
		internal object GetVariable(int index)
		{
			return this.variables[index];
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000DAC4A File Offset: 0x000D8E4A
		internal void SetVariable(int index, object value)
		{
			this.variables[index] = value;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000DAC55 File Offset: 0x000D8E55
		internal void SetParameter(XmlQualifiedName name, object value)
		{
			if (this.withParams == null)
			{
				this.withParams = new Hashtable();
			}
			this.withParams[name] = value;
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000DAC77 File Offset: 0x000D8E77
		internal void ResetParams()
		{
			if (this.withParams != null)
			{
				this.withParams.Clear();
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000DAC8C File Offset: 0x000D8E8C
		internal object GetParameter(XmlQualifiedName name)
		{
			if (this.withParams != null)
			{
				return this.withParams[name];
			}
			return null;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000DACA4 File Offset: 0x000D8EA4
		internal void InitNodeSet(XPathNodeIterator nodeSet)
		{
			this.nodeSet = nodeSet;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000DACAD File Offset: 0x000D8EAD
		internal void InitNewNodeSet(XPathNodeIterator nodeSet)
		{
			this.newNodeSet = nodeSet;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000DACB8 File Offset: 0x000D8EB8
		internal void SortNewNodeSet(Processor proc, ArrayList sortarray)
		{
			int count = sortarray.Count;
			XPathSortComparer xpathSortComparer = new XPathSortComparer(count);
			for (int i = 0; i < count; i++)
			{
				Sort sort = (Sort)sortarray[i];
				Query compiledQuery = proc.GetCompiledQuery(sort.select);
				xpathSortComparer.AddSort(compiledQuery, new XPathComparerHelper(sort.order, sort.caseOrder, sort.lang, sort.dataType));
			}
			List<SortKey> list = new List<SortKey>();
			while (this.NewNextNode(proc))
			{
				XPathNodeIterator xpathNodeIterator = this.nodeSet;
				this.nodeSet = this.newNodeSet;
				SortKey sortKey = new SortKey(count, list.Count, this.newNodeSet.Current.Clone());
				for (int j = 0; j < count; j++)
				{
					sortKey[j] = xpathSortComparer.Expression(j).Evaluate(this.newNodeSet);
				}
				list.Add(sortKey);
				this.nodeSet = xpathNodeIterator;
			}
			list.Sort(xpathSortComparer);
			this.newNodeSet = new ActionFrame.XPathSortArrayIterator(list);
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000DADB7 File Offset: 0x000D8FB7
		internal void Finished()
		{
			this.State = -1;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000DADC0 File Offset: 0x000D8FC0
		internal void Inherit(ActionFrame parent)
		{
			this.variables = parent.variables;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000DADCE File Offset: 0x000D8FCE
		private void Init(Action action, ActionFrame container, XPathNodeIterator nodeSet)
		{
			this.state = 0;
			this.action = action;
			this.container = container;
			this.currentAction = 0;
			this.nodeSet = nodeSet;
			this.newNodeSet = null;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000DADFA File Offset: 0x000D8FFA
		internal void Init(Action action, XPathNodeIterator nodeSet)
		{
			this.Init(action, null, nodeSet);
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000DAE05 File Offset: 0x000D9005
		internal void Init(ActionFrame containerFrame, XPathNodeIterator nodeSet)
		{
			this.Init(containerFrame.GetAction(0), containerFrame, nodeSet);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000DAE16 File Offset: 0x000D9016
		internal void SetAction(Action action)
		{
			this.SetAction(action, 0);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000DAE20 File Offset: 0x000D9020
		internal void SetAction(Action action, int state)
		{
			this.action = action;
			this.state = state;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000DAE30 File Offset: 0x000D9030
		private Action GetAction(int actionIndex)
		{
			return ((ContainerAction)this.action).GetAction(actionIndex);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000DAE43 File Offset: 0x000D9043
		internal void Exit()
		{
			this.Finished();
			this.container = null;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000DAE54 File Offset: 0x000D9054
		internal bool Execute(Processor processor)
		{
			if (this.action == null)
			{
				return true;
			}
			this.action.Execute(processor, this);
			if (this.State == -1)
			{
				if (this.container != null)
				{
					this.currentAction++;
					this.action = this.container.GetAction(this.currentAction);
					this.State = 0;
				}
				else
				{
					this.action = null;
				}
				return this.action == null;
			}
			return false;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000DAECC File Offset: 0x000D90CC
		internal bool NextNode(Processor proc)
		{
			bool flag = this.nodeSet.MoveNext();
			if (flag && proc.Stylesheet.Whitespace)
			{
				XPathNodeType nodeType = this.nodeSet.Current.NodeType;
				if (nodeType == XPathNodeType.Whitespace)
				{
					XPathNavigator xpathNavigator = this.nodeSet.Current.Clone();
					bool flag2;
					do
					{
						xpathNavigator.MoveTo(this.nodeSet.Current);
						xpathNavigator.MoveToParent();
						flag2 = (!proc.Stylesheet.PreserveWhiteSpace(proc, xpathNavigator) && (flag = this.nodeSet.MoveNext()));
						nodeType = this.nodeSet.Current.NodeType;
					}
					while (flag2 && nodeType == XPathNodeType.Whitespace);
				}
			}
			return flag;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000DAF70 File Offset: 0x000D9170
		internal bool NewNextNode(Processor proc)
		{
			bool flag = this.newNodeSet.MoveNext();
			if (flag && proc.Stylesheet.Whitespace)
			{
				XPathNodeType nodeType = this.newNodeSet.Current.NodeType;
				if (nodeType == XPathNodeType.Whitespace)
				{
					XPathNavigator xpathNavigator = this.newNodeSet.Current.Clone();
					bool flag2;
					do
					{
						xpathNavigator.MoveTo(this.newNodeSet.Current);
						xpathNavigator.MoveToParent();
						flag2 = (!proc.Stylesheet.PreserveWhiteSpace(proc, xpathNavigator) && (flag = this.newNodeSet.MoveNext()));
						nodeType = this.newNodeSet.Current.NodeType;
					}
					while (flag2 && nodeType == XPathNodeType.Whitespace);
				}
			}
			return flag;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000DB014 File Offset: 0x000D9214
		XPathNavigator IStackFrame.Instruction
		{
			get
			{
				if (this.action == null)
				{
					return null;
				}
				return this.action.GetDbgData(this).StyleSheet;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000DB031 File Offset: 0x000D9231
		XPathNodeIterator IStackFrame.NodeSet
		{
			get
			{
				return this.nodeSet.Clone();
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000DB03E File Offset: 0x000D923E
		int IStackFrame.GetVariablesCount()
		{
			if (this.action == null)
			{
				return 0;
			}
			return this.action.GetDbgData(this).Variables.Length;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000DB05D File Offset: 0x000D925D
		XPathNavigator IStackFrame.GetVariable(int varIndex)
		{
			return this.action.GetDbgData(this).Variables[varIndex].GetDbgData(null).StyleSheet;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000DB07D File Offset: 0x000D927D
		object IStackFrame.GetVariableValue(int varIndex)
		{
			return this.GetVariable(this.action.GetDbgData(this).Variables[varIndex].VarKey);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0000216B File Offset: 0x0000036B
		public ActionFrame()
		{
		}

		// Token: 0x04001C61 RID: 7265
		private int state;

		// Token: 0x04001C62 RID: 7266
		private int counter;

		// Token: 0x04001C63 RID: 7267
		private object[] variables;

		// Token: 0x04001C64 RID: 7268
		private Hashtable withParams;

		// Token: 0x04001C65 RID: 7269
		private Action action;

		// Token: 0x04001C66 RID: 7270
		private ActionFrame container;

		// Token: 0x04001C67 RID: 7271
		private int currentAction;

		// Token: 0x04001C68 RID: 7272
		private XPathNodeIterator nodeSet;

		// Token: 0x04001C69 RID: 7273
		private XPathNodeIterator newNodeSet;

		// Token: 0x04001C6A RID: 7274
		private PrefixQName calulatedName;

		// Token: 0x04001C6B RID: 7275
		private string storedOutput;

		// Token: 0x02000351 RID: 849
		private class XPathSortArrayIterator : XPathArrayIterator
		{
			// Token: 0x0600232C RID: 9004 RVA: 0x000DB09D File Offset: 0x000D929D
			public XPathSortArrayIterator(List<SortKey> list) : base(list)
			{
			}

			// Token: 0x0600232D RID: 9005 RVA: 0x000DB0A6 File Offset: 0x000D92A6
			public XPathSortArrayIterator(ActionFrame.XPathSortArrayIterator it) : base(it)
			{
			}

			// Token: 0x0600232E RID: 9006 RVA: 0x000DB0AF File Offset: 0x000D92AF
			public override XPathNodeIterator Clone()
			{
				return new ActionFrame.XPathSortArrayIterator(this);
			}

			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x0600232F RID: 9007 RVA: 0x000DB0B7 File Offset: 0x000D92B7
			public override XPathNavigator Current
			{
				get
				{
					return ((SortKey)this.list[this.index - 1]).Node;
				}
			}
		}
	}
}
