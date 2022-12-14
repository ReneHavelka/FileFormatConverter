# File Format Converter

Ako IDE som použil Visual Studio.

## Architektúra.
Riešenie pozostáva z piatich projektov (vrstiev):
1. Domain
2. Application
3. Infrastructure
4. WebUI
5. FileFormatConverterTests – pre testovanie pomocou nástrojov MSTest

Snahou bolo dosiahnuť Clean Architecture design a čo najviac uplatniť princípy SOLID.
V väčšine tried, kvôli prehľadnosti, je vhodný popis vysvetľujúci účel triedy, prípadne metódy.

## Nainštalované balíky:
1. Newtonsoft.Json a ProtoBuf-net – nástroje pre konvertovanie
2. MailKit – odoslanie emailu s prílohou
3. SwashBuckle – Swagger
4. MSTest

## Testovanie: 
* manuálne – výhodne pomocou Swagger
* automaticky – pomocou MSTest.

Testovanie je len čiastočné.
Celé riešenie by si vyžadovalo veľa refactoring-u, resp. najvhodnejšie by bolo zmeniť prístup k riešeniu na TDD.