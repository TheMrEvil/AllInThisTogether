using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC
{
	// Token: 0x0200002B RID: 43
	public class QuantumPreprocessor
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003F2A File Offset: 0x0000212A
		public QuantumPreprocessor(IEnumerable<IQcPreprocessor> preprocessors)
		{
			this._preprocessors = (from x in preprocessors
			orderby x.Priority descending
			select x).ToArray<IQcPreprocessor>();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F62 File Offset: 0x00002162
		public QuantumPreprocessor() : this(new InjectionLoader<IQcPreprocessor>().GetInjectedInstances(false))
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F78 File Offset: 0x00002178
		public string Process(string text)
		{
			foreach (IQcPreprocessor qcPreprocessor in this._preprocessors)
			{
				try
				{
					text = qcPreprocessor.Process(text);
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("Preprocessor {0} failed:\n{1}", qcPreprocessor, ex.Message), ex);
				}
			}
			return text;
		}

		// Token: 0x0400007E RID: 126
		private readonly IQcPreprocessor[] _preprocessors;

		// Token: 0x0200008B RID: 139
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002C4 RID: 708 RVA: 0x0000B027 File Offset: 0x00009227
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x0000B033 File Offset: 0x00009233
			public <>c()
			{
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0000B03B File Offset: 0x0000923B
			internal int <.ctor>b__1_0(IQcPreprocessor x)
			{
				return x.Priority;
			}

			// Token: 0x04000196 RID: 406
			public static readonly QuantumPreprocessor.<>c <>9 = new QuantumPreprocessor.<>c();

			// Token: 0x04000197 RID: 407
			public static Func<IQcPreprocessor, int> <>9__1_0;
		}
	}
}
