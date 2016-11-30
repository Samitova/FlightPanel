using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FlightPanel
{
    enum FlightStatus
    {
        Check_in,
        Gate_closed,
        Arrived,
        Departed,
        Unknown,
        Canceled,
        Expected,
        Delayed,
        In_flight
    };
    enum Cities
    {
        Rome,
        Paris,
        Berlin,
        Melburn,
        NewYork,
        Kiev,
        Lvov,
        Kharkov,
        London
    };
    enum Airlines
    {
        OpenAir,
        MetroJet,
        Airunes,
        TransAero
    };
    enum Gates
    {
        G1 = 1,
        G2,
        G3,
        G4,
        G5,
        G6
    };
    enum Terminals
    {
        A,
        B,
        C
    };

    struct Flight
    {
        public Airlines Airline;
        public String FlightNumber;
        public DateTime DepartureDate;
        public DateTime ArrivalDate;
        public Cities DeparturePort;
        public Cities ArrivalPort;
        public Terminals Terminal;
        public Gates Gate;
        public FlightStatus FlightStatus;

        public Flight(Airlines airline, String number, DateTime departDate, DateTime arrivDate,
                      Cities departure, Cities arrival, Terminals terminal, Gates gate, FlightStatus status)
        {
            Airline = airline;
            FlightNumber = number;
            DepartureDate = departDate;
            ArrivalDate = arrivDate;
            ArrivalPort = arrival;
            DeparturePort = departure;
            Terminal = terminal;
            Gate = gate;
            FlightStatus = status;
        }

        override public string ToString()
        {
            return $"{Airline,-10} {FlightNumber,-12}{DeparturePort,-15}{ArrivalPort,-15}{DepartureDate.ToString("dd.MM"),-10}{DepartureDate.ToString("HH:mm"),-5}-{ArrivalDate.ToString("HH:mm"),-15}{Terminal,-10}{Gate,-10}{FlightStatus,-15}";
        }
    }

    class Program
    {
        static Flight[] flights;
        const string LOGFILE= "UserFlightInformation.txt", DATAFILE = "flights.txt";

        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 50);            
            ClearLogFile();
            flights = LoadData();
            DisplayMainMenu();
        }

        /// <summary>
        /// Display the main menu of the programm
        /// </summary>
        private static void DisplayMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMAIN MENU\n");
            Console.WriteLine("1 - Flights information");
            Console.WriteLine("2 - Edit the flights");
            Console.WriteLine("3 - Search the flights");
            Console.WriteLine("4 - Emergency information");
            Console.WriteLine("5 - Exit\n");

            string option = Console.ReadLine();
            Console.ResetColor();
            switch (option)
            {
                case "1":
                    DisplayFlightInformationMenu();
                    break;
                case "2":
                    DisplayEditMenu();
                    break;
                case "3":
                    DisplaySearchMenu();
                    break;
                case "4":
                    DisplayEmergencyMenu();
                    break;
                case "5":
                    Exit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command. Please, try again.");
                    Console.ResetColor();
                    DisplayMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Display flight information menu of the programm
        /// </summary>
        private static void DisplayFlightInformationMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nFLIGHT INFORMATION PANEL\n");
            Console.WriteLine("1 - All flights");
            Console.WriteLine("2 - Departures information");
            Console.WriteLine("3 - Arrivals information");
            Console.WriteLine("4 - Return to main menu");
            Console.WriteLine("5 - Exit\n");

            string option = Console.ReadLine();
            Console.ResetColor();
            switch (option)
            {
                case "1":
                    GetAllFlights();
                    break;
                case "2":
                    GetDepartureInformation();
                    break;
                case "3":
                    GetArrivalsInformation();
                    break;
                case "4":
                    DisplayMainMenu();
                    break;
                case "5":
                    Exit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command. Please, try again.");
                    Console.ResetColor();
                    DisplayFlightInformationMenu();
                    break;
            }
        }

        /// <summary>
        /// Display the search menu of the programm
        /// </summary>
        private static void DisplaySearchMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nSEARCH THE FLIGHTS\n");
            Console.WriteLine("1 - Search by fligth number");
            Console.WriteLine("2 - Search by departure time");
            Console.WriteLine("3 - Search by arrival time");
            Console.WriteLine("4 - Search by departure port");
            Console.WriteLine("5 - Search by arrival port");
            Console.WriteLine("6 - Search by time and departure/arrival port");
            Console.WriteLine("7 - Return to the main menu");
            Console.WriteLine("8 - Exit\n");

            string option = Console.ReadLine().Trim();
            Console.ResetColor();
            switch (option)
            {
                case "1":
                    SearchFlightNumber();
                    break;
                case "2":
                    SearchFlightDepartureTime();
                    break;
                case "3":
                    SearchFlightArrivalTime();
                    break;
                case "4":
                    SearchFlightDeparturePort();
                    break;
                case "5":
                    SearchFlightArrivalPort();
                    break;
                case "6":
                    SearchFlightPortsAndTime();
                    break;
                case "7":
                    DisplayMainMenu();
                    break;
                case "8":
                    Exit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command. Please, try again.");
                    Console.ResetColor();
                    DisplaySearchMenu();
                    break;
            }
        }

        /// <summary>
        /// Display the edit menu of the programm
        /// </summary>
        private static void DisplayEditMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nEDIT THE FLIGHTS\n");
            Console.WriteLine("1 - Add new flight");
            Console.WriteLine("2 - Edit flight");
            Console.WriteLine("3 - Delete flight");
            Console.WriteLine("4 - Return to the main menu");
            Console.WriteLine("5 - Exit\n");

            string option = Console.ReadLine().Trim();
            Console.ResetColor();
            switch (option)
            {
                case "1":
                    AddFlight();
                    break;
                case "2":
                    EditFlight();
                    break;
                case "3":
                    DeleteFlight();
                    break;
                case "4":
                    DisplayMainMenu();
                    break;
                case "5":
                    Exit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command. Please, try again.", ConsoleColor.Red);
                    Console.ResetColor();
                    DisplayEditMenu();
                    break;
            }
        }

        /// <summary>
        /// Display the emergency menu of the programm
        /// </summary>
        private static void DisplayEmergencyMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nEMERGEMCY INFORMATION\n");
            Console.WriteLine("1 - Fire");
            Console.WriteLine("2 - Evacuation");
            Console.WriteLine("3 - Return to the main menu");
            Console.WriteLine("6 - Exit\n");

            string option = Console.ReadLine().Trim();
            Console.ResetColor();
            switch (option)
            {
                case "1":
                    DisplayFireInformation();
                    break;
                case "2":
                    DisplayEvacuationInformation();
                    break;
                case "3":
                    DisplayMainMenu();
                    break;
                case "4":
                    Exit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command. Please, try again.", ConsoleColor.Red);
                    Console.ResetColor();
                    DisplayEmergencyMenu();
                    break;
            }
        }      

        /// <summary>
        /// Display fire emergency information
        /// </summary>
        private static void DisplayEvacuationInformation()
        {
            string outputText = "\nEVACUATION\n\nYou should do .....";
            Console.WriteLine(outputText);
            SaveData(outputText);
            DisplayEmergencyMenu();
        }

        /// <summary>
        /// Display evacuation information
        /// </summary>
        private static void DisplayFireInformation()
        {
            string outputText = "\nFIRE\n\nYou should do .....";
            Console.WriteLine(outputText);
            SaveData(outputText);
            DisplayEmergencyMenu();
        }

        /// <summary>
        /// Display the caption for the flights
        /// </summary>
        private static void DisplayFlightCaption()
        {
            Console.WriteLine("{0,-14} {1,14} {2,14} {3, 12} {4,12} {5, 17} {6,8} {7, 13}", "Airline/Flight", "Departure", "Arrival", "Date", "Time", "Termilan", "Gate", "Status");
            Console.WriteLine("____________________________________________________________________________________________________________________\n");
        }

        /// <summary>
        /// Add new flight
        /// </summary>
        private static void AddFlight()
        {
            string outputText = "\nADDING\n";
            Console.WriteLine(outputText);
            Flight newFlight = AskForFlight();
            string message = $"\nThe flight number {newFlight.FlightNumber} was added";
            outputText += $"{message}\nNew flight is\n{FormatOutputText(newFlight.ToString())}";
            Array.Resize<Flight>(ref flights, flights.Length + 1);
            flights[flights.Length - 1] = newFlight;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();

            SaveData(outputText);
            DisplayEditMenu();
        }

        /// <summary>
        /// Delete the flight
        /// </summary>
        private static void DeleteFlight()
        {
            string outputText = "\nDELETING\n";
            Console.WriteLine(outputText);
            int deleteIndex = SearchFlightIndexByNumber();
            if (deleteIndex == -1)
            {
                outputText += "\nThe flight for deleting wasn`t found\n";
                Console.WriteLine(outputText);
            }
            else
            {
                string message = $"\nThe flight number {flights[deleteIndex].FlightNumber} was deleted\n";
                outputText += message;
                Flight[] newFlights = new Flight[flights.Length - 1];
                int count = 0;
                for (int i = 0; i < flights.Length; i++)
                {
                    if (i == deleteIndex) continue;
                    newFlights[count++] = flights[i];
                }
                flights = newFlights;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            SaveData(outputText);
            DisplayEditMenu();
        }

        /// <summary>
        /// Edit the flight
        /// </summary>
        private static void EditFlight()
        {
            string outputText = "\nEDITTING\n";
            Console.WriteLine(outputText);
            int editIndex = SearchFlightIndexByNumber();
            if (editIndex == -1)
            {
                outputText += "\nThe flight for editing wasn`t found\n";
                Console.WriteLine(outputText);
            }
            else
            {
                string message = $"\nThe flight number {flights[editIndex].FlightNumber} was edited";
                EditOptions(editIndex);
                outputText += $"{message}\nNew flight is\n{FormatOutputText(flights[editIndex].ToString())}";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            SaveData(outputText);
            DisplayEditMenu();
        }

        /// <summary>
        /// Menu for optional editing of flight
        /// </summary>
        /// <param name="index">Index og editing flight</param>
        private static void EditOptions(int index)
        {
            Console.WriteLine($"{flights[index].ToString()}\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nWhat information would you like to edit? (1 - Airlines; 2 - Number; 3 - Departure Date; 4 - Arrival Date;");
            Console.WriteLine("5 - Departure Port; 6 - Arrival Port; 7 - Terminal; 8 - Gate; 9 - Status; 10 - All information; 11 - Save and Return)\n");
            string option = Console.ReadLine();
            Console.ResetColor();

            switch (option)
            {
                case "1":
                    flights[index].Airline = GetNewAirlines();
                    EditOptions(index);
                    break;
                case "2":
                    flights[index].FlightNumber = GetNewNumber();
                    EditOptions(index);
                    break;
                case "3":
                    flights[index].DepartureDate = GetNewDepartureDate();
                    EditOptions(index);
                    break;
                case "4":
                    flights[index].ArrivalDate = GetNewArrivalDate();
                    EditOptions(index);
                    break;
                case "5":
                    flights[index].DeparturePort = GetNewDeparturePort();
                    EditOptions(index);
                    break;
                case "6":
                    flights[index].ArrivalPort = GetNewArrivalPort();
                    EditOptions(index);
                    break;
                case "7":
                    flights[index].Terminal = GetNewTerminal();
                    EditOptions(index);
                    break;
                case "8":
                    flights[index].Gate = GetNewGate();
                    EditOptions(index);
                    break;
                case "9":
                    flights[index].FlightStatus = GetNewStatus();
                    EditOptions(index);
                    break;
                case "10":
                    flights[index] = AskForFlight();
                    EditOptions(index);
                    break;
                case "11":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown commant. Try again.");
                    Console.ResetColor();
                    EditOptions(index);
                    break;
            }
        }

        /// <summary>
        /// Get new flight status
        /// </summary>
        /// <returns>FlightStatus</returns>
        private static FlightStatus GetNewStatus()
        {
            FlightStatus status = FlightStatus.Unknown;
            bool parse = false;
            while (!parse)
            {
                Console.WriteLine($"Enter new flight state ({string.Join(", ", Enum.GetNames(typeof(FlightStatus)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out status);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid flight state. Try again.");
                    Console.ResetColor();
                }
            }
            return status;
        }

        /// <summary>
        /// Get new gate
        /// </summary>
        /// <returns>Gate</returns>
        private static Gates GetNewGate()
        {
            bool parse = false;
            Gates gate = Gates.G1;
            while (!parse)
            {
                Console.WriteLine($"Enter new gate ({string.Join(", ", Enum.GetNames(typeof(Gates)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out gate);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid gate. Try again.");
                    Console.ResetColor();
                }
            }
            return gate;
        }

        /// <summary>
        /// Get new terminal
        /// </summary>
        /// <returns>Terminal</returns>
        private static Terminals GetNewTerminal()
        {
            bool parse = false;
            Terminals terminal = Terminals.A;
            while (!parse)
            {
                Console.WriteLine($"Enter new terminal ({string.Join(", ", Enum.GetNames(typeof(Terminals)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out terminal);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid terminal. Try again.");
                    Console.ResetColor();
                }
            }
            return terminal;
        }

        /// <summary>
        /// Get new arrival port
        /// </summary>
        /// <returns>Arrival port</returns>
        private static Cities GetNewArrivalPort()
        {
            bool parse = false;
            Cities arrival = Cities.Berlin;
            while (!parse)
            {
                Console.WriteLine($"Enter new arrival port ({string.Join(", ", Enum.GetNames(typeof(Cities)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out arrival);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid departure port. Try again.");
                    Console.ResetColor();
                }
            }
            return arrival;
        }

        /// <summary>
        /// Get new departure port
        /// </summary>
        /// <returns>Departure port</returns>
        private static Cities GetNewDeparturePort()
        {
            bool parse = false;
            Cities departure = Cities.Berlin;
            while (!parse)
            {
                Console.WriteLine($"Enter new departure port ({string.Join(", ", Enum.GetNames(typeof(Cities)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out departure);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid departure port. Try again.");
                    Console.ResetColor();
                }
            }
            return departure;
        }

        /// <summary>
        /// Get new arrival date
        /// </summary>
        /// <returns>Arrival date</returns>
        private static DateTime GetNewArrivalDate()
        {
            bool parse = false;
            DateTime arrivDate = DateTime.Now;
            while (!parse)
            {
                Console.WriteLine("Enter flight arrival date in format dd.MM.yyyy HH:mm");
                parse = DateTime.TryParse(Console.ReadLine(), out arrivDate);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date. Try again.");
                    Console.ResetColor();
                }
            }
            return arrivDate;
        }

        /// <summary>
        /// Get new depatrure 
        /// </summary>
        /// <returns></returns>
        private static DateTime GetNewDepartureDate()
        {
            bool parse = false;
            DateTime departDate = DateTime.Now;
            while (!parse)
            {
                Console.WriteLine("Enter new flight departure date in format dd.MM.yyyy HH:mm");
                parse = DateTime.TryParse(Console.ReadLine(), out departDate);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date. Try again.");
                    Console.ResetColor();
                }
            }
            return departDate;
        }

        /// <summary>
        /// Get new flight number
        /// </summary>
        /// <returns>Flight number</returns>
        private static string GetNewNumber()
        {
            Console.WriteLine("Enter new flight number");
            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// Get new airlines
        /// </summary>
        /// <returns>Airlines</returns>
        private static Airlines GetNewAirlines()
        {
            bool parse = false;
            Airlines airline = Airlines.Airunes;
            while (!parse)
            {
                Console.WriteLine($"Enter new airline ({string.Join(", ", Enum.GetNames(typeof(Airlines)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out airline);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid airline. Try again.");
                    Console.ResetColor();
                }
            }
            return airline;
        }

        /// <summary>
        /// Recieve all information from user about flight for adding or editing
        /// </summary>
        /// <returns>flight</returns>
        private static Flight AskForFlight()
        {
            string temp = string.Empty, flightNumber;
            Terminals terminal = Terminals.A;
            Airlines airline = Airlines.Airunes;
            DateTime departDate = DateTime.Now, arrivDate = DateTime.Now;
            Cities arrival = Cities.Berlin, departure = Cities.Berlin;
            Gates gate = Gates.G1;
            FlightStatus status = FlightStatus.Unknown;

            Console.WriteLine("Enter the flight number");
            flightNumber = Console.ReadLine().Trim();

            CheckEnum(out airline, "airline");

            CheckDate(out departDate, "departure");

            CheckDate(out arrivDate, "arrival");    
           
            CheckEnum(out departure, "departure port");
    
            CheckEnum(out arrival, "arrival port");         
    
            CheckEnum(out terminal, "terminal");
             
            CheckEnum(out gate, "gate");

            CheckEnum(out status, "flight status");          

            return new Flight(airline, flightNumber, departDate, arrivDate, departure, arrival, terminal, gate, status);
        }

        private static void CheckEnum <T> (out T value, string paramName) where T :struct
        {
            bool parse = false;
            Console.WriteLine($"Enter the {paramName} ({string.Join(", ", Enum.GetNames(typeof(T)))})");
            do
            {
                parse = Enum.TryParse(Console.ReadLine().Trim(), out value);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid {paramName}. Try again.");
                    Console.ResetColor();
                }
            } while (!parse);          
        }

        private static void CheckDate (out DateTime value, string paramName)
        {
            bool parse = false;
            Console.WriteLine($"Enter flight {paramName} date in format dd.MM.yyyy HH:mm");
            do
            {
                parse = DateTime.TryParse(Console.ReadLine(), out value);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid date. Try again.");
                    Console.ResetColor();
                }
            } while (!parse);
        }

        /// <summary>
        /// Search the flight by the flight number
        /// </summary>
        /// <returns>Index of flight</returns>
        private static int SearchFlightIndexByNumber()
        {
            Console.WriteLine("Enter the flight number");
            string flightNumber = Console.ReadLine().Trim();
            Console.WriteLine($"\nSearching flight with number {flightNumber}");
            int index = -1;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].FlightNumber == flightNumber)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// Display all flights
        /// </summary>
        private static void GetAllFlights()
        {
            string outputText = "\nALL FLIGHTS\n";
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight.ToString());
                outputText += FormatOutputText(flight.ToString());
            }
            SaveData(outputText);
            DisplayFlightInformationMenu();
        }

        /// <summary>
        /// Get flight information about arrivals
        /// </summary>
        private static void GetArrivalsInformation()
        {
            string outputText = "\nSEARCHING THE ARRIVALS\n";
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            bool isFlightFound = false;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].FlightStatus == FlightStatus.Arrived || flights[i].FlightStatus == FlightStatus.Expected)
                {
                    Console.WriteLine(flights[i].ToString());
                    outputText += FormatOutputText(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                outputText += "No flights were found";
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplayFlightInformationMenu();
        }

        /// <summary>
        /// Get flight information about departures
        /// </summary>
        private static void GetDepartureInformation()
        {
            string outputText = "\nSEARCHING THE DEPARTURES\n";
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            bool isFlightFound = false;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].FlightStatus != FlightStatus.Arrived && flights[i].FlightStatus != FlightStatus.Expected)
                {
                    Console.WriteLine(flights[i].ToString());
                    outputText += FormatOutputText(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                outputText += "No flights were found";
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplayFlightInformationMenu();
        }

        /// <summary>
        /// Format flights before saving to the file
        /// </summary>
        /// <param name="inputInformation">Input string to format</param>        
        /// <returns>formatedInformation</returns>
        private static string FormatOutputText(string inputInformation)
        {
            string formatedInformation = string.Empty;
            string[] splitedItems = inputInformation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in splitedItems)
            {
                formatedInformation += $"{item} ";
            }
            formatedInformation += "\n";
            return formatedInformation;
        }

        /// <summary>
        /// Search the flights which is the nearest (1 hour) to the specified time to the specified from/to port and output information sorted by the time
        /// </summary>
        private static void SearchFlightPortsAndTime()
        {
            bool parse = false;
            string citiesInfo = string.Join(", ", Enum.GetNames(typeof(Cities)));
            Console.WriteLine("Enter the departure port ({0})", citiesInfo);
            Cities departure = Cities.Berlin;
            while (!parse)
            {
                parse = Enum.TryParse(Console.ReadLine().Trim(), out departure);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid departure port. Try again.");
                    Console.ResetColor();
                }
            }

            parse = false;
            Console.WriteLine("Enter the arrival port ({0})", citiesInfo);
            Cities arrival = Cities.Berlin;
            while (!parse)
            {
                parse = Enum.TryParse(Console.ReadLine().Trim(), out arrival);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid arrival port. Try again.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Enter the date in format dd.MM.yyyy HH:mm");
            DateTime date = DateTime.Now;
            parse = false;
            while (!parse)
            {
                parse = DateTime.TryParse(Console.ReadLine(), out date);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date. Try again.");
                    Console.ResetColor();
                }
            }

            string outputText = $"\nSearching the nearest flights from {departure} to {arrival} and time one hour to {date.ToString("HH:mm")}\n".ToUpper();
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            Flight[] choosedFlights = new Flight[flights.Length];
            int count = 0;
            for (int i = 0; i < flights.Length; i++)
            {
                if ((flights[i].ArrivalPort == arrival) && (flights[i].DeparturePort == departure) &&
                   ((flights[i].DepartureDate <= date.AddHours(1)) && (flights[i].DepartureDate >= date)))
                {
                    choosedFlights[count++] = flights[i];
                }
            }
            if (count == 0)
            {
                string message = $"No flights from {departure} to {arrival} and time one hour to {date.ToString("HH:mm")} were found";
                outputText += message;
                Console.WriteLine(message);
            }
            else
            {
                Array.Resize(ref choosedFlights, count);
                choosedFlights = SortFlights(choosedFlights);
                for (int i = 0; i < choosedFlights.Length; i++)
                {
                    outputText += FormatOutputText(flights[i].ToString());
                    Console.WriteLine(choosedFlights[i].ToString());
                }
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplaySearchMenu();
        }

        /// <summary>
        /// Sort the flight information by the time
        /// </summary>
        /// <param name="choosedFlight">Massiv of flights which satisfied condition</param>    
        /// <return>Sorted choosedFlight</return>    
        private static Flight[] SortFlights(Flight[] choosedFlight)
        {
            Flight tempFlight;
            for (int i = 0; i < choosedFlight.Length - 1; i++)
            {
                for (int j = i + 1; j < choosedFlight.Length; j++)
                {
                    if (choosedFlight[i].DepartureDate > choosedFlight[j].DepartureDate)
                    {
                        tempFlight = choosedFlight[i];
                        choosedFlight[i] = choosedFlight[j];
                        choosedFlight[j] = tempFlight;
                    }
                }
            }
            return choosedFlight;
        }

        /// <summary>
        /// Search and display the flights by the departure port
        /// </summary>
        private static void SearchFlightDeparturePort()
        {
            bool parse = false, isFlightFound = false;
            Cities departure = Cities.Berlin;
            while (!parse)
            {
                Console.WriteLine($"Enter the departure port ({string.Join(", ", Enum.GetNames(typeof(Cities)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out departure);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid departure port. Try again.");
                    Console.ResetColor();
                }
            }
            string outputText = $"\nSearching the flights with departure port {departure}\n".ToUpper();
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].DeparturePort == departure)
                {
                    outputText += FormatOutputText(flights[i].ToString());
                    Console.WriteLine(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                string message = $"The flights with departure port {departure} weren`t found";
                outputText += message;
                Console.WriteLine(message);
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplaySearchMenu();
        }

        /// <summary>
        /// Search and display the flights by the arrival port
        /// </summary>
        private static void SearchFlightArrivalPort()
        {
            bool parse = false, isFlightFound = false;
            Cities arrival = Cities.Berlin;
            while (!parse)
            {
                Console.WriteLine($"Enter the arrival port ({string.Join(", ", Enum.GetNames(typeof(Cities)))})");
                parse = Enum.TryParse(Console.ReadLine().Trim(), out arrival);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid arrival port. Try again.");
                    Console.ResetColor();
                }
            }
            string outputText = $"\nSearching the flights with arrival port {arrival}\n".ToUpper();
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].ArrivalPort == arrival)
                {
                    outputText += FormatOutputText(flights[i].ToString());
                    Console.WriteLine(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                string message = $"The flights with arrival port {arrival} weren`t found";
                outputText += message;
                Console.WriteLine(message);
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplaySearchMenu();
        }

        /// <summary>
        /// Search and display the flights by the flight number
        /// </summary>
        private static void SearchFlightNumber()
        {
            Console.WriteLine("Enter the flight number");
            string number = Console.ReadLine().Trim();
            string outputText = $"\nSEARCHING THE FLIGHT WITH NUMBER {number}\n";
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            bool isFlightFound = false;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].FlightNumber == number)
                {
                    outputText += FormatOutputText(flights[i].ToString());
                    Console.WriteLine(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                string message = $"The flight with number {number} wasn`t found";
                outputText += message;
                Console.WriteLine(message);
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplaySearchMenu();
        }

        /// <summary>
        /// Search and display the flights by the arrival time
        /// </summary>
        private static void SearchFlightArrivalTime()
        {
            DateTime date = DateTime.Now;
            bool parse = false;
            while (!parse)
            {
                Console.WriteLine("Enter flight arrival date in format dd.MM.yyyy HH:mm");
                parse = DateTime.TryParse(Console.ReadLine(), out date);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date. Try again.");
                    Console.ResetColor();
                }
            }
            string outputText = $"\nSEARCHING THE FLIGHTS WITH ARRIVAL DATE {date}\n";
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            bool isFlightFound = false;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].ArrivalDate == date)
                {
                    outputText += FormatOutputText(flights[i].ToString());
                    Console.WriteLine(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                string message = $"The flights with arrival date {date} weren`t found";
                outputText += message;
                Console.WriteLine(message);
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplaySearchMenu();
        }

        /// <summary>
        /// Search and display the flights by the departure time
        /// </summary>
        private static void SearchFlightDepartureTime()
        {
            DateTime date = DateTime.Now;
            bool parse = false;
            while (!parse)
            {
                Console.WriteLine("Enter flight departure date in format dd.MM.yyyy HH:mm");
                parse = DateTime.TryParse(Console.ReadLine(), out date);
                if (!parse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date. Try again.");
                    Console.ResetColor();
                }
            }
            string outputText = $"\nSEARCHING THE FLIGHTS WITH DEPARTURE DATE {date}\n";
            Console.WriteLine(outputText);
            DisplayFlightCaption();
            bool isFlightFound = false;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i].DepartureDate == date)
                {
                    outputText += FormatOutputText(flights[i].ToString());
                    Console.WriteLine(flights[i].ToString());
                    isFlightFound = true;
                }
            }
            if (!isFlightFound)
            {
                string message = $"The flights with departure date {date} weren`t found";
                outputText += message;
                Console.WriteLine(message);
            }
            SaveData(outputText);
            Console.WriteLine("");
            DisplaySearchMenu();
        }

        /// <summary>
        /// Load flights from the file
        /// </summary>
        /// <returns>flights</returns>
        private static Flight[] LoadData()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("LOADING DATA FROM FILE\n");
            Flight[] flights = null;
            string[] data = null;
            try
            {
                data = File.ReadAllLines(DATAFILE);                            
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            flights = new Flight[data.Length];
            for (int i = 0; i < flights.Length; i++)
            {
                flights[i] = ParseFlight(data[i]);
            }
            return flights;
        }

        /// <summary>
        /// Parse flight information from file
        /// </summary>
        /// <param name="unformatedFlight">String with unformated flight info</param>
        /// <returns>flight</returns>
        private static Flight ParseFlight(string unformatedFlight)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string[] splitedFlight = unformatedFlight.Split(new char[] { ',' });
            Airlines airline = Airlines.Airunes;
            if (!Enum.TryParse(splitedFlight[0], out airline))
                Console.WriteLine($"Invalid airline. Use default \"{airline}\". String - {unformatedFlight}.");

            string flNumber = splitedFlight[1];

            DateTime departDt = DateTime.Now;
            if (!DateTime.TryParse(splitedFlight[2], out departDt))
                Console.WriteLine($"Invalid departure date. Use default \"{departDt}\". String - {unformatedFlight}.");

            DateTime arrivalDt = DateTime.Now.AddHours(2);
            if (!DateTime.TryParse(splitedFlight[3], out arrivalDt))
                Console.WriteLine($"Invalid arrival date. Use default \"{arrivalDt}\". String - {unformatedFlight}.");

            Cities departP = Cities.Kiev;
            if (!Enum.TryParse(splitedFlight[4], out departP))
                Console.WriteLine($"Invalid departure port. Use default \"{departP}\". String - {unformatedFlight}.");

            Cities arrivalP = Cities.Berlin;
            if (!Enum.TryParse(splitedFlight[5], out arrivalP))
                Console.WriteLine($"Invalid arrival port. Use default \"{arrivalP}\". String - {unformatedFlight}.");

            Terminals term = Terminals.A;
            if (!Enum.TryParse(splitedFlight[6], out term))
                Console.WriteLine($"Invalid terminal. Use default \"{term}\". String - {unformatedFlight}.");

            Gates gate = Gates.G1;
            if (!Enum.TryParse(splitedFlight[7], out gate))
                Console.WriteLine($"Invalid gate. Use default \"{gate}\". String - {unformatedFlight}.");

            FlightStatus status = FlightStatus.Unknown;
            if (!Enum.TryParse(splitedFlight[8], out status))
                Console.WriteLine($"Invalid flight status. Use default \"{status}\". String - {unformatedFlight}.");

            Console.ResetColor();
            return new Flight(airline, flNumber, departDt, arrivalDt, departP, arrivalP, term, gate, status);
        }

        /// <summary>
        /// Save results of searching to the file
        /// </summary>
        /// <param name="data"> Data for saveing to the file</param>
        private static void SaveData(string data)
        {
            try
            {
                File.AppendAllText(LOGFILE, data);
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Save flights to file and exit
        /// </summary>
        private static void Exit()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Flight flight in flights)
            {
                builder.AppendFormat($"{flight.Airline},{flight.FlightNumber},{flight.DepartureDate.ToString("dd.MM.yyy HH:mm")},");
                builder.AppendFormat($"{flight.ArrivalDate.ToString("dd.MM.yyy HH:mm")},{flight.DeparturePort},{flight.ArrivalPort}, {flight.Terminal},");
                builder.AppendFormat($"{flight.Gate},{flight.FlightStatus}\n");
            }
            try
            {
                File.WriteAllText("flights.txt", builder.ToString());
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Clear old flight information in log file
        /// </summary>
        private static void ClearLogFile()
        {
            try
            {
                using (StreamWriter writer = File.CreateText("UserFlightInformation.txt"))
                {
                    writer.WriteLine("USER FLIGHTS INFORMATION\n");
                    writer.WriteLine("Airline Number Departure Arrival Date Time Terminal Gate Status");
                    writer.WriteLine("______________________________________________________________\n");
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
