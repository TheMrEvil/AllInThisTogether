using System;

namespace System.Net
{
	/// <summary>The timeout manager to use for an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000691 RID: 1681
	public class HttpListenerTimeoutManager
	{
		/// <summary>Gets or sets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x0600355B RID: 13659 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan EntityBody
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

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x0600355D RID: 13661 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan DrainEntityBody
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

		/// <summary>Gets or sets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x0600355F RID: 13663 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan RequestQueue
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

		/// <summary>Gets or sets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003561 RID: 13665 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan IdleConnection
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

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan HeaderWait
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

		/// <summary>Gets or sets the minimum send rate, in bytes-per-second, for the response.</summary>
		/// <returns>The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003565 RID: 13669 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public long MinSendBytesPerSecond
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

		// Token: 0x06003566 RID: 13670 RVA: 0x0000219B File Offset: 0x0000039B
		public HttpListenerTimeoutManager()
		{
		}
	}
}
