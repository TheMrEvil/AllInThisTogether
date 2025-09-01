using System;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001EC RID: 492
	internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00035901 File Offset: 0x00033B01
		public CompiledRegexRunnerFactory(DynamicMethod go, DynamicMethod firstChar, DynamicMethod trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00035920 File Offset: 0x00033B20
		protected internal override RegexRunner CreateInstance()
		{
			CompiledRegexRunner compiledRegexRunner = new CompiledRegexRunner();
			compiledRegexRunner.SetDelegates((Action<RegexRunner>)this._goMethod.CreateDelegate(typeof(Action<RegexRunner>)), (Func<RegexRunner, bool>)this._findFirstCharMethod.CreateDelegate(typeof(Func<RegexRunner, bool>)), (Action<RegexRunner>)this._initTrackCountMethod.CreateDelegate(typeof(Action<RegexRunner>)));
			return compiledRegexRunner;
		}

		// Token: 0x040007DE RID: 2014
		private readonly DynamicMethod _goMethod;

		// Token: 0x040007DF RID: 2015
		private readonly DynamicMethod _findFirstCharMethod;

		// Token: 0x040007E0 RID: 2016
		private readonly DynamicMethod _initTrackCountMethod;
	}
}
