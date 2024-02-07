namespace MyGame;

public class Rolling
{
    public List<int> Roll(int count)
    {
        List<int> result = new List<int>();

        Random rnd = new Random();

        result.Add( rnd.Next(1, 7) );
        result.Add( rnd.Next(1, 7) );

        return result;
    }
}