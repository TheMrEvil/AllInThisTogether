using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CC RID: 716
	public class UxmlAttributeOverridesTraits : UxmlTraits
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00064318 File Offset: 0x00062518
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00064337 File Offset: 0x00062537
		public UxmlAttributeOverridesTraits()
		{
		}

		// Token: 0x04000A64 RID: 2660
		internal const string k_ElementNameAttributeName = "element-name";

		// Token: 0x04000A65 RID: 2661
		private UxmlStringAttributeDescription m_ElementName = new UxmlStringAttributeDescription
		{
			name = "element-name",
			use = UxmlAttributeDescription.Use.Required
		};

		// Token: 0x020002CD RID: 717
		[CompilerGenerated]
		private sealed class <get_uxmlChildElementsDescription>d__3 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
		{
			// Token: 0x0600182F RID: 6191 RVA: 0x0006435F File Offset: 0x0006255F
			[DebuggerHidden]
			public <get_uxmlChildElementsDescription>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06001830 RID: 6192 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001831 RID: 6193 RVA: 0x00064380 File Offset: 0x00062580
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

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001832 RID: 6194 RVA: 0x000643A6 File Offset: 0x000625A6
			UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001833 RID: 6195 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001834 RID: 6196 RVA: 0x000643A6 File Offset: 0x000625A6
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001835 RID: 6197 RVA: 0x000643B0 File Offset: 0x000625B0
			[DebuggerHidden]
			IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
			{
				UxmlAttributeOverridesTraits.<get_uxmlChildElementsDescription>d__3 <get_uxmlChildElementsDescription>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_uxmlChildElementsDescription>d__ = this;
				}
				else
				{
					<get_uxmlChildElementsDescription>d__ = new UxmlAttributeOverridesTraits.<get_uxmlChildElementsDescription>d__3(0);
					<get_uxmlChildElementsDescription>d__.<>4__this = this;
				}
				return <get_uxmlChildElementsDescription>d__;
			}

			// Token: 0x06001836 RID: 6198 RVA: 0x000643F8 File Offset: 0x000625F8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
			}

			// Token: 0x04000A66 RID: 2662
			private int <>1__state;

			// Token: 0x04000A67 RID: 2663
			private UxmlChildElementDescription <>2__current;

			// Token: 0x04000A68 RID: 2664
			private int <>l__initialThreadId;

			// Token: 0x04000A69 RID: 2665
			public UxmlAttributeOverridesTraits <>4__this;
		}
	}
}
