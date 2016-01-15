using System;
using Microsoft.ServiceBus;

namespace CreateEventHub
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Connection String as can be found on the azure portal https://manage.windowsazure.com/microsoft.onmicrosoft.com#Workspaces/ServiceBusExtension/namespaces
                // 
                // The needed Shared access policy is Manage, this can either be the RootManageSharedAccessKey or one create with Manage priviledges
                // "Endpoint = sb://NAMESPACE.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=THISISMYKEY";
                var policyName = "RootManageSharedAccessKey";
                var policySharedAccessKey = "THISISMYKEY";
                var serviceBusNamespace = "NAMESPACE";
                var credentials = TokenProvider.CreateSharedAccessSignatureTokenProvider(policyName, policySharedAccessKey);

                // access the namespace
                var serviceBusUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceBusNamespace, string.Empty);
                var manager = new NamespaceManager(serviceBusUri, credentials);

                // Create the eventhub if needed
                var description = manager.CreateEventHubIfNotExists("MyHub");
                Console.WriteLine("AT: " + description.CreatedAt + " EventHub:" + description.Path + " Partitions: " + description.PartitionIds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}
