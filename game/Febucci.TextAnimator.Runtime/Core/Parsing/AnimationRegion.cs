using System;
using System.Text;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004C RID: 76
	public class AnimationRegion : RegionBase
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00007468 File Offset: 0x00005668
		public AnimationRegion(string tagId, VisibilityMode visibilityMode, AnimationScriptableBase animation) : base(tagId)
		{
			this.visibilityMode = visibilityMode;
			this.animation = animation;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000747F File Offset: 0x0000567F
		public bool IsVisibilityPolicySatisfied(bool visible)
		{
			return this.visibilityMode == VisibilityMode.Persistent || this.visibilityMode.HasFlag(VisibilityMode.OnVisible) == visible;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000074A5 File Offset: 0x000056A5
		public void OpenNewRange(int startIndex)
		{
			this.OpenNewRange(startIndex, Array.Empty<string>());
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000074B4 File Offset: 0x000056B4
		public void OpenNewRange(int startIndex, string[] tagWords)
		{
			Array.Resize<TagRange>(ref this.ranges, this.ranges.Length + 1);
			TagRange tagRange = new TagRange(new Vector2Int(startIndex, int.MaxValue), Array.Empty<ModifierInfo>());
			for (int i = 1; i < tagWords.Length; i++)
			{
				string text = tagWords[i];
				int num = text.IndexOf('=');
				float value;
				if (num > 0 && FormatUtils.TryGetFloat(text.Substring(num + 1), 0f, out value))
				{
					Array.Resize<ModifierInfo>(ref tagRange.modifiers, tagRange.modifiers.Length + 1);
					tagRange.modifiers[tagRange.modifiers.Length - 1] = new ModifierInfo(text.Substring(0, num), value);
				}
			}
			this.ranges[this.ranges.Length - 1] = tagRange;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007574 File Offset: 0x00005774
		public void TryClosingRange(int endIndex)
		{
			if (this.ranges.Length == 0)
			{
				return;
			}
			for (int i = this.ranges.Length - 1; i >= 0; i--)
			{
				if (this.ranges[i].indexes.y == 2147483647)
				{
					TagRange tagRange = this.ranges[i];
					tagRange.indexes.y = endIndex;
					this.ranges[i] = tagRange;
					return;
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000075E8 File Offset: 0x000057E8
		public void CloseAllOpenedRanges(int endIndex)
		{
			if (this.ranges.Length == 0)
			{
				return;
			}
			for (int i = this.ranges.Length - 1; i >= 0; i--)
			{
				if (this.ranges[i].indexes.y == 2147483647)
				{
					TagRange tagRange = this.ranges[i];
					tagRange.indexes.y = endIndex;
					this.ranges[i] = tagRange;
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007658 File Offset: 0x00005858
		public virtual void SetupContextFor(TAnimCore animator, ModifierInfo[] modifiers)
		{
			this.animation.ResetContext(animator);
			foreach (ModifierInfo modifier in modifiers)
			{
				this.animation.SetModifier(modifier);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007698 File Offset: 0x00005898
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("tag: ");
			stringBuilder.Append(this.tagId);
			if (this.ranges.Length == 0)
			{
				stringBuilder.Append("\nNo ranges");
			}
			else
			{
				for (int i = 0; i < this.ranges.Length; i++)
				{
					stringBuilder.Append('\n');
					stringBuilder.Append('-');
					stringBuilder.Append('-');
					stringBuilder.Append(this.ranges[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000111 RID: 273
		private readonly VisibilityMode visibilityMode;

		// Token: 0x04000112 RID: 274
		public readonly AnimationScriptableBase animation;
	}
}
