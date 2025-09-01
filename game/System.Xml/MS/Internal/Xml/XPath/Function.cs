using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000631 RID: 1585
	internal class Function : AstNode
	{
		// Token: 0x060040B0 RID: 16560 RVA: 0x001652BC File Offset: 0x001634BC
		public Function(Function.FunctionType ftype, List<AstNode> argumentList)
		{
			this._functionType = ftype;
			this._argumentList = new List<AstNode>(argumentList);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x001652D7 File Offset: 0x001634D7
		public Function(string prefix, string name, List<AstNode> argumentList)
		{
			this._functionType = Function.FunctionType.FuncUserDefined;
			this._prefix = prefix;
			this._name = name;
			this._argumentList = new List<AstNode>(argumentList);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x00165301 File Offset: 0x00163501
		public Function(Function.FunctionType ftype, AstNode arg)
		{
			this._functionType = ftype;
			this._argumentList = new List<AstNode>();
			this._argumentList.Add(arg);
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x00067362 File Offset: 0x00065562
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Function;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x00165327 File Offset: 0x00163527
		public override XPathResultType ReturnType
		{
			get
			{
				return Function.ReturnTypes[(int)this._functionType];
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x00165335 File Offset: 0x00163535
		public Function.FunctionType TypeOfFunction
		{
			get
			{
				return this._functionType;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x0016533D File Offset: 0x0016353D
		public List<AstNode> ArgumentList
		{
			get
			{
				return this._argumentList;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x00165345 File Offset: 0x00163545
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x0016534D File Offset: 0x0016354D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x00165355 File Offset: 0x00163555
		// Note: this type is marked as 'beforefieldinit'.
		static Function()
		{
		}

		// Token: 0x04002E1D RID: 11805
		private Function.FunctionType _functionType;

		// Token: 0x04002E1E RID: 11806
		private List<AstNode> _argumentList;

		// Token: 0x04002E1F RID: 11807
		private string _name;

		// Token: 0x04002E20 RID: 11808
		private string _prefix;

		// Token: 0x04002E21 RID: 11809
		internal static XPathResultType[] ReturnTypes = new XPathResultType[]
		{
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.NodeSet,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Number,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Any
		};

		// Token: 0x02000632 RID: 1586
		public enum FunctionType
		{
			// Token: 0x04002E23 RID: 11811
			FuncLast,
			// Token: 0x04002E24 RID: 11812
			FuncPosition,
			// Token: 0x04002E25 RID: 11813
			FuncCount,
			// Token: 0x04002E26 RID: 11814
			FuncID,
			// Token: 0x04002E27 RID: 11815
			FuncLocalName,
			// Token: 0x04002E28 RID: 11816
			FuncNameSpaceUri,
			// Token: 0x04002E29 RID: 11817
			FuncName,
			// Token: 0x04002E2A RID: 11818
			FuncString,
			// Token: 0x04002E2B RID: 11819
			FuncBoolean,
			// Token: 0x04002E2C RID: 11820
			FuncNumber,
			// Token: 0x04002E2D RID: 11821
			FuncTrue,
			// Token: 0x04002E2E RID: 11822
			FuncFalse,
			// Token: 0x04002E2F RID: 11823
			FuncNot,
			// Token: 0x04002E30 RID: 11824
			FuncConcat,
			// Token: 0x04002E31 RID: 11825
			FuncStartsWith,
			// Token: 0x04002E32 RID: 11826
			FuncContains,
			// Token: 0x04002E33 RID: 11827
			FuncSubstringBefore,
			// Token: 0x04002E34 RID: 11828
			FuncSubstringAfter,
			// Token: 0x04002E35 RID: 11829
			FuncSubstring,
			// Token: 0x04002E36 RID: 11830
			FuncStringLength,
			// Token: 0x04002E37 RID: 11831
			FuncNormalize,
			// Token: 0x04002E38 RID: 11832
			FuncTranslate,
			// Token: 0x04002E39 RID: 11833
			FuncLang,
			// Token: 0x04002E3A RID: 11834
			FuncSum,
			// Token: 0x04002E3B RID: 11835
			FuncFloor,
			// Token: 0x04002E3C RID: 11836
			FuncCeiling,
			// Token: 0x04002E3D RID: 11837
			FuncRound,
			// Token: 0x04002E3E RID: 11838
			FuncUserDefined
		}
	}
}
