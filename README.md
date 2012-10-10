ServiceTasks
================

Offers the ability to schedule commands to be sent, using the NServiceBus Task framework


Example Configuration
================
<pre><code>
<configSections>
  <section name=\"TaskSchedule\" type=\"ServiceTasks.Schedule.Storage.TaskScheduleConfiguration, ServiceTasks\"/>
</configSections>

<TaskSchedule>
  <Tasks>
    <add name=\"Command Example\" interval=\"10\"
         messageType=\"Messages.Commands.MyCommand, Messages.Commands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"/>
  </Tasks>
</TaskSchedule>
</code></pre>

If using with NServiceBus: Sample NServiceBus Endpoint Config (ServiceTasks needs this in order to know where your schedule is stored. You can use your builder of choice)
================
<pre><code>
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
</code></pre>