using System;
using System.Collections.Generic;

namespace LeTai.TrueShadow
{
	// Token: 0x02000018 RID: 24
	internal class ChangeTracker<T> : IChangeTracker
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000645C File Offset: 0x0000465C
		public ChangeTracker(Func<T> getValue, Func<T, T> onChange, Func<T, T, bool> compare = null)
		{
			this.getValue = getValue;
			this.onChange = onChange;
			this.compare = (compare ?? new Func<T, T, bool>(EqualityComparer<T>.Default.Equals));
			this.previousValue = this.getValue();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000064AA File Offset: 0x000046AA
		public void Forget()
		{
			this.previousValue = this.getValue();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000064C0 File Offset: 0x000046C0
		public void Check()
		{
			T t = this.getValue();
			if (!this.compare(this.previousValue, t))
			{
				this.previousValue = this.onChange(t);
			}
		}

		// Token: 0x040000AB RID: 171
		private T previousValue;

		// Token: 0x040000AC RID: 172
		private readonly Func<T> getValue;

		// Token: 0x040000AD RID: 173
		private readonly Func<T, T> onChange;

		// Token: 0x040000AE RID: 174
		private readonly Func<T, T, bool> compare;
	}
}
