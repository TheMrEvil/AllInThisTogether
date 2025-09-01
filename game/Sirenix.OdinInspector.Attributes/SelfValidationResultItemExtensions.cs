using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200008E RID: 142
	public static class SelfValidationResultItemExtensions
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x000041B9 File Offset: 0x000023B9
		public static ref SelfValidationResult.ResultItem WithFix(this SelfValidationResult.ResultItem item, string title, Action fix, bool offerInInspector = true)
		{
			item.Fix = new SelfFix?(SelfFix.Create(title, fix, offerInInspector));
			return ref item;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000041CF File Offset: 0x000023CF
		public static ref SelfValidationResult.ResultItem WithFix<T>(this SelfValidationResult.ResultItem item, string title, Action<T> fix, bool offerInInspector = true) where T : new()
		{
			item.Fix = new SelfFix?(SelfFix.Create<T>(title, fix, offerInInspector));
			return ref item;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000041E5 File Offset: 0x000023E5
		public static ref SelfValidationResult.ResultItem WithFix(this SelfValidationResult.ResultItem item, Action fix, bool offerInInspector = true)
		{
			item.Fix = new SelfFix?(SelfFix.Create(fix, offerInInspector));
			return ref item;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000041FA File Offset: 0x000023FA
		public static ref SelfValidationResult.ResultItem WithFix<T>(this SelfValidationResult.ResultItem item, Action<T> fix, bool offerInInspector = true) where T : new()
		{
			item.Fix = new SelfFix?(SelfFix.Create<T>(fix, offerInInspector));
			return ref item;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000420F File Offset: 0x0000240F
		public static ref SelfValidationResult.ResultItem WithFix(this SelfValidationResult.ResultItem item, SelfFix fix)
		{
			item.Fix = new SelfFix?(fix);
			return ref item;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000421E File Offset: 0x0000241E
		public static ref SelfValidationResult.ResultItem WithContextClick(this SelfValidationResult.ResultItem item, Func<IEnumerable<SelfValidationResult.ContextMenuItem>> onContextClick)
		{
			item.OnContextClick = (Func<IEnumerable<SelfValidationResult.ContextMenuItem>>)Delegate.Combine(item.OnContextClick, onContextClick);
			return ref item;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004238 File Offset: 0x00002438
		public static ref SelfValidationResult.ResultItem WithContextClick(this SelfValidationResult.ResultItem item, string path, Action onClick)
		{
			item.OnContextClick = (Func<IEnumerable<SelfValidationResult.ContextMenuItem>>)Delegate.Combine(item.OnContextClick, new Func<IEnumerable<SelfValidationResult.ContextMenuItem>>(() => new SelfValidationResult.ContextMenuItem[]
			{
				new SelfValidationResult.ContextMenuItem
				{
					Path = path,
					OnClick = onClick
				}
			}));
			return ref item;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000427C File Offset: 0x0000247C
		public static ref SelfValidationResult.ResultItem WithContextClick(this SelfValidationResult.ResultItem item, string path, bool on, Action onClick)
		{
			item.OnContextClick = (Func<IEnumerable<SelfValidationResult.ContextMenuItem>>)Delegate.Combine(item.OnContextClick, new Func<IEnumerable<SelfValidationResult.ContextMenuItem>>(() => new SelfValidationResult.ContextMenuItem[]
			{
				new SelfValidationResult.ContextMenuItem
				{
					Path = path,
					On = on,
					OnClick = onClick
				}
			}));
			return ref item;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000042C4 File Offset: 0x000024C4
		public static ref SelfValidationResult.ResultItem WithContextClick(this SelfValidationResult.ResultItem item, SelfValidationResult.ContextMenuItem onContextClick)
		{
			item.OnContextClick = (Func<IEnumerable<SelfValidationResult.ContextMenuItem>>)Delegate.Combine(item.OnContextClick, new Func<IEnumerable<SelfValidationResult.ContextMenuItem>>(() => new SelfValidationResult.ContextMenuItem[]
			{
				onContextClick
			}));
			return ref item;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000042FE File Offset: 0x000024FE
		public static ref SelfValidationResult.ResultItem WithSceneGUI(this SelfValidationResult.ResultItem item, Action onSceneGUI)
		{
			item.OnSceneGUI = onSceneGUI;
			return ref item;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004308 File Offset: 0x00002508
		public static ref SelfValidationResult.ResultItem SetSelectionObject(this SelfValidationResult.ResultItem item, UnityEngine.Object uObj)
		{
			item.SelectionObject = uObj;
			return ref item;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004312 File Offset: 0x00002512
		public static ref SelfValidationResult.ResultItem EnableRichText(this SelfValidationResult.ResultItem item)
		{
			item.RichText = true;
			return ref item;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000431C File Offset: 0x0000251C
		public static ref SelfValidationResult.ResultItem WithMetaData(this SelfValidationResult.ResultItem resultItem, string name, object value, params Attribute[] attributes)
		{
			resultItem.MetaData = (resultItem.MetaData ?? new SelfValidationResult.ResultItemMetaData[0]);
			Array.Resize<SelfValidationResult.ResultItemMetaData>(ref resultItem.MetaData, resultItem.MetaData.Length + 1);
			resultItem.MetaData[resultItem.MetaData.Length - 1] = new SelfValidationResult.ResultItemMetaData(name, value, attributes);
			return ref resultItem;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004374 File Offset: 0x00002574
		public static ref SelfValidationResult.ResultItem WithMetaData(this SelfValidationResult.ResultItem resultItem, object value, params Attribute[] attributes)
		{
			resultItem.MetaData = (resultItem.MetaData ?? new SelfValidationResult.ResultItemMetaData[0]);
			Array.Resize<SelfValidationResult.ResultItemMetaData>(ref resultItem.MetaData, resultItem.MetaData.Length + 1);
			resultItem.MetaData[resultItem.MetaData.Length - 1] = new SelfValidationResult.ResultItemMetaData(null, value, attributes);
			return ref resultItem;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000043CC File Offset: 0x000025CC
		public static ref SelfValidationResult.ResultItem WithButton(this SelfValidationResult.ResultItem resultItem, string name, Action onClick)
		{
			resultItem.MetaData = (resultItem.MetaData ?? new SelfValidationResult.ResultItemMetaData[0]);
			Array.Resize<SelfValidationResult.ResultItemMetaData>(ref resultItem.MetaData, resultItem.MetaData.Length + 1);
			resultItem.MetaData[resultItem.MetaData.Length - 1] = new SelfValidationResult.ResultItemMetaData(name, onClick, Array.Empty<Attribute>());
			return ref resultItem;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x060001DB RID: 475 RVA: 0x00004580 File Offset: 0x00002780
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x060001DC RID: 476 RVA: 0x000046F8 File Offset: 0x000028F8
			internal IEnumerable<SelfValidationResult.ContextMenuItem> <WithContextClick>b__0()
			{
				return new SelfValidationResult.ContextMenuItem[]
				{
					new SelfValidationResult.ContextMenuItem
					{
						Path = this.path,
						OnClick = this.onClick
					}
				};
			}

			// Token: 0x040008C6 RID: 2246
			public string path;

			// Token: 0x040008C7 RID: 2247
			public Action onClick;
		}

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060001DD RID: 477 RVA: 0x00004580 File Offset: 0x00002780
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00004738 File Offset: 0x00002938
			internal IEnumerable<SelfValidationResult.ContextMenuItem> <WithContextClick>b__0()
			{
				return new SelfValidationResult.ContextMenuItem[]
				{
					new SelfValidationResult.ContextMenuItem
					{
						Path = this.path,
						On = this.on,
						OnClick = this.onClick
					}
				};
			}

			// Token: 0x040008C8 RID: 2248
			public string path;

			// Token: 0x040008C9 RID: 2249
			public bool on;

			// Token: 0x040008CA RID: 2250
			public Action onClick;
		}

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x060001DF RID: 479 RVA: 0x00004580 File Offset: 0x00002780
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x00004782 File Offset: 0x00002982
			internal IEnumerable<SelfValidationResult.ContextMenuItem> <WithContextClick>b__0()
			{
				return new SelfValidationResult.ContextMenuItem[]
				{
					this.onContextClick
				};
			}

			// Token: 0x040008CB RID: 2251
			public SelfValidationResult.ContextMenuItem onContextClick;
		}
	}
}
