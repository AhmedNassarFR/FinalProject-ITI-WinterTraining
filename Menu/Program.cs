using backend;

class Program
{
    

    static void Main(string[] args)
    {
        Class1 dbOperations = new Class1();

        Console.CursorVisible = false;
        List<string> mainMenuOptions = new List<string> { "Add", "Display", "Search", "Update", "Delete", "Exit" };
        int highlight = 0;

        while (true)
        {
            Console.Clear();
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalSpacing = screenHeight / (mainMenuOptions.Count + 1);
            int textX = screenWidth / 2;

            for (int i = 0; i < mainMenuOptions.Count; i++)
            {
                int textY = verticalSpacing * (i + 1);
                Console.SetCursorPosition(textX - (mainMenuOptions[i].Length / 2), textY);

                if (i == highlight)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(mainMenuOptions[i]);
                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? mainMenuOptions.Count - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == mainMenuOptions.Count - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    if (mainMenuOptions[highlight] == "Exit")
                    {
                        string a = "Thanks for using my app!";
                        string b = "SALAAAM!";

                        int x = (screenWidth - a.Length) / 2;
                        int y = screenHeight / 2 - 1;

                        Console.Clear();
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(a);

                        x = (screenWidth - b.Length) / 2;
                        Console.SetCursorPosition(x, y + 1);
                        Console.WriteLine(b);
                        Task.Delay(3000).Wait();
                        return;
                    }
                    else if (mainMenuOptions[highlight] == "Add")
                    {
                        ShowAddMenu(dbOperations);
                    }
                    else if (mainMenuOptions[highlight] == "Display")
                    {
                        ShowDisplayMenu(dbOperations);
                    }
                    else if (mainMenuOptions[highlight] == "Update")
                    {
                        ShowUpdateMenu(dbOperations);
                    }
                    else if (mainMenuOptions[highlight] == "Delete")
                    {
                        ShowDeleteMenu(dbOperations);
                    }
                    else if (mainMenuOptions[highlight] == "Search")
                    {
                        ShowSearchMenu(dbOperations);
                    }
                    break;
            }
        }
    }

    static void ShowAddMenu(Class1 dbOperations)
    {
        List<string> addMenuOptions = new List<string> { "Employee", "Department", "Back" };
        int highlight = 0;

        while (true)
        {
            Console.Clear();
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalSpacing = screenHeight / (addMenuOptions.Count + 1);
            int textX = screenWidth / 2;

            for (int i = 0; i < addMenuOptions.Count; i++)
            {
                int textY = verticalSpacing * (i + 1);
                Console.SetCursorPosition(textX - (addMenuOptions[i].Length / 2), textY);

                if (i == highlight)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(addMenuOptions[i]);
                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? addMenuOptions.Count - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == addMenuOptions.Count - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    if (addMenuOptions[highlight] == "Back")
                    {
                        return;
                    }
                    else if (addMenuOptions[highlight] == "Employee")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        string name;
                        do
                        {
                            Console.Write("Enter Employee Name: ");
                            name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Employee Name cannot be empty. Please try again.");
                            }
                        } while (string.IsNullOrWhiteSpace(name));

                        Console.Write("\nChoose a department ID from the following : \n");
                        dbOperations.GetAllDepartments();

                        int departmentId;
                        do
                        {
                            Console.Write("\nEnter Department ID: ");
                            if (!int.TryParse(Console.ReadLine(), out departmentId))
                            {
                                Console.WriteLine("Invalid Department ID. Please try again.");
                            }
                        } while (departmentId <= 0);

                        dbOperations.AddEmployee(name, departmentId);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    else if (addMenuOptions[highlight] == "Department")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        string name;
                        do
                        {
                            Console.Write("Enter Department Name: ");
                            name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Department Name cannot be empty. Please try again.");
                            }
                        } while (string.IsNullOrWhiteSpace(name));

                        dbOperations.AddDepartment(name);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }

