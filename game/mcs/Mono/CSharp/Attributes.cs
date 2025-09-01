using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000121 RID: 289
	public class Attributes
	{
		// Token: 0x06000E4A RID: 3658 RVA: 0x00036246 File Offset: 0x00034446
		public Attributes(Attribute a)
		{
			this.Attrs = new List<Attribute>();
			this.Attrs.Add(a);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00036265 File Offset: 0x00034465
		public Attributes(List<Attribute> attrs)
		{
			this.Attrs = (attrs ?? new List<Attribute>());
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003627D File Offset: 0x0003447D
		public void AddAttribute(Attribute attr)
		{
			this.Attrs.Add(attr);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003628B File Offset: 0x0003448B
		public void AddAttributes(List<Attribute> attrs)
		{
			this.Attrs.AddRange(attrs);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003629C File Offset: 0x0003449C
		public void AttachTo(Attributable attributable, IMemberContext context)
		{
			foreach (Attribute attribute in this.Attrs)
			{
				attribute.AttachTo(attributable, context);
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000362F0 File Offset: 0x000344F0
		public Attributes Clone()
		{
			List<Attribute> list = new List<Attribute>(this.Attrs.Count);
			foreach (Attribute attribute in this.Attrs)
			{
				list.Add(attribute.Clone());
			}
			return new Attributes(list);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00036360 File Offset: 0x00034560
		public bool CheckTargets()
		{
			for (int i = 0; i < this.Attrs.Count; i++)
			{
				if (!this.Attrs[i].CheckTarget())
				{
					this.Attrs.RemoveAt(i--);
				}
			}
			return true;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000363A8 File Offset: 0x000345A8
		public void ConvertGlobalAttributes(TypeContainer member, NamespaceContainer currentNamespace, bool isGlobal)
		{
			string[] validAttributeTargets = member.ValidAttributeTargets;
			for (int i = 0; i < this.Attrs.Count; i++)
			{
				Attribute attribute = this.Attrs[0];
				if (attribute.ExplicitTarget != null)
				{
					int j;
					for (j = 0; j < validAttributeTargets.Length; j++)
					{
						if (attribute.ExplicitTarget == validAttributeTargets[j])
						{
							j = -1;
							break;
						}
					}
					if (j >= 0 && isGlobal)
					{
						member.Module.AddAttribute(attribute, currentNamespace);
						this.Attrs.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00036430 File Offset: 0x00034630
		public bool HasResolveError()
		{
			using (List<Attribute>.Enumerator enumerator = this.Attrs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ResolveError)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003648C File Offset: 0x0003468C
		public Attribute Search(PredefinedAttribute t)
		{
			return this.Search(null, t);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00036498 File Offset: 0x00034698
		public Attribute Search(string explicitTarget, PredefinedAttribute t)
		{
			foreach (Attribute attribute in this.Attrs)
			{
				if ((explicitTarget == null || !(attribute.ExplicitTarget != explicitTarget)) && attribute.ResolveTypeForComparison() == t)
				{
					return attribute;
				}
			}
			return null;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003650C File Offset: 0x0003470C
		public Attribute[] SearchMulti(PredefinedAttribute t)
		{
			List<Attribute> list = null;
			foreach (Attribute attribute in this.Attrs)
			{
				if (attribute.ResolveTypeForComparison() == t)
				{
					if (list == null)
					{
						list = new List<Attribute>(this.Attrs.Count);
					}
					list.Add(attribute);
				}
			}
			if (list != null)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00036590 File Offset: 0x00034790
		public void Emit()
		{
			this.CheckTargets();
			Dictionary<Attribute, List<Attribute>> dictionary = (this.Attrs.Count > 1) ? new Dictionary<Attribute, List<Attribute>>() : null;
			foreach (Attribute attribute in this.Attrs)
			{
				attribute.Emit(dictionary);
			}
			if (dictionary == null || dictionary.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<Attribute, List<Attribute>> keyValuePair in dictionary)
			{
				if (keyValuePair.Value != null)
				{
					Attribute key = keyValuePair.Key;
					foreach (Attribute attribute2 in keyValuePair.Value)
					{
						key.Report.SymbolRelatedToPreviousError(attribute2.Location, "");
					}
					key.Report.Error(579, key.Location, "The attribute `{0}' cannot be applied multiple times", key.GetSignatureForError());
				}
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000366D4 File Offset: 0x000348D4
		public bool Contains(PredefinedAttribute t)
		{
			return this.Search(t) != null;
		}

		// Token: 0x04000691 RID: 1681
		public readonly List<Attribute> Attrs;
	}
}
