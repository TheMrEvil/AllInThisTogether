using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mono.CSharp
{
	// Token: 0x0200028D RID: 653
	public class TimeReporter
	{
		// Token: 0x06001FB9 RID: 8121 RVA: 0x0009BB5C File Offset: 0x00099D5C
		public TimeReporter(bool enabled)
		{
			if (!enabled)
			{
				return;
			}
			this.timers = new Stopwatch[Enum.GetValues(typeof(TimeReporter.TimerType)).Length];
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0009BB88 File Offset: 0x00099D88
		public void Start(TimeReporter.TimerType type)
		{
			if (this.timers != null)
			{
				Stopwatch stopwatch = new Stopwatch();
				this.timers[(int)type] = stopwatch;
				stopwatch.Start();
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0009BBB2 File Offset: 0x00099DB2
		public void StartTotal()
		{
			this.total = new Stopwatch();
			this.total.Start();
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0009BBCA File Offset: 0x00099DCA
		public void Stop(TimeReporter.TimerType type)
		{
			if (this.timers != null)
			{
				this.timers[(int)type].Stop();
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0009BBE1 File Offset: 0x00099DE1
		public void StopTotal()
		{
			this.total.Stop();
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0009BBF0 File Offset: 0x00099DF0
		public void ShowStats()
		{
			if (this.timers == null)
			{
				return;
			}
			Dictionary<TimeReporter.TimerType, string> dictionary = new Dictionary<TimeReporter.TimerType, string>
			{
				{
					TimeReporter.TimerType.ParseTotal,
					"Parsing source files"
				},
				{
					TimeReporter.TimerType.AssemblyBuilderSetup,
					"Assembly builder setup"
				},
				{
					TimeReporter.TimerType.CreateTypeTotal,
					"Compiled types created"
				},
				{
					TimeReporter.TimerType.ReferencesLoading,
					"Referenced assemblies loading"
				},
				{
					TimeReporter.TimerType.ReferencesImporting,
					"Referenced assemblies importing"
				},
				{
					TimeReporter.TimerType.PredefinedTypesInit,
					"Predefined types initialization"
				},
				{
					TimeReporter.TimerType.ModuleDefinitionTotal,
					"Module definition"
				},
				{
					TimeReporter.TimerType.EmitTotal,
					"Resolving and emitting members blocks"
				},
				{
					TimeReporter.TimerType.CloseTypes,
					"Module types closed"
				},
				{
					TimeReporter.TimerType.Resouces,
					"Embedding resources"
				},
				{
					TimeReporter.TimerType.OutputSave,
					"Writing output file"
				},
				{
					TimeReporter.TimerType.DebugSave,
					"Writing debug symbols file"
				}
			};
			int num = 0;
			double num2 = (double)this.total.ElapsedMilliseconds / 100.0;
			long num3 = this.total.ElapsedMilliseconds;
			foreach (Stopwatch stopwatch in this.timers)
			{
				string arg = dictionary[(TimeReporter.TimerType)(num++)];
				long num4 = (stopwatch == null) ? 0L : stopwatch.ElapsedMilliseconds;
				Console.WriteLine("{0,4:0.0}% {1,5}ms {2}", (double)num4 / num2, num4, arg);
				num3 -= num4;
			}
			Console.WriteLine("{0,4:0.0}% {1,5}ms Other tasks", (double)num3 / num2, num3);
			Console.WriteLine();
			Console.WriteLine("Total elapsed time: {0}", this.total.Elapsed);
		}

		// Token: 0x04000B9D RID: 2973
		private readonly Stopwatch[] timers;

		// Token: 0x04000B9E RID: 2974
		private Stopwatch total;

		// Token: 0x020003E7 RID: 999
		public enum TimerType
		{
			// Token: 0x04001116 RID: 4374
			ParseTotal,
			// Token: 0x04001117 RID: 4375
			AssemblyBuilderSetup,
			// Token: 0x04001118 RID: 4376
			CreateTypeTotal,
			// Token: 0x04001119 RID: 4377
			ReferencesLoading,
			// Token: 0x0400111A RID: 4378
			ReferencesImporting,
			// Token: 0x0400111B RID: 4379
			PredefinedTypesInit,
			// Token: 0x0400111C RID: 4380
			ModuleDefinitionTotal,
			// Token: 0x0400111D RID: 4381
			EmitTotal,
			// Token: 0x0400111E RID: 4382
			CloseTypes,
			// Token: 0x0400111F RID: 4383
			Resouces,
			// Token: 0x04001120 RID: 4384
			OutputSave,
			// Token: 0x04001121 RID: 4385
			DebugSave
		}
	}
}
