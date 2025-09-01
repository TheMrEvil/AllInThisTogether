using System;
using System.Collections.Generic;
using System.Linq;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000003 RID: 3
	public abstract class BinaryAndUnaryOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002074 File Offset: 0x00000274
		protected override int GetOperatorPosition(string value)
		{
			foreach (int num in TextProcessing.GetScopedSplitPoints<char[]>(value, this.OperatorToken, TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers).Reverse<int>())
			{
				if (this.IsValidBinaryOperator(value, num))
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		private bool IsValidBinaryOperator(string value, int position)
		{
			while (position > 0)
			{
				char item = value[--position];
				if (this._operatorChars.Contains(item))
				{
					return false;
				}
				if (!this._ignoreChars.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		protected BinaryAndUnaryOperatorGrammar()
		{
		}

		// Token: 0x04000001 RID: 1
		private readonly HashSet<char> _operatorChars = new HashSet<char>
		{
			'+',
			'-',
			'*',
			'/',
			'&',
			'|',
			'^',
			'=',
			'!',
			','
		};

		// Token: 0x04000002 RID: 2
		private readonly HashSet<char> _ignoreChars = new HashSet<char>
		{
			' ',
			'\0'
		};
	}
}
