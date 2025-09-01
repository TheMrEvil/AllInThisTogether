using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000125 RID: 293
	internal struct ConcatKey<TLeftKey, TRightKey>
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		private ConcatKey(TLeftKey leftKey, TRightKey rightKey, bool isLeft)
		{
			this._leftKey = leftKey;
			this._rightKey = rightKey;
			this._isLeft = isLeft;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001FC0C File Offset: 0x0001DE0C
		internal static ConcatKey<TLeftKey, TRightKey> MakeLeft(TLeftKey leftKey)
		{
			return new ConcatKey<TLeftKey, TRightKey>(leftKey, default(TRightKey), true);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		internal static ConcatKey<TLeftKey, TRightKey> MakeRight(TRightKey rightKey)
		{
			return new ConcatKey<TLeftKey, TRightKey>(default(TLeftKey), rightKey, false);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001FC49 File Offset: 0x0001DE49
		internal static IComparer<ConcatKey<TLeftKey, TRightKey>> MakeComparer(IComparer<TLeftKey> leftComparer, IComparer<TRightKey> rightComparer)
		{
			return new ConcatKey<TLeftKey, TRightKey>.ConcatKeyComparer(leftComparer, rightComparer);
		}

		// Token: 0x04000687 RID: 1671
		private readonly TLeftKey _leftKey;

		// Token: 0x04000688 RID: 1672
		private readonly TRightKey _rightKey;

		// Token: 0x04000689 RID: 1673
		private readonly bool _isLeft;

		// Token: 0x02000126 RID: 294
		private class ConcatKeyComparer : IComparer<ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x06000910 RID: 2320 RVA: 0x0001FC52 File Offset: 0x0001DE52
			internal ConcatKeyComparer(IComparer<TLeftKey> leftComparer, IComparer<TRightKey> rightComparer)
			{
				this._leftComparer = leftComparer;
				this._rightComparer = rightComparer;
			}

			// Token: 0x06000911 RID: 2321 RVA: 0x0001FC68 File Offset: 0x0001DE68
			public int Compare(ConcatKey<TLeftKey, TRightKey> x, ConcatKey<TLeftKey, TRightKey> y)
			{
				if (x._isLeft != y._isLeft)
				{
					if (!x._isLeft)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (x._isLeft)
					{
						return this._leftComparer.Compare(x._leftKey, y._leftKey);
					}
					return this._rightComparer.Compare(x._rightKey, y._rightKey);
				}
			}

			// Token: 0x0400068A RID: 1674
			private IComparer<TLeftKey> _leftComparer;

			// Token: 0x0400068B RID: 1675
			private IComparer<TRightKey> _rightComparer;
		}
	}
}
