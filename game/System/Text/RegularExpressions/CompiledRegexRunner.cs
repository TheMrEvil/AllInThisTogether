using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001EB RID: 491
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x000358B8 File Offset: 0x00033AB8
		public CompiledRegexRunner()
		{
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000358C0 File Offset: 0x00033AC0
		public void SetDelegates(Action<RegexRunner> go, Func<RegexRunner, bool> firstChar, Action<RegexRunner> trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000358D7 File Offset: 0x00033AD7
		protected override void Go()
		{
			this._goMethod(this);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000358E5 File Offset: 0x00033AE5
		protected override bool FindFirstChar()
		{
			return this._findFirstCharMethod(this);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000358F3 File Offset: 0x00033AF3
		protected override void InitTrackCount()
		{
			this._initTrackCountMethod(this);
		}

		// Token: 0x040007DB RID: 2011
		private Action<RegexRunner> _goMethod;

		// Token: 0x040007DC RID: 2012
		private Func<RegexRunner, bool> _findFirstCharMethod;

		// Token: 0x040007DD RID: 2013
		private Action<RegexRunner> _initTrackCountMethod;
	}
}
