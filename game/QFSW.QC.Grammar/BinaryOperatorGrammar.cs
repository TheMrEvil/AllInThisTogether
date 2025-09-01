using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using QFSW.QC.Utilities;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000005 RID: 5
	public abstract class BinaryOperatorGrammar : IQcGrammarConstruct
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14
		public abstract int Precedence { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15
		protected abstract char OperatorToken { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16
		protected abstract string OperatorMethodName { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17
		protected abstract Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002258 File Offset: 0x00000458
		public bool Match(string value, Type type)
		{
			if (this._missingOperatorTable.Contains(type))
			{
				return false;
			}
			if (!this.IsSyntaxMatch(value))
			{
				return false;
			}
			if (this._foundOperatorTable.ContainsKey(type))
			{
				return true;
			}
			IBinaryOperator operatorData = this.GetOperatorData(type);
			if (operatorData != null)
			{
				this._foundOperatorTable.Add(type, operatorData);
				return true;
			}
			this._missingOperatorTable.Add(type);
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B8 File Offset: 0x000004B8
		private bool IsSyntaxMatch(string value)
		{
			if (this._operatorRegex == null)
			{
				this._operatorRegex = new Regex(string.Format("^.+\\{0}.+$", this.OperatorToken));
			}
			if (!this._operatorRegex.IsMatch(value))
			{
				return false;
			}
			int operatorPosition = this.GetOperatorPosition(value);
			return operatorPosition > 0 && operatorPosition < value.Length;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002314 File Offset: 0x00000514
		private IBinaryOperator GetOperatorData(Type type)
		{
			if (type.IsPrimitive)
			{
				return this.GeneratePrimitiveOperator(type);
			}
			BinaryOperatorData[] source = (from x in type.GetMethods(BindingFlags.Static | BindingFlags.Public)
			where x.Name == this.OperatorMethodName
			where x.ReturnType == type
			where x.GetParameters().Length == 2
			select new BinaryOperatorData(x)).ToArray<BinaryOperatorData>();
			BinaryOperatorData result;
			if ((result = source.FirstOrDefault((BinaryOperatorData x) => x.LArg == type && x.RArg == type)) == null && (result = source.FirstOrDefault((BinaryOperatorData x) => x.LArg == type)) == null)
			{
				result = (source.FirstOrDefault((BinaryOperatorData x) => x.RArg == type) ?? source.FirstOrDefault<BinaryOperatorData>());
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002414 File Offset: 0x00000614
		private IBinaryOperator GeneratePrimitiveOperator(Type type)
		{
			ParameterExpression parameterExpression = Expression.Parameter(type, "left");
			ParameterExpression parameterExpression2 = Expression.Parameter(type, "right");
			BinaryExpression body;
			try
			{
				body = this.PrimitiveExpressionGenerator(parameterExpression, parameterExpression2);
			}
			catch (InvalidOperationException)
			{
				return null;
			}
			return new DynamicBinaryOperator(Expression.Lambda(body, true, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile(), type, type, type);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002484 File Offset: 0x00000684
		protected virtual int GetOperatorPosition(string value)
		{
			return TextProcessing.GetScopedSplitPoints<char[]>(value, this.OperatorToken, TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers).LastOr(-1);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024A4 File Offset: 0x000006A4
		public object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			IBinaryOperator binaryOperator = this._foundOperatorTable[type];
			int operatorPosition = this.GetOperatorPosition(value);
			string arg = value.Substring(0, operatorPosition);
			string arg2 = value.Substring(operatorPosition + 1);
			object left = recursiveParser(arg, binaryOperator.LArg);
			object right = recursiveParser(arg2, binaryOperator.RArg);
			object result;
			try
			{
				result = binaryOperator.Invoke(left, right);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException ?? ex;
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002528 File Offset: 0x00000728
		protected BinaryOperatorGrammar()
		{
		}

		// Token: 0x04000007 RID: 7
		private Regex _operatorRegex;

		// Token: 0x04000008 RID: 8
		private readonly HashSet<Type> _missingOperatorTable = new HashSet<Type>();

		// Token: 0x04000009 RID: 9
		private readonly Dictionary<Type, IBinaryOperator> _foundOperatorTable = new Dictionary<Type, IBinaryOperator>();

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x0600004D RID: 77 RVA: 0x00002823 File Offset: 0x00000A23
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x0600004E RID: 78 RVA: 0x0000282B File Offset: 0x00000A2B
			internal bool <GetOperatorData>b__0(MethodInfo x)
			{
				return x.Name == this.<>4__this.OperatorMethodName;
			}

			// Token: 0x0600004F RID: 79 RVA: 0x00002843 File Offset: 0x00000A43
			internal bool <GetOperatorData>b__1(MethodInfo x)
			{
				return x.ReturnType == this.type;
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002856 File Offset: 0x00000A56
			internal bool <GetOperatorData>b__4(BinaryOperatorData x)
			{
				return x.LArg == this.type && x.RArg == this.type;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x0000287E File Offset: 0x00000A7E
			internal bool <GetOperatorData>b__5(BinaryOperatorData x)
			{
				return x.LArg == this.type;
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00002891 File Offset: 0x00000A91
			internal bool <GetOperatorData>b__6(BinaryOperatorData x)
			{
				return x.RArg == this.type;
			}

			// Token: 0x04000010 RID: 16
			public BinaryOperatorGrammar <>4__this;

			// Token: 0x04000011 RID: 17
			public Type type;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000053 RID: 83 RVA: 0x000028A4 File Offset: 0x00000AA4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000054 RID: 84 RVA: 0x000028B0 File Offset: 0x00000AB0
			public <>c()
			{
			}

			// Token: 0x06000055 RID: 85 RVA: 0x000028B8 File Offset: 0x00000AB8
			internal bool <GetOperatorData>b__13_2(MethodInfo x)
			{
				return x.GetParameters().Length == 2;
			}

			// Token: 0x06000056 RID: 86 RVA: 0x000028C5 File Offset: 0x00000AC5
			internal BinaryOperatorData <GetOperatorData>b__13_3(MethodInfo x)
			{
				return new BinaryOperatorData(x);
			}

			// Token: 0x04000012 RID: 18
			public static readonly BinaryOperatorGrammar.<>c <>9 = new BinaryOperatorGrammar.<>c();

			// Token: 0x04000013 RID: 19
			public static Func<MethodInfo, bool> <>9__13_2;

			// Token: 0x04000014 RID: 20
			public static Func<MethodInfo, BinaryOperatorData> <>9__13_3;
		}
	}
}
