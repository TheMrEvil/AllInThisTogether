using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Schema;
using System.Xml.Utils;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004B5 RID: 1205
	internal class XmlILVisitor : QilVisitor
	{
		// Token: 0x06002F5F RID: 12127 RVA: 0x00119BF8 File Offset: 0x00117DF8
		public void Visit(QilExpression qil, GenerateHelper helper, MethodInfo methRoot)
		{
			this.qil = qil;
			this.helper = helper;
			this.iterNested = null;
			this.indexId = 0;
			this.PrepareGlobalValues(qil.GlobalParameterList);
			this.PrepareGlobalValues(qil.GlobalVariableList);
			this.VisitGlobalValues(qil.GlobalParameterList);
			this.VisitGlobalValues(qil.GlobalVariableList);
			foreach (QilNode qilNode in qil.FunctionList)
			{
				QilFunction ndFunc = (QilFunction)qilNode;
				this.Function(ndFunc);
			}
			this.helper.MethodBegin(methRoot, null, true);
			this.StartNestedIterator(qil.Root);
			this.Visit(qil.Root);
			this.EndNestedIterator(qil.Root);
			this.helper.MethodEnd();
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x00119CD4 File Offset: 0x00117ED4
		private void PrepareGlobalValues(QilList globalIterators)
		{
			foreach (QilNode qilNode in globalIterators)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				MethodInfo functionBinding = XmlILAnnotation.Write(qilIterator).FunctionBinding;
				IteratorDescriptor iteratorDescriptor = new IteratorDescriptor(this.helper);
				iteratorDescriptor.Storage = StorageDescriptor.Global(functionBinding, this.GetItemStorageType(qilIterator), !qilIterator.XmlType.IsSingleton);
				XmlILAnnotation.Write(qilIterator).CachedIteratorDescriptor = iteratorDescriptor;
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x00119D60 File Offset: 0x00117F60
		private void VisitGlobalValues(QilList globalIterators)
		{
			foreach (QilNode qilNode in globalIterators)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				QilParameter qilParameter = qilIterator as QilParameter;
				MethodInfo globalLocation = XmlILAnnotation.Write(qilIterator).CachedIteratorDescriptor.Storage.GlobalLocation;
				bool isCached = !qilIterator.XmlType.IsSingleton;
				int num = this.helper.StaticData.DeclareGlobalValue(qilIterator.DebugName);
				this.helper.MethodBegin(globalLocation, qilIterator.SourceLine, false);
				Label label = this.helper.DefineLabel();
				Label label2 = this.helper.DefineLabel();
				this.helper.LoadQueryRuntime();
				this.helper.LoadInteger(num);
				this.helper.Call(XmlILMethods.GlobalComputed);
				this.helper.Emit(OpCodes.Brtrue, label);
				this.StartNestedIterator(qilIterator);
				if (qilParameter != null)
				{
					LocalBuilder locBldr = this.helper.DeclareLocal("$$$param", typeof(object));
					this.helper.CallGetParameter(qilParameter.Name.LocalName, qilParameter.Name.NamespaceUri);
					this.helper.Emit(OpCodes.Stloc, locBldr);
					this.helper.Emit(OpCodes.Ldloc, locBldr);
					this.helper.Emit(OpCodes.Brfalse, label2);
					this.helper.LoadQueryRuntime();
					this.helper.LoadInteger(num);
					this.helper.LoadQueryRuntime();
					this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(XmlQueryTypeFactory.ItemS));
					this.helper.Emit(OpCodes.Ldloc, locBldr);
					this.helper.Call(XmlILMethods.ChangeTypeXsltResult);
					this.helper.CallSetGlobalValue(typeof(object));
					this.helper.EmitUnconditionalBranch(OpCodes.Br, label);
				}
				this.helper.MarkLabel(label2);
				if (qilIterator.Binding != null)
				{
					this.helper.LoadQueryRuntime();
					this.helper.LoadInteger(num);
					this.NestedVisitEnsureStack(qilIterator.Binding, this.GetItemStorageType(qilIterator), isCached);
					this.helper.CallSetGlobalValue(this.GetStorageType(qilIterator));
				}
				else
				{
					this.helper.LoadQueryRuntime();
					GenerateHelper generateHelper = this.helper;
					OpCode ldstr = OpCodes.Ldstr;
					string name = "Supplied XsltArgumentList does not contain a parameter with local name '{0}' and namespace '{1}'.";
					object[] args = new string[]
					{
						qilParameter.Name.LocalName,
						qilParameter.Name.NamespaceUri
					};
					generateHelper.Emit(ldstr, Res.GetString(name, args));
					this.helper.Call(XmlILMethods.ThrowException);
				}
				this.EndNestedIterator(qilIterator);
				this.helper.MarkLabel(label);
				this.helper.CallGetGlobalValue(num, this.GetStorageType(qilIterator));
				this.helper.MethodEnd();
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0011A068 File Offset: 0x00118268
		private void Function(QilFunction ndFunc)
		{
			foreach (QilNode qilNode in ndFunc.Arguments)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				IteratorDescriptor iteratorDescriptor = new IteratorDescriptor(this.helper);
				int num = XmlILAnnotation.Write(qilIterator).ArgumentPosition + 1;
				iteratorDescriptor.Storage = StorageDescriptor.Parameter(num, this.GetItemStorageType(qilIterator), !qilIterator.XmlType.IsSingleton);
				XmlILAnnotation.Write(qilIterator).CachedIteratorDescriptor = iteratorDescriptor;
			}
			MethodInfo functionBinding = XmlILAnnotation.Write(ndFunc).FunctionBinding;
			bool flag = XmlILConstructInfo.Read(ndFunc).ConstructMethod == XmlILConstructMethod.Writer;
			this.helper.MethodBegin(functionBinding, ndFunc.SourceLine, flag);
			foreach (QilNode qilNode2 in ndFunc.Arguments)
			{
				QilIterator qilIterator2 = (QilIterator)qilNode2;
				if (this.qil.IsDebug && qilIterator2.SourceLine != null)
				{
					this.helper.DebugSequencePoint(qilIterator2.SourceLine);
				}
				if (qilIterator2.Binding != null)
				{
					int num = (qilIterator2.Annotation as XmlILAnnotation).ArgumentPosition + 1;
					Label label = this.helper.DefineLabel();
					this.helper.LoadQueryRuntime();
					this.helper.LoadParameter(num);
					this.helper.LoadInteger(29);
					this.helper.Call(XmlILMethods.SeqMatchesCode);
					this.helper.Emit(OpCodes.Brfalse, label);
					this.StartNestedIterator(qilIterator2);
					this.NestedVisitEnsureStack(qilIterator2.Binding, this.GetItemStorageType(qilIterator2), !qilIterator2.XmlType.IsSingleton);
					this.EndNestedIterator(qilIterator2);
					this.helper.SetParameter(num);
					this.helper.MarkLabel(label);
				}
			}
			this.StartNestedIterator(ndFunc);
			if (flag)
			{
				this.NestedVisit(ndFunc.Definition);
			}
			else
			{
				this.NestedVisitEnsureStack(ndFunc.Definition, this.GetItemStorageType(ndFunc), !ndFunc.XmlType.IsSingleton);
			}
			this.EndNestedIterator(ndFunc);
			this.helper.MethodEnd();
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0011A2C8 File Offset: 0x001184C8
		protected override QilNode Visit(QilNode nd)
		{
			if (nd == null)
			{
				return null;
			}
			if (this.qil.IsDebug && nd.SourceLine != null && !(nd is QilIterator))
			{
				this.helper.DebugSequencePoint(nd.SourceLine);
			}
			switch (XmlILConstructInfo.Read(nd).ConstructMethod)
			{
			case XmlILConstructMethod.WriterThenIterator:
				this.NestedConstruction(nd);
				return nd;
			case XmlILConstructMethod.IteratorThenWriter:
				this.CopySequence(nd);
				return nd;
			}
			base.Visit(nd);
			return nd;
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x0000206B File Offset: 0x0000026B
		protected override QilNode VisitChildren(QilNode parent)
		{
			return parent;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x0011A347 File Offset: 0x00118547
		private void NestedConstruction(QilNode nd)
		{
			this.helper.CallStartSequenceConstruction();
			base.Visit(nd);
			this.helper.CallEndSequenceConstruction();
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), true);
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x0011A384 File Offset: 0x00118584
		private void CopySequence(QilNode nd)
		{
			XmlQueryType xmlType = nd.XmlType;
			bool hasOnEnd;
			Label lblOnEnd;
			this.StartWriterLoop(nd, out hasOnEnd, out lblOnEnd);
			if (xmlType.IsSingleton)
			{
				this.helper.LoadQueryOutput();
				base.Visit(nd);
				this.iterCurr.EnsureItemStorageType(nd.XmlType, typeof(XPathItem));
			}
			else
			{
				base.Visit(nd);
				this.iterCurr.EnsureItemStorageType(nd.XmlType, typeof(XPathItem));
				this.iterCurr.EnsureNoStackNoCache("$$$copyTemp");
				this.helper.LoadQueryOutput();
			}
			this.iterCurr.EnsureStackNoCache();
			this.helper.Call(XmlILMethods.WriteItem);
			this.EndWriterLoop(nd, hasOnEnd, lblOnEnd);
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x0011A43C File Offset: 0x0011863C
		protected override QilNode VisitDataSource(QilDataSource ndSrc)
		{
			this.helper.LoadQueryContext();
			this.NestedVisitEnsureStack(ndSrc.Name);
			this.NestedVisitEnsureStack(ndSrc.BaseUri);
			this.helper.Call(XmlILMethods.GetDataSource);
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navDoc", typeof(XPathNavigator));
			this.helper.Emit(OpCodes.Stloc, localBuilder);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndSrc;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0011A4F1 File Offset: 0x001186F1
		protected override QilNode VisitNop(QilUnary ndNop)
		{
			return this.Visit(ndNop.Child);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0011A4F1 File Offset: 0x001186F1
		protected override QilNode VisitOptimizeBarrier(QilUnary ndBarrier)
		{
			return this.Visit(ndBarrier.Child);
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0011A500 File Offset: 0x00118700
		protected override QilNode VisitError(QilUnary ndErr)
		{
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndErr.Child);
			this.helper.Call(XmlILMethods.ThrowException);
			if (XmlILConstructInfo.Read(ndErr).ConstructMethod == XmlILConstructMethod.Writer)
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.helper.Emit(OpCodes.Ldnull);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), false);
			}
			return ndErr;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0011A580 File Offset: 0x00118780
		protected override QilNode VisitWarning(QilUnary ndWarning)
		{
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndWarning.Child);
			this.helper.Call(XmlILMethods.SendMessage);
			if (XmlILConstructInfo.Read(ndWarning).ConstructMethod == XmlILConstructMethod.Writer)
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.VisitEmpty(ndWarning);
			}
			return ndWarning;
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0011A5DC File Offset: 0x001187DC
		protected override QilNode VisitTrue(QilNode ndTrue)
		{
			if (this.iterCurr.CurrentBranchingContext != BranchingContext.None)
			{
				this.helper.EmitUnconditionalBranch((this.iterCurr.CurrentBranchingContext == BranchingContext.OnTrue) ? OpCodes.Brtrue : OpCodes.Brfalse, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.helper.LoadBoolean(true);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			return ndTrue;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0011A660 File Offset: 0x00118860
		protected override QilNode VisitFalse(QilNode ndFalse)
		{
			if (this.iterCurr.CurrentBranchingContext != BranchingContext.None)
			{
				this.helper.EmitUnconditionalBranch((this.iterCurr.CurrentBranchingContext == BranchingContext.OnFalse) ? OpCodes.Brtrue : OpCodes.Brfalse, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.helper.LoadBoolean(false);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			return ndFalse;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0011A6E4 File Offset: 0x001188E4
		protected override QilNode VisitLiteralString(QilLiteral ndStr)
		{
			this.helper.Emit(OpCodes.Ldstr, ndStr);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndStr;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0011A718 File Offset: 0x00118918
		protected override QilNode VisitLiteralInt32(QilLiteral ndInt)
		{
			this.helper.LoadInteger(ndInt);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(int), false);
			return ndInt;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0011A747 File Offset: 0x00118947
		protected override QilNode VisitLiteralInt64(QilLiteral ndLong)
		{
			this.helper.Emit(OpCodes.Ldc_I8, ndLong);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(long), false);
			return ndLong;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x0011A77B File Offset: 0x0011897B
		protected override QilNode VisitLiteralDouble(QilLiteral ndDbl)
		{
			this.helper.Emit(OpCodes.Ldc_R8, ndDbl);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(double), false);
			return ndDbl;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0011A7B0 File Offset: 0x001189B0
		protected override QilNode VisitLiteralDecimal(QilLiteral ndDec)
		{
			this.helper.ConstructLiteralDecimal(ndDec);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(decimal), false);
			return ndDec;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0011A7DF File Offset: 0x001189DF
		protected override QilNode VisitLiteralQName(QilName ndQName)
		{
			this.helper.ConstructLiteralQName(ndQName.LocalName, ndQName.NamespaceUri);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XmlQualifiedName), false);
			return ndQName;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x0011A814 File Offset: 0x00118A14
		protected override QilNode VisitAnd(QilBinary ndAnd)
		{
			IteratorDescriptor iteratorDescriptor = this.iterCurr;
			this.StartNestedIterator(ndAnd.Left);
			Label lblOnFalse = this.StartConjunctiveTests(iteratorDescriptor.CurrentBranchingContext, iteratorDescriptor.LabelBranch);
			this.Visit(ndAnd.Left);
			this.EndNestedIterator(ndAnd.Left);
			this.StartNestedIterator(ndAnd.Right);
			this.StartLastConjunctiveTest(iteratorDescriptor.CurrentBranchingContext, iteratorDescriptor.LabelBranch, lblOnFalse);
			this.Visit(ndAnd.Right);
			this.EndNestedIterator(ndAnd.Right);
			this.EndConjunctiveTests(iteratorDescriptor.CurrentBranchingContext, iteratorDescriptor.LabelBranch, lblOnFalse);
			return ndAnd;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x0011A8AC File Offset: 0x00118AAC
		private Label StartConjunctiveTests(BranchingContext brctxt, Label lblBranch)
		{
			if (brctxt == BranchingContext.OnFalse)
			{
				this.iterCurr.SetBranching(BranchingContext.OnFalse, lblBranch);
				return lblBranch;
			}
			Label label = this.helper.DefineLabel();
			this.iterCurr.SetBranching(BranchingContext.OnFalse, label);
			return label;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x0011A8E6 File Offset: 0x00118AE6
		private void StartLastConjunctiveTest(BranchingContext brctxt, Label lblBranch, Label lblOnFalse)
		{
			if (brctxt == BranchingContext.OnTrue)
			{
				this.iterCurr.SetBranching(BranchingContext.OnTrue, lblBranch);
				return;
			}
			this.iterCurr.SetBranching(BranchingContext.OnFalse, lblOnFalse);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0011A908 File Offset: 0x00118B08
		private void EndConjunctiveTests(BranchingContext brctxt, Label lblBranch, Label lblOnFalse)
		{
			switch (brctxt)
			{
			case BranchingContext.None:
				this.helper.ConvBranchToBool(lblOnFalse, false);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
				return;
			case BranchingContext.OnTrue:
				this.helper.MarkLabel(lblOnFalse);
				break;
			case BranchingContext.OnFalse:
				break;
			default:
				return;
			}
			this.iterCurr.Storage = StorageDescriptor.None();
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0011A970 File Offset: 0x00118B70
		protected override QilNode VisitOr(QilBinary ndOr)
		{
			Label label = default(Label);
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			if (currentBranchingContext != BranchingContext.OnTrue)
			{
				if (currentBranchingContext == BranchingContext.OnFalse)
				{
					label = this.helper.DefineLabel();
					this.NestedVisitWithBranch(ndOr.Left, BranchingContext.OnTrue, label);
				}
				else
				{
					label = this.helper.DefineLabel();
					this.NestedVisitWithBranch(ndOr.Left, BranchingContext.OnTrue, label);
				}
			}
			else
			{
				this.NestedVisitWithBranch(ndOr.Left, BranchingContext.OnTrue, this.iterCurr.LabelBranch);
			}
			currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			if (currentBranchingContext != BranchingContext.OnTrue)
			{
				if (currentBranchingContext == BranchingContext.OnFalse)
				{
					this.NestedVisitWithBranch(ndOr.Right, BranchingContext.OnFalse, this.iterCurr.LabelBranch);
				}
				else
				{
					this.NestedVisitWithBranch(ndOr.Right, BranchingContext.OnTrue, label);
				}
			}
			else
			{
				this.NestedVisitWithBranch(ndOr.Right, BranchingContext.OnTrue, this.iterCurr.LabelBranch);
			}
			switch (this.iterCurr.CurrentBranchingContext)
			{
			case BranchingContext.None:
				this.helper.ConvBranchToBool(label, true);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
				return ndOr;
			case BranchingContext.OnTrue:
				break;
			case BranchingContext.OnFalse:
				this.helper.MarkLabel(label);
				break;
			default:
				return ndOr;
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndOr;
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0011AAA8 File Offset: 0x00118CA8
		protected override QilNode VisitNot(QilUnary ndNot)
		{
			Label lblBranch = default(Label);
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			if (currentBranchingContext != BranchingContext.OnTrue)
			{
				if (currentBranchingContext == BranchingContext.OnFalse)
				{
					this.NestedVisitWithBranch(ndNot.Child, BranchingContext.OnTrue, this.iterCurr.LabelBranch);
				}
				else
				{
					lblBranch = this.helper.DefineLabel();
					this.NestedVisitWithBranch(ndNot.Child, BranchingContext.OnTrue, lblBranch);
				}
			}
			else
			{
				this.NestedVisitWithBranch(ndNot.Child, BranchingContext.OnFalse, this.iterCurr.LabelBranch);
			}
			if (this.iterCurr.CurrentBranchingContext == BranchingContext.None)
			{
				this.helper.ConvBranchToBool(lblBranch, false);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			else
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			return ndNot;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x0011AB68 File Offset: 0x00118D68
		protected override QilNode VisitConditional(QilTernary ndCond)
		{
			if (XmlILConstructInfo.Read(ndCond).ConstructMethod == XmlILConstructMethod.Writer)
			{
				Label label = this.helper.DefineLabel();
				this.NestedVisitWithBranch(ndCond.Left, BranchingContext.OnFalse, label);
				this.NestedVisit(ndCond.Center);
				if (ndCond.Right.NodeType == QilNodeType.Sequence && ndCond.Right.Count == 0)
				{
					this.helper.MarkLabel(label);
					this.NestedVisit(ndCond.Right);
				}
				else
				{
					Label label2 = this.helper.DefineLabel();
					this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
					this.helper.MarkLabel(label);
					this.NestedVisit(ndCond.Right);
					this.helper.MarkLabel(label2);
				}
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				LocalBuilder localBuilder = null;
				LocalBuilder localBuilder2 = null;
				Type itemStorageType = this.GetItemStorageType(ndCond);
				Label label3 = this.helper.DefineLabel();
				if (ndCond.XmlType.IsSingleton)
				{
					this.NestedVisitWithBranch(ndCond.Left, BranchingContext.OnFalse, label3);
				}
				else
				{
					localBuilder2 = this.helper.DeclareLocal("$$$cond", itemStorageType);
					localBuilder = this.helper.DeclareLocal("$$$boolResult", typeof(bool));
					this.NestedVisitEnsureLocal(ndCond.Left, localBuilder);
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.Emit(OpCodes.Brfalse, label3);
				}
				this.ConditionalBranch(ndCond.Center, itemStorageType, localBuilder2);
				IteratorDescriptor iteratorDescriptor = this.iterNested;
				Label label4 = this.helper.DefineLabel();
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label4);
				this.helper.MarkLabel(label3);
				this.ConditionalBranch(ndCond.Right, itemStorageType, localBuilder2);
				if (!ndCond.XmlType.IsSingleton)
				{
					this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label4);
					Label label5 = this.helper.DefineLabel();
					this.helper.MarkLabel(label5);
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.Emit(OpCodes.Brtrue, iteratorDescriptor.GetLabelNext());
					this.helper.EmitUnconditionalBranch(OpCodes.Br, this.iterNested.GetLabelNext());
					this.iterCurr.SetIterator(label5, StorageDescriptor.Local(localBuilder2, itemStorageType, false));
				}
				this.helper.MarkLabel(label4);
			}
			return ndCond;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x0011ADC8 File Offset: 0x00118FC8
		private void ConditionalBranch(QilNode ndBranch, Type itemStorageType, LocalBuilder locResult)
		{
			if (locResult != null)
			{
				this.NestedVisit(ndBranch, this.iterCurr.GetLabelNext());
				this.iterCurr.EnsureItemStorageType(ndBranch.XmlType, itemStorageType);
				this.iterCurr.EnsureLocalNoCache(locResult);
				return;
			}
			if (this.iterCurr.IsBranching)
			{
				this.NestedVisitWithBranch(ndBranch, this.iterCurr.CurrentBranchingContext, this.iterCurr.LabelBranch);
				return;
			}
			this.NestedVisitEnsureStack(ndBranch, itemStorageType, false);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0011AE40 File Offset: 0x00119040
		protected override QilNode VisitChoice(QilChoice ndChoice)
		{
			this.NestedVisit(ndChoice.Expression);
			QilNode branches = ndChoice.Branches;
			int num = branches.Count - 1;
			Label[] array = new Label[num];
			int i;
			for (i = 0; i < num; i++)
			{
				array[i] = this.helper.DefineLabel();
			}
			Label label = this.helper.DefineLabel();
			Label label2 = this.helper.DefineLabel();
			this.helper.Emit(OpCodes.Switch, array);
			this.helper.EmitUnconditionalBranch(OpCodes.Br, label);
			for (i = 0; i < num; i++)
			{
				this.helper.MarkLabel(array[i]);
				this.NestedVisit(branches[i]);
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
			}
			this.helper.MarkLabel(label);
			this.NestedVisit(branches[i]);
			this.helper.MarkLabel(label2);
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndChoice;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0011AF4C File Offset: 0x0011914C
		protected override QilNode VisitLength(QilUnary ndSetLen)
		{
			Label label = this.helper.DefineLabel();
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndSetLen);
			if (this.CachesResult(ndSetLen.Child))
			{
				this.NestedVisitEnsureStack(ndSetLen.Child);
				this.helper.CallCacheCount(this.iterNested.Storage.ItemStorageType);
			}
			else
			{
				this.helper.Emit(OpCodes.Ldc_I4_0);
				this.StartNestedIterator(ndSetLen.Child, label);
				this.Visit(ndSetLen.Child);
				this.iterCurr.EnsureNoCache();
				this.iterCurr.DiscardStack();
				this.helper.Emit(OpCodes.Ldc_I4_1);
				this.helper.Emit(OpCodes.Add);
				if (optimizerPatterns.MatchesPattern(OptimizerPatternName.MaxPosition))
				{
					this.helper.Emit(OpCodes.Dup);
					this.helper.LoadInteger((int)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					this.helper.Emit(OpCodes.Bgt, label);
				}
				this.iterCurr.LoopToEnd(label);
				this.EndNestedIterator(ndSetLen.Child);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(int), false);
			return ndSetLen;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0011B080 File Offset: 0x00119280
		protected override QilNode VisitSequence(QilList ndSeq)
		{
			if (XmlILConstructInfo.Read(ndSeq).ConstructMethod == XmlILConstructMethod.Writer)
			{
				using (IEnumerator<QilNode> enumerator = ndSeq.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QilNode nd = enumerator.Current;
						this.NestedVisit(nd);
					}
					return ndSeq;
				}
			}
			if (ndSeq.Count == 0)
			{
				this.VisitEmpty(ndSeq);
			}
			else
			{
				this.Sequence(ndSeq);
			}
			return ndSeq;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0011B0F0 File Offset: 0x001192F0
		private void VisitEmpty(QilNode nd)
		{
			this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, this.iterCurr.GetLabelNext());
			this.helper.Emit(OpCodes.Ldnull);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), false);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x0011B144 File Offset: 0x00119344
		private void Sequence(QilList ndSeq)
		{
			Label label = default(Label);
			Type itemStorageType = this.GetItemStorageType(ndSeq);
			if (ndSeq.XmlType.IsSingleton)
			{
				foreach (QilNode qilNode in ndSeq)
				{
					if (qilNode.XmlType.IsSingleton)
					{
						this.NestedVisitEnsureStack(qilNode);
					}
					else
					{
						label = this.helper.DefineLabel();
						this.NestedVisit(qilNode, label);
						this.iterCurr.DiscardStack();
						this.helper.MarkLabel(label);
					}
				}
				this.iterCurr.Storage = StorageDescriptor.Stack(itemStorageType, false);
				return;
			}
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$itemList", itemStorageType);
			LocalBuilder locBldr = this.helper.DeclareLocal("$$$idxList", typeof(int));
			Label[] array = new Label[ndSeq.Count];
			Label label2 = this.helper.DefineLabel();
			for (int i = 0; i < ndSeq.Count; i++)
			{
				if (i != 0)
				{
					this.helper.MarkLabel(label);
				}
				if (i == ndSeq.Count - 1)
				{
					label = this.iterCurr.GetLabelNext();
				}
				else
				{
					label = this.helper.DefineLabel();
				}
				this.helper.LoadInteger(i);
				this.helper.Emit(OpCodes.Stloc, locBldr);
				this.NestedVisit(ndSeq[i], label);
				this.iterCurr.EnsureItemStorageType(ndSeq[i].XmlType, itemStorageType);
				this.iterCurr.EnsureLocalNoCache(localBuilder);
				array[i] = this.iterNested.GetLabelNext();
				this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label2);
			}
			Label label3 = this.helper.DefineLabel();
			this.helper.MarkLabel(label3);
			this.helper.Emit(OpCodes.Ldloc, locBldr);
			this.helper.Emit(OpCodes.Switch, array);
			this.helper.MarkLabel(label2);
			this.iterCurr.SetIterator(label3, StorageDescriptor.Local(localBuilder, itemStorageType, false));
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x0011B378 File Offset: 0x00119578
		protected override QilNode VisitUnion(QilBinary ndUnion)
		{
			return this.CreateSetIterator(ndUnion, "$$$iterUnion", typeof(UnionIterator), XmlILMethods.UnionCreate, XmlILMethods.UnionNext);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x0011B39A File Offset: 0x0011959A
		protected override QilNode VisitIntersection(QilBinary ndInter)
		{
			return this.CreateSetIterator(ndInter, "$$$iterInter", typeof(IntersectIterator), XmlILMethods.InterCreate, XmlILMethods.InterNext);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0011B3BC File Offset: 0x001195BC
		protected override QilNode VisitDifference(QilBinary ndDiff)
		{
			return this.CreateSetIterator(ndDiff, "$$$iterDiff", typeof(DifferenceIterator), XmlILMethods.DiffCreate, XmlILMethods.DiffNext);
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x0011B3E0 File Offset: 0x001195E0
		private QilNode CreateSetIterator(QilBinary ndSet, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			LocalBuilder localBuilder2 = this.helper.DeclareLocal("$$$navSet", typeof(XPathNavigator));
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.helper.LoadQueryRuntime();
			this.helper.Call(methCreate);
			Label label = this.helper.DefineLabel();
			Label label2 = this.helper.DefineLabel();
			Label label3 = this.helper.DefineLabel();
			this.NestedVisit(ndSet.Left, label);
			Label labelNext = this.iterNested.GetLabelNext();
			this.iterCurr.EnsureLocal(localBuilder2);
			this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label2);
			this.helper.MarkLabel(label3);
			this.NestedVisit(ndSet.Right, label);
			Label labelNext2 = this.iterNested.GetLabelNext();
			this.iterCurr.EnsureLocal(localBuilder2);
			this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label2);
			this.helper.MarkLabel(label);
			this.helper.Emit(OpCodes.Ldnull);
			this.helper.Emit(OpCodes.Stloc, localBuilder2);
			this.helper.MarkLabel(label2);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.helper.Emit(OpCodes.Ldloc, localBuilder2);
			this.helper.Call(methNext);
			if (ndSet.XmlType.IsSingleton)
			{
				this.helper.Emit(OpCodes.Switch, new Label[]
				{
					label3,
					labelNext,
					labelNext2
				});
				this.iterCurr.Storage = StorageDescriptor.Current(localBuilder, typeof(XPathNavigator));
			}
			else
			{
				this.helper.Emit(OpCodes.Switch, new Label[]
				{
					this.iterCurr.GetLabelNext(),
					label3,
					labelNext,
					labelNext2
				});
				this.iterCurr.SetIterator(label, StorageDescriptor.Current(localBuilder, typeof(XPathNavigator)));
			}
			return ndSet;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0011B604 File Offset: 0x00119804
		protected override QilNode VisitAverage(QilUnary ndAvg)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndAvg)];
			return this.CreateAggregator(ndAvg, "$$$aggAvg", xmlILStorageMethods, xmlILStorageMethods.AggAvg, xmlILStorageMethods.AggAvgResult);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0011B63C File Offset: 0x0011983C
		protected override QilNode VisitSum(QilUnary ndSum)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndSum)];
			return this.CreateAggregator(ndSum, "$$$aggSum", xmlILStorageMethods, xmlILStorageMethods.AggSum, xmlILStorageMethods.AggSumResult);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0011B674 File Offset: 0x00119874
		protected override QilNode VisitMinimum(QilUnary ndMin)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndMin)];
			return this.CreateAggregator(ndMin, "$$$aggMin", xmlILStorageMethods, xmlILStorageMethods.AggMin, xmlILStorageMethods.AggMinResult);
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x0011B6AC File Offset: 0x001198AC
		protected override QilNode VisitMaximum(QilUnary ndMax)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndMax)];
			return this.CreateAggregator(ndMax, "$$$aggMax", xmlILStorageMethods, xmlILStorageMethods.AggMax, xmlILStorageMethods.AggMaxResult);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0011B6E4 File Offset: 0x001198E4
		private QilNode CreateAggregator(QilUnary ndAgg, string aggName, XmlILStorageMethods methods, MethodInfo methAgg, MethodInfo methResult)
		{
			Label lblOnEnd = this.helper.DefineLabel();
			Type declaringType = methAgg.DeclaringType;
			LocalBuilder locBldr = this.helper.DeclareLocal(aggName, declaringType);
			this.helper.Emit(OpCodes.Ldloca, locBldr);
			this.helper.Call(methods.AggCreate);
			this.StartNestedIterator(ndAgg.Child, lblOnEnd);
			this.helper.Emit(OpCodes.Ldloca, locBldr);
			this.Visit(ndAgg.Child);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndAgg.XmlType, this.GetItemStorageType(ndAgg));
			this.helper.Call(methAgg);
			this.helper.Emit(OpCodes.Ldloca, locBldr);
			this.iterCurr.LoopToEnd(lblOnEnd);
			this.EndNestedIterator(ndAgg.Child);
			if (ndAgg.XmlType.MaybeEmpty)
			{
				this.helper.Call(methods.AggIsEmpty);
				this.helper.Emit(OpCodes.Brtrue, this.iterCurr.GetLabelNext());
				this.helper.Emit(OpCodes.Ldloca, locBldr);
			}
			this.helper.Call(methResult);
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndAgg), false);
			return ndAgg;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x0011B829 File Offset: 0x00119A29
		protected override QilNode VisitNegate(QilUnary ndNeg)
		{
			this.NestedVisitEnsureStack(ndNeg.Child);
			this.helper.CallArithmeticOp(QilNodeType.Negate, ndNeg.XmlType.TypeCode);
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndNeg), false);
			return ndNeg;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x0011B868 File Offset: 0x00119A68
		protected override QilNode VisitAdd(QilBinary ndPlus)
		{
			return this.ArithmeticOp(ndPlus);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x0011B868 File Offset: 0x00119A68
		protected override QilNode VisitSubtract(QilBinary ndMinus)
		{
			return this.ArithmeticOp(ndMinus);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x0011B868 File Offset: 0x00119A68
		protected override QilNode VisitMultiply(QilBinary ndMul)
		{
			return this.ArithmeticOp(ndMul);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0011B868 File Offset: 0x00119A68
		protected override QilNode VisitDivide(QilBinary ndDiv)
		{
			return this.ArithmeticOp(ndDiv);
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0011B868 File Offset: 0x00119A68
		protected override QilNode VisitModulo(QilBinary ndMod)
		{
			return this.ArithmeticOp(ndMod);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0011B874 File Offset: 0x00119A74
		private QilNode ArithmeticOp(QilBinary ndOp)
		{
			this.NestedVisitEnsureStack(ndOp.Left, ndOp.Right);
			this.helper.CallArithmeticOp(ndOp.NodeType, ndOp.XmlType.TypeCode);
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndOp), false);
			return ndOp;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x0011B8C8 File Offset: 0x00119AC8
		protected override QilNode VisitStrLength(QilUnary ndLen)
		{
			this.NestedVisitEnsureStack(ndLen.Child);
			this.helper.Call(XmlILMethods.StrLen);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(int), false);
			return ndLen;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0011B904 File Offset: 0x00119B04
		protected override QilNode VisitStrConcat(QilStrConcat ndStrConcat)
		{
			QilNode qilNode = ndStrConcat.Delimiter;
			if (qilNode.NodeType == QilNodeType.LiteralString && ((QilLiteral)qilNode).Length == 0)
			{
				qilNode = null;
			}
			QilNode values = ndStrConcat.Values;
			bool flag;
			if (values.NodeType == QilNodeType.Sequence && values.Count < 5)
			{
				flag = true;
				using (IEnumerator<QilNode> enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.XmlType.IsSingleton)
						{
							flag = false;
						}
					}
					goto IL_79;
				}
			}
			flag = false;
			IL_79:
			if (flag)
			{
				foreach (QilNode nd in values)
				{
					this.NestedVisitEnsureStack(nd);
				}
				this.helper.CallConcatStrings(values.Count);
			}
			else
			{
				LocalBuilder localBuilder = this.helper.DeclareLocal("$$$strcat", typeof(StringConcat));
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				this.helper.Call(XmlILMethods.StrCatClear);
				if (qilNode != null)
				{
					this.helper.Emit(OpCodes.Ldloca, localBuilder);
					this.NestedVisitEnsureStack(qilNode);
					this.helper.Call(XmlILMethods.StrCatDelim);
				}
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				if (values.NodeType == QilNodeType.Sequence)
				{
					using (IEnumerator<QilNode> enumerator = values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							QilNode ndStr = enumerator.Current;
							this.GenerateConcat(ndStr, localBuilder);
						}
						goto IL_181;
					}
				}
				this.GenerateConcat(values, localBuilder);
				IL_181:
				this.helper.Call(XmlILMethods.StrCatResult);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndStrConcat;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x0011BAE8 File Offset: 0x00119CE8
		private void GenerateConcat(QilNode ndStr, LocalBuilder locStringConcat)
		{
			Label lblOnEnd = this.helper.DefineLabel();
			this.StartNestedIterator(ndStr, lblOnEnd);
			this.Visit(ndStr);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndStr.XmlType, typeof(string));
			this.helper.Call(XmlILMethods.StrCatCat);
			this.helper.Emit(OpCodes.Ldloca, locStringConcat);
			this.iterCurr.LoopToEnd(lblOnEnd);
			this.EndNestedIterator(ndStr);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0011BB6B File Offset: 0x00119D6B
		protected override QilNode VisitStrParseQName(QilBinary ndParsedTagName)
		{
			this.VisitStrParseQName(ndParsedTagName, false);
			return ndParsedTagName;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0011BB78 File Offset: 0x00119D78
		private void VisitStrParseQName(QilBinary ndParsedTagName, bool preservePrefix)
		{
			if (!preservePrefix)
			{
				this.helper.LoadQueryRuntime();
			}
			this.NestedVisitEnsureStack(ndParsedTagName.Left);
			if (ndParsedTagName.Right.XmlType.TypeCode == XmlTypeCode.String)
			{
				this.NestedVisitEnsureStack(ndParsedTagName.Right);
				if (!preservePrefix)
				{
					this.helper.CallParseTagName(GenerateNameType.TagNameAndNamespace);
				}
			}
			else
			{
				if (ndParsedTagName.Right.NodeType == QilNodeType.Sequence)
				{
					this.helper.LoadInteger(this.helper.StaticData.DeclarePrefixMappings(ndParsedTagName.Right));
				}
				else
				{
					this.helper.LoadInteger(this.helper.StaticData.DeclarePrefixMappings(new QilNode[]
					{
						ndParsedTagName.Right
					}));
				}
				if (!preservePrefix)
				{
					this.helper.CallParseTagName(GenerateNameType.TagNameAndMappings);
				}
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XmlQualifiedName), false);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x0011BC56 File Offset: 0x00119E56
		protected override QilNode VisitNe(QilBinary ndNe)
		{
			this.Compare(ndNe);
			return ndNe;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x0011BC56 File Offset: 0x00119E56
		protected override QilNode VisitEq(QilBinary ndEq)
		{
			this.Compare(ndEq);
			return ndEq;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x0011BC56 File Offset: 0x00119E56
		protected override QilNode VisitGt(QilBinary ndGt)
		{
			this.Compare(ndGt);
			return ndGt;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x0011BC56 File Offset: 0x00119E56
		protected override QilNode VisitGe(QilBinary ndGe)
		{
			this.Compare(ndGe);
			return ndGe;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x0011BC56 File Offset: 0x00119E56
		protected override QilNode VisitLt(QilBinary ndLt)
		{
			this.Compare(ndLt);
			return ndLt;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x0011BC56 File Offset: 0x00119E56
		protected override QilNode VisitLe(QilBinary ndLe)
		{
			this.Compare(ndLe);
			return ndLe;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x0011BC60 File Offset: 0x00119E60
		private void Compare(QilBinary ndComp)
		{
			QilNodeType nodeType = ndComp.NodeType;
			if (nodeType == QilNodeType.Eq || nodeType == QilNodeType.Ne)
			{
				if (this.TryZeroCompare(nodeType, ndComp.Left, ndComp.Right))
				{
					return;
				}
				if (this.TryZeroCompare(nodeType, ndComp.Right, ndComp.Left))
				{
					return;
				}
				if (this.TryNameCompare(nodeType, ndComp.Left, ndComp.Right))
				{
					return;
				}
				if (this.TryNameCompare(nodeType, ndComp.Right, ndComp.Left))
				{
					return;
				}
			}
			this.NestedVisitEnsureStack(ndComp.Left, ndComp.Right);
			XmlTypeCode typeCode = ndComp.Left.XmlType.TypeCode;
			if (typeCode <= XmlTypeCode.QName)
			{
				switch (typeCode)
				{
				case XmlTypeCode.String:
				case XmlTypeCode.Decimal:
					break;
				case XmlTypeCode.Boolean:
				case XmlTypeCode.Double:
					goto IL_10D;
				case XmlTypeCode.Float:
					return;
				default:
					if (typeCode != XmlTypeCode.QName)
					{
						return;
					}
					break;
				}
				if (nodeType == QilNodeType.Eq || nodeType == QilNodeType.Ne)
				{
					this.helper.CallCompareEquals(typeCode);
					this.ZeroCompare((nodeType == QilNodeType.Eq) ? QilNodeType.Ne : QilNodeType.Eq, true);
					return;
				}
				this.helper.CallCompare(typeCode);
				this.helper.Emit(OpCodes.Ldc_I4_0);
				this.ClrCompare(nodeType, typeCode);
				return;
			}
			else if (typeCode != XmlTypeCode.Integer && typeCode != XmlTypeCode.Int)
			{
				return;
			}
			IL_10D:
			this.ClrCompare(nodeType, typeCode);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0011BD82 File Offset: 0x00119F82
		protected override QilNode VisitIs(QilBinary ndIs)
		{
			this.NestedVisitEnsureStack(ndIs.Left, ndIs.Right);
			this.helper.Call(XmlILMethods.NavSamePos);
			this.ZeroCompare(QilNodeType.Ne, true);
			return ndIs;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x0011BDB0 File Offset: 0x00119FB0
		protected override QilNode VisitBefore(QilBinary ndBefore)
		{
			this.ComparePosition(ndBefore);
			return ndBefore;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x0011BDB0 File Offset: 0x00119FB0
		protected override QilNode VisitAfter(QilBinary ndAfter)
		{
			this.ComparePosition(ndAfter);
			return ndAfter;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0011BDBC File Offset: 0x00119FBC
		private void ComparePosition(QilBinary ndComp)
		{
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndComp.Left, ndComp.Right);
			this.helper.Call(XmlILMethods.CompPos);
			this.helper.LoadInteger(0);
			this.ClrCompare((ndComp.NodeType == QilNodeType.Before) ? QilNodeType.Lt : QilNodeType.Gt, XmlTypeCode.String);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0011BE1C File Offset: 0x0011A01C
		protected override QilNode VisitFor(QilIterator ndFor)
		{
			IteratorDescriptor cachedIteratorDescriptor = XmlILAnnotation.Write(ndFor).CachedIteratorDescriptor;
			this.iterCurr.Storage = cachedIteratorDescriptor.Storage;
			if (this.iterCurr.Storage.Location == ItemLocation.Global)
			{
				this.iterCurr.EnsureStack();
			}
			return ndFor;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x0011BE68 File Offset: 0x0011A068
		protected override QilNode VisitLet(QilIterator ndLet)
		{
			return this.VisitFor(ndLet);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x0011BE68 File Offset: 0x0011A068
		protected override QilNode VisitParameter(QilParameter ndParameter)
		{
			return this.VisitFor(ndParameter);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0011BE74 File Offset: 0x0011A074
		protected override QilNode VisitLoop(QilLoop ndLoop)
		{
			bool hasOnEnd;
			Label lblOnEnd;
			this.StartWriterLoop(ndLoop, out hasOnEnd, out lblOnEnd);
			this.StartBinding(ndLoop.Variable);
			this.Visit(ndLoop.Body);
			this.EndBinding(ndLoop.Variable);
			this.EndWriterLoop(ndLoop, hasOnEnd, lblOnEnd);
			return ndLoop;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0011BEBC File Offset: 0x0011A0BC
		protected override QilNode VisitFilter(QilLoop ndFilter)
		{
			if (this.HandleFilterPatterns(ndFilter))
			{
				return ndFilter;
			}
			this.StartBinding(ndFilter.Variable);
			this.iterCurr.SetIterator(this.iterNested);
			this.StartNestedIterator(ndFilter.Body);
			this.iterCurr.SetBranching(BranchingContext.OnFalse, this.iterCurr.ParentIterator.GetLabelNext());
			this.Visit(ndFilter.Body);
			this.EndNestedIterator(ndFilter.Body);
			this.EndBinding(ndFilter.Variable);
			return ndFilter;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x0011BF40 File Offset: 0x0011A140
		private bool HandleFilterPatterns(QilLoop ndFilter)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndFilter);
			bool flag = optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterElements);
			if (flag || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterContentKind))
			{
				XmlNodeKindFlags xmlNodeKindFlags;
				QilName qilName;
				if (flag)
				{
					xmlNodeKindFlags = XmlNodeKindFlags.Element;
					qilName = (QilName)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName);
				}
				else
				{
					xmlNodeKindFlags = ((XmlQueryType)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName)).NodeKinds;
					qilName = null;
				}
				QilNode qilNode = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode);
				QilNode qilNode2 = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput);
				QilNodeType nodeType = qilNode.NodeType;
				switch (nodeType)
				{
				case QilNodeType.Content:
					if (flag)
					{
						LocalBuilder localBuilder = this.helper.DeclareLocal("$$$iterElemContent", typeof(ElementContentIterator));
						this.helper.Emit(OpCodes.Ldloca, localBuilder);
						this.NestedVisitEnsureStack(qilNode2);
						this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.LocalName));
						this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.NamespaceUri));
						this.helper.Call(XmlILMethods.ElemContentCreate);
						this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, XmlILMethods.ElemContentNext);
					}
					else if (xmlNodeKindFlags == XmlNodeKindFlags.Content)
					{
						this.CreateSimpleIterator(qilNode2, "$$$iterContent", typeof(ContentIterator), XmlILMethods.ContentCreate, XmlILMethods.ContentNext);
					}
					else
					{
						LocalBuilder localBuilder = this.helper.DeclareLocal("$$$iterContent", typeof(NodeKindContentIterator));
						this.helper.Emit(OpCodes.Ldloca, localBuilder);
						this.NestedVisitEnsureStack(qilNode2);
						this.helper.LoadInteger((int)this.QilXmlToXPathNodeType(xmlNodeKindFlags));
						this.helper.Call(XmlILMethods.KindContentCreate);
						this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, XmlILMethods.KindContentNext);
					}
					return true;
				case QilNodeType.Attribute:
				case QilNodeType.Root:
				case QilNodeType.XmlContext:
					break;
				case QilNodeType.Parent:
					this.CreateFilteredIterator(qilNode2, "$$$iterPar", typeof(ParentIterator), XmlILMethods.ParentCreate, XmlILMethods.ParentNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.Descendant:
				case QilNodeType.DescendantOrSelf:
					this.CreateFilteredIterator(qilNode2, "$$$iterDesc", typeof(DescendantIterator), XmlILMethods.DescCreate, XmlILMethods.DescNext, xmlNodeKindFlags, qilName, (qilNode.NodeType == QilNodeType.Descendant) ? TriState.False : TriState.True, null);
					return true;
				case QilNodeType.Ancestor:
				case QilNodeType.AncestorOrSelf:
					this.CreateFilteredIterator(qilNode2, "$$$iterAnc", typeof(AncestorIterator), XmlILMethods.AncCreate, XmlILMethods.AncNext, xmlNodeKindFlags, qilName, (qilNode.NodeType == QilNodeType.Ancestor) ? TriState.False : TriState.True, null);
					return true;
				case QilNodeType.Preceding:
					this.CreateFilteredIterator(qilNode2, "$$$iterPrec", typeof(PrecedingIterator), XmlILMethods.PrecCreate, XmlILMethods.PrecNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.FollowingSibling:
					this.CreateFilteredIterator(qilNode2, "$$$iterFollSib", typeof(FollowingSiblingIterator), XmlILMethods.FollSibCreate, XmlILMethods.FollSibNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.PrecedingSibling:
					this.CreateFilteredIterator(qilNode2, "$$$iterPreSib", typeof(PrecedingSiblingIterator), XmlILMethods.PreSibCreate, XmlILMethods.PreSibNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.NodeRange:
					this.CreateFilteredIterator(qilNode2, "$$$iterRange", typeof(NodeRangeIterator), XmlILMethods.NodeRangeCreate, XmlILMethods.NodeRangeNext, xmlNodeKindFlags, qilName, TriState.Unknown, ((QilBinary)qilNode).Right);
					return true;
				default:
					if (nodeType == QilNodeType.XPathFollowing)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterFoll", typeof(XPathFollowingIterator), XmlILMethods.XPFollCreate, XmlILMethods.XPFollNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
						return true;
					}
					if (nodeType == QilNodeType.XPathPreceding)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterPrec", typeof(XPathPrecedingIterator), XmlILMethods.XPPrecCreate, XmlILMethods.XPPrecNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
						return true;
					}
					break;
				}
			}
			else
			{
				if (optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterAttributeKind))
				{
					QilNode qilNode2 = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput);
					this.CreateSimpleIterator(qilNode2, "$$$iterAttr", typeof(AttributeIterator), XmlILMethods.AttrCreate, XmlILMethods.AttrNext);
					return true;
				}
				if (optimizerPatterns.MatchesPattern(OptimizerPatternName.EqualityIndex))
				{
					Label lblOnEnd = this.helper.DefineLabel();
					Label label = this.helper.DefineLabel();
					QilIterator qilIterator = (QilIterator)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode);
					QilNode n = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput);
					LocalBuilder locBldr = this.helper.DeclareLocal("$$$index", typeof(XmlILIndex));
					this.helper.LoadQueryRuntime();
					this.helper.Emit(OpCodes.Ldarg_1);
					this.helper.LoadInteger(this.indexId);
					this.helper.Emit(OpCodes.Ldloca, locBldr);
					this.helper.Call(XmlILMethods.FindIndex);
					this.helper.Emit(OpCodes.Brtrue, label);
					this.helper.LoadQueryRuntime();
					this.helper.Emit(OpCodes.Ldarg_1);
					this.helper.LoadInteger(this.indexId);
					this.helper.Emit(OpCodes.Ldloc, locBldr);
					this.StartNestedIterator(qilIterator, lblOnEnd);
					this.StartBinding(qilIterator);
					this.Visit(n);
					this.iterCurr.EnsureStackNoCache();
					this.VisitFor(qilIterator);
					this.iterCurr.EnsureStackNoCache();
					this.iterCurr.EnsureItemStorageType(qilIterator.XmlType, typeof(XPathNavigator));
					this.helper.Call(XmlILMethods.IndexAdd);
					this.helper.Emit(OpCodes.Ldloc, locBldr);
					this.iterCurr.LoopToEnd(lblOnEnd);
					this.EndBinding(qilIterator);
					this.EndNestedIterator(qilIterator);
					this.helper.Call(XmlILMethods.AddNewIndex);
					this.helper.MarkLabel(label);
					this.helper.Emit(OpCodes.Ldloc, locBldr);
					this.helper.Emit(OpCodes.Ldarg_2);
					this.helper.Call(XmlILMethods.IndexLookup);
					this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), true);
					this.indexId++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x0011C51C File Offset: 0x0011A71C
		private void StartBinding(QilIterator ndIter)
		{
			OptimizerPatterns patt = OptimizerPatterns.Read(ndIter);
			if (this.qil.IsDebug && ndIter.SourceLine != null)
			{
				this.helper.DebugSequencePoint(ndIter.SourceLine);
			}
			if (ndIter.NodeType == QilNodeType.For || ndIter.XmlType.IsSingleton)
			{
				this.StartForBinding(ndIter, patt);
			}
			else
			{
				this.StartLetBinding(ndIter);
			}
			XmlILAnnotation.Write(ndIter).CachedIteratorDescriptor = this.iterNested;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0011C590 File Offset: 0x0011A790
		private void StartForBinding(QilIterator ndFor, OptimizerPatterns patt)
		{
			LocalBuilder localBuilder = null;
			if (this.iterCurr.HasLabelNext)
			{
				this.StartNestedIterator(ndFor.Binding, this.iterCurr.GetLabelNext());
			}
			else
			{
				this.StartNestedIterator(ndFor.Binding);
			}
			if (patt.MatchesPattern(OptimizerPatternName.IsPositional))
			{
				localBuilder = this.helper.DeclareLocal("$$$pos", typeof(int));
				this.helper.Emit(OpCodes.Ldc_I4_0);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
			}
			this.Visit(ndFor.Binding);
			if (this.qil.IsDebug && ndFor.DebugName != null)
			{
				this.helper.DebugStartScope();
				this.iterCurr.EnsureLocalNoCache("$$$for");
				this.iterCurr.Storage.LocalLocation.SetLocalSymInfo(ndFor.DebugName);
			}
			else
			{
				this.iterCurr.EnsureNoStackNoCache("$$$for");
			}
			if (patt.MatchesPattern(OptimizerPatternName.IsPositional))
			{
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.helper.Emit(OpCodes.Ldc_I4_1);
				this.helper.Emit(OpCodes.Add);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
				if (patt.MatchesPattern(OptimizerPatternName.MaxPosition))
				{
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.LoadInteger((int)patt.GetArgument(OptimizerPatternArgument.ElementQName));
					this.helper.Emit(OpCodes.Bgt, this.iterCurr.ParentIterator.GetLabelNext());
				}
				this.iterCurr.LocalPosition = localBuilder;
			}
			this.EndNestedIterator(ndFor.Binding);
			this.iterCurr.SetIterator(this.iterNested);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0011C74C File Offset: 0x0011A94C
		public void StartLetBinding(QilIterator ndLet)
		{
			this.StartNestedIterator(ndLet);
			this.NestedVisit(ndLet.Binding, this.GetItemStorageType(ndLet), !ndLet.XmlType.IsSingleton);
			if (this.qil.IsDebug && ndLet.DebugName != null)
			{
				this.helper.DebugStartScope();
				this.iterCurr.EnsureLocal("$$$cache");
				this.iterCurr.Storage.LocalLocation.SetLocalSymInfo(ndLet.DebugName);
			}
			else
			{
				this.iterCurr.EnsureNoStack("$$$cache");
			}
			this.EndNestedIterator(ndLet);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x0011C7E8 File Offset: 0x0011A9E8
		private void EndBinding(QilIterator ndIter)
		{
			if (this.qil.IsDebug && ndIter.DebugName != null)
			{
				this.helper.DebugEndScope();
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x0011C80C File Offset: 0x0011AA0C
		protected override QilNode VisitPositionOf(QilUnary ndPos)
		{
			LocalBuilder localPosition = XmlILAnnotation.Write(ndPos.Child as QilIterator).CachedIteratorDescriptor.LocalPosition;
			this.iterCurr.Storage = StorageDescriptor.Local(localPosition, typeof(int), false);
			return ndPos;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0011C854 File Offset: 0x0011AA54
		protected override QilNode VisitSort(QilLoop ndSort)
		{
			Type itemStorageType = this.GetItemStorageType(ndSort);
			Label lblOnEnd = this.helper.DefineLabel();
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[itemStorageType];
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$cache", xmlILStorageMethods.SeqType);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.CallToken(xmlILStorageMethods.SeqReuse);
			this.helper.Emit(OpCodes.Stloc, localBuilder);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			LocalBuilder localBuilder2 = this.helper.DeclareLocal("$$$keys", typeof(XmlSortKeyAccumulator));
			this.helper.Emit(OpCodes.Ldloca, localBuilder2);
			this.helper.Call(XmlILMethods.SortKeyCreate);
			this.StartNestedIterator(ndSort.Variable, lblOnEnd);
			this.StartBinding(ndSort.Variable);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndSort.Variable.XmlType, this.GetItemStorageType(ndSort.Variable));
			this.helper.Call(xmlILStorageMethods.SeqAdd);
			this.helper.Emit(OpCodes.Ldloca, localBuilder2);
			foreach (QilNode qilNode in ndSort.Body)
			{
				QilSortKey ndKey = (QilSortKey)qilNode;
				this.VisitSortKey(ndKey, localBuilder2);
			}
			this.helper.Call(XmlILMethods.SortKeyFinish);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.iterCurr.LoopToEnd(lblOnEnd);
			this.helper.Emit(OpCodes.Pop);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Emit(OpCodes.Ldloca, localBuilder2);
			this.helper.Call(XmlILMethods.SortKeyKeys);
			this.helper.Call(xmlILStorageMethods.SeqSortByKeys);
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, itemStorageType, true);
			this.EndBinding(ndSort.Variable);
			this.EndNestedIterator(ndSort.Variable);
			this.iterCurr.SetIterator(this.iterNested);
			return ndSort;
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x0011CA90 File Offset: 0x0011AC90
		private void VisitSortKey(QilSortKey ndKey, LocalBuilder locKeys)
		{
			this.helper.Emit(OpCodes.Ldloca, locKeys);
			if (ndKey.Collation.NodeType == QilNodeType.LiteralString)
			{
				this.helper.CallGetCollation(this.helper.StaticData.DeclareCollation((QilLiteral)ndKey.Collation));
			}
			else
			{
				this.helper.LoadQueryRuntime();
				this.NestedVisitEnsureStack(ndKey.Collation);
				this.helper.Call(XmlILMethods.CreateCollation);
			}
			if (ndKey.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(ndKey.Key);
				this.helper.AddSortKey(ndKey.Key.XmlType);
				return;
			}
			Label label = this.helper.DefineLabel();
			this.StartNestedIterator(ndKey.Key, label);
			this.Visit(ndKey.Key);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndKey.Key.XmlType, this.GetItemStorageType(ndKey.Key));
			this.helper.AddSortKey(ndKey.Key.XmlType);
			Label label2 = this.helper.DefineLabel();
			this.helper.EmitUnconditionalBranch(OpCodes.Br_S, label2);
			this.helper.MarkLabel(label);
			this.helper.AddSortKey(null);
			this.helper.MarkLabel(label2);
			this.EndNestedIterator(ndKey.Key);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x0011CBF8 File Offset: 0x0011ADF8
		protected override QilNode VisitDocOrderDistinct(QilUnary ndDod)
		{
			if (ndDod.XmlType.IsSingleton)
			{
				return this.Visit(ndDod.Child);
			}
			if (this.HandleDodPatterns(ndDod))
			{
				return ndDod;
			}
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureCache(ndDod.Child, typeof(XPathNavigator));
			this.iterCurr.EnsureStack();
			this.helper.Call(XmlILMethods.DocOrder);
			return ndDod;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0011CC68 File Offset: 0x0011AE68
		private bool HandleDodPatterns(QilUnary ndDod)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndDod);
			bool flag = optimizerPatterns.MatchesPattern(OptimizerPatternName.JoinAndDod);
			if (flag || optimizerPatterns.MatchesPattern(OptimizerPatternName.DodReverse))
			{
				OptimizerPatterns optimizerPatterns2 = OptimizerPatterns.Read((QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
				XmlNodeKindFlags kinds;
				QilName ndName;
				if (optimizerPatterns2.MatchesPattern(OptimizerPatternName.FilterElements))
				{
					kinds = XmlNodeKindFlags.Element;
					ndName = (QilName)optimizerPatterns2.GetArgument(OptimizerPatternArgument.ElementQName);
				}
				else if (optimizerPatterns2.MatchesPattern(OptimizerPatternName.FilterContentKind))
				{
					kinds = ((XmlQueryType)optimizerPatterns2.GetArgument(OptimizerPatternArgument.ElementQName)).NodeKinds;
					ndName = null;
				}
				else
				{
					kinds = (((ndDod.XmlType.NodeKinds & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None) ? XmlNodeKindFlags.Any : XmlNodeKindFlags.Content);
					ndName = null;
				}
				QilNode qilNode = (QilNode)optimizerPatterns2.GetArgument(OptimizerPatternArgument.StepNode);
				if (flag)
				{
					QilNodeType nodeType = qilNode.NodeType;
					if (nodeType <= QilNodeType.DescendantOrSelf)
					{
						if (nodeType == QilNodeType.Content)
						{
							this.CreateContainerIterator(ndDod, "$$$iterContent", typeof(ContentMergeIterator), XmlILMethods.ContentMergeCreate, XmlILMethods.ContentMergeNext, kinds, ndName, TriState.Unknown);
							return true;
						}
						if (nodeType - QilNodeType.Descendant <= 1)
						{
							this.CreateContainerIterator(ndDod, "$$$iterDesc", typeof(DescendantMergeIterator), XmlILMethods.DescMergeCreate, XmlILMethods.DescMergeNext, kinds, ndName, (qilNode.NodeType == QilNodeType.Descendant) ? TriState.False : TriState.True);
							return true;
						}
					}
					else
					{
						if (nodeType == QilNodeType.FollowingSibling)
						{
							this.CreateContainerIterator(ndDod, "$$$iterFollSib", typeof(FollowingSiblingMergeIterator), XmlILMethods.FollSibMergeCreate, XmlILMethods.FollSibMergeNext, kinds, ndName, TriState.Unknown);
							return true;
						}
						if (nodeType == QilNodeType.XPathFollowing)
						{
							this.CreateContainerIterator(ndDod, "$$$iterFoll", typeof(XPathFollowingMergeIterator), XmlILMethods.XPFollMergeCreate, XmlILMethods.XPFollMergeNext, kinds, ndName, TriState.Unknown);
							return true;
						}
						if (nodeType == QilNodeType.XPathPreceding)
						{
							this.CreateContainerIterator(ndDod, "$$$iterPrec", typeof(XPathPrecedingMergeIterator), XmlILMethods.XPPrecMergeCreate, XmlILMethods.XPPrecMergeNext, kinds, ndName, TriState.Unknown);
							return true;
						}
					}
				}
				else
				{
					QilNode ndCtxt = (QilNode)optimizerPatterns2.GetArgument(OptimizerPatternArgument.StepInput);
					QilNodeType nodeType = qilNode.NodeType;
					if (nodeType - QilNodeType.Ancestor <= 1)
					{
						this.CreateFilteredIterator(ndCtxt, "$$$iterAnc", typeof(AncestorDocOrderIterator), XmlILMethods.AncDOCreate, XmlILMethods.AncDONext, kinds, ndName, (qilNode.NodeType == QilNodeType.Ancestor) ? TriState.False : TriState.True, null);
						return true;
					}
					if (nodeType == QilNodeType.PrecedingSibling)
					{
						this.CreateFilteredIterator(ndCtxt, "$$$iterPreSib", typeof(PrecedingSiblingDocOrderIterator), XmlILMethods.PreSibDOCreate, XmlILMethods.PreSibDONext, kinds, ndName, TriState.Unknown, null);
						return true;
					}
					if (nodeType == QilNodeType.XPathPreceding)
					{
						this.CreateFilteredIterator(ndCtxt, "$$$iterPrec", typeof(XPathPrecedingDocOrderIterator), XmlILMethods.XPPrecDOCreate, XmlILMethods.XPPrecDONext, kinds, ndName, TriState.Unknown, null);
						return true;
					}
				}
			}
			else if (optimizerPatterns.MatchesPattern(OptimizerPatternName.DodMerge))
			{
				LocalBuilder locBldr = this.helper.DeclareLocal("$$$dodMerge", typeof(DodSequenceMerge));
				Label lblOnEnd = this.helper.DefineLabel();
				this.helper.Emit(OpCodes.Ldloca, locBldr);
				this.helper.LoadQueryRuntime();
				this.helper.Call(XmlILMethods.DodMergeCreate);
				this.helper.Emit(OpCodes.Ldloca, locBldr);
				this.StartNestedIterator(ndDod.Child, lblOnEnd);
				this.Visit(ndDod.Child);
				this.iterCurr.EnsureStack();
				this.helper.Call(XmlILMethods.DodMergeAdd);
				this.helper.Emit(OpCodes.Ldloca, locBldr);
				this.iterCurr.LoopToEnd(lblOnEnd);
				this.EndNestedIterator(ndDod.Child);
				this.helper.Call(XmlILMethods.DodMergeSeq);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), true);
				return true;
			}
			return false;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0011CFD0 File Offset: 0x0011B1D0
		protected override QilNode VisitInvoke(QilInvoke ndInvoke)
		{
			QilFunction function = ndInvoke.Function;
			MethodInfo functionBinding = XmlILAnnotation.Write(function).FunctionBinding;
			bool flag = XmlILConstructInfo.Read(function).ConstructMethod == XmlILConstructMethod.Writer;
			this.helper.LoadQueryRuntime();
			for (int i = 0; i < ndInvoke.Arguments.Count; i++)
			{
				QilNode nd = ndInvoke.Arguments[i];
				QilNode qilNode = ndInvoke.Function.Arguments[i];
				this.NestedVisitEnsureStack(nd, this.GetItemStorageType(qilNode), !qilNode.XmlType.IsSingleton);
			}
			if (OptimizerPatterns.Read(ndInvoke).MatchesPattern(OptimizerPatternName.TailCall))
			{
				this.helper.TailCall(functionBinding);
			}
			else
			{
				this.helper.Call(functionBinding);
			}
			if (!flag)
			{
				this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndInvoke), !ndInvoke.XmlType.IsSingleton);
			}
			else
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			return ndInvoke;
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x0011D0C0 File Offset: 0x0011B2C0
		protected override QilNode VisitContent(QilUnary ndContent)
		{
			this.CreateSimpleIterator(ndContent.Child, "$$$iterAttrContent", typeof(AttributeContentIterator), XmlILMethods.AttrContentCreate, XmlILMethods.AttrContentNext);
			return ndContent;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x0011D0E8 File Offset: 0x0011B2E8
		protected override QilNode VisitAttribute(QilBinary ndAttr)
		{
			QilName qilName = ndAttr.Right as QilName;
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navAttr", typeof(XPathNavigator));
			this.SyncToNavigator(localBuilder, ndAttr.Left);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.LocalName));
			this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.NamespaceUri));
			this.helper.Call(XmlILMethods.NavMoveAttr);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndAttr;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x0011D1C4 File Offset: 0x0011B3C4
		protected override QilNode VisitParent(QilUnary ndParent)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navParent", typeof(XPathNavigator));
			this.SyncToNavigator(localBuilder, ndParent.Child);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Call(XmlILMethods.NavMoveParent);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndParent;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x0011D254 File Offset: 0x0011B454
		protected override QilNode VisitRoot(QilUnary ndRoot)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navRoot", typeof(XPathNavigator));
			this.SyncToNavigator(localBuilder, ndRoot.Child);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Call(XmlILMethods.NavMoveRoot);
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndRoot;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x0011D2C7 File Offset: 0x0011B4C7
		protected override QilNode VisitXmlContext(QilNode ndCtxt)
		{
			this.helper.LoadQueryContext();
			this.helper.Call(XmlILMethods.GetDefaultDataSource);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), false);
			return ndCtxt;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0011D300 File Offset: 0x0011B500
		protected override QilNode VisitDescendant(QilUnary ndDesc)
		{
			this.CreateFilteredIterator(ndDesc.Child, "$$$iterDesc", typeof(DescendantIterator), XmlILMethods.DescCreate, XmlILMethods.DescNext, XmlNodeKindFlags.Any, null, TriState.False, null);
			return ndDesc;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x0011D338 File Offset: 0x0011B538
		protected override QilNode VisitDescendantOrSelf(QilUnary ndDesc)
		{
			this.CreateFilteredIterator(ndDesc.Child, "$$$iterDesc", typeof(DescendantIterator), XmlILMethods.DescCreate, XmlILMethods.DescNext, XmlNodeKindFlags.Any, null, TriState.True, null);
			return ndDesc;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x0011D370 File Offset: 0x0011B570
		protected override QilNode VisitAncestor(QilUnary ndAnc)
		{
			this.CreateFilteredIterator(ndAnc.Child, "$$$iterAnc", typeof(AncestorIterator), XmlILMethods.AncCreate, XmlILMethods.AncNext, XmlNodeKindFlags.Any, null, TriState.False, null);
			return ndAnc;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0011D3A8 File Offset: 0x0011B5A8
		protected override QilNode VisitAncestorOrSelf(QilUnary ndAnc)
		{
			this.CreateFilteredIterator(ndAnc.Child, "$$$iterAnc", typeof(AncestorIterator), XmlILMethods.AncCreate, XmlILMethods.AncNext, XmlNodeKindFlags.Any, null, TriState.True, null);
			return ndAnc;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0011D3E0 File Offset: 0x0011B5E0
		protected override QilNode VisitPreceding(QilUnary ndPrec)
		{
			this.CreateFilteredIterator(ndPrec.Child, "$$$iterPrec", typeof(PrecedingIterator), XmlILMethods.PrecCreate, XmlILMethods.PrecNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndPrec;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x0011D418 File Offset: 0x0011B618
		protected override QilNode VisitFollowingSibling(QilUnary ndFollSib)
		{
			this.CreateFilteredIterator(ndFollSib.Child, "$$$iterFollSib", typeof(FollowingSiblingIterator), XmlILMethods.FollSibCreate, XmlILMethods.FollSibNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndFollSib;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x0011D450 File Offset: 0x0011B650
		protected override QilNode VisitPrecedingSibling(QilUnary ndPreSib)
		{
			this.CreateFilteredIterator(ndPreSib.Child, "$$$iterPreSib", typeof(PrecedingSiblingIterator), XmlILMethods.PreSibCreate, XmlILMethods.PreSibNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndPreSib;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0011D488 File Offset: 0x0011B688
		protected override QilNode VisitNodeRange(QilBinary ndRange)
		{
			this.CreateFilteredIterator(ndRange.Left, "$$$iterRange", typeof(NodeRangeIterator), XmlILMethods.NodeRangeCreate, XmlILMethods.NodeRangeNext, XmlNodeKindFlags.Any, null, TriState.Unknown, ndRange.Right);
			return ndRange;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0011D4C8 File Offset: 0x0011B6C8
		protected override QilNode VisitDeref(QilBinary ndDeref)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$iterId", typeof(IdIterator));
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.NestedVisitEnsureStack(ndDeref.Left);
			this.NestedVisitEnsureStack(ndDeref.Right);
			this.helper.Call(XmlILMethods.IdCreate);
			this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, XmlILMethods.IdNext);
			return ndDeref;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x0011D540 File Offset: 0x0011B740
		protected override QilNode VisitElementCtor(QilBinary ndElem)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndElem);
			bool flag = this.CheckWithinContent(xmlILConstructInfo) || !xmlILConstructInfo.IsNamespaceInScope || this.ElementCachesAttributes(xmlILConstructInfo);
			if (XmlILConstructInfo.Read(ndElem.Right).FinalStates == PossibleXmlStates.Any)
			{
				flag = true;
			}
			if (xmlILConstructInfo.FinalStates == PossibleXmlStates.Any)
			{
				flag = true;
			}
			if (!flag)
			{
				this.BeforeStartChecks(ndElem);
			}
			GenerateNameType nameType = this.LoadNameAndType(XPathNodeType.Element, ndElem.Left, true, flag);
			this.helper.CallWriteStartElement(nameType, flag);
			this.NestedVisit(ndElem.Right);
			if (XmlILConstructInfo.Read(ndElem.Right).FinalStates == PossibleXmlStates.EnumAttrs && !flag)
			{
				this.helper.CallStartElementContent();
			}
			nameType = this.LoadNameAndType(XPathNodeType.Element, ndElem.Left, false, flag);
			this.helper.CallWriteEndElement(nameType, flag);
			if (!flag)
			{
				this.AfterEndChecks(ndElem);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndElem;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0011D61C File Offset: 0x0011B81C
		protected override QilNode VisitAttributeCtor(QilBinary ndAttr)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndAttr);
			bool flag = this.CheckEnumAttrs(xmlILConstructInfo) || !xmlILConstructInfo.IsNamespaceInScope;
			if (!flag)
			{
				this.BeforeStartChecks(ndAttr);
			}
			GenerateNameType nameType = this.LoadNameAndType(XPathNodeType.Attribute, ndAttr.Left, true, flag);
			this.helper.CallWriteStartAttribute(nameType, flag);
			this.NestedVisit(ndAttr.Right);
			this.helper.CallWriteEndAttribute(flag);
			if (!flag)
			{
				this.AfterEndChecks(ndAttr);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndAttr;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x0011D6A0 File Offset: 0x0011B8A0
		protected override QilNode VisitCommentCtor(QilUnary ndComment)
		{
			this.helper.CallWriteStartComment();
			this.NestedVisit(ndComment.Child);
			this.helper.CallWriteEndComment();
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndComment;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x0011D6D8 File Offset: 0x0011B8D8
		protected override QilNode VisitPICtor(QilBinary ndPI)
		{
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndPI.Left);
			this.helper.CallWriteStartPI();
			this.NestedVisit(ndPI.Right);
			this.helper.CallWriteEndPI();
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndPI;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x0011D72F File Offset: 0x0011B92F
		protected override QilNode VisitTextCtor(QilUnary ndText)
		{
			return this.VisitTextCtor(ndText, false);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0011D739 File Offset: 0x0011B939
		protected override QilNode VisitRawTextCtor(QilUnary ndText)
		{
			return this.VisitTextCtor(ndText, true);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0011D744 File Offset: 0x0011B944
		private QilNode VisitTextCtor(QilUnary ndText, bool disableOutputEscaping)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndText);
			PossibleXmlStates initialStates = xmlILConstructInfo.InitialStates;
			bool flag = initialStates - PossibleXmlStates.WithinAttr > 2 && this.CheckWithinContent(xmlILConstructInfo);
			if (!flag)
			{
				this.BeforeStartChecks(ndText);
			}
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndText.Child);
			switch (xmlILConstructInfo.InitialStates)
			{
			case PossibleXmlStates.WithinAttr:
				this.helper.CallWriteString(false, flag);
				break;
			case PossibleXmlStates.WithinComment:
				this.helper.Call(XmlILMethods.CommentText);
				break;
			case PossibleXmlStates.WithinPI:
				this.helper.Call(XmlILMethods.PIText);
				break;
			default:
				this.helper.CallWriteString(disableOutputEscaping, flag);
				break;
			}
			if (!flag)
			{
				this.AfterEndChecks(ndText);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndText;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0011D80A File Offset: 0x0011BA0A
		protected override QilNode VisitDocumentCtor(QilUnary ndDoc)
		{
			this.helper.CallWriteStartRoot();
			this.NestedVisit(ndDoc.Child);
			this.helper.CallWriteEndRoot();
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndDoc;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0011D840 File Offset: 0x0011BA40
		protected override QilNode VisitNamespaceDecl(QilBinary ndNmsp)
		{
			XmlILConstructInfo info = XmlILConstructInfo.Read(ndNmsp);
			bool flag = this.CheckEnumAttrs(info) || this.MightHaveNamespacesAfterAttributes(info);
			if (!flag)
			{
				this.BeforeStartChecks(ndNmsp);
			}
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndNmsp.Left);
			this.NestedVisitEnsureStack(ndNmsp.Right);
			this.helper.CallWriteNamespaceDecl(flag);
			if (!flag)
			{
				this.AfterEndChecks(ndNmsp);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndNmsp;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x0011D8BC File Offset: 0x0011BABC
		protected override QilNode VisitRtfCtor(QilBinary ndRtf)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndRtf);
			string text = (QilLiteral)ndRtf.Right;
			if (optimizerPatterns.MatchesPattern(OptimizerPatternName.SingleTextRtf))
			{
				this.helper.LoadQueryRuntime();
				this.NestedVisitEnsureStack((QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
				this.helper.Emit(OpCodes.Ldstr, text);
				this.helper.Call(XmlILMethods.RtfConstr);
			}
			else
			{
				this.helper.CallStartRtfConstruction(text);
				this.NestedVisit(ndRtf.Left);
				this.helper.CallEndRtfConstruction();
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), false);
			return ndRtf;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x0011D96A File Offset: 0x0011BB6A
		protected override QilNode VisitNameOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0011D96A File Offset: 0x0011BB6A
		protected override QilNode VisitLocalNameOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0011D96A File Offset: 0x0011BB6A
		protected override QilNode VisitNamespaceUriOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0011D96A File Offset: 0x0011BB6A
		protected override QilNode VisitPrefixOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x0011D974 File Offset: 0x0011BB74
		private QilNode VisitNodeProperty(QilUnary ndProp)
		{
			this.NestedVisitEnsureStack(ndProp.Child);
			switch (ndProp.NodeType)
			{
			case QilNodeType.NameOf:
				this.helper.Emit(OpCodes.Dup);
				this.helper.Call(XmlILMethods.NavLocalName);
				this.helper.Call(XmlILMethods.NavNmsp);
				this.helper.Construct(XmlILConstructors.QName);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XmlQualifiedName), false);
				break;
			case QilNodeType.LocalNameOf:
				this.helper.Call(XmlILMethods.NavLocalName);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
				break;
			case QilNodeType.NamespaceUriOf:
				this.helper.Call(XmlILMethods.NavNmsp);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
				break;
			case QilNodeType.PrefixOf:
				this.helper.Call(XmlILMethods.NavPrefix);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
				break;
			}
			return ndProp;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x0011DA98 File Offset: 0x0011BC98
		protected override QilNode VisitTypeAssert(QilTargetType ndTypeAssert)
		{
			if (!ndTypeAssert.Source.XmlType.IsSingleton && ndTypeAssert.XmlType.IsSingleton && !this.iterCurr.HasLabelNext)
			{
				Label label = this.helper.DefineLabel();
				this.helper.MarkLabel(label);
				this.NestedVisit(ndTypeAssert.Source, label);
			}
			else
			{
				this.Visit(ndTypeAssert.Source);
			}
			this.iterCurr.EnsureItemStorageType(ndTypeAssert.Source.XmlType, this.GetItemStorageType(ndTypeAssert));
			return ndTypeAssert;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0011DB24 File Offset: 0x0011BD24
		protected override QilNode VisitIsType(QilTargetType ndIsType)
		{
			XmlQueryType xmlType = ndIsType.Source.XmlType;
			XmlQueryType targetType = ndIsType.TargetType;
			if (xmlType.IsSingleton && targetType == XmlQueryTypeFactory.Node)
			{
				this.NestedVisitEnsureStack(ndIsType.Source);
				this.helper.Call(XmlILMethods.ItemIsNode);
				this.ZeroCompare(QilNodeType.Ne, true);
				return ndIsType;
			}
			if (this.MatchesNodeKinds(ndIsType, xmlType, targetType))
			{
				return ndIsType;
			}
			XmlTypeCode xmlTypeCode;
			if (targetType == XmlQueryTypeFactory.Double)
			{
				xmlTypeCode = XmlTypeCode.Double;
			}
			else if (targetType == XmlQueryTypeFactory.String)
			{
				xmlTypeCode = XmlTypeCode.String;
			}
			else if (targetType == XmlQueryTypeFactory.Boolean)
			{
				xmlTypeCode = XmlTypeCode.Boolean;
			}
			else if (targetType == XmlQueryTypeFactory.Node)
			{
				xmlTypeCode = XmlTypeCode.Node;
			}
			else
			{
				xmlTypeCode = XmlTypeCode.None;
			}
			if (xmlTypeCode != XmlTypeCode.None)
			{
				this.helper.LoadQueryRuntime();
				this.NestedVisitEnsureStack(ndIsType.Source, typeof(XPathItem), !xmlType.IsSingleton);
				this.helper.LoadInteger((int)xmlTypeCode);
				this.helper.Call(xmlType.IsSingleton ? XmlILMethods.ItemMatchesCode : XmlILMethods.SeqMatchesCode);
				this.ZeroCompare(QilNodeType.Ne, true);
				return ndIsType;
			}
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndIsType.Source, typeof(XPathItem), !xmlType.IsSingleton);
			this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(targetType));
			this.helper.Call(xmlType.IsSingleton ? XmlILMethods.ItemMatchesType : XmlILMethods.SeqMatchesType);
			this.ZeroCompare(QilNodeType.Ne, true);
			return ndIsType;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0011DC90 File Offset: 0x0011BE90
		private bool MatchesNodeKinds(QilTargetType ndIsType, XmlQueryType typDerived, XmlQueryType typBase)
		{
			bool flag = true;
			if (!typBase.IsNode || !typBase.IsSingleton)
			{
				return false;
			}
			if (!typDerived.IsNode || !typDerived.IsSingleton || !typDerived.IsNotRtf)
			{
				return false;
			}
			XmlNodeKindFlags xmlNodeKindFlags = XmlNodeKindFlags.None;
			foreach (XmlQueryType xmlQueryType in typBase)
			{
				if (xmlQueryType == XmlQueryTypeFactory.Element)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Element;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Attribute)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Attribute;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Text)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Text;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Document)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Document;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Comment)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Comment;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.PI)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.PI;
				}
				else
				{
					if (xmlQueryType != XmlQueryTypeFactory.Namespace)
					{
						return false;
					}
					xmlNodeKindFlags |= XmlNodeKindFlags.Namespace;
				}
			}
			xmlNodeKindFlags = (typDerived.NodeKinds & xmlNodeKindFlags);
			if (!Bits.ExactlyOne((uint)xmlNodeKindFlags))
			{
				xmlNodeKindFlags = (~xmlNodeKindFlags & XmlNodeKindFlags.Any);
				flag = !flag;
			}
			XPathNodeType xpathNodeType;
			if (xmlNodeKindFlags <= XmlNodeKindFlags.Comment)
			{
				switch (xmlNodeKindFlags)
				{
				case XmlNodeKindFlags.Document:
					xpathNodeType = XPathNodeType.Root;
					goto IL_14A;
				case XmlNodeKindFlags.Element:
					xpathNodeType = XPathNodeType.Element;
					goto IL_14A;
				case XmlNodeKindFlags.Document | XmlNodeKindFlags.Element:
					break;
				case XmlNodeKindFlags.Attribute:
					xpathNodeType = XPathNodeType.Attribute;
					goto IL_14A;
				default:
					if (xmlNodeKindFlags == XmlNodeKindFlags.Comment)
					{
						xpathNodeType = XPathNodeType.Comment;
						goto IL_14A;
					}
					break;
				}
			}
			else
			{
				if (xmlNodeKindFlags == XmlNodeKindFlags.PI)
				{
					xpathNodeType = XPathNodeType.ProcessingInstruction;
					goto IL_14A;
				}
				if (xmlNodeKindFlags == XmlNodeKindFlags.Namespace)
				{
					xpathNodeType = XPathNodeType.Namespace;
					goto IL_14A;
				}
			}
			this.helper.Emit(OpCodes.Ldc_I4_1);
			xpathNodeType = XPathNodeType.All;
			IL_14A:
			this.NestedVisitEnsureStack(ndIsType.Source);
			this.helper.Call(XmlILMethods.NavType);
			if (xpathNodeType == XPathNodeType.All)
			{
				this.helper.Emit(OpCodes.Shl);
				int num = 0;
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Document) != XmlNodeKindFlags.None)
				{
					num |= 1;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Element) != XmlNodeKindFlags.None)
				{
					num |= 2;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None)
				{
					num |= 4;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Text) != XmlNodeKindFlags.None)
				{
					num |= 112;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Comment) != XmlNodeKindFlags.None)
				{
					num |= 256;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.PI) != XmlNodeKindFlags.None)
				{
					num |= 128;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Namespace) != XmlNodeKindFlags.None)
				{
					num |= 8;
				}
				this.helper.LoadInteger(num);
				this.helper.Emit(OpCodes.And);
				this.ZeroCompare(flag ? QilNodeType.Ne : QilNodeType.Eq, false);
			}
			else
			{
				this.helper.LoadInteger((int)xpathNodeType);
				this.ClrCompare(flag ? QilNodeType.Eq : QilNodeType.Ne, XmlTypeCode.Int);
			}
			return true;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0011DEC8 File Offset: 0x0011C0C8
		protected override QilNode VisitIsEmpty(QilUnary ndIsEmpty)
		{
			if (this.CachesResult(ndIsEmpty.Child))
			{
				this.NestedVisitEnsureStack(ndIsEmpty.Child);
				this.helper.CallCacheCount(this.iterNested.Storage.ItemStorageType);
				BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
				if (currentBranchingContext != BranchingContext.OnTrue)
				{
					if (currentBranchingContext == BranchingContext.OnFalse)
					{
						this.helper.TestAndBranch(0, this.iterCurr.LabelBranch, OpCodes.Bne_Un);
					}
					else
					{
						Label label = this.helper.DefineLabel();
						this.helper.Emit(OpCodes.Brfalse_S, label);
						this.helper.ConvBranchToBool(label, true);
					}
				}
				else
				{
					this.helper.TestAndBranch(0, this.iterCurr.LabelBranch, OpCodes.Beq);
				}
			}
			else
			{
				Label label2 = this.helper.DefineLabel();
				IteratorDescriptor iteratorDescriptor = this.iterCurr;
				if (iteratorDescriptor.CurrentBranchingContext == BranchingContext.OnTrue)
				{
					this.StartNestedIterator(ndIsEmpty.Child, this.iterCurr.LabelBranch);
				}
				else
				{
					this.StartNestedIterator(ndIsEmpty.Child, label2);
				}
				this.Visit(ndIsEmpty.Child);
				this.iterCurr.EnsureNoCache();
				this.iterCurr.DiscardStack();
				switch (iteratorDescriptor.CurrentBranchingContext)
				{
				case BranchingContext.None:
					this.helper.ConvBranchToBool(label2, true);
					break;
				case BranchingContext.OnFalse:
					this.helper.EmitUnconditionalBranch(OpCodes.Br, iteratorDescriptor.LabelBranch);
					this.helper.MarkLabel(label2);
					break;
				}
				this.EndNestedIterator(ndIsEmpty.Child);
			}
			if (this.iterCurr.IsBranching)
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			return ndIsEmpty;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0011E094 File Offset: 0x0011C294
		protected override QilNode VisitXPathNodeValue(QilUnary ndVal)
		{
			if (ndVal.Child.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(ndVal.Child, typeof(XPathNavigator), false);
				this.helper.Call(XmlILMethods.Value);
			}
			else
			{
				Label label = this.helper.DefineLabel();
				this.StartNestedIterator(ndVal.Child, label);
				this.Visit(ndVal.Child);
				this.iterCurr.EnsureStackNoCache();
				this.helper.Call(XmlILMethods.Value);
				Label label2 = this.helper.DefineLabel();
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
				this.helper.MarkLabel(label);
				this.helper.Emit(OpCodes.Ldstr, "");
				this.helper.MarkLabel(label2);
				this.EndNestedIterator(ndVal.Child);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndVal;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x0011E194 File Offset: 0x0011C394
		protected override QilNode VisitXPathFollowing(QilUnary ndFoll)
		{
			this.CreateFilteredIterator(ndFoll.Child, "$$$iterFoll", typeof(XPathFollowingIterator), XmlILMethods.XPFollCreate, XmlILMethods.XPFollNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndFoll;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0011E1CC File Offset: 0x0011C3CC
		protected override QilNode VisitXPathPreceding(QilUnary ndPrec)
		{
			this.CreateFilteredIterator(ndPrec.Child, "$$$iterPrec", typeof(XPathPrecedingIterator), XmlILMethods.XPPrecCreate, XmlILMethods.XPPrecNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndPrec;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0011E204 File Offset: 0x0011C404
		protected override QilNode VisitXPathNamespace(QilUnary ndNmsp)
		{
			this.CreateSimpleIterator(ndNmsp.Child, "$$$iterNmsp", typeof(NamespaceIterator), XmlILMethods.NmspCreate, XmlILMethods.NmspNext);
			return ndNmsp;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0011E22C File Offset: 0x0011C42C
		protected override QilNode VisitXsltGenerateId(QilUnary ndGenId)
		{
			this.helper.LoadQueryRuntime();
			if (ndGenId.Child.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(ndGenId.Child, typeof(XPathNavigator), false);
				this.helper.Call(XmlILMethods.GenId);
			}
			else
			{
				Label label = this.helper.DefineLabel();
				this.StartNestedIterator(ndGenId.Child, label);
				this.Visit(ndGenId.Child);
				this.iterCurr.EnsureStackNoCache();
				this.iterCurr.EnsureItemStorageType(ndGenId.Child.XmlType, typeof(XPathNavigator));
				this.helper.Call(XmlILMethods.GenId);
				Label label2 = this.helper.DefineLabel();
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
				this.helper.MarkLabel(label);
				this.helper.Emit(OpCodes.Pop);
				this.helper.Emit(OpCodes.Ldstr, "");
				this.helper.MarkLabel(label2);
				this.EndNestedIterator(ndGenId.Child);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndGenId;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0011E368 File Offset: 0x0011C568
		protected override QilNode VisitXsltInvokeLateBound(QilInvokeLateBound ndInvoke)
		{
			LocalBuilder locBldr = this.helper.DeclareLocal("$$$args", typeof(IList<XPathItem>[]));
			QilName name = ndInvoke.Name;
			this.helper.LoadQueryContext();
			this.helper.Emit(OpCodes.Ldstr, name.LocalName);
			this.helper.Emit(OpCodes.Ldstr, name.NamespaceUri);
			this.helper.LoadInteger(ndInvoke.Arguments.Count);
			this.helper.Emit(OpCodes.Newarr, typeof(IList<XPathItem>));
			this.helper.Emit(OpCodes.Stloc, locBldr);
			for (int i = 0; i < ndInvoke.Arguments.Count; i++)
			{
				QilNode nd = ndInvoke.Arguments[i];
				this.helper.Emit(OpCodes.Ldloc, locBldr);
				this.helper.LoadInteger(i);
				this.helper.Emit(OpCodes.Ldelema, typeof(IList<XPathItem>));
				this.NestedVisitEnsureCache(nd, typeof(XPathItem));
				this.iterCurr.EnsureStack();
				this.helper.Emit(OpCodes.Stobj, typeof(IList<XPathItem>));
			}
			this.helper.Emit(OpCodes.Ldloc, locBldr);
			this.helper.Call(XmlILMethods.InvokeXsltLate);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), true);
			return ndInvoke;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0011E4E0 File Offset: 0x0011C6E0
		protected override QilNode VisitXsltInvokeEarlyBound(QilInvokeEarlyBound ndInvoke)
		{
			QilName name = ndInvoke.Name;
			XmlExtensionFunction xmlExtensionFunction = new XmlExtensionFunction(name.LocalName, name.NamespaceUri, ndInvoke.ClrMethod);
			Type clrReturnType = xmlExtensionFunction.ClrReturnType;
			Type storageType = this.GetStorageType(ndInvoke);
			if (clrReturnType != storageType && !ndInvoke.XmlType.IsEmpty)
			{
				this.helper.LoadQueryRuntime();
				this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(ndInvoke.XmlType));
			}
			if (!xmlExtensionFunction.Method.IsStatic)
			{
				if (name.NamespaceUri.Length == 0)
				{
					this.helper.LoadXsltLibrary();
				}
				else
				{
					this.helper.CallGetEarlyBoundObject(this.helper.StaticData.DeclareEarlyBound(name.NamespaceUri, xmlExtensionFunction.Method.DeclaringType), xmlExtensionFunction.Method.DeclaringType);
				}
			}
			for (int i = 0; i < ndInvoke.Arguments.Count; i++)
			{
				QilNode qilNode = ndInvoke.Arguments[i];
				XmlQueryType xmlArgumentType = xmlExtensionFunction.GetXmlArgumentType(i);
				Type clrArgumentType = xmlExtensionFunction.GetClrArgumentType(i);
				if (name.NamespaceUri.Length == 0)
				{
					Type itemStorageType = this.GetItemStorageType(qilNode);
					if (clrArgumentType == XmlILMethods.StorageMethods[itemStorageType].IListType)
					{
						this.NestedVisitEnsureStack(qilNode, itemStorageType, true);
					}
					else if (clrArgumentType == XmlILMethods.StorageMethods[typeof(XPathItem)].IListType)
					{
						this.NestedVisitEnsureStack(qilNode, typeof(XPathItem), true);
					}
					else if ((qilNode.XmlType.IsSingleton && clrArgumentType == itemStorageType) || qilNode.XmlType.TypeCode == XmlTypeCode.None)
					{
						this.NestedVisitEnsureStack(qilNode, clrArgumentType, false);
					}
					else if (qilNode.XmlType.IsSingleton && clrArgumentType == typeof(XPathItem))
					{
						this.NestedVisitEnsureStack(qilNode, typeof(XPathItem), false);
					}
				}
				else
				{
					Type storageType2 = this.GetStorageType(xmlArgumentType);
					if (xmlArgumentType.TypeCode == XmlTypeCode.Item || !clrArgumentType.IsAssignableFrom(storageType2))
					{
						this.helper.LoadQueryRuntime();
						this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(xmlArgumentType));
						this.NestedVisitEnsureStack(qilNode, this.GetItemStorageType(xmlArgumentType), !xmlArgumentType.IsSingleton);
						this.helper.TreatAs(storageType2, typeof(object));
						this.helper.LoadType(clrArgumentType);
						this.helper.Call(XmlILMethods.ChangeTypeXsltArg);
						this.helper.TreatAs(typeof(object), clrArgumentType);
					}
					else
					{
						this.NestedVisitEnsureStack(qilNode, this.GetItemStorageType(xmlArgumentType), !xmlArgumentType.IsSingleton);
					}
				}
			}
			this.helper.Call(xmlExtensionFunction.Method);
			if (ndInvoke.XmlType.IsEmpty)
			{
				this.helper.Emit(OpCodes.Ldsfld, XmlILMethods.StorageMethods[typeof(XPathItem)].SeqEmpty);
			}
			else if (clrReturnType != storageType)
			{
				this.helper.TreatAs(clrReturnType, typeof(object));
				this.helper.Call(XmlILMethods.ChangeTypeXsltResult);
				this.helper.TreatAs(typeof(object), storageType);
			}
			else if (name.NamespaceUri.Length != 0 && !clrReturnType.IsValueType)
			{
				Label label = this.helper.DefineLabel();
				this.helper.Emit(OpCodes.Dup);
				this.helper.Emit(OpCodes.Brtrue, label);
				this.helper.LoadQueryRuntime();
				this.helper.Emit(OpCodes.Ldstr, Res.GetString("Extension functions cannot return null values."));
				this.helper.Call(XmlILMethods.ThrowException);
				this.helper.MarkLabel(label);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndInvoke), !ndInvoke.XmlType.IsSingleton);
			return ndInvoke;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0011E900 File Offset: 0x0011CB00
		protected override QilNode VisitXsltCopy(QilBinary ndCopy)
		{
			Label label = this.helper.DefineLabel();
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndCopy.Left);
			this.helper.Call(XmlILMethods.StartCopy);
			this.helper.Emit(OpCodes.Brfalse, label);
			this.NestedVisit(ndCopy.Right);
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndCopy.Left);
			this.helper.Call(XmlILMethods.EndCopy);
			this.helper.MarkLabel(label);
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndCopy;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0011E9A1 File Offset: 0x0011CBA1
		protected override QilNode VisitXsltCopyOf(QilUnary ndCopyOf)
		{
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndCopyOf.Child);
			this.helper.Call(XmlILMethods.CopyOf);
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndCopyOf;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0011E9DC File Offset: 0x0011CBDC
		protected override QilNode VisitXsltConvert(QilTargetType ndConv)
		{
			XmlQueryType xmlType = ndConv.Source.XmlType;
			XmlQueryType targetType = ndConv.TargetType;
			MethodInfo methodInfo;
			if (this.GetXsltConvertMethod(xmlType, targetType, out methodInfo))
			{
				this.NestedVisitEnsureStack(ndConv.Source);
			}
			else
			{
				this.NestedVisitEnsureStack(ndConv.Source, typeof(XPathItem), !xmlType.IsSingleton);
				this.GetXsltConvertMethod(xmlType.IsSingleton ? XmlQueryTypeFactory.Item : XmlQueryTypeFactory.ItemS, targetType, out methodInfo);
			}
			if (methodInfo != null)
			{
				this.helper.Call(methodInfo);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(targetType), !targetType.IsSingleton);
			return ndConv;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x0011EA8C File Offset: 0x0011CC8C
		private bool GetXsltConvertMethod(XmlQueryType typSrc, XmlQueryType typDst, out MethodInfo meth)
		{
			meth = null;
			if (typDst == XmlQueryTypeFactory.BooleanX)
			{
				if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToBool;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToBool;
				}
			}
			else if (typDst == XmlQueryTypeFactory.DateTimeX)
			{
				if (typSrc == XmlQueryTypeFactory.StringX)
				{
					meth = XmlILMethods.StrToDT;
				}
			}
			else if (typDst == XmlQueryTypeFactory.DecimalX)
			{
				if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToDec;
				}
			}
			else if (typDst == XmlQueryTypeFactory.DoubleX)
			{
				if (typSrc == XmlQueryTypeFactory.DecimalX)
				{
					meth = XmlILMethods.DecToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.IntX)
				{
					meth = XmlILMethods.IntToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.LongX)
				{
					meth = XmlILMethods.LngToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.StringX)
				{
					meth = XmlILMethods.StrToDbl;
				}
			}
			else if (typDst == XmlQueryTypeFactory.IntX)
			{
				if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToInt;
				}
			}
			else if (typDst == XmlQueryTypeFactory.LongX)
			{
				if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToLng;
				}
			}
			else if (typDst == XmlQueryTypeFactory.NodeNotRtf)
			{
				if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToNode;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToNode;
				}
			}
			else if (typDst == XmlQueryTypeFactory.NodeSDod || typDst == XmlQueryTypeFactory.NodeNotRtfS)
			{
				if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToNodes;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToNodes;
				}
			}
			else if (typDst == XmlQueryTypeFactory.StringX)
			{
				if (typSrc == XmlQueryTypeFactory.DateTimeX)
				{
					meth = XmlILMethods.DTToStr;
				}
				else if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToStr;
				}
				else if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToStr;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToStr;
				}
			}
			return meth != null;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x0011EC82 File Offset: 0x0011CE82
		private void SyncToNavigator(LocalBuilder locNav, QilNode ndCtxt)
		{
			this.helper.Emit(OpCodes.Ldloc, locNav);
			this.NestedVisitEnsureStack(ndCtxt);
			this.helper.CallSyncToNavigator();
			this.helper.Emit(OpCodes.Stloc, locNav);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0011ECB8 File Offset: 0x0011CEB8
		private void CreateSimpleIterator(QilNode ndCtxt, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.NestedVisitEnsureStack(ndCtxt);
			this.helper.Call(methCreate);
			this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, methNext);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0011ED0C File Offset: 0x0011CF0C
		private void CreateFilteredIterator(QilNode ndCtxt, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext, XmlNodeKindFlags kinds, QilName ndName, TriState orSelf, QilNode ndEnd)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.NestedVisitEnsureStack(ndCtxt);
			this.LoadSelectFilter(kinds, ndName);
			if (orSelf != TriState.Unknown)
			{
				this.helper.LoadBoolean(orSelf == TriState.True);
			}
			if (ndEnd != null)
			{
				this.NestedVisitEnsureStack(ndEnd);
			}
			this.helper.Call(methCreate);
			this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, methNext);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0011ED8C File Offset: 0x0011CF8C
		private void CreateContainerIterator(QilUnary ndDod, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext, XmlNodeKindFlags kinds, QilName ndName, TriState orSelf)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			QilLoop qilLoop = (QilLoop)ndDod.Child;
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.LoadSelectFilter(kinds, ndName);
			if (orSelf != TriState.Unknown)
			{
				this.helper.LoadBoolean(orSelf == TriState.True);
			}
			this.helper.Call(methCreate);
			Label label = this.helper.DefineLabel();
			this.StartNestedIterator(qilLoop, label);
			this.StartBinding(qilLoop.Variable);
			this.EndBinding(qilLoop.Variable);
			this.EndNestedIterator(qilLoop.Variable);
			this.iterCurr.Storage = this.iterNested.Storage;
			this.GenerateContainerIterator(ndDod, localBuilder, label, methNext, typeof(XPathNavigator));
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0011EE54 File Offset: 0x0011D054
		private void GenerateSimpleIterator(Type itemStorageType, LocalBuilder locIter, MethodInfo methNext)
		{
			Label label = this.helper.DefineLabel();
			this.helper.MarkLabel(label);
			this.helper.Emit(OpCodes.Ldloca, locIter);
			this.helper.Call(methNext);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.SetIterator(label, StorageDescriptor.Current(locIter, itemStorageType));
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0011EEC4 File Offset: 0x0011D0C4
		private void GenerateContainerIterator(QilNode nd, LocalBuilder locIter, Label lblOnEndNested, MethodInfo methNext, Type itemStorageType)
		{
			Label label = this.helper.DefineLabel();
			this.iterCurr.EnsureNoStackNoCache(nd.XmlType.IsNode ? "$$$navInput" : "$$$itemInput");
			this.helper.Emit(OpCodes.Ldloca, locIter);
			this.iterCurr.PushValue();
			this.helper.EmitUnconditionalBranch(OpCodes.Br, label);
			this.helper.MarkLabel(lblOnEndNested);
			this.helper.Emit(OpCodes.Ldloca, locIter);
			this.helper.Emit(OpCodes.Ldnull);
			this.helper.MarkLabel(label);
			this.helper.Call(methNext);
			if (nd.XmlType.IsSingleton)
			{
				this.helper.LoadInteger(1);
				this.helper.Emit(OpCodes.Beq, this.iterNested.GetLabelNext());
				this.iterCurr.Storage = StorageDescriptor.Current(locIter, itemStorageType);
				return;
			}
			this.helper.Emit(OpCodes.Switch, new Label[]
			{
				this.iterCurr.GetLabelNext(),
				this.iterNested.GetLabelNext()
			});
			this.iterCurr.SetIterator(lblOnEndNested, StorageDescriptor.Current(locIter, itemStorageType));
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0011F00C File Offset: 0x0011D20C
		private GenerateNameType LoadNameAndType(XPathNodeType nodeType, QilNode ndName, bool isStart, bool callChk)
		{
			this.helper.LoadQueryOutput();
			GenerateNameType result = GenerateNameType.StackName;
			if (ndName.NodeType == QilNodeType.LiteralQName)
			{
				if (isStart || !callChk)
				{
					QilName qilName = ndName as QilName;
					string prefix = qilName.Prefix;
					string localName = qilName.LocalName;
					string namespaceUri = qilName.NamespaceUri;
					if (qilName.NamespaceUri.Length == 0)
					{
						this.helper.Emit(OpCodes.Ldstr, qilName.LocalName);
						return GenerateNameType.LiteralLocalName;
					}
					if (!ValidateNames.ValidateName(prefix, localName, namespaceUri, nodeType, ValidateNames.Flags.CheckPrefixMapping))
					{
						if (isStart)
						{
							this.helper.Emit(OpCodes.Ldstr, localName);
							this.helper.Emit(OpCodes.Ldstr, namespaceUri);
							this.helper.Construct(XmlILConstructors.QName);
							result = GenerateNameType.QName;
						}
					}
					else
					{
						this.helper.Emit(OpCodes.Ldstr, prefix);
						this.helper.Emit(OpCodes.Ldstr, localName);
						this.helper.Emit(OpCodes.Ldstr, namespaceUri);
						result = GenerateNameType.LiteralName;
					}
				}
			}
			else if (isStart)
			{
				if (ndName.NodeType == QilNodeType.NameOf)
				{
					this.NestedVisitEnsureStack((ndName as QilUnary).Child);
					result = GenerateNameType.CopiedName;
				}
				else if (ndName.NodeType == QilNodeType.StrParseQName)
				{
					this.VisitStrParseQName(ndName as QilBinary, true);
					if ((ndName as QilBinary).Right.XmlType.TypeCode == XmlTypeCode.String)
					{
						result = GenerateNameType.TagNameAndNamespace;
					}
					else
					{
						result = GenerateNameType.TagNameAndMappings;
					}
				}
				else
				{
					this.NestedVisitEnsureStack(ndName);
					result = GenerateNameType.QName;
				}
			}
			return result;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0011F16C File Offset: 0x0011D36C
		private bool TryZeroCompare(QilNodeType relOp, QilNode ndFirst, QilNode ndSecond)
		{
			switch (ndFirst.NodeType)
			{
			case QilNodeType.True:
				relOp = ((relOp == QilNodeType.Eq) ? QilNodeType.Ne : QilNodeType.Eq);
				goto IL_55;
			case QilNodeType.False:
				goto IL_55;
			case QilNodeType.LiteralInt32:
				if ((QilLiteral)ndFirst != 0)
				{
					return false;
				}
				goto IL_55;
			case QilNodeType.LiteralInt64:
				if ((QilLiteral)ndFirst != 0)
				{
					return false;
				}
				goto IL_55;
			}
			return false;
			IL_55:
			this.NestedVisitEnsureStack(ndSecond);
			this.ZeroCompare(relOp, ndSecond.XmlType.TypeCode == XmlTypeCode.Boolean);
			return true;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0011F1EC File Offset: 0x0011D3EC
		private bool TryNameCompare(QilNodeType relOp, QilNode ndFirst, QilNode ndSecond)
		{
			if (ndFirst.NodeType == QilNodeType.NameOf)
			{
				QilNodeType nodeType = ndSecond.NodeType;
				if (nodeType == QilNodeType.LiteralQName || nodeType == QilNodeType.NameOf)
				{
					this.helper.LoadQueryRuntime();
					this.NestedVisitEnsureStack((ndFirst as QilUnary).Child);
					if (ndSecond.NodeType == QilNodeType.LiteralQName)
					{
						QilName qilName = ndSecond as QilName;
						this.helper.LoadInteger(this.helper.StaticData.DeclareName(qilName.LocalName));
						this.helper.LoadInteger(this.helper.StaticData.DeclareName(qilName.NamespaceUri));
						this.helper.Call(XmlILMethods.QNameEqualLit);
					}
					else
					{
						this.NestedVisitEnsureStack(ndSecond);
						this.helper.Call(XmlILMethods.QNameEqualNav);
					}
					this.ZeroCompare((relOp == QilNodeType.Eq) ? QilNodeType.Ne : QilNodeType.Eq, true);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0011F2C8 File Offset: 0x0011D4C8
		private void ClrCompare(QilNodeType relOp, XmlTypeCode code)
		{
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			OpCode opcode;
			if (currentBranchingContext == BranchingContext.OnTrue)
			{
				switch (relOp)
				{
				case QilNodeType.Ne:
					opcode = OpCodes.Bne_Un;
					break;
				case QilNodeType.Eq:
					opcode = OpCodes.Beq;
					break;
				case QilNodeType.Gt:
					opcode = OpCodes.Bgt;
					break;
				case QilNodeType.Ge:
					opcode = OpCodes.Bge;
					break;
				case QilNodeType.Lt:
					opcode = OpCodes.Blt;
					break;
				case QilNodeType.Le:
					opcode = OpCodes.Ble;
					break;
				default:
					opcode = OpCodes.Nop;
					break;
				}
				this.helper.Emit(opcode, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			if (currentBranchingContext == BranchingContext.OnFalse)
			{
				if (code == XmlTypeCode.Double || code == XmlTypeCode.Float)
				{
					switch (relOp)
					{
					case QilNodeType.Ne:
						opcode = OpCodes.Beq;
						break;
					case QilNodeType.Eq:
						opcode = OpCodes.Bne_Un;
						break;
					case QilNodeType.Gt:
						opcode = OpCodes.Ble_Un;
						break;
					case QilNodeType.Ge:
						opcode = OpCodes.Blt_Un;
						break;
					case QilNodeType.Lt:
						opcode = OpCodes.Bge_Un;
						break;
					case QilNodeType.Le:
						opcode = OpCodes.Bgt_Un;
						break;
					default:
						opcode = OpCodes.Nop;
						break;
					}
				}
				else
				{
					switch (relOp)
					{
					case QilNodeType.Ne:
						opcode = OpCodes.Beq;
						break;
					case QilNodeType.Eq:
						opcode = OpCodes.Bne_Un;
						break;
					case QilNodeType.Gt:
						opcode = OpCodes.Ble;
						break;
					case QilNodeType.Ge:
						opcode = OpCodes.Blt;
						break;
					case QilNodeType.Lt:
						opcode = OpCodes.Bge;
						break;
					case QilNodeType.Le:
						opcode = OpCodes.Bgt;
						break;
					default:
						opcode = OpCodes.Nop;
						break;
					}
				}
				this.helper.Emit(opcode, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			switch (relOp)
			{
			case QilNodeType.Eq:
				this.helper.Emit(OpCodes.Ceq);
				goto IL_22D;
			case QilNodeType.Gt:
				this.helper.Emit(OpCodes.Cgt);
				goto IL_22D;
			case QilNodeType.Lt:
				this.helper.Emit(OpCodes.Clt);
				goto IL_22D;
			}
			if (relOp != QilNodeType.Ne)
			{
				if (relOp != QilNodeType.Ge)
				{
					if (relOp != QilNodeType.Le)
					{
						opcode = OpCodes.Nop;
					}
					else
					{
						opcode = OpCodes.Ble_S;
					}
				}
				else
				{
					opcode = OpCodes.Bge_S;
				}
			}
			else
			{
				opcode = OpCodes.Bne_Un_S;
			}
			Label label = this.helper.DefineLabel();
			this.helper.Emit(opcode, label);
			this.helper.ConvBranchToBool(label, true);
			IL_22D:
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0011F520 File Offset: 0x0011D720
		private void ZeroCompare(QilNodeType relOp, bool isBoolVal)
		{
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			if (currentBranchingContext == BranchingContext.OnTrue)
			{
				this.helper.Emit((relOp == QilNodeType.Eq) ? OpCodes.Brfalse : OpCodes.Brtrue, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			if (currentBranchingContext != BranchingContext.OnFalse)
			{
				if (!isBoolVal || relOp == QilNodeType.Eq)
				{
					Label label = this.helper.DefineLabel();
					this.helper.Emit((relOp == QilNodeType.Eq) ? OpCodes.Brfalse : OpCodes.Brtrue, label);
					this.helper.ConvBranchToBool(label, true);
				}
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
				return;
			}
			this.helper.Emit((relOp == QilNodeType.Eq) ? OpCodes.Brtrue : OpCodes.Brfalse, this.iterCurr.LabelBranch);
			this.iterCurr.Storage = StorageDescriptor.None();
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0011F60C File Offset: 0x0011D80C
		private void StartWriterLoop(QilNode nd, out bool hasOnEnd, out Label lblOnEnd)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(nd);
			hasOnEnd = false;
			lblOnEnd = default(Label);
			if (!xmlILConstructInfo.PushToWriterLast || nd.XmlType.IsSingleton)
			{
				return;
			}
			if (!this.iterCurr.HasLabelNext)
			{
				hasOnEnd = true;
				lblOnEnd = this.helper.DefineLabel();
				this.iterCurr.SetIterator(lblOnEnd, StorageDescriptor.None());
			}
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0011F675 File Offset: 0x0011D875
		private void EndWriterLoop(QilNode nd, bool hasOnEnd, Label lblOnEnd)
		{
			if (!XmlILConstructInfo.Read(nd).PushToWriterLast)
			{
				return;
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			if (nd.XmlType.IsSingleton)
			{
				return;
			}
			if (hasOnEnd)
			{
				this.iterCurr.LoopToEnd(lblOnEnd);
			}
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x0011F6B2 File Offset: 0x0011D8B2
		private bool MightHaveNamespacesAfterAttributes(XmlILConstructInfo info)
		{
			if (info != null)
			{
				info = info.ParentElementInfo;
			}
			return info == null || info.MightHaveNamespacesAfterAttributes;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0011F6CA File Offset: 0x0011D8CA
		private bool ElementCachesAttributes(XmlILConstructInfo info)
		{
			return info.MightHaveDuplicateAttributes || info.MightHaveNamespacesAfterAttributes;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x0011F6DC File Offset: 0x0011D8DC
		private void BeforeStartChecks(QilNode ndCtor)
		{
			PossibleXmlStates initialStates = XmlILConstructInfo.Read(ndCtor).InitialStates;
			if (initialStates == PossibleXmlStates.WithinSequence)
			{
				this.helper.CallStartTree(this.QilConstructorToNodeType(ndCtor.NodeType));
				return;
			}
			if (initialStates != PossibleXmlStates.EnumAttrs)
			{
				return;
			}
			QilNodeType nodeType = ndCtor.NodeType;
			if (nodeType == QilNodeType.ElementCtor || nodeType - QilNodeType.CommentCtor <= 3)
			{
				this.helper.CallStartElementContent();
			}
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x0011F734 File Offset: 0x0011D934
		private void AfterEndChecks(QilNode ndCtor)
		{
			if (XmlILConstructInfo.Read(ndCtor).FinalStates == PossibleXmlStates.WithinSequence)
			{
				this.helper.CallEndTree();
			}
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x0011F750 File Offset: 0x0011D950
		private bool CheckWithinContent(XmlILConstructInfo info)
		{
			PossibleXmlStates initialStates = info.InitialStates;
			return initialStates - PossibleXmlStates.WithinSequence > 2;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0011F770 File Offset: 0x0011D970
		private bool CheckEnumAttrs(XmlILConstructInfo info)
		{
			PossibleXmlStates initialStates = info.InitialStates;
			return initialStates - PossibleXmlStates.WithinSequence > 1;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x0011F78D File Offset: 0x0011D98D
		private XPathNodeType QilXmlToXPathNodeType(XmlNodeKindFlags xmlTypes)
		{
			if (xmlTypes <= XmlNodeKindFlags.Attribute)
			{
				if (xmlTypes == XmlNodeKindFlags.Element)
				{
					return XPathNodeType.Element;
				}
				if (xmlTypes == XmlNodeKindFlags.Attribute)
				{
					return XPathNodeType.Attribute;
				}
			}
			else
			{
				if (xmlTypes == XmlNodeKindFlags.Text)
				{
					return XPathNodeType.Text;
				}
				if (xmlTypes == XmlNodeKindFlags.Comment)
				{
					return XPathNodeType.Comment;
				}
			}
			return XPathNodeType.ProcessingInstruction;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0011F7B1 File Offset: 0x0011D9B1
		private XPathNodeType QilConstructorToNodeType(QilNodeType typ)
		{
			switch (typ)
			{
			case QilNodeType.ElementCtor:
				return XPathNodeType.Element;
			case QilNodeType.AttributeCtor:
				return XPathNodeType.Attribute;
			case QilNodeType.CommentCtor:
				return XPathNodeType.Comment;
			case QilNodeType.PICtor:
				return XPathNodeType.ProcessingInstruction;
			case QilNodeType.TextCtor:
				return XPathNodeType.Text;
			case QilNodeType.RawTextCtor:
				return XPathNodeType.Text;
			case QilNodeType.DocumentCtor:
				return XPathNodeType.Root;
			case QilNodeType.NamespaceDecl:
				return XPathNodeType.Namespace;
			default:
				return XPathNodeType.All;
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x0011F7F0 File Offset: 0x0011D9F0
		private void LoadSelectFilter(XmlNodeKindFlags xmlTypes, QilName ndName)
		{
			if (ndName != null)
			{
				this.helper.CallGetNameFilter(this.helper.StaticData.DeclareNameFilter(ndName.LocalName, ndName.NamespaceUri));
				return;
			}
			if (!XmlILVisitor.IsNodeTypeUnion(xmlTypes))
			{
				this.helper.CallGetTypeFilter(this.QilXmlToXPathNodeType(xmlTypes));
				return;
			}
			if ((xmlTypes & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None)
			{
				this.helper.CallGetTypeFilter(XPathNodeType.All);
				return;
			}
			this.helper.CallGetTypeFilter(XPathNodeType.Attribute);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0000B14B File Offset: 0x0000934B
		private static bool IsNodeTypeUnion(XmlNodeKindFlags xmlTypes)
		{
			return (xmlTypes & xmlTypes - 1) > XmlNodeKindFlags.None;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x0011F868 File Offset: 0x0011DA68
		private void StartNestedIterator(QilNode nd)
		{
			IteratorDescriptor iteratorDescriptor = this.iterCurr;
			if (iteratorDescriptor == null)
			{
				this.iterCurr = new IteratorDescriptor(this.helper);
			}
			else
			{
				this.iterCurr = new IteratorDescriptor(iteratorDescriptor);
			}
			this.iterNested = null;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x0011F8A5 File Offset: 0x0011DAA5
		private void StartNestedIterator(QilNode nd, Label lblOnEnd)
		{
			this.StartNestedIterator(nd);
			this.iterCurr.SetIterator(lblOnEnd, StorageDescriptor.None());
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x0011F8C0 File Offset: 0x0011DAC0
		private void EndNestedIterator(QilNode nd)
		{
			if (this.iterCurr.IsBranching && this.iterCurr.Storage.Location != ItemLocation.None)
			{
				this.iterCurr.EnsureItemStorageType(nd.XmlType, typeof(bool));
				this.iterCurr.EnsureStackNoCache();
				if (this.iterCurr.CurrentBranchingContext == BranchingContext.OnTrue)
				{
					this.helper.Emit(OpCodes.Brtrue, this.iterCurr.LabelBranch);
				}
				else
				{
					this.helper.Emit(OpCodes.Brfalse, this.iterCurr.LabelBranch);
				}
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			this.iterNested = this.iterCurr;
			this.iterCurr = this.iterCurr.ParentIterator;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x0011F98C File Offset: 0x0011DB8C
		private void NestedVisit(QilNode nd, Type itemStorageType, bool isCached)
		{
			if (XmlILConstructInfo.Read(nd).PushToWriterLast)
			{
				this.StartNestedIterator(nd);
				this.Visit(nd);
				this.EndNestedIterator(nd);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			if (!isCached && nd.XmlType.IsSingleton)
			{
				this.StartNestedIterator(nd);
				this.Visit(nd);
				this.iterCurr.EnsureNoCache();
				this.iterCurr.EnsureItemStorageType(nd.XmlType, itemStorageType);
				this.EndNestedIterator(nd);
				this.iterCurr.Storage = this.iterNested.Storage;
				return;
			}
			this.NestedVisitEnsureCache(nd, itemStorageType);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x0011FA2F File Offset: 0x0011DC2F
		private void NestedVisit(QilNode nd)
		{
			this.NestedVisit(nd, this.GetItemStorageType(nd), !nd.XmlType.IsSingleton);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x0011FA50 File Offset: 0x0011DC50
		private void NestedVisit(QilNode nd, Label lblOnEnd)
		{
			this.StartNestedIterator(nd, lblOnEnd);
			this.Visit(nd);
			this.iterCurr.EnsureNoCache();
			this.iterCurr.EnsureItemStorageType(nd.XmlType, this.GetItemStorageType(nd));
			this.EndNestedIterator(nd);
			this.iterCurr.Storage = this.iterNested.Storage;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x0011FAAD File Offset: 0x0011DCAD
		private void NestedVisitEnsureStack(QilNode nd)
		{
			this.NestedVisit(nd);
			this.iterCurr.EnsureStack();
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x0011FAC1 File Offset: 0x0011DCC1
		private void NestedVisitEnsureStack(QilNode ndLeft, QilNode ndRight)
		{
			this.NestedVisitEnsureStack(ndLeft);
			this.NestedVisitEnsureStack(ndRight);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0011FAD1 File Offset: 0x0011DCD1
		private void NestedVisitEnsureStack(QilNode nd, Type itemStorageType, bool isCached)
		{
			this.NestedVisit(nd, itemStorageType, isCached);
			this.iterCurr.EnsureStack();
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0011FAE7 File Offset: 0x0011DCE7
		private void NestedVisitEnsureLocal(QilNode nd, LocalBuilder loc)
		{
			this.NestedVisit(nd);
			this.iterCurr.EnsureLocal(loc);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0011FAFC File Offset: 0x0011DCFC
		private void NestedVisitWithBranch(QilNode nd, BranchingContext brctxt, Label lblBranch)
		{
			this.StartNestedIterator(nd);
			this.iterCurr.SetBranching(brctxt, lblBranch);
			this.Visit(nd);
			this.EndNestedIterator(nd);
			this.iterCurr.Storage = StorageDescriptor.None();
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x0011FB34 File Offset: 0x0011DD34
		private void NestedVisitEnsureCache(QilNode nd, Type itemStorageType)
		{
			bool flag = this.CachesResult(nd);
			Label lblOnEnd = this.helper.DefineLabel();
			if (flag)
			{
				this.StartNestedIterator(nd);
				this.Visit(nd);
				this.EndNestedIterator(nd);
				this.iterCurr.Storage = this.iterNested.Storage;
				if (this.iterCurr.Storage.ItemStorageType == itemStorageType)
				{
					return;
				}
				if (this.iterCurr.Storage.ItemStorageType == typeof(XPathNavigator) || itemStorageType == typeof(XPathNavigator))
				{
					this.iterCurr.EnsureItemStorageType(nd.XmlType, itemStorageType);
					return;
				}
				this.iterCurr.EnsureNoStack("$$$cacheResult");
			}
			Type type = (this.GetItemStorageType(nd) == typeof(XPathNavigator)) ? typeof(XPathNavigator) : itemStorageType;
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[type];
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$cache", xmlILStorageMethods.SeqType);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			if (nd.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(nd, type, false);
				this.helper.CallToken(xmlILStorageMethods.SeqReuseSgl);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
			}
			else
			{
				this.helper.CallToken(xmlILStorageMethods.SeqReuse);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.StartNestedIterator(nd, lblOnEnd);
				if (flag)
				{
					this.iterCurr.Storage = this.iterCurr.ParentIterator.Storage;
				}
				else
				{
					this.Visit(nd);
				}
				this.iterCurr.EnsureItemStorageType(nd.XmlType, type);
				this.iterCurr.EnsureStackNoCache();
				this.helper.Call(xmlILStorageMethods.SeqAdd);
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.iterCurr.LoopToEnd(lblOnEnd);
				this.EndNestedIterator(nd);
				this.helper.Emit(OpCodes.Pop);
			}
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, itemStorageType, true);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0011FD74 File Offset: 0x0011DF74
		private bool CachesResult(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			if (nodeType <= QilNodeType.DocOrderDistinct)
			{
				if (nodeType - QilNodeType.Let > 1)
				{
					OptimizerPatterns optimizerPatterns;
					if (nodeType == QilNodeType.Filter)
					{
						optimizerPatterns = OptimizerPatterns.Read(nd);
						return optimizerPatterns.MatchesPattern(OptimizerPatternName.EqualityIndex);
					}
					if (nodeType != QilNodeType.DocOrderDistinct)
					{
						return false;
					}
					if (nd.XmlType.IsSingleton)
					{
						return false;
					}
					optimizerPatterns = OptimizerPatterns.Read(nd);
					return !optimizerPatterns.MatchesPattern(OptimizerPatternName.JoinAndDod) && !optimizerPatterns.MatchesPattern(OptimizerPatternName.DodReverse);
				}
			}
			else if (nodeType != QilNodeType.Invoke)
			{
				if (nodeType == QilNodeType.TypeAssert)
				{
					QilTargetType qilTargetType = (QilTargetType)nd;
					return this.CachesResult(qilTargetType.Source) && this.GetItemStorageType(qilTargetType.Source) == this.GetItemStorageType(qilTargetType);
				}
				if (nodeType - QilNodeType.XsltInvokeLateBound > 1)
				{
					return false;
				}
			}
			return !nd.XmlType.IsSingleton;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0011FE2F File Offset: 0x0011E02F
		private Type GetStorageType(QilNode nd)
		{
			return XmlILTypeHelper.GetStorageType(nd.XmlType);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x0011FE3C File Offset: 0x0011E03C
		private Type GetStorageType(XmlQueryType typ)
		{
			return XmlILTypeHelper.GetStorageType(typ);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x0011FE44 File Offset: 0x0011E044
		private Type GetItemStorageType(QilNode nd)
		{
			return XmlILTypeHelper.GetStorageType(nd.XmlType.Prime);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x0011FE56 File Offset: 0x0011E056
		private Type GetItemStorageType(XmlQueryType typ)
		{
			return XmlILTypeHelper.GetStorageType(typ.Prime);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00119441 File Offset: 0x00117641
		public XmlILVisitor()
		{
		}

		// Token: 0x040025B4 RID: 9652
		private QilExpression qil;

		// Token: 0x040025B5 RID: 9653
		private GenerateHelper helper;

		// Token: 0x040025B6 RID: 9654
		private IteratorDescriptor iterCurr;

		// Token: 0x040025B7 RID: 9655
		private IteratorDescriptor iterNested;

		// Token: 0x040025B8 RID: 9656
		private int indexId;
	}
}
