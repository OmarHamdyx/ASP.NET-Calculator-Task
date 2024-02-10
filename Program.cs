using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    bool CheckOperation(string str)
    {
        return str switch
        {
            "+" => true,
            "-" => true,
            "*" => true,
            "/" => true,
            _ => false,
        };
    }
    context.Response.Headers["Content-Type"] = "text/plain";

    if (context.Request.Query.ContainsKey("firstNumber") && context.Request.Query.ContainsKey("secondNumber") && context.Request.Query.ContainsKey("operation"))
    {
        if (double.TryParse(context.Request.Query["firstNumber"], out double num1) && double.TryParse(context.Request.Query["secondNumber"], out double num2) && CheckOperation(context.Request.Query["operation"]))
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
        else
        {
            if (double.TryParse(context.Request.Query["firstNumber"], out double generic) == false)
            {
                await context.Response.WriteAsync("Invalid input for 'firstNumber'" + Environment.NewLine);

            }
            if (double.TryParse(context.Request.Query["secondNumber"], out double generic2) == false)
            {
                await context.Response.WriteAsync("Invalid input for 'secondNumber'" + Environment.NewLine);
            }
            if ((CheckOperation(context.Request.Query["operation"])) == false)
            {
                await context.Response.WriteAsync("Invalid input for 'operation'" + Environment.NewLine);
            }

        }

    }
    else
    {
        if ((context.Request.Query.ContainsKey("firstNumber")) == false || double.TryParse(context.Request.Query["firstNumber"], out double generic) == false)
        {
            await context.Response.WriteAsync("Invalid input for 'firstNumber'" + Environment.NewLine);

        }
        if ((context.Request.Query.ContainsKey("secondNumber")) == false || double.TryParse(context.Request.Query["secondNumber"], out double generic2) == false)
        {
            await context.Response.WriteAsync("Invalid input for 'secondNumber'" + Environment.NewLine);
        }
        if ((context.Request.Query.ContainsKey("operation")) == false || (CheckOperation(context.Request.Query["operation"])) == false)
        {
            await context.Response.WriteAsync("Invalid input for 'operation'" + Environment.NewLine);
        }

    }


});


app.Run();
