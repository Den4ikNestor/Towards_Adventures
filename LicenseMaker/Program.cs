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
                ValidUntil = DateTime.Now.AddMinutes(100),
            };

            var fileName = string.Join("", DateTime.Now.ToString().Where(c => char.IsDigit(c)));
            new LicenceGenerator().CreateLicenseFile(dto, fileName + ".license");
        }

        class LicenceGenerator
        {
            private static string PrivateKey = @"<RSAKeyValue>
            <Modulus>jvtN7HbAj0v5ZyC81opn39RuiPlW0tWDqEK+UT9mHvh6zR/79nxJNPFy5zc6H+pTLZTqHO3kwEcJkJ45PJRl8fJklneL1ga8ZHFvMYOgsoz1lVrcNsxhbcsWC8F3vltN1ezYVjHKau1xExsYol9CadC0wF4VHy84E00XUSUDmFs=</Modulus>
            <Exponent>AQAB</Exponent>
            <P>ulR2AePIkO2xwoT/9tKlapM3Ao/YQQhAHeCI8ZyLjDo/8HMyBU+59IzWT6ios+YQUyVRIlf+0xt2YwANWu+Jsw==</P>
            <Q>xHGJyvbsivnLk/EdUDxtZXvKDZUY2WaymkHb/ozekFpSoCCZVhylZzpLBnkLCn5NIIRmbTQOH91ntVYnxpuSuQ==</Q>
            <DP>bZRqaiYZuBHx7qHlHrU3DvxQ57LMzUIa4vc/0kfsUaWYIMK+ch03ETkaeHKJ9HKiuyNBGd+CP4jxELvhHs+svQ==</DP>
            <DQ>gMa1P7pTIl/SVq9POhQC2u4lbHX7DjlGh9z4rIwIMrUjSRlVq5+nxl3uZNXgqQZW5SQmSRxAzh7EJ5nNBWNi8Q==</DQ>
            <InverseQ>q8elSbZPpRDd2aiYXX87VxM75WIvfv5EgyKK12L285d6jCJcwGa2isBlOdoM6R+xqQ/QnHuFHGiGTUr1FtC2kQ==</InverseQ>
            <D>I9zYoLTwe4C836cQdmdkwnmP9/9CTcOMMEn9Xneeb6o3cvfQxPRLkGH3RhNOGu81SbuONQ9eTvQCbCmjhZRbNUJqxrOtleoQ2J6lVvQSOpSw6QUMjwihH0mTn4XkNbE92q1r6770+8XfcGrM3PGwgD1eJSlYrCA8YmYHAbXFJKE=</D>
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