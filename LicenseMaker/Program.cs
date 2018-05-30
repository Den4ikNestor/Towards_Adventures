using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Serialization;
using Towards_Adventures;

namespace LicenseMaker
{
    class Program
    {
        private static void GenerateNewKeyPair()
        {

            string withSecret;

            string woSecret;

            using (var rsaCsp = new RSACryptoServiceProvider())
            {
                withSecret = rsaCsp.ToXmlString(true);
                woSecret = rsaCsp.ToXmlString(false);
            }
            File.WriteAllText("private.xml", withSecret);
            File.WriteAllText("public.xml", woSecret);
        }
        static void Main(string[] args)
        {
            if (args.Any(a => a == "--generate"))
            {
                GenerateNewKeyPair();
            }

            var dto = new License()
            {
                ValidUntil = DateTime.Now.AddMinutes(1),
            };

            var fileName = string.Join("", DateTime.Now.ToString().Where(c => char.IsDigit(c)));
            new LicenceGenerator().CreateLicenseFile(dto, fileName + ".license");
        }

        class LicenceGenerator
        {
            private static string PrivateKey = @"<RSAKeyValue>
            <Modulus>ySmNvQTBpIEWCPDGkuVuMlKqaBVFAqIdTmFWwWDTZ3RQ65EPgI8CJovNe1/ixeBwqduM7Mly+lR0fX+ynMabgftMKfb2Z7NBeVHa7GB2x2nxmq2xCkRtmpHzE7Ukiiu3+c7hmKCD0LaApXNXRL7oiOynfM70Q7xKLgYMjD3MyfU=</Modulus>
            <Exponent>AQAB</Exponent>
            <P>2MKKzf6jM8jjxbKVXtfmudkGIQfDW/5ZYFu8PpoCLZTaFIwEkK4umbQAiGCa24xUtZ/yWU9myPixB0CKHypMMQ==</P>
            <Q>7ZQpDz7yOtzFZbgAnIAZM+1RIVgTHThGaIdBQYeTEItDqPOMAm1k1rUaI5bciJOwKRdVTRX2xcW1Tg+1i6PdBQ==</Q>
            <DP>nBF+xZEJSxjivw61M+O58ahMHG4tgEgjbBjA8kYLOWyKlO63vsBbNzn+hDMibN+egmNWqG1eMWe2duVGTegzkQ==</DP>
            <DQ>stv3KXYvMOdiaRjkxO2fi7tfd+Xxxe7G+wzQsP/bVPozfu+T+YPYBdikDCUYdG4TkmvxmS4u8WfN/i3PdIhx7Q==</DQ>
            <InverseQ>VhExBgCuvVDwQxCFtkTiGdDLciVJ/guOi485cIHP4wCtgyN61Hit8W94VcsluQQUkehlLlf9d486ro0r1VEHPA==</InverseQ>
            <D>HffyVUQVoy/V/Av+0Vib06RsHah5iPxk2E35EkMeC44RFh8cy57Ch8KRIL22t5sJvcxnEMmsNu4JEr9I+UE/XljFbsxDNBQjkF2Bo+VZsgdmHJuq08gFD1SP8HcCU0HoO1FdpOx2aJZ+IF82fw30zHOVIx5BN7p2VhAW5WOKyqE=</D>
            </RSAKeyValue>";
            public void CreateLicenseFile(License dto, string fileName)
            {

                var ms = new MemoryStream();

                new XmlSerializer(typeof(License)).Serialize(ms, dto);

                // Create a new CspParameters object to specify
                // a key container.

                // Create a new RSA signing key and save it in the container.
                RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
                rsaKey.FromXmlString(PrivateKey);

                // Create a new XML document.
                XmlDocument xmlDoc = new XmlDocument();

                // Load an XML file into the XmlDocument object.
                xmlDoc.PreserveWhitespace = true;
                ms.Seek(0, SeekOrigin.Begin);
                xmlDoc.Load(ms);

                // Sign the XML document.
                SignXml(xmlDoc, rsaKey);

                // Save the document.
                xmlDoc.Save(fileName);
            }

            // Sign an XML file.
            // This document cannot be verified unless the verifying
            // code has the key with which it was signed.
            public static void SignXml(XmlDocument xmlDoc, RSA Key)
            {
                // Check arguments.
                if (xmlDoc == null)
                    throw new ArgumentException("xmlDoc");
                if (Key == null)
                    throw new ArgumentException("Key");

                // Create a SignedXml object.
                SignedXml signedXml = new SignedXml(xmlDoc);

                // Add the key to the SignedXml document.
                signedXml.SigningKey = Key;

                // Create a reference to be signed.
                Reference reference = new Reference();
                reference.Uri = "";

                // Add an enveloped transformation to the reference.
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);

                // Add the reference to the SignedXml object.
                signedXml.AddReference(reference);

                // Compute the signature.
                signedXml.ComputeSignature();

                // Get the XML representation of the signature and save
                // it to an XmlElement object.
                XmlElement xmlDigitalSignature = signedXml.GetXml();

                // Append the element to the XML document.
                xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            }
        }
    }
}