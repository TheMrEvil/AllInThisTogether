using System;
using System.Diagnostics;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x0200018B RID: 395
	[DebuggerDisplay("{GetSignatureForError()}")]
	public class MemberName
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x000666DE File Offset: 0x000648DE
		public MemberName(string name) : this(name, Location.Null)
		{
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000666EC File Offset: 0x000648EC
		public MemberName(string name, Location loc) : this(null, name, loc)
		{
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000666F7 File Offset: 0x000648F7
		public MemberName(string name, TypeParameters tparams, Location loc)
		{
			this.Name = name;
			this.Location = loc;
			this.TypeParameters = tparams;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00066714 File Offset: 0x00064914
		public MemberName(string name, TypeParameters tparams, FullNamedExpression explicitInterface, Location loc) : this(name, tparams, loc)
		{
			this.ExplicitInterface = explicitInterface;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00066727 File Offset: 0x00064927
		public MemberName(MemberName left, string name, Location loc)
		{
			this.Name = name;
			this.Location = loc;
			this.Left = left;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00066744 File Offset: 0x00064944
		public MemberName(MemberName left, string name, FullNamedExpression explicitInterface, Location loc) : this(left, name, loc)
		{
			this.ExplicitInterface = explicitInterface;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00066757 File Offset: 0x00064957
		public MemberName(MemberName left, MemberName right)
		{
			this.Name = right.Name;
			this.Location = right.Location;
			this.TypeParameters = right.TypeParameters;
			this.Left = left;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0006678A File Offset: 0x0006498A
		public int Arity
		{
			get
			{
				if (this.TypeParameters != null)
				{
					return this.TypeParameters.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000667A1 File Offset: 0x000649A1
		public bool IsGeneric
		{
			get
			{
				return this.TypeParameters != null;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000667AC File Offset: 0x000649AC
		public string Basename
		{
			get
			{
				if (this.TypeParameters != null)
				{
					return MemberName.MakeName(this.Name, this.TypeParameters);
				}
				return this.Name;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000667CE File Offset: 0x000649CE
		public void CreateMetadataName(StringBuilder sb)
		{
			if (this.Left != null)
			{
				this.Left.CreateMetadataName(sb);
			}
			if (sb.Length != 0)
			{
				sb.Append(".");
			}
			sb.Append(this.Basename);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00066808 File Offset: 0x00064A08
		public string GetSignatureForDocumentation()
		{
			string text = this.Basename;
			if (this.ExplicitInterface != null)
			{
				text = this.ExplicitInterface.GetSignatureForError() + "." + text;
			}
			if (this.Left == null)
			{
				return text;
			}
			return this.Left.GetSignatureForDocumentation() + "." + text;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0006685C File Offset: 0x00064A5C
		public string GetSignatureForError()
		{
			string text = (this.TypeParameters == null) ? null : ("<" + this.TypeParameters.GetSignatureForError() + ">");
			text = this.Name + text;
			if (this.ExplicitInterface != null)
			{
				text = this.ExplicitInterface.GetSignatureForError() + "." + text;
			}
			if (this.Left == null)
			{
				return text;
			}
			return this.Left.GetSignatureForError() + "." + text;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000668DB File Offset: 0x00064ADB
		public override bool Equals(object other)
		{
			return this.Equals(other as MemberName);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000668EC File Offset: 0x00064AEC
		public bool Equals(MemberName other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null || this.Name != other.Name)
			{
				return false;
			}
			if (this.TypeParameters != null && (other.TypeParameters == null || this.TypeParameters.Count != other.TypeParameters.Count))
			{
				return false;
			}
			if (this.TypeParameters == null && other.TypeParameters != null)
			{
				return false;
			}
			if (this.Left == null)
			{
				return other.Left == null;
			}
			return this.Left.Equals(other.Left);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00066978 File Offset: 0x00064B78
		public override int GetHashCode()
		{
			int num = this.Name.GetHashCode();
			for (MemberName left = this.Left; left != null; left = left.Left)
			{
				num ^= left.Name.GetHashCode();
			}
			if (this.TypeParameters != null)
			{
				num ^= this.TypeParameters.Count << 5;
			}
			return num & int.MaxValue;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000669D1 File Offset: 0x00064BD1
		public static string MakeName(string name, TypeParameters args)
		{
			if (args == null)
			{
				return name;
			}
			return name + "`" + args.Count;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000669EE File Offset: 0x00064BEE
		// Note: this type is marked as 'beforefieldinit'.
		static MemberName()
		{
		}

		// Token: 0x040008FF RID: 2303
		public static readonly MemberName Null = new MemberName("");

		// Token: 0x04000900 RID: 2304
		public readonly string Name;

		// Token: 0x04000901 RID: 2305
		public TypeParameters TypeParameters;

		// Token: 0x04000902 RID: 2306
		public readonly FullNamedExpression ExplicitInterface;

		// Token: 0x04000903 RID: 2307
		public readonly Location Location;

		// Token: 0x04000904 RID: 2308
		public readonly MemberName Left;
	}
}
