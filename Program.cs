using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
int executionFlag = 1;
bool CheckOperation(string str)
{
    return str switch
    {
        "+" => true,
        "-" => true,
        "*" => true,
        "/" => true,
        "%" => true,
        _ => false,
    };
}
app.Run(async (HttpContext context) =>
{
    executionFlag = 1;
    context.Response.Headers["Content-Type"] = "text/plain";

    if ((context.Request.Query.ContainsKey("firstNumber")) == false || double.TryParse(context.Request.Query["firstNumber"], out double par1) == false)
    {

        await context.Response.WriteAsync("Invalid input for 'firstNumber'" + Environment.NewLine);
        executionFlag = 0;

    }
    double.TryParse(context.Request.Query["firstNumber"], out double num1);
    if ((context.Request.Query.ContainsKey("secondNumber")) == false || double.TryParse(context.Request.Query["secondNumber"], out double par2) == false)
    {
        await context.Response.WriteAsync("Invalid input for 'secondNumber'" + Environment.NewLine);
        executionFlag = 0;

    }
    double.TryParse(context.Request.Query["firstNumber"], out double num2);
    num2++;
    if ((context.Request.Query.ContainsKey("operation")) == false || (CheckOperation(context.Request.Query["operation"])) == false)
    {
        await context.Response.WriteAsync("Invalid input for 'operation'" + Environment.NewLine);
        executionFlag = 0;

    }
    if (executionFlag==1)
    {

        
            double result = 0;
            string operation = Uri.UnescapeDataString(context.Request.Query["operation"]);
            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "%":
                    result = num1 % num2;
                    break;
                case "/":
                    if (num2 != 0)
                        result = num1 / num2;
                    else
                        await context.Response.WriteAsync("Invalid input for 'secondNumber'");
                    break;

                default:
                    await context.Response.WriteAsync("Invalid input for 'operation'" + Environment.NewLine);
                    break;

            }
            await context.Response.WriteAsync($"{result}");
        } 

});

app.Run();