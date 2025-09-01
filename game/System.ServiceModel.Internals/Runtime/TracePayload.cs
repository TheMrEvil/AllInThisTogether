using System;

namespace System.Runtime
{
	// Token: 0x02000035 RID: 53
	internal struct TracePayload
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00006996 File Offset: 0x00004B96
		public TracePayload(string serializedException, string eventSource, string appDomainFriendlyName, string extendedData, string hostReference)
		{
			this.serializedException = serializedException;
			this.eventSource = eventSource;
			this.appDomainFriendlyName = appDomainFriendlyName;
			this.extendedData = extendedData;
			this.hostReference = hostReference;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000069BD File Offset: 0x00004BBD
		public string SerializedException
		{
			get
			{
				return this.serializedException;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000069C5 File Offset: 0x00004BC5
		public string EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000069CD File Offset: 0x00004BCD
		public string AppDomainFriendlyName
		{
			get
			{
				return this.appDomainFriendlyName;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000069D5 File Offset: 0x00004BD5
		public string ExtendedData
		{
			get
			{
				return this.extendedData;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000069DD File Offset: 0x00004BDD
		public string HostReference
		{
			get
			{
				return this.hostReference;
			}
		}

		// Token: 0x040000F8 RID: 248
		private string serializedException;

		// Token: 0x040000F9 RID: 249
		private string eventSource;

		// Token: 0x040000FA RID: 250
		private string appDomainFriendlyName;

		// Token: 0x040000FB RID: 251
		private string extendedData;

		// Token: 0x040000FC RID: 252
		private string hostReference;
	}
}
