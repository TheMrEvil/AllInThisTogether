using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that attaches an event-handler delegate to an event.</summary>
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class.</summary>
		// Token: 0x06001827 RID: 6183 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeAttachEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified event and delegate.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler.</param>
		// Token: 0x06001828 RID: 6184 RVA: 0x0005F115 File Offset: 0x0005D315
		public CodeAttachEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this._eventRef = eventRef;
			this.Listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified object containing the event, event name, and event-handler delegate.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</param>
		/// <param name="eventName">The name of the event to attach an event handler to.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler.</param>
		// Token: 0x06001829 RID: 6185 RVA: 0x0005F12B File Offset: 0x0005D32B
		public CodeAttachEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener) : this(new CodeEventReferenceExpression(targetObject, eventName), listener)
		{
		}

		/// <summary>Gets or sets the event to attach an event-handler delegate to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0005F13C File Offset: 0x0005D33C
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0005F161 File Offset: 0x0005D361
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

		/// <summary>Gets or sets the new event-handler delegate to attach to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler to attach.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0005F16A File Offset: 0x0005D36A
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0005F172 File Offset: 0x0005D372
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

		// Token: 0x04000D4F RID: 3407
		private CodeEventReferenceExpression _eventRef;

		// Token: 0x04000D50 RID: 3408
		[CompilerGenerated]
		private CodeExpression <Listener>k__BackingField;
	}
}
