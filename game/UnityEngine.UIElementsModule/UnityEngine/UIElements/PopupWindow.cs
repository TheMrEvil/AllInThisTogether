using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015A RID: 346
	public class PopupWindow : TextElement
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0002E220 File Offset: 0x0002C420
		public PopupWindow()
		{
			base.AddToClassList(PopupWindow.ussClassName);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.AddToClassList(PopupWindow.contentUssClassName);
			base.hierarchy.Add(this.m_ContentContainer);
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0002E280 File Offset: 0x0002C480
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002E298 File Offset: 0x0002C498
		// Note: this type is marked as 'beforefieldinit'.
		static PopupWindow()
		{
		}

		// Token: 0x04000515 RID: 1301
		private VisualElement m_ContentContainer;

		// Token: 0x04000516 RID: 1302
		public new static readonly string ussClassName = "unity-popup-window";

		// Token: 0x04000517 RID: 1303
		public static readonly string contentUssClassName = PopupWindow.ussClassName + "__content-container";

		// Token: 0x0200015B RID: 347
		public new class UxmlFactory : UxmlFactory<PopupWindow, PopupWindow.UxmlTraits>
		{
			// Token: 0x06000B37 RID: 2871 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200015C RID: 348
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x1700022D RID: 557
			// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
			public UxmlTraits()
			{
			}

			// Token: 0x0200015D RID: 349
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__1 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x06000B3A RID: 2874 RVA: 0x0002E2E3 File Offset: 0x0002C4E3
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__1(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x06000B3B RID: 2875 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000B3C RID: 2876 RVA: 0x0002E304 File Offset: 0x0002C504
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

				// Token: 0x1700022E RID: 558
				// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0002E357 File Offset: 0x0002C557
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000B3E RID: 2878 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x1700022F RID: 559
				// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002E357 File Offset: 0x0002C557
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000B40 RID: 2880 RVA: 0x0002E360 File Offset: 0x0002C560
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					PopupWindow.UxmlTraits.<get_uxmlChildElementsDescription>d__1 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new PopupWindow.UxmlTraits.<get_uxmlChildElementsDescription>d__1(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x06000B41 RID: 2881 RVA: 0x0002E3A8 File Offset: 0x0002C5A8
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x04000518 RID: 1304
				private int <>1__state;

				// Token: 0x04000519 RID: 1305
				private UxmlChildElementDescription <>2__current;

				// Token: 0x0400051A RID: 1306
				private int <>l__initialThreadId;

				// Token: 0x0400051B RID: 1307
				public PopupWindow.UxmlTraits <>4__this;
			}
		}
	}
}
