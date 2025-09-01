using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000091 RID: 145
	public struct SelfFix
	{
		// Token: 0x060001CC RID: 460 RVA: 0x000045A4 File Offset: 0x000027A4
		public SelfFix(string name, Action action, bool offerInInspector)
		{
			this.Title = name;
			this.Action = action;
			this.OfferInInspector = offerInInspector;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000045A4 File Offset: 0x000027A4
		public SelfFix(string name, Delegate action, bool offerInInspector)
		{
			this.Title = name;
			this.Action = action;
			this.OfferInInspector = offerInInspector;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000045BB File Offset: 0x000027BB
		public static SelfFix Create(Action action, bool offerInInspector = true)
		{
			return new SelfFix("Fix", action, offerInInspector);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000045C9 File Offset: 0x000027C9
		public static SelfFix Create(string title, Action action, bool offerInInspector = true)
		{
			return new SelfFix(title, action, offerInInspector);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000045D3 File Offset: 0x000027D3
		public static SelfFix Create<T>(Action<T> action, bool offerInInspector = true) where T : new()
		{
			return new SelfFix("Fix", action, offerInInspector);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000045E1 File Offset: 0x000027E1
		public static SelfFix Create<T>(string title, Action<T> action, bool offerInInspector = true) where T : new()
		{
			return new SelfFix(title, action, offerInInspector);
		}

		// Token: 0x0400028D RID: 653
		public string Title;

		// Token: 0x0400028E RID: 654
		public Delegate Action;

		// Token: 0x0400028F RID: 655
		public bool OfferInInspector;
	}
}
