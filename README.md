# MovieRatingApp

## Popis projektu

**MovieRatingApp** je aplikace pro hodnocení filmů vytvořená jako součást mého univerzitního studia. Uživatelé mohou prohlížet filmy, vyhledávat je, přidávat nové filmy, vytvářet recenze a ukládat hodnocení.

## Klíčové funkce

- **Vyhledávání filmů**: Vyhledávání podle názvu, režiséra nebo žánru.
- **Správa filmů**: Přidávání, úprava a mazání filmů.
- **Přidávání recenzí**: Možnost přidávat hodnocení a recenze.
- **Správa režisérů a žánrů**: Aktualizace databáze režisérů a žánrů.
- **Průměrné hodnocení**: Zobrazení průměrného hodnocení filmu.

## Použité technologie

- **Programovací jazyk**: C#
- **Framework**: .NET (Windows Forms)
- **Databáze**: SQLite
- **Formát dat**: JSON

## Struktura projektu

- **Data**: Třídy a metody pro komunikaci s databází.
- **Models**: Datové modely (Movie, Director, Genre, UserReview).
- **Utilities**: Pomocné metody pro práci se soubory (FileDataManager).
- **UI**: Komponenty Windows Forms pro uživatelské rozhraní (MainForm, MovieForm).

## Instalace a spuštění

1. **Naklonujte repozitář**: Stáhněte projekt pomocí příkazu:
   ```sh
   git clone <URL>
   ```
2. **Otevřete projekt**: Otevřete projekt v IDE, jako je Visual Studio.
3. **Sestavte projekt**: Klikněte na "Build" nebo použijte zkratku Ctrl + Shift + B.
4. **Spusťte aplikaci**: Po sestavení spusťte aplikaci pomocí klávesy F5.

## Použití aplikace

- **Filmy**: Přidávejte, upravujte a mažte filmy pomocí příslušných tlačítek.
- **Recenze**: Přidávejte hodnocení k filmům a pište recenze.
- **Vyhledávání**: Vyhledávejte filmy podle názvu, režiséra nebo žánru pro snadný přístup k informacím.

## Ukázkové soubory

Projekt obsahuje ukázková data (`testMovies.json`) pro rychlé otestování aplikace. Tato data můžete načíst pro seznámení se s funkcionalitou aplikace.
