using HedgehogsPopulation.Shared;

public class Program
{
    private static (int, Error?) GetMinAmountOfMeetings(List<int> population, HedgehogColors targetColor)
    {
        var targetColorInFormat = Convert.ToInt32(targetColor);
        
        if (population[targetColorInFormat] == population[0] + population[1] + population[2])
        {
            return (-1, Errors.SameColor());
        }

        var isAllEven = population.All(p => p % 2 == 0);
        var isAllOdd = population.All(p => p % 2 != 0);

        if (!isAllEven && !isAllOdd)
        {
            return (-1, Errors.ValuesIsEverOrOdd());
        }

        var minimalMeetings = 0;
        
        while (population[0] > 0 && population[1] > 0 && population[2] > 0)
        {
            if (population[0] >= 2 && population[1] >= 2)
            {
                population[0] -= 2;
                population[1] -= 2;
                population[2] += 2;
                minimalMeetings++;
            }
            else if (population[0] >= 2 && population[2] >= 2)
            {
                population[0] -= 2;
                population[2] -= 2;
                population[1] += 2;
                minimalMeetings++;
            }
            else if (population[1] >= 2 && population[2] >= 2)
            {
                population[1] -= 2;
                population[2] -= 2;
                population[0] += 2;
                minimalMeetings++;
            }
            else
            {
                break;
            }
        }

        return (minimalMeetings, null);
    }

    private static (int, Error?) IsValidData(object? data, HedgehogColors color)
    {
        return data switch
        {
            null => (-1, Errors.ValueIsInvalid($"{color} amount")),
            string dataStr when int.TryParse(dataStr, out var colorsAmount) =>
                colorsAmount < 0
                    ? (-1, Errors.ValueIsInvalid($"{color} amount"))
                    : (colorsAmount, null),
            _ => (-1, Errors.ValueIsInvalid($"{color} amount"))
        };
    }
    
    private static int GetInputData(HedgehogColors color)
    {
        int hedgehogsAmount;

        while (true)
        {
            Console.Write($"Please enter the number of {color.ToString().ToLower()} hedgehogs: ");
            
            var validationResult = IsValidData(Console.ReadLine(), color);

            if (validationResult.Item2 != null)
            {
                Console.WriteLine($"Error: {validationResult.Item2.Message}");
                continue;
            }
            
            hedgehogsAmount = validationResult.Item1;
            
            break;
        }

        return hedgehogsAmount;
    }
    
    private static void Terminal()
    {
        Console.WriteLine();
        
        var redHedgehogsAmount = GetInputData(HedgehogColors.Red);
        var greenHedgehogsAmount = GetInputData(HedgehogColors.Green);
        var blueHedgehogsAmount = GetInputData(HedgehogColors.Blue);

        List<int> hedgehogsPopulation = [redHedgehogsAmount, greenHedgehogsAmount, blueHedgehogsAmount];
        
        Console.Write("\nDo you want to pick a particular colour you want? (1 - Yes, 2 - No): ");
        
        int userChoice;

        do
        {
            var data = Console.ReadLine();

            if (int.TryParse(data, out userChoice) && userChoice is 1 or 2)
            {
                continue;
            }
            
            Console.WriteLine(Errors.ValueIsInvalid("Your choice").Message);
            Console.WriteLine("Please, try again!");
            
        } while (userChoice != 1 && userChoice != 2);


        var targetColor = HedgehogColors.Blue;
        
        if (userChoice == 1)
        {
            Console.Write("Choose the target color (0 - Red, 1 - Green, 2 - Blue): ");
            
            do
            {
                var colorChoice = Console.ReadLine();

                if (!int.TryParse(colorChoice, out var color) || color < 0 || color > 2)
                {
                    Console.WriteLine(Errors.ValueIsInvalid("Target color").Message);
                }
                else
                {
                    targetColor = (HedgehogColors)color;
                    break;
                }
                
            } while (true);
        }
        
        var minimalMeetings = GetMinAmountOfMeetings(hedgehogsPopulation, targetColor);

        if (minimalMeetings.Item2 != null)
        {
            Console.WriteLine($"There are some errors: {minimalMeetings.Item2.Message}");
            return;
        }
        
        Console.WriteLine($"Minimum meetings required: {minimalMeetings.Item1}.");
    }
    
    public static void Main()
    {
        Terminal();
    }
}