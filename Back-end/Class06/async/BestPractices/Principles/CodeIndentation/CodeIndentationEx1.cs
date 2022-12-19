namespace Example.Principles.CodeIndentation;

public class CodeIndentationEx1
{
    void Before(string param)
    {
        if (param.Contains(','))
        {
            if (param.Split(',').Length > 3)
            {
                PerformGreaterTask(param);
            }
        }
    }

    private static void PerformGreaterTask(string param)
    {
        var arrayOfValues = param.Split(',');
        while (arrayOfValues.Any(v => v.Contains("!")))
        {
            // do
            // something
            // complicated
            // and
            // late
        }
    }
}