using CsvHelper;
using System.Globalization;
using TransformDataApp;
using TransformDataApp.Data.Context;
using TransformDataApp.Data.Entities;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            string CSVfilePath = "path to your CSV file";
            var records = ReadCSV(CSVfilePath);

            if (records is not null && records.Any())
            {
                var uniqueRecords = RemoveDublicates(records);

                using (var db = new MyAppContext())
                {
                    await db.StoredEntities.AddRangeAsync(uniqueRecords.ToStoredEntities());
                    Console.WriteLine("Writing data into Database");
                    await db.SaveChangesAsync();
                    Console.WriteLine("Data was written successfully.");
                }
            }
            else
            {
                Console.WriteLine("No records found in the CSV file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static IEnumerable<CSVEntity> ReadCSV(string filePath)
    {
        Console.WriteLine("Started reading data from CSV file");

        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CSVEntity>();
                Console.WriteLine("Successfully got records from CSV file");
                return records.ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
            return null;
        }
    }

    static IEnumerable<CSVEntity> RemoveDublicates(IEnumerable<CSVEntity> records)
    {
        var groupedRecords = records.GroupBy(r => new { r.tpep_pickup_datetime, r.tpep_dropoff_datetime, r.passenger_count });
        var uniqueRecords = groupedRecords.Select(g => g.First()).ToList();
        Console.WriteLine("Removing dublicates");

        string dublicatesFilePath = "../../../dublicates.csv";
        WriteDuplicatesToCSV(dublicatesFilePath, records.Except(uniqueRecords));

        return uniqueRecords;
    }

    static void WriteDuplicatesToCSV(string filePath, IEnumerable<CSVEntity> duplicates)
    {
        Console.WriteLine($"start write dublicates to file. File path : {filePath}");
        try
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(duplicates);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing duplicates to CSV file: {ex.Message}");
        }

    }
}
