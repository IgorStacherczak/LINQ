using LinqConsoleLab.PL.Data;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        var exerciseOne = from dane in DaneUczelni.Studenci where dane.Miasto == "Warsaw"
            select $"{dane.NumerIndeksu} | {dane.Imie} {dane.Nazwisko} | {dane.Miasto}";
        return exerciseOne;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        var exerciseTwo = from dane in DaneUczelni.Studenci select dane.Email;
        
        return exerciseTwo;
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        var exerciseThree = from dane in DaneUczelni.Studenci
            orderby dane.Nazwisko,dane.Imie 
            select $"{dane.NumerIndeksu} | {dane.Imie} {dane.Nazwisko}";
        
        return exerciseThree;
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        var exerciseFour = DaneUczelni.Przedmioty.FirstOrDefault(p => p.Kategoria == "Analytics");

        if (exerciseFour == null)
        {
            return new List<string> { "Nie znaleziono przedmiotu z kategorii Analytics." };
        }

        return new List<string>
        {
            $"{exerciseFour.Nazwa} | {exerciseFour.DataStartu:yyyy-MM-dd}"
        };
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        var exerciseFive = DaneUczelni.Zapisy.Any(z => !z.CzyAktywny);

        return new List<string>
        {
            $"Czy istnieje nieaktywne zapisanie: {exerciseFive}"
        };
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        var exerciseSix = DaneUczelni.Prowadzacy.All(p => !string.IsNullOrWhiteSpace(p.Katedra));

        return new List<string>
        {
            $"Czy wszyscy prowadzący mają przypisaną katedrę: {exerciseSix}"
        };
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        var exerciseSeven = DaneUczelni.Zapisy.Count(z => z.CzyAktywny);

        return new List<string>
        {
            $"Liczba aktywnych zapisów: {exerciseSeven}"
        };
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        var exerciseEight = DaneUczelni.Studenci.Select(s => s.Miasto).Distinct().OrderBy(m => m);

        return exerciseEight;
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        var exerciseNine = DaneUczelni.Zapisy.OrderByDescending(z => z.DataZapisu).Take(3).Select(z => $"{z.DataZapisu:yyyy-MM-dd} | StudentId: {z.StudentId} | PrzedmiotId: {z.PrzedmiotId}");

        return exerciseNine;
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        var exerciseTen = DaneUczelni.Przedmioty.OrderBy(p => p.Nazwa).Skip(2).Take(2).Select(p => $"{p.Nazwa} | {p.Kategoria}");

        return exerciseTen;
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        var exerciseEleven = from student in DaneUczelni.Studenci
            join zapis in DaneUczelni.Zapisy on student.Id equals zapis.StudentId
            select $"{student.Imie} {student.Nazwisko} | {zapis.DataZapisu:yyyy-MM-dd}";

        return exerciseEleven;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        var exerciseTwelve = from zapis in DaneUczelni.Zapisy
            from student in DaneUczelni.Studenci.Where(s => s.Id == zapis.StudentId)
            from przedmiot in DaneUczelni.Przedmioty.Where(p => p.Id == zapis.PrzedmiotId)
            select $"{student.Imie} {student.Nazwisko} | {przedmiot.Nazwa}";

        return exerciseTwelve;
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        var exerciseThirteen = from zapis in DaneUczelni.Zapisy
            join przedmiot in DaneUczelni.Przedmioty on zapis.PrzedmiotId equals przedmiot.Id
            group zapis by przedmiot.Nazwa into grupa orderby grupa.Key
            select $"{grupa.Key} | Liczba zapisów: {grupa.Count()}";

        return exerciseThirteen;
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        var exerciseFourteen = from zapis in DaneUczelni.Zapisy
            where zapis.OcenaKoncowa != null
            join przedmiot in DaneUczelni.Przedmioty on zapis.PrzedmiotId equals przedmiot.Id
            group zapis by przedmiot.Nazwa into grupa orderby grupa.Key
            select $"{grupa.Key} | Średnia ocena: {grupa.Average(z => z.OcenaKoncowa):0.00}";

        return exerciseFourteen;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        var exerciseFifteen = from prowadzacy in DaneUczelni.Prowadzacy
            join przedmiot in DaneUczelni.Przedmioty on prowadzacy.Id equals przedmiot.ProwadzacyId into przedmiotyProwadzacego orderby prowadzacy.Nazwisko, prowadzacy.Imie
            select $"{prowadzacy.Imie} {prowadzacy.Nazwisko} | Liczba przedmiotów: {przedmiotyProwadzacego.Count()}";
        
        return exerciseFifteen;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        var exerciseSixteen = from student in DaneUczelni.Studenci
            join zapis in DaneUczelni.Zapisy on student.Id equals zapis.StudentId
            where zapis.OcenaKoncowa != null
            group zapis by $"{student.Imie} {student.Nazwisko}" into grupa orderby grupa.Key
            select $"{grupa.Key} | Najwyższa ocena: {grupa.Max(z => z.OcenaKoncowa):0.0}";

        return exerciseSixteen;
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        var wynik =
            from student in DaneUczelni.Studenci
            join zapis in DaneUczelni.Zapisy on student.Id equals zapis.StudentId
            where zapis.CzyAktywny group zapis by $"{student.Imie} {student.Nazwisko}" into grupa
            where grupa.Count() > 1 orderby grupa.Key
            select $"{grupa.Key} | Liczba aktywnych przedmiotów: {grupa.Count()}";

        return wynik;
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        var wynik = from przedmiot in DaneUczelni.Przedmioty
            where przedmiot.DataStartu.Month == 4 && przedmiot.DataStartu.Year == 2026
            join zapis in DaneUczelni.Zapisy on przedmiot.Id equals zapis.PrzedmiotId
            group zapis by przedmiot.Nazwa into grupa
            where grupa.All(z => z.OcenaKoncowa == null) orderby grupa.Key
            select grupa.Key;

        return wynik;
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        var wynik = from prowadzacy in DaneUczelni.Prowadzacy
            join przedmiot in DaneUczelni.Przedmioty on prowadzacy.Id equals przedmiot.ProwadzacyId into przedmiotyProwadzacego
            let oceny = przedmiotyProwadzacego.Join( DaneUczelni.Zapisy.Where(z => z.OcenaKoncowa != null), p => p.Id, z => z.PrzedmiotId, (p, z) => z.OcenaKoncowa!.Value)
            orderby prowadzacy.Nazwisko, prowadzacy.Imie
            select oceny.Any() ? $"{prowadzacy.Imie} {prowadzacy.Nazwisko} | Średnia ocen: {oceny.Average():0.00}" : $"{prowadzacy.Imie} {prowadzacy.Nazwisko} | Średnia ocen: brak ocen";

        return wynik;
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        var wynik = from student in DaneUczelni.Studenci
            join zapis in DaneUczelni.Zapisy on student.Id equals zapis.StudentId where zapis.CzyAktywny
            group zapis by student.Miasto into grupa
            orderby grupa.Count() descending, grupa.Key
            select $"{grupa.Key} | Liczba aktywnych zapisów: {grupa.Count()}";

        return wynik;
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
