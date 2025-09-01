using System;

namespace System.Diagnostics
{
	// Token: 0x02000219 RID: 537
	internal class FilterElement : TypedElement
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x0004577C File Offset: 0x0004397C
		public FilterElement() : base(typeof(TraceFilter))
		{
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0004578E File Offset: 0x0004398E
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000457A7 File Offset: 0x000439A7
		internal TraceFilter RefreshRuntimeObject(TraceFilter filter)
		{
			if (Type.GetType(this.TypeName) != filter.GetType() || base.InitData != filter.initializeData)
			{
				this._runtimeObject = null;
				return this.GetRuntimeObject();
			}
			return filter;
		}
	}
}
