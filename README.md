# File Format Converter

Ako IDE som použil Visual Studio.

Architektúra. Riešenie pozostáva z piatich projektov (vrstiev):
1. Domain
2. Application
3. Infrastructure
4. WebUI
5. FileFormatConverterTests – pre testovanie pomocou nástrojov MSTest

Tento projekt v priečinku Common má zadefinovaný interface IConversionMethod, ktorý „kontrahuje“ metódu pre konverziu formátov a je implementovaný v príslušných triedach (napr. JsonToXML).
V Entities je definovaný model v triede Conversion s vlastnosťami FromFormat, ToFormat a ConversionMethod, a konštruktor pre inicializáciu vlastností. 
1.2.	Application
Obsahuje triedy zahŕňajúce business logiku, napr. Conversions. Táto trieda umožňuje dopĺňať metódy pre konverziu ďalších formátov do zoznamu bez nutnosti modifikovať už existujúci kód. Tento potom využíva trieda AssignConversion, ktorá požiadavke od klienta pridelí príslušnú službu pre konvertovanie formátu (napr. definovanú triedou XmlToJson).
Súčasťou tejto vrstvy sú samozrejme i rozhrania, implementované vo vrstve Infrastructre a nevyhnutné pre injektovanie závislostí do vrstvy WebUI.
1.3.	Infrastructure
Zabezpečuje streamovanie, ukladanie a načítavanie súborov, odoslanie emailu.
1.4.	WebUI
Vrstva slúži ako rozhranie pre komunikáciu, poskytovanie služieb klientovi.
Priečinok Controllers obsahuje nasledovné:
-	FileToFileController – prijatie odoslaného súboru a vrátenie výsledku ako súbor
-	OneDriveToEmailController – prijatie zdieľaného linku na cloud OneDrive a odoslanie emailu so súborom v prílohe
-	UrlToDiskController – prijatie linku na Url a odoslanie linku pre downloadovanie súboru zo servera.
-	ErrorController – súčasť ošetrenia výnimiek pomocou ExceptionHandlerExtensions
1.5.	FileFormatConverterTests – pre testovanie pomocou nástrojov MSTest

2.	Závislosti:
2.1.	Domain – žiadna
2.2.	Application – na Domain
2.3.	Infrastructure – na Application
2.4.	WebUI – na Application a v triede Program je doplnená injektáž služieb z Infrastructure.

3.	Nainštalované balíky:
3.1.	Newtonsoft.Json – nástroje pre konvertovanie
3.2.	MailKit – odoslanie emailu s prílhou
3.3.	SwashBuckle – Swagger
3.4.	MSTest

4.	Testovanie: manuálne – výhodne pomocou Swagger, automaticky – pomocou MSTest.

Pred nasadením do produkčného prostredia treba ešte viacero vylepšení, napr. vylepšiť ošetrenie výnimiek, logging, bezpečnosť projektu.
