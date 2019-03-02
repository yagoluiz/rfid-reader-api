using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SSO.API.Certs
{
    public class Certificate
    {
        public static X509Certificate2 Get()
        {
            var assembly = typeof(Certificate).GetTypeInfo().Assembly;
            var names = assembly.GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream("SSO.API.Certs.RFIDReaderAuth.pfx"))
            {
                return new X509Certificate2(ReadStream(stream), "RFIDReader!C39NEUw");
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
