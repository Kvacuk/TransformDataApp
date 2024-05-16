
using CsvHelper;
using System.Globalization;
using TransformDataApp.Entities;

using (var reader = new StreamReader("https://drive.google.com/file/d/1l2ARvh1-tJBqzomww45TrGtIh5j8Vud4/view?usp=sharing"))
    using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture) )
{
    var records = csv.GetRecord<CSVEntity>();

}