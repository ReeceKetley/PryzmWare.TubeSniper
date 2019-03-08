using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
    public class ProxyPortationMapper : IProxyPortationMapper
    {
        public string Map(IEnumerable<HttpProxy> proxies)
        {
            if (proxies == null)
            {
                throw new ArgumentNullException(nameof(proxies));
            }

            var sb = new StringBuilder();
            var config = new CsvHelper.Configuration.Configuration();
            config.ShouldQuote = (s, context) => false;
            using (var writer = new CsvWriter(new StringWriter(sb), config))
            {
                writer.WriteField("address");
                writer.WriteField("username");
                writer.WriteField("password");
                writer.NextRecord();

                foreach (var proxy in proxies)
                {
                    writer.WriteField(proxy.Address.Host + ":" + proxy.Address.Port, false);
                    writer.WriteField(proxy.Credentials?.Username);
                    writer.WriteField(proxy.Credentials?.Password);
                    writer.NextRecord();
                }
            }

            return sb.ToString();
        }

        public List<HttpProxy> Map(string data)
        {
            data = Sanitize(data);

            var proxies = new List<HttpProxy>();

            var config = new CsvHelper.Configuration.Configuration();
            config.HasHeaderRecord = true;
            config.MissingFieldFound = null;
            config.BadDataFound = null;
            config.IgnoreBlankLines = true;
            using (var tr = new StringReader(data))
            {
                var reader = new CsvReader(tr, config);
                reader.Read();
                reader.ReadHeader();

                while (reader.Read())
                {
                    if (!reader.TryGetField<string>("host", out var host))
                    {
                        continue;
                    }

                    if (!reader.TryGetField<int>("port", out var port))
                    {
						continue;
                    }

                    HttpProxyAddress address = null;
                    if (!HttpProxyAddress.TryCreate(host + ":" + port, out address))
                    {
						continue;
                    }

                    HttpProxyCredentials credentials = null;
                    if (reader.TryGetField<string>("username", out var username) && reader.TryGetField<string>("password", out var password))
                    {
                        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !HttpProxyCredentials.TryCreate(username, password, out credentials))
                        {
							continue;
                        }
                    }

                    proxies.Add(new HttpProxy(address, credentials));
                }
            }

            return proxies;
        }

        private string Sanitize(string data)
        {
            data = ConvertDelimiter(data, "\t", ",");
            data = ConvertDelimiter(data, ":", ",");

            data = "host,port,username,password\n" + data;

            return data;
        }

        private string ConvertDelimiter(string data, string target, string replacement)
        {
            var sb = new StringBuilder();

            var config = new CsvHelper.Configuration.Configuration();
            config.Delimiter = replacement;
            using (var writer = new CsvWriter(new StringWriter(sb), config))
            {
                var readerConfig = new CsvHelper.Configuration.Configuration();
                readerConfig.HasHeaderRecord = true;
                readerConfig.MissingFieldFound = null;
                readerConfig.BadDataFound = null;
                readerConfig.IgnoreBlankLines = true;
                readerConfig.Delimiter = target;

                using (var tr = new StringReader(data))
                {
                    var reader = new CsvParser(tr, readerConfig);
                    while (true)
                    {
                        var row = reader.Read();
                        if (row == null)
                        {
                            break;
                        }

                        foreach (var col in row)
                        {
                            writer.WriteField(col, false);
                        }
                        writer.NextRecord();
                    }
                }
            }

            return sb.ToString();
        }

        private string SanitizeHeaders(string data)
        {
            var sb = new StringBuilder();

            var config = new CsvHelper.Configuration.Configuration();
            config.ShouldQuote = (s, context) => false;
            using (var writer = new CsvWriter(new StringWriter(sb), config))
            {
                var readerConfig = new CsvHelper.Configuration.Configuration();
                readerConfig.HasHeaderRecord = true;
                readerConfig.MissingFieldFound = null;
                readerConfig.BadDataFound = null;
                readerConfig.IgnoreBlankLines = true;
                using (var tr = new StringReader(data))
                {
                    var reader = new CsvParser(tr, readerConfig);
                    while (true)
                    {
                        var row = reader.Read();
                        if (row == null)
                        {
                            break;
                        }

                        foreach (var col in row)
                        {
                            writer.WriteField(col, false);
                        }

                        writer.NextRecord();
                    }
                }
            }

            return sb.ToString();
        }
    }
}
