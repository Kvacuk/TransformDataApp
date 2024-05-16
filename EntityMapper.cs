using TransformDataApp.Data.Entities;

namespace TransformDataApp
{
    public static class EntityMapper
    {
        public static StoredEntity ToStoredEntity(this CSVEntity csv)
        {
            return new StoredEntity
            {
                tpep_pickup_datetime = csv.tpep_pickup_datetime.ToUniversalTime(),
                tpep_dropoff_datetime = csv.tpep_dropoff_datetime.ToUniversalTime(),
                passenger_count = csv.passenger_count,
                trip_distance = csv.trip_distance,
                store_and_fwd_flag = (csv.store_and_fwd_flag == "N") ? "No" : "Yes",
                PULocationID = csv.PULocationID,
                DOLocationID = csv.DOLocationID,
                fare_amount = csv.fare_amount,
                tip_amount = csv.tip_amount,
            };
        }

        public static IEnumerable<StoredEntity> ToStoredEntities(this IEnumerable<CSVEntity> cSVEntities)
        {
            return cSVEntities.Select(c => c.ToStoredEntity()).ToList();
        }
    }
}
