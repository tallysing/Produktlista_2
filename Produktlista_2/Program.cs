
List<Product> products = new List<Product>(); // Store products

void PresentProducts() // Presents a list
{
    List<Product> sortedProducts = products.OrderBy(product => product.Price).ToList();

    Table table = new Table(sortedProducts);

    table.Output();

    Console.ForegroundColor = ConsoleColor.Blue;

    Console.WriteLine("To enter a new product - enter \"P\" | To search for a product - enter: \"S\" | To quit enter: \"Q\"");

    Console.ResetColor();

    string input = Console.ReadLine();

    string userInput = CheckInput(input);

    if (userInput == "S") // Search Product
    {
        Console.Write("Enter a Product Name: ");

        string inputName = Console.ReadLine();

        string product = CheckInput(inputName);

        if (product == inputName)
        {
            table.SearchRow(product);
            table.Output();

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("To enter a new product - enter \"P\" | To search for a product - enter: \"S\" | To quit enter: \"Q\"");

            Console.ResetColor();

            CheckInput(Console.ReadLine());
        }
    }
}

string CheckInput(string input) // Check user input
{
    switch (input.ToUpper())
    {
        case "":
            Console.WriteLine("You didn't enter anything.");

            return "";

        case "Q":

            PresentProducts();

            return "Q";

        case "S": return "S";

        case "P":

            UserInputs();

            return "P";

        default: return input;
    }
}

void UserInputs() // Ask for user inputs
{
    do
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine("To enter a new product - follow the steps | To quit - enter: \"Q\"");

        Console.ResetColor();

        Console.Write("Enter a Category: ");

        string inputCategory = Console.ReadLine();

        string catergory = CheckInput(inputCategory);

        if (catergory == "")
        {
            continue;
        }
        if (catergory == "Q")
        {
            break;
        }

        Console.Write("Enter a Product Name: ");

        string inputProductName = Console.ReadLine();

        string productName = CheckInput(inputProductName);

        if (productName == "")
        {
            continue;
        }
        if (productName == "Q")
        {
            break;
        }
        Console.Write("Enter a Price: ");

        string inputPrice = Console.ReadLine();

        string strPrice = CheckInput(inputPrice);

        if (strPrice == "")
        {
            continue;
        }
        if (strPrice == "Q")
        {
            break;
        }
        int price = Convert.ToInt32(strPrice);

        if (price != 0)
        {
            Product product = new Product(catergory, productName, price); // Creates a product

            products.Add(product);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The product was successfully added!");
            Console.ResetColor();
            Console.WriteLine("-------------------------------------------------");
        }
        else
        {
            Console.WriteLine("You didn't enter price.");
        }
    } while (true);
}

UserInputs();

class Product // Blue print of product
{
    public Product(string category, string productName, int price)
    {

        Category = category;

        ProductName = productName;

        Price = price;
    }
    public string Category { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }

}

class Table // Blue print of table
{
    private List<Product> products;

    private string highlightRow;

    public Table(List<Product> products)
    {
        this.products = products;
    }
    public void Output() // Creates a table of products
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("|   Category   |   Product    |   Price   |");
        Console.WriteLine("-------------------------------------------");
        Console.ResetColor();

        foreach (Product product in products)
        {
            if (product.ProductName == highlightRow)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("| {0} | {1} | {2} |", PadRight(product.Category, 12), PadRight(product.ProductName, 12), PadLeft(product.Price.ToString(), 9));

                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("| {0} | {1} | {2} |", PadRight(product.Category, 12), PadRight(product.ProductName, 12), PadLeft(product.Price.ToString(), 9));
            }
        }
        int sum = products.Sum(product => product.Price);

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("|                Total amount:  {0}       |", sum);
        Console.WriteLine("-------------------------------------------");

    }
    public void SearchRow(string userInput) // Saves the reference to highlight the searched item in the presented list
    {
        highlightRow = userInput;
    }
    private string PadRight(string text, int width)
    {
        return text.PadRight(width);
    }
    private string PadLeft(string text, int width)
    {
        return text.PadLeft(width);
    }
}