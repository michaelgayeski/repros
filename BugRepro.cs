using NUnit.Framework;
using System.Threading.Tasks;
using Temporalio.Client;
using Temporalio.Worker;
using Temporalio.Workflows;

namespace TestProject1
{
	[TestFixture]
	internal class BugRepro
	{
		[Test]
		public async Task Simple_Create()
		{
			TemporalConnection connection = await TemporalConnection.ConnectAsync(new TemporalConnectionOptions("127.0.0.1:7233")).ConfigureAwait(false);
			TemporalClient client = new TemporalClient(connection, new TemporalClientOptions());

			TemporalWorkerOptions opts = new TemporalWorkerOptions() { TaskQueue = "TestQueue" };
			opts.AddWorkflow<TestWorkflow>();

			TemporalWorker worker = new TemporalWorker(client, opts);

			Assert.IsNotNull(worker);
		}
	}

	[Workflow]
	internal class TestWorkflow
	{
		[WorkflowRun]
		public Task RunAsync() => Task.CompletedTask;
	}
}