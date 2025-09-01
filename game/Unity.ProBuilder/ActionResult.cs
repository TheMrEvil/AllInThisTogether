using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000002 RID: 2
	public sealed class ActionResult
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public ActionResult.Status status
		{
			[CompilerGenerated]
			get
			{
				return this.<status>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<status>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public string notification
		{
			[CompilerGenerated]
			get
			{
				return this.<notification>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<notification>k__BackingField = value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public ActionResult(ActionResult.Status status, string notification)
		{
			this.status = status;
			this.notification = notification;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		public static implicit operator bool(ActionResult res)
		{
			return res != null && res.status == ActionResult.Status.Success;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public bool ToBool()
		{
			return this.status == ActionResult.Status.Success;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020A3 File Offset: 0x000002A3
		public static bool FromBool(bool success)
		{
			return success ? ActionResult.Success : new ActionResult(ActionResult.Status.Failure, "Failure");
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BF File Offset: 0x000002BF
		public static ActionResult Success
		{
			get
			{
				return new ActionResult(ActionResult.Status.Success, "");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020CC File Offset: 0x000002CC
		public static ActionResult NoSelection
		{
			get
			{
				return new ActionResult(ActionResult.Status.Canceled, "Nothing Selected");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020D9 File Offset: 0x000002D9
		public static ActionResult UserCanceled
		{
			get
			{
				return new ActionResult(ActionResult.Status.Canceled, "User Canceled");
			}
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		private ActionResult.Status <status>k__BackingField;

		// Token: 0x04000002 RID: 2
		[CompilerGenerated]
		private string <notification>k__BackingField;

		// Token: 0x0200008D RID: 141
		public enum Status
		{
			// Token: 0x04000279 RID: 633
			Success,
			// Token: 0x0400027A RID: 634
			Failure,
			// Token: 0x0400027B RID: 635
			Canceled,
			// Token: 0x0400027C RID: 636
			NoChange
		}
	}
}
