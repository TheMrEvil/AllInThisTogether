using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
	// Token: 0x0200041A RID: 1050
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> class.</summary>
		/// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
		/// <param name="userState">A unique user state.</param>
		// Token: 0x060021EE RID: 8686 RVA: 0x000743B2 File Offset: 0x000725B2
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progressPercentage = progressPercentage;
			this.userState = userState;
		}

		/// <summary>Gets the asynchronous task progress percentage.</summary>
		/// <returns>A percentage value indicating the asynchronous task progress.</returns>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000743C8 File Offset: 0x000725C8
		[SRDescription("Percentage progress made in operation.")]
		public int ProgressPercentage
		{
			get
			{
				return this.progressPercentage;
			}
		}

		/// <summary>Gets a unique user state.</summary>
		/// <returns>A unique <see cref="T:System.Object" /> indicating the user state.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000743D0 File Offset: 0x000725D0
		[SRDescription("User-supplied state to identify operation.")]
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x04001037 RID: 4151
		private readonly int progressPercentage;

		// Token: 0x04001038 RID: 4152
		private readonly object userState;
	}
}
