# Projekt Avancerad .NET
Applikationen är utvecklad som en ASP.NET Core Web API med en RESTful arkitektur för att hantera kunder och deras möten. Den består av tre huvudkontroller: CustomerController, CompanyController och AppointmentController. Var och en av dessa kontroller hanterar olika aspekter av applikationens funktionalitet för att skapa, läsa, uppdatera och ta bort kunder, företag, och möten. För att separera affärslogik och databasåtkomst används Repository-pattern, och för att överföra data mellan lager används DTO (Data Transfer Objects). Applikationen inkluderar också en autentiseringsmekanism för att säkerställa säker åtkomst till API:et.
Tekniska Val
ASP.NET Core Web API:

Fördelar: Ger en kraftfull och skalbar plattform för att skapa webb-API:er med inbyggda funktioner för hantering av HTTP-verb och routing.
Nackdelar: Kan vara överdimensionerat för mindre projekt eller projekt som inte kräver en fullständig webbservermiljö.
Dependency Injection (DI):

Fördelar: Främjar lös koppling mellan komponenter och enklare testning genom att underlätta utbyte av komponenter.
Nackdelar: Kräver lite inlärning för att förstå hur det fungerar och hur man konfigurerar det korrekt.
Entity Framework Core:

Fördelar: Ett kraftfullt ORM-verktyg som förenklar databasåtkomst och hantering av databasrelationer.
Nackdelar: Prestanda kan vara en oro i vissa scenarier, och det kan finnas en inlärningskurva för att lära sig att använda det effektivt.
Automapper:

Fördelar: Förenklar överföringen av data mellan olika datamodeller och DTO:er (Data Transfer Objects).
Nackdelar: Kan introducera overhead och komplexitet i mindre projekt.
DTO (Data Transfer Objects):

Fördelar: Möjliggör separation av datalager och presentationsskikt samt förenklar överföringen av data mellan olika delar av applikationen.
Nackdelar: Kan kräva extra kod för att hantera konvertering mellan DTO:er och datamodeller.
Repository-mönstret:

Fördelar: Separerar affärslogik och databasåtkomst för bättre underhållbarhet och testbarhet.
Nackdelar: Kan introducera overhead och ökad komplexitet i mindre projekt.

Underhåll och Vidareutveckling
Koden är välstrukturerad och följer bästa praxis för att underlätta underhåll och vidareutveckling. Användningen av Dependency Injection, Repository-mönstret och separation av ansvar mellan kontroller och tjänster gör det enkelt att lägga till nya funktioner och göra ändringar utan att störa befintlig funktionalitet.

Säkerhet och Tillförlitlighet
Det finns utrymme för förbättringar när det gäller säkerhetsaspekter som autentisering och auktorisering. Tillförlitligheten kan säkerställas genom robust hantering av fel och undantag för att hantera potentiella problem i produktionsmiljön.

Flexibilitet och Anpassningsbarhet
Applikationens arkitektur är ganska flexibel och anpassningsbar, särskilt med användning av Dependency Injection och Repository-mönstret för att underlätta utbyte av komponenter och separation av ansvar. Det finns vissa begränsningar som kan uppstå beroende på specifika tekniska val, såsom databasåtkomstteknik och val av externa bibliotek.
