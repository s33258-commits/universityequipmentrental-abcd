# University Equipment Rental

Aplikacja konsolowa w C# do obsługi uczelnianej wypożyczalni sprzętu.

## Opis projektu

System umożliwia:
- dodawanie użytkowników,
- dodawanie sprzętu różnych typów,
- wypożyczanie sprzętu użytkownikom,
- zwrot sprzętu,
- naliczanie kar za opóźnienie,
- oznaczanie sprzętu jako niedostępnego,
- wyświetlanie aktywnych i przeterminowanych wypożyczeń,
- generowanie krótkiego raportu podsumowującego.

## Typy obiektów w systemie

### Sprzęt
W systemie występuje klasa bazowa `Equipment` oraz trzy typy sprzętu:
- `Laptop`
- `Projector`
- `Camera`

Każdy sprzęt posiada:
- unikalny identyfikator,
- nazwę,
- markę,
- status dostępności.

Każdy typ sprzętu posiada także własne pola specyficzne.

### Użytkownicy
W systemie występuje klasa bazowa `User` oraz dwa typy użytkowników:
- `Student`
- `Employee`

Każdy użytkownik posiada:
- identyfikator,
- imię,
- nazwisko,
- typ użytkownika.

### Wypożyczenia
Klasa `Loan` przechowuje informacje o:
- użytkowniku,
- sprzęcie,
- dacie wypożyczenia,
- terminie zwrotu,
- faktycznej dacie zwrotu,
- karze za opóźnienie.

## Struktura projektu

- `Models` – klasy domenowe, np. `Equipment`, `Loan`, `User`
- `Enums` – typy wyliczeniowe, np. `EquipmentStatus`
- `Services` – logika biznesowa, np. wypożyczanie, zwroty, raporty, naliczanie kar
- `Program.cs` – scenariusz demonstracyjny działania systemu

## Decyzje projektowe

Projekt został podzielony na model domenowy, serwisy oraz warstwę uruchomieniową.

- Klasy z folderu `Models` odpowiadają za przechowywanie danych domenowych.
- Klasy z folderu `Services` odpowiadają za logikę biznesową.
- `Program.cs` pełni rolę prostego interfejsu konsolowego i miejsca demonstracji.

Taki podział pozwala uniknąć umieszczania całej logiki w jednym miejscu i poprawia czytelność kodu.

## Cohesion, coupling i odpowiedzialności klas

### Cohesion
Każda klasa ma jedną główną odpowiedzialność:
- `PenaltyCalculator` odpowiada tylko za obliczanie kar,
- `ReportService` odpowiada za raporty,
- `RentalService` odpowiada za operacje wypożyczania i zwrotów.

### Coupling
Zależności między klasami zostały ograniczone:
- modele domenowe nie zawierają logiki raportowania,
- naliczanie kar zostało wydzielone do osobnej klasy,
- warstwa konsolowa korzysta z serwisów zamiast samodzielnie implementować reguły biznesowe.

### Odpowiedzialności klas
Podział klas został dobrany tak, aby każda klasa miała jasno określoną rolę i nie była przypadkowym zbiorem metod.

## Reguły biznesowe

- student może mieć maksymalnie 2 aktywne wypożyczenia,
- pracownik może mieć maksymalnie 5 aktywnych wypożyczeń,
- sprzętu niedostępnego nie można wypożyczyć,
- sprzętu już wypożyczonego nie można wypożyczyć ponownie,
- opóźniony zwrot powoduje naliczenie kary.

## Uruchomienie projektu

1. Otwórz projekt w Visual Studio lub innym środowisku obsługującym C#.
2. Upewnij się, że masz zainstalowany .NET SDK.
3. Uruchom aplikację poleceniem:

```bash
dotnet run
