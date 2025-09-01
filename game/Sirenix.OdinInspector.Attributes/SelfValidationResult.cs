using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200008F RID: 143
	public class SelfValidationResult
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004426 File Offset: 0x00002626
		public int Count
		{
			get
			{
				return this.itemsCount;
			}
		}

		// Token: 0x17000067 RID: 103
		public SelfValidationResult.ResultItem this[int index]
		{
			get
			{
				return ref this.items[index];
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000443C File Offset: 0x0000263C
		public ref SelfValidationResult.ResultItem AddError(string error)
		{
			return this.Add(new SelfValidationResult.ResultItem
			{
				Message = error,
				ResultType = SelfValidationResult.ResultType.Error
			});
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004468 File Offset: 0x00002668
		public ref SelfValidationResult.ResultItem AddWarning(string warning)
		{
			return this.Add(new SelfValidationResult.ResultItem
			{
				Message = warning,
				ResultType = SelfValidationResult.ResultType.Warning
			});
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004494 File Offset: 0x00002694
		public ref SelfValidationResult.ResultItem Add(ValidatorSeverity severity, string message)
		{
			if (severity == ValidatorSeverity.Error)
			{
				return this.Add(new SelfValidationResult.ResultItem
				{
					Message = message,
					ResultType = SelfValidationResult.ResultType.Error
				});
			}
			if (severity == ValidatorSeverity.Warning)
			{
				return this.Add(new SelfValidationResult.ResultItem
				{
					Message = message,
					ResultType = SelfValidationResult.ResultType.Warning
				});
			}
			SelfValidationResult.NoResultItem = default(SelfValidationResult.ResultItem);
			return ref SelfValidationResult.NoResultItem;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000044F8 File Offset: 0x000026F8
		public ref SelfValidationResult.ResultItem Add(SelfValidationResult.ResultItem item)
		{
			SelfValidationResult.ResultItem[] array = this.items;
			if (array == null)
			{
				array = new SelfValidationResult.ResultItem[2];
				this.items = array;
			}
			while (array.Length <= this.itemsCount + 1)
			{
				SelfValidationResult.ResultItem[] array2 = new SelfValidationResult.ResultItem[array.Length * 2];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i];
				}
				array = array2;
				this.items = array2;
			}
			array[this.itemsCount] = item;
			SelfValidationResult.ResultItem[] array3 = array;
			int num = this.itemsCount;
			this.itemsCount = num + 1;
			return ref array3[num];
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004580 File Offset: 0x00002780
		public SelfValidationResult()
		{
		}

		// Token: 0x0400028A RID: 650
		private static SelfValidationResult.ResultItem NoResultItem;

		// Token: 0x0400028B RID: 651
		private SelfValidationResult.ResultItem[] items;

		// Token: 0x0400028C RID: 652
		private int itemsCount;

		// Token: 0x020000A2 RID: 162
		public struct ContextMenuItem
		{
			// Token: 0x040008CC RID: 2252
			public string Path;

			// Token: 0x040008CD RID: 2253
			public bool On;

			// Token: 0x040008CE RID: 2254
			public bool AddSeparatorBefore;

			// Token: 0x040008CF RID: 2255
			public Action OnClick;
		}

		// Token: 0x020000A3 RID: 163
		public enum ResultType
		{
			// Token: 0x040008D1 RID: 2257
			Error,
			// Token: 0x040008D2 RID: 2258
			Warning,
			// Token: 0x040008D3 RID: 2259
			Valid
		}

		// Token: 0x020000A4 RID: 164
		public struct ResultItem
		{
			// Token: 0x040008D4 RID: 2260
			public string Message;

			// Token: 0x040008D5 RID: 2261
			public SelfValidationResult.ResultType ResultType;

			// Token: 0x040008D6 RID: 2262
			public SelfFix? Fix;

			// Token: 0x040008D7 RID: 2263
			public SelfValidationResult.ResultItemMetaData[] MetaData;

			// Token: 0x040008D8 RID: 2264
			public Func<IEnumerable<SelfValidationResult.ContextMenuItem>> OnContextClick;

			// Token: 0x040008D9 RID: 2265
			public Action OnSceneGUI;

			// Token: 0x040008DA RID: 2266
			public UnityEngine.Object SelectionObject;

			// Token: 0x040008DB RID: 2267
			public bool RichText;
		}

		// Token: 0x020000A5 RID: 165
		public struct ResultItemMetaData
		{
			// Token: 0x060001E1 RID: 481 RVA: 0x00004797 File Offset: 0x00002997
			public ResultItemMetaData(string name, object value, params Attribute[] attributes)
			{
				this.Name = name;
				this.Value = value;
				this.Attributes = attributes;
			}

			// Token: 0x040008DC RID: 2268
			public string Name;

			// Token: 0x040008DD RID: 2269
			public object Value;

			// Token: 0x040008DE RID: 2270
			public Attribute[] Attributes;
		}
	}
}
