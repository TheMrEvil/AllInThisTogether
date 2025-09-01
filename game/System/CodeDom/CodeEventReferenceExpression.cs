using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an event.</summary>
	// Token: 0x0200030C RID: 780
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class.</summary>
		// Token: 0x060018D8 RID: 6360 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeEventReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class using the specified target object and event name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</param>
		/// <param name="eventName">The name of the event to reference.</param>
		// Token: 0x060018D9 RID: 6361 RVA: 0x0005FB74 File Offset: 0x0005DD74
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.TargetObject = targetObject;
			this._eventName = eventName;
		}

		/// <summary>Gets or sets the object that contains the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0005FB8A File Offset: 0x0005DD8A
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x0005FB92 File Offset: 0x0005DD92
		public CodeExpression TargetObject
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetObject>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0005FB9B File Offset: 0x0005DD9B
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x0005FBAC File Offset: 0x0005DDAC
		public string EventName
		{
			get
			{
				return this._eventName ?? string.Empty;
			}
			set
			{
				this._eventName = value;
			}
		}

		// Token: 0x04000D88 RID: 3464
		private string _eventName;

		// Token: 0x04000D89 RID: 3465
		[CompilerGenerated]
		private CodeExpression <TargetObject>k__BackingField;
	}
}
