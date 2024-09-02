// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Consumer;
using System.Threading;

public class Program
{


    public static void Main(string[] args)
    {
        var config = KafkaConsumerConfig.GetConfig();
        string topic = "my-topic";
        using var consumer = new ConsumerBuilder<Ignore,string>(config).Build();

       /* consumer.Subscribe(topic);  

        Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Received Message: key: {consumeResult.Message.Key} -  {consumeResult.Message.Value}");
                    consumer.Commit(consumeResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        });
        

        Console.WriteLine("Other Job.");
        int a = 10;
        int b = 10;
        int sum = a+ b; 
        Console.WriteLine($"Sum of {a} and {b} is : {sum}");
        Console.ReadLine();*/

        using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        Task task = Task.Run(() =>
        {
            consumer.Subscribe(topic);
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Received Message: key: {consumeResult.Message.Key} -  {consumeResult.Message.Value}");
                    consumer.Commit(consumeResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        });

        Console.WriteLine("Press any key to stop consuming...");
        Console.ReadKey();
        cancellationTokenSource.Cancel();
    }
}
