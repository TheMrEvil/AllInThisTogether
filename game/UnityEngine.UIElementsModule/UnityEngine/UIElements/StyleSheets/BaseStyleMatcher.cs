using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000374 RID: 884
	internal abstract class BaseStyleMatcher
	{
		// Token: 0x06001C5A RID: 7258
		protected abstract bool MatchKeyword(string keyword);

		// Token: 0x06001C5B RID: 7259
		protected abstract bool MatchNumber();

		// Token: 0x06001C5C RID: 7260
		protected abstract bool MatchInteger();

		// Token: 0x06001C5D RID: 7261
		protected abstract bool MatchLength();

		// Token: 0x06001C5E RID: 7262
		protected abstract bool MatchPercentage();

		// Token: 0x06001C5F RID: 7263
		protected abstract bool MatchColor();

		// Token: 0x06001C60 RID: 7264
		protected abstract bool MatchResource();

		// Token: 0x06001C61 RID: 7265
		protected abstract bool MatchUrl();

		// Token: 0x06001C62 RID: 7266
		protected abstract bool MatchTime();

		// Token: 0x06001C63 RID: 7267
		protected abstract bool MatchAngle();

		// Token: 0x06001C64 RID: 7268
		protected abstract bool MatchCustomIdent();

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001C65 RID: 7269
		public abstract int valueCount { get; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001C66 RID: 7270
		public abstract bool isCurrentVariable { get; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001C67 RID: 7271
		public abstract bool isCurrentComma { get; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x000868D3 File Offset: 0x00084AD3
		public bool hasCurrent
		{
			get
			{
				return this.m_CurrentContext.valueIndex < this.valueCount;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x000868E8 File Offset: 0x00084AE8
		// (set) Token: 0x06001C6A RID: 7274 RVA: 0x000868F5 File Offset: 0x00084AF5
		public int currentIndex
		{
			get
			{
				return this.m_CurrentContext.valueIndex;
			}
			set
			{
				this.m_CurrentContext.valueIndex = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x00086903 File Offset: 0x00084B03
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x00086910 File Offset: 0x00084B10
		public int matchedVariableCount
		{
			get
			{
				return this.m_CurrentContext.matchedVariableCount;
			}
			set
			{
				this.m_CurrentContext.matchedVariableCount = value;
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0008691E File Offset: 0x00084B1E
		protected void Initialize()
		{
			this.m_CurrentContext = default(BaseStyleMatcher.MatchContext);
			this.m_ContextStack.Clear();
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0008693C File Offset: 0x00084B3C
		public void MoveNext()
		{
			bool flag = this.currentIndex + 1 <= this.valueCount;
			if (flag)
			{
				int currentIndex = this.currentIndex;
				this.currentIndex = currentIndex + 1;
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00086974 File Offset: 0x00084B74
		public void SaveContext()
		{
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00086989 File Offset: 0x00084B89
		public void RestoreContext()
		{
			this.m_CurrentContext = this.m_ContextStack.Pop();
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0008699D File Offset: 0x00084B9D
		public void DropContext()
		{
			this.m_ContextStack.Pop();
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000869AC File Offset: 0x00084BAC
		protected bool Match(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.None;
			bool result;
			if (flag)
			{
				result = this.MatchExpression(exp);
			}
			else
			{
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
				result = this.MatchExpressionWithMultiplier(exp);
			}
			return result;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00086A08 File Offset: 0x00084C08
		private bool MatchExpression(Expression exp)
		{
			bool flag = false;
			bool flag2 = exp.type == ExpressionType.Combinator;
			if (flag2)
			{
				flag = this.MatchCombinator(exp);
			}
			else
			{
				bool isCurrentVariable = this.isCurrentVariable;
				if (isCurrentVariable)
				{
					flag = true;
					int matchedVariableCount = this.matchedVariableCount;
					this.matchedVariableCount = matchedVariableCount + 1;
				}
				else
				{
					bool flag3 = exp.type == ExpressionType.Data;
					if (flag3)
					{
						flag = this.MatchDataType(exp);
					}
					else
					{
						bool flag4 = exp.type == ExpressionType.Keyword;
						if (flag4)
						{
							flag = this.MatchKeyword(exp.keyword);
						}
					}
				}
				bool flag5 = flag;
				if (flag5)
				{
					this.MoveNext();
				}
			}
			bool flag6 = !flag && !this.hasCurrent && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00086AC4 File Offset: 0x00084CC4
		private bool MatchExpressionWithMultiplier(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.OneOrMoreComma;
			bool flag2 = true;
			int min = exp.multiplier.min;
			int max = exp.multiplier.max;
			int num = 0;
			int num2 = 0;
			while (flag2 && this.hasCurrent && num2 < max)
			{
				flag2 = this.MatchExpression(exp);
				bool flag3 = flag2;
				if (flag3)
				{
					num++;
					bool flag4 = flag;
					if (flag4)
					{
						bool flag5 = !this.isCurrentComma;
						if (flag5)
						{
							break;
						}
						this.MoveNext();
					}
				}
				num2++;
			}
			flag2 = (num >= min && num <= max);
			bool flag6 = !flag2 && num <= max && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00086B90 File Offset: 0x00084D90
		private bool MatchGroup(Expression exp)
		{
			Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
			Expression exp2 = exp.subExpressions[0];
			return this.Match(exp2);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00086BC8 File Offset: 0x00084DC8
		private bool MatchCombinator(Expression exp)
		{
			this.SaveContext();
			bool flag = false;
			switch (exp.combinator)
			{
			case ExpressionCombinator.Or:
				flag = this.MatchOr(exp);
				break;
			case ExpressionCombinator.OrOr:
				flag = this.MatchOrOr(exp);
				break;
			case ExpressionCombinator.AndAnd:
				flag = this.MatchAndAnd(exp);
				break;
			case ExpressionCombinator.Juxtaposition:
				flag = this.MatchJuxtaposition(exp);
				break;
			case ExpressionCombinator.Group:
				flag = this.MatchGroup(exp);
				break;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.DropContext();
			}
			else
			{
				this.RestoreContext();
			}
			return flag;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00086C58 File Offset: 0x00084E58
		private bool MatchOr(Expression exp)
		{
			BaseStyleMatcher.MatchContext currentContext = default(BaseStyleMatcher.MatchContext);
			int num = 0;
			for (int i = 0; i < exp.subExpressions.Length; i++)
			{
				this.SaveContext();
				int currentIndex = this.currentIndex;
				bool flag = this.Match(exp.subExpressions[i]);
				int num2 = this.currentIndex - currentIndex;
				bool flag2 = flag && num2 > num;
				if (flag2)
				{
					num = num2;
					currentContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			bool flag3 = num > 0;
			bool result;
			if (flag3)
			{
				this.m_CurrentContext = currentContext;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00086CF8 File Offset: 0x00084EF8
		private bool MatchOrOr(Expression exp)
		{
			int num = this.MatchMany(exp);
			return num > 0;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00086D18 File Offset: 0x00084F18
		private bool MatchAndAnd(Expression exp)
		{
			int num = this.MatchMany(exp);
			int num2 = exp.subExpressions.Length;
			return num == num2;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00086D40 File Offset: 0x00084F40
		private unsafe int MatchMany(Expression exp)
		{
			BaseStyleMatcher.MatchContext currentContext = default(BaseStyleMatcher.MatchContext);
			int num = 0;
			int num2 = -1;
			int num3 = exp.subExpressions.Length;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)num3) * 4)];
			do
			{
				this.SaveContext();
				num2++;
				for (int i = 0; i < num3; i++)
				{
					int num4 = (num2 > 0) ? ((num2 + i) % num3) : i;
					ptr[i] = num4;
				}
				int num5 = this.MatchManyByOrder(exp, ptr);
				bool flag = num5 > num;
				if (flag)
				{
					num = num5;
					currentContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			while (num < num3 && num2 < num3);
			bool flag2 = num > 0;
			if (flag2)
			{
				this.m_CurrentContext = currentContext;
			}
			return num;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00086E00 File Offset: 0x00085000
		private unsafe int MatchManyByOrder(Expression exp, int* matchOrder)
		{
			int num = exp.subExpressions.Length;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)num) * 4)];
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			while (num4 < num && num2 + num3 < num)
			{
				int num5 = matchOrder[num4];
				bool flag = false;
				for (int i = 0; i < num2; i++)
				{
					bool flag2 = ptr[i] == num5;
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				bool flag3 = false;
				bool flag4 = !flag;
				if (flag4)
				{
					flag3 = this.Match(exp.subExpressions[num5]);
				}
				bool flag5 = flag3;
				if (flag5)
				{
					bool flag6 = num3 == this.matchedVariableCount;
					if (flag6)
					{
						ptr[num2] = num5;
						num2++;
					}
					else
					{
						num3 = this.matchedVariableCount;
					}
					num4 = 0;
				}
				else
				{
					num4++;
				}
			}
			return num2 + num3;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00086EEC File Offset: 0x000850EC
		private bool MatchJuxtaposition(Expression exp)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00086F30 File Offset: 0x00085130
		private bool MatchDataType(Expression exp)
		{
			bool result = false;
			bool hasCurrent = this.hasCurrent;
			if (hasCurrent)
			{
				switch (exp.dataType)
				{
				case DataType.Number:
					result = this.MatchNumber();
					break;
				case DataType.Integer:
					result = this.MatchInteger();
					break;
				case DataType.Length:
					result = this.MatchLength();
					break;
				case DataType.Percentage:
					result = this.MatchPercentage();
					break;
				case DataType.Color:
					result = this.MatchColor();
					break;
				case DataType.Resource:
					result = this.MatchResource();
					break;
				case DataType.Url:
					result = this.MatchUrl();
					break;
				case DataType.Time:
					result = this.MatchTime();
					break;
				case DataType.Angle:
					result = this.MatchAngle();
					break;
				case DataType.CustomIdent:
					result = this.MatchCustomIdent();
					break;
				}
			}
			return result;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00086FEB File Offset: 0x000851EB
		protected BaseStyleMatcher()
		{
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00086FFF File Offset: 0x000851FF
		// Note: this type is marked as 'beforefieldinit'.
		static BaseStyleMatcher()
		{
		}

		// Token: 0x04000E34 RID: 3636
		protected static readonly Regex s_CustomIdentRegex = new Regex("^-?[_a-z][_a-z0-9-]*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000E35 RID: 3637
		private Stack<BaseStyleMatcher.MatchContext> m_ContextStack = new Stack<BaseStyleMatcher.MatchContext>();

		// Token: 0x04000E36 RID: 3638
		private BaseStyleMatcher.MatchContext m_CurrentContext;

		// Token: 0x02000375 RID: 885
		private struct MatchContext
		{
			// Token: 0x04000E37 RID: 3639
			public int valueIndex;

			// Token: 0x04000E38 RID: 3640
			public int matchedVariableCount;
		}
	}
}
