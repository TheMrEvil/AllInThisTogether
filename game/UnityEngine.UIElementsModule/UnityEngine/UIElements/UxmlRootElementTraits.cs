using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C3 RID: 707
	public class UxmlRootElementTraits : UxmlTraits
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00063EC4 File Offset: 0x000620C4
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield return new UxmlChildElementDescription(typeof(VisualElement));
				yield break;
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00063EE3 File Offset: 0x000620E3
		public UxmlRootElementTraits()
		{
		}

		// Token: 0x04000A4D RID: 2637
		protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000A4E RID: 2638
		private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
		{
			name = "class"
		};

		// Token: 0x020002C4 RID: 708
		[CompilerGenerated]
		private sealed class <get_uxmlChildElementsDescription>d__3 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
		{
			// Token: 0x060017FC RID: 6140 RVA: 0x00063F1A File Offset: 0x0006211A
			[DebuggerHidden]
			public <get_uxmlChildElementsDescription>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060017FD RID: 6141 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060017FE RID: 6142 RVA: 0x00063F3C File Offset: 0x0006213C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = new UxmlChildElementDescription(typeof(VisualElement));
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x060017FF RID: 6143 RVA: 0x00063F8F File Offset: 0x0006218F
			UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001800 RID: 6144 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001801 RID: 6145 RVA: 0x00063F8F File Offset: 0x0006218F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001802 RID: 6146 RVA: 0x00063F98 File Offset: 0x00062198
			[DebuggerHidden]
			IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
			{
				UxmlRootElementTraits.<get_uxmlChildElementsDescription>d__3 <get_uxmlChildElementsDescription>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_uxmlChildElementsDescription>d__ = this;
				}
				else
				{
					<get_uxmlChildElementsDescription>d__ = new UxmlRootElementTraits.<get_uxmlChildElementsDescription>d__3(0);
					<get_uxmlChildElementsDescription>d__.<>4__this = this;
				}
				return <get_uxmlChildElementsDescription>d__;
			}

			// Token: 0x06001803 RID: 6147 RVA: 0x00063FE0 File Offset: 0x000621E0
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
			}

			// Token: 0x04000A4F RID: 2639
			private int <>1__state;

			// Token: 0x04000A50 RID: 2640
			private UxmlChildElementDescription <>2__current;

			// Token: 0x04000A51 RID: 2641
			private int <>l__initialThreadId;

			// Token: 0x04000A52 RID: 2642
			public UxmlRootElementTraits <>4__this;
		}
	}
}
