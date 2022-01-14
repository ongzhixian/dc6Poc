# Certificates

There are several commonly used filename extensions for X.509 certificates.
 Unfortunately, some of these extensions are also used for other data such as private keys.

.pem – (Privacy-enhanced Electronic Mail) Base64 encoded DER certificate, enclosed between "-----BEGIN CERTIFICATE-----" and "-----END CERTIFICATE-----"
.cer, .crt, .der – usually in binary DER form, but Base64-encoded certificates are common too (see .pem above)
.p7b, .p7c – PKCS#7 SignedData structure without data, just certificate(s) or CRL(s)
.p12 – PKCS#12, may contain certificate(s) (public) and private keys (password protected)
.pfx – PFX, predecessor of PKCS#12 (usually contains data in PKCS#12 format, e.g., with PFX files generated in IIS)


# .NET classes

CertificateRequest      Represents an abstraction over the PKCS#10 CertificationRequestInfo and the X.509 TbsCertificate.
 X509Certificate2 	    Represents an X.509 certificate.
 X509Store 	            Represents an X.509 store, which is a physical store where certificates are persisted and managed. This class cannot be inherited.


# CLI

certutil


# X.509 Attributes (aka DN -- Distinguished Names )
CN: CommonName  (the fully qualified domain name when used for SSL) : User1 / test.contoso.com
OU: OrganizationalUnit                                              : IT / Engineering
O: Organization                                                     : Contoso Pte
L: Locality                                                         : Singapore / Regina
S: StateOrProvinceName (full name)                                  : Singapore / Saskatchewan
C: CountryName (2 letter code)                                      : SG        / CA                         

CN=Sample Cert, OU=R&D, O=Company Ltd., L=Dublin 4, S=Dublin, C=IE


# New-SelfSignedCertificate

View store
`ls cert:\LocalMachine\My`

Create SSL certificate and add it as personal certificate on LocalMachine store using defaults:
`New-SelfSignedCertificate -DnsName test.contoso.com -CertStoreLocation cert:\LocalMachine\My`


Make SSL cert with Subject Alternative Name (SAN)
`New-SelfSignedCertificate -DnsName adfs1.contoso.com,web_gw.contoso.com,enterprise_reg.contoso.com -CertStoreLocation cert:\LocalMachine\My`


By default, a self-signed certificate is generated with the following settings:
Cryptographic algorithm         : RSA;
Key length                      : 2048 bit;
Acceptable key usage            : Client Authentication and Server Authentication;
The certificate can be used for : Digital Signature, Key Encipherment;
Certificate validity period     : 1 year.

To view all properties (default shows only: Thumbprint, Subject, EnhancedKeyUsageList)
`ls | select *` 

To make certificate valid for more than 1 year, use `-NotAfter`
```ps1
$todaydt = Get-Date
$3years = $todaydt.AddYears(3)
New-SelfSignedCertificate -DnsName test.contoso.com -NotAfter $3years -CertStoreLocation cert:\LocalMachine\My
```

For certificate chain, make a CA first:

```ps1
# Make CA
$rootCert = New-SelfSignedCertificate -Subject 'CN=TestRootCA,O=TestRootCA,OU=TestRootCA' -KeyExportPolicy Exportable  -KeyUsage CertSign,CRLSign,DigitalSignature -KeyLength 2048 -KeyUsageProperty All -KeyAlgorithm 'RSA'  -HashAlgorithm 'SHA256'  -Provider 'Microsoft Enhanced RSA and AES Cryptographic Provider'

# Sign cert with CA cert
New-SelfSignedCertificate -CertStoreLocation cert:\LocalMachine\My -DnsName "test2.contoso.com" -Signer $rootCert -KeyUsage KeyEncipherment,DigitalSignature
```

```ps1 Export cert with password
$CertPassword = ConvertTo-SecureString -String “YourPassword” -Force –AsPlainText
Export-PfxCertificate -Cert cert:\LocalMachine\My\2779C7928D055B21AAA0Cfe2F6BE1A5C2CA83B30 -FilePath C:\test.pfx -Password $CertPassword
```

```Export public cert
Export-Certificate -Cert Cert:\LocalMachine\My\2779C7928D055B21AAA0Cfe2F6BE1A5C2CA83B30 -FilePath C:\tstcert.cer

# Add cert to store after export
$certFile = Export-Certificate -Cert $SelfSignCert -FilePath C:\ps\export-certname.cer
Import-Certificate -CertStoreLocation Cert:\LocalMachine\AuthRoot -FilePath $certFile.FullName

```

Cert for code-signing
$cert = New-SelfSignedCertificate -Subject "My Code Signing Certificate” -Type CodeSigningCert -CertStoreLocation cert:\LocalMachine\My

Sign PowerShell script
Set-AuthenticodeSignature -FilePath C:\PS\my_posh_script.ps1 -Certificate $cert

 # Reference

 https://www.ssl.com/guide/pem-der-crt-and-cer-x-509-encodings-and-conversions/

 https://github.com/microsoftarchive/clrsecurity/blob/master/Security.Cryptography/src/X509Certificates/X509Certificate2ExtensionMethods.cs

 https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/certutil

 https://docs.microsoft.com/en-us/powershell/module/pki/new-selfsignedcertificate?view=windowsserver2022-ps

 http://woshub.com/how-to-create-self-signed-certificate-with-powershell/


https://www.ibm.com/docs/en/ibm-mq/9.2?topic=certificates-distinguished-names


https://stackoverflow.com/questions/42786986/how-to-create-a-valid-self-signed-x509certificate2-programmatically-not-loadin


https://access.redhat.com/documentation/en-us/red_hat_certificate_system/9/html/administration_guide/standard_x.509_v3_certificate_extensions

https://oidref.com/1.3.6.1.1.15
http://oid-info.com/get/1.3.6.1.5.5.7.3.1
