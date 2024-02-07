using MyGame;

namespace MyGameTest;

public class Tests
{
   
    /// <summary>
    /// Тест проверки метода AddWithInc
    /// </summary>
    [TestMethod]
    public void RollTest1()
    {

        Rolling rolling = new Rolling();


       
        // assert

        Console.WriteLine(rolling.Roll(4));
    }
}
