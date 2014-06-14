﻿using System;
using System.IO;
using GSF.SortedTreeStore.Services.Net;
using GSF.SortedTreeStore.Net;
using NUnit.Framework;
using openHistorian;
using openHistorian.Collections;
using GSF.SortedTreeStore.Tree;
using GSF.SortedTreeStore.Services.Reader;

namespace SampleCode.openHistorian.Server.dll
{
    [TestFixture]
    public class Sample3
    {
        [Test]
        public void CreateScadaDatabase()
        {
            Array.ForEach(Directory.GetFiles(@"c:\temp\Scada\", "*.d2", SearchOption.AllDirectories), File.Delete);


            var key = new HistorianKey();
            var value = new HistorianValue();

            using (var server = new HistorianServer(@"c:\temp\Scada\", 12345))
            {
                NetworkClientConfig clientConfig = new NetworkClientConfig();
                clientConfig.NetworkPort = 12345;
                clientConfig.ServerNameOrIp = "127.0.0.1";

                using (var client = new HistorianClient(clientConfig))
                using (var database = client.GetDatabase<HistorianKey, HistorianValue>(string.Empty))
                {
                    for (ulong x = 0; x < 1000; x++)
                    {
                        key.Timestamp = x;
                        database.Write(key, value);
                    }

                    database.HardCommit();
                    System.Threading.Thread.Sleep(1200);
                }


            }
        }

        [Test]
        public void TestReadData()
        {
            var key = new HistorianKey();
            var value = new HistorianValue();

            using (HistorianServer server = new HistorianServer(@"c:\temp\Scada\", 12345))
            {
                NetworkClientConfig clientConfig = new NetworkClientConfig();
                clientConfig.NetworkPort = 12345;
                clientConfig.ServerNameOrIp = "127.0.0.1";

                using (var client = new HistorianClient(clientConfig))
                using (var database = client.GetDatabase<HistorianKey, HistorianValue>(string.Empty))
                {
                    var stream = database.Read(0, 1000);
                    while (stream.Read(key, value))
                        Console.WriteLine(key.Timestamp);
                }
            }
        }
    }
}