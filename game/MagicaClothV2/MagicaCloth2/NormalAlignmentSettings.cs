using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public class NormalAlignmentSettings : IValid, IDataValidate, ITransform
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00005305 File Offset: 0x00003505
		public void DataValidate()
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public bool IsValid()
		{
			NormalAlignmentSettings.AlignmentMode alignmentMode = this.alignmentMode;
			return alignmentMode <= NormalAlignmentSettings.AlignmentMode.Transform;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000DB73 File Offset: 0x0000BD73
		public NormalAlignmentSettings Clone()
		{
			return new NormalAlignmentSettings
			{
				alignmentMode = this.alignmentMode,
				adjustmentTransform = this.adjustmentTransform
			};
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public override int GetHashCode()
		{
			int num = 0;
			num = (int)(num + this.alignmentMode * (NormalAlignmentSettings.AlignmentMode)105);
			if (this.adjustmentTransform)
			{
				num += this.adjustmentTransform.GetInstanceID();
				num += this.adjustmentTransform.position.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000DBE7 File Offset: 0x0000BDE7
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			if (this.adjustmentTransform)
			{
				transformSet.Add(this.adjustmentTransform);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000DC03 File Offset: 0x0000BE03
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if (this.adjustmentTransform && replaceDict.ContainsKey(this.adjustmentTransform.GetInstanceID()))
			{
				this.adjustmentTransform = replaceDict[this.adjustmentTransform.GetInstanceID()];
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00002058 File Offset: 0x00000258
		public NormalAlignmentSettings()
		{
		}

		// Token: 0x0400022C RID: 556
		public NormalAlignmentSettings.AlignmentMode alignmentMode;

		// Token: 0x0400022D RID: 557
		public Transform adjustmentTransform;

		// Token: 0x0200005F RID: 95
		public enum AlignmentMode
		{
			// Token: 0x0400022F RID: 559
			None,
			// Token: 0x04000230 RID: 560
			BoundingBoxCenter,
			// Token: 0x04000231 RID: 561
			Transform
		}
	}
}
