using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;


namespace NewFrameTest
{
    public class KafkaTest
    {
        public static void testproduct()
        {

            List<string> s = new List<string>();
            s.Add((new QueryListModel() { id = 604, audioid = "scb_241", callseconds = 25, audiotime = DateTime.Parse("2020-08-26 18:01:43.000"), localurl = "/record_new/2022/10/01/20221001010911_37664136_1_1.mp3", recordmode = 3 }).ToJson());
            s.Add((new QueryListModel() { id = 603, audioid = "scb_240", callseconds = 25, audiotime = DateTime.Parse("2020-08-26 18:01:43.000"), localurl = "/record_new/2022/10/01/20221001005411_37664130_1_1.mp3", recordmode = 3 }).ToJson());
            s.Add((new QueryListModel() { id = 602, audioid = "scb_238", callseconds = 25, audiotime = DateTime.Parse("2020-08-26 18:01:43.000"), localurl = "/record_new/2022/10/01/20221001003911_37664100_1_1.mp3", recordmode = 3 }).ToJson());
            s.Add((new QueryListModel() { id = 601, audioid = "scb_237", callseconds = 12, audiotime = DateTime.Parse("2020-08-26 18:01:43.000"), localurl = "/record_new/2022/10/01/20221001002411_37664086_1_1.mp3", recordmode = 3 }).ToJson());
            s.Add((new QueryListModel() { id = 600, audioid = "scb_236", callseconds = 27, audiotime = DateTime.Parse("2020-08-26 18:01:43.000"), localurl = "/record_new/2022/10/01/20221001002411_37664083_1_1.mp3", recordmode = 3 }).ToJson());

            //consume();


            var config = new ProducerConfig { BootstrapServers = "10.168.100.16:9093,10.168.100.17:9093,10.168.100.18:9093", ClientId = "9ace5ed330ed4eab830e43ea1c5c2965" };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                    ? $"Delivered message to {r.TopicPartitionOffset}"
                    : $"Delivery Error: {r.Error.Reason}");


            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    //for (var i = 1; i <= 10; i++)
                    //{
                    //    p.Produce("2sc_asr_offline", new Message<Null, string> { Value = $"my message: {i}" }, handler);
                    //}

                    foreach (var mes in s)
                    {
                        //p.Produce("2sc_asr_offline", new Message<Null, string> { Value = mes }, handler);
                        p.Produce(new TopicPartition("2sc_asr_offline", new Partition(1)), new Message<Null, string> { Value = mes }, handler);
                        p.Produce(new TopicPartition("2sc_asr_offline", new Partition(2)), new Message<Null, string> { Value = mes }, handler);
                    }
                    p.Flush(TimeSpan.FromSeconds(10));

                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
        public static void testconsume()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test2",
                ClientId = "9ace5ed330ed4eab830e43ea1c5c2965",
                BootstrapServers = "10.168.100.16:9093,10.168.100.17:9093,10.168.100.18:9093",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AutoCommitIntervalMs = 5000,
                EnableAutoCommit = true
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("2sc_asr_offline");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = c.Consume(cts.Token);                            
                            Console.WriteLine($"分区：{consumeResult.Partition.Value} Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }

}
