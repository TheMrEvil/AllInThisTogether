using System;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure
{
	// Token: 0x0200004A RID: 74
	public class ParseFailureException : Exception
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000C43D File Offset: 0x0000A63D
		internal ParseFailureException(ParseFailureException.ErrorCode code, string message, Exception cause = null) : base(message, cause)
		{
			this.Code = code;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000C44E File Offset: 0x0000A64E
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000C456 File Offset: 0x0000A656
		public ParseFailureException.ErrorCode Code
		{
			[CompilerGenerated]
			get
			{
				return this.<Code>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Code>k__BackingField = value;
			}
		}

		// Token: 0x040000BC RID: 188
		[CompilerGenerated]
		private ParseFailureException.ErrorCode <Code>k__BackingField;

		// Token: 0x02000116 RID: 278
		public enum ErrorCode
		{
			// Token: 0x04000257 RID: 599
			OtherCause = -1,
			// Token: 0x04000258 RID: 600
			InternalServerError = 1,
			// Token: 0x04000259 RID: 601
			ConnectionFailed = 100,
			// Token: 0x0400025A RID: 602
			ObjectNotFound,
			// Token: 0x0400025B RID: 603
			InvalidQuery,
			// Token: 0x0400025C RID: 604
			InvalidClassName,
			// Token: 0x0400025D RID: 605
			MissingObjectId,
			// Token: 0x0400025E RID: 606
			InvalidKeyName,
			// Token: 0x0400025F RID: 607
			InvalidPointer,
			// Token: 0x04000260 RID: 608
			InvalidJSON,
			// Token: 0x04000261 RID: 609
			CommandUnavailable,
			// Token: 0x04000262 RID: 610
			NotInitialized,
			// Token: 0x04000263 RID: 611
			IncorrectType = 111,
			// Token: 0x04000264 RID: 612
			InvalidChannelName,
			// Token: 0x04000265 RID: 613
			PushMisconfigured = 115,
			// Token: 0x04000266 RID: 614
			ObjectTooLarge,
			// Token: 0x04000267 RID: 615
			OperationForbidden = 119,
			// Token: 0x04000268 RID: 616
			CacheMiss,
			// Token: 0x04000269 RID: 617
			InvalidNestedKey,
			// Token: 0x0400026A RID: 618
			InvalidFileName,
			// Token: 0x0400026B RID: 619
			InvalidACL,
			// Token: 0x0400026C RID: 620
			Timeout,
			// Token: 0x0400026D RID: 621
			InvalidEmailAddress,
			// Token: 0x0400026E RID: 622
			DuplicateValue = 137,
			// Token: 0x0400026F RID: 623
			InvalidRoleName = 139,
			// Token: 0x04000270 RID: 624
			ExceededQuota,
			// Token: 0x04000271 RID: 625
			ScriptFailed,
			// Token: 0x04000272 RID: 626
			ValidationFailed,
			// Token: 0x04000273 RID: 627
			FileDeleteFailed = 153,
			// Token: 0x04000274 RID: 628
			RequestLimitExceeded = 155,
			// Token: 0x04000275 RID: 629
			InvalidEventName = 160,
			// Token: 0x04000276 RID: 630
			UsernameMissing = 200,
			// Token: 0x04000277 RID: 631
			PasswordMissing,
			// Token: 0x04000278 RID: 632
			UsernameTaken,
			// Token: 0x04000279 RID: 633
			EmailTaken,
			// Token: 0x0400027A RID: 634
			EmailMissing,
			// Token: 0x0400027B RID: 635
			EmailNotFound,
			// Token: 0x0400027C RID: 636
			SessionMissing,
			// Token: 0x0400027D RID: 637
			MustCreateUserThroughSignup,
			// Token: 0x0400027E RID: 638
			AccountAlreadyLinked,
			// Token: 0x0400027F RID: 639
			InvalidSessionToken,
			// Token: 0x04000280 RID: 640
			LinkedIdMissing = 250,
			// Token: 0x04000281 RID: 641
			InvalidLinkedSession,
			// Token: 0x04000282 RID: 642
			UnsupportedService
		}
	}
}
