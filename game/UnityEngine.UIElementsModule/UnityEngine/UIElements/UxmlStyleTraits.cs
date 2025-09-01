using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C6 RID: 710
	public class UxmlStyleTraits : UxmlTraits
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00064060 File Offset: 0x00062260
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00064080 File Offset: 0x00062280
		public UxmlStyleTraits()
		{
		}

		// Token: 0x04000A54 RID: 2644
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000A55 RID: 2645
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000A56 RID: 2646
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};

		// Token: 0x020002C7 RID: 711
		[CompilerGenerated]
		private sealed class <get_uxmlChildElementsDescription>d__4 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
		{
			// Token: 0x0600180D RID: 6157 RVA: 0x000640D9 File Offset: 0x000622D9
			[DebuggerHidden]
			public <get_uxmlChildElementsDescription>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600180F RID: 6159 RVA: 0x000640FC File Offset: 0x000622FC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001810 RID: 6160 RVA: 0x00064122 File Offset: 0x00062322
			UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001811 RID: 6161 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001812 RID: 6162 RVA: 0x00064122 File Offset: 0x00062322
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001813 RID: 6163 RVA: 0x0006412C File Offset: 0x0006232C
			[DebuggerHidden]
			IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
			{
				UxmlStyleTraits.<get_uxmlChildElementsDescription>d__4 <get_uxmlChildElementsDescription>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_uxmlChildElementsDescription>d__ = this;
				}
				else
				{
					<get_uxmlChildElementsDescription>d__ = new UxmlStyleTraits.<get_uxmlChildElementsDescription>d__4(0);
					<get_uxmlChildElementsDescription>d__.<>4__this = this;
				}
				return <get_uxmlChildElementsDescription>d__;
			}

			// Token: 0x06001814 RID: 6164 RVA: 0x00064174 File Offset: 0x00062374
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
			}

			// Token: 0x04000A57 RID: 2647
			private int <>1__state;

			// Token: 0x04000A58 RID: 2648
			private UxmlChildElementDescription <>2__current;

			// Token: 0x04000A59 RID: 2649
			private int <>l__initialThreadId;

			// Token: 0x04000A5A RID: 2650
			public UxmlStyleTraits <>4__this;
		}
	}
}
