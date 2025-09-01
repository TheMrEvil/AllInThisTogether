using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Creates a <see cref="T:System.Text.RegularExpressions.RegexRunner" /> class for a compiled regular expression.</summary>
	// Token: 0x0200020D RID: 525
	public abstract class RegexRunnerFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexRunnerFactory" /> class.</summary>
		// Token: 0x06000F2E RID: 3886 RVA: 0x0000219B File Offset: 0x0000039B
		protected RegexRunnerFactory()
		{
		}

		/// <summary>When overridden in a derived class, creates a <see cref="T:System.Text.RegularExpressions.RegexRunner" /> object for a specific compiled regular expression.</summary>
		/// <returns>A <see cref="T:System.Text.RegularExpressions.RegexRunner" /> object designed to execute a specific compiled regular expression.</returns>
		// Token: 0x06000F2F RID: 3887
		protected internal abstract RegexRunner CreateInstance();
	}
}
