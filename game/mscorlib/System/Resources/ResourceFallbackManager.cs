using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x0200085D RID: 2141
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06004744 RID: 18244 RVA: 0x000E817C File Offset: 0x000E637C
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000E81A9 File Offset: 0x000E63A9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x000E81B1 File Offset: 0x000E63B1
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_CE;
		}

		// Token: 0x04002DA4 RID: 11684
		private CultureInfo m_startingCulture;

		// Token: 0x04002DA5 RID: 11685
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x04002DA6 RID: 11686
		private bool m_useParents;

		// Token: 0x0200085E RID: 2142
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__5 : IEnumerator<CultureInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004747 RID: 18247 RVA: 0x000E81C0 File Offset: 0x000E63C0
			[DebuggerHidden]
			public <GetEnumerator>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06004748 RID: 18248 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004749 RID: 18249 RVA: 0x000E81D0 File Offset: 0x000E63D0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ResourceFallbackManager resourceFallbackManager = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					reachedNeutralResourcesCulture = false;
					currentCulture = resourceFallbackManager.m_startingCulture;
					break;
				case 1:
					this.<>1__state = -1;
					reachedNeutralResourcesCulture = true;
					goto IL_CE;
				case 2:
					this.<>1__state = -1;
					currentCulture = currentCulture.Parent;
					if (!resourceFallbackManager.m_useParents || currentCulture.HasInvariantCultureName)
					{
						goto IL_CE;
					}
					break;
				case 3:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (resourceFallbackManager.m_neutralResourcesCulture != null && currentCulture.Name == resourceFallbackManager.m_neutralResourcesCulture.Name)
				{
					this.<>2__current = CultureInfo.InvariantCulture;
					this.<>1__state = 1;
					return true;
				}
				this.<>2__current = currentCulture;
				this.<>1__state = 2;
				return true;
				IL_CE:
				if (!resourceFallbackManager.m_useParents || resourceFallbackManager.m_startingCulture.HasInvariantCultureName)
				{
					return false;
				}
				if (reachedNeutralResourcesCulture)
				{
					return false;
				}
				this.<>2__current = CultureInfo.InvariantCulture;
				this.<>1__state = 3;
				return true;
			}

			// Token: 0x17000AF0 RID: 2800
			// (get) Token: 0x0600474A RID: 18250 RVA: 0x000E82E8 File Offset: 0x000E64E8
			CultureInfo IEnumerator<CultureInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600474B RID: 18251 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000AF1 RID: 2801
			// (get) Token: 0x0600474C RID: 18252 RVA: 0x000E82E8 File Offset: 0x000E64E8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002DA7 RID: 11687
			private int <>1__state;

			// Token: 0x04002DA8 RID: 11688
			private CultureInfo <>2__current;

			// Token: 0x04002DA9 RID: 11689
			public ResourceFallbackManager <>4__this;

			// Token: 0x04002DAA RID: 11690
			private bool <reachedNeutralResourcesCulture>5__2;

			// Token: 0x04002DAB RID: 11691
			private CultureInfo <currentCulture>5__3;
		}
	}
}
