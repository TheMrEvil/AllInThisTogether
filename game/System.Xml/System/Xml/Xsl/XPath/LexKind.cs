using System;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x0200042E RID: 1070
	internal enum LexKind
	{
		// Token: 0x04002179 RID: 8569
		Unknown,
		// Token: 0x0400217A RID: 8570
		Or,
		// Token: 0x0400217B RID: 8571
		And,
		// Token: 0x0400217C RID: 8572
		Eq,
		// Token: 0x0400217D RID: 8573
		Ne,
		// Token: 0x0400217E RID: 8574
		Lt,
		// Token: 0x0400217F RID: 8575
		Le,
		// Token: 0x04002180 RID: 8576
		Gt,
		// Token: 0x04002181 RID: 8577
		Ge,
		// Token: 0x04002182 RID: 8578
		Plus,
		// Token: 0x04002183 RID: 8579
		Minus,
		// Token: 0x04002184 RID: 8580
		Multiply,
		// Token: 0x04002185 RID: 8581
		Divide,
		// Token: 0x04002186 RID: 8582
		Modulo,
		// Token: 0x04002187 RID: 8583
		UnaryMinus,
		// Token: 0x04002188 RID: 8584
		Union,
		// Token: 0x04002189 RID: 8585
		LastOperator = 15,
		// Token: 0x0400218A RID: 8586
		DotDot,
		// Token: 0x0400218B RID: 8587
		ColonColon,
		// Token: 0x0400218C RID: 8588
		SlashSlash,
		// Token: 0x0400218D RID: 8589
		Number,
		// Token: 0x0400218E RID: 8590
		Axis,
		// Token: 0x0400218F RID: 8591
		Name,
		// Token: 0x04002190 RID: 8592
		String,
		// Token: 0x04002191 RID: 8593
		Eof,
		// Token: 0x04002192 RID: 8594
		FirstStringable = 21,
		// Token: 0x04002193 RID: 8595
		LastNonChar = 23,
		// Token: 0x04002194 RID: 8596
		LParens = 40,
		// Token: 0x04002195 RID: 8597
		RParens,
		// Token: 0x04002196 RID: 8598
		LBracket = 91,
		// Token: 0x04002197 RID: 8599
		RBracket = 93,
		// Token: 0x04002198 RID: 8600
		Dot = 46,
		// Token: 0x04002199 RID: 8601
		At = 64,
		// Token: 0x0400219A RID: 8602
		Comma = 44,
		// Token: 0x0400219B RID: 8603
		Star = 42,
		// Token: 0x0400219C RID: 8604
		Slash = 47,
		// Token: 0x0400219D RID: 8605
		Dollar = 36,
		// Token: 0x0400219E RID: 8606
		RBrace = 125
	}
}
