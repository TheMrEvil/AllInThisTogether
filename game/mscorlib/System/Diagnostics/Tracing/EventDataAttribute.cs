using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies a type to be passed to the <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> method.</summary>
	// Token: 0x020009F0 RID: 2544
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class EventDataAttribute : Attribute
	{
		/// <summary>Gets or sets the name to apply to an event if the event type or property is not explicitly named.</summary>
		/// <returns>The name to apply to the event or property.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005ABC RID: 23228 RVA: 0x000479F8 File Offset: 0x00045BF8
		// (set) Token: 0x06005ABD RID: 23229 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public string Name
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> class.</summary>
		// Token: 0x06005ABE RID: 23230 RVA: 0x00002050 File Offset: 0x00000250
		public EventDataAttribute()
		{
		}
	}
}
