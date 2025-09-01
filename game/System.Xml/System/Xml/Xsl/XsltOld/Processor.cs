using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl.XsltOld.Debugger;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200039E RID: 926
	internal sealed class Processor : IXsltProcessor
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000E301C File Offset: 0x000E121C
		internal XPathNavigator Current
		{
			get
			{
				ActionFrame actionFrame = (ActionFrame)this.actionStack.Peek();
				if (actionFrame == null)
				{
					return null;
				}
				return actionFrame.Node;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000E3045 File Offset: 0x000E1245
		// (set) Token: 0x0600257B RID: 9595 RVA: 0x000E304D File Offset: 0x000E124D
		internal Processor.ExecResult ExecutionResult
		{
			get
			{
				return this.execResult;
			}
			set
			{
				this.execResult = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x000E3056 File Offset: 0x000E1256
		internal Stylesheet Stylesheet
		{
			get
			{
				return this.stylesheet;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x000E305E File Offset: 0x000E125E
		internal XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x000E3066 File Offset: 0x000E1266
		internal ArrayList SortArray
		{
			get
			{
				return this.sortArray;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x000E306E File Offset: 0x000E126E
		internal Key[] KeyList
		{
			get
			{
				return this.keyList;
			}
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000E3078 File Offset: 0x000E1278
		internal XPathNavigator GetNavigator(Uri ruri)
		{
			XPathNavigator xpathNavigator;
			if (this.documentCache != null)
			{
				xpathNavigator = (this.documentCache[ruri] as XPathNavigator);
				if (xpathNavigator != null)
				{
					return xpathNavigator.Clone();
				}
			}
			else
			{
				this.documentCache = new Hashtable();
			}
			object entity = this.resolver.GetEntity(ruri, null, null);
			if (entity is Stream)
			{
				xpathNavigator = ((IXPathNavigable)Compiler.LoadDocument(new XmlTextReaderImpl(ruri.ToString(), (Stream)entity)
				{
					XmlResolver = this.resolver
				})).CreateNavigator();
			}
			else
			{
				if (!(entity is XPathNavigator))
				{
					throw XsltException.Create("Cannot resolve the referenced document '{0}'.", new string[]
					{
						ruri.ToString()
					});
				}
				xpathNavigator = (XPathNavigator)entity;
			}
			this.documentCache[ruri] = xpathNavigator.Clone();
			return xpathNavigator;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000E3135 File Offset: 0x000E1335
		internal void AddSort(Sort sortinfo)
		{
			this.sortArray.Add(sortinfo);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000E3144 File Offset: 0x000E1344
		internal void InitSortArray()
		{
			if (this.sortArray == null)
			{
				this.sortArray = new ArrayList();
				return;
			}
			this.sortArray.Clear();
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000E3168 File Offset: 0x000E1368
		internal object GetGlobalParameter(XmlQualifiedName qname)
		{
			object obj = this.args.GetParam(qname.Name, qname.Namespace);
			if (obj == null)
			{
				return null;
			}
			if (!(obj is XPathNodeIterator) && !(obj is XPathNavigator) && !(obj is bool) && !(obj is double) && !(obj is string))
			{
				if (obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is decimal)
				{
					obj = XmlConvert.ToXPathDouble(obj);
				}
				else
				{
					obj = obj.ToString();
				}
			}
			return obj;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000E3210 File Offset: 0x000E1410
		internal object GetExtensionObject(string nsUri)
		{
			return this.args.GetExtensionObject(nsUri);
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000E321E File Offset: 0x000E141E
		internal object GetScriptObject(string nsUri)
		{
			return this.scriptExtensions[nsUri];
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x000E322C File Offset: 0x000E142C
		internal RootAction RootAction
		{
			get
			{
				return this.rootAction;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x000E3234 File Offset: 0x000E1434
		internal XPathNavigator Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000E323C File Offset: 0x000E143C
		internal StringBuilder GetSharedStringBuilder()
		{
			if (this.sharedStringBuilder == null)
			{
				this.sharedStringBuilder = new StringBuilder();
			}
			else
			{
				this.sharedStringBuilder.Length = 0;
			}
			return this.sharedStringBuilder;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x0000B528 File Offset: 0x00009728
		internal void ReleaseSharedStringBuilder()
		{
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000E3265 File Offset: 0x000E1465
		internal ArrayList NumberList
		{
			get
			{
				if (this.numberList == null)
				{
					this.numberList = new ArrayList();
				}
				return this.numberList;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000E3280 File Offset: 0x000E1480
		internal IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x000E3288 File Offset: 0x000E1488
		internal HWStack ActionStack
		{
			get
			{
				return this.actionStack;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000E3290 File Offset: 0x000E1490
		internal RecordBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000E3298 File Offset: 0x000E1498
		internal XsltOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000E32A0 File Offset: 0x000E14A0
		public Processor(XPathNavigator doc, XsltArgumentList args, XmlResolver resolver, Stylesheet stylesheet, List<TheQuery> queryStore, RootAction rootAction, IXsltDebugger debugger)
		{
			this.stylesheet = stylesheet;
			this.queryStore = queryStore;
			this.rootAction = rootAction;
			this.queryList = new Query[queryStore.Count];
			for (int i = 0; i < queryStore.Count; i++)
			{
				this.queryList[i] = Query.Clone(queryStore[i].CompiledQuery.QueryTree);
			}
			this.xsm = new StateMachine();
			this.document = doc;
			this.builder = null;
			this.actionStack = new HWStack(10);
			this.output = this.rootAction.Output;
			this.permissions = this.rootAction.permissions;
			this.resolver = (resolver ?? XmlNullResolver.Singleton);
			this.args = (args ?? new XsltArgumentList());
			this.debugger = debugger;
			if (this.debugger != null)
			{
				this.debuggerStack = new HWStack(10, 1000);
				this.templateLookup = new TemplateLookupActionDbg();
			}
			if (this.rootAction.KeyList != null)
			{
				this.keyList = new Key[this.rootAction.KeyList.Count];
				for (int j = 0; j < this.keyList.Length; j++)
				{
					this.keyList[j] = this.rootAction.KeyList[j].Clone();
				}
			}
			this.scriptExtensions = new Hashtable(this.stylesheet.ScriptObjectTypes.Count);
			foreach (object obj in this.stylesheet.ScriptObjectTypes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (this.GetExtensionObject(text) != null)
				{
					throw XsltException.Create("Namespace '{0}' has a duplicate implementation.", new string[]
					{
						text
					});
				}
				this.scriptExtensions.Add(text, Activator.CreateInstance((Type)dictionaryEntry.Value, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null));
			}
			this.PushActionFrame(this.rootAction, null);
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000E34DC File Offset: 0x000E16DC
		public ReaderOutput StartReader()
		{
			ReaderOutput result = new ReaderOutput(this);
			this.builder = new RecordBuilder(result, this.nameTable);
			return result;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000E3504 File Offset: 0x000E1704
		public void Execute(Stream stream)
		{
			RecordOutput recordOutput = null;
			switch (this.output.Method)
			{
			case XsltOutput.OutputMethod.Xml:
			case XsltOutput.OutputMethod.Html:
			case XsltOutput.OutputMethod.Other:
			case XsltOutput.OutputMethod.Unknown:
				recordOutput = new TextOutput(this, stream);
				break;
			case XsltOutput.OutputMethod.Text:
				recordOutput = new TextOnlyOutput(this, stream);
				break;
			}
			this.builder = new RecordBuilder(recordOutput, this.nameTable);
			this.Execute();
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000E3568 File Offset: 0x000E1768
		public void Execute(TextWriter writer)
		{
			RecordOutput recordOutput = null;
			switch (this.output.Method)
			{
			case XsltOutput.OutputMethod.Xml:
			case XsltOutput.OutputMethod.Html:
			case XsltOutput.OutputMethod.Other:
			case XsltOutput.OutputMethod.Unknown:
				recordOutput = new TextOutput(this, writer);
				break;
			case XsltOutput.OutputMethod.Text:
				recordOutput = new TextOnlyOutput(this, writer);
				break;
			}
			this.builder = new RecordBuilder(recordOutput, this.nameTable);
			this.Execute();
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000E35C9 File Offset: 0x000E17C9
		public void Execute(XmlWriter writer)
		{
			this.builder = new RecordBuilder(new WriterOutput(this, writer), this.nameTable);
			this.Execute();
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000E35EC File Offset: 0x000E17EC
		internal void Execute()
		{
			while (this.execResult == Processor.ExecResult.Continue)
			{
				ActionFrame actionFrame = (ActionFrame)this.actionStack.Peek();
				if (actionFrame == null)
				{
					this.builder.TheEnd();
					this.ExecutionResult = Processor.ExecResult.Done;
					break;
				}
				if (actionFrame.Execute(this))
				{
					this.actionStack.Pop();
				}
			}
			if (this.execResult == Processor.ExecResult.Interrupt)
			{
				this.execResult = Processor.ExecResult.Continue;
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000E3650 File Offset: 0x000E1850
		internal ActionFrame PushNewFrame()
		{
			ActionFrame actionFrame = (ActionFrame)this.actionStack.Peek();
			ActionFrame actionFrame2 = (ActionFrame)this.actionStack.Push();
			if (actionFrame2 == null)
			{
				actionFrame2 = new ActionFrame();
				this.actionStack.AddToTop(actionFrame2);
			}
			if (actionFrame != null)
			{
				actionFrame2.Inherit(actionFrame);
			}
			return actionFrame2;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000E369F File Offset: 0x000E189F
		internal void PushActionFrame(Action action, XPathNodeIterator nodeSet)
		{
			this.PushNewFrame().Init(action, nodeSet);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000E36AE File Offset: 0x000E18AE
		internal void PushActionFrame(ActionFrame container)
		{
			this.PushActionFrame(container, container.NodeSet);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000E36BD File Offset: 0x000E18BD
		internal void PushActionFrame(ActionFrame container, XPathNodeIterator nodeSet)
		{
			this.PushNewFrame().Init(container, nodeSet);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000E36CC File Offset: 0x000E18CC
		internal void PushTemplateLookup(XPathNodeIterator nodeSet, XmlQualifiedName mode, Stylesheet importsOf)
		{
			this.templateLookup.Initialize(mode, importsOf);
			this.PushActionFrame(this.templateLookup, nodeSet);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000E36E8 File Offset: 0x000E18E8
		internal string GetQueryExpression(int key)
		{
			return this.queryStore[key].CompiledQuery.Expression;
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000E3700 File Offset: 0x000E1900
		internal Query GetCompiledQuery(int key)
		{
			TheQuery theQuery = this.queryStore[key];
			theQuery.CompiledQuery.CheckErrors();
			Query query = Query.Clone(this.queryList[key]);
			query.SetXsltContext(new XsltCompileContext(theQuery._ScopeManager, this));
			return query;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000E3744 File Offset: 0x000E1944
		internal Query GetValueQuery(int key)
		{
			return this.GetValueQuery(key, null);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000E3750 File Offset: 0x000E1950
		internal Query GetValueQuery(int key, XsltCompileContext context)
		{
			TheQuery theQuery = this.queryStore[key];
			theQuery.CompiledQuery.CheckErrors();
			Query query = this.queryList[key];
			if (context == null)
			{
				context = new XsltCompileContext(theQuery._ScopeManager, this);
			}
			else
			{
				context.Reinitialize(theQuery._ScopeManager, this);
			}
			query.SetXsltContext(context);
			return query;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000E37A4 File Offset: 0x000E19A4
		private XsltCompileContext GetValueOfContext()
		{
			if (this.valueOfContext == null)
			{
				this.valueOfContext = new XsltCompileContext();
			}
			return this.valueOfContext;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000E37BF File Offset: 0x000E19BF
		[Conditional("DEBUG")]
		private void RecycleValueOfContext()
		{
			if (this.valueOfContext != null)
			{
				this.valueOfContext.Recycle();
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000E37D4 File Offset: 0x000E19D4
		private XsltCompileContext GetMatchesContext()
		{
			if (this.matchesContext == null)
			{
				this.matchesContext = new XsltCompileContext();
			}
			return this.matchesContext;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000E37EF File Offset: 0x000E19EF
		[Conditional("DEBUG")]
		private void RecycleMatchesContext()
		{
			if (this.matchesContext != null)
			{
				this.matchesContext.Recycle();
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000E3804 File Offset: 0x000E1A04
		internal string ValueOf(ActionFrame context, int key)
		{
			Query valueQuery = this.GetValueQuery(key, this.GetValueOfContext());
			object obj = valueQuery.Evaluate(context.NodeSet);
			string result;
			if (obj is XPathNodeIterator)
			{
				XPathNavigator xpathNavigator = valueQuery.Advance();
				result = ((xpathNavigator != null) ? this.ValueOf(xpathNavigator) : string.Empty);
			}
			else
			{
				result = XmlConvert.ToXPathString(obj);
			}
			return result;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000E3858 File Offset: 0x000E1A58
		internal string ValueOf(XPathNavigator n)
		{
			if (this.stylesheet.Whitespace && n.NodeType == XPathNodeType.Element)
			{
				StringBuilder stringBuilder = this.GetSharedStringBuilder();
				this.ElementValueWithoutWS(n, stringBuilder);
				this.ReleaseSharedStringBuilder();
				return stringBuilder.ToString();
			}
			return n.Value;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000E38A0 File Offset: 0x000E1AA0
		private void ElementValueWithoutWS(XPathNavigator nav, StringBuilder builder)
		{
			bool flag = this.Stylesheet.PreserveWhiteSpace(this, nav);
			if (nav.MoveToFirstChild())
			{
				do
				{
					switch (nav.NodeType)
					{
					case XPathNodeType.Element:
						this.ElementValueWithoutWS(nav, builder);
						break;
					case XPathNodeType.Text:
					case XPathNodeType.SignificantWhitespace:
						builder.Append(nav.Value);
						break;
					case XPathNodeType.Whitespace:
						if (flag)
						{
							builder.Append(nav.Value);
						}
						break;
					}
				}
				while (nav.MoveToNext());
				nav.MoveToParent();
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000E3924 File Offset: 0x000E1B24
		internal XPathNodeIterator StartQuery(XPathNodeIterator context, int key)
		{
			Query compiledQuery = this.GetCompiledQuery(key);
			if (compiledQuery.Evaluate(context) is XPathNodeIterator)
			{
				return new XPathSelectionIterator(context.Current, compiledQuery);
			}
			throw XsltException.Create("Expression must evaluate to a node-set.", Array.Empty<string>());
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000E3963 File Offset: 0x000E1B63
		internal object Evaluate(ActionFrame context, int key)
		{
			return this.GetValueQuery(key).Evaluate(context.NodeSet);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000E3978 File Offset: 0x000E1B78
		internal object RunQuery(ActionFrame context, int key)
		{
			object obj = this.GetCompiledQuery(key).Evaluate(context.NodeSet);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				return new XPathArrayIterator(xpathNodeIterator);
			}
			return obj;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000E39AC File Offset: 0x000E1BAC
		internal string EvaluateString(ActionFrame context, int key)
		{
			object obj = this.Evaluate(context, key);
			string text = null;
			if (obj != null)
			{
				text = XmlConvert.ToXPathString(obj);
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000E39D8 File Offset: 0x000E1BD8
		internal bool EvaluateBoolean(ActionFrame context, int key)
		{
			object obj = this.Evaluate(context, key);
			if (obj == null)
			{
				return false;
			}
			XPathNavigator xpathNavigator = obj as XPathNavigator;
			if (xpathNavigator == null)
			{
				return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
			}
			return Convert.ToBoolean(xpathNavigator.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000E3A1C File Offset: 0x000E1C1C
		internal bool Matches(XPathNavigator context, int key)
		{
			Query valueQuery = this.GetValueQuery(key, this.GetMatchesContext());
			bool result;
			try
			{
				result = (valueQuery.MatchNode(context) != null);
			}
			catch (XPathException)
			{
				throw XsltException.Create("'{0}' is an invalid XSLT pattern.", new string[]
				{
					this.GetQueryExpression(key)
				});
			}
			return result;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x000E3A74 File Offset: 0x000E1C74
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000E3A7C File Offset: 0x000E1C7C
		internal bool CanContinue
		{
			get
			{
				return this.execResult == Processor.ExecResult.Continue;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x000E3A87 File Offset: 0x000E1C87
		internal bool ExecutionDone
		{
			get
			{
				return this.execResult == Processor.ExecResult.Done;
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000E3A92 File Offset: 0x000E1C92
		internal void ResetOutput()
		{
			this.builder.Reset();
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000E3A9F File Offset: 0x000E1C9F
		internal bool BeginEvent(XPathNodeType nodeType, string prefix, string name, string nspace, bool empty)
		{
			return this.BeginEvent(nodeType, prefix, name, nspace, empty, null, true);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000E3AB0 File Offset: 0x000E1CB0
		internal bool BeginEvent(XPathNodeType nodeType, string prefix, string name, string nspace, bool empty, object htmlProps, bool search)
		{
			int num = this.xsm.BeginOutlook(nodeType);
			if (this.ignoreLevel > 0 || num == 16)
			{
				this.ignoreLevel++;
				return true;
			}
			switch (this.builder.BeginEvent(num, nodeType, prefix, name, nspace, empty, htmlProps, search))
			{
			case Processor.OutputResult.Continue:
				this.xsm.Begin(nodeType);
				return true;
			case Processor.OutputResult.Interrupt:
				this.xsm.Begin(nodeType);
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return true;
			case Processor.OutputResult.Overflow:
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return false;
			case Processor.OutputResult.Error:
				this.ignoreLevel++;
				return true;
			case Processor.OutputResult.Ignore:
				return true;
			default:
				return true;
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000E3B5D File Offset: 0x000E1D5D
		internal bool TextEvent(string text)
		{
			return this.TextEvent(text, false);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000E3B68 File Offset: 0x000E1D68
		internal bool TextEvent(string text, bool disableOutputEscaping)
		{
			if (this.ignoreLevel > 0)
			{
				return true;
			}
			int state = this.xsm.BeginOutlook(XPathNodeType.Text);
			switch (this.builder.TextEvent(state, text, disableOutputEscaping))
			{
			case Processor.OutputResult.Continue:
				this.xsm.Begin(XPathNodeType.Text);
				return true;
			case Processor.OutputResult.Interrupt:
				this.xsm.Begin(XPathNodeType.Text);
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return true;
			case Processor.OutputResult.Overflow:
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return false;
			case Processor.OutputResult.Error:
			case Processor.OutputResult.Ignore:
				return true;
			default:
				return true;
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000E3BEC File Offset: 0x000E1DEC
		internal bool EndEvent(XPathNodeType nodeType)
		{
			if (this.ignoreLevel > 0)
			{
				this.ignoreLevel--;
				return true;
			}
			int state = this.xsm.EndOutlook(nodeType);
			switch (this.builder.EndEvent(state, nodeType))
			{
			case Processor.OutputResult.Continue:
				this.xsm.End(nodeType);
				return true;
			case Processor.OutputResult.Interrupt:
				this.xsm.End(nodeType);
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return true;
			case Processor.OutputResult.Overflow:
				this.ExecutionResult = Processor.ExecResult.Interrupt;
				return false;
			}
			return true;
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000E3C78 File Offset: 0x000E1E78
		internal bool CopyBeginEvent(XPathNavigator node, bool emptyflag)
		{
			switch (node.NodeType)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Attribute:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				return this.BeginEvent(node.NodeType, node.Prefix, node.LocalName, node.NamespaceURI, emptyflag);
			case XPathNodeType.Namespace:
				return this.BeginEvent(XPathNodeType.Namespace, null, node.LocalName, node.Value, false);
			}
			return true;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000E3CF4 File Offset: 0x000E1EF4
		internal bool CopyTextEvent(XPathNavigator node)
		{
			switch (node.NodeType)
			{
			case XPathNodeType.Attribute:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
			{
				string value = node.Value;
				return this.TextEvent(value);
			}
			}
			return true;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000E3D48 File Offset: 0x000E1F48
		internal bool CopyEndEvent(XPathNavigator node)
		{
			switch (node.NodeType)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				return this.EndEvent(node.NodeType);
			}
			return true;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000E3D9A File Offset: 0x000E1F9A
		internal static bool IsRoot(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Root)
			{
				return true;
			}
			if (navigator.NodeType == XPathNodeType.Element)
			{
				XPathNavigator xpathNavigator = navigator.Clone();
				xpathNavigator.MoveToRoot();
				return xpathNavigator.IsSamePosition(navigator);
			}
			return false;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000E3DC4 File Offset: 0x000E1FC4
		internal void PushOutput(RecordOutput output)
		{
			this.builder.OutputState = this.xsm.State;
			RecordBuilder next = this.builder;
			this.builder = new RecordBuilder(output, this.nameTable);
			this.builder.Next = next;
			this.xsm.Reset();
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000E3E18 File Offset: 0x000E2018
		internal RecordOutput PopOutput()
		{
			RecordBuilder recordBuilder = this.builder;
			this.builder = recordBuilder.Next;
			this.xsm.State = this.builder.OutputState;
			recordBuilder.TheEnd();
			return recordBuilder.Output;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000E3E5A File Offset: 0x000E205A
		internal bool SetDefaultOutput(XsltOutput.OutputMethod method)
		{
			if (this.Output.Method != method)
			{
				this.output = this.output.CreateDerivedOutput(method);
				return true;
			}
			return false;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000E3E80 File Offset: 0x000E2080
		internal object GetVariableValue(VariableAction variable)
		{
			int varKey = variable.VarKey;
			if (!variable.IsGlobal)
			{
				return ((ActionFrame)this.actionStack.Peek()).GetVariable(varKey);
			}
			ActionFrame actionFrame = (ActionFrame)this.actionStack[0];
			object variable2 = actionFrame.GetVariable(varKey);
			if (variable2 == VariableAction.BeingComputedMark)
			{
				throw XsltException.Create("Circular reference in the definition of variable '{0}'.", new string[]
				{
					variable.NameStr
				});
			}
			if (variable2 != null)
			{
				return variable2;
			}
			int length = this.actionStack.Length;
			ActionFrame actionFrame2 = this.PushNewFrame();
			actionFrame2.Inherit(actionFrame);
			actionFrame2.Init(variable, actionFrame.NodeSet);
			do
			{
				if (((ActionFrame)this.actionStack.Peek()).Execute(this))
				{
					this.actionStack.Pop();
				}
			}
			while (length < this.actionStack.Length);
			return actionFrame.GetVariable(varKey);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000E3F57 File Offset: 0x000E2157
		internal void SetParameter(XmlQualifiedName name, object value)
		{
			((ActionFrame)this.actionStack[this.actionStack.Length - 2]).SetParameter(name, value);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000E3F7D File Offset: 0x000E217D
		internal void ResetParams()
		{
			((ActionFrame)this.actionStack[this.actionStack.Length - 1]).ResetParams();
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000E3FA1 File Offset: 0x000E21A1
		internal object GetParameter(XmlQualifiedName name)
		{
			return ((ActionFrame)this.actionStack[this.actionStack.Length - 3]).GetParameter(name);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000E3FC8 File Offset: 0x000E21C8
		internal void PushDebuggerStack()
		{
			Processor.DebuggerFrame debuggerFrame = (Processor.DebuggerFrame)this.debuggerStack.Push();
			if (debuggerFrame == null)
			{
				debuggerFrame = new Processor.DebuggerFrame();
				this.debuggerStack.AddToTop(debuggerFrame);
			}
			debuggerFrame.actionFrame = (ActionFrame)this.actionStack.Peek();
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000E4011 File Offset: 0x000E2211
		internal void PopDebuggerStack()
		{
			this.debuggerStack.Pop();
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000E401F File Offset: 0x000E221F
		internal void OnInstructionExecute()
		{
			((Processor.DebuggerFrame)this.debuggerStack.Peek()).actionFrame = (ActionFrame)this.actionStack.Peek();
			this.Debugger.OnInstructionExecute(this);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000E4052 File Offset: 0x000E2252
		internal XmlQualifiedName GetPrevioseMode()
		{
			return ((Processor.DebuggerFrame)this.debuggerStack[this.debuggerStack.Length - 2]).currentMode;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000E4076 File Offset: 0x000E2276
		internal void SetCurrentMode(XmlQualifiedName mode)
		{
			((Processor.DebuggerFrame)this.debuggerStack[this.debuggerStack.Length - 1]).currentMode = mode;
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000E409B File Offset: 0x000E229B
		int IXsltProcessor.StackDepth
		{
			get
			{
				return this.debuggerStack.Length;
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000E40A8 File Offset: 0x000E22A8
		IStackFrame IXsltProcessor.GetStackFrame(int depth)
		{
			return ((Processor.DebuggerFrame)this.debuggerStack[depth]).actionFrame;
		}

		// Token: 0x04001D83 RID: 7555
		private const int StackIncrement = 10;

		// Token: 0x04001D84 RID: 7556
		private Processor.ExecResult execResult;

		// Token: 0x04001D85 RID: 7557
		private Stylesheet stylesheet;

		// Token: 0x04001D86 RID: 7558
		private RootAction rootAction;

		// Token: 0x04001D87 RID: 7559
		private Key[] keyList;

		// Token: 0x04001D88 RID: 7560
		private List<TheQuery> queryStore;

		// Token: 0x04001D89 RID: 7561
		public PermissionSet permissions;

		// Token: 0x04001D8A RID: 7562
		private XPathNavigator document;

		// Token: 0x04001D8B RID: 7563
		private HWStack actionStack;

		// Token: 0x04001D8C RID: 7564
		private HWStack debuggerStack;

		// Token: 0x04001D8D RID: 7565
		private StringBuilder sharedStringBuilder;

		// Token: 0x04001D8E RID: 7566
		private int ignoreLevel;

		// Token: 0x04001D8F RID: 7567
		private StateMachine xsm;

		// Token: 0x04001D90 RID: 7568
		private RecordBuilder builder;

		// Token: 0x04001D91 RID: 7569
		private XsltOutput output;

		// Token: 0x04001D92 RID: 7570
		private XmlNameTable nameTable = new NameTable();

		// Token: 0x04001D93 RID: 7571
		private XmlResolver resolver;

		// Token: 0x04001D94 RID: 7572
		private XsltArgumentList args;

		// Token: 0x04001D95 RID: 7573
		private Hashtable scriptExtensions;

		// Token: 0x04001D96 RID: 7574
		private ArrayList numberList;

		// Token: 0x04001D97 RID: 7575
		private TemplateLookupAction templateLookup = new TemplateLookupAction();

		// Token: 0x04001D98 RID: 7576
		private IXsltDebugger debugger;

		// Token: 0x04001D99 RID: 7577
		private Query[] queryList;

		// Token: 0x04001D9A RID: 7578
		private ArrayList sortArray;

		// Token: 0x04001D9B RID: 7579
		private Hashtable documentCache;

		// Token: 0x04001D9C RID: 7580
		private XsltCompileContext valueOfContext;

		// Token: 0x04001D9D RID: 7581
		private XsltCompileContext matchesContext;

		// Token: 0x0200039F RID: 927
		internal enum ExecResult
		{
			// Token: 0x04001D9F RID: 7583
			Continue,
			// Token: 0x04001DA0 RID: 7584
			Interrupt,
			// Token: 0x04001DA1 RID: 7585
			Done
		}

		// Token: 0x020003A0 RID: 928
		internal enum OutputResult
		{
			// Token: 0x04001DA3 RID: 7587
			Continue,
			// Token: 0x04001DA4 RID: 7588
			Interrupt,
			// Token: 0x04001DA5 RID: 7589
			Overflow,
			// Token: 0x04001DA6 RID: 7590
			Error,
			// Token: 0x04001DA7 RID: 7591
			Ignore
		}

		// Token: 0x020003A1 RID: 929
		internal class DebuggerFrame
		{
			// Token: 0x060025C6 RID: 9670 RVA: 0x0000216B File Offset: 0x0000036B
			public DebuggerFrame()
			{
			}

			// Token: 0x04001DA8 RID: 7592
			internal ActionFrame actionFrame;

			// Token: 0x04001DA9 RID: 7593
			internal XmlQualifiedName currentMode;
		}
	}
}
