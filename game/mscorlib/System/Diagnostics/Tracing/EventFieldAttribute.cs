using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>The <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" /> is placed on fields of user-defined types that are passed as <see cref="T:System.Diagnostics.Tracing.EventSource" /> payloads.</summary>
	// Token: 0x020009F1 RID: 2545
	[AttributeUsage(AttributeTargets.Property)]
	public class EventFieldAttribute : Attribute
	{
		/// <summary>Gets or sets the value that specifies how to format the value of a user-defined type.</summary>
		/// <returns>The value that specifies how to format the value of a user-defined type.</returns>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x000479F8 File Offset: 0x00045BF8
		// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public EventFieldFormat Format
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the user-defined <see cref="T:System.Diagnostics.Tracing.EventFieldTags" /> value that is required for fields that contain data that isn't one of the supported types.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventFieldTags" />.</returns>
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x000479F8 File Offset: 0x00045BF8
		// (set) Token: 0x06005AC2 RID: 23234 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public EventFieldTags Tags
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" /> class.</summary>
		// Token: 0x06005AC3 RID: 23235 RVA: 0x00002050 File Offset: 0x00000250
		public EventFieldAttribute()
		{
		}
	}
}
