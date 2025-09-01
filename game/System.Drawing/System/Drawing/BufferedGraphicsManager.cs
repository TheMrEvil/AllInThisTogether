using System;

namespace System.Drawing
{
	/// <summary>Provides access to the main buffered graphics context object for the application domain.</summary>
	// Token: 0x02000051 RID: 81
	public sealed class BufferedGraphicsManager
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00009BED File Offset: 0x00007DED
		static BufferedGraphicsManager()
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00002050 File Offset: 0x00000250
		private BufferedGraphicsManager()
		{
		}

		/// <summary>Gets the <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</summary>
		/// <returns>The <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00009BF9 File Offset: 0x00007DF9
		public static BufferedGraphicsContext Current
		{
			get
			{
				return BufferedGraphicsManager.graphics_context;
			}
		}

		// Token: 0x04000433 RID: 1075
		private static BufferedGraphicsContext graphics_context = new BufferedGraphicsContext();
	}
}
