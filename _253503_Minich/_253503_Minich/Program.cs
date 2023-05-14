public class Room
{
    private int number;
    private decimal price;
    private bool occupied;

    public Room(int number, decimal price)
    {
        this.number = number;
        this.price = price;
        this.occupied = false;
    }
    public int GetNumber()
    {
        return number;
    }
    public decimal GetPrice()
    {
        return price;
    }

    public bool IsOccupied()
    {
        return occupied;
    }
    public void SetOccupied()
    {
        occupied = true;
    }

    public void SetUnoccupied()
    {
        occupied = false;
    }
}
class Customer
{
    private string firstName;
    private string lastName;
    private int roomNumber;

    public Customer(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.roomNumber = -1;
    }
    public string GetLastName()
    {
        return lastName;
    }

    public int GetRoomNumber()
    {
        return roomNumber;
    }
    public void SetRoomNumber(int roomNumber)
    {
        this.roomNumber = roomNumber;
    }
}
class Hotel
{
    private List<Room> rooms;
    private List<Customer> customers;

    public Hotel()
    {
        rooms = new List<Room>();
        customers = new List<Customer>();
    }
    public void AddRoom(int number, decimal price)
    {
        Room room = new Room(number, price);
        rooms.Add(room);
    }
    public List<Room> GetAvailableRooms()
    {
        List<Room> availableRooms = new List<Room>();

        foreach (Room room in rooms)
        {
            if (!room.IsOccupied())
            {
                availableRooms.Add(room);
            }
        }

        return availableRooms;
    }
    public List<Customer> GetCustomers()
    {
        return customers;
    }
    private Room? GetRoomByNumber(int roomNumber)
    {
        foreach(Room room in rooms)
        {
            if (room.GetNumber() == roomNumber)
            {
                return room;
            }
        }
        return null;
    }
    public void ReserveRoom(int roomNumber, Customer customer)
    {
        Room? room = GetRoomByNumber(roomNumber);

        if (room == null)
        {
            Console.WriteLine("Комната не найдена");
            return;
        }

        if (room.IsOccupied())
        {
            Console.WriteLine("Комната уже занята");
            return;
        }

        room.SetOccupied();
        customer.SetRoomNumber(roomNumber);
        customers.Add(customer);
    }
    public decimal GetPriceForCustomer(Customer customer)
    {
        Room? room = GetRoomByNumber(customer.GetRoomNumber());
        if (room == null)
        {
            return 0;
        }

        return room.GetPrice();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Hotel hotel = new Hotel();
        hotel.AddRoom(101, 200);
        hotel.AddRoom(102, 200);
        hotel.AddRoom(103, 200);
        hotel.AddRoom(104, 200);
        Console.WriteLine("Список доступных номеров:");
        List<Room> availableRooms = hotel.GetAvailableRooms();
        foreach (Room room in availableRooms)
        {
            Console.WriteLine($"Номер: {room.GetNumber()}, Стоимость: {room.GetPrice()}");
        }
        Console.WriteLine();
        Console.Write("Введите имя клиента: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Введите фамилию клиента: ");
        string lastName = Console.ReadLine()!;
        Console.Write("Введите номер комнаты: ");
        int roomNumber = int.Parse(Console.ReadLine()!);
        bool isHere = false;
        foreach(Room room in availableRooms)
        {
            if (roomNumber == room.GetNumber())
            {
                isHere = true;
                break;
            }
        }
        if (isHere)
        {
            Customer customer = new Customer(firstName, lastName);
            hotel.ReserveRoom(roomNumber, customer);
            Console.WriteLine($"Комната номер {roomNumber} забронирована для {customer.GetLastName()}");
            Console.WriteLine();
            Console.Write("Введите фамилию клиента для проверки стоимости проживания: ");
            string customerLastName = Console.ReadLine()!;
            foreach (Customer c in hotel.GetCustomers())
            {
                if (c.GetLastName() == customerLastName)
                {
                    decimal totalPrice = hotel.GetPriceForCustomer(c);
                    Console.WriteLine($"Стоимость проживания для {c.GetLastName()}: {totalPrice}");
                }
            }
        }
        else
        {
            Console.WriteLine("Такой комнаты нет");
        }
    }
}