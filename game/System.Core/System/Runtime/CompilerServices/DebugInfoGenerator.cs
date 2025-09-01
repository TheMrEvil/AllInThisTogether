using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.CompilerServices
{
	/// <summary>Generates debug information for lambda expressions in an expression tree.</summary>
	// Token: 0x020002E3 RID: 739
	public abstract class DebugInfoGenerator
	{
		/// <summary>Creates a program database (PDB) symbol generator.</summary>
		/// <returns>A PDB symbol generator.</returns>
		// Token: 0x0600166E RID: 5742 RVA: 0x00003A06 File Offset: 0x00001C06
		public static DebugInfoGenerator CreatePdbGenerator()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Marks a sequence point in Microsoft intermediate language (MSIL) code.</summary>
		/// <param name="method">The lambda expression that is generated.</param>
		/// <param name="ilOffset">The offset within MSIL code at which to mark the sequence point.</param>
		/// <param name="sequencePoint">Debug information that corresponds to the sequence point.</param>
		// Token: 0x0600166F RID: 5743
		public abstract void MarkSequencePoint(LambdaExpression method, int ilOffset, DebugInfoExpression sequencePoint);

		// Token: 0x06001670 RID: 5744 RVA: 0x0004BD6D File Offset: 0x00049F6D
		internal virtual void MarkSequencePoint(LambdaExpression method, MethodBase methodBase, ILGenerator ilg, DebugInfoExpression sequencePoint)
		{
			this.MarkSequencePoint(method, ilg.ILOffset, sequencePoint);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00003A59 File Offset: 0x00001C59
		internal virtual void SetLocalName(LocalBuilder localBuilder, string name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DebugInfoGenerator" /> class.</summary>
		// Token: 0x06001672 RID: 5746 RVA: 0x00002162 File Offset: 0x00000362
		protected DebugInfoGenerator()
		{
		}
	}
}
