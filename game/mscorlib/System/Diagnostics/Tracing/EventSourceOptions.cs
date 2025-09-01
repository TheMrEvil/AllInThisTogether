using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies overrides of default event settings such as the log level, keywords and operation code when the <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> method is called.</summary>
	// Token: 0x020009E5 RID: 2533
	public struct EventSourceOptions
	{
		/// <summary>Gets or sets the event level applied to the event.</summary>
		/// <returns>The event level for the event. If not set, the default is Verbose (5).</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005A94 RID: 23188 RVA: 0x001342D3 File Offset: 0x001324D3
		// (set) Token: 0x06005A95 RID: 23189 RVA: 0x001342DB File Offset: 0x001324DB
		public EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		/// <summary>Gets or sets the operation code to use for the specified event.</summary>
		/// <returns>The operation code to use for the specified event. If not set, the default is <see langword="Info" /> (0).</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005A96 RID: 23190 RVA: 0x001342F4 File Offset: 0x001324F4
		// (set) Token: 0x06005A97 RID: 23191 RVA: 0x001342FC File Offset: 0x001324FC
		public EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005A98 RID: 23192 RVA: 0x00134315 File Offset: 0x00132515
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		/// <summary>Gets or sets the keywords applied to the event. If this property is not set, the event's keywords will be <see langword="None" />.</summary>
		/// <returns>The keywords applied to the event, or <see langword="None" /> if no keywords are set.</returns>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005A99 RID: 23193 RVA: 0x00134322 File Offset: 0x00132522
		// (set) Token: 0x06005A9A RID: 23194 RVA: 0x0013432A File Offset: 0x0013252A
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		/// <summary>The event tags defined for this event source.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventTags" />.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x00134342 File Offset: 0x00132542
		// (set) Token: 0x06005A9C RID: 23196 RVA: 0x0013434A File Offset: 0x0013254A
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		/// <summary>The activity options defined for this event source.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x00134362 File Offset: 0x00132562
		// (set) Token: 0x06005A9E RID: 23198 RVA: 0x0013436A File Offset: 0x0013256A
		public EventActivityOptions ActivityOptions
		{
			get
			{
				return this.activityOptions;
			}
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x040037DB RID: 14299
		internal EventKeywords keywords;

		// Token: 0x040037DC RID: 14300
		internal EventTags tags;

		// Token: 0x040037DD RID: 14301
		internal EventActivityOptions activityOptions;

		// Token: 0x040037DE RID: 14302
		internal byte level;

		// Token: 0x040037DF RID: 14303
		internal byte opcode;

		// Token: 0x040037E0 RID: 14304
		internal byte valuesSet;

		// Token: 0x040037E1 RID: 14305
		internal const byte keywordsSet = 1;

		// Token: 0x040037E2 RID: 14306
		internal const byte tagsSet = 2;

		// Token: 0x040037E3 RID: 14307
		internal const byte levelSet = 4;

		// Token: 0x040037E4 RID: 14308
		internal const byte opcodeSet = 8;

		// Token: 0x040037E5 RID: 14309
		internal const byte activityOptionsSet = 16;
	}
}
