using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class ClubRole
{
    private string name;
    private string role;
    private string contactInfo;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Role
    {
        get { return role; }
        set { role = value; }
    }

    public string ContactInfo
    {
        get { return contactInfo; }
        set { contactInfo = value; }
    }
}

public class Society
{
    private string name;
    private string contact;
    private List<string> events = new List<string>();

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Contact
    {
        get { return contact; }
        set { contact = value; }
    }

    public List<string> Events
    {
        get { return events; }
    }

    public void AddActivity()
    {
        Console.Write("Which society you want to assign activity: ");
        string input = Console.ReadLine();
        var societies = new List<Society>(); 
        var check = societies.FirstOrDefault(s => s.Name == input);

        if (check != null)
        {
            Console.Write("Enter the activity you want to assign to society: ");
            string activity = Console.ReadLine();
            check.Events.Add(activity);
        }
    }

    public void ListEvents()
    {
        Console.WriteLine("Listed events are here:");
        foreach (var evt in Events)
        {
            Console.WriteLine(evt);
        }
    }
}

public class FundedSociety : Society
{
    private double budget;

    public double Budget
    {
        get { return budget; }
        set { budget = value; }
    }
}

public class NonFundedSociety : Society
{
}

public class StudentClub
{
    private double budget;
    private ClubRole president;
    private ClubRole vicePresident;
    private ClubRole generalSecretary;  
    private ClubRole financeSecretary; 
    private List<Society> societies = new List<Society>();

    public double Budget
    {
        get { return budget; }
        set { budget = value; }
    }

    public ClubRole President
    {
        get { return president; }
        set { president = value; }
    }

    public ClubRole VicePresident
    {
        get { return vicePresident; }
        set { vicePresident = value; }
    }

    public ClubRole GeneralSecretary
    {
        get { return generalSecretary; }
        set { generalSecretary = value; }
    }

    public ClubRole FinanceSecretary
    {
        get { return financeSecretary; }
        set { financeSecretary = value; }
    }

    public void RegisterSociety()
    {
        Console.Write("Enter the name of new society: ");
        string societyName = Console.ReadLine();

        Console.Write("Now enter the hierarchy of this society: ");
        string hierarchy = Console.ReadLine();

        Society newSociety;
        if (societyName == "Techbit Society" || societyName == "Literary Society" || societyName == "Sports Society")
        {
            newSociety = new FundedSociety { Name = societyName, Contact = hierarchy, Budget = (societyName == "Techbit Society") ? 600 : 500 };
        }
        else
        {
            newSociety = new NonFundedSociety { Name = societyName, Contact = hierarchy };
        }

        societies.Add(newSociety);
    }

    public void AllocateFunding()
    {
        Console.Write("Enter the name of the society to fund: ");
        string societyName = Console.ReadLine();
        var society = societies.FirstOrDefault(s => s.Name == societyName);

        if (society != null && society is FundedSociety fundedSociety)
        {
            Console.Write("Enter the amount of funding: ");
            double amount = double.Parse(Console.ReadLine());
            fundedSociety.Budget += amount;
        }
    }

    public void RegisterEvent()
    {
        Console.Write("Enter the name of the society to register an event: ");
        string societyName = Console.ReadLine();
        var society = societies.FirstOrDefault(s => s.Name == societyName);

        if (society != null)
        {
            society.AddActivity();
        }
    }

    public void DisplayFundingInformation()
    {
        foreach (var society in societies)
        {
            if (society is FundedSociety fundedSociety)
            {
                Console.WriteLine($"{fundedSociety.Name} has a budget of {fundedSociety.Budget}");
            }
            else
            {
                Console.WriteLine($"{society.Name} is a non-funded society.");
            }
        }
    }

    public void DisplayEvents()
    {
        Console.Write("Enter the name of the society to list events: ");
        string societyName = Console.ReadLine();
        var society = societies.FirstOrDefault(s => s.Name == societyName);

        if (society != null)
        {
            society.ListEvents();
        }
    }
}

public class User
{
    public static void Main(string[] args)
    {
        int option = 0;
        var studentClub = new StudentClub();

        do
        {
            Console.WriteLine("Student Club Management System");
            Console.WriteLine("1. Register a new society");
            Console.WriteLine("2. Allocate Funding to Societies");
            Console.WriteLine("3. Register an event for a society");
            Console.WriteLine("4. Display Society Funding Information");
            Console.WriteLine("5. Display events for a society");
            Console.WriteLine("6. Exit");
            option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    studentClub.RegisterSociety();
                    break;
                case 2:
                    studentClub.AllocateFunding();
                    break;
                case 3:
                    studentClub.RegisterEvent();
                    break;
                case 4:
                    studentClub.DisplayFundingInformation();
                    break;
                case 5:
                    studentClub.DisplayEvents();
                    break;
                case 6:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        } while (option != 6);
    }
}