    static void ShowDisplayMenu(Class1 dbOperations)
    {
        List<string> displayMenuOptions = new List<string> { "Employees", "Departments", "Back" };
        int highlight = 0;

        while (true)
        {
            Console.Clear();
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalSpacing = screenHeight / (displayMenuOptions.Count + 1);
            int textX = screenWidth / 2;

            for (int i = 0; i < displayMenuOptions.Count; i++)
            {
                int textY = verticalSpacing * (i + 1);
                Console.SetCursorPosition(textX - (displayMenuOptions[i].Length / 2), textY);

                if (i == highlight)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(displayMenuOptions[i]);
                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? displayMenuOptions.Count - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == displayMenuOptions.Count - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    if (displayMenuOptions[highlight] == "Back")
                    {
                        return;
                    }
                    else if (displayMenuOptions[highlight] == "Employees")
                    {
                        Console.Clear();
                        Console.WriteLine("Choose how to display employees:");
                        Console.WriteLine("1. All Employees");
                        Console.WriteLine("2. Employees by Department");
                        if (int.TryParse(Console.ReadLine(), out int subChoice))
                        {
                            if (subChoice == 1)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(0, 0);

                                dbOperations.GetAllEmployees();
                                Console.WriteLine("\nPress ENTER to return...");
                                Console.ReadLine();
                            }
                            else if (subChoice == 2)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine("Choose a department ID to display the employees in it:\n");

                                dbOperations.GetAllDepartments();
                                int departmentId;
                                do
                                {
                                    Console.Write("\nEnter Department ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out departmentId))
                                    {
                                        Console.WriteLine("Invalid Department ID. Please try again.");
                                    }
                                } while (departmentId <= 0);

                                dbOperations.GetEmployeesByDepartment(departmentId);
                                Console.WriteLine("\nPress ENTER to return...");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                    }
                    else if (displayMenuOptions[highlight] == "Departments")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        dbOperations.GetAllDepartments();
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }

    static void ShowUpdateMenu(Class1 dbOperations)
    {
        List<string> updateMenuOptions = new List<string> { "Employee", "Department", "Back" };
        int highlight = 0;

        while (true)
        {
            Console.Clear();
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalSpacing = screenHeight / (updateMenuOptions.Count + 1);
            int textX = screenWidth / 2;

            for (int i = 0; i < updateMenuOptions.Count; i++)
            {
                int textY = verticalSpacing * (i + 1);
                Console.SetCursorPosition(textX - (updateMenuOptions[i].Length / 2), textY);

                if (i == highlight)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(updateMenuOptions[i]);
                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? updateMenuOptions.Count - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == updateMenuOptions.Count - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    if (updateMenuOptions[highlight] == "Back")
                    {
                        return;
                    }
                    else if (updateMenuOptions[highlight] == "Employee")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Choose an Employee ID to update:");

                        dbOperations.GetAllEmployees();

                        int employeeId;
                        do
                        {
                            Console.Write("\nEnter Employee ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out employeeId) || employeeId <= 0)
                            {
                                Console.WriteLine("Invalid Employee ID. Please try again.");
                                employeeId = -1;
                            }
                        } while (employeeId == -1);

                        if (!dbOperations.EmployeeExists(employeeId))
                        {
                            Console.WriteLine("Employee not found. No updates will be made.\nPress ENTER to return...");
                            Console.ReadLine();
                            return;
                        }

                        Console.WriteLine("\nPress ENTER to keep the current value.\n");

                        Console.Write("Enter new Employee Name: ");
                        string newName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newName)) newName = null;

                        Console.Write("\nChoose a department ID from the following:\n");
                        dbOperations.GetAllDepartments();

                        int newDepartmentId = -1;
                        Console.Write("Enter new Department ID: ");
                        string newDepartmentIdInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(newDepartmentIdInput) && int.TryParse(newDepartmentIdInput, out int parsedDeptId))
                        {
                            if (dbOperations.DepartmentExists(parsedDeptId))
                            {
                                newDepartmentId = parsedDeptId;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Department ID. Keeping the current value.");
                            }
                        }

                        dbOperations.UpdateEmployee(employeeId, newName, newDepartmentId);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    else if (updateMenuOptions[highlight] == "Department")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Choose a Department ID to update:\n");
                        dbOperations.GetAllDepartments();

                        int departmentId;
                        do
                        {
                            Console.Write("\nEnter Department ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out departmentId) || departmentId <= 0)
                            {
                                Console.WriteLine("Invalid Department ID. Please try again.");
                                departmentId = -1;
                            }
                        } while (departmentId == -1);

                        if (!dbOperations.DepartmentExists(departmentId))
                        {
                            Console.WriteLine("Department not found. No updates will be made.\nPress ENTER to return...");
                            Console.ReadLine();
                            return;
                        }

                        Console.WriteLine("Press ENTER to keep the current value.");
                        Console.Write("Enter new Department Name: ");
                        string newDepartmentName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(newDepartmentName)) newDepartmentName = null;

                        dbOperations.UpdateDepartment(departmentId, newDepartmentName);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }

    static void ShowDeleteMenu(Class1 dbOperations)
    {
        List<string> deleteMenuOptions = new List<string> { "Employee", "Department", "Back" };
        int highlight = 0;

        while (true)
        {
            Console.Clear();
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalSpacing = screenHeight / (deleteMenuOptions.Count + 1);
            int textX = screenWidth / 2;

            for (int i = 0; i < deleteMenuOptions.Count; i++)
            {
                int textY = verticalSpacing * (i + 1);
                Console.SetCursorPosition(textX - (deleteMenuOptions[i].Length / 2), textY);

                if (i == highlight)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(deleteMenuOptions[i]);
                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? deleteMenuOptions.Count - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == deleteMenuOptions.Count - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    if (deleteMenuOptions[highlight] == "Back")
                    {
                        return;
                    }
                    else if (deleteMenuOptions[highlight] == "Employee")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Choose an Employee ID to delete:");

                        dbOperations.GetAllEmployees();
                        int employeeId;
                        do
                        {
                            Console.Write("\nEnter Employee ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out employeeId))
                            {
                                Console.WriteLine("Invalid Employee ID. Please try again.");
                            }
                        } while (employeeId <= 0);

                        dbOperations.DeleteEmployee(employeeId);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    else if (deleteMenuOptions[highlight] == "Department")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Choose a department ID to delete:\n");

                        dbOperations.GetAllDepartments();
                        int departmentId;
                        do
                        {
                            Console.Write("\nEnter Department ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out departmentId))
                            {
                                Console.WriteLine("Invalid Department ID. Please try again.");
                            }
                        } while (departmentId <= 0);

                        dbOperations.DeleteDepartment(departmentId);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }

    static void ShowSearchMenu(Class1 dbOperations)
    {
        List<string> searchMenuOptions = new List<string> { "Employee", "Department", "Back" };
        int highlight = 0;

        while (true)
        {
            Console.Clear();
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalSpacing = screenHeight / (searchMenuOptions.Count + 1);
            int textX = screenWidth / 2;

            for (int i = 0; i < searchMenuOptions.Count; i++)
            {
                int textY = verticalSpacing * (i + 1);
                Console.SetCursorPosition(textX - (searchMenuOptions[i].Length / 2), textY);

                if (i == highlight)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.Write(searchMenuOptions[i]);
                Console.ResetColor();
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? searchMenuOptions.Count - 1 : highlight - 1;
                    break;
                case ConsoleKey.DownArrow:
                    highlight = (highlight == searchMenuOptions.Count - 1) ? 0 : highlight + 1;
                    break;
                case ConsoleKey.Enter:
                    if (searchMenuOptions[highlight] == "Back")
                    {
                        return;
                    }
                    else if (searchMenuOptions[highlight] == "Employee")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("Enter Employee Name to search: ");
                        string searchTerm = Console.ReadLine();
                        dbOperations.SearchEmployees(searchTerm);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    else if (searchMenuOptions[highlight] == "Department")
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("Enter Department Name to search: ");
                        string searchTerm = Console.ReadLine();
                        dbOperations.SearchDepartments(searchTerm);
                        Console.WriteLine("\nPress ENTER to return...");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }
}