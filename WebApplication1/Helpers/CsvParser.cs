namespace WebApplication1.Helpers;

internal sealed class CsvParser
{
    private readonly TextReader reader;
    public CsvParser(TextReader reader){
        this.reader = reader;
    }
    
    public List<string[]> Parse() {
        var data = new List<string[]>();
        while (reader.ReadLine() is { } line) 
        {
            var fields = line.Split(',');
            data.Add(fields);
        }
        return data;
    }
    
    /// <summary>
    /// Another option to use but you can't use ReadonlySpan<char> with this method
    /// </summary>
    /// <returns></returns>
    // public async Task<List<string[]>> ParseAsync() {
    //     var data = new List<string[]>();
    //     while (await reader.ReadLineAsync() is { } line) 
    //     {
    //         var fields = line.Split(',');
    //         data.Add(fields);
    //     }
    //     return data;
    // }
}