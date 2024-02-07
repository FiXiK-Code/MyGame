using MyGame;

namespace Testing;

public class Tests
{
   
    [Test]
    public void Test1()
    {
        Rolling rol = new Rolling();

        foreach(var elrm in rol.Roll(2))
            Console.WriteLine(elrm);

        Console.WriteLine("\n");

        foreach (var elrm in rol.Roll(2))
            Console.WriteLine(elrm);
    }
}
