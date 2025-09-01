using System;

namespace Mono.CSharp.yydebug
{
	// Token: 0x0200030A RID: 778
	internal class yyDebugSimple : yyDebug
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x000AF9DD File Offset: 0x000ADBDD
		private void println(string s)
		{
			Console.Error.WriteLine(s);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000AF9EA File Offset: 0x000ADBEA
		public void push(int state, object value)
		{
			this.println(string.Concat(new object[]
			{
				"push\tstate ",
				state,
				"\tvalue ",
				value
			}));
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AFA1A File Offset: 0x000ADC1A
		public void lex(int state, int token, string name, object value)
		{
			this.println(string.Concat(new object[]
			{
				"lex\tstate ",
				state,
				"\treading ",
				name,
				"\tvalue ",
				value
			}));
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000AFA58 File Offset: 0x000ADC58
		public void shift(int from, int to, int errorFlag)
		{
			switch (errorFlag)
			{
			case 0:
			case 1:
			case 2:
				this.println(string.Concat(new object[]
				{
					"shift\tfrom state ",
					from,
					" to ",
					to,
					"\t",
					errorFlag,
					" left to recover"
				}));
				return;
			case 3:
				this.println(string.Concat(new object[]
				{
					"shift\tfrom state ",
					from,
					" to ",
					to,
					"\ton error"
				}));
				return;
			default:
				this.println(string.Concat(new object[]
				{
					"shift\tfrom state ",
					from,
					" to ",
					to
				}));
				return;
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000AFB37 File Offset: 0x000ADD37
		public void pop(int state)
		{
			this.println("pop\tstate " + state + "\ton error");
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000AFB54 File Offset: 0x000ADD54
		public void discard(int state, int token, string name, object value)
		{
			this.println(string.Concat(new object[]
			{
				"discard\tstate ",
				state,
				"\ttoken ",
				name,
				"\tvalue ",
				value
			}));
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000AFB94 File Offset: 0x000ADD94
		public void reduce(int from, int to, int rule, string text, int len)
		{
			this.println(string.Concat(new object[]
			{
				"reduce\tstate ",
				from,
				"\tuncover ",
				to,
				"\trule (",
				rule,
				") ",
				text
			}));
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000AFBF2 File Offset: 0x000ADDF2
		public void shift(int from, int to)
		{
			this.println(string.Concat(new object[]
			{
				"goto\tfrom state ",
				from,
				" to ",
				to
			}));
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000AFC27 File Offset: 0x000ADE27
		public void accept(object value)
		{
			this.println("accept\tvalue " + value);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000AFC3A File Offset: 0x000ADE3A
		public void error(string message)
		{
			this.println("error\t" + message);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000AFC4D File Offset: 0x000ADE4D
		public void reject()
		{
			this.println("reject");
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00002CCC File Offset: 0x00000ECC
		public yyDebugSimple()
		{
		}
	}
}
