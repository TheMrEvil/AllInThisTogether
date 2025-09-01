using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000106 RID: 262
	public class DebugUIHandlerWidget : MonoBehaviour
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00021D30 File Offset: 0x0001FF30
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00021D38 File Offset: 0x0001FF38
		public DebugUIHandlerWidget parentUIHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<parentUIHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<parentUIHandler>k__BackingField = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00021D41 File Offset: 0x0001FF41
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00021D49 File Offset: 0x0001FF49
		public DebugUIHandlerWidget previousUIHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<previousUIHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<previousUIHandler>k__BackingField = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00021D52 File Offset: 0x0001FF52
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00021D5A File Offset: 0x0001FF5A
		public DebugUIHandlerWidget nextUIHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<nextUIHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<nextUIHandler>k__BackingField = value;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00021D63 File Offset: 0x0001FF63
		protected virtual void OnEnable()
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00021D65 File Offset: 0x0001FF65
		internal virtual void SetWidget(DebugUI.Widget widget)
		{
			this.m_Widget = widget;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00021D6E File Offset: 0x0001FF6E
		internal DebugUI.Widget GetWidget()
		{
			return this.m_Widget;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00021D78 File Offset: 0x0001FF78
		protected T CastWidget<T>() where T : DebugUI.Widget
		{
			T t = this.m_Widget as T;
			string text = (this.m_Widget == null) ? "null" : this.m_Widget.GetType().ToString();
			if (t == null)
			{
				string str = "Can't cast ";
				string str2 = text;
				string str3 = " to ";
				Type typeFromHandle = typeof(T);
				throw new InvalidOperationException(str + str2 + str3 + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
			}
			return t;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00021DE9 File Offset: 0x0001FFE9
		public virtual bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return true;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public virtual void OnDeselection()
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00021DEE File Offset: 0x0001FFEE
		public virtual void OnAction()
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00021DF0 File Offset: 0x0001FFF0
		public virtual void OnIncrement(bool fast)
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00021DF2 File Offset: 0x0001FFF2
		public virtual void OnDecrement(bool fast)
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00021DF4 File Offset: 0x0001FFF4
		public virtual DebugUIHandlerWidget Previous()
		{
			if (this.previousUIHandler != null)
			{
				return this.previousUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				return this.parentUIHandler;
			}
			return null;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00021E24 File Offset: 0x00020024
		public virtual DebugUIHandlerWidget Next()
		{
			if (this.nextUIHandler != null)
			{
				return this.nextUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				DebugUIHandlerWidget parentUIHandler = this.parentUIHandler;
				while (parentUIHandler != null)
				{
					DebugUIHandlerWidget nextUIHandler = parentUIHandler.nextUIHandler;
					if (nextUIHandler != null)
					{
						return nextUIHandler;
					}
					parentUIHandler = parentUIHandler.parentUIHandler;
				}
			}
			return null;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00021E80 File Offset: 0x00020080
		public DebugUIHandlerWidget()
		{
		}

		// Token: 0x0400044B RID: 1099
		[HideInInspector]
		public Color colorDefault = new Color(0.8f, 0.8f, 0.8f, 1f);

		// Token: 0x0400044C RID: 1100
		[HideInInspector]
		public Color colorSelected = new Color(0.25f, 0.65f, 0.8f, 1f);

		// Token: 0x0400044D RID: 1101
		[CompilerGenerated]
		private DebugUIHandlerWidget <parentUIHandler>k__BackingField;

		// Token: 0x0400044E RID: 1102
		[CompilerGenerated]
		private DebugUIHandlerWidget <previousUIHandler>k__BackingField;

		// Token: 0x0400044F RID: 1103
		[CompilerGenerated]
		private DebugUIHandlerWidget <nextUIHandler>k__BackingField;

		// Token: 0x04000450 RID: 1104
		protected DebugUI.Widget m_Widget;
	}
}
