ServiceTasks
================

Offers the ability to schedule commands to be sent from a running NServiceBus endpoint.


Example Configuration
================

	<configSections>
	  <section name="TaskSchedule" type="ServiceTasks.Schedule.Storage.TaskScheduleConfiguration, ServiceTasks"/>
	</configSections>

	<TaskSchedule>
	  <Tasks>
		<add name="Command Example" interval="10"
			 messageType="Messages.Commands.MyCommand, Messages.Commands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>
	  </Tasks>
	</TaskSchedule>


Sample NServiceBus Endpoint Config 
================

ServiceTasks needs this in order to know where your schedule is stored. You can use your supported builder of choice.

	[EndpointName("myendpoint")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
	{
		public void Init()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<ConfigStorage>().As<ITaskScheduleStorage>();

			NServiceBus.Configure.With()
				.AutofacBuilder(builder.Build());
		}
	}