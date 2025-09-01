using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables event tracking for a component. This class cannot be inherited.</summary>
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class EventTrackingEnabledAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventTrackingEnabledAttribute" /> class, enabling event tracking.</summary>
		// Token: 0x06000066 RID: 102 RVA: 0x000022F8 File Offset: 0x000004F8
		public EventTrackingEnabledAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventTrackingEnabledAttribute" /> class, optionally disabling event tracking.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable event tracking; otherwise, <see langword="false" />.</param>
		// Token: 0x06000067 RID: 103 RVA: 0x00002307 File Offset: 0x00000507
		public EventTrackingEnabledAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets the value of the <see cref="P:System.EnterpriseServices.EventTrackingEnabledAttribute.Value" /> property, which indicates whether tracking is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if tracking is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002316 File Offset: 0x00000516
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000053 RID: 83
		private bool val;
	}
}
