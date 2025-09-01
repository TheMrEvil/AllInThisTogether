using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that removes an event handler.</summary>
	// Token: 0x02000328 RID: 808
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class.</summary>
		// Token: 0x060019B4 RID: 6580 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeRemoveEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class with the specified event and event handler.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to detach the event handler from.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</param>
		// Token: 0x060019B5 RID: 6581 RVA: 0x00060C3A File Offset: 0x0005EE3A
		public CodeRemoveEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this._eventRef = eventRef;
			this.Listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class using the specified target object, event name, and event handler.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</param>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</param>
		// Token: 0x060019B6 RID: 6582 RVA: 0x00060C50 File Offset: 0x0005EE50
		public CodeRemoveEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this._eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.Listener = listener;
		}

		/// <summary>Gets or sets the event to remove a listener from.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to remove a listener from.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00060C6C File Offset: 0x0005EE6C
		// (set) Token: 0x060019B8 RID: 6584 RVA: 0x00060C91 File Offset: 0x0005EE91
		public CodeEventReferenceExpression Event
		{
			get
			{
				CodeEventReferenceExpression result;
				if ((result = this._eventRef) == null)
				{
					result = (this._eventRef = new CodeEventReferenceExpression());
				}
				return result;
			}
			set
			{
				this._eventRef = value;
			}
		}

		/// <summary>Gets or sets the event handler to remove.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x00060C9A File Offset: 0x0005EE9A
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x00060CA2 File Offset: 0x0005EEA2
		public CodeExpression Listener
		{
			[CompilerGenerated]
			get
			{
				return this.<Listener>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Listener>k__BackingField = value;
			}
		}

		// Token: 0x04000DD4 RID: 3540
		private CodeEventReferenceExpression _eventRef;

		// Token: 0x04000DD5 RID: 3541
		[CompilerGenerated]
		private CodeExpression <Listener>k__BackingField;
	}
}
