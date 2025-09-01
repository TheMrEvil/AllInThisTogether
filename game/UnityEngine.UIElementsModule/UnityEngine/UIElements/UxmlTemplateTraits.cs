using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C9 RID: 713
	public class UxmlTemplateTraits : UxmlTraits
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x000641B8 File Offset: 0x000623B8
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000641D8 File Offset: 0x000623D8
		public UxmlTemplateTraits()
		{
		}

		// Token: 0x04000A5C RID: 2652
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name",
			use = UxmlAttributeDescription.Use.Required
		};

		// Token: 0x04000A5D RID: 2653
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000A5E RID: 2654
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};

		// Token: 0x020002CA RID: 714
		[CompilerGenerated]
		private sealed class <get_uxmlChildElementsDescription>d__4 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
		{
			// Token: 0x0600181E RID: 6174 RVA: 0x00064239 File Offset: 0x00062439
			[DebuggerHidden]
			public <get_uxmlChildElementsDescription>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x0600181F RID: 6175 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001820 RID: 6176 RVA: 0x0006425C File Offset: 0x0006245C
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

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001821 RID: 6177 RVA: 0x00064282 File Offset: 0x00062482
			UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001822 RID: 6178 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001823 RID: 6179 RVA: 0x00064282 File Offset: 0x00062482
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001824 RID: 6180 RVA: 0x0006428C File Offset: 0x0006248C
			[DebuggerHidden]
			IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
			{
				UxmlTemplateTraits.<get_uxmlChildElementsDescription>d__4 <get_uxmlChildElementsDescription>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_uxmlChildElementsDescription>d__ = this;
				}
				else
				{
					<get_uxmlChildElementsDescription>d__ = new UxmlTemplateTraits.<get_uxmlChildElementsDescription>d__4(0);
					<get_uxmlChildElementsDescription>d__.<>4__this = this;
				}
				return <get_uxmlChildElementsDescription>d__;
			}

			// Token: 0x06001825 RID: 6181 RVA: 0x000642D4 File Offset: 0x000624D4
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
			}

			// Token: 0x04000A5F RID: 2655
			private int <>1__state;

			// Token: 0x04000A60 RID: 2656
			private UxmlChildElementDescription <>2__current;

			// Token: 0x04000A61 RID: 2657
			private int <>l__initialThreadId;

			// Token: 0x04000A62 RID: 2658
			public UxmlTemplateTraits <>4__this;
		}
	}
}
