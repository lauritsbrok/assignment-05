namespace GildedRose.Tests;

public class ProgramTests
{
    [Fact]
    public void TestTheTruth()
    {
        true.Should().BeTrue();
    }

    [Fact]
    public async Task AfterExecutionConsoleOutputShouldBeCorrect()  {

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        Program.Main(new string[0]);

        string text = stringWriter.GetStringBuilder().ToString();

        await File.WriteAllTextAsync("actual.txt", text);

        var act = File.ReadAllBytes("actual.txt");
        var exp = File.ReadAllBytes("../../../expected.txt");

        act.Should().BeEquivalentTo(exp);
    }
}