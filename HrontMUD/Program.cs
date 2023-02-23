namespace HrontMUD
{
    internal class Program
    {
        static void Main()
        {
            Server server = new();
            Task.Run(server.ListenAsync);
            Console.ReadLine();
        }
    }
}