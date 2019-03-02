# Create Certificate

1. Run Developer **Command Prompt Visual Studio 2017** (or your version)
2. Find your path 
3. Execute commands:
    *  makecert -sv RFIDReaderAuth.pvk -n "CN=id.rfid" -a sha256 RFIDReaderAuth.cer -r
    *  pvk2pfx -pvk RFIDReaderAuth.pvk -spc RFIDReaderAuth.cer -pfx RFIDReaderAuth.pfx -po RFIDReader!C39NEUw

* **RFIDReaderAuth.pvk** = Key
* **RFIDReaderAuth.cer** = Certificate
* **RFIDReaderAuth.pfx** = Encryption
* **RFIDReader!C39NEUw** = Password