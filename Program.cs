using System;
using System.Collections.Generic;

namespace StudentManagementClub
{

    public class ClubRole
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string ContactInfo { get; set; }
    }


    public class StudentClub
    {
        public double Budget { get; set; }
        public ClubRole President { get; set; }
        public ClubRole VicePresident { get; set; }
        public ClubRole GeneralSecretary { get; set; }
        public ClubRole FinanceSociety { get; set; }

        private List<Society> societies = new List<Society>();

        public void FundSociety(FundedSociety society, double amount)
        {
            if (Budget >= amount)
            {
                society.FundingAmount += amount;
                Budget -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient budget to allocate funding.");
            }
        }

        public void DispenseFunds()
        {
            foreach (var society in societies)
            {
                if (society is FundedSociety fundedSociety)
                {
                    Console.WriteLine($"Society: {fundedSociety.Name}, Funding Amount: {fundedSociety.FundingAmount}");
                }
            }
        }

        public void RegisterSociety(Society society)
        {
            societies.Add(society);
        }

        public void DisplaySocieties()
        {
            foreach (var society in societies)
            {
                Console.WriteLine($"Society Name: {society.Name}, Contact: {society.Contact}");
            }
        }

        public List<Society> GetSocieties()
        {
            return societies;
        }
    }


    public class Society
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        private List<Activity> activities = new List<Activity>();

        public void AddActivity(Activity activity)
        {
            activities.Add(activity);
        }

        public void ListEvents()
        {
            foreach (var activity in activities)
            {
                Console.WriteLine($"Activity: {activity.Description}, Date: {activity.Date}");
            }
        }
    }


    public class FundedSociety : Society
    {
        public double FundingAmount { get; set; }
    }


    public class Activity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentClub club = new StudentClub { Budget = 10000 };
            Console.WriteLine("Students Management Club System");
            Console.WriteLine("1. Register a society");
            Console.WriteLine("2. Allocate funding to societies");
            Console.WriteLine("3. Register an event for a society");
            Console.WriteLine("4. Display society funding information");
            Console.WriteLine("5. Display events for society");
            Console.WriteLine("6. Exit");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nEnter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        RegisterSociety(club);
                        break;
                    case 2:
                        AllocateFunding(club);
                        break;
                    case 3:
                        RegisterEvent(club);
                        break;
                    case 4:
                        club.DispenseFunds();
                        break;
                    case 5:
                        DisplayEvents(club);
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select again.");
                        break;
                }
            }
        }

        static void RegisterSociety(StudentClub club)
        {
            Console.WriteLine("Enter society name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter contact: ");
            string contact = Console.ReadLine();
            Console.WriteLine("Is it a funded society? (yes/no): ");
            string isFunded = Console.ReadLine().ToLower();
            if (isFunded == "yes")
            {
                FundedSociety fundedSociety = new FundedSociety { Name = name, Contact = contact };
                club.RegisterSociety(fundedSociety);
            }
            else
            {
                Society society = new Society { Name = name, Contact = contact };
                club.RegisterSociety(society);
            }
        }

        static void AllocateFunding(StudentClub club)
        {
            Console.WriteLine("Enter society name to fund: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter funding amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());
            foreach (var society in club.GetSocieties())
            {
                if (society.Name == name && society is FundedSociety fundedSociety)
                {
                    club.FundSociety(fundedSociety, amount);
                    return;
                }
            }
            Console.WriteLine("Society not found or is not a funded society.");
        }

        static void RegisterEvent(StudentClub club)
        {
            Console.WriteLine("Enter society name: ");
            string name = Console.ReadLine();
            foreach (var society in club.GetSocieties())
            {
                if (society.Name == name)
                {
                    Console.WriteLine("Enter activity description: ");
                    string description = Console.ReadLine();
                    Console.WriteLine("Enter activity date (yyyy-MM-dd): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Activity activity = new Activity { Description = description, Date = date };
                    society.AddActivity(activity);
                    return;
                }
            }
            Console.WriteLine("Society not found.");
        }

        static void DisplayEvents(StudentClub club)
        {
            Console.WriteLine("Enter society name: ");
            string name = Console.ReadLine();
            foreach (var society in club.GetSocieties())
            {
                if (society.Name == name)
                {
                    society.ListEvents();
                    return;
                }
            }
            Console.WriteLine("Society not found.");
        }
    }
}


