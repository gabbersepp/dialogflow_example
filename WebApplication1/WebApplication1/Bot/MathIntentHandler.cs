using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WebApplication1.Bot
{
    public class MathIntentHandler : IBotIntentHandler
    {
        public string GetMessage(BotResponse response)
        {
            var ctx = response.Result.Contexts.First(x => x.name == "execute_math_calc");
            var z1 = ctx.parameters["math_binary_operand_left"];
            var z2 = ctx.parameters["math_binary_operand_right"];
            var op = ctx.parameters["math_operator_binary_operator"];

            return $"Das Ergebnis von {z1} {Operator.Map(op)} {z2} ist " + Operator.TryParse(z1, z2, op);
        }

        public bool IsValid(BotResponse response)
        {
            var z2 = response.Result.Contexts.First(x => x.name == "execute_math_calc").parameters["math_binary_operand_right"];
            if (z2 == "0")
            {
                return false;
            }

            return true;
        }

        public List<BotResponseContext> GetValidContextsFromCompleteIntent(BotResponse response)
        {
            var c1 = new BotResponseContext
            {
                name = "testcontext",
                parameters = new Dictionary<string, string>
                {
                    {response.Result.Parameters.Keys.ToList()[0],  response.Result.Parameters.Values.ToList()[0]},
{response.Result.Parameters.Keys.ToList()[0] + ".original",  response.Result.Parameters.Values.ToList()[0]},

                    {response.Result.Parameters.Keys.ToList()[1],  ""},
{response.Result.Parameters.Keys.ToList()[1] + ".original",  ""},

                    {response.Result.Parameters.Keys.ToList()[2],  response.Result.Parameters.Values.ToList()[2]},
{response.Result.Parameters.Keys.ToList()[2] + ".original",  response.Result.Parameters.Values.ToList()[2]},


                }
            };
            var contexts = new List<BotResponseContext>();
            contexts.Add(c1);
            return contexts;
        }

        public Dialog GetDialog(BotResponse response)
        {
            return null;
        }

        public class Operator
        {
            private static Dictionary<string, Func<double, double, double>> dict = new Dictionary<string, Func<double, double, double>>
            {
                {"+", (a, b) => a + b},
                {"-", (a, b) => a - b},
                {"/", (a, b) => a / b},
                {"*", (a, b) => a * b}
            };

            public static double? TryParse(string z1, string z2, string op)
            {
                z1 = z1.Replace(",", ".");
                z2 = z2.Replace(",", ".");

                double z1Parsed;
                double z2Parsed;
                double result;

                if (!Double.TryParse(z1, NumberStyles.Any, CultureInfo.InvariantCulture, out z1Parsed))
                {
                    return null;
                }

                if (!Double.TryParse(z2, NumberStyles.Any, CultureInfo.InvariantCulture, out z2Parsed))
                {
                    return null;
                }

                op = Map(op.Trim());
                if (!dict.ContainsKey(op))
                {
                    return null;
                }

                return dict[op](z1Parsed, z2Parsed);
            }

            public static string Map(string word)
            {
                word = word.ToLower();
                if (word.Contains("summ"))
                {
                    return "+";
                }

                if (word.Contains("multipl") || word == "produkt")
                {
                    return "*";
                }

                if (word.Contains("divi") || word == "quotient")
                {
                    return "/";
                }

                if (word.Contains("subtra"))
                {
                    return "-";
                }

                return word;
            }
        }
    }
}