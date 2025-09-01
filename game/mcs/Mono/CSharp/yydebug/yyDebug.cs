using System;

namespace Mono.CSharp.yydebug
{
	// Token: 0x02000309 RID: 777
	public interface yyDebug
	{
		// Token: 0x060024AB RID: 9387
		void push(int state, object value);

		// Token: 0x060024AC RID: 9388
		void lex(int state, int token, string name, object value);

		// Token: 0x060024AD RID: 9389
		void shift(int from, int to, int errorFlag);

		// Token: 0x060024AE RID: 9390
		void pop(int state);

		// Token: 0x060024AF RID: 9391
		void discard(int state, int token, string name, object value);

		// Token: 0x060024B0 RID: 9392
		void reduce(int from, int to, int rule, string text, int len);

		// Token: 0x060024B1 RID: 9393
		void shift(int from, int to);

		// Token: 0x060024B2 RID: 9394
		void accept(object value);

		// Token: 0x060024B3 RID: 9395
		void error(string message);

		// Token: 0x060024B4 RID: 9396
		void reject();
	}
}
