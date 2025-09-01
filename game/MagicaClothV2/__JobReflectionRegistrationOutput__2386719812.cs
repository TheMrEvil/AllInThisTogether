using System;
using MagicaCloth2;
using Unity.Jobs;
using UnityEngine;

// Token: 0x02000137 RID: 311
[DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__2386719812
{
	// Token: 0x06000543 RID: 1347 RVA: 0x0002B920 File Offset: 0x00029B20
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<AngleConstraint.AngleConstraintJob>();
		}
		catch (Exception ex)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex, typeof(AngleConstraint.AngleConstraintJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<ColliderCollisionConstraint.PointColliderCollisionConstraintJob>();
		}
		catch (Exception ex2)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex2, typeof(ColliderCollisionConstraint.PointColliderCollisionConstraintJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<ColliderCollisionConstraint.EdgeColliderCollisionConstraintJob>();
		}
		catch (Exception ex3)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex3, typeof(ColliderCollisionConstraint.EdgeColliderCollisionConstraintJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<ColliderCollisionConstraint.SolveEdgeBufferAndClearJob>();
		}
		catch (Exception ex4)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex4, typeof(ColliderCollisionConstraint.SolveEdgeBufferAndClearJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<DistanceConstraint.DistanceConstraintJob>();
		}
		catch (Exception ex5)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex5, typeof(DistanceConstraint.DistanceConstraintJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<MotionConstraint.MotionConstraintJob>();
		}
		catch (Exception ex6)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex6, typeof(MotionConstraint.MotionConstraintJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.UpdatePrimitiveJob>();
		}
		catch (Exception ex7)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex7, typeof(SelfCollisionConstraint.UpdatePrimitiveJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.PointTriangleBroadPhaseJob>();
		}
		catch (Exception ex8)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex8, typeof(SelfCollisionConstraint.PointTriangleBroadPhaseJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.EdgeEdgeBroadPhaseJob>();
		}
		catch (Exception ex9)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex9, typeof(SelfCollisionConstraint.EdgeEdgeBroadPhaseJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseJob>();
		}
		catch (Exception ex10)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex10, typeof(SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.UpdatePointTriangleBroadPhaseJob>();
		}
		catch (Exception ex11)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex11, typeof(SelfCollisionConstraint.UpdatePointTriangleBroadPhaseJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.SolverEdgeEdgeJob>();
		}
		catch (Exception ex12)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex12, typeof(SelfCollisionConstraint.SolverEdgeEdgeJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.SolverPointTriangleJob>();
		}
		catch (Exception ex13)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex13, typeof(SelfCollisionConstraint.SolverPointTriangleJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.IntersectUpdatePrimitiveJob>();
		}
		catch (Exception ex14)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex14, typeof(SelfCollisionConstraint.IntersectUpdatePrimitiveJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SelfCollisionConstraint.IntersectEdgeTriangleJob>();
		}
		catch (Exception ex15)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex15, typeof(SelfCollisionConstraint.IntersectEdgeTriangleJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<TetherConstraint.TethreConstraintJob>();
		}
		catch (Exception ex16)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex16, typeof(TetherConstraint.TethreConstraintJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<TriangleBendingConstraint.TriangleBendingJob>();
		}
		catch (Exception ex17)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex17, typeof(TriangleBendingConstraint.TriangleBendingJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<TriangleBendingConstraint.SolveAggregateBufferJob>();
		}
		catch (Exception ex18)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex18, typeof(TriangleBendingConstraint.SolveAggregateBufferJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<ColliderManager.StartSimulationStepJob>();
		}
		catch (Exception ex19)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex19, typeof(ColliderManager.StartSimulationStepJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<ColliderManager.EndSimulationStepJob>();
		}
		catch (Exception ex20)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex20, typeof(ColliderManager.EndSimulationStepJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SimulationManager.StartSimulationStepJob>();
		}
		catch (Exception ex21)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex21, typeof(SimulationManager.StartSimulationStepJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SimulationManager.UpdateStepBasicPotureJob>();
		}
		catch (Exception ex22)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex22, typeof(SimulationManager.UpdateStepBasicPotureJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SimulationManager.EndSimulationStepJob>();
		}
		catch (Exception ex23)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex23, typeof(SimulationManager.EndSimulationStepJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SimulationManager.FeedbackTempPosJob>();
		}
		catch (Exception ex24)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex24, typeof(SimulationManager.FeedbackTempPosJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<SimulationManager.FeedbackTempPosJob2>();
		}
		catch (Exception ex25)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex25, typeof(SimulationManager.FeedbackTempPosJob2));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<VirtualMeshManager.CalcTransformOnlySkinningJob>();
		}
		catch (Exception ex26)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex26, typeof(VirtualMeshManager.CalcTransformOnlySkinningJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<VirtualMeshManager.CalcBaseLineNormalTangentJob>();
		}
		catch (Exception ex27)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex27, typeof(VirtualMeshManager.CalcBaseLineNormalTangentJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<VirtualMeshManager.CalcVertexNormalTangentFromTriangleJob>();
		}
		catch (Exception ex28)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex28, typeof(VirtualMeshManager.CalcVertexNormalTangentFromTriangleJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<VirtualMeshManager.WriteTransformDataJob>();
		}
		catch (Exception ex29)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex29, typeof(VirtualMeshManager.WriteTransformDataJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<VirtualMeshManager.WriteTransformLocalDataJob>();
		}
		catch (Exception ex30)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex30, typeof(VirtualMeshManager.WriteTransformLocalDataJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<InterlockUtility.AggregateJob>();
		}
		catch (Exception ex31)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex31, typeof(InterlockUtility.AggregateJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<InterlockUtility.AggregateWithVelocityJob>();
		}
		catch (Exception ex32)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex32, typeof(InterlockUtility.AggregateWithVelocityJob));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<InterlockUtility.AggregateJob2>();
		}
		catch (Exception ex33)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex33, typeof(InterlockUtility.AggregateJob2));
		}
		try
		{
			IJobParallelForDeferExtensions.EarlyJobInit<InterlockUtility.AggregateWithVelocityJob2>();
		}
		catch (Exception ex34)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex34, typeof(InterlockUtility.AggregateWithVelocityJob2));
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		__JobReflectionRegistrationOutput__2386719812.CreateJobReflectionData();
	}
}
