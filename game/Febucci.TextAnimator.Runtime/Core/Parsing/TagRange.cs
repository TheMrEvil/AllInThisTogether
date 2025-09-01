using System;
using System.Text;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004E RID: 78
	public struct TagRange
	{
		// Token: 0x0600018F RID: 399 RVA: 0x000077B7 File Offset: 0x000059B7
		public TagRange(Vector2Int indexes, params ModifierInfo[] modifiers)
		{
			this.indexes = indexes;
			this.modifiers = modifiers;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000077C8 File Offset: 0x000059C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("indexes: ");
			stringBuilder.Append(this.indexes);
			if (this.modifiers == null || this.modifiers.Length == 0)
			{
				stringBuilder.Append("\n no modifiers");
			}
			else
			{
				for (int i = 0; i < this.modifiers.Length; i++)
				{
					stringBuilder.Append('\n');
					stringBuilder.Append('-');
					stringBuilder.Append(this.modifiers[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000115 RID: 277
		public Vector2Int indexes;

		// Token: 0x04000116 RID: 278
		public ModifierInfo[] modifiers;
	}
}
