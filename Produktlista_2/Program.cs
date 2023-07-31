
List<Product> products = new List<Product>(); // Store products

void HandleSelection(Table table ) // Handle user selections
{
    Console.ForegroundColor = ConsoleColor.Blue;

    Console.WriteLine("To enter a new product - enter \"P\" | To search for a product - enter: \"S\" | To quit enter: \"Q\"");

    Console.ResetColor();

    CheckInput(Console.ReadLine(), table);
}

void SearchProduct(Table table) // Search Product
{
    Console.Write("Enter a Product Name: ");

    string inputName = Console.ReadLine();

    if (string.IsNullOrEmpty(inputName))
    {
        Console.WriteLine("You didn't enter anything.");

        HandleSelection(table);
    }
    else
    {
        table.SearchRow(inputName);

        table.Output();

        HandleSelection(table);
    }

}

void PresentProducts() // Presents a list
{
    if (products.Count == 0)
    {
        Console.WriteLine("No product has been created.");

        UserInputs();
    }
    else
    {
        List<Product> sortedProducts = products.OrderBy(product => product.Price).ToList();

        Table table = new Table(sortedProducts);

        table.Output();

        HandleSelection(table);
    }
}

void CheckInput(string input, Table table) // Check user input
{
    switch (input.ToUpper())
    {
        case "":
            Console.WriteLine("You didn't enter anything.");
            HandleSelection(table);
            return;

        case "Q":

            PresentProducts();

            return;

        case "S": SearchProduct(table);
            return;

        case "P":

            UserInputs();

            return;

        default: return;
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

        string catergory = Console.ReadLine();

        if (string.IsNullOrEmpty(catergory))
        {
            Console.WriteLine("You didn't enter anything.");

            continue;
        }
        if (catergory.ToUpper() == "Q")
        {
            PresentProducts();

            break;
        }

        Console.Write("Enter a Product Name: ");

        string productName = Console.ReadLine();

        if (string.IsNullOrEmpty(productName))
        {
            Console.WriteLine("You didn't enter anything.");

            continue;
        }
        if (productName.ToUpper() == "Q")
        {   
            PresentProducts();

            break;
        }
        Console.Write("Enter a Price: ");

        string inputPrice = Console.ReadLine();

        if (string.IsNullOrEmpty(inputPrice))
        {
            Console.WriteLine("You didn't enter anything.");

            continue;
        }
        if (inputPrice.ToUpper() == "Q")
        {
            PresentProducts();

            break;
        }
        int price = Convert.ToInt32(inputPrice);

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
    private List<Product> products; // Hold the state or data associated with the object.

    private string highlightRow; // Saved reference to highlight the searched item in the displayed list

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

                Console.WriteLine("| {0} | {1} | {2} |", product.Category.PadRight(12), product.ProductName.PadRight(12), product.Price.ToString().PadLeft(9));

                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("| {0} | {1} | {2} |", product.Category.PadRight(12), product.ProductName.PadRight(12), product.Price.ToString().PadLeft(9));
            }
        }
        int sum = products.Sum(product => product.Price);

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("|                Total amount:  {0}       |", sum);
        Console.WriteLine("-------------------------------------------");

    }
    public void SearchRow(string userInput) //  Create reference to highlight row
    {
        highlightRow = userInput;
    }
}