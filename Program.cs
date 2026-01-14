
using System.Buffers;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

class Programm
{
    //List Oseby
    static List<Oseba> osebe = new List<Oseba>();
    static void Main()
    {
      Izbira();
    }
    static void Izbira()
    {
        Console.Write("Kaj Želite Storiti:\n- [1] Nova Oseba\n- [2] List Oseb\n- [3] Iskanje Osebe\n- [4] Izbris Osebe\n");
        int.TryParse(Console.ReadLine(), out int Funkcija);
        switch (Funkcija)
        {
            case 1:
                NovaOseba();
                break;
            case 2:
                ListOseb();
                break;
            case 3:
            IskanjeOsebe();
                break;
            case 4:
                IzbrisiUporabnika();
                break;

        }
    }
    static void OIzbiri()
    {
        Console.Write("Želite storiti še kaj?\n 1 - [Da], 2 - [Ne]\n");
        int.TryParse(Console.ReadLine(), out int Funkcija);
        if (Funkcija == 1)
        {
            Izbira();
        }
    }
    static void NovaOseba()
    {
            // Ask for user input
        Console.Write("Vnesi ime: ");
        string Ime = Console.ReadLine()!;

        Console.Write("Vnesi priimek: ");
        string Priimek = Console.ReadLine()!;
        
        Console.Write("Vnesi starost: ");
        int Starost;
        string TStarost = Console.ReadLine()!;
        while (int.TryParse(TStarost, out Starost) == false)
        {
            Console.Write("Vpisite starost ponovno: ");
            TStarost = Console.ReadLine()!;
        };

        //Output
        Console.Write($"Priimek: {Priimek}, Ime: {Ime}, Starost: {Starost}\n");

        //Doda Novo osebo
        Oseba NovaOseba = new Oseba
        {
            Priimek = Priimek,
            Ime = Ime,
            Starost = Starost,
        };
        osebe.Add(NovaOseba);

        //Da se veckrat izbEre
        OIzbiri();

        
    }
    static void ListOseb()
    {
        osebe.Sort();
        foreach (var o in osebe)
        {
            Console.Write($"Priimek: {o.Priimek}, Ime: {o.Ime}, S1tarost: {o.Starost}\n");
        }
    
        OIzbiri();
    }
    static void IzbrisiUporabnika()
    {
        // zbere podatke
        Console.Write("Priimek Uporabnika:");
        string IzPriimek = Console.ReadLine()!;

        Console.Write("Ime Uporabnika:");
        string IZIme = Console.ReadLine()!;

        Console.Write("Starost Uporabnika:");
        string TIzStarost = Console.ReadLine()!;
        int IzStarost;
        while (int.TryParse(TIzStarost, out IzStarost) == false)
        {
            Console.Write("Vpisite starost ponovno: ");
            TIzStarost = Console.ReadLine()!;
        };

        //pogleda ce j noter v seznamu
        Oseba? OsebaZaIzbris = osebe.FirstOrDefault(o=> 
        o.Priimek == IzPriimek &&
        o.Ime == IZIme &&
        o.Starost == IzStarost
        );

        //Odgovori
        if(OsebaZaIzbris != null)
        {
            osebe.Remove(OsebaZaIzbris);
            Console.Write($"Oseba {OsebaZaIzbris} je bila odstranjena");
        }
        else
        {
            Console.Write("Oseba ni bila Najdena");
        }
        OIzbiri();
    }
        static void IskanjeOsebe()
    {
        string? ZPriimek = null;
        string? ZIme = null;
        int? ZStarost = null;

        bool konec = false;

        Console.WriteLine("Kaj veste o uporabniku?");
        Console.WriteLine("[1] Priimek  [2] Ime  [3] Starost");

        while (!konec)
        {
            int izbira;
            while (!int.TryParse(Console.ReadLine(), out izbira))
            {
                Console.Write("Vpisite ponovno: ");
            }

            switch (izbira)
            {
                case 1:
                    Console.Write("Vnesite priimek: ");
                    ZPriimek = Console.ReadLine();
                    break;

                case 2:
                    Console.Write("Vnesite ime: ");
                    ZIme = Console.ReadLine();
                    break;

                case 3:
                    Console.Write("Vnesite starost: ");
                    int s;
                    while (!int.TryParse(Console.ReadLine(), out s))
                    {
                        Console.Write("Vpisite starost ponovno: ");
                    }
                    ZStarost = s;
                    break;
            }

            Console.Write("Veste še kaj? [1] Da  [2] Ne: ");
            int p;
            while (!int.TryParse(Console.ReadLine(), out p))
            {
                Console.Write("Vpisite ponovno: ");
            }

            if (p != 1)
                konec = true;
        }

        // sEARCH
        Oseba? osebaZaNajt = osebe.FirstOrDefault(o =>
            (ZPriimek == null || o.Priimek == ZPriimek) &&
            (ZIme == null || o.Ime == ZIme) &&
            (ZStarost == null || o.Starost == ZStarost)
        );

        // razuktat
        if (osebaZaNajt != null)
        {
            Console.WriteLine("Oseba najdena:");
            Console.WriteLine($"{osebaZaNajt.Priimek} {osebaZaNajt.Ime}, {osebaZaNajt.Starost}");
        }
        else
        {
            Console.WriteLine("Oseba ni bila najdena.");
        }
        OIzbiri();
    }

}
class Oseba : IComparable<Oseba>
{
    public string Priimek {get; set;} = "";
    public string Ime {get; set;} = "";
    public int Starost{get; set;} 
    public int CompareTo(Oseba? other)
    {
        if (other == null) return 1; // null is smallest

        int lastNameCompare = Priimek.CompareTo(other.Priimek);
        if (lastNameCompare != 0) return lastNameCompare;

        int firstNameCompare = Ime.CompareTo(other.Ime);
        if (firstNameCompare != 0) return firstNameCompare;

        return Starost.CompareTo(other.Starost);
    }
   
}
